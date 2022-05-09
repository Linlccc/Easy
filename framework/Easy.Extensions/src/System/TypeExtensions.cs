namespace System;

/// <summary>
/// <see cref="Type"/> 拓展
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// 判断 <paramref name="type"/> 的接口定义是否包含 <paramref name="baseType"/> 的定义
    /// <br>如果还要判断父类的话请使用 <see cref="IsInheritFrom"/> 方法</br>
    /// </summary>
    /// <param name="type">要判断的类型</param>
    /// <param name="baseType">是否实现的接口</param>
    /// <returns>实现返回true，否者false</returns>
    public static bool IsInterfaceDefinitionInclude(this Type type, Type baseType) => type.GetInterfaces().Any(t => t.GetTypeDefinition() == baseType.GetTypeDefinition());

    /// <summary>
    /// 判断 <paramref name="type"/> 类型是否继承自 <paramref name="baseType"/> 类型
    /// <br>如果是泛型则只判断类型定义</br>
    /// </summary>
    /// <param name="type">当前类型</param>
    /// <param name="baseType">判断是否是基类的类型</param>
    /// <returns></returns>
    public static bool IsInheritFrom(this Type type, Type baseType)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));
        _ = baseType ?? throw new ArgumentNullException(nameof(baseType));

        baseType = baseType.GetTypeDefinition();

        // 如果有已实现的接口直接返回结果
        if (type.GetInterfaces().Any(t => t.GetTypeDefinition() == baseType)) return true;

        // 判断基类
        while (type != null && type != typeof(object))
        {
            if (type.GetTypeDefinition() == baseType) return true;
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            type = type.BaseType;
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
        }

        return false;
    }

    

    /// <summary>
    /// 判断是否是开放泛型
    /// </summary>
    /// <param name="type">要判断的类型</param>
    /// <returns>是开放泛型返回true，否者false</returns>
    public static bool IsOpenGeneric(this Type type) => type.IsGenericType && !type.GenericTypeArguments.Any(a => a.GUID != Guid.Empty);

    /// <summary>
    /// 获取类型定义
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Type GetTypeDefinition(this Type type) => type.IsGenericType && !type.IsGenericTypeDefinition ? type.GetGenericTypeDefinition() : type;

    /// <summary>
    /// 获取类型的名称
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public static string Name(this Type type)
    {
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
        if (!type.IsGenericType) return type.FullName ?? type.Name;

        string argumentNames = string.Join(",", type.GetGenericArguments().Select(t => t.FullName()));
        string fullName = type.FullName ?? type.GetTypeDefinition().FullName!;
        string typeName = fullName.Remove(fullName.IndexOf('`'));
        string result = $"{typeName}<{argumentNames}>";
        return result;
    }

}
