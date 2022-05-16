using System.Reflection;
using Xunit;

namespace Easy.Extensions.Test;

public class ICustomAttributeProviderExtensions
{
    [Fact]
    public void GetCustomAttributes()
    {
        Type type = typeof(MyClass);

        // 根据特性类型获取特性
        var a1 = type.GetAttribute<MyAttribute>(false);
        Assert.NotNull(a1);
        // 根据特性的接口类型获取特性
        var a2 = type.GetAttribute<IMyInterface>(false);
        Assert.NotNull(a2);
    }


    public interface IMyInterface
    {
        void MyMethod();
    }
    public class MyAttribute : Attribute, IMyInterface
{
        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    [MyAttribute]
    public class MyClass
    {
        
    }
}
