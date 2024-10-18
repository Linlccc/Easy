using System.Reflection;

namespace Easy.Extensions.Test.System.Reflection;

public class ICustomAttributeProviderExtensions
{
    [Fact]
    public void GetAttributes()
    {
        Type type = typeof(AttributeTest1);

        IEnumerable<Test1Attribute?> atts1 = type.GetAttributes<Test1Attribute>(false);
        Assert.Single(atts1);

        IEnumerable<Test2Attribute?> atts2 = type.GetAttributes<Test2Attribute>(false);
        Assert.Empty(atts2);

        IEnumerable<IAttribute?> atts3 = type.GetAttributes<IAttribute>(false);
        Assert.Single(atts3);
    }

    [Fact]
    public void GetAttribute()
    {
        Type type = typeof(AttributeTest1);

        Test1Attribute? atts1 = type.GetAttribute<Test1Attribute>(false);
        Assert.NotNull(atts1);

        Test2Attribute? atts2 = type.GetAttribute<Test2Attribute>(false);
        Assert.Null(atts2);

        IAttribute? atts3 = type.GetAttribute<IAttribute>(false);
        Assert.NotNull(atts3);
    }

    [Fact]
    public void IsExistAttribute()
    {
        Type type = typeof(AttributeTest1);

        Assert.True(type.IsExistAttribute(false, out Test1Attribute? att1));
        Assert.NotNull(att1);

        Assert.False(type.IsExistAttribute(false, out Test2Attribute? att2));
        Assert.Null(att2);

        Assert.True(type.IsExistAttribute(false, out IAttribute? att3));
        Assert.NotNull(att3);
    }
}

// 特性接口
public interface IAttribute { }
// 测试自定义特性

[AttributeUsage(AttributeTargets.All)]
public class Test1Attribute : Attribute, IAttribute { }
[AttributeUsage(AttributeTargets.All)]
public class Test2Attribute : Attribute, IAttribute { }

[Test1]
public class AttributeTest1 { }
