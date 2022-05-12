namespace Easy.Extensions.DynamicProxy;

/// <summary>
/// 片面激活器
/// </summary>
public interface IAspectActivator
{
    TResult Invoke<TResult>(AspectActivatorContext aspectActivatorContext);
    Task<TResult> InvokeTask<TResult>(AspectActivatorContext aspectActivatorContext);
    ValueTask<TResult> InvokeValueTask<TResult>(AspectActivatorContext aspectActivatorContext);
}
