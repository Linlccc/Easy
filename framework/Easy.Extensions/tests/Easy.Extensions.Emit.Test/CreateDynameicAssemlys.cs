using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Linq;

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

        #region 数组
        // 获取一个数组
        Invoke("Array1", out int[] arrar1);
        Assert.Equal([3, 4, 5], arrar1);

        // 获取数组某一索引的值
        Assert.Throws<IndexOutOfRangeException>(ThrowReal(() => Invoke("Array2", out int _, new string[] { "aa", "bb", "cc" }, 3)));
        Invoke("Array2", out string array2, new string[] { "aa", "bb", "cc" }, 1);
        Assert.Equal("bb", array2);
        // 先获取地址，再从地址获取值
        Invoke("Array3", out string array3, new string[] { "aa", "bb", "cc" }, 1);
        Assert.Equal("bb", array2);

        // 获取数组长度
        Invoke("Array4", out int array4, [new string[] { "aa", "bb", "cc" }]);
        Assert.Equal(3, array4);
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

        #region 通过指针调用方法
        // 实例方法
        Invoke("Calli1", out string calli1, new Test1(), "a");
        Assert.Equal("##a", calli1);
        Invoke("Calli1", out string calli1_2, new Test2(), "a");
        Assert.Equal("##a", calli1_2);

        // 实例虚方法
        Invoke("Calli2", out string calli2, new Test1(), "a");
        Assert.Equal("a##", calli2);
        Invoke("Calli2", out string calli2_2, new Test2(), "a");
        Assert.Equal("a%%", calli2_2);

        // 静态方法
        Invoke("Calli3", out int calli3, 5, 2);
        Assert.Equal(2, calli3);

        // 外部方法,因为每次调用都会弹出一个窗口，所以注释掉
        // 点击 确定返回1，取消返回2
        //Invoke("Calli4", out int calli4, "我是内容", "我是标题");
        //Assert.True(calli4 is 1 or 2);
        #endregion

        #region 地址/指针赋值
        // 设置值到地址
        Invoke("SetValueToAddr1", out int setValueToAddr1, 100);
        Assert.Equal(100, setValueToAddr1);
        // 设置值到地址
        object setValueToAddr2Expected = new();
        Invoke("SetValueToAddr2", out object setValueToAddr2, setValueToAddr2Expected);
        Assert.Equal(setValueToAddr2Expected, setValueToAddr2);
        #endregion

        #region Try Catch Finally
        // Try_Catch1
        Assert.Throws<Exception>(ThrowReal(() => Invoke("Try_Catch1", out object _)));

        // Try_Catch2
        Invoke("Try_Catch2", out string try_Catch2, false);
        Assert.Equal("没有异常", try_Catch2);
        Invoke("Try_Catch2", out string try_Catch2_2, true);
        Assert.Equal("Try_Catch2测试异常", try_Catch2_2);

        // Try_Catch3 不判断类型的异常捕捉
        Invoke("Try_Catch3", out string try_Catch3);
        Assert.Equal("进入了一个不判断类型的异常捕捉", try_Catch3);

        // Try_Catch4 有筛选的异常捕捉
        Invoke("Try_Catch4", out string try_Catch4, 0);
        Assert.Equal("没有异常", try_Catch4);
        Invoke("Try_Catch4", out string try_Catch4_2, -1);
        Assert.Equal("123", try_Catch4_2);
        Invoke("Try_Catch4", out string try_Catch4_3, 1);
        Assert.Equal("456----", try_Catch4_3);

        // Try_Catch_Finally1
        Invoke("Try_Catch_Finally1", out string try_Catch_Finally1);
        Assert.Equal("进入Try 进入Catch 进入Finally", try_Catch_Finally1);
        #endregion

        #region 关键字
        // sizeof
        Invoke("SizeOf1", out int[] sizeof1);
        Assert.Equal([1, 2, 4, 8, 8, 16, 8, 8], sizeof1);

        // typeof
        Invoke("TypeOf1", out Type typeof1);
        Assert.Equal(typeof(string), typeof1);

        // default
        Invoke("Default1", out int default1);
        Assert.Equal(default, default1);
        #endregion

        #region 字段
        // 声明设置字段
        Invoke("Field1", out int filed1);
        Assert.Equal(3, filed1);
        #endregion

        #region 参数
        // SetArg1
        Invoke("SetArg1", out int setArg1, 1);
        Assert.Equal(10, setArg1);
        #endregion

        #region 值类型引用化
        // 值类型引用化
        Invoke("Mkrefany1", out User mkrefany1);
        Assert.Equal("B", mkrefany1.Name);

        // 值类型没有引用化,修改值
        Invoke("Mkrefany2", out User mkrefany2);
        Assert.Equal("A", mkrefany2.Name);

        // 获取嵌套在引用化类型中的值的类型
        Invoke("Refanytype1", out Type refanytype1);
        Assert.Equal(typeof(User), refanytype1);

        // 获取嵌套在引用化类型中的值的地址
        Invoke("Refanyval1", out User refanyval1);
        Assert.Equal("A", refanyval1.Name);
        #endregion

        #region 其他
        // 从地址复制值到地址
        Test2 copyAddrValueToAddr1_org = new();
        Invoke("CopyAddrValueToAddr1", out Test2 copyAddrValueToAddr1, copyAddrValueToAddr1_org);
        Assert.Equal(copyAddrValueToAddr1_org, copyAddrValueToAddr1);
        copyAddrValueToAddr1.Num++;
        copyAddrValueToAddr1.Txt += "1";
        Assert.Equal(2, copyAddrValueToAddr1_org.Num);
        Assert.Equal("Test11", copyAddrValueToAddr1_org.Txt);

        // 将对象地址的值复制到目标对象地址
        Test2 cpobj1_org = new();
        Invoke("Cpobj1", out Test2 cpobj1, cpobj1_org);
        Assert.Equal(cpobj1_org, cpobj1);
        cpobj1.Num++;
        cpobj1.Txt += "1";
        Assert.Equal(2, cpobj1_org.Num);
        Assert.Equal("Test11", cpobj1_org.Txt);

        // 动态分配空间，设置默认值
        Invoke("Localloc_Initblk1", out uint localloc_Initblk1);
        Assert.Equal(uint.MaxValue >> 8, localloc_Initblk1);
        #endregion



        #region 字符串相加
        // string1 + string2
        Invoke("Add1", out string concat1, "abc", "def");
        Assert.Equal("abcdef", concat1);
        #endregion



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
