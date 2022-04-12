namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 自动注入
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class InjectAttribute : Attribute
{
    public InjectAttribute() { }

    /// <summary>
    /// 要求
    /// <br>如果没有值会抛出异常</br>
    /// </summary>
    public bool Require { get; set; }

    /// <summary>
    /// 服务key
    /// <br>获取服务时，会通过key获取</br>
    /// </summary>
    public string? Key { get; set; }
}

