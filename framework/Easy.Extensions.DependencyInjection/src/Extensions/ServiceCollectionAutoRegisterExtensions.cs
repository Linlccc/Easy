using System.Reflection;
using Easy.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="ServiceCollection"/> 自动注册拓展
/// </summary>
public static class ServiceCollectionAutoRegisterExtensions
{
    /// <summary>
    /// 自动注册
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblies">要注册的程序集</param>
    /// <returns>注册后的服务集合</returns>
    public static IServiceCollection AutoRegister(this IServiceCollection services, IEnumerable<Assembly> assemblies) => new EasyServicesRegistrar(assemblies).Registrar(services);
    public static IServiceCollection AutoRegister(this IServiceCollection services, params Assembly[] assemblies) => new EasyServicesRegistrar(assemblies).Registrar(services);
}
