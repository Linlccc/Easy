using System.Collections.Concurrent;
using System.Reflection;
using Easy.Extensions.DependencyInjection.Abstractions;
using Easy.Extensions.DependencyInjection.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 服务注册器
/// </summary>
internal sealed class EasyServicesRegistrar
{
    /// <summary>
    /// 依赖注入扫描程序集集合
    /// </summary>
    private readonly ConcurrentDictionary<Assembly, Assembly> _dIAssemblies = new();

    /// <summary>
    /// 不进行注册的服务类型
    /// </summary>
    private readonly Type[] _notRegisterServiceTypes =
    {
        typeof(IDisposable),
        typeof(IAsyncDisposable),
        typeof(System.Collections.IEnumerable),
        typeof(IEnumerable<>),
        typeof(System.Collections.IList),
        typeof(System.Collections.ICollection)
    };

    /// <summary>
    /// 初始化服务注册器
    /// </summary>
    internal EasyServicesRegistrar(IEnumerable<Assembly> assemblys) => AddAssemblies(assemblys);

    /// <summary>
    /// 添加程序集，如果程序集存在将覆盖
    /// </summary>
    /// <param name="assemblys">程序集</param>
    /// <returns>this</returns>
    internal EasyServicesRegistrar AddAssemblies(IEnumerable<Assembly> assemblys)
    {
        if (assemblys.IsNullOrEmpty()) return this;
        foreach (Assembly assembly in assemblys) _dIAssemblies.AddOrUpdate(assembly, assembly, (_, _) => assembly);
        return this;
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <exception cref="ArgumentNullException">服务为空</exception>
    internal IServiceCollection Registrar(IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        // 扫描注册普通注册类型
        ScanRegisterServiceTypes(services);

        // 扫描注册工厂注册类型
        ScanFactoryRegisterServiceTypes(services);

        // 清除程序集引用
        _dIAssemblies.Clear();

        return services;
    }


    #region 私有方法
    /// <summary>
    /// 扫描注册服务
    /// </summary>
    /// <param name="services">服务集合</param>
    private void ScanRegisterServiceTypes(IServiceCollection services)
    {
        // 得到要注册的类型和注册信息
        IEnumerable<(Type ImplementationType, RegisterAttribute RegisterInfo)> registerTypes = _dIAssemblies.Values.SelectMany(assembly => assembly.ExportedTypes
            .Where(type => type.IsClass && !type.IsAbstract && type.IsDefined(typeof(RegisterAttribute), false))
            .Select(type => (ImplementationType: type, RegisterInfo: type.GetAttribute<RegisterAttribute>(false)!)));

        foreach ((Type ImplementationType, RegisterAttribute RegisterInfo) in registerTypes)
        {
            // 得到要注册为服务的类型，【如果是开放泛型 就根据定义判断 也获取定义】
            List<Type> serviceTypes = serviceTypes = RegisterInfo.ServiceTypes
            .Where(type => type.IsOpenGeneric() ? ImplementationType.IsInheritFrom(type) : type.IsAssignableFrom(ImplementationType))
            .Select(type => type.IsOpenGeneric() ? type.GetTypeDefinition() : type).ToList();
            // 如果自定义没有就找接口。从接口获取类型，【如果是开放泛型就获取类型定义】
            if (serviceTypes.IsNullOrEmpty()) serviceTypes = ImplementationType.GetInterfaces()
                 .Where(type => !_notRegisterServiceTypes.Any(ntst => ntst == type.GetTypeDefinition()))
                 .Select(type => type.IsOpenGeneric() ? type.GetTypeDefinition() : type).ToList();

            // 如果没有服务类型可注册，以自己为服务类型注册
            if (serviceTypes.IsNullOrEmpty()) serviceTypes.Add(ImplementationType);
            // 如果以key不为空，添加代理类型
            if (!RegisterInfo.ServiceKey.IsNullOrEmpty()) serviceTypes.AddRange(serviceTypes.Select(st => st.Proxy(RegisterInfo.ServiceKey)).ToArray());

            foreach (Type serviceType in serviceTypes) services.Add(ServiceDescriptor.Describe(serviceType, ImplementationType, RegisterInfo.ServiceLifetime));
        }
    }

    /// <summary>
    /// 扫描工厂注册服务
    /// </summary>
    /// <param name="services">服务集合</param>
    private void ScanFactoryRegisterServiceTypes(IServiceCollection services)
    {
        // 得到要使用工厂注册的类型
        Type iRegisterFactoryType = typeof(IRegisterFactory<,>);
        IEnumerable<Type> factoryRegisterTypes = _dIAssemblies.Values.SelectMany(assembly => assembly.ExportedTypes
            .Where(type => type.IsClass && !type.IsAbstract && type.IsInterfaceDefinitionInclude(iRegisterFactoryType)));

        TypeInfo iRegisterFactoryTypeInfo = iRegisterFactoryType.GetTypeInfo();
        // 得到通用实例工厂方法名
        string universalImplementationFactoryMethodName = iRegisterFactoryTypeInfo.DeclaredMethods.First(dm => dm.GetParameters()[0].ParameterType == typeof(IServiceProvider)).Name;
        // 得到通用服务Key方法名
        string universalServiceKeyMethodName = iRegisterFactoryTypeInfo.DeclaredMethods.First(dm => dm.GetParameters().Length == 0).Name;

        factoryRegisterTypes.ToList().ForEach(implementationType =>
        {
            // 得到所有工厂接口
            IEnumerable<Type> factoryInterfaces = implementationType.GetInterfaces().Where(type => type.IsGenericType && type.GetGenericTypeDefinition() == iRegisterFactoryType);
            foreach (Type factoryInterface in factoryInterfaces)
            {
                // 得到服务类型和生命周期
                Type[] arguments = factoryInterface.GetGenericArguments();
                // 得到服务类型，如果是 object 就以自己为服务注册
                Type serviceType = arguments[0];
                // 如果当前类型没有实现服务类型，跳过
                if (!serviceType.IsAssignableFrom(implementationType)) continue;
                // 得到生命周期
                ServiceLifetime serviceLifetime = ServiceLifetimeExtension.GetLifetime(arguments[1]);

                // 得到接口完全限定名
                string factoryInterfaceFullName = factoryInterface.FullName();
                // 得到专属实例工厂方法名
                string implementationFactoryMethodName = $"{factoryInterfaceFullName}.{universalImplementationFactoryMethodName}";
                // 得到创建实例方法
                TypeInfo implementationTypeInfo = implementationType.GetTypeInfo();
                MethodInfo? createImplementationMethod = implementationTypeInfo.GetDeclaredMethods(implementationFactoryMethodName).FirstOrDefault(m => m.GetParameters()[0].ParameterType == typeof(IServiceProvider));
                if (createImplementationMethod is null) createImplementationMethod = implementationTypeInfo.GetDeclaredMethods(universalImplementationFactoryMethodName).FirstOrDefault(m => m.GetParameters()[0].ParameterType == typeof(IServiceProvider));
                if (createImplementationMethod is null) continue;
                // 将创建实例的方法做成委托
#if NET5_0 || NET6_0
                Func<IServiceProvider, object> implementationFactory = createImplementationMethod.CreateDelegate<Func<IServiceProvider, object>>(Convert.ChangeType(default, implementationType));
#else
                Func<IServiceProvider, object> implementationFactory = (Func<IServiceProvider, object>)createImplementationMethod.CreateDelegate(typeof(Func<IServiceProvider, object>), Convert.ChangeType(default, implementationType));
#endif

                // 如果服务类型是 object 就以自己为服务类型注册
                if (serviceType == typeof(object)) serviceType = implementationType;
                // 如果是开放泛型获取类型定义
                if (serviceType.IsOpenGeneric()) serviceType = serviceType.GetTypeDefinition();
                services.Add(ServiceDescriptor.Describe(serviceType, useEasyimplementationFactory, serviceLifetime));

                // 得到服务Key
                string serviceKeyMethodName = $"{factoryInterfaceFullName}.{universalServiceKeyMethodName}";
                MethodInfo? serviceKeyMethod = implementationTypeInfo.GetDeclaredMethods(serviceKeyMethodName).FirstOrDefault(dm => dm.GetParameters().Length == 0);
                if (serviceKeyMethod is null) continue;
#if NET5_0 || NET6_0
                string? serviceKey = serviceKeyMethod.CreateDelegate<Func<string>>(Convert.ChangeType(default, implementationType))();
#else
                string? serviceKey = ((Func<string>)serviceKeyMethod.CreateDelegate(typeof(Func<string>), Convert.ChangeType(default, implementationType)))();
#endif

                // 使用key注册服务
                if (!serviceKey.IsNullOrEmpty()) services.Add(ServiceDescriptor.Describe(serviceType.Proxy(serviceKey), useEasyimplementationFactory, serviceLifetime));


                // 使用Easy的服务提供商
                object useEasyimplementationFactory(IServiceProvider serviceProvider) => implementationFactory(serviceProvider.GetRequiredService<IServiceProvider>());
            }
        });
    }
    #endregion
}
