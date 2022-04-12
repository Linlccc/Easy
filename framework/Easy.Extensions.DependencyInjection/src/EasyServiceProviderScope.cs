using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection;

/// <summary>
/// 服务提供商范围
/// <br><c>ServiceProviderEngineScope</c> 的容器</br>
/// </summary>
internal class EasyServiceProviderScope : IServiceScopeFactory, IServiceScope, IServiceProvider, ISupportRequiredService, IAsyncDisposable
{
    /// <summary>
    /// 根服务提供商
    /// </summary>
    readonly EasyServiceProvider _rootServiceProvider;

    /// <summary>
    /// 真实服务提供商
    /// 存放 <c>ServiceProviderEngineScope</c> 类型对象
    /// </summary>
    internal readonly IServiceProvider _realServiceProvider;

    /// <summary>
    /// 是否是根服务提供商
    /// </summary>
    readonly bool _isRoot;

    /// <summary>
    /// 服务提供商字典
    /// </summary>
    static readonly ConcurrentDictionary<IServiceProvider, EasyServiceProviderScope> _serviceProviders = new();

    bool _disposed;

    /// <summary>
    /// 创建服务提供商范围容器
    /// </summary>
    /// <param name="fastServiceProvider">原始服务提供商容器</param>
    /// <param name="realServiceProvider">真实服务提供商范围</param>
    /// <param name="isRoot">是否是root</param>
    EasyServiceProviderScope(EasyServiceProvider fastServiceProvider, IServiceProvider realServiceProvider, bool isRoot)
    {
        _rootServiceProvider = fastServiceProvider;
        _realServiceProvider = realServiceProvider;
        _isRoot = isRoot;
    }

    /// <summary>
    /// 获取或创建服务提供商范围容器
    /// </summary>
    /// <param name="fastServiceProvider">原始服务提供商容器</param>
    /// <param name="realServiceProvider">真实服务提供商范围</param>
    /// <param name="isRoot">是否是root</param>
    /// <returns></returns>
    public static EasyServiceProviderScope GetOrCreate(EasyServiceProvider fastServiceProvider, IServiceProvider realServiceProvider, bool isRoot) => _serviceProviders.GetOrAdd(realServiceProvider, rsp => new EasyServiceProviderScope(fastServiceProvider, rsp, isRoot));

    public IServiceProvider ServiceProvider => this;

    public object? GetService(Type serviceType) => EasyServiceProvider.GetService(_realServiceProvider, serviceType);

    public object GetRequiredService(Type serviceType) => EasyServiceProvider.GetRequiredService(_realServiceProvider, serviceType);

    public IServiceScope CreateScope() => _rootServiceProvider.CreateScope();


    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _serviceProviders.TryRemove(_realServiceProvider, out _);
        (_realServiceProvider as IDisposable)!.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        if (_disposed) return default;
        _disposed = true;
        _serviceProviders.TryRemove(_realServiceProvider, out _);
        return (_realServiceProvider as IAsyncDisposable)!.DisposeAsync();
    }
}
