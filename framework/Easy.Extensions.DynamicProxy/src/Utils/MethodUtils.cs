using System.Reflection;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 方法实用程序
/// </summary>
internal static class MethodUtils
{
    /// <summary>
    /// object 的构造函数
    /// </summary>
    public static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes)!;
    /// <summary>
    /// 实现接口方法的特性
    /// </summary>
    public static readonly MethodAttributes ImplementInterfaceMethodAttributes = MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot;

    /// <summary>
    /// 设置接口方法代理
    /// </summary>
    /// <param name="proxyTypeDescriptor">代理类型描述器</param>
    /// <param name="interfaceType">接口类型</param>
    /// <param name="classType">实现类型</param>
    /// <param name="additionalInterfaceTypes">附加皆苦类型</param>
    public static void SetInterfaceMethodProxy(ProxyTypeDescriptor proxyTypeDescriptor, Type interfaceType, Type classType, params Type[] additionalInterfaceTypes)
    {
        foreach (MethodInfo method in interfaceType.GetTypeInfo().DeclaredMethods.Where(dm => !dm.IsBindProperty()))
        {

        }
    }

    public static MethodBuilder DefineInterfaceImplementMethod(this TypeBuilder typeBuilder, MethodInfo interfaceMethod)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod(interfaceMethod.Name, ImplementInterfaceMethodAttributes, interfaceMethod.ReturnType, interfaceMethod.GetParameters().Select(p => p.ParameterType).ToArray());
        ILGenerator methodILGen = methodBuilder.GetILGenerator();
        
        return methodBuilder;
    }
    
    /// <summary>
    /// 定义方法
    /// </summary>
    /// <param name="proxyTypeDescriptor">代理类型描述器</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="refMethod">参考方法</param>
    /// <param name="methodAttributes">方法特性</param>
    /// <param name="classType">实现类型</param>
    /// <returns></returns>
    private static MethodBuilder DefineMethod(ProxyTypeDescriptor proxyTypeDescriptor,string methodName,MethodInfo refMethod,MethodAttributes methodAttributes,Type classType)
    {
        MethodBuilder methodBuilder = proxyTypeDescriptor.Builder.DefineMethod(methodName, methodAttributes, refMethod.CallingConvention, refMethod.ReturnType, refMethod.GetParameters().Select(p => p.ParameterType).ToArray());

        // 根据参考方法定义泛型参数
        methodBuilder.DefineGenericParameterByMethod(refMethod);

        // 设置自定义特性
        foreach (CustomAttributeData attributeData in refMethod.CustomAttributes) methodBuilder.SetCustomAttribute(CustomAttributeUtils.GetCustomAttributeBuilder(attributeData));

        // 设置参数
        methodBuilder.DefineParameters(refMethod);

        return methodBuilder;
    }
}
