using Easy.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="ServiceCollection"/> 容器构建拓展
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// 构建服务提供商
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="serviceProviderOptions">服务提供商配置</param>
    /// <param name="holdDefaultServiceProvider">是否保留默认服务提供商</param>
    /// <returns></returns>
    public static EasyServiceProvider BuildEasyServiceProvider(this IServiceCollection services, ServiceProviderOptions? serviceProviderOptions, bool holdDefaultServiceProvider)
        => new(services, serviceProviderOptions, holdDefaultServiceProvider);

    public static EasyServiceProvider BuildEasyServiceProvider(this IServiceCollection services) => new(services, null, true);
}
