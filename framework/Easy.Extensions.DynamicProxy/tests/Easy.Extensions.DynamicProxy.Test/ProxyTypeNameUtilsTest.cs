using Easy.Extensions.DynamicProxy.Utils;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Testl;

public class ProxyTypeNameUtilsTest
{

    [Fact]
    public void GetProxyTypeName()
    {
        ProxyTypeNameUtils proxyTypeNameUtils = new ProxyTypeNameUtils();
        string n = proxyTypeNameUtils.GetProxyTypeName(typeof(string), typeof(int));
    }
}
