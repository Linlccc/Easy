namespace Easy.Extensions.DynamicProxy.Reflection;

/// <summary>
/// 成员反射器
/// </summary>
public abstract partial class MemberReflector<TMember> where TMember : MethodInfo
{
    protected TMember _reflectionInfo;

    protected MemberReflector(TMember reflectionInfo)
    {
        _reflectionInfo = reflectionInfo;
    }
}
