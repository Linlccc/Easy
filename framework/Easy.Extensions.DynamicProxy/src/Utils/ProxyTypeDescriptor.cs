using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 代理类型描述器实用程序
/// </summary>
internal class ProxyTypeDescriptorUtils
{
    public static ProxyTypeDescriptor Describe(ModuleBuilder _moduleBuilder,string proxyTypeName,Type serviceType,Type implementationType,Type[] interfaceTypes)
    {
        // 代理类型定义
        TypeAttributes proxyTypeDefine = TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed;
        // 定义代理类型构建器
        TypeBuilder typeBuilder = _moduleBuilder.DefineType(proxyTypeName, proxyTypeDefine, implementationType, interfaceTypes);

        // 根据服务类型定义类型参数
        typeBuilder.DefineGenericParameterByType(serviceType);

        // 设置默认字段
        Dictionary<string, FieldBuilder> fields = FieldUtils.SetDefaultField(typeBuilder, serviceType);


        return new ProxyTypeDescriptor(serviceType,typeBuilder,fields);
    }


}

/// <summary>
/// 代理类型描述器
/// </summary>
internal class ProxyTypeDescriptor
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public Type ServiceType { get; }

    /// <summary>
    /// 类型构建器
    /// </summary>
    public TypeBuilder Builder { get; }

    /// <summary>
    /// 字段
    /// </summary>
    public Dictionary<string,FieldBuilder> Fields { get; }

    /// <summary>
    /// 方法常量
    /// </summary>
    public MethodConstantCollection MethodConstants { get; }

    public ProxyTypeDescriptor(Type serviceType, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fields)
    {
        ServiceType = serviceType;
        Builder = typeBuilder;
        Fields = fields;
        MethodConstants = new(typeBuilder);
    }
}

/// <summary>
/// 方法集合
/// </summary>
internal class MethodConstantCollection
{
    /// <summary>
    /// 嵌套类型名称
    /// </summary>
    public const string NestedTypeName = "MethodConstant";
    /// <summary>
    /// 嵌套类型，用来存放实现类的方法
    /// </summary>
    private readonly TypeBuilder _nestedTypeBuilder;
    /// <summary>
    /// 嵌套类型构造函数il
    /// </summary>
    private readonly ILGenerator _nestedTypeConstructorILGen;
    /// <summary>
    /// 嵌套类型字段
    /// </summary>
    private readonly Dictionary<string, FieldBuilder> _nestedTypeFields;

    public MethodConstantCollection(TypeBuilder typeBuilder)
    {
        _nestedTypeBuilder = typeBuilder.DefineNestedType(NestedTypeName, TypeAttributes.NestedPrivate);
        _nestedTypeConstructorILGen = _nestedTypeBuilder.DefineTypeInitializer().GetILGenerator();
        _nestedTypeFields = new();
    }
}
