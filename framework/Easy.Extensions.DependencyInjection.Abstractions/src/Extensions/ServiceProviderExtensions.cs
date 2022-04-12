using Easy.Extensions.DependencyInjection.Abstractions;
using Easy.Extensions.DependencyInjection.Abstractions.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="IServiceProvider"/> 拓展
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// 根据服务key和服务类型获取服务实例
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例</returns>
    public static object? GetService(this IServiceProvider serviceProvider, Type serviceType, string key) => serviceProvider.GetService(serviceType.Proxy(key));

    /// <summary>
    /// 根据服务key和服务类型获取服务实例
    /// </summary>
    /// <typeparam name="T">服务类型</typeparam>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例</returns>
    public static T? GetService<T>(this IServiceProvider serviceProvider, string key) => (T?)serviceProvider.GetService(typeof(T), key);


    /// <summary>
    /// 根据服务key和服务类型获取服务实例
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例</returns>
    /// <exception cref="InvalidOperationException">服务实例,服务实例不存在抛出<see cref="InvalidOperationException"/>异常</exception>
    public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType, string key)
    {
        if (serviceProvider is ISupportRequiredService supportRequiredService) return supportRequiredService.GetRequiredService(serviceType.Proxy(key));

        object? result = serviceProvider.GetService(serviceType, key);
        if (result is null) throw new InvalidOperationException(string.Format(Strings.NoServiceRegistered, nameof(serviceType)));
        return result;
    }

    /// <summary>
    /// 根据服务key和服务类型获取服务实例
    /// </summary>
    /// <typeparam name="T">服务类型</typeparam>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例,服务实例不存在抛出<see cref="InvalidOperationException"/>异常</returns>
    public static T GetRequiredService<T>(this IServiceProvider serviceProvider, string key) => (T)serviceProvider.GetRequiredService(typeof(T), key);


    /// <summary>
    /// 根据服务key和服务类型获取服务实例集合
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例集合</returns>
    public static IEnumerable<object?> GetServices(this IServiceProvider serviceProvider, Type serviceType, string key) => serviceProvider.GetServices(serviceType.Proxy(key));

    /// <summary>
    /// 根据服务key和服务类型获取服务实例集合
    /// </summary>
    /// <typeparam name="T">服务类型</typeparam>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="key">服务key</param>
    /// <returns>服务实例集合</returns>
    public static IEnumerable<T> GetServices<T>(this IServiceProvider serviceProvider, string key) => (IEnumerable<T>)serviceProvider.GetServices(typeof(T), key);
}
