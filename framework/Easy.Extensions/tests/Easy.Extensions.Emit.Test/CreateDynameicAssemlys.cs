﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace Easy.Extensions.Emit.Test;


/// <summary>
/// 创建动态程序集
/// </summary>
public class CreateDynameicAssemlys
{
    /// <summary>
    /// 创建程序集
    /// </summary>
    [Fact]
    public void CreateAssemlys()
    {
        string assemblyName = nameof(CreateDynameicAssemlys);
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndSave);
        ModuleBuilder module = assembly.DefineDynamicModule(assemblyName, $"{assemblyName}.dll");

        // 定义类型
        module.DefineType_HelloWorld();
        module.DefineType_EmitOpCodesVerify();

        // 保存程序集
        assembly.Save($"{assemblyName}.dll");
    }

    /// <summary>
    /// 创建类型 HelloWorld
    /// </summary>
    [Fact]
    public void Create_HelloWorld()
    {
        string assemblyName = nameof(CreateDynameicAssemlys);
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndSave);
        ModuleBuilder module = assembly.DefineDynamicModule(assemblyName, $"{assemblyName}.dll");

        // 定义类型
        Type type = module.DefineType_HelloWorld();

        // 创建实例
        object instance = Activator.CreateInstance(type, 100, "abc");

        // 验证字段
        Assert.Equal("abc", type.GetField("_text", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance));
        Assert.Equal(100, type.GetField("_num", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance));

        // 验证属性
        Assert.Equal("abc - 100", type.GetProperty("Txt").GetValue(instance));

        // 验证方法
        Assert.Equal(20 * 100, type.InvokeMember("Mul", BindingFlags.InvokeMethod, null, instance, [20]));

        // 验证控制台输出
        type.InvokeMember("WriteHello", BindingFlags.InvokeMethod, null, null, []);
        type.InvokeMember("WriteTxt", BindingFlags.InvokeMethod, null, instance, []);
    }

    /// <summary>
    /// 创建类型 EmitOpCodesVerify
    /// </summary>
    [Fact]
    public void Create_EmitOpCodesVerify()
    {
        string assemblyName = nameof(CreateDynameicAssemlys);
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndSave);
        ModuleBuilder module = assembly.DefineDynamicModule(assemblyName, $"{assemblyName}.dll");


        // 定义类型
        Type type = module.DefineType_EmitOpCodesVerify();

        // 调用方法
        void Invoke<T>(string name, out T res, params object[] pars) => res = (T)type.InvokeMember(name, BindingFlags.InvokeMethod, null, null, pars);
        // 返回一个抛出真实异常的方法
        Action ThrowReal(Action action)
        {
            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Exception realEx = ex;
                    while (realEx.InnerException != null) realEx = realEx.InnerException;
                    throw realEx;
                }
            };
        }

        #region 数学
        // +
        Invoke("Add1", out int add1, int.MaxValue, 1);
        Assert.Equal(-2147483648, add1);
        // 检查溢出 +
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Add2", out int _, int.MaxValue, 1)));
        Invoke("Add2", out int add2, int.MaxValue, -1);
        Assert.Equal(2147483646, add2);
        // 检查溢出无符号 +
        // unchecked((uint)-1) == 4294967295
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Add3", out int _, -1, -1)));
        Invoke("Add3", out int add3, int.MaxValue, int.MaxValue);
        Assert.Equal(-2, add3);

        // -
        Invoke("Sub1", out int sub1, int.MinValue, 1);
        Assert.Equal(2147483647, sub1);
        // 检查溢出 -
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Sub2", out int _, int.MinValue, 1)));
        Invoke("Sub2", out int sub2, int.MinValue, -1);
        Assert.Equal(-2147483647, sub2);
        // 检查溢出无符号 -
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Sub3", out int _, 1, 2)));
        Invoke("Sub3", out int sub3, 10, 2);
        Assert.Equal(8, sub3);

        // *
        Invoke("Mul1", out int mul1, (int)(2147483648 / 2), 2);
        Assert.Equal(-2147483648, mul1);
        // 检查溢出 *
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Mul2", out int _, (int)(2147483648 / 2), 2)));
        Invoke("Mul2", out int mul2, (int)(2147483648 / 2), 1);
        Assert.Equal((int)(2147483648 / 2), mul2);
        // 检查溢出无符号 *
        Assert.Throws<OverflowException>(ThrowReal(() => Invoke("Mul3", out int _, int.MaxValue, 3)));
        Invoke("Mul3", out int mul3, int.MaxValue, 2);
        Assert.Equal(-2, mul3);

        // /
        Invoke("Div1", out int div1, 100, 2);
        Assert.Equal(50, div1);
        // 无符号 /
        Invoke("Div2", out int div2, -1, int.MaxValue);
        Assert.Equal(2, div2);

        // %
        Invoke("Rem1", out int rem1, 100, 3);
        Assert.Equal(1, rem1);
        // 无符号 % unchecked((uint)-1) == 4294967295
        Invoke("Rem2", out int rem2, -1, int.MaxValue);
        Assert.Equal(1, rem2);

        // 求反 -arg
        Invoke("Neg1", out int neg1, int.MaxValue);
        Assert.Equal(-2147483647, neg1);
        Invoke("Neg1", out int neg2, -2147483647);
        Assert.Equal(2147483647, neg2);
        #endregion

        #region 位运算
        // &
        Invoke("And1", out int and1, int.MaxValue, 3);
        Assert.Equal(3, and1);

        // |
        Invoke("Or1", out int or1, 12, 7);
        Assert.Equal(15, or1);

        // ^
        Invoke("Xor1", out int xor1, 12, 7);
        Assert.Equal(11, xor1);

        // ~
        Invoke("Not1", out int not1, 0);
        Assert.Equal(-1, not1);

        // <<
        Invoke("ShiftLeft1", out int shiftLeft1, 1, 2);
        Assert.Equal(4, shiftLeft1);

        // >>
        Invoke("ShiftRight1", out int shiftRight1, 8, 2);
        Assert.Equal(2, shiftRight1);
        // >> 无符号
        Invoke("ShiftRight2", out int shiftRight2, -1, 1);
        Assert.Equal(int.MaxValue, shiftRight2);
        #endregion

        #region 比较
        // ==
        Invoke("Equal1", out bool equal1, 100, 100);
        Assert.True(equal1);

        // >
        Invoke("Greater1", out bool greater1, int.MaxValue, -1);
        Assert.True(greater1);
        // 无符号 >
        Invoke("Greater2", out bool greater2, int.MaxValue, -1);
        Assert.False(greater2);

        // <
        Invoke("Less1", out bool less1, -1, int.MaxValue);
        Assert.True(less1);
        // 无符号 <
        Invoke("Less2", out bool less2, -1, int.MaxValue);
        Assert.False(less2);
        #endregion

        #region 类型转换
        // 装箱
        Invoke("Box1", out object box1, 100);
        Assert.Equal(100, box1);

        // 拆箱
        Invoke("UnBox1", out int unBox1, 100);
        Assert.Equal(100, unBox1);
        Invoke("UnBox1", out int unBox2, 100);
        Assert.Equal(100, unBox2);

        // float to int
        Invoke("ConvertInteger1", out int convert1, 123.999f);
        Assert.Equal((int)123.999, convert1);

        // as
        Invoke("As1", out object as1, typeof(int));
        Assert.Equal(typeof(int), as1);
        #endregion

        #region 调用方法
        // 调用普通指令调用虚方法
        Invoke("Call1", out string call1, "a");
        Assert.Equal("a##", call1);
        // 调用普通指令调用普通方法
        Invoke("Call2", out string call2, "a");
        Assert.Equal("##a", call2);
        // 最后的调用
        Invoke("Call3", out string call3, "a");
        Assert.Equal("##a", call3);

        // 调用虚方法指令调用虚方法
        Invoke("CallVirtual1", out string callVirtual1, "a");
        Assert.Equal("a%%", callVirtual1);
        // 调用虚方法指令调用普通方法
        Invoke("CallVirtual2", out string callVirtual2, "a");
        Assert.Equal("##a", callVirtual2);
        // 调用虚方法指令调用虚方法，调用前进行约束
        Invoke("CallVirtual3", out string callVirtual3, 123);
        Assert.Equal("123", callVirtual3);
        Invoke("CallVirtual3", out string callVirtual3_2, "aa");
        Assert.Equal("aa", callVirtual3_2);
        Invoke("CallVirtual3", out string callVirtual3_3, new Size(10, 10));
        Assert.Equal("{Width=10, Height=10}", callVirtual3_3);
        #endregion




        #region 字符串相加
        // string1 + string2
        Invoke("Add1", out string concat1, "abc", "def");
        Assert.Equal("abcdef", concat1);
        #endregion



        // 获取数组元素
        object array1 = type.InvokeMember("Array1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(8, array1);
        // 获取数组元素
        object array2 = type.InvokeMember("Array2", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("8", array2);
        // 获取数组元素的地址的值
        object array3 = type.InvokeMember("Array3", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("8", array3);
        // 获取数组长度
        object array4 = type.InvokeMember("Array4", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(99, array4);



        // ** 地址
        // SetValueToAddr
        object setValueToAddr1 = type.InvokeMember("SetValueToAddr1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(10, setValueToAddr1);
        object setValueToAddr2 = type.InvokeMember("SetValueToAddr2", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(typeof(object), setValueToAddr2.GetType());



        // ** 方法指针调用方法
        // 方法指针调用方法1
        int calli_Ldftn1 = (int)type.InvokeMember("Calli_Ldftn1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(2, calli_Ldftn1);
        // 方法指针调用方法2
        int calli_Ldftn2 = (int)type.InvokeMember("Calli_Ldftn2", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(3, calli_Ldftn2);
        // 方法指针调用方法3
        int calli_Ldftn3 = (int)type.InvokeMember("Calli_Ldftn3", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(4, calli_Ldftn3);
        // 方法指针调用方法4
        int calli_Ldftn4 = (int)type.InvokeMember("Calli_Ldftn4", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(-4, calli_Ldftn4);



        // ** Try Catch Finally
        // Try_Catch1
        Assert.Throws<TargetInvocationException>(() => type.InvokeMember("Try_Catch1", BindingFlags.InvokeMethod, null, null, new object[] { }));
        // Try_Catch2
        object try_Catch2 = type.InvokeMember("Try_Catch2", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("Try_Catch2 测试 异常", try_Catch2);
        // Try_Catch3
        object try_Catch3 = type.InvokeMember("Try_Catch3", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("进入了一个不判断类型的异常捕捉", try_Catch3);

        // Try_Catch4
        object try_Catch4 = type.InvokeMember("Try_Catch4", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("123", try_Catch4);

        // Try_Catch_Finally1
        object Tty_Catch_Finally1 = type.InvokeMember("Try_Catch_Finally1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal("进入了 Try      触发了 Exception 类型异常      进入了 Finally      开始返回", Tty_Catch_Finally1);
        // Try_Catch_Finally2
        object try_Catch_Finally2 = type.InvokeMember("Try_Catch_Finally2", BindingFlags.InvokeMethod, null, null, new object[] { 1, 20 });
        Assert.Equal(12, try_Catch_Finally2);


        // sizeof
        object sizeof1 = type.InvokeMember("SizeOf1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(new int[] { 1, 2, 4, 8, 8, 16, 8, 8 }, sizeof1);


        // set field
        object field1 = type.InvokeMember("SetField1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(3, field1);


        // typeof
        object typeof1 = type.InvokeMember("Typeof1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(typeof(string), typeof1);


        // CopyAddrValueToAddr1
        EmitTest2 copyAddrValueToAddr1 = new() { MyProperty = 9 };
        EmitTest2 copyAddrValueToAddr2 = (EmitTest2)type.InvokeMember("CopyAddrValueToAddr1", BindingFlags.InvokeMethod, null, null, new object[] { copyAddrValueToAddr1 });
        Assert.Equal(copyAddrValueToAddr1, copyAddrValueToAddr2);
        copyAddrValueToAddr1.MyProperty = 99;
        Assert.Equal(99, copyAddrValueToAddr2.MyProperty);

        // Cpobj1
        EmitTest2 cpobj1 = new() { MyProperty = 9 };
        EmitTest2 cpobj2 = (EmitTest2)type.InvokeMember("Cpobj1", BindingFlags.InvokeMethod, null, null, new object[] { cpobj1 });
        Assert.Equal(cpobj1, cpobj2);
        cpobj1.MyProperty = 99;
        Assert.Equal(99, cpobj2.MyProperty);


        // Localloc_Initblk
        uint localloc_Initblk1 = (uint)type.InvokeMember("Localloc_Initblk1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(uint.MaxValue, localloc_Initblk1);


        // Initobj1
        int initobj1 = (int)type.InvokeMember("Initobj1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(default, initobj1);

        // Mkrefany1（值类型引用化）
        object mkrefany1 = type.InvokeMember("Mkrefany1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(new MyStruct("2"), mkrefany1);
        // Mkrefany2（值类型没有引用化）
        object mkrefany2 = type.InvokeMember("Mkrefany2", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(new MyStruct("1"), mkrefany2);


        // Refanytype1 （获取嵌套在引用化类型中的值的类型）
        object refanytype1 = type.InvokeMember("Refanytype1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(typeof(MyStruct), refanytype1);


        // Refanytype1 （获取嵌套在引用化类型中的值的类型）
        object refanyval1 = type.InvokeMember("Refanyval1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(new MyStruct("1"), refanyval1);


        // 参数赋值
        object setArg1 = type.InvokeMember("SetArg1", BindingFlags.InvokeMethod, null, null, new object[] { 1, 2 });
        Assert.Equal(999, setArg1);


        // 使用对其操作
        byte[] bytes1 = { 1, 2, 3, 4, 5, 6 };
        byte[] bytes2 = new byte[7];
        Unaligned1 unaligned1 = (Unaligned1)type.GetMethod("Unaligned1").CreateDelegate(typeof(Unaligned1));
        unaligned1(ref bytes1[1], ref bytes2[2], 3);
        Assert.Equal(new byte[] { 0, 0, 2, 3, 4, 0, 0 }, bytes2);


        // Volatile1
        int volatile1 = (int)type.InvokeMember("Volatile1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(1, volatile1);


        // Readonly1
        EmitTest2[] readonly1 = (EmitTest2[])type.InvokeMember("Readonly1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(5, readonly1[1].MyInt1);

        // Jmp1
        object jmp1 = type.InvokeMember("Jmp1", BindingFlags.InvokeMethod, null, null, new object[] { 9999 });
        Assert.Equal("9999", jmp1);
        // Jmp2 以下方法会显示一个弹窗
        //object jmp2 = type.InvokeMember("Jmp2", BindingFlags.InvokeMethod, null, null, new object[] { 0, "这是一个消息", "这个一个titil", 1 });
        //Assert.Equal(1, jmp2);

        object ckfinite1 = type.InvokeMember("Ckfinite1", BindingFlags.InvokeMethod, null, null, new object[] { });
        Assert.Equal(10.0 / 3.0, ckfinite1);
    }

    // 使用对其操作 代理类型声明
    private delegate void Unaligned1(ref byte bs1, ref byte bs2, int length);
}
