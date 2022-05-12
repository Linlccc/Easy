namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 代理类型名称实用程序
/// </summary>
internal class ProxyTypeNameUtils
{
    /// <summary>
    /// 代理类型命名空间
    /// </summary>
    private const string ProxyTypeNamespace = "Easy.DynamicProxy.Generate";
    /// <summary>
    /// 代理类型名称索引
    /// </summary>
    private readonly Dictionary<string, NameIndex> _proxyTypeNameIndex;
    /// <summary>
    /// 代理类型名称地图
    /// </summary>
    private readonly Dictionary<Tuple<Type,Type>,string> _proxyTypeNameMap;

    public ProxyTypeNameUtils()
    {
        _proxyTypeNameIndex = new();
        _proxyTypeNameMap = new();
    }

    /// <summary>
    /// 获取代理类型名称
    /// </summary>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationType">实现类型</param>
    /// <returns></returns>
    public string GetProxyTypeName(Type serviceType,Type implementationType)
    {
        Tuple<Type,Type> mapKey = Tuple.Create(serviceType,implementationType);
        if(_proxyTypeNameMap.TryGetValue(mapKey, out string? proxyTypeName))return proxyTypeName;

        proxyTypeName = $"{ProxyTypeNamespace}.{serviceType.Name()}";
        if (!_proxyTypeNameIndex.TryGetValue(proxyTypeName, out NameIndex? nameIndex)) _proxyTypeNameIndex[proxyTypeName] = nameIndex = new NameIndex();
        proxyTypeName += $"_{nameIndex.Index()}";

        _proxyTypeNameMap[mapKey] = proxyTypeName;
        return proxyTypeName;
    }

    /// <summary>
    /// 名称索引
    /// </summary>
    private class NameIndex
    {
        private int _index = 0;
        
        public int Index() => Interlocked.Increment(ref _index);
    }
}
