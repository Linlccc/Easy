using System;
using Easy.Extensions.DependencyInjection.Abstractions.Extensions;
using Xunit;

namespace Easy.Extensions.DependencyInjection.Abstractions.Test;

/// <summary>
/// 服务类型代理测试
/// </summary>
public class ServiceTypeProxyTests
{
    /// <summary>
    /// 测试类型转换
    /// </summary>
    [Fact]
    public void TestAssignableFrom()
    {
        Type proxyType = typeof(int).Proxy();

        Assert.True(proxyType.IsAssignableFrom(typeof(int)));
        Assert.True(typeof(int).IsAssignableFrom(proxyType));

        Assert.False(proxyType.IsAssignableFrom(typeof(string)));
        Assert.False(typeof(string).IsAssignableFrom(proxyType));
    }
}
