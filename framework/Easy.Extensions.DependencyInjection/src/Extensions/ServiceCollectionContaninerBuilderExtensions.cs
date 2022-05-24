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
    /// <param name="easyServiceProviderOptions">Easy 服务提供商配置</param>
    /// <returns></returns>
    public static EasyServiceProvider BuildEasyServiceProvider(this IServiceCollection services, EasyServiceProviderOptions easyServiceProviderOptions) => new(services, easyServiceProviderOptions);

    public static EasyServiceProvider BuildEasyServiceProvider(this IServiceCollection services) => new(services, new EasyServiceProviderOptions());
}
