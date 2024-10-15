namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// 以类代码是通过Emit 生成的
/// </summary>
public class EmitOpCodesVerify
{
    private static int _num;

    private static volatile int _num2;

    public static int Add1(int P_0, int P_1)
    {
        return P_0 + P_1;
    }

    public static int Add2(int P_0, int P_1)
    {
        return checked(P_0 + P_1);
    }

    public static int Add3(int P_0, int P_1)
    {
        return (int)checked(unchecked((uint)P_0) + unchecked((uint)P_1));
    }

    public static int Sub1(int P_0, int P_1)
    {
        return P_0 - P_1;
    }

    public static int Sub2(int P_0, int P_1)
    {
        return checked(P_0 - P_1);
    }

    public static int Sub3(int P_0, int P_1)
    {
        return (int)checked(unchecked((uint)P_0) - unchecked((uint)P_1));
    }

    public static int Mul1(int P_0, int P_1)
    {
        return P_0 * P_1;
    }

    public static int Mul2(int P_0, int P_1)
    {
        return checked(P_0 * P_1);
    }

    public static int Mul3(int P_0, int P_1)
    {
        return (int)checked(unchecked((uint)P_0) * unchecked((uint)P_1));
    }

    public static int Div1(int P_0, int P_1)
    {
        return P_0 / P_1;
    }

    public static int Div2(int P_0, int P_1)
    {
        return (int)((uint)P_0 / (uint)P_1);
    }

    public static int Rem1(int P_0, int P_1)
    {
        return P_0 % P_1;
    }

