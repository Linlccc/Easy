namespace Easy.Extensions.DynamicProxy.Reflection;

/// <summary>
/// 自定义特性反射器
/// </summary>
public class CustomAttributeReflector
{
    private readonly CustomAttributeData _customAttributeData;
    private readonly Type _attributeType;
    private readonly Func<Attribute> _invoker;

    internal readonly HashSet<RuntimeTypeHandle> _tokens;

    private CustomAttributeReflector(CustomAttributeData customAttributeData)
    {
        _customAttributeData = customAttributeData;
        _attributeType = customAttributeData.AttributeType;
        _invoker = CreateInvoker();
        _tokens = GetAttrTokens(_attributeType);
    }

    private Func<Attribute> CreateInvoker()
    {
        return null!;
    }

    private HashSet<RuntimeTypeHandle> GetAttrTokens(Type attributeType)
    {
        return null!;
    }
}
