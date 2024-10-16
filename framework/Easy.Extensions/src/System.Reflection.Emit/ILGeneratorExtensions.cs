namespace System.Reflection.Emit;

/* ** 概念
* 1. Label:     (标签) 类似于代码中的 goto
* 2. 地址:      (address) 数据值存放的地址(获取p变量的地址 &p),一串数字
* 3. 指针:      (pointer)
*  3.1. 指针类型,是一种数据类型 比如:int*(int指针类型)
*  3.2. 指针变量,是一个变量,指针变量的值是一个地址,指针变量也有自己的地址
* 4. nativa int : 表示一个与平台相关的整数类型
*  4.1. 32位系统上,是4字节。64位系统上,是8字节
*  4.2. 主要用于与本机代码交互或进行低级操作，比如 P/Invoke（平台调用）
*  4.3. 可以用于表示指针或句柄
*  4.4. 在 C# 中，可以使用 IntPtr/nint 类型来表示 native int
* 
* see https://learn.microsoft.com/zh-cn/dotnet/api/system.reflection.emit
*/

/// <summary>
/// <see cref="ILGenerator"/>(Microsoft 中间语言指令生成器) 拓展
/// </summary>
public static partial class ILGeneratorExtensions
{
    #region 加载/推送(将指定的值推送到计算堆栈)
    #region 变量
    /// <summary>
    /// 推送参数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="index">
    /// <list type="bullet">
    ///     <item>参数索引 </item>
    ///     <item>
    ///         <term>实例方法</term>
    ///         <description>0是当前实例,1是第一个参数</description><br/>
    ///         
    ///         <term>静态方法</term>
    ///         <description>0是第一个参数</description>
    ///     </item>
    /// </list>
    /// </param>
    public static void LoadArg(this ILGenerator iLGenerator, ushort index = 0)
    {
        switch (index)
        {
            case 0: iLGenerator.Emit(OpCodes.Ldarg_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldarg_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldarg_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldarg_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldarg_S, (byte)index); return;
            default: iLGenerator.Emit(OpCodes.Ldarg, index); return;
        }
    }

    /// <summary>
    /// 推送可变参数(__arglist)
    /// <br>只能在有 <see cref="CallingConventions.VarArgs"/> 标记的方法中使用</br>
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void LoadVarArgs(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Arglist);

    /// <summary>
    /// 推送局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="localBuilder">局部变量构建器</param>
    public static void LoadLocal(this ILGenerator iLGenerator, LocalBuilder localBuilder)
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
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="index">局部变量索引</param>
    public static void LoadLocal(this ILGenerator iLGenerator, ushort index)
    {
        switch (index)
        {
            case 0: iLGenerator.Emit(OpCodes.Ldloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Ldloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Ldloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Ldloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldloc_S, (byte)index); return;
            default: iLGenerator.Emit(OpCodes.Ldloc, index); return;
        }
    }

