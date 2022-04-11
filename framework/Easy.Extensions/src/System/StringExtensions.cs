namespace System;

/// <summary>
/// <see cref="string"/> 拓展
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 判断字符串是否为空
    /// </summary>
    /// <param name="value">要判断的字符串</param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);
}
