using Easy.Extensions.DependencyInjection.Abstractions.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="ServiceCollection"/> 服务拓展
/// </summary>
public static class ServiceCollectionServiceExtensions
{
    #region Transient
    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationType">实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Type implementationType, string? key) => services.AddTransient(serviceType.Proxy(key), implementationType);
    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <typeparam name="TImplementation">实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient<TIService, TImplementation>(this IServiceCollection services, string? key) => services.AddTransient(typeof(TIService).Proxy(key), typeof(TImplementation));
    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型/实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, string? key) => services.AddTransient(serviceType.Proxy(key), serviceType);
    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <typeparam name="TIService">服务类型/实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient<TIService>(this IServiceCollection services, string? key) => services.AddTransient(typeof(TIService).Proxy(key), typeof(TIService));

    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddTransient(serviceType.Proxy(key), implementationFactory);
    /// <summary>
    /// 瞬时注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTransient<TIService>(this IServiceCollection services, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddTransient(typeof(TIService).Proxy(key), implementationFactory);
    #endregion

    #region Scoped
    /// <summary>
    /// 范围注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationType">实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Type implementationType, string? key) => services.AddScoped(serviceType.Proxy(key), implementationType);
    /// <summary>
    /// 范围注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <typeparam name="TImplementation">实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped<TIService, TImplementation>(this IServiceCollection services, string? key) => services.AddScoped(typeof(TIService).Proxy(key), typeof(TImplementation));
    /// <summary>
    /// 范围注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型/实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, string? key) => services.AddScoped(serviceType.Proxy(key), serviceType);
    /// <summary>
    /// 范围注册
    /// </summary>
    /// <typeparam name="TIService">服务类型/实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped<TIService>(this IServiceCollection services, string? key) => services.AddScoped(typeof(TIService).Proxy(key), typeof(TIService));

    /// <summary>
    /// 范围注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddScoped(serviceType.Proxy(key), implementationFactory);
    /// <summary>
    /// 范围注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScoped<TIService>(this IServiceCollection services, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddScoped(typeof(TIService).Proxy(key), implementationFactory);
    #endregion

    #region Singleton
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationType">实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Type implementationType, string? key) => services.AddSingleton(serviceType.Proxy(key), implementationType);
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <typeparam name="TImplementation">实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton<TIService, TImplementation>(this IServiceCollection services, string? key) => services.AddSingleton(typeof(TIService).Proxy(key), typeof(TImplementation));
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型/实例类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, string? key) => services.AddSingleton(serviceType.Proxy(key), serviceType);
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <typeparam name="TIService">服务类型/实例类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton<TIService>(this IServiceCollection services, string? key) => services.AddSingleton(typeof(TIService).Proxy(key), typeof(TIService));

    /// <summary>
    /// 单例注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddSingleton(serviceType.Proxy(key), implementationFactory);
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="implementationFactory">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton<TIService>(this IServiceCollection services, Func<IServiceProvider, object> implementationFactory, string? key) => services.AddSingleton(typeof(TIService).Proxy(key), implementationFactory);

    /// <summary>
    /// 单例注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationInstance">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, object implementationInstance, string? key) => services.AddSingleton(serviceType.Proxy(key), implementationInstance);
    /// <summary>
    /// 单例注册
    /// </summary>
    /// <typeparam name="TIService">服务类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="implementationInstance">实例工厂</param>
    /// <param name="key">服务key</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSingleton<TIService>(this IServiceCollection services, object implementationInstance, string? key) => services.AddSingleton(typeof(TIService).Proxy(key), implementationInstance);
    #endregion
}
