using System.Runtime.InteropServices;

namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// 这是以下代码动态创建的类
/// </summary>
public class EmitOpCodesVerify
{
    public static int field1;

    private static volatile int _volatile1;

    public static int[] Add1(int P_0, int P_1)
    {
        int[] array = new int[3];
        int num = P_0 + P_1;
        int num2 = checked(P_0 + P_1);
        int num3 = (int)checked(unchecked((uint)P_0) + unchecked((uint)P_1));
        array[0] = num;
        array[1] = num2;
        array[2] = num3;
        return array;
    }

    public static int[] Add2(int P_0, int P_1)
    {
        return new int[3]
        {
            P_0 + P_1,
            checked(P_0 + P_1),
            (int)checked(unchecked((uint)P_0) + unchecked((uint)P_1))
        };
    }

    public static int[] Sub1(int P_0, int P_1)
    {
        return new int[3]
        {
            P_0 - P_1,
            checked(P_0 - P_1),
            (int)checked(unchecked((uint)P_0) - unchecked((uint)P_1))
        };
    }

    public static int[] Mul1(int P_0, int P_1)
    {
        return new int[3]
        {
            P_0 * P_1,
            checked(P_0 * P_1),
            (int)checked(unchecked((uint)P_0) * unchecked((uint)P_1))
        };
    }

    public static int[] Div1(int P_0, int P_1)
    {
        return new int[2]
        {
            P_0 / P_1,
            (int)((uint)P_0 / (uint)P_1)
        };
    }

    public static int[] Rem1(int P_0, int P_1)
    {
        return new int[2]
        {
            P_0 % P_1,
            (int)((uint)P_0 % (uint)P_1)
        };
    }

    public static int Neg1(int P_0)
    {
        return -P_0;
    }

    public static int And1(int P_0, int P_1)
    {
        return P_0 & P_1;
    }

    public static int Or1(int P_0, int P_1)
    {
        return P_0 | P_1;
    }

    public static int Xor1(int P_0, int P_1)
    {
        return P_0 ^ P_1;
    }

    public static int Not1(int P_0)
    {
        return ~P_0;
    }

    public static int ShiftLeft1(int P_0, int P_1)
    {
        return P_0 << P_1;
    }

    public static int[] ShiftRight1(int P_0, int P_1)
    {
        return new int[2]
        {
            P_0 >> P_1,
            (int)((uint)P_0 >> P_1)
        };
    }

    public static int Equal1(int P_0, int P_1)
    {
        return (P_0 == P_1) ? 1 : 0;
    }

    public static bool Equal2(int P_0, int P_1)
    {
        return P_0 == P_1;
    }

    public static int Greater1(int P_0, int P_1)
    {
        return (P_0 > P_1) ? 1 : 0;
    }

    public static int Less1(int P_0, int P_1)
    {
        return (P_0 < P_1) ? 1 : 0;
    }

    public static string StringAdd1(string P_0, string P_1)
    {
        return P_0 + P_1;
    }

    public static object[] Arglist1(__arglist)
    {
        int num = 0;
        ArgIterator argIterator = new(__arglist);
        object[] array = new object[argIterator.GetRemainingCount()];
        while (argIterator.GetRemainingCount() > 0)
        {
            array[num] = TypedReference.ToObject(argIterator.GetNextArg());
            num++;
        }
        return array;
    }

    public static object[] Arglist_Invoke1(object P_0, object P_1, object P_2, object P_3)
    {
        return Arglist1(__arglist(P_0, P_1, P_2, P_3));
    }

    public static object Box1(int P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return P_0;
    }

    public static int UnBox1(object P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return (int)P_0;
    }

    public static int UnBox2(object P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return (int)P_0;
    }

    public static int ConvertInteger1(float P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return (int)P_0;
    }

    public static Type AS1(object P_0)
    {
        return P_0 as Type;
    }

    public static string CallVirtual1(string P_0)
    {
        EmitTest1 emitTest = new EmitTest2();
        return emitTest.T1(P_0);
    }

    public static string CallVirtual2(string P_0)
    {
        EmitTest2 emitTest = new();
        return emitTest.T1(P_0);
    }

    public static string CallVirtual3(string P_0)
    {
        EmitTest1 emitTest = new EmitTest2();
        return emitTest.T1(P_0);
    }

    public static string CallVirtual4(string P_0)
    {
        EmitTest1 emitTest = new EmitTest2();
        return emitTest.T1(P_0);
    }

    //public unsafe static string CallVirtual5(string P_0)
    //{
    //    //IL_0014: Expected O, but got Ref
    //    EmitTest1 emitTest = new EmitTest2();
    //    return ((EmitTest1)(&emitTest)).T1(P_0);
    //}

    public static object Array1()
    {
        int[] array = new int[3] { 3, 5, 8 };
        return array[2];
    }

    public static object Array2()
    {
        string[] array = new string[3] { "3", "5", "8" };
        return array[2];
    }

    public static object Array3()
    {
        string[] array = new string[3] { "3", "5", "8" };
        return array[2];
    }

    public static object Array4()
    {
        string[] array = new string[99];
        return array.Length;
    }

    public static int SetValueToAddr1()
    {
        return 10;
    }

    public static object SetValueToAddr2()
    {
        return new object();
    }

    public static unsafe int Calli_Ldftn1()
    {
        return ((delegate* unmanaged[Stdcall]<int>)(delegate*<int>)(&EmitTest2.T2))();
    }

    //public unsafe static int Calli_Ldftn2()
    //{
    //    return ((delegate* unmanaged[Thiscall]<EmitTest2, int>)__ldftn(EmitTest2.T3))(new EmitTest2());
    //}

    //public unsafe static int Calli_Ldftn3()
    //{
    //    EmitTest2 emitTest = new EmitTest2();
    //    return ((delegate* unmanaged[Thiscall]<EmitTest2, int>)__ldvirtftn(EmitTest2.T4))(emitTest);
    //}

    //public unsafe static int Calli_Ldftn4()
    //{
    //    EmitTest2 emitTest = new EmitTest2();
    //    return ((delegate* unmanaged[Thiscall]<EmitTest2, int>)__ldvirtftn(EmitTest2.T4))(emitTest);
    //}

