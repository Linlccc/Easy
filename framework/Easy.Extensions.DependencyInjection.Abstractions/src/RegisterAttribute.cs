using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 注册类型（不可继承）
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class RegisterAttribute : Attribute
{
    /// <summary>
    /// 注册的生命类型
    /// </summary>
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Scoped;

    /// <summary>
    /// 注册成为的服务类型
    /// <list type="bullet">
    ///     <item>
    ///         <term>优先级</term>
    ///         <description>
    ///             优先使用该参数中的类型
    ///             <br>为空时，自动注册除 <see cref="IDisposable"/>/<see cref="IAsyncDisposable"/> 外以实现的接口</br>
    ///             <br>没有实现任何接口时，将自己作为服务类型注册</br>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>注</term>
    ///         <description>没有实现的服务类型将会被排除，类型可是实现的接口/类型本身/基类</description>
    ///     </item>
    /// </list>
    /// </summary>
    public IEnumerable<Type> ServiceTypes { get; }

    /// <summary>
    /// 同时使用Key注册服务
    /// </summary>
    public string? ServiceKey { get; set; }

    /// <summary>
    /// 注册类型
    /// </summary>
    /// <param name="types">注册的服务类型</param>
    public RegisterAttribute(params Type[] types) => ServiceTypes = types.Distinct();
}
