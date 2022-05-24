namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// Easy 服务提供商的事件配置
/// </summary>
public class EasyServiceProviderEvents
{
    /// <summary>
    /// 获取服务前
    /// </summary>
    public Func<IServiceProvider, Type, Task> OnBeforeGetService { get; set; } = (IServiceProvider serviceProvider, Type serviceType) => Task.CompletedTask;
    /// <summary>
    /// 获取服务后,成员(属性/字段)注入之前
    /// </summary>
    public Func<IServiceProvider, Type, object?, Task> OnAfterGetService { get; set; } = (IServiceProvider serviceProvider, Type serviceType, object? instance) => Task.CompletedTask;

    /// <summary>
    /// 获取服务完成
    /// </summary>
    public Func<IServiceProvider, Type, object?, Task> OnGetServiceCompleted { get; set; } = (IServiceProvider serviceProvider, Type serviceType, object? instance) => Task.CompletedTask;



    /// <summary>
    /// 获取服务前
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <returns></returns>
    public virtual Task BeforeGetService(IServiceProvider serviceProvider, ref Type serviceType) => OnBeforeGetService(serviceProvider, serviceType);

    /// <summary>
    /// 获取服务后,成员(属性/字段)注入之前
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="instance">获取到的实例</param>
    /// <returns></returns>
    public virtual Task AfterGetService(IServiceProvider serviceProvider, Type serviceType, ref object? instance) => OnAfterGetService(serviceProvider, serviceType, instance);

    /// <summary>
    /// 获取服务完成
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <param name="serviceType">服务类型</param>
    /// <param name="instance">获取到的实例</param>
    /// <returns></returns>
    public virtual Task GetServiceCompleted(IServiceProvider serviceProvider, Type serviceType, ref object? instance) => OnGetServiceCompleted(serviceProvider, serviceType, instance);
}
