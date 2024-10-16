namespace Easy.Extensions.Test.System;

public class StringExtensions
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("abcdefg", false)]
    public void IsNullOrEmpty(string? val, bool expected)
    {
        Assert.Equal(expected, val.IsNullOrEmpty());
    }
}
