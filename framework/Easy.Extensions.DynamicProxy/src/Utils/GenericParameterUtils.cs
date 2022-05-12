using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 泛型参数实用程序
/// </summary>
internal static class GenericParameterUtils
{
    /// <summary>
    /// 根据指定类型定义泛型参数
    /// </summary>
    /// <param name="typeBuilder">类型构建器</param>
    /// <param name="serviceType">服务类型（参考类型）</param>
    /// <returns>类型构建器</returns>
    public static TypeBuilder DefineGenericParameterByType(this TypeBuilder typeBuilder,Type serviceType)
    {
        if (!serviceType.IsGenericTypeDefinition) return typeBuilder;

        Type[] genericArguments = serviceType.GetGenericArguments();
        GenericTypeParameterBuilder[] genericTypeParameterBuilders = typeBuilder.DefineGenericParameters(genericArguments.Select(ga => ga.Name).ToArray());
        // 设置泛型参数的特性和约束
        foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in genericTypeParameterBuilders)
        {
            Type genericArgumentType = genericArguments[genericTypeParameterBuilder.GenericParameterPosition];
            genericTypeParameterBuilder.SetGenericParameterAttributes(genericArgumentType.GenericParameterAttributes);
            foreach (Type constraint in genericArgumentType.GetGenericParameterConstraints())
            {
                if (constraint.IsClass) genericTypeParameterBuilder.SetBaseTypeConstraint(constraint);
                if (constraint.IsInterface) genericTypeParameterBuilder.SetInterfaceConstraints(constraint);
            }
        }
        return typeBuilder;
    }
}