    public static void Try_Catch1()
    {
        //Discarded unreachable code: IL_000b, IL_0012, IL_0017
        //Error decoding local variables: Signature type sequence must have at least one element.
        try
        {
            throw new Exception("Try_Catch1 测试 异常");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string Try_Catch2()
    {
        //Discarded unreachable code: IL_000b, IL_0016, IL_001b
        //Error decoding local variables: Signature type sequence must have at least one element.
        try
        {
            throw new Exception("Try_Catch2 测试 异常");
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string Try_Catch3()
    {
        //Discarded unreachable code: IL_000b, IL_0016, IL_0017
        //Error decoding local variables: Signature type sequence must have at least one element.
        try
        {
            throw new Exception("Try_Catch3 测试 异常");
        }
        catch
        {
            //try-fault
            return "进入了一个不判断类型的异常捕捉";
            throw;
        }
    }

    public static string Try_Catch4()
    {
        //Discarded unreachable code: IL_001a, IL_001c, IL_0058, IL_005d
        string text = "1";
        try
        {
            text = "2";
            throw new Exception("123");
        }
        catch (Exception ex) when (ex.Message == "123")
        {
            return ex.Message;
        }
    }

    public static string Try_Catch4_Extension()
    {
        //Discarded unreachable code: IL_0017, IL_0019, IL_0044, IL_0049
        string text = "1";
        try
        {
            text = "2";
            throw new Exception("123");
        }
        catch (Exception ex) when (ex.Message == "123")
        {
            return ex.Message;
        }
    }

    public static int Try_Catch5(int P_0, int P_1)
    {
        //Discarded unreachable code: IL_002b, IL_0047
        OverflowException ex = default;
        try
        {
            if (P_0 <= 10 && P_1 <= 10)
            {
                return (int)checked(unchecked((uint)P_0) + unchecked((uint)P_1));
            }
            ex = new OverflowException("Cannot accept values over 10 for add.");
            throw new OverflowException();
        }
        catch when (((Func<bool>)delegate
        {
            // Could not convert BlockContainer to single expression
            Console.WriteLine("Except filter block called.");
            return true;
        }).Invoke())
        {
            return P_0 - P_1;
        }
        catch (OverflowException)
        {
            Console.WriteLine("{0}", ex.ToString());
            return -1;
        }
        finally
        {
            Console.WriteLine("Finally block called.");
        }
    }

    public static string Try_Catch_Finally1()
    {
        //Discarded unreachable code: IL_0017, IL_0050
        string text = default;
        try
        {
            text += "进入了 Try      ";
            throw new Exception("Try_Catch_Finally1 测试 异常");
        }
        catch (Exception)
        {
            text += "触发了 Exception 类型异常      ";
        }
        //catch (ArgumentNullException)
        //{
        //    text += "触发了 ArgumentNullException 类型异常      ";
        //}
        finally
        {
            text += "进入了 Finally      ";
        }
        return text + "开始返回";
    }

    //public unsafe static int[] SizeOf1()
    //{
    //    //IL_001b: Expected O, but got I4
    //    //IL_0025: Expected O, but got I4
    //    //IL_002f: Expected O, but got I4
    //    //IL_0039: Expected O, but got I4
    //    //IL_0043: Expected O, but got I4
    //    //IL_004d: Expected O, but got I4
    //    //IL_0057: Expected O, but got I4
    //    return new int[8]
    //    {
    //        sizeof(byte),
    //        (int)(object)sizeof(short),
    //        (int)(object)sizeof(int),
    //        (int)(object)sizeof(long),
    //        (int)(object)sizeof(double),
    //        (int)(object)sizeof(decimal),
    //        (int)(object)sizeof(string),
    //        (int)(object)sizeof(object)
    //    };
    //}

    public static int SetField1()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        field1 = 3;
        return field1;
    }

    public static object Typeof1()
    {
        return typeof(string);
    }

    //public unsafe static object CopyAddrValueToAddr1(EmitTest2 P_0)
    //{
    //    EmitTest2 result = default(EmitTest2);
    //    // IL cpblk instruction
    //    System.Runtime.CompilerServices.Unsafe.CopyBlock(ref result, ref P_0, sizeof(EmitTest2));
    //    return result;
    //}

    //public unsafe static object Cpobj1(EmitTest2 P_0)
    //{
    //    EmitTest2 result = default(EmitTest2);
    //    *(int*)(&result) = *(int*)(&P_0);
    //    return result;
    //}

    //public unsafe static uint Localloc_Initblk1()
    //{
    //    //Error decoding local variables: Signature type sequence must have at least one element.
    //    byte* intPtr = stackalloc byte[sizeof(uint)];
    //    // IL initblk instruction
    //    System.Runtime.CompilerServices.Unsafe.InitBlock(intPtr, 255, sizeof(uint));
    //    return *(uint*)intPtr;
    //}

    public static int Initobj1()
    {
        return default;
    }

    public static MyStruct Mkrefany1()
    {
        MyStruct result = new("1");
        TypedReference obj = __makeref(result);
        FieldInfo field = typeof(MyStruct).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic);
        field.SetValueDirect(obj, "2");
        return result;
    }

    public static MyStruct Mkrefany2()
    {
        MyStruct myStruct = new("1");
        FieldInfo field = typeof(MyStruct).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic);
        field.SetValue(myStruct, "2");
        return myStruct;
    }

    public static Type Refanytype1()
    {
        MyStruct myStruct = new("1");
        TypedReference typedReference = __makeref(myStruct);
        return __reftype(typedReference);
    }

    public static MyStruct Refanyval1()
    {
        MyStruct myStruct = new("1");
        TypedReference typedReference = __makeref(myStruct);
        return __refvalue(typedReference, MyStruct);
    }

    public static int SetArg1(int P_0, int P_1)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        P_0 = 999;
        return P_0;
    }

