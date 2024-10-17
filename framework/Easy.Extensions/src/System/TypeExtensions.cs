namespace System;

/// <summary>
/// <see cref="Type"/> 扩展
/// </summary>
public static class TypeExtensions
{
    #region 检查扩展
    /// <summary>
    /// 检查指定类型是否实现了指定的接口类型定义。
    /// </summary>
    /// <param name="type">要检查的类型。不能为 <c>null</c>。</param>
    /// <param name="interfaceType">要匹配的接口类型定义。不能为 <c>null</c>。</param>
    /// <returns>如果指定类型实现了接口类型定义，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 或 <paramref name="interfaceType"/> 为 <c>null</c>。</exception>
    public static bool IsImplementsInterfaceDefinition(this Type type, Type interfaceType)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));
        _ = interfaceType ?? throw new ArgumentNullException(nameof(interfaceType));

        return interfaceType.IsInterface && type.GetInterfaces().Any(t => t.GetTypeDefinition() == interfaceType.GetTypeDefinition());
    }

    /// <summary>
    /// 检查指定类型是否实现了指定的接口。
    /// </summary>
    /// <param name="type">要检查的类型。不能为 <c>null</c>。</param>
    /// <param name="interfaceType">要匹配的接口类型。必须是接口类型且不能为 <c>null</c>。</param>
    /// <returns>如果指定类型实现了接口，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 或 <paramref name="interfaceType"/> 为 <c>null</c>。</exception>
    public static bool IsImplementsInterface(this Type type, Type interfaceType)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));
        _ = interfaceType ?? throw new ArgumentNullException(nameof(interfaceType));

        return interfaceType.IsInterface && type.GetInterfaces().Any(t => t == interfaceType);
    }

    /// <summary>
    /// 检查指定类型是否是完全开放的泛型类型（其所有的泛型参数都未被具体类型替换）。
    /// </summary>
    /// <param name="type">要检查的类型。不能为 <c>null</c>。</param>
    /// <returns>如果类型是是完全开放的泛型类型，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static bool IsAllOpenGeneric(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsGenericType && !type.GenericTypeArguments.Any(a => a.GUID != Guid.Empty);
    }

    /// <summary>
    /// 检查指定类型是否为可空类型（Nullable[T]）。
    /// </summary>
    /// <param name="type">要检查的类型。不能为 <c>null</c>。</param>
    /// <returns>如果类型是 <c>Nullable[T]</c> 的可空类型，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static bool IsNullableType(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.GetTypeDefinition() == typeof(Nullable<>);
    }
    #endregion

    #region 获取扩展
    /// <summary>
    /// 获取类型的名称。
    /// </summary>
    /// <param name="type">要获取名称的类型。不能为 <c>null</c>。</param>
    /// <returns>
    /// 如果类型是泛型类型，则返回包含泛型参数的名称（如 <c>List&lt;T&gt;</c>）；否则返回类型的名称。
    /// </returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static string Name(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        if (!type.IsGenericType) return type.Name;

        string argumentNames = string.Join(",", type.GetGenericArguments().Select(t => t.Name()));
        string typeName = type.Name.Remove(type.Name.IndexOf('`'));
        string result = $"{typeName}<{argumentNames}>";
        return result;
    }

    /// <summary>
    /// 获取类型的完整名称。
    /// </summary>
    /// <param name="type">要获取完整名称的类型。不能为 <c>null</c>。</param>
    /// <returns>如果类型是泛型类型，则返回包含泛型参数的完整名称（如 <c>System.Collections.Generic.List&lt;System.Int32&gt;</c>）；否则返回类型的完整名称。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static string? FullName(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        if (!type.IsGenericType) return type.FullName;

        if (type.FullName is not string fullName) return null;
        string typeName = fullName.Remove(fullName.IndexOf('`'));
        string argumentNames = string.Join(",", type.GetGenericArguments().Select(t => t.FullName()));
        string result = $"{typeName}<{argumentNames}>";
        return result;
    }

    /// <summary>
    /// 获取指定类型的类型定义。
    /// </summary>
    /// <param name="type">要获取泛型类型定义的类型。不能为 <c>null</c>。</param>
    /// <returns>如果 <paramref name="type"/> 是泛型类型，则返回其泛型类型定义；否则返回类型本身。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static Type GetTypeDefinition(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
    }

    /// <summary>
    /// 获取 <c>Nullable&lt;&gt;</c> 类型中的基础类型
    /// </summary>
    /// <param name="type">要检查的类型。不能为 <c>null</c>。</param>
    /// <returns>如果 <paramref name="type"/> 是 <c>Nullable&lt;&gt;</c> 类型，则返回其基础类型；否则返回类型本身。</returns>
    /// <exception cref="ArgumentNullException">如果 <paramref name="type"/> 为 <c>null</c>。</exception>
    public static Type GetTypeFromNullable(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsNullableType() ? type.GetGenericArguments()[0] : type;
    }
    #endregion
}
