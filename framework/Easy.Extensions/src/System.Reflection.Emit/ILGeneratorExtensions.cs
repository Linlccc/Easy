namespace System.Reflection.Emit;

/// <summary>
/// <see cref="ILGenerator"/>(Microsoft 中间语言指令) 拓展
/// </summary>
public static class ILGeneratorExtensions
{
    #region 加载\推送(将指定的东西推送到堆栈上)
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
            case 0: iLGenerator.Emit(OpCodes.Ldarg_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldarg_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldarg_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldarg_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldarg_S, index); return;
            default: iLGenerator.Emit(OpCodes.Ldarg, index); return;
        }
    }

    /// <summary>
    /// 加载局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void LoadLocal(this ILGenerator iLGenerator, LocalBuilder localBuilder)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        _ = localBuilder ?? throw new ArgumentNullException(nameof(localBuilder));

        switch (localBuilder.LocalIndex)
        {
            case 0: iLGenerator.Emit(OpCodes.Ldloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldloc_S, localBuilder); return;
            default: iLGenerator.Emit(OpCodes.Ldloc, localBuilder); return;
        }
    }


    #region 加载数字
    /// <summary>
    /// 加载数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadInt(this ILGenerator iLGenerator, int value)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));

        switch (value)
        {
            case -1: iLGenerator.Emit(OpCodes.Ldc_I4_M1); return;
            case 0: iLGenerator.Emit(OpCodes.Ldc_I4_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldc_I4_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldc_I4_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldc_I4_3); return;
            case 4: iLGenerator.Emit(OpCodes.Ldc_I4_4); return;
            case 5: iLGenerator.Emit(OpCodes.Ldc_I4_5); return;
            case 6: iLGenerator.Emit(OpCodes.Ldc_I4_6); return;
            case 7: iLGenerator.Emit(OpCodes.Ldc_I4_7); return;
            case 8: iLGenerator.Emit(OpCodes.Ldc_I4_8); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldc_I4_S, value); return;
            default: iLGenerator.Emit(OpCodes.Ldc_I4, value); return;
        }
    }

    /// <summary>
    /// 加载 <see cref="Int64"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void LoadInt64(this ILGenerator iLGenerator, long value)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        iLGenerator.Emit(OpCodes.Ldc_I8, value);
    }

    /// <summary>
    /// 加载 <see cref="Single"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void LoadFloat(this ILGenerator iLGenerator, float value)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        iLGenerator.Emit(OpCodes.Ldc_R4, value);
    }

    /// <summary>
    /// 加载 <see cref="Double"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void LoadDouble(this ILGenerator iLGenerator, double value)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        iLGenerator.Emit(OpCodes.Ldc_R8, value);
    }
    #endregion
    #endregion



    #region 定义
    /// <summary>
    /// 定义一个数组
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="type">数组类型</param>
    public static void DefineArray(this ILGenerator iLGenerator, Type type)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        iLGenerator.Emit(OpCodes.Newarr, type);
    }
    #endregion

    #region 赋值
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
    /// 将当前堆栈的值设置到局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetLocal(this ILGenerator iLGenerator, LocalBuilder localBuilder)
    {
        _ = iLGenerator ?? throw new ArgumentNullException(nameof(iLGenerator));
        _ = localBuilder ?? throw new ArgumentNullException(nameof(localBuilder));

        iLGenerator.Emit(OpCodes.Stloc, localBuilder);
    }

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

    #endregion



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
