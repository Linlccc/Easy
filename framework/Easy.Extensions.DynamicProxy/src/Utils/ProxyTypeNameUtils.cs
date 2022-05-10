namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 代理类型名称实用程序
/// </summary>
public class ProxyTypeNameUtils
{
    /// <summary>
    /// 代理类型命名空间
    /// </summary>
    private const string ProxyTypeNamespace = "Easy.DynamicProxy.Generate";
    /// <summary>
    /// 代理类型名称索引
    /// </summary>
    private readonly Dictionary<string, int> _proxyTypeNameIndex;
    /// <summary>
    /// 代理类型名称地图
    /// </summary>
    private readonly Dictionary<Tuple<Type,Type>,string> _proxyTypeNameMap;

    public ProxyTypeNameUtils()
    {
        _proxyTypeNameIndex = new();
        _proxyTypeNameMap = new();
    }

    public string GetProxyTypeName(Type serviceType,Type implementationType)
    {
        Tuple<Type,Type> mapKey = Tuple.Create(serviceType,implementationType);
        if(_proxyTypeNameMap.TryGetValue(mapKey, out string? proxyTypeName))return proxyTypeName;

        proxyTypeName = $"{ProxyTypeNamespace}.{serviceType.Name()}";
        if (!_proxyTypeNameIndex.TryGetValue(proxyTypeName, out int index)) _proxyTypeNameIndex[proxyTypeName] = 0;
        proxyTypeName += $"_{Interlocked.Increment(ref index)}";

        _proxyTypeNameMap[mapKey] = proxyTypeName;
        return proxyTypeName;
    }
}
