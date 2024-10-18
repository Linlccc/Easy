namespace System.Reflection;

/// <summary>
/// <see cref="PropertyInfo"/> 扩展
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// 设置指定对象的属性值。
    /// </summary>
    /// <param name="propertyInfo">要设置值的属性信息。</param>
    /// <param name="obj">要设置属性值的对象。</param>
    /// <param name="value">要设置的值。</param>
    /// <exception cref="ArgumentNullException">如果 <paramref name="propertyInfo"/> 或 <paramref name="obj"/> 为 <c>null</c>。</exception>
    public static void SetPropertyValue(this PropertyInfo propertyInfo, object? obj, object? value)
    {
        _ = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        _ = obj ?? throw new ArgumentNullException(nameof(obj));

        // 如果有 Set 方法，直接使用 SetValue 方法；否则使用字段赋值
        if (propertyInfo.SetMethod is not null) propertyInfo.SetValue(obj, value);
        else obj.GetType().GetField($"<{propertyInfo.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(obj, value);
    }
}
