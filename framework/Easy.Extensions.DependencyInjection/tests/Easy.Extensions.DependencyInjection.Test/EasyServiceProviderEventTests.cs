using Easy.Extensions.DependencyInjection.Abstractions.Extensions;
using Easy.Extensions.DependencyInjection.Test.Models;

namespace Easy.Extensions.DependencyInjection.Test;

/// <summary>
/// EasyServiceProvider 事件测试
/// </summary>
public class EasyServiceProviderEventTests
{
    [Fact]
    public void EventTests()
    {
        IServiceProvider service = new EasyServiceProviderFactory(new EasyServiceProviderOptions()
        {
            RegisterScanAssemblys = new Assembly[] { GetType().Assembly },
            ServiceProviderEventsType = typeof(EasyServiceProviderEventTests_Event)

        }).CreateServiceProvider(new ServiceCollection());

        TypeR1 typeR1_1 = service.GetRequiredService<TypeR1>();
        Assert.NotNull(typeR1_1);

        // 该实例通过获取服务时的aop修改了，获取的对象一个是 TypeR2 的实例
        TypeR2 typeR2_1 = (TypeR2)service.GetRequiredService(typeof(TypeR1).WearMask("ToTypeR2"));
        Assert.NotNull(typeR2_1);
    }
}

/// <summary>
/// 测试服务提供商的AOP
/// </summary>
public class EasyServiceProviderEventTests_Event : EasyServiceProviderEvents
{
    /// <summary>
    /// 获取服务前
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public override Task BeforeGetService(IServiceProvider serviceProvider,ref Type serviceType)
    {
        if (typeof(TypeR1).WearMask("ToTypeR2") == serviceType)
        {
            serviceType = typeof(ITypeR2);
        }
        return base.BeforeGetService(serviceProvider,ref serviceType);
    }

    /// <summary>
    /// 获取服务后，成员注入前
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceType"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public override Task AfterGetService(IServiceProvider serviceProvider, Type serviceType,ref object? instance)
    {
        return base.AfterGetService(serviceProvider, serviceType,ref instance);
    }

    /// <summary>
    /// 获取服务完成
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceType"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public override Task GetServiceCompleted(IServiceProvider serviceProvider, Type serviceType,ref object? instance)
    {
        return base.GetServiceCompleted(serviceProvider, serviceType,ref instance);
    }
}
