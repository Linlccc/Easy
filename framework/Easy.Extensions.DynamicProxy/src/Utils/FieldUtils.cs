using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 字段使用程序
/// </summary>
internal class FieldUtils
{
    /// <summary>
    /// 片段激活器工厂字段名
    /// </summary>
    public const string AspectActivatorFactory = "_aspectActivatorFactory";
    /// <summary>
    /// 实现对象
    /// </summary>
    public const string Implementation = "_implementation";

    /// <summary>
    /// 设置默认字段
    /// </summary>
    /// <param name="typeBuilder">类型构建器</param>
    /// <param name="serviceType">服务类型</param>
    /// <returns>默认字段</returns>
    public static Dictionary<string,FieldBuilder> SetDefaultField(TypeBuilder typeBuilder,Type serviceType)
    {
        Dictionary<string, FieldBuilder> fields = new();
        fields[AspectActivatorFactory] = typeBuilder.DefineField(AspectActivatorFactory, typeof(IAspectActivatorFactory), FieldAttributes.Private);
        fields[AspectActivatorFactory] = typeBuilder.DefineField(Implementation, serviceType, FieldAttributes.Private);
        return fields;
    }
}
