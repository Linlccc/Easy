namespace Easy.Extensions.DependencyInjection.Abstractions;

/// <summary>
/// 自动注入
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class InjectAttribute : Attribute
{
    public InjectAttribute() { }
    public InjectAttribute(string? key) => Key = key;

    /// <summary>
    /// 要求
    /// <br>如果没有值会抛出异常</br>
    /// </summary>
    public bool Require { get; set; }

    /// <summary>
    /// 是否使用key注入服务
    /// </summary>
    public bool IsUseKey { get; private set; }

    private string? _key;
    /// <summary>
    /// 服务key
    /// <br>获取服务时，会通过key获取</br>
    /// </summary>
    public string? Key {
        get => _key;
        set
        {
            _key = value;
            IsUseKey = true;
        }
    }
}

