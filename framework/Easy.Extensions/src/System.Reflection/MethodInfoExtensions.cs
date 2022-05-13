using System.Runtime.CompilerServices;

namespace System.Reflection;

/// <summary>
/// <see cref="MethodInfo"/> 拓展
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    /// 判断 <paramref name="method"/> 是否与某个属性绑定
    /// </summary>
    /// <param name="method">方法</param>
    /// <returns>如果 <paramref name="method"/> 是某个属性的get/set方法返回true,否者false</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsBindProperty(this MethodInfo method)
    {
        _ = method ?? throw new ArgumentNullException(nameof(method));

        return method.GetBindProperty() != null;
    }

    /// <summary>
    /// 获取 <paramref name="method"/> 绑定的属性
    /// </summary>
    /// <param name="method">方法</param>
    /// <returns>如果是某个属性的get/set方法返回属性，如果不是返回null</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static PropertyInfo? GetBindProperty(this MethodInfo method)
    {
        _ = method ?? throw new ArgumentNullException(nameof(method));

        // 不是特殊名称 || 不是以get_/set_开头
        if (!method.IsSpecialName || !(method.Name.StartsWith("get_") || method.Name.StartsWith("set_"))) return null;

        return method.DeclaringType?.GetProperty(method.Name.Substring(4));
    }
}
