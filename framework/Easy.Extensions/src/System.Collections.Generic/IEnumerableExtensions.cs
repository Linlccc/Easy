// ReSharper disable InconsistentNaming

#if !(NET462 || NETSTANDARD2_0)
using System.Diagnostics.CodeAnalysis;
#endif

namespace System.Collections.Generic;

/// <summary>
/// <see cref="IEnumerable{T}"/> 扩展
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// 检查集合是否为 <c>null</c> 或为空。
    /// </summary>
    /// <typeparam name="T">集合中元素的类型。</typeparam>
    /// <param name="collection">要检查的集合。</param>
    /// <returns>如果集合为 <c>null</c> 或没有元素，则返回 <c>true</c>； 否则返回 <c>false</c>。 </returns>
#if NET462 || NETSTANDARD2_0
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection) => collection is null || !collection.Any();
#else
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? collection) => collection is null || !collection.Any();
#endif
}
