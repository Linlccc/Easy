using System.Reflection;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 方法实用程序
/// </summary>
internal class MethodUtils
{
    /// <summary>
    /// object 的构造函数
    /// </summary>
    public static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes)!;
}
