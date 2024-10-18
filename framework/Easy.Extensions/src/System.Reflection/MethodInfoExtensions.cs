namespace System.Reflection;

/// <summary>
/// <see cref="MethodInfo"/> 扩展
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    /// 获取与指定方法相关联的属性信息（PropertyInfo）。
    /// </summary>
    /// <param name="method">要获取属性的方法信息。</param>
    /// <returns>与指定方法关联的属性信息；如果不是属性访问器，则返回 <c>null</c>。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="method"/> 为 <c>null</c>。</exception>
    public static PropertyInfo? GetBindProperty(this MethodInfo method)
    {
        _ = method ?? throw new ArgumentNullException(nameof(method));

        // 不是特殊名称 || 不是以get_/set_开头
        if (!method.IsSpecialName || !(method.Name.StartsWith("get_") || method.Name.StartsWith("set_"))) return null;

#if NETSTANDARD2_0 || NET462
        return method.DeclaringType?.GetProperty(method.Name.Substring(4));
#else
        return method.DeclaringType?.GetProperty(method.Name[4..]);
#endif
    }

    /// <summary>
    /// 检查指定的方法是否与一个属性相关联。
    /// </summary>
    /// <param name="method">要检查的方法信息。</param>
    /// <returns>如果方法是属性访问器，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsBindProperty(this MethodInfo method) => method.GetBindProperty() is not null;
}