    //public static void Unaligned1(ref byte P_0, ref byte P_1, int P_2)
    //{
    //    //Error decoding local variables: Signature type sequence must have at least one element.
    //    // IL cpblk instruction
    //    System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(ref P_1, ref P_0, P_2);
    //}

    public static int Volatile1()
    {
        _volatile1 = 1;
        return _volatile1;
    }

    //public static object Readonly1()
    //{
    //    EmitTest2[] array = new EmitTest2[10]
    //    {
    //        null,
    //        new EmitTest2(),
    //        null,
    //        null,
    //        null,
    //        null,
    //        null,
    //        null,
    //        null,
    //        null
    //    };
    //    System.Runtime.CompilerServices.Unsafe.WriteUnaligned(ref System.Runtime.CompilerServices.Unsafe.As<int, byte>(ref array[1].MyInt1), 5);
    //    return array;
    //}

    public static string Jmp1(int P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return EmitTest2.T5(P_0);
    }

    public static int Jmp2(int P_0, string P_1, string P_2, int P_3)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        return EmitTest1.MsgBox(P_0, P_1, P_2, P_3);
    }

    public static double Ckfinite1()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        double num = 10.0 / 3.0;
        /*OpCode not supported: Ckfinite*/
        ;
        return num;
    }
}



/// <summary>
/// Emit 指令验证
/// </summary>
public static class EmitOpCodesVerifyCreator
{
    // 类型构建器
    private static TypeBuilder _typeBuilder;


    public static Type DefineType_EmitOpCodesVerify1(this ModuleBuilder moduleBuilder)
    {
        // 定义类型构建器
        _typeBuilder = moduleBuilder.DefineType("EmitOpCodesVerify", TypeAttributes.Public);

        // +
        DefineMethod_Add1();
        DefineMethod_Add2();
        DefineMethod_Add3();
        // -
        DefineMethod_Sub1();
        // *
        DefineMethod_Mul1();
        // /
        DefineMethod_Div1();
        // %
        DefineMethod_Rem1();
        // -arg
        DefineMethod_Neg1();

        // &
        DefineMethod_And1();
        // |
        DefineMethod_Or1();
        // ^
        DefineMethod_Xor1();
        // ~arg
        DefineMethod_Not1();
        // <<
        DefineMethod_ShiftLeft1();
        // >>
        DefineMethod_ShiftRight1();

        // ==
        DefineMethod_Equal1();
        // >
        DefineMethod_Greater1();
        // <
        DefineMethod_Less1();

        // 字符串拼接
        DefineMethod_StringAdd1();


        // ** 特殊
        // 定义一个执行可变参数的方法，里面包含定义一个有可变参数的方法
        // Arglist 个数不定，类型不定参数
        DefineMethod_Arglist_Invoke1();


        // ** 类型转换
        // (object)int
        DefineMethod_Box1();
        // (int)obj
        DefineMethod_UnBox1();
        // (int)obj
        DefineMethod_UnBox2();
        // float to int
        DefineMethod_ConvertInteger1();
        // as 
        DefineMethod_As1();



        // ** 调用方法
        // 普通指令调用虚方法
        DefineMethod_CallVirtual1();
        // 普通指令调用虚方法
        DefineMethod_CallVirtual2();
        // 使用调用虚方法的指令调用虚方法
        DefineMethod_CallVirtual3();
        // 尾方法
        DefineMethod_CallVirtual4();
        // 使用调用虚方法的指令调用虚方法 调用前进行约束
        DefineMethod_CallVirtual5();


        // ** 数组
        // 获取数组元素
        DefineMethod_Array1();
        // 获取数组元素
        DefineMethod_Array2();
        // 返回数组中某个索引的值的地址的值
        DefineMethod_Array3();
        // 返回数组长度
        DefineMethod_Array4();



        // ** 地址
        // SetValueToAddr
        DefineMethod_SetValueToAddr1();
        DefineMethod_SetValueToAddr2();


        // ** 方法指针调用方法
        // 方法指针调用方法1
        DefineMethod_Calli_Ldftn1();
        // 方法指针调用方法2
        DefineMethod_Calli_Ldftn2();
        // 方法指针调用方法3(虚方法)
        DefineMethod_Calli_Ldftn3();
        // 方法指针调用方法3(虚方法)
        DefineMethod_Calli_Ldftn4();



        // ** Try Catch Finally
        // Try_Catch1
        DefineMethod_Try_Catch1();
        // Try_Catch2
        DefineMethod_Try_Catch2();
        // Try_Catch3
        DefineMethod_Try_Catch3();
        // Try_Catch4
        DefineMethod_Try_Catch4();
        // 无法命中筛选异常
        DefineMethod_Try_Catch_Finally2();
        // Try_Catch_Finally1
        DefineMethod_Try_Catch_Finally1();


        // sizeof
        DefineMethod_SizeOf1();


        // set field
        DefineMethod_SetField1();


        // typeof
        DefineMethod_Typeof1();



        // CopyAddrValueToAddr1
        DefineMethod_CopyAddrValueToAddr1();


        // Cpobj1
        DefineMethod_Cpobj1();



        // Localloc_Initblk
        DefineMethod_Localloc_Initblk1();


        // Initobj
        DefineMethod_Initobj1();


        // Mkrefany1（值类型引用化）
        DefineMethod_Mkrefany1();
        // Mkrefany1（值类型没有引用化）
        DefineMethod_Mkrefany2();


        // Refanytype1 （获取嵌套在引用化类型中的值的类型）
        DefineMethod_Refanytype1();


        // Refanyval1 （获取嵌套在引用化类型中的值的地址）
        DefineMethod_Refanyval1();


        // 参数赋值
        DefineMethod_SetArg1();



        // 使用对其操作
        DefineMethod_Unaligned1();


        // Volatile1
        DefineMethod_Volatile1();


        // Readonly1
        // 该方法用于测试只读，不过只读没有用，设置了只读,还是可以设置值和修改值
        DefineMethod_Readonly1();


        // Jmp1
        DefineMethod_Jmp1();
        // Jmp2 外部方法
        DefineMethod_Jmp2();

        // Ckfinite1
        DefineMethod_Ckfinite1();


        return _typeBuilder.CreateType();
    }

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





