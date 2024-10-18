using System.Reflection;

namespace Easy.Extensions.Test.System.Reflection;

public class PropertyInfoExtensions
{
    [Fact]
    public void SetPropertyValue()
    {
        PropertyInfoTest test = new();
        Type type = test.GetType();

        PropertyInfo propNum = type.GetProperty(nameof(PropertyInfoTest.Num))!;
        Assert.NotNull(propNum);
        Assert.Equal(0, propNum.GetValue(test));
        propNum.SetPropertyValue(test, 10);
        Assert.Equal(10, propNum.GetValue(test));

        PropertyInfo propI = type.GetProperty(nameof(PropertyInfoTest.I))!;
        Assert.NotNull(propI);
        Assert.Equal(0, propI.GetValue(test));
        propI.SetPropertyValue(test, 100);
        Assert.Equal(100, propI.GetValue(test));
    }
}

public class PropertyInfoTest
{
    public int I { get; }

    public int Num { get; set; }
}
