namespace Easy.Extensions.Test.System.Collections.Generic;

public class IEnumerableExtensions
{
    [Theory]
    [InlineData(null, true)]
    [InlineData(new int[0], true)]
    [InlineData(new[] { 1, 2, 3 }, false)]
    public void IsNullOrEmpty(IEnumerable<int>? collection, bool expected)
    {
        Assert.Equal(expected, collection.IsNullOrEmpty());
    }
}
