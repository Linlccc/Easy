using Easy.Extensions.DynamicProxy.Utils;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Testl;

public class ProxyTypeNameUtilsTest
{

    [Fact]
    public void GetProxyTypeName()
    {
        TT<string> tT = new TT<string>();
        Type t1 = tT.Value.GetType();
    }
}


public interface IT<T1,T2>
{

}

public class TT<T1> : IT<T1, string>
{
    public Nullable<int> Value { get; set; } = 1;
}
