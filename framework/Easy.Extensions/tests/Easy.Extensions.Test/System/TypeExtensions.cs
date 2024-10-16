namespace Easy.Extensions.Test.System;

public class TypeExtensions
{
    [Theory]
    [InlineData(typeof(List<>), typeof(IList<>), true)]
    [InlineData(typeof(List<string>), typeof(IList<>), true)]
    [InlineData(typeof(List<>), typeof(IList<string>), true)]
    [InlineData(typeof(List<string>), typeof(IList<string>), true)]
    [InlineData(typeof(List<>), typeof(List<>), false)]
    public void IsInterfaceDefinitionInclude(Type type, Type interfaceType, bool expected)
    {
        Assert.Equal(expected, type.IsImplementsInterfaceDefinition(interfaceType));
    }

    [Theory]
    [InlineData(typeof(List<>), typeof(IList<>), false)]
    [InlineData(typeof(List<string>), typeof(IList<>), false)]
    [InlineData(typeof(List<>), typeof(IList<string>), false)]
    [InlineData(typeof(List<string>), typeof(IList<string>), true)]
    [InlineData(typeof(List<>), typeof(List<>), false)]
    public void IsImplementsInterface(Type type, Type interfaceType, bool expected)
    {
        Assert.Equal(expected, type.IsImplementsInterface(interfaceType));
    }
}
