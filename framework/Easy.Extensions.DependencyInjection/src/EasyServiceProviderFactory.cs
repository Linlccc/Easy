using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 服务提供商工厂
/// </summary>
public class EasyServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
    private readonly EasyServiceProviderOptions _easyServiceProviderOptions;
    public EasyServiceProviderFactory() => _easyServiceProviderOptions = EasyServiceProviderOptions.Default;
    public EasyServiceProviderFactory(EasyServiceProviderOptions easyServiceProviderOptions) => _easyServiceProviderOptions = easyServiceProviderOptions;

    public IServiceCollection CreateBuilder(IServiceCollection services) => services;

    public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder) =>
        // 自动注册服务
        containerBuilder.AutoRegister(_easyServiceProviderOptions.RegisterScanAssemblys)
        // 构建 Easy 服务提供商
        .BuildEasyServiceProvider(_easyServiceProviderOptions.ServiceProviderOptions, _easyServiceProviderOptions.HoldDefaultServiceProvider);
}
