namespace System.Reflection.Emit;

/// <summary>
/// <see cref="ILGenerator"/>(Microsoft 中间语言指令) 拓展
/// </summary>
public static partial class ILGeneratorExtensions
{
    #region 加载\推送(将指定的值推送到计算堆栈)
    /// <summary>
    /// 推送参数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="index">参数索引,0是当前实例</param>
    public static void LoadArg(this ILGenerator iLGenerator!!, uint index)
    {
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
    /// 推送局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>
    public static void LoadLocal(this ILGenerator iLGenerator!!, LocalBuilder localBuilder!!)
    {
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


    #region 推送数字
    /// <summary>
    /// 推送 <see cref="Int32"/>/<see cref="Int16"/>/<see cref="byte"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadInt(this ILGenerator iLGenerator!!, int value)
    {
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
    /// 推送 <see cref="Int64"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadInt64(this ILGenerator iLGenerator!!, long value) => iLGenerator.Emit(OpCodes.Ldc_I8, value);

    /// <summary>
    /// 推送 <see cref="Single"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadFloat(this ILGenerator iLGenerator!!, float value) => iLGenerator.Emit(OpCodes.Ldc_R4, value);

    /// <summary>
    /// 推送 <see cref="Double"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadDouble(this ILGenerator iLGenerator!!, double value) => iLGenerator.Emit(OpCodes.Ldc_R8, value);
    #endregion
    #endregion

    #region 数学运算
    /// <summary>
    /// 将两个值相加并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isOverflowCheck">是否启动溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动开启溢出检查</param>
    public static void MathAdd(this ILGenerator iLGenerator!!, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Add_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Add_Ovf);
        else iLGenerator.Emit(OpCodes.Add);
    }
    #endregion

    #region 计算
    /// <summary>
    /// 计算两个值按位"与",并推送结果
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void BitwiseAnd(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.And);
    #endregion


    #region Debug
    /// <summary>
    /// 通知调试器以碰撞一个断点
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void DebugBreakPoint(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Break);
    #endregion

    /// <summary>
    /// 转换值类型为引用类型(object类型),并推送结果
    /// <br>执行装箱操作</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">要转换成object类型的值的类型(int/float)</param>
    public static void Box(this ILGenerator iLGenerator!!, Type valueType!!) => iLGenerator.Emit(OpCodes.Box, valueType);

    /// <summary>
    /// 调用方法
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="methodInfo">方法信息</param>
    /// <param name="optionalParameterTypes">可选参数类型,如果存在该值应该是调用约定为 <see cref=" CallingConventions.VarArgs"/> 的方法</param>
    public static void Call(this ILGenerator iLGenerator!!, MethodInfo methodInfo!!,params Type[] optionalParameterTypes)
    {
        if (optionalParameterTypes.IsNullOrEmpty()) iLGenerator.Emit(OpCodes.Call, methodInfo);
        else iLGenerator.EmitCall(OpCodes.Call, methodInfo, optionalParameterTypes);
    }

    /// <summary>
    /// 调用构造函数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="constructorInfo">构造函数信息</param>
    public static void Call(this ILGenerator iLGenerator!!, ConstructorInfo constructorInfo!!) => iLGenerator.Emit(OpCodes.Call, constructorInfo);








    #region 定义
    /// <summary>
    /// 定义一个数组
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="type">数组类型</param>
    public static void DefineArray(this ILGenerator iLGenerator!!, Type type!!) => iLGenerator.Emit(OpCodes.Newarr, type);
    #endregion

    #region 赋值
    /// <summary>
    /// 将当前堆栈的值设置到字段
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="fieldInfo">字段信息</param>

    public static void SetField(this ILGenerator iLGenerator!!, FieldInfo fieldInfo!!) => iLGenerator.Emit(OpCodes.Stfld, fieldInfo);

    /// <summary>
    /// 将当前堆栈的值设置到局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>

    public static void SetLocal(this ILGenerator iLGenerator, LocalBuilder localBuilder) => iLGenerator.Emit(OpCodes.Stloc, localBuilder);

    #endregion







    /// <summary>
    /// 返回值
    /// <br>将返回值(如果存在)从被调用者推送到调用者</br>
    /// </summary>
    /// <param name="iLGenerator"></param>

    public static void Return(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ret);



    #region 暂时不做拓展（不太清楚用法的）
    /* https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.emit.opcodes?view=net-6.0#fields
     * Arglist      返回指向当前方法的参数列表的非托管指针
     * 
     * 
     * Beq          如果两个值相等，则将控制转移到目标指令。
     *  
     * Bge          如果第一个值大于或等于第二个值，则将控制转移到目标指令。
     *  
     * Bgt          如果第一个值大于第二个值，则将控制转移到目标指令。
     * 
     * Ble          如果第一个值小于或等于第二个值，则将控制转移到目标指令。
     * 
     * Blt          如果第一个值小于第二个值，则将控制转移到目标指令。
     * 
     * Bne_Un       当两个无符号整数值或未经排序的浮点值不相等时，将控制转移到目标指令。
     * 
     * Br           无条件地将控制转移到目标指令。
     * 
     * Brfalse      如果 value 为 false、空引用（Visual Basic 中的 Nothing）或零，则将控制转移到目标指令。
     * 
     * Brtrue       如果 value 为 true、非空或非零，则将控制转移到目标指令。
     * 
     * 
     * Calli        下一个
     */
    #endregion
}
