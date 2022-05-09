using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 代理类型生成器
/// </summary>
internal class ProxyTypeGeneratorUtils
{
    /// <summary>
    /// 代理类型程序集名称
    /// </summary>
    private const string ProxyAssemblyName = "Easy.DynamicProxy.Generate";
    /// <summary>
    /// 模块构建器
    /// </summary>
    private readonly ModuleBuilder moduleBuilder;


    public ProxyTypeGeneratorUtils()
    {
        moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(ProxyAssemblyName), AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule(ProxyAssemblyName);
    }


    internal Type CreateInterfaceProxyType(Type interfaceType, Type classType)
    {
        string typeName = $"{interfaceType.FullName}Proxy";

        TypeBuilder typeBuild = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed, classType, new[] { interfaceType });



        return Type.GetType("")!;
    }


    /// <summary>
    /// 定义泛型参数
    /// </summary>
    /// <param name="typeBuilder">类型构造器</param>
    /// <param name="targetType">目标类型</param>
    private static void DefineGenericParameter(TypeBuilder typeBuilder,Type targetType)
    {
        if (!targetType.IsGenericTypeDefinition) return;

        Type[] genericArgumentTypes = targetType.GetGenericArguments();
        GenericTypeParameterBuilder[] genericTypeParameterBuilders = typeBuilder.DefineGenericParameters(genericArgumentTypes.Select(gat=>gat.Name).ToArray());
        // 配置泛型参数的约束信息
        foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in genericTypeParameterBuilders)
        {
            Type genericArgumentType = genericArgumentTypes[genericTypeParameterBuilder.GenericParameterPosition];
            genericTypeParameterBuilder.SetGenericParameterAttributes(genericArgumentType.GenericParameterAttributes);
            foreach (Type constraint in genericArgumentType.GetGenericParameterConstraints())
            {
                if (constraint.IsClass) genericTypeParameterBuilder.SetBaseTypeConstraint(constraint);
                if (constraint.IsInterface) genericTypeParameterBuilder.SetInterfaceConstraints(constraint);
            }
        }

    }
}

