using System;
using Easy.Extensions.DependencyInjection.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Easy.Extensions.DependencyInjection.Test;

/// <summary>
/// 服务提供商测试
/// </summary>
public class EasyServiceProviderTests
{
    /// <summary>
    /// 获取服务提供商，以自己注册
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private EasyServiceProvider GetEasyServiceProvider(IServiceCollection? services = null)
    {
        services ??= new ServiceCollection();
        return services.AutoRegister(GetType().Assembly).BuildEasyServiceProvider();
    }



    /// <summary>
    /// 测试服务提供商 获取IServiceProvider(范围)、IServiceScopeFactory(单例)
    /// </summary>
    [Fact(DisplayName = "测试服务提供商 获取IServiceProvider(范围)、IServiceScopeFactory(单例)")]
    public void ServiceProvider()
    {
        EasyServiceProvider service1 = GetEasyServiceProvider();

        // 从根容器获取应该相等
        IServiceProvider rootSp1 = service1.GetRequiredService<IServiceProvider>();
        IServiceScopeFactory rootSsf1 = service1.GetRequiredService<IServiceScopeFactory>();
        Assert.NotNull(rootSp1);
        Assert.True(rootSp1 == rootSsf1);

        // 创建一个新范围
        IServiceProvider service2 = service1.CreateScope().ServiceProvider;
        // 在次获取
        IServiceProvider scopeSp1 = service2.GetRequiredService<IServiceProvider>();
        IServiceScopeFactory scopeSsf1 = service2.GetRequiredService<IServiceScopeFactory>();
        Assert.NotNull(scopeSp1);
        Assert.True(scopeSp1 != scopeSsf1);
        Assert.True(rootSsf1 == scopeSsf1);
    }

