// ReSharper disable InconsistentNaming

#if !(NET462 || NETSTANDARD2_0)
using System.Diagnostics.CodeAnalysis;
#endif

namespace System.Reflection;

/// <summary>
/// <see cref="ICustomAttributeProvider"/>(为支持反射对象的对象提供自定义属性接口) 扩展
/// <list type="bullet">人话：该扩展具备提供从对象中获取<see cref="Attribute"/>(特性)的服务</list>
/// </summary>
public static class ICustomAttributeProviderExtensions
{
    /// <summary>
    /// 获取指定类型的自定义特性（Attributes）。
    /// </summary>
    /// <typeparam name="TAttribute">要获取的特性类型。</typeparam>
    /// <param name="customAttributeProvider">提供自定义特性的对象。</param>
    /// <param name="inherit">指示是否在基类型中查找特性。</param>
    /// <returns>包含指定类型的自定义特性的可枚举集合。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="customAttributeProvider"/> 为 <c>null</c>。</exception>
    public static IEnumerable<TAttribute?> GetAttributes<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit)
    {
        _ = customAttributeProvider ?? throw new ArgumentNullException(nameof(customAttributeProvider));

        return customAttributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
    }

    /// <summary>
    /// 获取指定类型的自定义特性（Attribute）。
    /// </summary>
    /// <typeparam name="TAttribute">要获取的特性类型。</typeparam>
    /// <param name="customAttributeProvider">提供自定义特性的对象。</param>
    /// <param name="inherit">指示是否在基类型中查找特性。</param>
    /// <returns>如果存在指定类型的特性，则返回该特性；否则返回 <c>null</c>。</returns>
    public static TAttribute? GetAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit) => customAttributeProvider.GetAttributes<TAttribute>(inherit).FirstOrDefault();

    /// <summary>
    /// 检查指定类型的自定义特性是否存在。
    /// <list type="bullet">如果只检查是否存在建议使用 <see cref="ICustomAttributeProvider.IsDefined"/> 方法</list>
    /// </summary>
    /// <typeparam name="TAttribute">要检查的特性类型。</typeparam>
    /// <param name="customAttributeProvider">提供自定义特性的对象。</param>
    /// <param name="inherit">指示是否在基类型中查找特性。</param>
    /// <param name="attribute">输出参数，返回找到的特性，如果不存在则为 <c>null</c>。</param>
    /// <returns>如果存在指定类型的特性，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
#if NET462 || NETSTANDARD2_0
    public static bool IsExistAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit, out TAttribute? attribute)
#else
    public static bool IsExistAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit, [NotNullWhen(true)] out TAttribute? attribute)
#endif
    {
        attribute = customAttributeProvider.GetAttribute<TAttribute>(inherit);
        return attribute is not null;
    }
}
