using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 服务类型代理
/// </summary>
public class ServiceTypeProxy : TypeDelegator
{
    private readonly int _hashCode;

    internal ServiceTypeProxy(Type type, string? key = null) : base(type is ServiceTypeProxy serviceTypeProxy ? serviceTypeProxy.typeImpl : type)
    {
        Key = key;
        _hashCode = HashCode.Combine(typeImpl, Key);
    }

    /// <summary>
    /// 类型自定义key
    /// <br>参与 <see cref="_hashCode"/> 计算</br>
    /// </summary>
    public string? Key { get; }

    public override int GetHashCode() => _hashCode;

    /// <summary>
    /// 获取泛型类型定义是也代理一下
    /// </summary>
    /// <returns></returns>
    public override Type GetGenericTypeDefinition() => new ServiceTypeProxy(typeImpl.GetGenericTypeDefinition(), Key);

    #region 使用原类型数据
    public override bool ContainsGenericParameters => typeImpl.ContainsGenericParameters;
    public override IEnumerable<CustomAttributeData> CustomAttributes => typeImpl.CustomAttributes;
    public override MethodBase? DeclaringMethod => typeImpl.DeclaringMethod;
    public override Type[] GenericTypeArguments => typeImpl.GenericTypeArguments;
    public override Type[] GenericTypeParameters => typeImpl.GetTypeInfo().GenericTypeParameters;
    public override bool IsGenericType => typeImpl.IsGenericType;
    public override bool IsGenericTypeDefinition => typeImpl.IsGenericTypeDefinition;
    public override bool IsSecurityCritical => typeImpl.IsSecurityCritical;
    public override bool IsSecuritySafeCritical => typeImpl.IsSecuritySafeCritical;
    public override bool IsSecurityTransparent => typeImpl.IsSecurityTransparent;
    public override StructLayoutAttribute? StructLayoutAttribute => typeImpl.StructLayoutAttribute;

    public override Type[] GetGenericArguments() => typeImpl.GetGenericArguments();
    #endregion
}
