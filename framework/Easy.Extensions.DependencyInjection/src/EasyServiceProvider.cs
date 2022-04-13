using System.Collections.Concurrent;
using System.Reflection;
using Easy.Extensions.DependencyInjection.Abstractions;
using Easy.Extensions.DependencyInjection.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 服务提供商容器
/// </summary>
public sealed class EasyServiceProvider : IServiceProvider, ISupportRequiredService, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// 根范围服务提供商
    /// </summary>
    private readonly EasyServiceProviderScope _rootServiceProvider;

    /// <summary>
    /// 真实服务提供商
    /// </summary>
    private readonly ServiceProvider _realServiceProvider;

    /// <summary>
    /// 真是服务提供商范围的类型
    /// </summary>
    private readonly Type _serviceProviderEngineScopeType;

    /// <summary>
    /// 服务提供商字典
    /// </summary>
    internal readonly ConcurrentDictionary<IServiceProvider, EasyServiceProviderScope> _serviceProviders = new();

    private bool _disposed;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceDescriptors">服务描述集合</param>
    /// <param name="serviceProviderOptions">服务提供商配置</param>
    /// <param name="holdDefaultServiceProvider">是否保留默认服务提供商</param>
    /// <exception cref="InvalidOperationException">永远不该出现的错误</exception>
    internal EasyServiceProvider(IServiceCollection serviceDescriptors, ServiceProviderOptions? serviceProviderOptions, bool holdDefaultServiceProvider)
    {
        // 注册默认 IServiceProvider
        serviceDescriptors.AddScoped<IServiceProvider>(service => GetOrCreate( service, false));
#if DEBUG
        // 如果不添加，获取默认的服务商会被 IsService 筛掉(只针对测试环境)
        serviceDescriptors.AddScoped(typeof(IServiceProvider).DefaultProxy(), service => string.Empty);
        serviceDescriptors.AddSingleton(typeof(IServiceScopeFactory).DefaultProxy(), service => string.Empty);
#endif

        _realServiceProvider = serviceDescriptors.BuildServiceProvider(serviceProviderOptions ?? new ServiceProviderOptions());

        // 设置服务提供商范围
        if (typeof(ServiceProvider).GetProperty("Root", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_realServiceProvider) is not IServiceProvider realServiceProviderScope) throw new InvalidOperationException($"{nameof(realServiceProviderScope)} 永远不应为空");
        _rootServiceProvider = GetOrCreate( realServiceProviderScope, true);
        _serviceProviderEngineScopeType = realServiceProviderScope.GetType();

        // 替换默认服务提供商
        ReplaceDefaultProvider(holdDefaultServiceProvider, realServiceProviderScope);
    }

    /// <summary>
    /// 获取服务实例
    /// </summary>
    /// <param name="serviceType">服务类型</param>
    /// <returns></returns>
    public object? GetService(Type serviceType) => GetService(_realServiceProvider, serviceType);

    /// <summary>
    /// 获取所需服务
    /// </summary>
    /// <param name="serviceType">服务类型</param>
    /// <returns></returns>
    public object GetRequiredService(Type serviceType) => GetRequiredService(_realServiceProvider, serviceType);

    /// <summary>
    /// 创建范围
    /// </summary>
    /// <returns></returns>
    internal IServiceScope CreateScope() => GetOrCreate((IServiceProvider)Activator.CreateInstance(_serviceProviderEngineScopeType, _realServiceProvider, false)!, false);

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _realServiceProvider.Dispose();
        _rootServiceProvider.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        if (_disposed) return default;
        _disposed = true;
        _realServiceProvider.DisposeAsync().GetAwaiter();
        return _rootServiceProvider.DisposeAsync();
    }


    #region 静态方法
    internal static object GetRequiredService(IServiceProvider serviceProvider, Type serviceType)
    {
        object? result = GetService(serviceProvider, serviceType);
        if (result is null) throw new InvalidOperationException($"没有注册\"{nameof(serviceType)}\"类型的服务");
        return result;
    }

    internal static object? GetService(IServiceProvider serviceProvider, Type serviceType)
    {
        object? result = serviceProvider.GetService(serviceType);

        // 属性注入
        PropertyInject(result, serviceProvider);
        // 字段注入
        FieldInject(result, serviceProvider);

        return result;
    }

    /// <summary>
    /// 属性注入
    /// </summary>
    /// <param name="instance">需要属性注入的实例</param>
    /// <param name="serviceProvider">属性注入的服务提供商</param>
    /// <returns></returns>
    private static object? PropertyInject(object? instance, IServiceProvider serviceProvider)
    {
        if (instance is null) return instance;

        // 得到要注入的属性
        IEnumerable<(PropertyInfo Property, InjectAttribute Inject)> injectPropertyInfos = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => (p.PropertyType.IsClass || p.PropertyType.IsInterface) && p.IsDefined(typeof(InjectAttribute), false))
            .Select(p => (p, p.GetAttribute<InjectAttribute>(false)!));

        // 对属性赋值
        foreach ((PropertyInfo Property, InjectAttribute Inject) in injectPropertyInfos)
        {
            // 得到获取实例的服务类型
            Type serviceType = Inject.Key.IsNullOrEmpty() ? Property.PropertyType : Property.PropertyType.Proxy(Inject.Key);
            // 获取服务
            if (Inject.Require) Property.SetPropertyValue(instance, GetRequiredService(serviceProvider, serviceType));
            else Property.SetPropertyValue(instance, GetService(serviceProvider, serviceType));
        }

        return instance;
    }

    /// <summary>
    /// 字段注入
    /// </summary>
    /// <param name="instance">需要属性注入的实例</param>
    /// <param name="serviceProvider">属性注入的服务提供商</param>
    /// <returns></returns>
    private static object? FieldInject(object? instance, IServiceProvider serviceProvider)
    {
        if (instance is null) return instance;

        IEnumerable<(FieldInfo Field, InjectAttribute Inject)> injectFieldInfos = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => (f.FieldType.IsClass || f.FieldType.IsInterface) && f.IsDefined(typeof(InjectAttribute), false))
            .Select(f => (f, f.GetAttribute<InjectAttribute>(false)!));

        // 对属性赋值
        foreach ((FieldInfo Field, InjectAttribute Inject) in injectFieldInfos)
        {
            // 得到获取实例的服务类型
            Type serviceType = Inject.Key.IsNullOrEmpty() ? Field.FieldType : Field.FieldType.Proxy(Inject.Key);
            // 获取服务
            if (Inject.Require) Field.SetValue(instance, GetRequiredService(serviceProvider, serviceType));
            else Field.SetValue(instance, GetService(serviceProvider, serviceType));
        }

        return instance;
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 替换默认服务提供商
    /// <br>因为默认 IServiceProvider 和 IServiceScopeFactory 直接加入缓存,所以直接修改缓存即可</br>
    /// </summary>
    /// <param name="holdDefaultServiceProvider">是否保留默认服务提供商</param>
    /// <param name="originalRoot">原Root服务提供商范围</param>
    private void ReplaceDefaultProvider(bool holdDefaultServiceProvider, IServiceProvider originalRoot)
    {
        Type serviceProviderType = typeof(ServiceProvider);
        Assembly dIAssembly = serviceProviderType.Assembly;

        // 搜索用的成员规则
        BindingFlags instanceNonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

        // 获取站点(服务)访问工厂
        object callSiteFactory = serviceProviderType.GetProperty("CallSiteFactory", instanceNonPublic)!.GetValue(_realServiceProvider)!;
        // 获取站点(服务)缓存字典
        object callSiteCashe = callSiteFactory.GetType().GetField("_callSiteCache", instanceNonPublic)!.GetValue(callSiteFactory)!;
        // 得到该字典的 Add 和 Remove 方法
        Type callSiteCacheType = callSiteCashe.GetType();
        MethodInfo add = callSiteCacheType.GetMethod("System.Collections.IDictionary.Add", instanceNonPublic)!;
        MethodInfo remove = callSiteCacheType.GetMethod("System.Collections.IDictionary.Remove", instanceNonPublic)!;

        // 得到缓存key类型
        Type serviceCacheKeyType = dIAssembly.GetType("Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceCacheKey")!;
        // 持续访问站点类型(有固定值)
        Type constantCallSiteType = dIAssembly.GetType("Microsoft.Extensions.DependencyInjection.ServiceLookup.ConstantCallSite")!;

        // 创建默认 Key
        object defaultIServiceProviderKey = Activator.CreateInstance(serviceCacheKeyType, typeof(IServiceProvider), 0)!;
        object defaultIServiceScopeFactoryKey = Activator.CreateInstance(serviceCacheKeyType, typeof(IServiceScopeFactory), 0)!;
        // 删除默认 IServiceProvider 和 IServiceScopeFactory 缓存
        remove.Invoke(callSiteCashe, new object[] { defaultIServiceProviderKey });
        remove.Invoke(callSiteCashe, new object[] { defaultIServiceScopeFactoryKey });

        // 创建 Fast Value
        object defaultIServiceScopeFactoryVal = Activator.CreateInstance(constantCallSiteType, typeof(IServiceScopeFactory), _rootServiceProvider)!;
        // 添加 Fast 容器（IServiceProvider 不用添加正常注册）
        add.Invoke(callSiteCashe, new object[] { defaultIServiceScopeFactoryKey, defaultIServiceScopeFactoryVal });

        // 添加默认服务提供商
        if (holdDefaultServiceProvider)
        {
            // 创建 microsoft Key
            object microsoftIServiceProviderKey = Activator.CreateInstance(serviceCacheKeyType, typeof(IServiceProvider).DefaultProxy(), 0)!;
            object microsoftIServiceScopeFactoryKey = Activator.CreateInstance(serviceCacheKeyType, typeof(IServiceScopeFactory).DefaultProxy(), 0)!;

            // 服务提供商访问站点类型(IServiceProvider 获取的默认类型)
            Type serviceCallSiteType = dIAssembly.GetType("Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderCallSite")!;
            // 创建 microsoft Value
            object microsoftIServiceProviderVal = Activator.CreateInstance(serviceCallSiteType)!;
            object microsoftIServiceScopeFactoryVal = Activator.CreateInstance(constantCallSiteType, typeof(IServiceScopeFactory), originalRoot)!;

            // 添加 microsoft 容器
            add.Invoke(callSiteCashe, new object[] { microsoftIServiceProviderKey, microsoftIServiceProviderVal });
            add.Invoke(callSiteCashe, new object[] { microsoftIServiceScopeFactoryKey, microsoftIServiceScopeFactoryVal });
        }
    }

    /// <summary>
    /// 获取或创建服务提供商范围容器
    /// </summary>
    /// <param name="realServiceProvider">真实服务提供商范围</param>
    /// <param name="isRoot">是否是root</param>
    /// <returns></returns>
    private EasyServiceProviderScope GetOrCreate(IServiceProvider realServiceProvider, bool isRoot) => _serviceProviders.GetOrAdd(realServiceProvider, rsp => new EasyServiceProviderScope(this, rsp, isRoot));
    #endregion
}
