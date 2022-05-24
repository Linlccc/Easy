using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 用于配置服务提供商的行为选项
/// </summary>
public class EasyServiceProviderOptions
{
    public EasyServiceProviderOptions() { }
    /// <summary>
    /// Easy 服务提供商配置选项
    /// </summary>
    /// <param name="serviceProviderEventsType">Easy 服务提供商的事件类型,需继承自 <see cref="EasyServiceProviderEvents"/></param>
    public EasyServiceProviderOptions(Type serviceProviderEventsType) => ServiceProviderEventsType = serviceProviderEventsType;

    /// <summary>
    /// 是否保留默认的服务提供商
    /// </summary>
    public bool HoldDefaultServiceProvider { get; set; } = true;

    /// <summary>
    /// 服务提供商的行为配置
    /// </summary>
    public ServiceProviderOptions ServiceProviderOptions { get; set; } = new ServiceProviderOptions();

    /// <summary>
    /// Easy 服务提供商的事件类型,需继承自 <see cref="EasyServiceProviderEvents"/>
    /// </summary>
    public Type ServiceProviderEventsType { get; set; } = typeof(EasyServiceProviderEvents);
}
