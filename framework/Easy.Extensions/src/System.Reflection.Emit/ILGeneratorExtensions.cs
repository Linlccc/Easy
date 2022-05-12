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

    /// <summary>
    /// 调用方法
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="methodInfo">方法信息</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Call(this ILGenerator iLGenerator, MethodInfo methodInfo)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        iLGenerator.Emit(OpCodes.Call, methodInfo);
        // TODO:这个好像是调用静态方法后面看一下
        // iLGenerator.Emit(OpCodes.Callvirt, methodInfo);
    }

    /// <summary>
    /// 调用构造函数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="constructorInfo">构造函数信息</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Call(this ILGenerator iLGenerator, ConstructorInfo constructorInfo)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        iLGenerator.Emit(OpCodes.Call, constructorInfo);
    }

    /// <summary>
    /// 将当前堆栈的值设置到字段
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="fieldInfo">字段信息</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetField(this ILGenerator iLGenerator, FieldInfo fieldInfo)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        iLGenerator.Emit(OpCodes.Stfld, fieldInfo);
    }

    /// <summary>
    /// 返回值
    /// <br>将返回值(如果存在)从被调用者推送到调用者</br>
    /// </summary>
    /// <param name="iLGenerator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Return(this ILGenerator iLGenerator)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        iLGenerator.Emit(OpCodes.Ret);
    }
}
