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
    public static void LoadArg(this ILGenerator iLGenerator!!, int index)
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

    /// <summary>
    /// 推送索引处局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localIndex">局部变量索引</param>
    public static void LoadLocal(this ILGenerator iLGenerator!!, int localIndex)
    {
        switch (localIndex)
        {
            case 0: iLGenerator.Emit(OpCodes.Ldloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldloc_S, localIndex); return;
            default: iLGenerator.Emit(OpCodes.Ldloc, localIndex); return;
        }
    }

    /// <summary>
    /// 推送字段
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="fieldInfo">字段信息</param>
    public static void LoadField(this ILGenerator iLGenerator!!, FieldInfo fieldInfo!!)
    {
        if (fieldInfo.IsStatic) iLGenerator.Emit(OpCodes.Ldsfld, fieldInfo);
        else iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
    }

    /// <summary>
    /// 推送方法指针
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="methodInfo">方法信息</param>
    public static void LoadMethodPointer(this ILGenerator iLGenerator!!, MethodInfo methodInfo!!)
    {
        if (methodInfo.IsVirtual) iLGenerator.Emit(OpCodes.Ldvirtftn, methodInfo);
        else iLGenerator.Emit(OpCodes.Ldftn, methodInfo);
    }

    /// <summary>
    /// 推送该指针出的对象的值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">值类型</param>
    public static void LoadPointerObject(this ILGenerator iLGenerator!!, Type valueType!!) => iLGenerator.Emit(OpCodes.Ldobj, valueType);




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
    /// 推送 <see cref="Single"/>(float32) 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadFloat(this ILGenerator iLGenerator!!, float value) => iLGenerator.Emit(OpCodes.Ldc_R4, value);

    /// <summary>
    /// 推送 <see cref="Double"/>(float64) 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">要加载的数字常量</param>
    public static void LoadDouble(this ILGenerator iLGenerator!!, double value) => iLGenerator.Emit(OpCodes.Ldc_R8, value);
    #endregion

    /// <summary>
    /// 推送 <see cref="string"/> 类型对象
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="value">值</param>
    public static void LoadString(this ILGenerator iLGenerator!!, string value) => iLGenerator.Emit(OpCodes.Ldstr, value);

    /// <summary>
    /// 推送空引用(null)
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void LoadNull(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Ldnull);
    #endregion

    #region 数学运算
    /// <summary>
    /// 将两个值相加,并推送结果
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

    /// <summary>
    /// 将两个值相减,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isOverflowCheck">是否启动溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动开启溢出检查</param>
    public static void MathSub(this ILGenerator iLGenerator!!, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Sub_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Sub_Ovf);
        else iLGenerator.Emit(OpCodes.Sub);
    }

    /// <summary>
    /// 将两个值相乘,并将结果推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isOverflowCheck">是否启动溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动开启溢出检查</param>
    public static void MathMul(this ILGenerator iLGenerator!!, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Mul_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Mul_Ovf);
        else iLGenerator.Emit(OpCodes.Mul);
    }

    /// <summary>
    /// 将两个值相除,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isUnsigned">被除数和除数是否是无符号</param>
    public static void MathDiv(this ILGenerator iLGenerator!!, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Div_Un);
        else iLGenerator.Emit(OpCodes.Div);
    }

    /// <summary>
    /// 将两个值求余,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isUnsigned">两个求余的值是否是无符号</param>
    public static void MathRem(this ILGenerator iLGenerator!!, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Rem_Un);
        else iLGenerator.Emit(OpCodes.Rem);
    }
    #endregion

    #region 计算
    /// <summary>
    /// 计算两个值按位"与",并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseAnd(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.And);

    /// <summary>
    /// 计算两个值按位"或",并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseOr(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Or);

    /// <summary>
    /// 计算两个值按位"异或",并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseXor(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Xor);

    /// <summary>
    /// 对值求反,并将结果推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseNeg(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Neg);

    /// <summary>
    /// 对整数值按位求补,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseNot(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Not);

    /// <summary>
    /// 对整数值左移位,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void BitwiseShiftLeft(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Shl);

    /// <summary>
    /// 对整数值右移位,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isUnsigned">移位值是否是无符号值</param>
    public static void BitwiseShiftRight(this ILGenerator iLGenerator!!, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Shr_Un);
        else iLGenerator.Emit(OpCodes.Shr);
    }
    #endregion

    #region 比较
    /// <summary>
    /// 比较两个值,如果相等推送 (<see cref="int"/>)1,否者推送 (<see cref="int"/>)0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void CompareEqual(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Ceq);

    /// <summary>
    /// 比较两个值,如果第一个值大于第二个值推送 (<see cref="int"/>)1,否者推送 (<see cref="int"/>)0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isUnsigned">是否是无符号的或未经排序的值</param>
    public static void CompareGreater(this ILGenerator iLGenerator!!, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Cgt_Un);
        else iLGenerator.Emit(OpCodes.Cgt);
    }

    /// <summary>
    /// 比较两个值,如果第一个值小于第二个值推送 (<see cref="int"/>)1,否者推送 (<see cref="int"/>)0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="isUnsigned">是否是无符号的或未经排序的值</param>
    public static void CompareLess(this ILGenerator iLGenerator!!, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Clt_Un);
        else iLGenerator.Emit(OpCodes.Clt);
    }
    #endregion

    #region 检查
    /// <summary>
    /// 如果值是有限数,将值推送回到栈上,否者引发<see cref="ArithmeticException"/> 异常
    /// <list type="bullet">
    ///     <item>如果推送的值不是浮点值不执行</item>
    ///     <item>当值为 非数字/+-无穷大 的情况会引发异常</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void CheckFiniteNum(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Ckfinite);
    #endregion

    #region 转换
    /// <summary>
    /// 转换对象为 <paramref name="targetType"/> 类型,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="targetType">目标类型</param>
    public static void ConvertType(this ILGenerator iLGenerator!!, Type targetType!!) => iLGenerator.Emit(OpCodes.Castclass, targetType);

    /// <summary>
    /// 转换值为有符号整数类型,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="basis">执行转换基础
    /// <list type="bullet">
    ///     <item>
    ///         <term><see cref="IntegerType.SByte"/></term>
    ///         <description>将值转换成有符号的 <see cref="SByte"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Byte"/></term>
    ///         <description>将无符号值转换成有符号的 <see cref="SByte"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int16"/></term>
    ///         <description>将值转换成有符号的 <see cref="Int16"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt16"/></term>
    ///         <description>将无符号值转换成有符号的 <see cref="Int16"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int32"/></term>
    ///         <description>将值转换成有符号的 <see cref="Int32"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt32"/></term>
    ///         <description>将无符号值转换成有符号的 <see cref="Int32"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int64"/></term>
    ///         <description>将值转换成有符号的 <see cref="Int64"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt64"/></term>
    ///         <description>将无符号值转换成有符号的 <see cref="Int64"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.NativeInt"/></term>
    ///         <description>将值转换成有符号的 native int 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UNativeInt"/></term>
    ///         <description>将无符号值转换成有符号的 native int 类型的值,一定会执行溢出检查</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="isOverflowCheck">是否检查溢出,如果是无符号类型一定会检查溢出</param>
    public static void ConvertInteger(this ILGenerator iLGenerator!!, IntegerType basis = IntegerType.Int32, bool isOverflowCheck = false)
    {
        switch (basis)
        {
            case IntegerType.SByte: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_I1); else iLGenerator.Emit(OpCodes.Conv_I1); return;
            case IntegerType.Byte: iLGenerator.Emit(OpCodes.Conv_Ovf_I1_Un); return;
            case IntegerType.Int16: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_I2); else iLGenerator.Emit(OpCodes.Conv_I2); return;
            case IntegerType.UInt16: iLGenerator.Emit(OpCodes.Conv_Ovf_I2_Un); return;
            case IntegerType.Int32: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_I4); else iLGenerator.Emit(OpCodes.Conv_I4); return;
            case IntegerType.UInt32: iLGenerator.Emit(OpCodes.Conv_Ovf_I4_Un); return;
            case IntegerType.Int64: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_I8); else iLGenerator.Emit(OpCodes.Conv_I8); return;
            case IntegerType.UInt64: iLGenerator.Emit(OpCodes.Conv_Ovf_I8_Un); return;
            case IntegerType.NativeInt: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_I); else iLGenerator.Emit(OpCodes.Conv_I); return;
            case IntegerType.UNativeInt: iLGenerator.Emit(OpCodes.Conv_Ovf_I_Un); return;
            default: throw new ArgumentException(Strings.InvalidParameter, nameof(basis));
        }
    }

    /// <summary>
    /// 转换值为无符号整数类型,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="basis">执行转换基础
    /// <list type="bullet">
    ///     <item>
    ///         <term><see cref="IntegerType.SByte"/></term>
    ///         <description>将值转换成无符号的 <see cref="Byte"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Byte"/></term>
    ///         <description>将无符号值转换成无符号的 <see cref="Byte"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int16"/></term>
    ///         <description>将值转换成无符号的 <see cref="UInt16"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt16"/></term>
    ///         <description>将无符号值转换成无符号的 <see cref="UInt16"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int32"/></term>
    ///         <description>将值转换成无符号的 <see cref="UInt32"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt32"/></term>
    ///         <description>将无符号值转换成无符号的 <see cref="UInt32"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int64"/></term>
    ///         <description>将值转换成无符号的 <see cref="UInt64"/> 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt64"/></term>
    ///         <description>将无符号值转换成无符号的 <see cref="UInt64"/> 类型的值,一定会执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.NativeInt"/></term>
    ///         <description>将值转换成无符号的 native int 类型的值,根据 <paramref name="isOverflowCheck"/> 值判断是否执行溢出检查</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UNativeInt"/></term>
    ///         <description>将无符号值转换成无符号的 native int 类型的值,一定会执行溢出检查</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="isOverflowCheck">是否检查溢出,如果是无符号类型一定会检查溢出</param>
    public static void ConvertUnsignedInteger(this ILGenerator iLGenerator!!, IntegerType basis = IntegerType.Int32, bool isOverflowCheck = false)
    {
        switch (basis)
        {
            case IntegerType.SByte: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_U1); else iLGenerator.Emit(OpCodes.Conv_U1); return;
            case IntegerType.Byte: iLGenerator.Emit(OpCodes.Conv_Ovf_U1_Un); return;
            case IntegerType.Int16: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_U2); else iLGenerator.Emit(OpCodes.Conv_U2); return;
            case IntegerType.UInt16: iLGenerator.Emit(OpCodes.Conv_Ovf_U2_Un); return;
            case IntegerType.Int32: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_U4); else iLGenerator.Emit(OpCodes.Conv_U4); return;
            case IntegerType.UInt32: iLGenerator.Emit(OpCodes.Conv_Ovf_U4_Un); return;
            case IntegerType.Int64: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_U8); else iLGenerator.Emit(OpCodes.Conv_U8); return;
            case IntegerType.UInt64: iLGenerator.Emit(OpCodes.Conv_Ovf_U8_Un); return;
            case IntegerType.NativeInt: if (isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_Ovf_U); else iLGenerator.Emit(OpCodes.Conv_U); return;
            case IntegerType.UNativeInt: iLGenerator.Emit(OpCodes.Conv_Ovf_U_Un); return;
            default: throw new ArgumentException(Strings.InvalidParameter, nameof(basis));
        }
    }

    /// <summary>
    /// 转换值为浮点数,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="floatType">
    /// <list type="bullet">
    ///     <item><see cref="Single"/> (float32)</item>
    ///     <item><see cref="Double"/> (float64)</item>
    /// </list>
    /// </param>
    public static void ConvertFloat(this ILGenerator iLGenerator!!, FloatType floatType = FloatType.Single)
    {
        switch (floatType)
        {
            case FloatType.Single: iLGenerator.Emit(OpCodes.Conv_R4); break;
            case FloatType.Double: iLGenerator.Emit(OpCodes.Conv_R8); break;
            default: throw new ArgumentException(Strings.InvalidParameter, nameof(floatType));
        }
    }


    /// <summary>
    /// 如果对象是 <paramref name="targetType"/> 类型的实例,推送该类型实例,否者推送null
    /// </summary>
    /// <param name="iLGenerator"></param>
    /// <param name="targetType"></param>
    public static void AS(this ILGenerator iLGenerator!!, Type targetType!!) => iLGenerator.Emit(OpCodes.Isinst, targetType);

    /// <summary>
    /// 转换 <paramref name="valueType"/> 类型的值为引用类型(object类型),并推送结果
    /// <br>执行装箱操作</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">要转换成object类型的值的类型(int/float)</param>
    public static void Box(this ILGenerator iLGenerator!!, Type valueType!!) => iLGenerator.Emit(OpCodes.Box, valueType);

    /// <summary>
    /// 转换object类型为指定类型,根据需求推送指令/值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">值类型</param>
    /// <param name="isPointer">是否推送指针</param>
    public static void UnBox(this ILGenerator iLGenerator!!, Type valueType!!,bool isPointer = false)
    {
        if (isPointer) iLGenerator.Emit(OpCodes.Unbox, valueType);
        else iLGenerator.Emit(OpCodes.Unbox_Any, valueType);
    }
    #endregion


    #region Get
    /// <summary>
    /// 将索引处的元素作为 <paramref name="valueType"/> 类型推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">索引处值的类型</param>
    public static void GetArrayIndexValue(this ILGenerator iLGenerator!!, Type valueType!!) => iLGenerator.Emit(OpCodes.Ldelem, valueType);

    /// <summary>
    /// 将索引处的元素作为整数类型推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="basis">推送类型的根据
    /// <list type="bullet">
    ///     <item>
    ///         <term><see cref="IntegerType.SByte"/></term>
    ///         <description>有符号 <see cref="SByte"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Byte"/></term>
    ///         <description>无符号 <see cref="Byte"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int16"/></term>
    ///         <description>有符号 <see cref="Int16"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt16"/></term>
    ///         <description>无符号 <see cref="UInt16"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int32"/></term>
    ///         <description>有符号 <see cref="Int32"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.UInt32"/></term>
    ///         <description>无符号 <see cref="UInt32"/> 类型的值,作为 <see cref="Int32"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.Int64"/>/<see cref="IntegerType.UInt64"/></term>
    ///         <description><see cref="Int64"/> 类型的值,作为 <see cref="Int64"/> 类型推送</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="IntegerType.NativeInt"/>/<see cref="IntegerType.UNativeInt"/></term>
    ///         <description> native int 类型的值,作为 native int 类型推送</description>
    ///     </item>
    /// </list>
    /// </param>
    public static void GetArrayIndexInteger(this ILGenerator iLGenerator!!, IntegerType basis = IntegerType.Int32)
    {
        OpCode opCode = basis switch
        {
            IntegerType.SByte => OpCodes.Ldelem_I1,
            IntegerType.Byte => OpCodes.Ldelem_U1,
            IntegerType.Int16 => OpCodes.Ldelem_I2,
            IntegerType.UInt16 => OpCodes.Ldelem_U2,
            IntegerType.Int32 => OpCodes.Ldelem_I4,
            IntegerType.UInt32 => OpCodes.Ldelem_U4,
            IntegerType.Int64 or IntegerType.UInt64 => OpCodes.Ldelem_I8,
            IntegerType.NativeInt or IntegerType.UNativeInt => OpCodes.Ldelem_I,
            _ => throw new ArgumentException(Strings.InvalidParameter, nameof(basis))
        };
        iLGenerator.Emit(opCode);
    }

    /// <summary>
    /// 将索引处的元素作为浮点类型推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="basis">推送类型的根据</param>
    public static void GetArrayIndexFloat(this ILGenerator iLGenerator!!, FloatType basis = FloatType.Single)
    {
        OpCode opCode = basis switch
        {
            FloatType.Single => OpCodes.Ldelem_R4,
            FloatType.Double => OpCodes.Ldelem_R8,
            _ => throw new ArgumentException(Strings.InvalidParameter, nameof(basis))
        };
        iLGenerator.Emit(opCode);
    }

    /// <summary>
    /// 将索引处的对象作为object类型推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void GetArrayIndexObject(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Ldelem_Ref);

    /// <summary>
    /// 推送数组的长度
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void GetArrayLength(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Ldlen);
    #endregion


    #region Debug
    /// <summary>
    /// 通知调试器以碰撞一个断点
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void DebugBreakPoint(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Break);
    #endregion



    /// <summary>
    /// 复制值,并推送副本
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void Copy(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Dup);





    /// <summary>
    /// 调用方法
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="methodInfo">方法信息</param>
    /// <param name="optionalParameterTypes">可选参数类型,如果存在该值应该是调用约定为 <see cref=" CallingConventions.VarArgs"/> 的方法</param>
    public static void Call(this ILGenerator iLGenerator!!, MethodInfo methodInfo!!, params Type[] optionalParameterTypes)
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

    /// <summary>
    /// 弹出当前值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void Pop(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Pop);

    /// <summary>
    /// 推送类型的大小,单位字节(byte)
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="type">类型</param>
    public static void SizeOf(this ILGenerator iLGenerator!!, Type type!!) => iLGenerator.Emit(OpCodes.Sizeof, type);

    /// <summary>
    /// 引发异常
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void Throw(this ILGenerator iLGenerator!!) => iLGenerator.Emit(OpCodes.Throw);










    #region 定义
    /// <summary>
    /// 创建一个数组,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="type">数组类型</param>
    public static void NewArray(this ILGenerator iLGenerator!!, Type type!!) => iLGenerator.Emit(OpCodes.Newarr, type);

    /// <summary>
    /// 创建一个对象,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="constructorInfo">创建对象的构造函数</param>
    public static void NewObject(this ILGenerator iLGenerator!!, ConstructorInfo constructorInfo!!) => iLGenerator.Emit(OpCodes.Newobj, constructorInfo);
    #endregion

    #region 赋值
    /// <summary>
    /// 将当前堆栈的值设置到字段
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="fieldInfo">字段信息</param>
    public static void SetField(this ILGenerator iLGenerator!!, FieldInfo fieldInfo!!)
    {
        if (fieldInfo.IsStatic) iLGenerator.Emit(OpCodes.Stsfld, fieldInfo);
        else iLGenerator.Emit(OpCodes.Stfld, fieldInfo);
    }

    /// <summary>
    /// 将当前堆栈的值设置到局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localBuilder">局部变量</param>
    public static void SetLocal(this ILGenerator iLGenerator!!, LocalBuilder localBuilder!!)
    {
        switch (localBuilder.LocalIndex)
        {
            case 0: iLGenerator.Emit(OpCodes.Stloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Stloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Stloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Stloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Stloc_S, localBuilder); return;
            default: iLGenerator.Emit(OpCodes.Stloc, localBuilder); return;
        }
    }

    /// <summary>
    /// 将当前堆栈的值设置到索引处局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="localIndex">局部变量索引</param>
    public static void SetLocal(this ILGenerator iLGenerator!!,int localIndex)
    {
        switch (localIndex)
        {
            case 0: iLGenerator.Emit(OpCodes.Stloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Stloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Stloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Stloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Stloc_S, localIndex); return;
            default: iLGenerator.Emit(OpCodes.Stloc, localIndex); return;
        }
    }

    /// <summary>
    /// 设置数组值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    /// <param name="valueType">值类型</param>
    public static void SetArrayValue(this ILGenerator iLGenerator!!, Type valueType!!)
    {
        // native int 类型无法判断这里不会使用 OpCodes.Stelem_I 赋值
        if (valueType == typeof(byte)) iLGenerator.Emit(OpCodes.Stelem_I1);// int8
        else if (valueType == typeof(Int16)) iLGenerator.Emit(OpCodes.Stelem_I2);
        else if (valueType == typeof(Int32)) iLGenerator.Emit(OpCodes.Stelem_I4);
        else if (valueType == typeof(Int64)) iLGenerator.Emit(OpCodes.Stelem_I8);// long
        else if (valueType == typeof(Single)) iLGenerator.Emit(OpCodes.Stelem_R4);// float32
        else if (valueType == typeof(Double)) iLGenerator.Emit(OpCodes.Stelem_R8);// float64
        else iLGenerator.Emit(OpCodes.Stelem, valueType);
    }

    #endregion







    /// <summary>
    /// 返回值
    /// <br>将返回值(如果存在)从被调用者推送到调用者</br>
    /// </summary>
    /// <param name="iLGenerator"></param>

    public static void Return(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ret);



    #region 暂时不做拓展（不太清楚用法的）
    /* 待验证问题
     * 1.个人感觉好像 IntPtr 就是这里提到的 native int(本地int)
     */

    /* https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.emit.opcodes?view=net-6.0#fields
     * Arglist      返回指向当前方法的参数列表的非托管指针
     *
     * ** 转移到目标指令
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
     * Leave        退出受保护的代码区域，无条件将控制转移到特定目标指令
     * 
     * Switch       实现跳转表。
     *
     *
     * ** 调用方法
     * Calli        通过调用约定描述的参数调用在计算堆栈上指示的方法（作为指向入口点的指针）
     *
     * Callvirt     对对象调用后期绑定方法，并且将返回值推送到计算堆栈上。【应该是调用静态方法】
     *
     *
     * ** 约束
     * Constrained  约束要对其进行虚方法调用的类型。
     *
     *
     * ** 复制
     * Cpblk        将指定数目的字节从源地址复制到目标地址。【应该是复制一部分字符串】
     *
     * Cpobj        将位于对象地址的值类型（类型 & 或native  int）复制到目标对象的地址（类型 & 或native  int）。
     *
     *
     * ** 异常
     * Endfilter    将控制从异常的 filter 子句转移回公共语言结构 (CLI) 异常处理程序。
     *
     * Endfinally   将控制从异常块的 fault 或 finally 子句转移回公共语言结构 (CLI) 异常处理程序。
     *
     *
     * ** 初始化
     * Initblk      将位于特定地址的内存的指定块初始化为给定大小和初始值。
     *
     * Initobj      将位于指定地址的值类型的每个字段初始化为空引用或适当的基元类型的 0。
     *
     *
     * ** 跳转
     * Jmp          退出当前方法并跳至指定方法。
     *
     * ** 加载/推送
     * Ldarga       将参数地址加载到计算堆栈上。
     *
     * Ldelema      将位于指定数组索引的数组元素的地址作为 & 类型（托管指针）加载到计算堆栈的顶部。
     * 
     * Ldflda       查找对象中其引用当前位于计算堆栈的字段的地址。
     * 
     * Ldftn        将指向实现特定方法的本机代码的非托管指针（native int 类型）推送到计算堆栈上。
     * 
     * Ldind_I      将 native int 类型的值作为 native int 间接加载到计算堆栈上。
     * 
     * Ldloca       将位于特定索引处的局部变量的地址加载到计算堆栈上。
     * 
     * Ldsflda      将静态字段的地址推送到计算堆栈上。
     * 
     * Ldtoken      将元数据标记转换为其运行时表示形式，并将其推送到计算堆栈上。
     * 
     * 
     * ** 分配空间
     * Localloc     从本地动态内存池分配特定数目的字节并将第一个分配的字节的地址（瞬态指针，* 类型）推送到计算堆栈上
     * 
     * 
     * ** 转换
     * Mkrefany     将对特定类型实例的类型化引用推送到计算堆栈上。
     * 
     * 
     * ** 没有任何操作
     * Nop          如果修补操作码，则填充空间。 尽管可能消耗处理周期，但未执行任何有意义的操作
     * 
     * ** 只读
     * Readonly     指定后面的数组地址操作在运行时不执行类型检查，并且返回可变性受限的托管指针。
     * 
     * 
     * ** 获取信息
     * Refanytype   检索嵌入在类型化引用内的类型标记。【应该是获取值的类型】
     * 
     * Refanyval    检索嵌入在类型化引用内的地址（& 类型）。
     * 
     * ** 异常
     * Rethrow      再次引发当前异常
     * 
     * 
     * ** 放置参数
     * Starg        将位于计算堆栈顶部的值存储到位于指定索引的自变量槽中。
     * 
     * 
     * ** 设置值
     * Stelem_I     用计算堆栈上的 native int 值替换给定索引处的数组元素。
     * 
     * Stelem_Ref   用计算堆栈上的对象 ref 值（O 类型）替换给定索引处的数组元素。
     * 
     * Stind_I      在所提供的地址存储 native int 类型的值。
     * 
     * Stobj        将指定类型的值从计算堆栈复制到所提供的内存地址中。
     * 
     * 
     * ** 调用方法移除堆栈帧
     * Tailcall     执行后缀的方法调用指令，以便在执行实际调用指令前移除当前方法的堆栈帧。
     * 
     * 
     * Unaligned    指示当前位于计算堆栈上的地址可能没有与紧接的 ldind、stind、ldfld、stfld、ldobj、stobj、initblk 或 cpblk 指令的自然大小对齐。
     * 
     * 
     * Volatile     指定当前位于计算堆栈顶部的地址可以是易失的，并且读取该位置的结果不能被缓存，或者对该地址的多个存储区不能被取消。
     */
    #endregion
}