    #region 特殊
    // 使用 Arglist 示例
    public static MethodBuilder DefineMethod_Arglist1()
    {
        // 需要定义 CallingConventions.VarArgs 就会在方法参数默认添加一个 __arglist 参数
        // 该方法不能使用反射调用所以写不了测试,如需测试引用生成的程序集然后调用 EmitOpCodesVerify.Arglist1(__arglist(1, 2, "aa", "d", 4.6, new { A = 1 }));

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Arglist1", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.VarArgs, typeof(object[]), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 局部变量
        LocalBuilder lb_result = il.DeclareLocal(typeof(object[]));//结果
        LocalBuilder lb_currentIndex = il.DeclareLocal(typeof(int));//结果的当前索引
        LocalBuilder lb_varargs = il.DeclareLocal(typeof(ArgIterator));//可变参数

        // 结束
        Label returnL = il.DefineLabel();
        // 判断是否有可变参数
        Label haveValueL = il.DefineLabel();

        // lb_currentIndex = 0;
        il.LoadInt(0);
        il.SetLocal(lb_currentIndex);

        // lb_varargs = new ArgIterator(__arglist);
        il.LoadLocalAddr((ushort)lb_varargs.LocalIndex); // 因为构造 ArgIterator 类型返回的是地址，所以加载地址去接收
        il.LoadVarArgs();
        il.Call(typeof(ArgIterator).GetConstructor(new Type[] { typeof(RuntimeArgumentHandle) }));

        // lb_result = new object[lb_varargs.GetRemainingCount()];
        il.LoadLocalAddr((ushort)lb_varargs.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod("GetRemainingCount"));
        il.NewArray(typeof(object));
        il.SetLocal(lb_result);

        // if(lb_havearg = lb_varargs.GetRemainingCount() < 0) return lb_result;
        il.MarkLabel(haveValueL);
        il.LoadLocalAddr((ushort)lb_varargs.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod("GetRemainingCount"));
        il.LoadInt(0);
        il.CompareGreater();
        il.GotoIfFalse(returnL);

        // lb_result[lb_currentIndex] = lb_varargs.GetNextArg();
        il.Nop();
        il.LoadLocal(lb_result);
        il.LoadLocal(lb_currentIndex);
        il.LoadLocalAddr((ushort)lb_varargs.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod("GetNextArg", Type.EmptyTypes));
        il.Call(typeof(TypedReference).GetMethod("ToObject"));
        il.SetArray(typeof(object));
        // lb_currentIndex++;
        il.LoadLocal(lb_currentIndex);
        il.LoadInt(1);
        il.MathAdd();
        il.SetLocal(lb_currentIndex);
        il.Nop();
        il.Goto(haveValueL);

        // 结束
        il.MarkLabel(returnL);
        il.LoadLocal(lb_result);
        il.Return();
        return methodBuilder;
    }

    // 调用有可变参数的方法
    public static MethodBuilder DefineMethod_Arglist_Invoke1()
    {
        // 有可变参数的方法
        MethodBuilder varargMethod = DefineMethod_Arglist1();

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Arglist_Invoke1", MethodAttributes.Public | MethodAttributes.Static, typeof(object[]), new Type[] { typeof(object), typeof(object), typeof(object), typeof(object) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(object[]));

        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadArg(2);
        il.LoadArg(3);
        il.Call(varargMethod, typeof(object), typeof(object), typeof(object), typeof(object));
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;

    }
    #endregion

    #region 调用方法
    // 使用调用普通方法的指令调用虚方法 编译时类型 EmitTest1 (调用定义的虚方法而不是类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CallVirtual1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.Call(typeof(EmitTest1).GetMethod(nameof(EmitTest1.T1), new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 使用调用普通方法的指令调用虚方法 编译时类型 EmitTest2(调用定义的虚方法而不是类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CallVirtual2", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.Call(typeof(EmitTest1).GetMethod(nameof(EmitTest1.T1), new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 使用调用虚方法的指令调用虚方法 编译时类型 EmitTest1(调用类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual3()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CallVirtual3", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.CallVirtual(typeof(EmitTest1).GetMethod(nameof(EmitTest1.T1), new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 将方法标记为最后的方法
    public static MethodBuilder DefineMethod_CallVirtual4()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CallVirtual4", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // return l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.TailCall();
        il.CallVirtual(typeof(EmitTest1).GetMethod(nameof(EmitTest1.T1), new Type[] { typeof(string) }));
        il.Return();

