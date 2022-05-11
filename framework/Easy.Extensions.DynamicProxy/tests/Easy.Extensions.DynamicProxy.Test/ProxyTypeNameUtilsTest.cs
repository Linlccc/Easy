using Easy.Extensions.DynamicProxy.Utils;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Testl;

public class ProxyTypeNameUtilsTest
{

    [Fact]
    public void GetProxyTypeName()
    {
        ProxyTypeNameUtils proxyTypeNameUtils = new ProxyTypeNameUtils();
        string n1 = proxyTypeNameUtils.GetProxyTypeName(typeof(string), typeof(int));
        string n2 = proxyTypeNameUtils.GetProxyTypeName(typeof(string), typeof(int));

    }
}
