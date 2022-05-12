namespace System.Reflection.Emit;

/// <summary>
/// <see cref="ILGenerator"/>(Microsoft 中间语言指令) 拓展
/// </summary>
public static class ILGeneratorExtensions
{
    /// <summary>
    /// 加载参数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="index">参数索引,0是当前实例</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void LoadArg(this ILGenerator iLGenerator, uint index)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        switch (index)
        {
            case 0:
                iLGenerator.Emit(OpCodes.Ldarg_0);
                return;
            case 1:
                iLGenerator.Emit(OpCodes.Ldarg_1);
                break;
            case 2:
                iLGenerator.Emit(OpCodes.Ldarg_2);
                break;
            case 3:
                iLGenerator.Emit(OpCodes.Ldarg_3);
                break;
            case <= byte.MaxValue:
                iLGenerator.Emit(OpCodes.Ldarg_S, index);
                break;
            default:
                iLGenerator.Emit(OpCodes.Ldarg, index);
                break;
        }
    }
}
