using Easy.Extensions.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Easy.Extensions.DependencyInjection.Test.Models;

#region 类型注册
#region 1
/// <summary>
/// 将自己为服务类型注册
/// </summary>
[Register]
public class TypeR1 { }
#endregion

#region 2
/// <summary>
/// 将实现的接口作为服务类型注册
/// </summary>
[Register]
public class TypeR2 : ITypeR2 { }

public interface ITypeR2 { }
#endregion

#region 3
/// <summary>
/// 将所有（直接实现，间接实现）接口作为服务类型注册
/// </summary>
[Register]
public class TypeR3 : TypeR2, ITypeR3 { }

public interface ITypeR3 { }
#endregion

#region 4
/// <summary>
/// 指定注册的服务类型
/// 该类实现了 ITypeR2、ITypeR3、ITypeR4 接口，但是只会以 TypeR3、ITypeR4 作为服务类型注册
/// </summary>
[Register(typeof(TypeR3), typeof(ITypeR4))]
public class TypeR4 : TypeR3, ITypeR4 { }

public interface ITypeR4 { }
#endregion

#region 5
/// <summary>
/// 只以自己作为服务类型注册
/// </summary>
[Register(typeof(TypeR5))]
public class TypeR5 : TypeR4 { }
#endregion

#region 6
/// <summary>
/// 将实现的接口作为服务类型注册，同时使用key注册服务
/// 只会使用 ITypeR6<string> 注册，IDisposable 会被自动排除
/// </summary>
[Register(ServiceKey = "1")]
public class TypeR6 : ITypeR6<string>, IDisposable
{
    public void Dispose() { }
}

public interface ITypeR6<T> { }
#endregion

#region 7
/// <summary>
/// 将泛型类型作为服务类型注册
/// </summary>
/// <typeparam name="T"></typeparam>
[Register]
public class TypeR7<T> : ITypeR7<T>
{
    #region 字段注入
    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public readonly TypeR1 TypeR1_1;

    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public readonly ITypeR2 TypeR2_1;

    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public ITypeR3 TypeR3_1;

    /// <summary>
    /// null
    /// </summary>
    [Inject("1")]
    public ITypeR3 TypeR3_2;
    #endregion
    #region 属性注入
    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public TypeR3 ITypeR4_1 { get; set; }

    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public ITypeR4 ITypeR4_2 { get; set; }

    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public TypeR5 TypeR5_1 { get; set; }

    /// <summary>
    /// not null
    /// </summary>
    [Inject]
    public ITypeR6<string> TypeR6_1 { get; set; }

    /// <summary>
    /// not null,not Eqaul TypeR6_1
    /// </summary>
    [Inject("1")]
    public ITypeR6<string> TypeR6_2 { get; set; }
    #endregion
}

public interface ITypeR7<T> { }
#endregion

#region 8
/// <summary>
/// 手动将泛型类型作为服务类型注册
/// </summary>
[Register(typeof(ITypeR8<,>))]
public class TypeR8<T1, T2> : ITypeR8<T1, T2> { }

public interface ITypeR8<T1, T2> { }
#endregion
#endregion

#region 工厂注册
#region 1
/// <summary>
/// 使用自己作为服务类型注册
/// 隐式实现实例工厂
/// </summary>
public class FacotryR1 : IRegisterFactory<FacotryR1, ILifetimeScoped>
{
    public object ImplementationFactory(IServiceProvider serviceProvider)
    {
        return new FacotryR1();
    }

#if NET462
    public string ServiceKey() => string.Empty;
#endif
}
#endregion

#region 2
/// <summary>
/// 使用 IFacotryR2 接口注册，显示实现工厂
/// 使用 FacotryR2 类型注册，隐式实现工厂，同时使用key注册
/// </summary>
public class FacotryR2 : IFacotryR2, IRegisterFactory<IFacotryR2, ILifetimeScoped>, IRegisterFactory<FacotryR2, ILifetimeScoped>
{
    public object ImplementationFactory(IServiceProvider serviceProvider)
    {
        var v1 = serviceProvider.GetService<IServiceProvider>();
        return new FacotryR2();
    }

    object IRegisterFactory<IFacotryR2, ILifetimeScoped>.ImplementationFactory(IServiceProvider serviceProvider)
    {
        return new FacotryR2();
    }

    string IRegisterFactory<FacotryR2, ILifetimeScoped>.ServiceKey() => "1";
#if NET462
    public string ServiceKey() => string.Empty;
#endif
}

public interface IFacotryR2 { }
#endregion

#region 3
/// <summary>
/// 自动使用父类的 IFacotryR2、FacotryR2 注册
/// 只需要实现一个 <see cref="ImplementationFactory(IServiceProvider)"/> 相同签名的方法即可
/// </summary>
public class FacotryR3 : FacotryR2
{
    new object ImplementationFactory(IServiceProvider serviceProvider)
    {
        return new FacotryR3();
    }
}
#endregion
#endregion
