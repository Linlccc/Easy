namespace System.Reflection.Emit;

/// <summary>
/// <see cref="ILGenerator"/>(Microsoft 中间语言指令) 组合拓展
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// 设置局部数组变量之前
    /// <br>示例:</br>
    /// <code>
    /// iLGenerator.SetLocalArrayValueBefore(localBuilder,0);
    /// // 将要设置的值加载到堆栈上
    /// iLGenerator.SetLocalArrayValueAfter(type[设置值的类型]);
    /// </code>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>
    /// <param name="index">要修改值的索引</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetLocalArrayValueBefore(this ILGenerator iLGenerator, LocalBuilder localBuilder, int index)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        _ = localBuilder ?? throw new ArgumentNullException(nameof(localBuilder));

        iLGenerator.LoadLocal(localBuilder);
        iLGenerator.LoadInt(index);
    }

    /// <summary>
    /// 设置局部数组变量之后
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="type">设置的变量的类型</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetLocalArrayValueAfter(this ILGenerator iLGenerator, Type type)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        iLGenerator.Emit(OpCodes.Stelem, type);
    }
}
