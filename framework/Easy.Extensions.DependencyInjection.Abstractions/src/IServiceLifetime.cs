using Microsoft.Extensions.DependencyInjection;

namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 生命周期接口
/// </summary>
public interface IServiceLifetime { }

/// <summary>
/// 单例
/// </summary>
public interface ILifetimeSingleton : IServiceLifetime { }
/// <summary>
/// 范围
/// </summary>
public interface ILifetimeScoped : IServiceLifetime { }
/// <summary>
/// 瞬时
/// </summary>
public interface ILifetimeTransient : IServiceLifetime { }

public static class ServiceLifetimeExtension
{
    /// <summary>
    /// 获取生命周期
    /// </summary>
    /// <param name="type">生命周期接口类型</param>
    /// <returns>生命周期</returns>
    /// <exception cref="ArgumentException">无效的注册生命周期</exception>
    public static ServiceLifetime GetLifetime(Type type)
    {
        if (type == typeof(ILifetimeSingleton)) return ServiceLifetime.Singleton;
        else if (type == typeof(ILifetimeScoped)) return ServiceLifetime.Scoped;
        else if (type == typeof(ILifetimeTransient)) return ServiceLifetime.Transient;
        throw new ArgumentException("无效的注册生命周期");
    }
}