        return methodBuilder;
    }

    // 使用调用虚方法的指令调用虚方法 调用前进行约束
    public static MethodBuilder DefineMethod_CallVirtual5()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CallVirtual5", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.LoadArg(0);
        il.Constrained(typeof(EmitTest1));
        il.CallVirtual(typeof(EmitTest1).GetMethod(nameof(EmitTest1.T1), new Type[] { typeof(string) }));
        il.SetLocal(l2);

        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 数组
    // 返回数组中某个索引的值
    public static MethodBuilder DefineMethod_Array1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Array1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int[]));

        // ** l1 = new int[3];
        // new int[3]
        il.LoadInt(3);
        il.NewArray(typeof(int));
        // int[0] = 3
        il.Copy();//拷贝一个值是为了让堆栈上一直存在一个数组
        il.LoadInt(0);
        il.LoadInt(3);
        il.SetArray(typeof(int));
        // int[1] = 5
        il.Copy();
        il.LoadInt(1);
        il.LoadInt(5);
        il.SetArray(typeof(int));
        // int[2] = 8
        il.Copy();
        il.LoadInt(2);
        il.LoadInt(8);
        il.SetArray(typeof(int));
        // l1 = 赋值了的数组
        il.SetLocal(l1);

        // return l1[2];
        il.LoadLocal(l1);
        il.LoadInt(2);
        il.LoadArrayIndexValue(typeof(int));
        il.Box(typeof(int));
        il.Return();
        return methodBuilder;
    }

    // 返回数组中某个索引的值
    public static MethodBuilder DefineMethod_Array2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Array2", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(string[]));

        // ** l1 = new string[3];
        il.LoadInt(3);
        il.NewArray(typeof(string));
        il.Copy();//拷贝一个值是为了让堆栈上一直存在一个数组
        il.LoadInt(0);
        il.LoadString("3");
        il.SetArray();
        il.Copy();
        il.LoadInt(1);
        il.LoadString("5");
        il.SetArray();
        il.Copy();
        il.LoadInt(2);
        il.LoadString("8");
        il.SetArray();
        il.SetLocal(l1);

        // return l1[2];
        il.LoadLocal(l1);
        il.LoadInt(2);
        il.LoadArrayIndexValue();
        il.Return();
        return methodBuilder;
    }

    // 返回数组中某个索引的值的地址的值
    public static MethodBuilder DefineMethod_Array3()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Array3", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(string[]));

        // ** l1 = new string[3];
        il.LoadInt(3);
        il.NewArray(typeof(string));
        il.Copy();//拷贝一个值是为了让堆栈上一直存在一个数组
        il.LoadInt(0);
        il.LoadString("3");
        il.SetArray();
        il.Copy();
        il.LoadInt(1);
        il.LoadString("5");
        il.SetArray();
        il.Copy();
        il.LoadInt(2);
        il.LoadString("8");
        il.SetArray();
        il.SetLocal(l1);

        // return l1[2];
        il.LoadLocal(l1);
        il.LoadInt(2);
        il.LoadArrayIndexAddr(typeof(string));//得到地址
        il.LoadAddrValue(typeof(string));//从地址得到值
        il.Return();
        return methodBuilder;
    }

    // 返回数组长度
    public static MethodBuilder DefineMethod_Array4()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Array4", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(string[]));

        il.LoadInt(99);
        il.NewArray(typeof(string));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.LoadLength();
        il.Box(typeof(int));
        il.Return();
        return methodBuilder;
    }
    #endregion

    #region 地址
    // SetValueToAddr
    public static MethodBuilder DefineMethod_SetValueToAddr1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("SetValueToAddr1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.LoadInt(10);
        il.SetValueToAddr(typeof(int));

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // SetValueToAddr
    public static MethodBuilder DefineMethod_SetValueToAddr2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("SetValueToAddr2", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(object));

        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.NewObject(typeof(object).GetConstructor(Type.EmptyTypes));
        il.SetValueToAddr(typeof(object));

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }
    #endregion

    #region 方法指针调用方法(如果设置返回类型为string的话,没办法测试)
    // 方法指针调用方法1
    public static MethodBuilder DefineMethod_Calli_Ldftn1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Calli_Ldftn1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        // l1 = EmitTest2.T2();
        il.LoadMethodPointer(typeof(EmitTest2).GetMethod(nameof(EmitTest2.T2)));
        il.Calli(System.Runtime.InteropServices.CallingConvention.StdCall, typeof(int));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法2(实例方法)
    public static MethodBuilder DefineMethod_Calli_Ldftn2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Calli_Ldftn2", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        // l1 = new EmitTest2().T3();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.LoadMethodPointer(typeof(EmitTest2).GetMethod(nameof(EmitTest2.T3)));
        il.Calli(System.Runtime.InteropServices.CallingConvention.ThisCall, typeof(int), typeof(EmitTest2));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法3(虚方法)
    public static MethodBuilder DefineMethod_Calli_Ldftn3()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Calli_Ldftn3", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        // l1 = new EmitTest2().T4();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.Copy();
        il.LoadMethodPointer(typeof(EmitTest2).GetMethod(nameof(EmitTest2.T4)));
        il.Calli(System.Runtime.InteropServices.CallingConvention.ThisCall, typeof(int), typeof(EmitTest2));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // 方法指针调用方法4(虚方法)
    public static MethodBuilder DefineMethod_Calli_Ldftn4()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Calli_Ldftn4", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        // l1 = ((EmitTest1)new EmitTest2()).T4();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.NewObject(typeof(EmitTest1).GetConstructor(Type.EmptyTypes));
        il.LoadMethodPointer(typeof(EmitTest2).GetMethod(nameof(EmitTest2.T4)));
        il.Calli(System.Runtime.InteropServices.CallingConvention.ThisCall, typeof(int), typeof(EmitTest2));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Try Catch Finally
    // Try_Catch1
    public static MethodBuilder DefineMethod_Try_Catch1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch1", MethodAttributes.Public | MethodAttributes.Static, typeof(void), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // try{
        il.BeginExceptionBlock();

        // throw new Exception("Try_Catch1 测试 异常");
        il.LoadString("Try_Catch1 测试 异常");
        il.NewObject(typeof(Exception).GetConstructor(new Type[] { typeof(string) }));
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
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch2", MethodAttributes.Public | MethodAttributes.Static, typeof(string), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 声明本地变量
        LocalBuilder l1 = il.DeclareLocal(typeof(string));
        LocalBuilder l2 = il.DeclareLocal(typeof(Exception));

        // try{
        il.BeginExceptionBlock();
        // l1 = "没有异常";
        il.LoadString("没有异常");
        il.SetLocal(l1);
        // throw new Exception("Try_Catch2 测试 异常");
        il.LoadString("Try_Catch2 测试 异常");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // }catch (Exception e){
        il.BeginCatchBlock(typeof(Exception));
        // l2 = e;
        il.SetLocal(l2);
        // l1 = l2.Message;
        il.LoadLocal(l2);
        il.Call(typeof(Exception).GetMethod("get_Message"));
        il.SetLocal(l1);
        // }
        il.EndExceptionBlock();
        // return l1;
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // Try_Catch3
    public static MethodBuilder DefineMethod_Try_Catch3()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch3", MethodAttributes.Public | MethodAttributes.Static, typeof(string), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 声明本地变量
        LocalBuilder l1 = il.DeclareLocal(typeof(string));

        // try{
        il.BeginExceptionBlock();
        // throw new Exception("Try_Catch3 测试 异常");
        il.LoadString("Try_Catch3 测试 异常");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // }catch{
        il.BeginCatchBlock(typeof(object));
        // l1 = "进入了一个不判断类型的异常捕捉";
        il.LoadString("进入了一个不判断类型的异常捕捉");
        il.SetLocal(l1);
        // }
        il.EndExceptionBlock();
        // return l1;
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // 有筛选的try catch
    public static MethodBuilder DefineMethod_Try_Catch4()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch4", MethodAttributes.Public | MethodAttributes.Static, typeof(string), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 声明本地变量
        LocalBuilder v1 = il.DeclareLocal(typeof(string));
        // 声明标签
        Label l1 = il.DefineLabel();
        Label l2 = il.DefineLabel();

        // try{
        il.BeginExceptionBlock();
        // v1 = "没有异常";
        il.LoadString("没有异常");
        il.SetLocal(v1);
        // throw new Exception("123");
        il.LoadString("123");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // } catch (Exception ex) 
        il.BeginExceptFilterBlock();
        il.As(typeof(Exception));
        il.Copy();
        il.GotoIfTrue(l1);
        // 如果异常类型检查未通过
        il.Pop();
        il.LoadInt(0);
        il.Goto(l2);
        // when (ex.Message == "123"){
        il.MarkLabel(l1);
        il.Call(typeof(Exception).GetProperty("Message").GetGetMethod());
        il.LoadString("123");
        il.Call(typeof(string).GetMethod("op_Equality", [typeof(string), typeof(string)]));
        il.MarkLabel(l2);
        il.BeginCatchBlock(null);
        // v1 = ex.Message;
        il.Call(typeof(Exception).GetProperty("Message").GetGetMethod());
        il.SetLocal(v1);
        // } catch(Exception ex) {
        il.BeginCatchBlock(typeof(Exception));
        // v1 = ex.Message + "----";
        il.Call(typeof(Exception).GetProperty("Message").GetGetMethod());
        il.LoadString("----");
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string)]));
        il.SetLocal(v1);
        // }
        il.EndExceptionBlock();
        // return v1;
        il.LoadLocal(v1);
        il.Return();

        return methodBuilder;
    }

    // Try_Catch_Finally1
    public static MethodBuilder DefineMethod_Try_Catch_Finally1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch_Finally1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(string));

        // try{
        il.BeginExceptionBlock();

        // l1 += "进入了 Try"; 
        il.LoadLocal(l1);
        il.LoadString("进入了 Try      ");
        il.Call(typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(l1);
        // throw new Exception("Try_Catch1 测试 异常");
        il.LoadString("Try_Catch_Finally1 测试 异常");
        il.NewObject(typeof(Exception).GetConstructor(new Type[] { typeof(string) }));
        il.Throw();

        // }catch (Exception){
        il.BeginCatchBlock(typeof(Exception));

        // l1 += "触发了 Exception 类型异常";
        il.LoadLocal(l1);
        il.LoadString("触发了 Exception 类型异常      ");
        il.Call(typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(l1);

        // }catch (ArgumentNullException){
        il.BeginCatchBlock(typeof(ArgumentNullException));

        // l1 += "触发了 ArgumentNullException 类型异常";
        il.LoadLocal(l1);
        il.LoadString("触发了 ArgumentNullException 类型异常      ");
        il.Call(typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(l1);

        // } Finally{
        il.BeginFinallyBlock();

        // l1 += "进入了 Finally"
        il.LoadLocal(l1);
        il.LoadString("进入了 Finally      ");
        il.Call(typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(l1);

        il.EndFinally(); // Finally 块结束,非必须,没有该指令也不会报错

        // }
        il.EndExceptionBlock();

        // l1 += "开始返回"
        il.LoadLocal(l1);
        il.LoadString("开始返回");
        il.Call(typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Try_Catch_Finally2
    public static MethodBuilder DefineMethod_Try_Catch_Finally2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Try_Catch_Finally2", MethodAttributes.Public | MethodAttributes.Static, typeof(int), [typeof(int), typeof(int)]);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 声明本地变量
        LocalBuilder v1 = il.DeclareLocal(typeof(int));
        // 声明标签
        Label l1 = il.DefineLabel();
        Label l2 = il.DefineLabel();

        // try{
        il.BeginExceptionBlock();
        // v1 = 1;
        il.LoadInt(1);
        il.SetLocal(v1);
        // throw new Exception("123");
        il.LoadString("123");
        il.NewObject(typeof(Exception).GetConstructor([typeof(string)]));
        il.Throw();
        // } catch (Exception ex) 
        il.BeginExceptFilterBlock();
        il.As(typeof(Exception));
        il.Copy();
        il.GotoIfTrue(l1);
        // 如果异常类型检查未通过
        il.Pop();
        il.LoadInt(0);
        il.Goto(l2);
        // when (ex.Message == "123"){
        il.MarkLabel(l1);
        il.Call(typeof(Exception).GetProperty("Message").GetGetMethod());
        il.LoadString("123");
        il.Call(typeof(string).GetMethod("op_Equality", [typeof(string), typeof(string)]));
        il.MarkLabel(l2);
        il.BeginCatchBlock(null);
        // v1 = v1 + arg0;
        il.LoadLocal(v1);
        il.LoadArg(0);
        il.MathAdd();
        il.SetLocal(v1);
        // } catch(Exception ex) {
        il.BeginCatchBlock(typeof(Exception));
        // v1 = v1 + arg1;
        il.LoadLocal(v1);
        il.LoadArg(1);
        il.MathAdd();
        il.SetLocal(v1);
        // } finally {
        il.BeginFinallyBlock();
        // v1 = v1 + 10;
        il.LoadLocal(v1);
        il.LoadInt(10);
        il.MathAdd();
        il.SetLocal(v1);
        // }
        il.EndExceptionBlock();
        // return v1;
        il.LoadLocal(v1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    // sizeof
    public static MethodBuilder DefineMethod_SizeOf1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("SizeOf1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int[]));

        il.LoadInt(8);
        il.NewArray(typeof(int));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.LoadInt(0);
        il.SizeOf(typeof(byte));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(1);
        il.SizeOf(typeof(short));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(2);
        il.SizeOf(typeof(int));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(3);
        il.SizeOf(typeof(long));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(4);
        il.SizeOf(typeof(double));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(5);
        il.SizeOf(typeof(decimal));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(6);
        il.SizeOf(typeof(string));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.LoadInt(7);
        il.SizeOf(typeof(object));
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // set Field
    public static MethodBuilder DefineMethod_SetField1()
    {
        FieldBuilder fb = _typeBuilder.DefineField("field1", typeof(int), FieldAttributes.Public | FieldAttributes.Static);
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("SetField1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadDouble(10.0);
        il.LoadDouble(3.0);
        il.MathDiv();
        il.ConvertInteger(typeof(int));
        il.SetField(fb);

        il.LoadField(fb);
        il.Return();
        return methodBuilder;
    }

    // typeof
    public static MethodBuilder DefineMethod_Typeof1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Typeof1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(Type));

        il.LoadRuntimeHandle(typeof(string));
        il.Call(typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // copyAddrValueToAddr
    public static MethodBuilder DefineMethod_CopyAddrValueToAddr1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("CopyAddrValueToAddr1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(EmitTest2) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));

        // l1 = arg1;
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.LoadArgAddr(0);
        il.SizeOf(typeof(EmitTest2));
        il.CopyAddrValueToAddr();

        // return l1; 
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Cpobj
    public static MethodBuilder DefineMethod_Cpobj1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Cpobj1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(EmitTest2) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));

        // l1 = arg1;
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.LoadArgAddr(0);
        il.Cpobj(typeof(EmitTest2));

        // return l1; 
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }


    // Localloc_Initblk
    public static MethodBuilder DefineMethod_Localloc_Initblk1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Localloc_Initblk1", MethodAttributes.Public | MethodAttributes.Static, typeof(uint), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 开辟一个 4 字节的空间
        il.SizeOf(typeof(uint));
        il.Localloc();
        il.Copy();// 复制一个地址待会直接取值

        // 为刚刚开辟的空间设置初始值(将4个字节的值都初始化为255，也是11111111)
        il.LoadInt(byte.MaxValue);
        il.SizeOf(typeof(uint));
        il.Initblk();

        // 从地址取值,返回
        il.LoadAddrValue(typeof(uint));
        il.Return();
        return methodBuilder;
    }


    // Initobj
    public static MethodBuilder DefineMethod_Initobj1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Initobj1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.Emit(OpCodes.Initobj, typeof(int));

        il.LoadLocal(l1);

        il.Return();
        return methodBuilder;
    }


    // Mkrefany1（值类型引用化）
    public static MethodBuilder DefineMethod_Mkrefany1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Mkrefany1", MethodAttributes.Public | MethodAttributes.Static, typeof(MyStruct), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(MyStruct));
        LocalBuilder l2 = il.DeclareLocal(typeof(TypedReference));
        LocalBuilder l3 = il.DeclareLocal(typeof(FieldInfo));

        // l1 = new MyStruct("1");
        // 初始化结构
        il.LoadString("1");
        il.NewObject(typeof(MyStruct).GetConstructor(new Type[] { typeof(string) }));
        il.SetLocal(l1);

        // l2 = __makeref(l1)
        // 将结构引用化
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.Mkrefany(typeof(MyStruct));
        il.SetLocal(l2);

        // l3 = typeof(MyStruct).GetField("_name",BindingFlags.NonPublic | BindingFlags.Instance);
        // 获取结构的_name字段
        il.LoadRuntimeHandle(typeof(MyStruct));
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), new Type[] { typeof(RuntimeTypeHandle) }));
        il.LoadString("_name");
        il.LoadInt(32 | 4);
        il.CallVirtual(typeof(Type).GetMethod(nameof(Type.GetField), new Type[] { typeof(string), typeof(BindingFlags) }));
        il.SetLocal(l3);

        // l3.SetValueDirect(l2,"2");
        // 设置结构的_name字段值
        il.LoadLocal(l3);
        il.LoadLocal(l2);
        il.LoadString("2");
        il.CallVirtual(typeof(FieldInfo).GetMethod(nameof(FieldInfo.SetValueDirect), new Type[] { typeof(TypedReference), typeof(object) }));

        // return l1;
        // 返回结构
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Mkrefany2（值类型没有引用化）
    public static MethodBuilder DefineMethod_Mkrefany2()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Mkrefany2", MethodAttributes.Public | MethodAttributes.Static, typeof(MyStruct), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(MyStruct));
        LocalBuilder l2 = il.DeclareLocal(typeof(FieldInfo));

        // l1 = new MyStruct("1");
        // 初始化结构
        il.LoadString("1");
        il.NewObject(typeof(MyStruct).GetConstructor(new Type[] { typeof(string) }));
        il.SetLocal(l1);

        // l2 = typeof(MyStruct).GetField("_name",BindingFlags.NonPublic | BindingFlags.Instance);
        // 获取结构的_name字段
        il.LoadRuntimeHandle(typeof(MyStruct));
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), new Type[] { typeof(RuntimeTypeHandle) }));
        il.LoadString("_name");
        il.LoadInt(32 | 4);
        il.CallVirtual(typeof(Type).GetMethod(nameof(Type.GetField), new Type[] { typeof(string), typeof(BindingFlags) }));
        il.SetLocal(l2);

        // l2.SetValueDirect(l1,"2");
        // 设置结构的_name字段值
        il.LoadLocal(l2);
        il.LoadLocal(l1);
        il.Box(typeof(MyStruct));
        il.LoadString("2");
        il.CallVirtual(typeof(FieldInfo).GetMethod(nameof(FieldInfo.SetValue), new Type[] { typeof(object), typeof(object) }));

        // return l1;
        // 返回结构
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Refanytype1 （获取嵌套在引用化类型中的值的类型）
    public static MethodBuilder DefineMethod_Refanytype1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Refanytype1", MethodAttributes.Public | MethodAttributes.Static, typeof(Type), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(MyStruct));
        LocalBuilder l2 = il.DeclareLocal(typeof(TypedReference));

        // l1 = new MyStruct("1");
        // 初始化结构
        il.LoadString("1");
        il.NewObject(typeof(MyStruct).GetConstructor(new Type[] { typeof(string) }));
        il.SetLocal(l1);

        // l2 = __makeref(l1)
        // 将结构引用化
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.Mkrefany(typeof(MyStruct));
        il.SetLocal(l2);

        // __reftype(l2);
        il.LoadLocal(l2);
        il.Refanytype();
        il.Call(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle), new Type[] { typeof(RuntimeTypeHandle) }));
        il.Return();
        return methodBuilder;
    }

    // Refanyval1 （获取嵌套在引用化类型中的值的地址）
    public static MethodBuilder DefineMethod_Refanyval1()
    {
        // 测试方法

        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Refanyval1", MethodAttributes.Public | MethodAttributes.Static, typeof(MyStruct), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(MyStruct));
        LocalBuilder l2 = il.DeclareLocal(typeof(TypedReference));

        // l1 = new MyStruct("1");
        // 初始化结构
        il.LoadString("1");
        il.NewObject(typeof(MyStruct).GetConstructor(new Type[] { typeof(string) }));
        il.SetLocal(l1);

        // l2 = __makeref(l1)
        // 将结构引用化
        il.LoadLocalAddr((ushort)l1.LocalIndex);
        il.Mkrefany(typeof(MyStruct));
        il.SetLocal(l2);

        // 获取值类型的地址，再从地址获取值
        il.LoadLocal(l2);
        il.Refanyval(typeof(MyStruct));
        il.LoadAddrValue(typeof(MyStruct));
        il.Return();
        return methodBuilder;
    }

    // 参数赋值
    public static MethodBuilder DefineMethod_SetArg1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("SetArg1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadInt(999);
        il.SetArg();
        il.LoadArg();
        il.Return();

        return methodBuilder;
    }


    // 使用对其操作
    // 该方法创建的方法 可以用来复制byte数组
    public static MethodBuilder DefineMethod_Unaligned1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Unaligned1", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadArg(1); // 目标地址
        il.LoadArg(0); // 原地址

        // 复制的长度 
        il.LoadArg(2);

        // 因为字节的大小是 1 所以要执行对其
        // sizeof(T) % 8 不是0的都应该执行对其 see https://stackoverflow.com/questions/24122973/what-should-i-pin-when-working-on-arrays/47128947#47128947
        il.Unaligned(sizeof(byte));
        // 从原地址将指定的长度的值复制到目标地址
        il.CopyAddrValueToAddr();

        il.Return();
        return methodBuilder;
    }

    // Volatile1
    // 表示指定地址是易失的,当多线程修改同时操作值时不会排列对该值的读取和写入
    public static MethodBuilder DefineMethod_Volatile1()
    {
        // 声明一个易失的字段
        FieldBuilder _volatile1 = _typeBuilder.DefineField("_volatile1", typeof(int), new Type[] { typeof(System.Runtime.CompilerServices.IsVolatile) }, new Type[] { }, FieldAttributes.Static);

        // 声明方法
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Volatile1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        // 为易失字段赋值
        il.LoadInt(1);
        il.Volatile();
        il.SetField(_volatile1);

        // 获取易失字段值
        il.Volatile();
        il.LoadField(_volatile1);
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Readonly1
    // 该方法用于测试只读，不过只读没有用，设置了只读,还是可以设置值和修改值
    public static MethodBuilder DefineMethod_Readonly1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Readonly1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2[]));

        // l1 = new EmitTest2[10];
        il.LoadInt(10);
        il.NewArray(typeof(EmitTest2));
        il.SetLocal(l1);

        // l1[1] = new EmitTest2();
        il.LoadLocal(l1);
        il.LoadInt(1);
        il.Readonly();
        il.LoadArrayIndexAddr(typeof(EmitTest2));
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetValueToAddr();

        // l1[1].MyInt1 = 5;
        il.LoadLocal(l1);
        il.LoadInt(1);
        il.Readonly();
        il.LoadArrayIndexAddr(typeof(EmitTest2));
        il.LoadAddrValue(typeof(EmitTest2));
        il.LoadInt(5);
        il.Unaligned(sizeof(int));
        il.SetField(typeof(EmitTest2).GetField("MyInt1"));


        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // Jmp1
    public static MethodBuilder DefineMethod_Jmp1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Jmp1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.Jmp(typeof(EmitTest2).GetMethod(nameof(EmitTest2.T5)));
        return methodBuilder;
    }

    // Jmp2 外部方法
    public static MethodBuilder DefineMethod_Jmp2()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Jmp2", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(string), typeof(string), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.Jmp(typeof(EmitTest1).GetMethod(nameof(EmitTest1.MsgBox)));
        return methodBuilder;
    }

    // Ckfinite1
    public static MethodBuilder DefineMethod_Ckfinite1()
    {
        MethodBuilder methodBuilder = _typeBuilder.DefineMethod("Ckfinite1", MethodAttributes.Public | MethodAttributes.Static, typeof(double), new Type[] { });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadDouble(10.0);
        il.LoadDouble(3.0);
        il.MathDiv();
        il.Ckfinite();
        il.Return();
        return methodBuilder;
    }
}

public class EmitTest1
{
    public virtual string T1(string s) => s + "###";

    public virtual int T4() => -4;


    /// <summary>
    /// 这是一个弹出消息框的外部方法
    /// </summary>
    [DllImport("user32.dll", EntryPoint = "MessageBoxA")]
    public static extern int MsgBox(int hWnd, string msg, string caption, int type);
}

public class EmitTest2 : EmitTest1
{
    public int MyInt1;
    public int MyProperty { get; set; }
    public override string T1(string s) => s + "%%%";


    public static int T2() => 2;

    public int T3() => 3;

    public override int T4() => 4;

    public static string T5(int i) => i.ToString();

    public static string T6() => throw new Exception("T6 测试异常");
}

public struct MyStruct
{
    public MyStruct(string name)
    {
        _name = name;
    }

    private string _name;

    public string Name => _name;
}
