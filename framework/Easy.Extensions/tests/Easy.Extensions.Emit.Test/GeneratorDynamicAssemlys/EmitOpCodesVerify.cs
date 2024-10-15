using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// Emit 指令验证
/// </summary>
public static class EmitOpCodesVerifyCreator
{
    // 类型构建器
    private static TypeBuilder _typeBuilder;
    // 字段
    private static FieldBuilder _field_num;
    // 易失字段
    private static FieldBuilder _field_num2;

    public static Type DefineType_EmitOpCodesVerify(this ModuleBuilder moduleBuilder)
    {
        // 定义类型构建器
        _typeBuilder = moduleBuilder.DefineType("EmitOpCodesVerify", TypeAttributes.Public);

        // 执行声明方法的方法
        typeof(EmitOpCodesVerifyCreator).GetMethods()
            .Where(m => m.Name.StartsWith("DefineMethod_")).ToList()
            .ForEach(m => m.Invoke(null, []));

        return _typeBuilder.CreateType();
    }


    #region 数学
    #region +
    // +
    public static MethodBuilder DefineMethod_Add1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Add1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 + arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathAdd();
        il.Return();

        return methodBuilder;
    }
    // 检查溢出 +
    public static MethodBuilder DefineMethod_Add2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Add2", typeof(int), [typeof(int), typeof(int)]);

        // return checked(arg1 + arg2);
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathAdd(isOverflowCheck: true);
        il.Return();

        return methodBuilder;
    }
    // 检查溢出无符号 +
    public static MethodBuilder DefineMethod_Add3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Add3", typeof(int), [typeof(int), typeof(int)]);

        // return (int)checked(unchecked((uint)P_0) + unchecked((uint)P_1));
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathAdd(isUnsigned: true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region -
    // -
    public static MethodBuilder DefineMethod_Sub1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Sub1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 - arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathSub();
        il.Return();

        return methodBuilder;
    }
    // 检查溢出 -
    public static MethodBuilder DefineMethod_Sub2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Sub2", typeof(int), [typeof(int), typeof(int)]);

        // return checked(arg1 - arg2);
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathSub(isOverflowCheck: true);
        il.Return();

        return methodBuilder;
    }
    // 检查溢出无符号 -
    public static MethodBuilder DefineMethod_Sub3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Sub3", typeof(int), [typeof(int), typeof(int)]);

        // return (int)checked(unchecked((uint)P_0) - unchecked((uint)P_1));
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathSub(isUnsigned: true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region *
    // *
    public static MethodBuilder DefineMethod_Mul1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Mul1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 * arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathMul();
        il.Return();

        return methodBuilder;
    }
    // 检查溢出 *
    public static MethodBuilder DefineMethod_Mul2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Mul2", typeof(int), [typeof(int), typeof(int)]);

        // return checked(arg1 * arg2);
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathMul(isOverflowCheck: true);
        il.Return();

        return methodBuilder;
    }
    // 检查溢出无符号 *
    public static MethodBuilder DefineMethod_Mul3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Mul3", typeof(int), [typeof(int), typeof(int)]);

        // return (int)checked(unchecked((uint)P_0) * unchecked((uint)P_1));
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathMul(isUnsigned: true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region /
    // /
    public static MethodBuilder DefineMethod_Div1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Div1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 / arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathDiv();
        il.Return();

        return methodBuilder;
    }
    // 无符号 /
    public static MethodBuilder DefineMethod_Div2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Div2", typeof(int), [typeof(int), typeof(int)]);

        // return (int)((uint)P_0 / (uint)P_1);
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathDiv(isUnsigned: true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region %
    // %
    public static MethodBuilder DefineMethod_Rem1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Rem1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 % arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathRem();
        il.Return();

        return methodBuilder;
    }
    // 无符号 %
    public static MethodBuilder DefineMethod_Rem2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Rem2", typeof(int), [typeof(int), typeof(int)]);

        // return (int)((uint)P_0 % (uint)P_1);
        il.LoadArg(0);
        il.LoadArg(1);
        il.MathRem(isUnsigned: true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 求反
    // -arg
    public static MethodBuilder DefineMethod_Neg1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Neg1", typeof(int), [typeof(int)]);

        // return -arg1;
        il.LoadArg(0);
        il.MathNeg();
        il.Return();

        return methodBuilder;
    }
    #endregion
    #endregion

    #region 位运算
    // &
    public static MethodBuilder DefineMethod_And1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("And1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 & arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseAnd();
        il.Return();

        return methodBuilder;
    }

    // |
    public static MethodBuilder DefineMethod_Or1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Or1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 | arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseOr();
        il.Return();

        return methodBuilder;
    }

    // ^
    public static MethodBuilder DefineMethod_Xor1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Xor1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 ^ arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseXor();
        il.Return();

        return methodBuilder;
    }

    // ~
    public static MethodBuilder DefineMethod_Not1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Not1", typeof(int), [typeof(int)]);

        // return ~arg1;
        il.LoadArg(0);
        il.BitwiseNot();
        il.Return();

        return methodBuilder;
    }

    // <<
    public static MethodBuilder DefineMethod_ShiftLeft1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ShiftLeft1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 << arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseShiftLeft();
        il.Return();

        return methodBuilder;
    }

    // >>
    public static MethodBuilder DefineMethod_ShiftRight1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ShiftRight1", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 >> arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseShiftRight();
        il.Return();

        return methodBuilder;
    }
    // 无符号 >>
    public static MethodBuilder DefineMethod_ShiftRight2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ShiftRight2", typeof(int), [typeof(int), typeof(int)]);

        // return arg1 >> arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.BitwiseShiftRight(true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 比较
    // ==
    public static MethodBuilder DefineMethod_Equal1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Equal1", typeof(bool), [typeof(int), typeof(int)]);

        // return arg1 == arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.CompareEqual();
        il.Return();

        return methodBuilder;
    }

    // >
    public static MethodBuilder DefineMethod_Greater1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Greater1", typeof(bool), [typeof(int), typeof(int)]);

        // return arg1 > arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.CompareGreater();
        il.Return();

        return methodBuilder;
    }
    // 无符号 >
    public static MethodBuilder DefineMethod_Greater2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Greater2", typeof(bool), [typeof(int), typeof(int)]);

        // return arg1 > arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.CompareGreater(true);
        il.Return();

        return methodBuilder;
    }

    // <
    public static MethodBuilder DefineMethod_Less1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Less1", typeof(bool), [typeof(int), typeof(int)]);

        // return arg1 < arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.CompareLess();
        il.Return();

        return methodBuilder;
    }
    // 无符号 <
    public static MethodBuilder DefineMethod_Less2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Less2", typeof(bool), [typeof(int), typeof(int)]);

        // return arg1 < arg2;
        il.LoadArg(0);
        il.LoadArg(1);
        il.CompareLess(true);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 数组
    // 获取一个数组
    public static MethodBuilder DefineMethod_Array1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Array1", typeof(int[]), Type.EmptyTypes);

        // int[] v_array;
        LocalBuilder v_array = il.DeclareLocal(typeof(int[]));
        // v_array = new int[3];
        il.LoadInt(3);
        il.NewArray(typeof(int));
        il.SetLocal(v_array);
        // v_array[0] = 3;
        il.LoadLocal(v_array);
        il.LoadInt(0);
        il.LoadInt(3);
        il.SetArray(typeof(int));
        // v_array[1] = 4;
        il.LoadLocal(v_array);
        il.LoadInt(1);
        il.LoadInt(4);
        il.SetArray(typeof(int));
        // v_array[2] = 5;
        il.LoadLocal(v_array);
        il.LoadInt(2);
        il.LoadInt(5);
        il.SetArray(typeof(int));
        // return v_array;
        il.LoadLocal(v_array);
        il.Return();

        return methodBuilder;
    }

    // 获取数组元素
    public static MethodBuilder DefineMethod_Array2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Array2", typeof(string), [typeof(string[]), typeof(int)]);

        // return arg1[arg2];
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadArrayIndexValue(typeof(string));
        il.Return();

        return methodBuilder;
    }

    // 获取数组中某个索引的地址的值
    public static MethodBuilder DefineMethod_Array3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Array3", typeof(string), [typeof(string[]), typeof(int)]);

        // return arg1[arg2];
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadArrayIndexAddr(typeof(string));
        il.LoadAddrValue(typeof(string));
        il.Return();

        return methodBuilder;
    }

    // 获取数组长度
    public static MethodBuilder DefineMethod_Array4()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Array4", typeof(int), [typeof(string[])]);

        // return arg1.Length;
        il.LoadArg(0);
        il.LoadLength();
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 类型转换
    // 装箱
    public static MethodBuilder DefineMethod_Box1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Box1", typeof(object), [typeof(int)]);

        // return (object)arg1;
        il.LoadArg(0);
        il.Box(typeof(int));
        il.Return();

        return methodBuilder;
    }

    // 拆箱
    public static MethodBuilder DefineMethod_UnBox1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("UnBox1", typeof(int), [typeof(object)]);

        // return (int)arg1;
        il.LoadArg(0);
        il.UnBox(typeof(int));
        il.Return();

        return methodBuilder;
    }
    // 拆箱后推送指针再从指针加载数据
    public static MethodBuilder DefineMethod_UnBox2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("UnBox2", typeof(int), [typeof(object)]);

        // return (int)arg1;
        il.LoadArg(0);
        il.UnBoxThenLoadPointer(typeof(int));
        il.LoadAddrValue(typeof(int));
        il.Return();

        return methodBuilder;
    }

    // float to int
    public static MethodBuilder DefineMethod_ConvertInteger1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ConvertInteger1", typeof(int), [typeof(float)]);

        // return (int)arg1;
        il.LoadArg(0);
        il.ConvertInteger(typeof(int));
        il.Return();

        return methodBuilder;
    }

    // as
    public static MethodBuilder DefineMethod_As1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("As1", typeof(Type), [typeof(object)]);

        // return (Type)arg1;
        il.LoadArg(0);
        il.As(typeof(Type));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 字符串相加
    // string1 + string2
    public static MethodBuilder DefineMethod_StringAdd1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Add1", typeof(string), [typeof(string), typeof(string)]);

        il.LoadArg(0);
        il.LoadArg(1);
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string)]));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 调用方法
    // 执行的方法是普通方法
    //  普通指令执行：从哪个类型获取的方法就执行哪个类型的方法
    //  虚指令执行：从哪个类型获取的方法就执行哪个类型的方法
    // 执行的方法是虚方法
    //  普通指令执行：就像执行普通方法一样
    //  虚指令执行：执行对象实例真实类型的方法

    // 普通指令调用虚方法
    public static MethodBuilder DefineMethod_Call1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Call1", typeof(string), [typeof(string)]);

        // Test1 v_test;
        LocalBuilder v_test = il.DeclareLocal(typeof(Test1));
        // v_test = new Test2();
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(v_test);
        // return v_test.AddSuffix(arg0);
        il.LoadLocal(v_test);
        il.LoadArg(0);
        il.Call(typeof(Test1).GetMethod(nameof(Test1.AddSuffix), [typeof(string)]));
        il.Return();

        return methodBuilder;
    }
    // 普通指令调用普通方法
    public static MethodBuilder DefineMethod_Call2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Call2", typeof(string), [typeof(string)]);

        // Test1 v_test;
        LocalBuilder v_test = il.DeclareLocal(typeof(Test1));
        // v_test = new Test2();
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(v_test);
        // return v_test.AddSuffix(arg0);
        il.LoadLocal(v_test);
        il.LoadArg(0);
        il.Call(typeof(Test1).GetMethod(nameof(Test1.AddPrefix), [typeof(string)]));
        il.Return();

        return methodBuilder;
    }
    // 将方法标记为最后的方法(被标记的方法执行后必须返回)
    public static MethodBuilder DefineMethod_Call3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Call3", typeof(string), [typeof(string)]);

        // Test1 v_test;
        LocalBuilder v_test = il.DeclareLocal(typeof(Test1));
        // v_test = new Test2();
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(v_test);
        // return v_test.AddSuffix(arg0);
        il.LoadLocal(v_test);
        il.LoadArg(0);
        il.TailCall();
        il.Call(typeof(Test1).GetMethod(nameof(Test1.AddPrefix), [typeof(string)]));
        // 不能再执行其他指令
        //il.Pop();
        //il.LoadString("##a");
        il.Return();

        return methodBuilder;
    }

    // 调用虚方法指令调用虚方法
    public static MethodBuilder DefineMethod_CallVirtual1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("CallVirtual1", typeof(string), [typeof(string)]);

        // Test1 v_test;
        LocalBuilder v_test = il.DeclareLocal(typeof(Test1));
        // v_test = new Test2();
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(v_test);
        // return v_test.AddSuffix(arg0);
        il.LoadLocal(v_test);
        il.LoadArg(0);
        il.CallVirtual(typeof(Test1).GetMethod(nameof(Test1.AddSuffix), [typeof(string)]));
        il.Return();

        return methodBuilder;
    }
    // 调用虚方法指令调用普通方法
    public static MethodBuilder DefineMethod_CallVirtual2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("CallVirtual2", typeof(string), [typeof(string)]);

        // Test1 v_test;
        LocalBuilder v_test = il.DeclareLocal(typeof(Test1));
        // v_test = new Test2();
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(v_test);
        // return v_test.AddPrefix(arg0);
        il.LoadLocal(v_test);
        il.LoadArg(0);
        il.CallVirtual(typeof(Test1).GetMethod(nameof(Test1.AddPrefix), [typeof(string)]));
        il.Return();

        return methodBuilder;
    }
    // 调用虚方法指令调用虚方法，调用前进行约束
    public static MethodBuilder DefineMethod_CallVirtual3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("CallVirtual3", typeof(string), [typeof(object)]);

        // return arg0.ToString();
        il.LoadArgAddr(0);
        il.Constrained(typeof(object));
        il.CallVirtual(typeof(object).GetMethod(nameof(object.ToString), Type.EmptyTypes));
        il.Return();

        return methodBuilder;
    }

    // 跳转到其他方法
    public static MethodBuilder DefineMethod_Jmp1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Jmp1", typeof(int), [typeof(int), typeof(int)]);

        il.Jmp(typeof(Test1).GetMethod(nameof(Test1.Div)));

        return methodBuilder;
    }
    // 跳转到外部方法
    public static MethodBuilder DefineMethod_Jmp2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Jmp2", typeof(int), [typeof(int), typeof(string), typeof(string), typeof(int)]);

        il.Jmp(typeof(Test1).GetMethod(nameof(Test1.MsgBox)));

        return methodBuilder;
    }
    #endregion

    #region 通过指针调用方法
    // 方法指针调用方法,实例方法
    public static MethodBuilder DefineMethod_Calli1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Calli1", typeof(string), [typeof(object), typeof(string)]);

        // ((delegate*<object, string, string>)__ldftn(Test1.AddPrefix))(P_0, P_1);
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadMethodPointer(typeof(Test1).GetMethod(nameof(Test1.AddPrefix)));
        il.Calli(CallingConventions.Standard, typeof(string), [typeof(object), typeof(string)]);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法，实例虚方法
    public static MethodBuilder DefineMethod_Calli2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Calli2", typeof(string), [typeof(object), typeof(string)]);

        // ((delegate*<object, string, string>)__ldvirtftn(Test1.AddSuffix))(P_0, P_1);
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadArg(0);
        il.LoadMethodPointer(typeof(Test1).GetMethod(nameof(Test1.AddSuffix)));
        il.Calli(CallingConventions.Standard, typeof(string), [typeof(object), typeof(string)]);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法，静态方法
    public static MethodBuilder DefineMethod_Calli3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Calli3", typeof(int), [typeof(int), typeof(int)]);

        // ((delegate*<int, int, int>)(&Test1.Div))(P_0, P_1);
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadMethodPointer(typeof(Test1).GetMethod(nameof(Test1.Div)));
        il.Calli(CallingConventions.Standard, typeof(int), [typeof(int), typeof(int)]);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法，外部方法
    public static MethodBuilder DefineMethod_Calli4()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Calli4", typeof(int), [typeof(string), typeof(string)]);

        // return ((delegate*<int, string, string, int, int>)(&Test1.MsgBox))(0, P_0, P_1, 1);
        il.LoadInt(0);
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadInt(1);
        il.LoadMethodPointer(typeof(Test1).GetMethod(nameof(Test1.MsgBox)));
        il.Calli(CallingConventions.Standard, typeof(int), [typeof(int), typeof(string), typeof(string), typeof(int)]);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 地址/指针赋值
    // 设置值到地址
    public static MethodBuilder DefineMethod_SetValueToAddr1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("SetValueToAddr1", typeof(int), [typeof(int)]);

        // int v1;
        LocalBuilder v1 = il.DeclareLocal(typeof(int));
        // v1 = 10;
        il.LoadLocalAddr((ushort)v1.LocalIndex);
        il.LoadArg(0);
        il.SetValueToAddr(typeof(int));
        // return v1;
        il.LoadLocal(v1);
        il.Return();

        return methodBuilder;
    }

    // 设置值到地址
    public static MethodBuilder DefineMethod_SetValueToAddr2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("SetValueToAddr2", typeof(object), [typeof(object)]);

        // int v1;
        LocalBuilder v1 = il.DeclareLocal(typeof(object));
        // v1 = 10;
        il.LoadLocalAddr((ushort)v1.LocalIndex);
        il.LoadArg(0);
        il.SetValueToAddr(typeof(object));
        // return v1;
        il.LoadLocal(v1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Try Catch Finally
    // Try_Catch1
    public static MethodBuilder DefineMethod_Try_Catch1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Try_Catch1", typeof(void), Type.EmptyTypes);

        // try{
        il.BeginExceptionBlock();
        // throw new Exception("Try_Catch1 测试 异常");
        il.LoadString("Try_Catch1 测试 异常");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // }catch (Exception){
        il.BeginCatchBlock(typeof(Exception));
        // throw;
        il.ReThrow();
        // }
        il.EndExceptionBlock();
        il.Return();

        return methodBuilder;
    }
    // Try_Catch2
    public static MethodBuilder DefineMethod_Try_Catch2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Try_Catch2", typeof(string), [typeof(bool)]);

        // 定义标签
        Label l_tryEnd = il.DefineLabel();

        // string v_result;
        LocalBuilder v_result = il.DeclareLocal(typeof(string));
        // try{
        il.BeginExceptionBlock();
        // v_result = "没有异常";
        il.LoadString("没有异常");
        il.SetLocal(v_result);
        // if(!arg0) goto end;
        il.LoadArg(0);
        il.GotoIfFalse(l_tryEnd);
        // throw new Exception("Try_Catch2 测试 异常");
        il.LoadString("Try_Catch2测试异常");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // end:
        il.MarkLabel(l_tryEnd);
        // }catch (Exception e){
        il.BeginCatchBlock(typeof(Exception));
        // v_result = e.Message;
        il.Call(typeof(Exception).GetProperty("Message").GetMethod);
        il.SetLocal(v_result);
        // }
        il.EndExceptionBlock();
        // return v_result;
        il.LoadLocal(v_result);
        il.Return();

        return methodBuilder;
    }
    // Try_Catch3 不判断类型的异常捕捉
    public static MethodBuilder DefineMethod_Try_Catch3()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Try_Catch3", typeof(string), Type.EmptyTypes);

        // string v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(string));
        // try{
        il.BeginExceptionBlock();
        // throw new Exception();
        il.NewObject(typeof(Exception).GetConstructor(Type.EmptyTypes));
        il.Throw();
        // }catch{
        il.BeginCatchBlock(typeof(object));
        // v_res = "进入了一个不判断类型的异常捕捉";
        il.LoadString("进入了一个不判断类型的异常捕捉");
        il.SetLocal(v_res);
        // }
        il.EndExceptionBlock();
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }
    // Try_Catch4 有过滤的异常捕捉
    public static MethodBuilder DefineMethod_Try_Catch4()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Try_Catch4", typeof(string), [typeof(int)]);

        // 声明标签
        Label l_try1 = il.DefineLabel();
        Label l_tryEnd = il.DefineLabel();
        Label l_catch1Start = il.DefineLabel();
        Label l_catch1End = il.DefineLabel();

        // string v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(string));
        // try{
        il.BeginExceptionBlock();
        // v1 = "没有异常";
        il.LoadString("没有异常");
        il.SetLocal(v_res);
        // if(arg0 == 0) goto tryEnd;
        il.LoadArg(0);
        il.LoadInt(0);
        il.GotoIfEqual(l_tryEnd);
        // if(arg0 > 0) goto try1;
        il.LoadArg(0);
        il.LoadInt(0);
        il.GotoIfGreater(l_try1);
        // throw new Exception("123");
        il.LoadString("123");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // try1:
        il.MarkLabel(l_try1);
        // throw new Exception("456");
        il.LoadString("456");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // tryEnd:
        il.MarkLabel(l_tryEnd);
        // } catch (Exception ex) 
        il.BeginExceptFilterBlock();
        il.As(typeof(Exception));
        il.Copy();
        il.GotoIfTrue(l_catch1Start);
        // 如果异常类型检查未通过
        il.Pop();
        il.LoadInt(0);
        il.Goto(l_catch1End);
        // when (ex.Message == "123")
        il.MarkLabel(l_catch1Start);
        il.Call(typeof(Exception).GetProperty("Message").GetMethod);
        il.LoadString("123");
        il.Call(typeof(string).GetMethod("op_Equality", [typeof(string), typeof(string)]));
        il.MarkLabel(l_catch1End);
        // {
        il.BeginCatchBlock(null);
        // v1 = ex.Message;
        il.Call(typeof(Exception).GetProperty("Message").GetMethod);
        il.SetLocal(v_res);
        // } catch(Exception ex) {
        il.BeginCatchBlock(typeof(Exception));
        // v1 = ex.Message + "----";
        il.Call(typeof(Exception).GetProperty("Message").GetMethod);
        il.LoadString("----");
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string)]));
        il.SetLocal(v_res);
        // }
        il.EndExceptionBlock();
        // return v1;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // Try_Catch_Finally1
    public static MethodBuilder DefineMethod_Try_Catch_Finally1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Try_Catch_Finally1", typeof(string), Type.EmptyTypes);

        // string v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(string));
        // try{
        il.BeginExceptionBlock();
        // v_res = "进入Try";
        il.LoadString("进入Try");
        il.SetLocal(v_res);
        // throw new Exception("Try_Catch_Finally1测试异常");
        il.LoadString("Try_Catch_Finally1测试异常");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // }catch (Exception ex){
        il.BeginCatchBlock(typeof(Exception));
        // v_res = v_res + " 进入Catch";
        il.LoadLocal(v_res);
        il.LoadString(" 进入Catch");
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string)]));
        il.SetLocal(v_res);
        //}finally{
        il.BeginFinallyBlock();
        // v_res = v_res + " 进入Finally";
        il.LoadLocal(v_res);
        il.LoadString(" 进入Finally");
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string)]));
        il.SetLocal(v_res);
        //}
        il.EndExceptionBlock();
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 关键字
    // szieof
    public static MethodBuilder DefineMethod_SizeOf1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("SizeOf1", typeof(int[]), Type.EmptyTypes);

        // int[] v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(int[]));
        // v_res = new int[8];
        il.LoadInt(8);
        il.NewArray(typeof(int));
        il.SetLocal(v_res);
        // v_res[0] = sizeof(byte);
        il.LoadLocal(v_res);
        il.LoadInt(0);
        il.SizeOf(typeof(byte));
        il.SetArray(typeof(int));
        // v_res[1] = sizeof(short);
        il.LoadLocal(v_res);
        il.LoadInt(1);
        il.SizeOf(typeof(short));
        il.SetArray(typeof(int));
        // v_res[2] = sizeof(int);
        il.LoadLocal(v_res);
        il.LoadInt(2);
        il.SizeOf(typeof(int));
        il.SetArray(typeof(int));
        // v_res[3] = sizeof(long);
        il.LoadLocal(v_res);
        il.LoadInt(3);
        il.SizeOf(typeof(long));
        il.SetArray(typeof(int));
        // v_res[4] = sizeof(double);
        il.LoadLocal(v_res);
        il.LoadInt(4);
        il.SizeOf(typeof(double));
        il.SetArray(typeof(int));
        // v_res[5] = sizeof(decimal);
        il.LoadLocal(v_res);
        il.LoadInt(5);
        il.SizeOf(typeof(decimal));
        il.SetArray(typeof(int));
        // v_res[6] = sizeof(string);
        il.LoadLocal(v_res);
        il.LoadInt(6);
        il.SizeOf(typeof(string));
        il.SetArray(typeof(int));
        // v_res[7] = sizeof(object);
        il.LoadLocal(v_res);
        il.LoadInt(7);
        il.SizeOf(typeof(object));
        il.SetArray(typeof(int));
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // typeof
    public static MethodBuilder DefineMethod_TypeOf1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("TypeOf1", typeof(Type), Type.EmptyTypes);

        // Type v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(Type));
        // v_res = typeof(string);
        il.LoadRuntimeHandle(typeof(string));
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), [typeof(RuntimeTypeHandle)]));
        il.SetLocal(v_res);
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // default
    public static MethodBuilder DefineMethod_Default1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Default1", typeof(int), Type.EmptyTypes);

        // int v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(int));
        // v_res = default(int);
        il.LoadLocalAddr((ushort)v_res.LocalIndex);
        il.Initobj(typeof(int));
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 字段
    // 声明设置字段
    public static MethodBuilder DefineMethod_Field1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Field1", typeof(int), Type.EmptyTypes);

        // private static int _num;
        _field_num = _typeBuilder.DefineField("_num", typeof(int), FieldAttributes.Private | FieldAttributes.Static);

        // _num = (int)(10.0 / 3.0);
        il.LoadFloat(10);
        il.LoadFloat(3);
        il.MathDiv();
        il.ConvertInteger(typeof(int));
        il.SetField(_field_num);
        // return _num;
        il.LoadField(_field_num);
        il.Return();

        return methodBuilder;
    }

    // 易失字段，表示指定地址是易失的,当多线程修改同时操作值时不会排列对该值的读取和写入
    public static MethodBuilder DefineMethod_Volatile1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Volatile1", typeof(int), Type.EmptyTypes);

        // private static volatile int _num2;
        _field_num2 = _typeBuilder.DefineField("_num2", typeof(int), [typeof(IsVolatile)], [], FieldAttributes.Private | FieldAttributes.Static);

        // _num2 = 10;
        il.LoadInt(10);
        il.Volatile();
        il.SetField(_field_num);
        // return _num;
        il.Volatile();
        il.LoadField(_field_num);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 参数
    // SetArg1
    public static MethodBuilder DefineMethod_SetArg1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("SetArg1", typeof(int), [typeof(int)]);

        // arg0 = 10;
        il.LoadInt(10);
        il.SetArg(0);
        // return arg0;
        il.LoadArg(0);
        il.Return();

        return methodBuilder;
    }

    // ref 参数
    public static MethodBuilder DefineMethod_RefArg0()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("RefArg1", typeof(int), [typeof(int).MakeByRefType()]);

        // arg0 = 10;
        il.LoadArg(0);
        il.LoadInt(10);
        il.SetValueToAddr(typeof(int));
        // return agr0;
        il.LoadArg(0);
        il.LoadAddrValue(typeof(int));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 值类型引用化
    // 值类型引用化
    public static MethodBuilder DefineMethod_Mkrefany1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Mkrefany1", typeof(User), Type.EmptyTypes);

        // User v_user;
        LocalBuilder v_user = il.DeclareLocal(typeof(User));
        // v_user = new User("A");
        il.LoadString("A");
        il.NewObject(typeof(User).GetConstructor([typeof(string)]));
        il.SetLocal(v_user);

        // TypedReference v_refUser;
        LocalBuilder v_refUser = il.DeclareLocal(typeof(TypedReference));
        // v_refUser = __makeref(v_user);
        il.LoadLocalAddr((ushort)v_user.LocalIndex);
        il.Mkrefany(typeof(User));
        il.SetLocal(v_refUser);

        // FieldInfo v_fieldInfo;
        LocalBuilder v_fieldInfo = il.DeclareLocal(typeof(FieldInfo));
        // v_fieldInfo = typeof(User).GetField("_name",BindingFlags.NonPublic | BindingFlags.Instance);
        il.LoadRuntimeHandle(typeof(User));
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), [typeof(RuntimeTypeHandle)]));
        il.LoadString("_name");
        il.LoadInt((int)(BindingFlags.NonPublic | BindingFlags.Instance));
        il.CallVirtual(typeof(Type).GetMethod(nameof(Type.GetField), [typeof(string), typeof(BindingFlags)]));
        il.SetLocal(v_fieldInfo);

        // v_fieldInfo.SetValueDirect(v_refUser, "B");
        il.LoadLocal(v_fieldInfo);
        il.LoadLocal(v_refUser);
        il.LoadString("B");
        il.CallVirtual(typeof(FieldInfo).GetMethod(nameof(FieldInfo.SetValueDirect), [typeof(TypedReference), typeof(object)]));

        // return v_user;
        il.LoadLocal(v_user);
        il.Return();

        return methodBuilder;
    }

    // 值类型不引用化修改值
    public static MethodBuilder DefineMethod_Mkrefany2()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Mkrefany2", typeof(User), Type.EmptyTypes);

        // User v_user;
        LocalBuilder v_user = il.DeclareLocal(typeof(User));
        // v_user = new User("A");
        il.LoadString("A");
        il.NewObject(typeof(User).GetConstructor([typeof(string)]));
        il.SetLocal(v_user);

        // FieldInfo v_fieldInfo;
        LocalBuilder v_fieldInfo = il.DeclareLocal(typeof(FieldInfo));
        // v_fieldInfo = typeof(User).GetField("_name",BindingFlags.NonPublic | BindingFlags.Instance);
        il.LoadRuntimeHandle(typeof(User));
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), [typeof(RuntimeTypeHandle)]));
        il.LoadString("_name");
        il.LoadInt((int)(BindingFlags.NonPublic | BindingFlags.Instance));
        il.CallVirtual(typeof(Type).GetMethod(nameof(Type.GetField), [typeof(string), typeof(BindingFlags)]));
        il.SetLocal(v_fieldInfo);

        // v_fieldInfo.SetValue(v_refUser, "B");
        il.LoadLocal(v_fieldInfo);
        il.LoadLocal(v_user);
        il.Box(typeof(User));
        il.LoadString("B");
        il.CallVirtual(typeof(FieldInfo).GetMethod(nameof(FieldInfo.SetValue), [typeof(object), typeof(object)]));

        // return v_user;
        il.LoadLocal(v_user);
        il.Return();

        return methodBuilder;
    }

    // 获取嵌套在引用化类型中的值的类型
    public static MethodBuilder DefineMethod_Refanytype1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Refanytype1", typeof(Type), Type.EmptyTypes);

        // User v_user;
        LocalBuilder v_user = il.DeclareLocal(typeof(User));
        // v_user = new User("A");
        il.LoadString("A");
        il.NewObject(typeof(User).GetConstructor([typeof(string)]));
        il.SetLocal(v_user);

        // TypedReference v_refUser;
        LocalBuilder v_refUser = il.DeclareLocal(typeof(TypedReference));
        // v_refUser = __makeref(v_user);
        il.LoadLocalAddr((ushort)v_user.LocalIndex);
        il.Mkrefany(typeof(User));
        il.SetLocal(v_refUser);

        // return __reftype(v_refUser);
        il.LoadLocal(v_refUser);
        il.Refanytype();
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), [typeof(RuntimeTypeHandle)]));
        il.Return();

        return methodBuilder;
    }

    // 获取嵌套在引用化类型中的值的地址
    public static MethodBuilder DefineMethod_Refanyval1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Refanyval1", typeof(User), Type.EmptyTypes);

        // User v_user;
        LocalBuilder v_user = il.DeclareLocal(typeof(User));
        // v_user = new User("A");
        il.LoadString("A");
        il.NewObject(typeof(User).GetConstructor([typeof(string)]));
        il.SetLocal(v_user);

        // TypedReference v_refUser;
        LocalBuilder v_refUser = il.DeclareLocal(typeof(TypedReference));
        // v_refUser = __makeref(v_user);
        il.LoadLocalAddr((ushort)v_user.LocalIndex);
        il.Mkrefany(typeof(User));
        il.SetLocal(v_refUser);

        // return __refvalue(v_refUser, typeof(User));
        il.LoadLocal(v_refUser);
        il.Refanyval(typeof(User));
        il.LoadAddrValue(typeof(User));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 其他
    // 从地址复制值到地址
    public static MethodBuilder DefineMethod_CopyAddrValueToAddr1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("CopyAddrValueToAddr1", typeof(Test2), [typeof(Test2)]);

        // Test2 v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(Test2));
        // v_res = arg0;
        il.LoadLocalAddr((ushort)v_res.LocalIndex);
        il.LoadArgAddr(0);
        il.SizeOf(typeof(Test2));
        il.CopyAddrValueToAddr();
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // 将对象地址的值复制到目标对象地址
    public static MethodBuilder DefineMethod_Cpobj1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Cpobj1", typeof(Test2), [typeof(Test2)]);

        // Test2 v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(Test2));
        // v_res = arg0;
        il.LoadLocalAddr((ushort)v_res.LocalIndex);
        il.LoadArgAddr(0);
        il.Cpobj(typeof(Test2));
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // 动态分配空间，设置默认值
    public static MethodBuilder DefineMethod_Localloc_Initblk1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Localloc_Initblk1", typeof(uint), Type.EmptyTypes);

        // byte* v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(byte*));
        // intPtr = stackalloc byte[sizeof(uint)]; // 分配4个字节的空间
        il.SizeOf(typeof(uint));
        il.Localloc();
        il.SetLocal(v_res);
        // 设置3个字节的默认值
        // System.Runtime.CompilerServices.Unsafe.InitBlock(v_res, 255, sizeof(uint) - sizeof(byte)); // 初始化后为 00000000 11111111 11111111 11111111
        il.LoadLocal(v_res);
        il.LoadInt(byte.MaxValue);
        il.SizeOf(typeof(uint));
        il.SizeOf(typeof(byte));
        il.MathSub();
        il.Initblk();
        // return *(uint*)v_res;
        il.LoadLocal(v_res);
        il.LoadAddrValue(typeof(uint));
        il.Return();

        return methodBuilder;
    }

    // 对其，ref 参数，拷贝数组
    public static MethodBuilder DefineMethod_Unaligned1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Unaligned1", typeof(void), [typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(int)]);

        // System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(ref P_1, ref P_0, P_2);
        il.LoadArg(1);
        il.LoadArg(0);
        il.LoadArg(2);
        // 因为字节的大小是 1 所以要执行对其
        // sizeof(T) % 8 不是0的都应该执行对其 see https://stackoverflow.com/questions/24122973/what-should-i-pin-when-working-on-arrays/47128947#47128947
        il.Unaligned(sizeof(byte));
        il.CopyAddrValueToAddr();
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region __arglist 参数
    // 当前块创建的方法都不能通过反射调用，因为 __arglist 参数无法通过反射调用，所以写不了测试
    // 如需测试，1.引用生成的程序集  2.调用 EmitOpCodesVerify.Arglist1(__arglist(1, 2, "aa", "d", 4.6, new { A = 1 }));

    // __arglist 参数
    public static MethodBuilder DefineMethodArglist1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Arglist1", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.VarArgs, typeof(object[]), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 声明标签
        Label l_calCount = il.DefineLabel();
        Label l_end = il.DefineLabel();

        // ArgIterator v_args;
        LocalBuilder v_args = il.DeclareLocal(typeof(ArgIterator));
        // v_args = new ArgIterator(__arglist);
        il.LoadVarArgs();
        il.NewObject(typeof(ArgIterator).GetConstructor([typeof(RuntimeArgumentHandle)]));
        il.SetLocal(v_args);

        // object[] v_res;
        LocalBuilder v_res = il.DeclareLocal(typeof(object[]));
        // v_res = new object[v_args.GetRemainingCount()];
        il.LoadLocalAddr((ushort)v_args.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod(nameof(ArgIterator.GetRemainingCount)));
        il.NewArray(typeof(object));
        il.SetLocal(v_res);

        // int i;
        LocalBuilder v_i = il.DeclareLocal(typeof(int));
        // i = 0;
        il.LoadInt(0);
        il.SetLocal(v_i);

        // calCount:
        il.MarkLabel(l_calCount);
        // if(v_args.GetRemainingCount() == 0) goto end;
        il.LoadLocalAddr((ushort)v_args.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod(nameof(ArgIterator.GetRemainingCount)));
        il.LoadInt(0);
        il.GotoIfEqual(l_end);

        // v_res[i] = TypedReference.ToObject(argIterator.GetNextArg());
        il.LoadLocal(localBuilder: v_res);
        il.LoadLocal(v_i);
        il.LoadLocalAddr((ushort)v_args.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod(nameof(ArgIterator.GetNextArg), Type.EmptyTypes));
        il.Call(typeof(TypedReference).GetMethod(nameof(TypedReference.ToObject)));
        il.SetArray(typeof(object));
        // v_i++;
        il.LoadLocal(v_i);
        il.LoadInt(1);
        il.MathAdd();
        il.SetLocal(v_i);
        // goto CalCount;
        il.Goto(l_calCount);

        // end:
        il.MarkLabel(l_end);
        // return v_res;
        il.LoadLocal(v_res);
        il.Return();

        return methodBuilder;
    }

    // 调用 __arglist 参数 方法
    public static MethodBuilder DefineMethod_ArglistInvoke1()
    {
        // 声明带有__arglist 参数的方法
        MethodBuilder varargMethod = DefineMethodArglist1();

        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ArglistInvoke1", typeof(object[]), [typeof(object), typeof(object), typeof(object), typeof(object)]);

        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadArg(2);
        il.LoadArg(3);
        il.Call(varargMethod, typeof(object), typeof(object), typeof(object), typeof(object));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 没搞懂具体用法的指令
    // 检查值是否是正常数字，(这个不知道什么原因，抛出的异常和文档不一致)
    public static MethodBuilder DefineMethod_Ckfinite1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("Ckfinite1", typeof(double), [typeof(double)]);

        il.LoadArg(0);
        il.Ckfinite();
        il.Return();

        return methodBuilder;
    }

    // 只读
    public static MethodBuilder DefineMethod_ReadOnly1()
    {
        (MethodBuilder methodBuilder, ILGenerator il) = CreateMethod_PublicStatic("ReadOnly1", typeof(Test1[]), [typeof(Test1[])]);

        // 这里我理解的是只读的数组，不能修改数组的值，但是这里可以修改数组的值，不知道是不是我理解错了
        // 使用了两种方式去修改使用 Readonly 的值都成功了

        // arg0[0] = new Test2();
        il.LoadArg(0);
        il.LoadInt(0);
        il.Readonly();
        il.LoadArrayIndexAddr(typeof(Test1));
        il.NewObject(typeof(Test2).GetConstructor(Type.EmptyTypes));
        il.SetValueToAddr(typeof(Test1));
        // arg0[0].str = "aa";
        il.LoadArg(0);
        il.LoadInt(0);
        il.Readonly();
        il.LoadArrayIndexAddr(typeof(Test1));
        il.LoadAddrValue(typeof(Test1));
        il.LoadString("aa");
        il.SetField(typeof(Test1).GetField("str"));
        // return arg0;
        il.LoadArg(0);
        il.Return();

        return methodBuilder;
    }
    #endregion


    #region 帮助方法
    /// <summary>
    /// 定义公开静态方法
    /// </summary>
    /// <param name="_typeBuilder">类型构建器</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="returnType">返回类型</param>
    /// <param name="parameterTypes">参数类型</param>
    /// <returns>方法构建器，il 中间语言指令</returns>
    private static (MethodBuilder, ILGenerator) CreateMethod_PublicStatic(string methodName, Type returnType, Type[] parameterTypes)
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod(methodName, MethodAttributes.Public | MethodAttributes.Static, returnType, parameterTypes);
        ILGenerator il = methodBuilder.GetILGenerator();
        return (methodBuilder, il);
    }
    #endregion
}

public class Test1
{
    private readonly string _str = "##";

    public string str = "##";

    public int Num { get; set; } = 1;

    public string Txt { get; set; } = "Test1";

    /// <summary>
    /// 添加前缀
    /// </summary>
    public string AddPrefix(string str) => $"{_str}{str}";

    /// <summary>
    /// 添加后缀
    /// </summary>
    public virtual string AddSuffix(string str) => $"{str}{_str}";

    public static int Div(int a, int b) => a / b;

    /// <summary>
    /// 这是一个弹出消息框的外部方法
    /// </summary>
    [DllImport("user32.dll", EntryPoint = "MessageBoxA")]
    public static extern int MsgBox(int hWnd, string msg, string caption, int type);
}
public class Test2 : Test1
{
    private readonly string _str = "%%";

    public new string str = "%%";

    public new string AddPrefix(string str) => $"{_str}{str}";

    public override string AddSuffix(string str) => $"{str}{_str}";
}


public struct User(string user)
{
    private string _name = user;

    public readonly string Name => _name;
}



/// <summary>
/// 这是以下代码动态创建的类
/// </summary>
