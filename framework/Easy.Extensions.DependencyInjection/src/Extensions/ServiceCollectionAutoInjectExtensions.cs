using Easy.Extensions.DependencyInjection;
using Easy.Extensions.DependencyInjection.Abstractions;
using Easy.Extensions.DependencyInjection.Abstractions.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="ServiceCollection"/> 自动注册拓展
/// </summary>
public static class ServiceCollectionAutoInjectExtensions
{
    /// <summary>
    /// 原始通过构造函数创建实例的服务面具key
    /// </summary>
    private const string _originalConstructorServiceKey = "OriginalConstructorService";

    public static IServiceCollection EnablePropertyInject(this IServiceCollection services)
    {
        // 替换原始的通过构造函数创建服务的服务描述器
        List<ServiceDescriptor> typeServices = services.Where(s => (s.ServiceType is not ServiceTypeMask maskType || maskType.Key != _originalConstructorServiceKey) && s.ImplementationType is not null).ToList();
        foreach (ServiceDescriptor typeService in typeServices)
        {
            services.Remove(typeService);
            ServiceDescriptor maskServiceDescriptor = new(typeService.ServiceType.WearMask(_originalConstructorServiceKey), typeService.ImplementationType!, typeService.Lifetime);
            ServiceDescriptor newServiceDescriptor = ServiceDescriptor.Describe(typeService.ServiceType, serviceProvider => serviceProvider.GetService(maskServiceDescriptor.ServiceType)!, typeService.Lifetime);
            services.Add(maskServiceDescriptor);
            services.Add(newServiceDescriptor);
        }

        // 实现属性和字段注入
        List<ServiceDescriptor> factoryServices = services.Where(s => s.ImplementationFactory is not null).ToList();
        foreach (ServiceDescriptor factoryService in factoryServices)
        {
            services.Remove(factoryService);
            ServiceDescriptor newServiceDescriptor = ServiceDescriptor.Describe(factoryService.ServiceType, serviceProvider => FieldInject(PropertyInject(factoryService.ImplementationFactory!(serviceProvider), serviceProvider), serviceProvider)!, factoryService.Lifetime);
            services.Add(newServiceDescriptor);
        }

        return services;
    }

    /// <summary>
    /// 属性注入
    /// </summary>
    /// <param name="instance">需要属性注入的实例</param>
    /// <param name="serviceProvider">属性注入的服务提供商</param>
    /// <returns>属性注入后的实例</returns>
    private static object? PropertyInject(object? instance, IServiceProvider serviceProvider)
    {
        if (instance is null) return instance;

        // 得到要注入的属性
        IEnumerable<(PropertyInfo Property, InjectAttribute Inject)> injectPropertyInfos = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => (p.PropertyType.IsClass || p.PropertyType.IsInterface) && p.IsDefined(typeof(InjectAttribute), false))
            .Select(p => (p, p.GetAttribute<InjectAttribute>(false)!));

        MemberInfo[]? a = instance.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        // 对属性赋值
        foreach ((PropertyInfo Property, InjectAttribute Inject) in injectPropertyInfos)
        {
            // 得到获取实例的服务类型
            Type serviceType = Inject.Key.IsNullOrEmpty() ? Property.PropertyType : Property.PropertyType.WearMask(Inject.Key);
            // 获取服务
            if (Inject.Require) Property.SetPropertyValue(instance, serviceProvider.GetRequiredService(serviceType));
            else Property.SetPropertyValue(instance, serviceProvider.GetService(serviceType));
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
            Type serviceType = Inject.Key.IsNullOrEmpty() ? Field.FieldType : Field.FieldType.WearMask(Inject.Key);
            // 获取服务
            if (Inject.Require) Field.SetValue(instance, serviceProvider.GetRequiredService(serviceType));
            else Field.SetValue(instance, serviceProvider.GetService(serviceType));
        }

        return instance;
    }
}