    /// <summary>
    /// 测试服务提供商处理
    /// </summary>
    [Fact(DisplayName = "测试服务提供商处理(单独运行)")]
    public void ServiceProviderDictionary()
    {
        EasyServiceProvider service1 = GetEasyServiceProvider();

        // 通过反射得到 服务提供商字典
        System.Collections.ICollection servicProviderDictionary = (service1.GetType().GetField("_serviceProviders", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!.GetValue(service1) as System.Collections.ICollection)!;
        Assert.Equal(1, servicProviderDictionary.Count);

        IServiceProvider scopeFastSp1 = service1.CreateScope().ServiceProvider;
        Assert.Equal(2, servicProviderDictionary.Count);
        IServiceProvider scopeFastSp2 = service1.CreateScope().ServiceProvider;
        Assert.Equal(3, servicProviderDictionary.Count);
        IServiceProvider scopeFastSp3 = service1.CreateScope().ServiceProvider;
        Assert.Equal(4, servicProviderDictionary.Count);
        IServiceProvider scopeFastSp4 = service1.CreateScope().ServiceProvider;
        Assert.Equal(5, servicProviderDictionary.Count);

        (scopeFastSp1 as IDisposable)!.Dispose();
        Assert.Equal(4, servicProviderDictionary.Count);
        (scopeFastSp2 as IDisposable)!.Dispose();
        Assert.Equal(3, servicProviderDictionary.Count);
        (scopeFastSp3 as IDisposable)!.Dispose();
        Assert.Equal(2, servicProviderDictionary.Count);
        (scopeFastSp4 as IDisposable)!.Dispose();
        Assert.Equal(1, servicProviderDictionary.Count);
        (service1 as IDisposable)!.Dispose();
        Assert.Equal(0, servicProviderDictionary.Count);
    }

    /// <summary>
    /// 测试自动注册
    /// </summary>
    [Fact(DisplayName = "测试自动注册")]
    public void AutoRegister()
    {
        IServiceCollection services = new ServiceCollection();
        EasyServiceProvider service = GetEasyServiceProvider(services);
        Assert.Equal(20, services.Count);

        // 类型注册
        // 1
        List<TypeR1> typeR1s = service.GetServices<TypeR1>().ToList();
        Assert.Equal(1, typeR1s.Count);
        Assert.Equal(typeof(TypeR1), typeR1s[0].GetType());
        // 2
        List<ITypeR2> itypeR2s = service.GetServices<ITypeR2>().ToList();
        Assert.Equal(2, itypeR2s.Count);
        Assert.Equal(typeof(TypeR2), itypeR2s[0].GetType());
        Assert.Equal(typeof(TypeR3), itypeR2s[1].GetType());
        // 1
        List<TypeR3> typeR3s = service.GetServices<TypeR3>().ToList();
        Assert.Equal(1, typeR3s.Count);
        Assert.Equal(typeof(TypeR4), typeR3s[0].GetType());
        // 1
        List<ITypeR3> itypeR3s = service.GetServices<ITypeR3>().ToList();
        Assert.Equal(1, itypeR3s.Count);
        Assert.Equal(typeof(TypeR3), itypeR3s[0].GetType());
        // 1
        List<ITypeR4> itypeR4s = service.GetServices<ITypeR4>().ToList();
        Assert.Equal(1, itypeR4s.Count);
        Assert.Equal(typeof(TypeR4), itypeR4s[0].GetType());
        // 1
        List<TypeR5> typeR5s = service.GetServices<TypeR5>().ToList();
        Assert.Equal(1, typeR5s.Count);
        Assert.Equal(typeof(TypeR5), typeR5s[0].GetType());
        // 1
        List<ITypeR6<string>> typeR6s_string = service.GetServices<ITypeR6<string>>().ToList();
        Assert.Equal(1, typeR6s_string.Count);
        Assert.Equal(typeof(TypeR6), typeR6s_string[0].GetType());
        // 1
        List<ITypeR7<string>> typeR7s_string = service.GetServices<ITypeR7<string>>().ToList();
        Assert.Equal(1, typeR7s_string.Count);
        Assert.Equal(typeof(TypeR7<string>), typeR7s_string[0].GetType());
        // 1
        List<ITypeR8<string, int>> typeR8s_string_int = service.GetServices<ITypeR8<string, int>>().ToList();
        Assert.Equal(1, typeR8s_string_int.Count);
        Assert.Equal(typeof(TypeR8<string, int>), typeR8s_string_int[0].GetType());

        // 工厂注册
        // 1
        List<FacotryR1> facotryR1s = service.GetServices<FacotryR1>().ToList();
        Assert.Equal(1, facotryR1s.Count);
        Assert.Equal(typeof(FacotryR1), facotryR1s[0].GetType());
        // 2
        List<FacotryR2> facotryR2s = service.GetServices<FacotryR2>().ToList();
        Assert.Equal(2, facotryR2s.Count);
        Assert.Equal(typeof(FacotryR2), facotryR2s[0].GetType());
        Assert.Equal(typeof(FacotryR3), facotryR2s[1].GetType());
        // 2
        List<IFacotryR2> ifacotryR2s = service.GetServices<IFacotryR2>().ToList();
        Assert.Equal(2, ifacotryR2s.Count);
        Assert.Equal(typeof(FacotryR2), ifacotryR2s[0].GetType());
        Assert.Equal(typeof(FacotryR3), ifacotryR2s[1].GetType());

        // 更具key获取
        // 1
        List<ITypeR6<string>> typeR6s_string_1 = service.GetServices<ITypeR6<string>>("1").ToList();
        Assert.Equal(1, typeR6s_string_1.Count);
        Assert.Equal(typeof(TypeR6), typeR6s_string_1[0].GetType());
        // 1
        List<FacotryR2> facotryR2s_1 = service.GetServices<FacotryR2>("1").ToList();
        Assert.Equal(1, facotryR2s_1.Count);
        Assert.Equal(typeof(FacotryR2), facotryR2s_1[0].GetType());
    }

    /// <summary>
    /// 测试属性/字段注入
    /// </summary>
    [Fact(DisplayName = "测试属性/字段注入")]
    public void Project_FieldInject()
    {
        EasyServiceProvider service = GetEasyServiceProvider();

        TypeR7<bool> typeR7 = (TypeR7<bool>)service.GetService<ITypeR7<bool>>();
        Assert.NotNull(typeR7);
        Assert.NotNull(typeR7.TypeR1_1);
        Assert.NotNull(typeR7.TypeR2_1);
        Assert.NotNull(typeR7.TypeR3_1);
        Assert.Null(typeR7.TypeR3_2);
        Assert.NotNull(typeR7.ITypeR4_1);
        Assert.NotNull(typeR7.ITypeR4_2);
        Assert.NotNull(typeR7.TypeR5_1);
        Assert.NotNull(typeR7.TypeR6_1);
        Assert.NotNull(typeR7.TypeR6_2);
        Assert.NotEqual(typeR7.TypeR6_1, typeR7.TypeR6_2);
    }
}