/// <summary>
/// 整型类型
/// </summary>
public enum IntegerType
{
    /// <summary>
    /// <see cref="System.SByte"/>(有符号byte) 类型
    /// </summary>
    SByte,
    /// <summary>
    /// <see cref="System.Byte"/>(无符号byte) 类型
    /// </summary>
    Byte,
    /// <summary>
    /// <see cref="System.Int16"/>(有符号short) 类型
    /// </summary>
    Int16,
    /// <summary>
    /// <see cref="System.UInt16"/>(无符号short) 类型
    /// </summary>
    UInt16,
    /// <summary>
    /// <see cref="System.Int32"/>(有符号int) 类型
    /// </summary>
    Int32,
    /// <summary>
    /// <see cref="System.UInt32"/>(无符号int) 类型
    /// </summary>
    UInt32,
    /// <summary>
    /// <see cref="System.Int64"/>(有符号long) 类型
    /// </summary>
    Int64,
    /// <summary>
    /// <see cref="System.UInt64"/>(无符号long) 类型
    /// </summary>
    UInt64,
    /// <summary>
    /// 本机int类型
    /// </summary>
    NativeInt,
    /// <summary>
    /// 无符号本机int类型
    /// </summary>
    UNativeInt
}

/// <summary>
/// 浮点类型
/// </summary>
public enum FloatType
{
    /// <summary>
    /// <see cref="System.Single"/>(单精度浮点) 类型
    /// </summary>
    Single,
    /// <summary>
    /// <see cref="System.Double"/>(双精度浮点) 类型
    /// </summary>
    Double
}
