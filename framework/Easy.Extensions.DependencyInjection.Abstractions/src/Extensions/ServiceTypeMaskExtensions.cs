namespace Easy.Extensions.DependencyInjection.Abstractions.Extensions;

/// <summary>
/// <see cref="ServiceTypeMask"/> 拓展
/// </summary>
public static class ServiceTypeMaskExtensions
{
    #region 默认代理
    /// <summary>
    /// 默认代理key
    /// </summary>
    private const string MicrosoftKey = nameof(Microsoft);

    /// <summary>
    /// 为类型佩戴默认微软key的面具
    /// </summary>
    /// <param name="type">要代理的类型</param>
    /// <returns></returns>
    public static Type WearMicrosoftMask(this Type type) => new ServiceTypeMask(type, MicrosoftKey);

    /// <summary>
    /// 为类型佩戴默认微软key的面具
    /// </summary>
    /// <typeparam name="T">要代理的类型</typeparam>
    /// <returns>代理后的类型</returns>
    public static Type WearMicrosoftMask<T>() => new ServiceTypeMask(typeof(T), MicrosoftKey);
    #endregion
    
    /// <summary>
    /// 为类型佩戴面具(代理类型)
    /// </summary>
    /// <param name="type">要代理的类型</param>
    /// <param name="key">
    ///     自定义key
    ///     <br>参与 <see cref="HashCode"/> 计算</br>
    /// </param>
    /// <returns>代理后的类型</returns>
    public static Type WearMask(this Type type, string? key = null) => new ServiceTypeMask(type, key);

    /// <summary>
    /// 为类型佩戴面具(代理类型)
    /// </summary>
    /// <typeparam name="T">要代理的类型</typeparam>
    /// <param name="key">
    ///     自定义key
    ///     <br>参与 <see cref="HashCode"/> 计算</br>
    /// </param>
    /// <returns>代理后的类型</returns>
    public static Type WearMask<T>(string? key = null) => new ServiceTypeMask(typeof(T), key);
}
