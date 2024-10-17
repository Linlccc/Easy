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

    [Theory]
    [InlineData(typeof(List<>), true)]
    [InlineData(typeof(List<string>), false)]
    [InlineData(typeof(Tuple<,,>), true)]
    [InlineData(typeof(Tuple<string, int, int>), false)]
    public void IsAllOpenGeneric(Type type, bool expected)
    {
        Assert.Equal(expected, type.IsAllOpenGeneric());
    }

    [Theory]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), true)]
    public void IsNullableType(Type type, bool expected)
    {
        Assert.Equal(expected, type.IsNullableType());
    }


    [Theory]
    [InlineData(typeof(string), "String")]
    [InlineData(typeof(Tuple<string, int>), "Tuple<String,Int32>")]
    [InlineData(typeof(Tuple<string, Tuple<int, float>>), "Tuple<String,Tuple<Int32,Single>>")]
    public void Name(Type type, string expected)
    {
        Assert.Equal(expected, type.Name());
    }

    [Theory]
    [InlineData(typeof(string), "System.String")]
    [InlineData(typeof(Tuple<string, int>), "System.Tuple<System.String,System.Int32>")]
    [InlineData(typeof(Tuple<string, Tuple<int, float>>), "System.Tuple<System.String,System.Tuple<System.Int32,System.Single>>")]
    public void FullName(Type type, string expected)
    {
        Assert.Equal(expected, type.FullName());
    }

    [Theory]
    [InlineData(typeof(string), typeof(string))]
    [InlineData(typeof(List<int>), typeof(List<>))]
    public void GetTypeDefinition(Type type, Type expected)
    {
        Assert.Equal(expected, type.GetTypeDefinition());
    }

    [Theory]
    [InlineData(typeof(string), typeof(string))]
    [InlineData(typeof(int?), typeof(int))]
    //[InlineData(typeof(Nullable<>), typeof(T))]
    public void GetTypeFromNullable(Type type, Type expected)
    {
        Assert.Equal(expected, type.GetTypeFromNullable());
    }
}
