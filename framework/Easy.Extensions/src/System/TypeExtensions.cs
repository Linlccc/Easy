namespace System;

/// <summary>
/// <see cref="Type"/> 扩展
/// </summary>
public static class TypeExtensions
{
    #region 继承类型扩展

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
    #endregion

    #region 检查
    /// <summary>
    /// 判断 <paramref name="type"/> 是否为泛型,并且所有的类型参数都未指定特定类型
    /// </summary>
    /// <param name="type">要判断的类型</param>
    /// <returns>是开放完全泛型返回true，否者false</returns>
    public static bool IsAllOpenGeneric(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsGenericType && !type.GenericTypeArguments.Any(a => a.GUID != Guid.Empty);
    }

    /// <summary>
    /// 判断 <paramref name="type"/> 是否是可空类型
    /// <br>Nullable[T] 类型</br>
    /// </summary>
    /// <param name="type">判断的类型</param>
    /// <returns>如果是可空类型返回true,否者返回false</returns>
    public static bool IsNullableType(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.GetTypeDefinition() == typeof(Nullable<>);
    }
    #endregion

    /// <summary>
    /// 获取类型的名称
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
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
    /// 获取类型的完全限定名
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public static string FullName(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        if (!type.IsGenericType) return type.FullName ?? type.Name;

        string argumentNames = string.Join(",", type.GetGenericArguments().Select(t => t.FullName()));
        string fullName = type.FullName ?? type.GetTypeDefinition().FullName!;
        string typeName = fullName.Remove(fullName.IndexOf('`'));
        string result = $"{typeName}<{argumentNames}>";
        return result;
    }

    /// <summary>
    /// 获取类型定义
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Type GetTypeDefinition(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsGenericType && !type.IsGenericTypeDefinition ? type.GetGenericTypeDefinition() : type;
    }

    /// <summary>
    /// 从指定类型中获取不可空类型
    /// </summary>
    /// <param name="type">获取不可空类型的类型</param>
    /// <returns>不可空的类型</returns>
    public static Type GetNonNullableType(this Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return type.IsNullableType() ? type.GetGenericArguments()[0] : type;
    }

}
