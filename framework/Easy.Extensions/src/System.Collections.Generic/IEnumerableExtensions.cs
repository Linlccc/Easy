namespace System.Collections.Generic;

/// <summary>
/// <see cref="IEnumerable{T}"/> 拓展
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// 是空
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">集合</param>
    /// <returns>null or 空集合 返回true,否者false</returns>
#if NET462 || NETSTANDARD2_0
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection) => collection is null || !collection.Any();
#else
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? collection) => collection is null || !collection.Any();
#endif
}
