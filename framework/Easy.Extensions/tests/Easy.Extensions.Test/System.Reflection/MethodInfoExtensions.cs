using System.Reflection;

namespace Easy.Extensions.Test.System.Reflection;

public class MethodInfoExtensions
{
    [Theory]
    [InlineData("SetNum", null)]
    [InlineData("get_Num", "Num")]
    [InlineData("set_Num", "Num")]
    public void GetBindProperty(string methodName, string? propName)
    {
        MethodInfo? method = typeof(MethodInfoTest).GetMethod(methodName);
        Assert.NotNull(method);

        PropertyInfo? property = method!.GetBindProperty();
        Assert.Equal(propName, property?.Name);
    }

    [Theory]
    [InlineData("SetNum", false)]
    [InlineData("get_Num", true)]
    [InlineData("set_Num", true)]
    public void IsBindProperty(string methodName, bool isPropMet)
    {
        MethodInfo? method = typeof(MethodInfoTest).GetMethod(methodName);
        Assert.NotNull(method);

        Assert.Equal(isPropMet, method!.IsBindProperty());
    }
}

public class MethodInfoTest
{
    public int Num { get; set; }

    public void SetNum(int num) => Num = num;
}
