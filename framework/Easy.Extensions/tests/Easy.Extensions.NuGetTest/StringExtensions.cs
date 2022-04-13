using System;
using Xunit;

namespace Easy.Extensions.Test;

public class StringExtensions
{
    [Fact]
    public void IsNullOrEmpty()
    {
        string str1 = null;
        string str2 = string.Empty;
        string str3 = "";
        string str4 = " ";
        string str5 = "abcdefg";

        Assert.True(str1.IsNullOrEmpty());
        Assert.True(str2.IsNullOrEmpty());
        Assert.True(str3.IsNullOrEmpty());
        Assert.False(str4.IsNullOrEmpty());
        Assert.False(str5.IsNullOrEmpty());
    }
}
