namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 工厂注册接口
/// <list type="bullet">
///     <item>
///         <term>提示</term>
///         <description>工厂注册不支持开放泛型类型注册，微软不允许我们这样做</description>
///     </item>
/// </list>
/// </summary>
/// <typeparam name="TService">
/// 注册的服务类型
/// <list type="bullet">
///     类型必须是实现的接口/类型本身/基类,否者忽略当前工厂
/// </list>
/// </typeparam>
/// <typeparam name="TServiceLifetime">生命周期</typeparam>
public interface IRegisterFactory<TService, TServiceLifetime> where TService : class where TServiceLifetime : IServiceLifetime 
{
    /// <summary>
    /// 实例对象的创建工厂
    /// <list type="bullet">
    ///     <item>
    ///         <term>优先级</term>
    ///         <description>优先采用显示实现，其次采用隐式实现</description>
    ///     </item>
    ///     <item>
    ///         <term>直接使用基类的工厂注册要求</term>
    ///         <description>
    ///             <br>1.基类已经实现了该接口</br>
    ///             <br>2.当前类定义一个 <see cref="ImplementationFactory(IServiceProvider)"/> 相同签名的方法</br>
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="serviceProvider">服务提供商</param>
    /// <returns></returns>
    object ImplementationFactory(IServiceProvider serviceProvider);

    /// <summary>
    /// 同时使用Key注册服务
    /// <list type="bullet">
    ///     <item>
    ///         <term>要求</term>
    ///         <description>必须显示实现,返回值不为null/Empty</description>
    ///     </item>
    ///     <item>
    ///         <term>实例对象的创建</term>
    ///         <description>使用该Key获取的对象采用你实现的 <see cref="ImplementationFactory(IServiceProvider)"/> 方法创建</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <returns></returns>
#if NET462 || NETSTANDARD2_0
    string ServiceKey();
#else
    string ServiceKey() => string.Empty;
#endif
}
