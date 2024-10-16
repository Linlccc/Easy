using Xunit;

namespace Easy.Extensions.Test.System;

public class TypeExtensions
{
    [Fact]
    public void IsInterfaceDefinitionInclude()
    {
        Assert.True(typeof(List<>).IsInterfaceDefinitionInclude(typeof(IList<>)));
        Assert.True(typeof(List<string>).IsInterfaceDefinitionInclude(typeof(IList<>)));
        Assert.True(typeof(List<>).IsInterfaceDefinitionInclude(typeof(IList<string>)));
        Assert.True(typeof(List<string>).IsInterfaceDefinitionInclude(typeof(IList<string>)));
    }
}
