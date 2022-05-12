using System.Reflection;

namespace Easy.Extensions.DynamicProxy;

/// <summary>
/// 片面激活器上下文
/// </summary>
public struct AspectActivatorContext
{
    /// <summary>
    /// 代理实例
    /// </summary>
    public object ProxyInstance { get; set; }

    /// <summary>
    /// 实现实例
    /// </summary>
    public object ImplementationInstance { get; set; }

    /// <summary>
    /// 代理方法
    /// </summary>
    public MethodInfo ProxyMethod { get; }

    /// <summary>
    /// 服务方法
    /// </summary>
    public MethodInfo ServiceMethod { get; }

    /// <summary>
    /// 实现方法
    /// </summary>
    public MethodInfo ImplementationMethod { get; set; }

    /// <summary>
    /// 参数集合
    /// </summary>
    public object[] Parameters { get; }


    public AspectActivatorContext(object proxyInstance, object implementationInstance, MethodInfo proxyMethod, MethodInfo serviceMethod, MethodInfo implementationMethod, object[] parameters)
    {
        ProxyInstance = proxyInstance;
        ImplementationInstance = implementationInstance;
        ProxyMethod = proxyMethod;
        ServiceMethod = serviceMethod;
        ImplementationMethod = implementationMethod;
        Parameters = parameters;
    }
}
