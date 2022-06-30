namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 服务提供商工厂
/// </summary>
public class EasyServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
    private readonly EasyServiceProviderOptions _easyServiceProviderOptions;
    private readonly IEnumerable<Assembly> _registerScanAssemblies;
    public EasyServiceProviderFactory() : this(new EasyServiceProviderOptions()) { }
    public EasyServiceProviderFactory(EasyServiceProviderOptions easyServiceProviderOptions) : this(easyServiceProviderOptions, AppDomain.CurrentDomain.GetAssemblies()) { }
    /// <summary>
    /// Easy 服务提供商工厂
    /// </summary>
    /// <param name="easyServiceProviderOptions">easy 服务提供商配置</param>
    /// <param name="assemblies">自动注册要扫描的程序集(如果不提供会去获取当前程序域中所有程序集)</param>
    public EasyServiceProviderFactory(EasyServiceProviderOptions easyServiceProviderOptions, params Assembly[] assemblies)
    {
        _easyServiceProviderOptions = easyServiceProviderOptions;
        _registerScanAssemblies = assemblies;
    }

    public IServiceCollection CreateBuilder(IServiceCollection services) => services;

    public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder) =>
        // 自动注册服务
        containerBuilder.AutoRegister(_registerScanAssemblies)
        // 构建 Easy 服务提供商
        .BuildEasyServiceProvider(_easyServiceProviderOptions);
}
