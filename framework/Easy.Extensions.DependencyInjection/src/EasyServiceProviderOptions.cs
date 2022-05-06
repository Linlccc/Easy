using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 用于配置服务提供商的行为选项
/// </summary>
public class EasyServiceProviderOptions
{
    public static readonly EasyServiceProviderOptions Default = new() { RegisterScanAssemblys = AppDomain.CurrentDomain.GetAssemblies().ToList() };

    /// <summary>
    /// 是否保留默认的服务提供商
    /// </summary>
    public bool HoldDefaultServiceProvider { get; set; } = true;

    /// <summary>
    /// 注册扫描程序集
    /// <br>如果为空，自动加载当前程序域的程序集</br>
    /// </summary>
    public IEnumerable<Assembly> RegisterScanAssemblys { get; set; } = new List<Assembly>();

    /// <summary>
    /// 服务提供商的行为配置
    /// </summary>
    public ServiceProviderOptions ServiceProviderOptions { get; set; } = new ServiceProviderOptions();
}