    public static int Rem2(int P_0, int P_1)
    {
        return (int)((uint)P_0 % (uint)P_1);
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

    public static int ShiftRight1(int P_0, int P_1)
    {
        return P_0 >> P_1;
    }

    public static int ShiftRight2(int P_0, int P_1)
    {
        return P_0 >>> P_1;
    }

    public static bool Equal1(int P_0, int P_1)
    {
        return P_0 == P_1;
    }

    public static bool Greater1(int P_0, int P_1)
    {
        return P_0 > P_1;
    }

    public static bool Greater2(int P_0, int P_1)
    {
        return (uint)P_0 > (uint)P_1;
    }

    public static bool Less1(int P_0, int P_1)
    {
        return P_0 < P_1;
    }

    public static bool Less2(int P_0, int P_1)
    {
        return (uint)P_0 < (uint)P_1;
    }

    public static int[] Array1()
    {
        return new int[3] { 3, 4, 5 };
    }

    public static string Array2(string[] P_0, int P_1)
    {
        return P_0[P_1];
    }

    public static string Array3(string[] P_0, int P_1)
    {
        return P_0[P_1];
    }

    public static int Array4(string[] P_0)
    {
        return P_0.Length;
    }

    public static object Box1(int P_0)
    {
        return P_0;
    }

    public static int UnBox1(object P_0)
    {
        return (int)P_0;
    }

    public static int UnBox2(object P_0)
    {
        return (int)P_0;
    }

    public static int ConvertInteger1(float P_0)
    {
        return (int)P_0;
    }

    public static Type As1(object P_0)
    {
        return P_0 as Type;
    }

    public static string Add1(string P_0, string P_1)
    {
        return P_0 + P_1;
    }

    public static string Call1(string P_0)
    {
        Test1 test = new Test2();
        return test.AddSuffix(P_0);
    }

    public static string Call2(string P_0)
    {
        Test1 test = new Test2();
        return test.AddPrefix(P_0);
    }

    public static string Call3(string P_0)
    {
        Test1 test = new Test2();
        return test.AddPrefix(P_0);
    }

    public static string CallVirtual1(string P_0)
    {
        Test1 test = new Test2();
        return test.AddSuffix(P_0);
    }

    public static string CallVirtual2(string P_0)
    {
        Test1 test = new Test2();
        return test.AddPrefix(P_0);
    }

    public static unsafe string CallVirtual3(object P_0)
    {
        //IL_000d: Expected O, but got Ref
        return (&P_0)->ToString();
    }

    public static int Jmp1(int P_0, int P_1)
    {
        return Test1.Div(P_0, P_1);
    }

    public static int Jmp2(int P_0, string P_1, string P_2, int P_3)
    {
        return Test1.MsgBox(P_0, P_1, P_2, P_3);
    }

    //public static unsafe string Calli1(object P_0, string P_1)
    //{
    //    return ((delegate*<object, string, string>)__ldftn(Test1.AddPrefix))(P_0, P_1);
    //}

    //public static unsafe string Calli2(object P_0, string P_1)
    //{
    //    return ((delegate*<object, string, string>)__ldvirtftn(Test1.AddSuffix))(P_0, P_1);
    //}

    public static unsafe int Calli3(int P_0, int P_1)
    {
        return ((delegate*<int, int, int>)(&Test1.Div))(P_0, P_1);
    }

    public static unsafe int Calli4(string P_0, string P_1)
    {
        return ((delegate*<int, string, string, int, int>)(&Test1.MsgBox))(0, P_0, P_1, 1);
    }

    public static int SetValueToAddr1(int P_0)
    {
        return P_0;
    }

    public static object SetValueToAddr2(object P_0)
    {
        return P_0;
    }

    public static void Try_Catch1()
    {
        try
        {
            throw new Exception("Try_Catch1 测试 异常");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string Try_Catch2(bool P_0)
    {
        string result;
        try
        {
            result = "没有异常";
            if (P_0)
            {
                throw new Exception("Try_Catch2测试异常");
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    public static string Try_Catch3()
    {
        try
        {
            throw new Exception();
        }
        catch
        {
            return "进入了一个不判断类型的异常捕捉";
        }
    }

    public static string Try_Catch4(int P_0)
    {
        string result;
        try
        {
            result = "没有异常";
            if (P_0 != 0)
            {
                if (P_0 <= 0)
                {
                    throw new Exception("123");
                }
                throw new Exception("456");
            }
        }
        catch (Exception ex) when (ex.Message == "123")
        {
            result = ex.Message;
        }
        catch (Exception ex2)
        {
            result = ex2.Message + "----";
        }
        return result;
    }

    public static string Try_Catch_Finally1()
    {
        string text = default;
        try
        {
            text = "进入Try";
            throw new Exception("Try_Catch_Finally1测试异常");
        }
        catch (Exception)
        {
            text += " 进入Catch";
        }
        finally
        {
            text += " 进入Finally";
        }
        return text;
    }

    //public static unsafe int[] SizeOf1()
    //{
    //    return new int[8]
    //    {
    //        sizeof(byte),
    //        sizeof(short),
    //        sizeof(int),
    //        sizeof(long),
    //        sizeof(double),
    //        sizeof(decimal),
    //        System.Runtime.CompilerServices.Unsafe.SizeOf<string>(),
    //        System.Runtime.CompilerServices.Unsafe.SizeOf<object>()
    //    };
    //}

    public static Type TypeOf1()
    {
        return typeof(string);
    }

    public static int Default1()
    {
        return default;
    }

    public static int Field1()
    {
        _num = 3;
        return _num;
    }

    public static int Volatile1()
    {
        _num = 10;
        return _num;
    }

    public static int SetArg1(int P_0)
    {
        P_0 = 10;
        return P_0;
    }

    public static int RefArg1(ref int P_0)
    {
        P_0 = 10;
        return P_0;
    }

    public static User Mkrefany1()
    {
        User result = new("A");
        TypedReference obj = __makeref(result);
        FieldInfo field = typeof(User).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic);
        field.SetValueDirect(obj, "B");
        return result;
    }

    public static User Mkrefany2()
    {
        User user = new("A");
        FieldInfo field = typeof(User).GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic);
        field.SetValue(user, "B");
        return user;
    }

    public static Type Refanytype1()
    {
        User user = new("A");
        TypedReference typedReference = __makeref(user);
        return __reftype(typedReference);
    }

    public static User Refanyval1()
    {
        User user = new("A");
        TypedReference typedReference = __makeref(user);
        return __refvalue(typedReference, User);
    }

    //public static Test2 CopyAddrValueToAddr1(Test2 P_0)
    //{
    //    Test2 result = default;
    //    // IL cpblk instruction
    //    System.Runtime.CompilerServices.Unsafe.CopyBlock(ref result, ref P_0, System.Runtime.CompilerServices.Unsafe.SizeOf<Test2>());
    //    return result;
    //}

    public static Test2 Cpobj1(Test2 P_0)
    {
        return P_0;
    }

    //public static unsafe uint Localloc_Initblk1()
    //{
    //    byte* ptr = stackalloc byte[sizeof(uint)];
    //    // IL initblk instruction
    //    System.Runtime.CompilerServices.Unsafe.InitBlock(ptr, 255, sizeof(uint) - sizeof(byte));
    //    return *(uint*)ptr;
    //}

    //public static void Unaligned1(ref byte P_0, ref byte P_1, int P_2)
    //{
    //    // IL cpblk instruction
    //    System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(ref P_1, ref P_0, P_2);
    //}

    public static object[] Arglist1(__arglist)
    {
        ArgIterator argIterator = new(__arglist);
        object[] array = new object[argIterator.GetRemainingCount()];
        int num = 0;
        while (argIterator.GetRemainingCount() != 0)
        {
            array[num] = TypedReference.ToObject(argIterator.GetNextArg());
            num++;
        }
        return array;
    }

    public static object[] ArglistInvoke1(object P_0, object P_1, object P_2, object P_3)
    {
        return Arglist1(__arglist(P_0, P_1, P_2, P_3));
    }

    public static double Ckfinite1(double P_0)
    {
        /*OpCode not supported: Ckfinite*/
        ;
        return P_0;
    }

    public static Test1[] ReadOnly1(Test1[] P_0)
    {
        ref Test1 reference = ref P_0[0];
        reference = new Test2();
        P_0[0].str = "aa";
        return P_0;
    }
}
