using System.Diagnostics.CodeAnalysis;

namespace System.Reflection;

/// <summary>
/// <see cref="ICustomAttributeProvider"/>(为支持反射对象的对象提供自定义属性接口) 拓展
/// <list type="bullet">人话：该拓展具备提供从对象中获取<see cref="Attribute"/>(特性)的服务</list>
/// </summary>
public static class ICustomAttributeProviderExtensions
{
    #region Attribute
    /// <summary>
    /// 获取 <typeparamref name="TAttribute"/> 类型特性
    /// </summary>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="customAttributeProvider">特性提供者</param>
    /// <param name="inherit">是否从继承链上获取</param>
    /// <returns><typeparamref name="TAttribute"/> 类型的第一个特性,不存在则为 null</returns>
    public static TAttribute? GetAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit)
    {
        _ = customAttributeProvider ?? throw new ArgumentNullException(nameof(customAttributeProvider));

        return (TAttribute?)customAttributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault();
    }

    /// <summary>
    /// 获取 <typeparamref name="TAttribute"/> 类型特性集合
    /// </summary>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="customAttributeProvider">特性提供者</param>
    /// <param name="inherit">是否从继承链上获取</param>
    /// <returns><typeparamref name="TAttribute"/> 类型的特性集合</returns>
    public static IEnumerable<TAttribute?> GetAttributes<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit) where TAttribute : class
    {
        _ = customAttributeProvider ?? throw new ArgumentNullException(nameof(customAttributeProvider));

        return customAttributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
    }

    /// <summary>
    /// 检查是否存在 <typeparamref name="TAttribute"/> 类型特性，如果存在过获取该特性
    /// <list type="bullet">如果只检查是否存在建议使用 <see cref="ICustomAttributeProvider.IsDefined"/> 方法</list>
    /// </summary>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="customAttributeProvider">特性提供者</param>
    /// <param name="inherit">是否从继承链上获取</param>
    /// <param name="attribute">如果特性存在，获取一个该特性对象</param>
    /// <returns>如果存在 <typeparamref name="TAttribute"/> 类型特性返回 true，否者 false</returns>
#if NET462 || NETSTANDARD2_0
    public static bool IsExistAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit, out TAttribute? attribute)
#else
    public static bool IsExistAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit, [NotNullWhen(true)] out TAttribute? attribute)
#endif
    {
        _ = customAttributeProvider ?? throw new ArgumentNullException(nameof(customAttributeProvider));
        
        attribute = (TAttribute?)customAttributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault();
        return attribute is not null;
    }
    #endregion


    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="propertyInfo">属性信息</param>
    /// <param name="obj">设置值得实例</param>
    /// <param name="value">要设置的值</param>
    public static void SetPropertyValue(this PropertyInfo propertyInfo, object? obj, object? value)
    {
        _ = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        _ = obj ?? throw new ArgumentNullException(nameof(obj));

        // 如果没有 Set 方法，获取到该属性的字段赋值
        if (propertyInfo.SetMethod is null) obj.GetType().GetField($"<{propertyInfo.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(obj, value);
        else propertyInfo.SetValue(obj, value);
    }
}
