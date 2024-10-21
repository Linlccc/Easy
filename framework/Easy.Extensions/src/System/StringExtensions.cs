using System.Diagnostics.CodeAnalysis;

namespace System;

/// <summary>
/// <see cref="string"/> 扩展
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 检查指定的字符串是否为 <c>null</c> 或空字符串.
    /// </summary>
    /// <param name="value">要检查的字符串，可以为 <c>null</c>。</param>
    /// <returns>如果字符串为 <c>null</c> 或空字符串，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
#if NET462 || NETSTANDARD2_0
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);
#else
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrEmpty(value);
#endif
}
