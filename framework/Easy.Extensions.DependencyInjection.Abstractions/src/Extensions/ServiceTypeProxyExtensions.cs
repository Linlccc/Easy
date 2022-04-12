namespace Easy.Extensions.DependencyInjection.Abstractions.Extensions;

/// <summary>
/// <see cref="ServiceTypeProxy"/> 拓展
/// </summary>
public static class ServiceTypeProxyExtensions
{
    #region 默认代理
    /// <summary>
    /// 默认代理key
    /// </summary>
    private const string DafeultProxyKey = "microsoft_default";

    /// <summary>
    /// 默认微软代理类型
    /// </summary>
    /// <param name="type">要代理的类型</param>
    /// <returns></returns>
    public static Type DefaultProxy(this Type type) => new ServiceTypeProxy(type, DafeultProxyKey);

    /// <summary>
    /// 创建默认代理类型
    /// </summary>
    /// <typeparam name="T">要代理的类型</typeparam>
    /// <param name="key">
    ///     自定义key
    ///     <br>参与 <see cref="HashCode"/> 计算</br>
    /// </param>
    /// <returns>代理后的类型</returns>
    public static Type CreateDefaultProxyType<T>() => new ServiceTypeProxy(typeof(T), DafeultProxyKey);
    #endregion
    
    /// <summary>
    /// 代理类型
    /// </summary>
    /// <param name="type">要代理的类型</param>
    /// <param name="key">
    ///     自定义key
    ///     <br>参与 <see cref="HashCode"/> 计算</br>
    /// </param>
    /// <returns>代理后的类型</returns>
    public static Type Proxy(this Type type, string? key = null) => new ServiceTypeProxy(type, key);

    /// <summary>
    /// 代理类型
    /// </summary>
    /// <typeparam name="T">要代理的类型</typeparam>
    /// <param name="key">
    ///     自定义key
    ///     <br>参与 <see cref="HashCode"/> 计算</br>
    /// </param>
    /// <returns>代理后的类型</returns>
    public static Type CreateProxyType<T>(string? key = null) => new ServiceTypeProxy(typeof(T), key);
}
