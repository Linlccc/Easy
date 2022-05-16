using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Easy.Extensions.DynamicProxy.Utils;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Testl;

public class ProxyTypeNameUtilsTest
{

    [Fact]
    public void GetProxyTypeName()
    {
        {
            ProxyTypeNameUtils proxyTypeNameUtils = new();
            ProxyTypeGeneratorUtils proxyTypeGeneratorUtils = new();
        }

        {
            Type t1 = typeof(TT<>);
            Type t2 = typeof(IT<,>);

            MethodInfo m1 = t2.GetMethod("get_T");
            MethodInfo[] m2s = t1.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        {
            Type t1 = typeof(IT<,>);
            var v1 = t1.GetTypeInfo().DeclaredMethods;
            v1.ToList().ForEach(v =>
            {
                var v2 = v.GetBindProperty();
            });
        }


        {
            Type t2 = typeof(TT<>);
            var v2 = t2.GetTypeInfo().DeclaredMethods;
            v2.ToList().ForEach(v =>
            {
                var v2 = v.GetBindProperty();
            });
        }

        {
            Type t1 = typeof(TT<>);
            t1.CustomAttributes.ToList().ForEach(v =>
            {
                var v2 = CustomAttributeUtils.GetCustomAttributeBuilder(v);
            });
        }

        
    }
}

[AttributeUsage(AttributeTargets.All)]
public class TAttribute : Attribute 
{
    public TAttribute(string name,params Type[] types)
    {
        Name = name;
        Types = new List<Type[]>();
        Types.Add(types);
        Types.Add(new Type[] {typeof(int)});
    }


    public string aaa = "nnnnnn";
    public string bbb { get; set; } = "nnnnnn";

    public string Name { get; }
    public List<Type[]> Types { get; set; }
}

[TAttribute("123",typeof(string))]
public interface IT<T1,T2>
{
    public int MyProperty { get; set; }


    [SpecialName]
    public void get_T();
}

[TAttribute("123", typeof(string), aaa = "111", bbb = null)]
public class TT<T1> : IT<T1, string>
{
    public Nullable<int> Value { get; set; } = 1;
    public int MyProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void get_T()
    {
        throw new NotImplementedException();
    }

    public void T()
    {
        throw new NotImplementedException();
    }
}