    /// <summary>
    /// 推送字段
    /// <list type="bullet">
    ///     <item>1.推送字段的对象引用(或指针)【静态字段忽略】</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="fieldInfo">字段信息</param>
    public static void LoadField(this ILGenerator iLGenerator, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsStatic) iLGenerator.Emit(OpCodes.Ldsfld, fieldInfo);
        else iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
    }

    /// <summary>
    /// 推送堆栈顶部值的长度
    /// <list type="bullet">
    ///     <item>1.推送 <see cref="Array"/>\<see cref="string"/> 等有"Length"属性的值</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void LoadLength(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ldlen);
    #endregion

    #region 数组
    /// <summary>
    /// 推送 <paramref name="type"/> 类型数组中索引处的值
    /// <list type="bullet">
    ///     <item>1.推送数组</item>
    ///     <item>2.推送索引</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">数组类型,如果是引用类型可以忽略该参数</param>
    public static void LoadArrayIndexValue(this ILGenerator iLGenerator, Type? type = null)
    {
        // 整数
        if (type == typeof(IntPtr)) iLGenerator.Emit(OpCodes.Ldelem_I);

        else if (type == typeof(sbyte)) iLGenerator.Emit(OpCodes.Ldelem_I1);
        else if (type == typeof(byte)) iLGenerator.Emit(OpCodes.Ldelem_U1);

        else if (type == typeof(short)) iLGenerator.Emit(OpCodes.Ldelem_I2);
        else if (type == typeof(ushort)) iLGenerator.Emit(OpCodes.Ldelem_U2);

        else if (type == typeof(int)) iLGenerator.Emit(OpCodes.Ldelem_I4);
        else if (type == typeof(uint)) iLGenerator.Emit(OpCodes.Ldelem_U4);

        else if (type == typeof(long)) iLGenerator.Emit(OpCodes.Ldelem_I8);

        // 浮点数
        else if (type == typeof(float)) iLGenerator.Emit(OpCodes.Ldelem_R4);
        else if (type == typeof(double)) iLGenerator.Emit(OpCodes.Ldelem_R8);

        // 引用类型
        else if (type is null) iLGenerator.Emit(OpCodes.Ldelem_Ref);
        else iLGenerator.Emit(OpCodes.Ldelem, type);

    }

    /// <summary>
    /// 推送 <paramref name="arrayType"/> 类型数组中索引处值的地址
    /// <list type="bullet">
    ///     <item>1.推送数组</item>
    ///     <item>2.推送索引</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="arrayType">数组类型</param>
    public static void LoadArrayIndexAddr(this ILGenerator iLGenerator, Type arrayType) => iLGenerator.Emit(OpCodes.Ldelema, arrayType);
    #endregion

    #region 变量To地址
    /// <summary>
    /// 推送索引处参数地址
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="index">参数索引</param>
    public static void LoadArgAddr(this ILGenerator iLGenerator, ushort index)
    {
        switch (index)
        {
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldarga_S, (byte)index); return;
            default: iLGenerator.Emit(OpCodes.Ldarga, index); return;
        }
    }

    /// <summary>
    /// 推送索引处局部变量地址
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="index">局部变量索引</param>
    public static void LoadLocalAddr(this ILGenerator iLGenerator, ushort index)
    {
        switch (index)
        {
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Ldloca_S, (byte)index); return;
            default: iLGenerator.Emit(OpCodes.Ldloca, index); return;
        }
    }

    /// <summary>
    /// 推送字段地址
    /// <list type="bullet">
    ///     <item>1.推送字段的对象引用(或指针)【静态字段忽略】</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="fieldInfo">字段信息</param>
    public static void LoadFieldAddr(this ILGenerator iLGenerator, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsStatic) iLGenerator.Emit(OpCodes.Ldsflda, fieldInfo);
        else iLGenerator.Emit(OpCodes.Ldflda, fieldInfo);
    }
    #endregion

    #region 地址To值
    /// <summary>
    /// 推送堆栈顶部地址处 <paramref name="type"/> 类型的值<br/>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">数据类型,如果是引用类型可以忽略该参数</param>
    public static void LoadAddrValue(this ILGenerator iLGenerator, Type? type = null)
    {
        // 整数
        if (type == typeof(IntPtr)) iLGenerator.Emit(OpCodes.Ldind_I);

        else if (type == typeof(sbyte)) iLGenerator.Emit(OpCodes.Ldind_I1);
        else if (type == typeof(byte)) iLGenerator.Emit(OpCodes.Ldind_U1);

        else if (type == typeof(short)) iLGenerator.Emit(OpCodes.Ldind_I2);
        else if (type == typeof(ushort)) iLGenerator.Emit(OpCodes.Ldind_U2);

        else if (type == typeof(int)) iLGenerator.Emit(OpCodes.Ldind_I4);
        else if (type == typeof(uint)) iLGenerator.Emit(OpCodes.Ldind_U4);

        else if (type == typeof(long)) iLGenerator.Emit(OpCodes.Ldind_I8);

        // 浮点数
        else if (type == typeof(float)) iLGenerator.Emit(OpCodes.Ldind_R4);
        else if (type == typeof(double)) iLGenerator.Emit(OpCodes.Ldind_R8);

        // 引用类型
        else if (type is null) iLGenerator.Emit(OpCodes.Ldind_Ref);
        else iLGenerator.Emit(OpCodes.Ldobj, type);
    }
    #endregion

    #region 指针
    /// <summary>
    /// 推送方法指针
    /// <list type="bullet">
    ///     <item>1.推送虚方法的对象引用【非虚方法忽略】</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="methodInfo">方法信息</param>
    public static void LoadMethodPointer(this ILGenerator iLGenerator, MethodInfo methodInfo)
    {
        if (methodInfo.IsVirtual) iLGenerator.Emit(OpCodes.Ldvirtftn, methodInfo);
        else iLGenerator.Emit(OpCodes.Ldftn, methodInfo);
    }
    #endregion

    #region 常量
    /// <summary>
    /// 推送 <see cref="int"/> / <see cref="short"/> / <see cref="sbyte"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="value">要推送的数字常量</param>
    public static void LoadInt(this ILGenerator iLGenerator, int value)
    {
        if (value is >= sbyte.MinValue and <= sbyte.MaxValue)
        {
            iLGenerator.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
            return;
        }

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
            default: iLGenerator.Emit(OpCodes.Ldc_I4, value); return;
        }
    }

    /// <summary>
    /// 推送 <see cref="long"/> 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="value">要推送的数字常量</param>
    public static void LoadInt64(this ILGenerator iLGenerator, long value) => iLGenerator.Emit(OpCodes.Ldc_I8, value);

    /// <summary>
    /// 推送 <see cref="float"/>(float32) 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="value">要推送的数字常量</param>
    public static void LoadFloat(this ILGenerator iLGenerator, float value) => iLGenerator.Emit(OpCodes.Ldc_R4, value);

    /// <summary>
    /// 推送 <see cref="double"/>(float64) 数字常量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="value">要推送的数字常量</param>
    public static void LoadDouble(this ILGenerator iLGenerator, double value) => iLGenerator.Emit(OpCodes.Ldc_R8, value);

    /// <summary>
    /// 推送 <see cref="string"/> 类型对象
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="value">要推送的字符串</param>
    public static void LoadString(this ILGenerator iLGenerator, string value) => iLGenerator.Emit(OpCodes.Ldstr, value);

    /// <summary>
    /// 推送空引用(null)
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void LoadNull(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ldnull);
    #endregion
    #endregion

    #region 初始化
    /// <summary>
    /// 初始化一个数组,并推送结果
    /// <list type="bullet">
    ///     <item>1.推送数组长度</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">数组类型</param>
    public static void NewArray(this ILGenerator iLGenerator, Type type) => iLGenerator.Emit(OpCodes.Newarr, type);

    /// <summary>
    /// 初始化一个对象,并推送结果
    /// <list type="bullet">
    ///     <item>1.按顺序推送构造函数的参数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="constructorInfo">创建对象的构造函数</param>
    public static void NewObject(this ILGenerator iLGenerator, ConstructorInfo constructorInfo) => iLGenerator.Emit(OpCodes.Newobj, constructorInfo);
    #endregion

    #region 赋值
    /// <summary>
    /// 将堆栈顶部的值赋值给指定索引处的参数
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="index">参数索引</param>
    public static void SetArg(this ILGenerator iLGenerator, ushort index = 0)
    {
        switch (index)
        {
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Starg_S, (byte)index); return;
            default: iLGenerator.Emit(OpCodes.Starg, index); return;
        }
    }

    /// <summary>
    /// 将堆栈顶部的值赋值给指定的局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="localBuilder">局部变量</param>
    public static void SetLocal(this ILGenerator iLGenerator, LocalBuilder localBuilder)
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
    /// 将堆栈顶部的值赋值给索引处的局部变量
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="localIndex">局部变量索引</param>
    public static void SetLocal(this ILGenerator iLGenerator, ushort localIndex)
    {
        switch (localIndex)
        {
            case 0: iLGenerator.Emit(OpCodes.Stloc_0); return;
            case 1: iLGenerator.Emit(OpCodes.Stloc_1); return;
            case 2: iLGenerator.Emit(OpCodes.Stloc_2); return;
            case 3: iLGenerator.Emit(OpCodes.Stloc_3); return;
            case <= byte.MaxValue: iLGenerator.Emit(OpCodes.Stloc_S, (byte)localIndex); return;
            default: iLGenerator.Emit(OpCodes.Stloc, localIndex); return;
        }
    }

    /// <summary>
    /// 将堆栈顶部的值赋值给指定的字段
    /// <list type="bullet">
    ///     <item>1.推送字段的对象引用(或指针)【静态字段忽略】</item>
    ///     <item>2.推送值</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="fieldInfo">字段信息</param>
    public static void SetField(this ILGenerator iLGenerator, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsStatic) iLGenerator.Emit(OpCodes.Stsfld, fieldInfo);
        else iLGenerator.Emit(OpCodes.Stfld, fieldInfo);
    }

    /// <summary>
    /// 为数组指定索引处元素赋值
    /// <list type="bullet">
    ///     <item>1.推送数组</item>
    ///     <item>2.推送要赋值的索引</item>
    ///     <item>3.推送值</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valueType">值类型,如果是引用类型可以忽略</param>
    public static void SetArray(this ILGenerator iLGenerator, Type? valueType = null)
    {
        if (valueType is null) iLGenerator.Emit(OpCodes.Stelem_Ref);// 只支持引用类型
        else if (valueType == typeof(IntPtr)) iLGenerator.Emit(OpCodes.Stelem_I);// native int
        else if (valueType == typeof(sbyte)) iLGenerator.Emit(OpCodes.Stelem_I1);// int8
        else if (valueType == typeof(short)) iLGenerator.Emit(OpCodes.Stelem_I2);
        else if (valueType == typeof(int)) iLGenerator.Emit(OpCodes.Stelem_I4);
        else if (valueType == typeof(long)) iLGenerator.Emit(OpCodes.Stelem_I8);// long
        else if (valueType == typeof(float)) iLGenerator.Emit(OpCodes.Stelem_R4);// float32
        else if (valueType == typeof(double)) iLGenerator.Emit(OpCodes.Stelem_R8);// float64
        else iLGenerator.Emit(OpCodes.Stelem, valueType);
    }

    #region 地址
    /// <summary>
    /// 为指定地址设置值
    /// <list type="bullet">
    ///     <item>1.推送地址</item>
    ///     <item>2.推送值</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valueType">值类型,如果是引用类型可以忽略</param>
    public static void SetValueToAddr(this ILGenerator iLGenerator, Type? valueType = null)
    {
        if (valueType is null) iLGenerator.Emit(OpCodes.Stind_Ref);// 只支持引用类型
        else if (valueType == typeof(IntPtr)) iLGenerator.Emit(OpCodes.Stind_I);// native int
        else if (valueType == typeof(sbyte)) iLGenerator.Emit(OpCodes.Stind_I1);// int8
        else if (valueType == typeof(short)) iLGenerator.Emit(OpCodes.Stind_I2);
        else if (valueType == typeof(int)) iLGenerator.Emit(OpCodes.Stind_I4);
        else if (valueType == typeof(long)) iLGenerator.Emit(OpCodes.Stind_I8);// long
        else if (valueType == typeof(float)) iLGenerator.Emit(OpCodes.Stind_R4);// float32
        else if (valueType == typeof(double)) iLGenerator.Emit(OpCodes.Stind_R8);// float64
        else iLGenerator.Emit(OpCodes.Stobj, valueType);
    }
    #endregion
    #endregion

    #region 数学
    /// <summary>
    /// 将堆栈顶部两个值相加,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isOverflowCheck">是否启用溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动启用溢出检查</param>
    public static void MathAdd(this ILGenerator iLGenerator, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Add_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Add_Ovf);
        else iLGenerator.Emit(OpCodes.Add);
    }

    /// <summary>
    /// 将堆栈顶部两个值相减,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isOverflowCheck">是否启动溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动启用溢出检查</param>
    public static void MathSub(this ILGenerator iLGenerator, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Sub_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Sub_Ovf);
        else iLGenerator.Emit(OpCodes.Sub);
    }

    /// <summary>
    /// 将堆栈顶部两个值相乘,并将结果推送
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isOverflowCheck">是否启动溢出检查</param>
    /// <param name="isUnsigned">是否是无符号,开启无符号自动启用溢出检查</param>
    public static void MathMul(this ILGenerator iLGenerator, bool isOverflowCheck = false, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Mul_Ovf_Un);
        else if (isOverflowCheck) iLGenerator.Emit(OpCodes.Mul_Ovf);
        else iLGenerator.Emit(OpCodes.Mul);
    }

    /// <summary>
    /// 将堆栈顶部两个值相除,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isUnsigned">被除数和除数是否是无符号</param>
    public static void MathDiv(this ILGenerator iLGenerator, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Div_Un);
        else iLGenerator.Emit(OpCodes.Div);
    }

    /// <summary>
    /// 将堆栈顶部两个值求余,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isUnsigned">两个求余的值是否是无符号</param>
    public static void MathRem(this ILGenerator iLGenerator, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Rem_Un);
        else iLGenerator.Emit(OpCodes.Rem);
    }

    /// <summary>
    /// 将堆栈顶部的值执行"求反"操作,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void MathNeg(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Neg);
    #endregion

    #region 按位计算
    /// <summary>
    /// 将堆栈顶部两个值执行"按位与"操作,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BitwiseAnd(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.And);

    /// <summary>
    /// 将堆栈顶部两个值执行"按位或"操作,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BitwiseOr(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Or);

    /// <summary>
    /// 将堆栈顶部两个值执行"按位异或"操作,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BitwiseXor(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Xor);

    /// <summary>
    /// 将堆栈顶部的值执行"按位求补"操作,并推送结果
    /// 对整数值,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BitwiseNot(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Not);

    /// <summary>
    /// 将堆栈顶部整数值执行"左移位"操作,并推送结果
    /// <list type="bullet">
    ///     <item>1.推送值</item>
    ///     <item>2.推送位数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BitwiseShiftLeft(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Shl);

    /// <summary>
    /// 将堆栈顶部整数值执行"右移位"操作,并推送结果
    /// <list type="bullet">
    ///     <item>1.推送值</item>
    ///     <item>2.推送位数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isUnsigned">移位值是否是无符号值</param>
    public static void BitwiseShiftRight(this ILGenerator iLGenerator, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Shr_Un);
        else iLGenerator.Emit(OpCodes.Shr);
    }
    #endregion

    #region 比较
    /// <summary>
    /// 比较堆栈顶部两个值,如果<c>value1 == value2</c>推送 1,否则推送 0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void CompareEqual(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ceq);

    /// <summary>
    /// 比较堆栈顶部两个值,如果 value1 > value2 推送 1,否则推送 0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isUnsigned">是否是无符号的或未经排序的值</param>
    public static void CompareGreater(this ILGenerator iLGenerator, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Cgt_Un);
        else iLGenerator.Emit(OpCodes.Cgt);
    }

    /// <summary>
    /// 比较堆栈顶部两个值,如果<c>value1 &lt; value2</c>推送 1,否则推送 0
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="isUnsigned">是否是无符号的或未经排序的值</param>
    public static void CompareLess(this ILGenerator iLGenerator, bool isUnsigned = false)
    {
        if (isUnsigned) iLGenerator.Emit(OpCodes.Clt_Un);
        else iLGenerator.Emit(OpCodes.Clt);
    }
    #endregion

    #region 调用方法
    /// <summary>
    /// 调用构造函数,该方法针对返回地址的构造函数
    /// <br>如 <see cref="IntPtr"/> / ArgIterator 等类型</br>
    /// <br>该方法不会推送结果</br>
    /// <list type="bullet">
    ///     <item>1.推送接收构造函数返回的地址</item>
    ///     <item>2.推送构造函数的参数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="constructorInfo">构造函数信息</param>
    public static void Call(this ILGenerator iLGenerator, ConstructorInfo constructorInfo) => iLGenerator.Emit(OpCodes.Call, constructorInfo);

    /// <summary>
    /// 调用方法/虚方法,并推送结果
    /// <br>如果使用该方法调用虚方法,表明是使用方法指定的类来解析,而不是被调用的对象动态指定</br>
    /// <list type="bullet">
    ///     <item>1.推送实例(静态方法忽略)</item>
    ///     <item>2.推送方法的参数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="methodInfo">方法信息</param>
    /// <param name="optionalParameterTypes">可变参数类型,如果存在该值说明方法的调用约定包含 <see cref=" CallingConventions.VarArgs"/></param>
    public static void Call(this ILGenerator iLGenerator, MethodInfo methodInfo, params Type[] optionalParameterTypes)
    {
        if (optionalParameterTypes.IsNullOrEmpty()) iLGenerator.Emit(OpCodes.Call, methodInfo);
        else iLGenerator.EmitCall(OpCodes.Call, methodInfo, optionalParameterTypes);
    }

    /// <summary>
    /// 调用虚方法,并推送结果
    /// <br>方法是被调用的对象动态指定的,而不是根据编译时类型指定</br>
    /// <list type="bullet">
    ///     <item>1.推送实例</item>
    ///     <item>2.推送方法的参数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="methodInfo">方法信息</param>
    /// <param name="optionalParameterTypes">可变参数类型,如果存在该值说明方法的调用约定包含 <see cref=" CallingConventions.VarArgs"/></param>
    public static void CallVirtual(this ILGenerator iLGenerator, MethodInfo methodInfo, params Type[] optionalParameterTypes)
    {
        if (optionalParameterTypes.IsNullOrEmpty()) iLGenerator.Emit(OpCodes.Callvirt, methodInfo);
        else iLGenerator.EmitCall(OpCodes.Callvirt, methodInfo, optionalParameterTypes);
    }

    /// <summary>
    /// 尾调用,必须在 <see cref="OpCodes.Call"/> / <see cref="OpCodes.Callvirt"/> 指令之前调用,标记该调用为最后的调用
    /// <br>标记了该指令后,执行实际调用指令之前删除当前方法的堆栈帧</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void TailCall(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Tailcall);

    /// <summary>
    /// 对虚方法的实例类型进行约束
    /// <br>只能在 <see cref="OpCodes.Callvirt"/> 指令之前使用</br>
    /// <list type="bullet">
    ///     <item>1.推送实例指针</item>
    ///     <item>2.推送方法的参数</item>
    ///     <item>3.使用该方法进行约束</item>
    ///     <item>4.使用 <see cref="OpCodes.Callvirt"/> 指令进行调用</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="constrainedType">约束类型</param>
    public static void Constrained(this ILGenerator iLGenerator, Type constrainedType) => iLGenerator.Emit(OpCodes.Constrained, constrainedType);

    #region 地址
    /// <summary>
    /// 【用于托管调用】调用指针处的方法,并推送结果
    /// <list type="bullet">
    ///     <item>1.推送实例(静态方法忽略)</item>
    ///     <item>2.推送参数</item>
    ///     <item>2.推送方法指针</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="callingConvention">方法调用约定</param>
    /// <param name="returnType">方法的返回类型</param>
    /// <param name="parameterTypes">方法的参数类型数组</param>
    /// <param name="optionalParameterTypes">可变参数类型数组</param>
    public static void Calli(this ILGenerator iLGenerator, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[]? optionalParameterTypes = null) => iLGenerator.EmitCalli(OpCodes.Calli, callingConvention, returnType, parameterTypes, optionalParameterTypes);

