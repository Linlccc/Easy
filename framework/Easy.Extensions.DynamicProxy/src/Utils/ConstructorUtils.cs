using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 构造函数实用程序
/// </summary>
internal class ConstructorUtils
{
    /// <summary>
    /// 设置构造函数
    /// </summary>
    /// <param name="proxyTypeDescriptor">代理类型描述器</param>
    /// <param name="serviceType">服务类型</param>
    public static void SetConstuctor(ProxyTypeDescriptor proxyTypeDescriptor,Type serviceType)
    {
        // 定义构造函数
        ConstructorBuilder constructorBuilder = proxyTypeDescriptor.Builder.DefineConstructor(MethodAttributes.Public, MethodUtils.ObjectCtor.CallingConvention, new Type[] { typeof(IAspectActivatorFactory), serviceType });
        ILGenerator ctorILGen = constructorBuilder.GetILGenerator();

        // 运行object构造函数
        ctorILGen.LoadArg(0);
        ctorILGen.Call(MethodUtils.ObjectCtor);
        // 设置字段值
        ctorILGen.LoadArg(0);
        ctorILGen.LoadArg(1);
        ctorILGen.SetField(proxyTypeDescriptor.Fields[FieldUtils.AspectActivatorFactory]);

        ctorILGen.LoadArg(0);
        ctorILGen.LoadArg(2);
        ctorILGen.SetField(proxyTypeDescriptor.Fields[FieldUtils.Implementation]);

        ctorILGen.Return();
    }
}