#if !NETSTANDARD2_0
    /// <summary>
    /// 【用于非托管调用】调用指针处的方法,并推送结果
    /// <list type="bullet">
    ///     <item>1.推送实例(静态方法忽略)</item>
    ///     <item>2.推送参数</item>
    ///     <item>2.推送方法指针</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="callingConvention">方法调用约定</param>
    /// <param name="returnType">方法的返回类型</param>
    /// <param name="parameterTypes">方法的参数类型数组</param>
    public static void Calli(this ILGenerator iLGenerator, Runtime.InteropServices.CallingConvention callingConvention, Type returnType, params Type[] parameterTypes) => iLGenerator.EmitCalli(OpCodes.Calli, callingConvention, returnType, parameterTypes);
#endif
    #endregion
    #endregion

    #region Goto
    /// <summary>
    /// 跳转
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void Goto(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Br_S, label);
        else iLGenerator.Emit(OpCodes.Br, label);
    }
    /// <summary>
    /// 跳转(强制),可用于退出 try\filter\catch 块
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void GotoLeave(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Leave_S, label);
        else iLGenerator.Emit(OpCodes.Leave, label);
    }

    /// <summary>
    /// 如果值为 true\非空\非零 则跳转
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void GotoIfTrue(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Brtrue_S, label);
        else iLGenerator.Emit(OpCodes.Brtrue, label);
    }
    /// <summary>
    /// 如果堆栈顶部的值为 false\空引用\零 则跳转
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void GotoIfFalse(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Brfalse_S, label);
        else iLGenerator.Emit(OpCodes.Brfalse, label);
    }

    /// <summary>
    /// 如果 value1 == value2 则跳转
    /// <list type="bullet">
    ///     <item>1.推送value1</item>
    ///     <item>2.推送value2</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void GotoIfEqual(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Beq_S, label);
        else iLGenerator.Emit(OpCodes.Beq, label);
    }
    /// <summary>
    /// 如果 value1 > value2 则跳转
    /// <list type="bullet">
    ///     <item>1.推送value1</item>
    ///     <item>2.推送value2</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    /// <param name="isUnsigned">是否是无符号整数值或未经排序的浮点值</param>
    public static void GotoIfGreater(this ILGenerator iLGenerator, Label label, bool isShort = true, bool isUnsigned = false)
    {
        if (isShort == isUnsigned == true) iLGenerator.Emit(OpCodes.Bgt_Un_S, label);
        else if (isUnsigned) iLGenerator.Emit(OpCodes.Bgt_Un, label);
        else if (isShort) iLGenerator.Emit(OpCodes.Bgt_S, label);
        else iLGenerator.Emit(OpCodes.Bgt, label);
    }
    /// <summary>
    /// 如果 value1 >= value2 则跳转
    /// <list type="bullet">
    ///     <item>1.推送value1</item>
    ///     <item>2.推送value2</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    /// <param name="isUnsigned">是否是无符号整数值或未经排序的浮点值</param>
    public static void GotoIfGreaterOrEqual(this ILGenerator iLGenerator, Label label, bool isShort = true, bool isUnsigned = false)
    {
        if (isShort == isUnsigned == true) iLGenerator.Emit(OpCodes.Bge_Un_S, label);
        else if (isUnsigned) iLGenerator.Emit(OpCodes.Bge_Un, label);
        else if (isShort) iLGenerator.Emit(OpCodes.Bge_S, label);
        else iLGenerator.Emit(OpCodes.Bge, label);
    }
    /// <summary>
    /// 如果 value1 &lt; value2 则跳转
    /// <list type="bullet">
    ///     <item>1.推送value1</item>
    ///     <item>2.推送value2</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    /// <param name="isUnsigned">是否是无符号整数值或未经排序的浮点值</param>
    public static void GotoIfLess(this ILGenerator iLGenerator, Label label, bool isShort = true, bool isUnsigned = false)
    {
        if (isShort == isUnsigned == true) iLGenerator.Emit(OpCodes.Blt_Un_S, label);
        else if (isUnsigned) iLGenerator.Emit(OpCodes.Blt_Un, label);
        else if (isShort) iLGenerator.Emit(OpCodes.Blt_S, label);
        else iLGenerator.Emit(OpCodes.Blt, label);
    }
    /// <summary>
    /// 如果 value1 &lt;= value2 则跳转
    /// <list type="bullet">
    ///     <item>1.推送value1</item>
    ///     <item>2.推送value2</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    /// <param name="isUnsigned">是否是无符号整数值或未经排序的浮点值</param>
    public static void GotoIfLessOrEqual(this ILGenerator iLGenerator, Label label, bool isShort = true, bool isUnsigned = false)
    {
        if (isShort == isUnsigned == true) iLGenerator.Emit(OpCodes.Ble_Un_S, label);
        else if (isUnsigned) iLGenerator.Emit(OpCodes.Ble_Un, label);
        else if (isShort) iLGenerator.Emit(OpCodes.Ble_S, label);
        else iLGenerator.Emit(OpCodes.Ble, label);
    }

    /// <summary>
    /// 如果两个无符号整数值或未经排序的浮点值不相等则跳转
    /// <list type="bullet">
    ///     <item>1.推送第一个无符号整数值或未经排序的浮点值</item>
    ///     <item>2.推送第二个无符号整数值或未经排序的浮点值</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="label">跳转标签</param>
    /// <param name="isShort">是否短格式</param>
    public static void GotoIfUnsignedNotEqual(this ILGenerator iLGenerator, Label label, bool isShort = true)
    {
        if (isShort) iLGenerator.Emit(OpCodes.Bne_Un_S, label);
        else iLGenerator.Emit(OpCodes.Bne_Un, label);
    }

    /// <summary>
    /// 跳转到指定索引处的标签
    /// <list type="bullet">
    ///     <item>1.推送要跳转的标签的索引</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="labels">跳转标签集合</param>
    public static void Switch(this ILGenerator iLGenerator, Label[] labels) => iLGenerator.Emit(OpCodes.Switch, labels);
    #endregion

    #region 类型转换
    /// <summary>
    /// 将堆栈顶部的转换成 <paramref name="targetType"/>(引用类型) 类型,如果转换成功推送 <paramref name="targetType"/> 类型的对象,否则推送null
    /// <br>和代码中的as一样</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="targetType">目标类型(引用类型)</param>
    public static void As(this ILGenerator iLGenerator, Type targetType) => iLGenerator.Emit(OpCodes.Isinst, targetType);

    /// <summary>
    /// 将堆栈顶部的 <paramref name="valueType"/>(值类型) 类型的值转换为 <see cref="object"/> 类型,并推送结果
    /// <br>执行装箱操作</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valueType">值类型</param>
    public static void Box(this ILGenerator iLGenerator, Type valueType) => iLGenerator.Emit(OpCodes.Box, valueType);

    /// <summary>
    /// 将堆栈顶部的 <see cref="object"/> 类型的值转换为 <paramref name="valueType"/> 类型,并推送结果
    /// <br>执行拆箱操作</br>
    /// <list type="bullet">
    ///     <item>该指令应用于值类型时 == <see cref="OpCodes.Unbox"/> + <see cref="OpCodes.Ldobj"/></item>
    ///     <item>该指令应用于引用类型时 == <see cref="OpCodes.Castclass" /></item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valueType">值类型</param>
    public static void UnBox(this ILGenerator iLGenerator, Type valueType) => iLGenerator.Emit(OpCodes.Unbox_Any, valueType);

    /// <summary>
    /// 将堆栈顶部的 <see cref="object"/> 类型的值转换为 <paramref name="valueType"/>(值类型) 类型,并推送指针
    /// <br>该指令将对象引用(值类型的装箱表示)转换为未装箱形式的值类型指针</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valueType">值类型</param>
    public static void UnBoxThenLoadPointer(this ILGenerator iLGenerator, Type valueType) => iLGenerator.Emit(OpCodes.Unbox, valueType);

    /// <summary>
    /// 将堆栈顶部的引用类型对象转换成 <paramref name="targetType"/>(引用类型) 类型的引用,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="targetType">目标类型</param>
    public static void ConvertType(this ILGenerator iLGenerator, Type targetType) => iLGenerator.Emit(OpCodes.Castclass, targetType);

    /// <summary>
    /// 将堆栈顶部的值类型值转换成有符号整数类型的值,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="integerType">整数类型</param>
    /// <param name="isOverflowCheck">是否检查溢出,如果 <paramref name="integerType"/> 是无符号类型,将自动启用溢出检查</param>
    public static void ConvertInteger(this ILGenerator iLGenerator, Type integerType, bool isOverflowCheck = false)
    {
        if (integerType == typeof(IntPtr)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I); else iLGenerator.Emit(OpCodes.Conv_Ovf_I); } // 有符号转有符号
        else if (integerType == typeof(UIntPtr)) iLGenerator.Emit(OpCodes.Conv_Ovf_I_Un); //无符号转有符号

        else if (integerType == typeof(sbyte)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I1); else iLGenerator.Emit(OpCodes.Conv_Ovf_I1); }
        else if (integerType == typeof(byte)) iLGenerator.Emit(OpCodes.Conv_Ovf_I1_Un);

        else if (integerType == typeof(short)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I2); else iLGenerator.Emit(OpCodes.Conv_Ovf_I2); }
        else if (integerType == typeof(ushort)) iLGenerator.Emit(OpCodes.Conv_Ovf_I2_Un);

        else if (integerType == typeof(int)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I4); else iLGenerator.Emit(OpCodes.Conv_Ovf_I4); }
        else if (integerType == typeof(uint)) iLGenerator.Emit(OpCodes.Conv_Ovf_I4_Un);

        else if (integerType == typeof(long)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I8); else iLGenerator.Emit(OpCodes.Conv_Ovf_I8); }
        else if (integerType == typeof(ulong)) iLGenerator.Emit(OpCodes.Conv_Ovf_I8_Un);

        else throw new ArgumentException(Strings.InvalidParameter, nameof(integerType));
    }

    /// <summary>
    /// 将堆栈顶部的值类型值转换成无符号整数类型的值,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="integerType">整数类型</param>
    /// <param name="isOverflowCheck">是否检查溢出,如果 <paramref name="integerType"/> 是无符号类型,将自动启用溢出检查</param>
    public static void ConvertUnsignedInteger(this ILGenerator iLGenerator, Type integerType, bool isOverflowCheck = false)
    {
        if (integerType == typeof(IntPtr)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_U); else iLGenerator.Emit(OpCodes.Conv_Ovf_U); } // 有符号转无符号
        else if (integerType == typeof(UIntPtr)) iLGenerator.Emit(OpCodes.Conv_Ovf_U_Un); //无符号转无符号

        else if (integerType == typeof(sbyte)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_U1); else iLGenerator.Emit(OpCodes.Conv_Ovf_U1); }
        else if (integerType == typeof(byte)) iLGenerator.Emit(OpCodes.Conv_Ovf_U1_Un);

        else if (integerType == typeof(short)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I2); else iLGenerator.Emit(OpCodes.Conv_Ovf_U2); }
        else if (integerType == typeof(ulong)) iLGenerator.Emit(OpCodes.Conv_Ovf_U2_Un);

        else if (integerType == typeof(int)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I4); else iLGenerator.Emit(OpCodes.Conv_Ovf_U4); }
        else if (integerType == typeof(uint)) iLGenerator.Emit(OpCodes.Conv_Ovf_U4_Un);

        else if (integerType == typeof(long)) { if (!isOverflowCheck) iLGenerator.Emit(OpCodes.Conv_I8); else iLGenerator.Emit(OpCodes.Conv_Ovf_U8); }
        else if (integerType == typeof(ulong)) iLGenerator.Emit(OpCodes.Conv_Ovf_U8_Un);
    }

    /// <summary>
    /// 将堆栈顶部的值类型值转换成 <paramref name="floatType"/> 类型浮点值,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="floatType">浮点类型</param>
    public static void ConvertFloat(this ILGenerator iLGenerator, Type floatType)
    {
        if (floatType == typeof(float)) iLGenerator.Emit(OpCodes.Conv_R4);
        else if (floatType == typeof(double)) iLGenerator.Emit(OpCodes.Conv_R8);
        else throw new ArgumentException(Strings.InvalidParameter, nameof(floatType));
    }

    /// <summary>
    /// 将堆栈顶部的无符号整数值转换成 <see cref="float"/> 类型浮点值,并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void ConvertUnsignedIntegerToFloat(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Conv_R_Un);
    #endregion

    #region 检查
    /// <summary>
    /// 检查堆栈顶部的值,如果是 非数字/+-无穷大 引发 <see cref="ArgumentException"/> 异常,否则将原值推送回堆栈
    /// </summary>
    /// <param name="iLGenerator">中间语言指令</param>
    public static void Ckfinite(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ckfinite);
    #endregion

    #region 异常
    /// <summary>
    /// 抛出异常
    /// <list type="bullet">
    ///     <item>1.推送 <see cref="Exception"/> 类型(包括子类)异常对象</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Throw(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Throw);

    /// <summary>
    /// 再次抛出异常在 Catch 中使用
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void ReThrow(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Rethrow);
    #endregion

    #region Try Catch Finally
    /// <summary>
    /// 异常筛选完成
    /// <br>该指令不应自己添加,<see cref="ILGenerator.BeginCatchBlock(Type)"/> 方法中自动添加,手动添加会又异常</br>
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void EndFilter(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Endfilter);

    /// <summary>
    /// Finally 块完成
    /// <br>该指令可以忽略</br>
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void EndFinally(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Endfinally);
    #endregion

    #region 复制
    /// <summary>
    /// 复制堆栈顶部的值,并推送原值和副本
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Copy(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Dup);

    /// <summary>
    /// 复制地址的值到指定地址
    /// <list type="bullet">
    ///     <item>1.推送目标地址</item>
    ///     <item>2.推送原地址</item>
    ///     <item>3.推送数据的大小(byte 字节)</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void CopyAddrValueToAddr(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Cpblk);

    /// <summary>
    /// 原：将位于对象地址的值（类型 &amp; 或 native int）复制到目标对象的地址（类型 &amp; 或 native int）
    /// <list type="bullet">
    ///     <item>1.推送目标地址</item>
    ///     <item>2.推送原地址</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">测试中所有size相同的类型都可以正常运行</param>
    public static void Cpobj(this ILGenerator iLGenerator, Type type) => iLGenerator.Emit(OpCodes.Cpobj, type);
    #endregion

    #region 引用化
    /// <summary>
    /// 将堆栈顶部地址的值引用化,并推送 <see cref="TypedReference"/> 类型的值
    /// <list type="bullet">
    ///     <item>1.推送地址</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">要执行引用化的类型</param>
    public static void Mkrefany(this ILGenerator iLGenerator, Type type) => iLGenerator.Emit(OpCodes.Mkrefany, type);

    /// <summary>
    /// 推送堆栈顶部 <see cref="TypedReference"/>(引用化值类型) 类型的值嵌套的值的类型
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Refanytype(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Refanytype);

    /// <summary>
    /// 推送堆栈顶部 <see cref="TypedReference"/>(引用化值类型) 类型中嵌套的值的地址
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valType">值类型</param>
    public static void Refanyval(this ILGenerator iLGenerator, Type valType) => iLGenerator.Emit(OpCodes.Refanyval, valType);
    #endregion

    #region 未分类
    /// <summary>
    /// 方法结束，如果有返回值推送返回值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Return(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Ret);

    /// <summary>
    /// 退出当前方法并跳至指定方法
    /// <br>直接转移到指定方法,要求跳转的方法和当前方法有相同的签名</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="methodInfo">要跳转的方法,和当前方法签名一致</param>
    public static void Jmp(this ILGenerator iLGenerator, MethodInfo methodInfo) => iLGenerator.Emit(OpCodes.Jmp, methodInfo);

    /// <summary>
    /// 移除堆栈顶部的值
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Pop(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Pop);

    /// <summary>
    /// 计算 <paramref name="valType"/>(值类型) 类型的大小(byte 字节为单位),并推送结果
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="valType">值类型</param>
    public static void SizeOf(this ILGenerator iLGenerator, Type valType) => iLGenerator.Emit(OpCodes.Sizeof, valType);

    /// <summary>
    /// 推送 <paramref name="token"/> 的运行时句柄
    /// </summary>
    /// <typeparam name="T"><see cref="Type"/> / <seealso cref="FieldInfo"/> / <seealso cref="MethodInfo"/></typeparam>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="token">获取运行时句柄的token</param>
    public static void LoadRuntimeHandle<T>(this ILGenerator iLGenerator, T token) where T : MemberInfo
    {
        if (typeof(T) == typeof(Type)) iLGenerator.Emit(OpCodes.Ldtoken, (token as Type)!);
        else if (typeof(T) == typeof(FieldInfo)) iLGenerator.Emit(OpCodes.Ldtoken, (token as FieldInfo)!);
        else if (typeof(T) == typeof(MethodInfo)) iLGenerator.Emit(OpCodes.Ldtoken, (token as MethodInfo)!);
        else throw new ArgumentException(Strings.InvalidParameter, nameof(token));
    }

    /// <summary>
    /// 为指定地址的设置默认值
    /// <br>相当于 default(<paramref name="type"/>)</br>
    /// <list type="bullet">
    ///     <item>1.推送地址</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="type">数据类型</param>
    public static void Initobj(this ILGenerator iLGenerator, Type type) => iLGenerator.Emit(OpCodes.Initobj, type);

    /// <summary>
    /// 在本地动态内存池分配指定数目的字节,并推送第一个字节的地址
    /// <list type="bullet">
    ///     <item>1.推送要分配的字节数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Localloc(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Localloc);

    /// <summary>
    /// 为指定地址开始的字节数设置初始值(每一个字节最大值是255)
    /// <list type="bullet">
    ///     <item>1.推送起始地址</item>
    ///     <item>2.推送初始值(最大255)</item>
    ///     <item>3.推送要设置初始值的字节数</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Initblk(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Initblk);

    /// <summary>
    /// 不执行任何堆栈操作,通知调试器碰撞一个断点
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void BreakPoint(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Break);

    /// <summary>
    /// 指示当前位于计算堆栈的地址可能没有与紧接的 ldind, stind, ldfld, stfld, ldobj, stobj, initblk, cpblk 指令的自然大小对其
    /// <br>在必要的时候执行 ldind, stind, ldfld, stfld, ldobj, stobj, initblk, cpblk 指令前先进行对其</br>
    /// <br>理论上来说 sizeof(T) % 8 != 0 的都因使用此指令对其</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    /// <param name="alignment">对其值 1, 2, 4</param>
    public static void Unaligned(this ILGenerator iLGenerator, byte alignment) => iLGenerator.Emit(OpCodes.Unaligned, alignment);

    /// <summary>
    /// 表示后面要执行的指令操作的值是易失,在 ldind, stind, ldfld, stfld, ldobj, stobj, initblk, cpblk 指令前使用
    /// <br>如果声明的变量有 volatile 关键字,在操作该变量之前都要使用该指令</br>
    /// <br>使用 <see cref="Emit"/> 声明变量添加 <see cref="System.Runtime.CompilerServices.IsVolatile"/> 类型到必选修饰符</br>
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Volatile(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Volatile);

    /// <summary>
    /// 如果修补了操作码,则填充空间。但是没有执行任何有意义的操作
    /// </summary>
    /// <param name="iLGenerator">中间语言指令生成器</param>
    public static void Nop(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Nop);
    #endregion

    #region 有歧义
    /// <summary>
    /// 原：指定后面的数组地址不执行类型检查,并且推送可变性受限的指针 <see href="https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.emit.opcodes.readonly"/>
    /// <br>测试：标记了这个过后还可以可以设置指针的值,和修改值</br>
    /// <list type="bullet">
    ///     <item>1.推送数组</item>
    ///     <item>2.推送索引</item>
    ///     <item>3.使用该指令</item>
    ///     <item>4.使用 <see cref="OpCodes.Ldelema"/> 指令获取地址</item>
    /// </list>
    /// </summary>
    /// <param name="iLGenerator"></param>
    public static void Readonly(this ILGenerator iLGenerator) => iLGenerator.Emit(OpCodes.Readonly);
    #endregion
}
