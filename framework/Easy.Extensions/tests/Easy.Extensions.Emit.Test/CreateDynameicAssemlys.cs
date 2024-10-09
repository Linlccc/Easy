using System.Linq;

namespace Easy.Extensions.Emit.Test;


/// <summary>
/// 创建动态程序集
/// </summary>
public class CreateDynameicAssemlys
{
    #region 动态程序集基础
    /// <summary>
    /// 动态程序集名称
    /// </summary>
    private static readonly string _assemblyName = nameof(CreateDynameicAssemlys);
    /// <summary>
    /// 动态程序集构建器
    /// </summary>
    private static readonly AssemblyBuilder _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_assemblyName), AssemblyBuilderAccess.RunAndSave);
    /// <summary>
    /// 模块构建器
    /// </summary>
    private static readonly ModuleBuilder _moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName, $"{_assemblyName}.dll");
    /// <summary>
    /// 创建动态程序集
    /// </summary>
    private void CreateCynameicAssemlys() => _assemblyBuilder.Save($"{_assemblyName}.dll");
    #endregion

    /// <summary>
    /// 创建程序集
    /// </summary>
    [Fact]
    public void CreateAssemlys()
    {
        _moduleBuilder.DefineType_HelloWorld();
        _moduleBuilder.DefineType_EmitOpCodesVerify();
        CreateCynameicAssemlys();
    }

    /// <summary>
    /// 创建类型 HelloWorld
    /// </summary>
    [Fact]
    public void Create_HelloWorld()
    {
        Type type = _moduleBuilder.DefineType_HelloWorld();

        // 创建实例
        object instance = Activator.CreateInstance(type, 100, "abc");

        // 验证
        Assert.Equal(100, type.GetProperty("Number").GetValue(instance));
        Assert.Equal("abc", type.GetField("m_number2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance));
        Assert.Equal(20 * 100, type.InvokeMember("Mul", BindingFlags.InvokeMethod, null, instance, new object[] { 20 }));
    }

    /// <summary>
    /// 创建类型 EmitOpCodesVerify
    /// </summary>
    [Fact]
    public void Create_EmitOpCodesVerify()
    {
        Type type = _moduleBuilder.DefineType_EmitOpCodesVerify();

        // ** 数学
        // +
        int[] add1 = (int[])type.InvokeMember("Add1", BindingFlags.InvokeMethod, null, null, new object[] { 10, 2 });
        int[] add2 = (int[])type.InvokeMember("Add2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(12, add1[0]);
        Assert.Equal(125, add2[0]);
        // -
        int[] sub1 = (int[])type.InvokeMember("Sub1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(121, sub1[0]);
        // *
        int[] mul1 = (int[])type.InvokeMember("Mul1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(246, mul1[0]);
        // /
        int[] div1 = (int[])type.InvokeMember("Div1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(61, div1[0]);
        // %
        int[] rem1 = (int[])type.InvokeMember("Rem1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(1, rem1[0]);
        // -arg
        int neg1 = (int)type.InvokeMember("Neg1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
        Assert.Equal(-123, neg1);

        // ** 按位计算
        // &
        int and1 = (int)type.InvokeMember("And1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(123 & 2, and1);
        // |
        int or1 = (int)type.InvokeMember("Or1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(123 | 2, or1);
        // ^
        int xor1 = (int)type.InvokeMember("Xor1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(123 ^ 2, xor1);
        // ~arg
        int not1 = (int)type.InvokeMember("Not1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
        Assert.Equal(~123, not1);
        // <<
        int shiftLeft1 = (int)type.InvokeMember("ShiftLeft1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(123 << 2, shiftLeft1);
        // >>
        int[] shiftRight1 = (int[])type.InvokeMember("ShiftRight1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
        Assert.Equal(123 >> 2, shiftRight1[0]);

        // ** 比较
        // ==
        int equal1 = (int)type.InvokeMember("Equal1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
        Assert.Equal(123 == 123 ? 1 : 0, equal1);
        bool equal2 = (bool)type.InvokeMember("Equal2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
        Assert.Equal(123 == 123, equal2);
        // >
        int greater1 = (int)type.InvokeMember("Greater1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
        Assert.Equal(123 > 123 ? 1 : 0, greater1);
        // >
        int less1 = (int)type.InvokeMember("Less1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
        Assert.Equal(123 < 123 ? 1 : 0, less1);

        // ** 特殊
        // 调用和定义包含有可变参数的方法
        object[] arglist_Invoke1 = (object[])type.InvokeMember("Arglist_Invoke1", BindingFlags.InvokeMethod, null, null, new object[] { 2, 3.1F, "5", typeof(int) });
        Assert.Equal(new object[] { 2, 3.1F, "5", typeof(int) }, arglist_Invoke1);

        // ** 类型转换
        // (object)int
        object box1 = (int)type.InvokeMember("Box1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
        Assert.Equal(123, box1);
        // (int)obj
        int unBox1 = (int)type.InvokeMember("UnBox1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
        Assert.Equal(123, unBox1);
        // (int)obj
        int unBox2 = (int)type.InvokeMember("UnBox2", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
        Assert.Equal(123, unBox2);
        // float to int
        int convert1 = (int)type.InvokeMember("ConvertInteger1", BindingFlags.InvokeMethod, null, null, new object[] { 123.999f });
        Assert.Equal((int)123.999, convert1);
        // as
        object as1 = type.InvokeMember("AS1", BindingFlags.InvokeMethod, null, null, new object[] { typeof(int) });
        Assert.Equal(typeof(int), as1);


        // ** 调用方法
        // 普通指令调用虚方法
        string callVirtual1 = (string)type.InvokeMember("CallVirtual1", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
        Assert.Equal("a" + "###", callVirtual1);
        // 普通指令调用虚方法
        string callVirtual2 = (string)type.InvokeMember("CallVirtual2", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
        Assert.Equal("a" + "###", callVirtual2);
        // 使用调用虚方法的指令调用虚方法
        string callVirtual3 = (string)type.InvokeMember("CallVirtual3", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
        Assert.Equal("a" + "%%%", callVirtual3);
        // 最后的方法指令
        string callVirtual4 = (string)type.InvokeMember("CallVirtual4", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
        Assert.Equal("a" + "%%%", callVirtual4);
        // 使用调用虚方法的指令调用虚方法 调用前进行约束
        string callVirtual5 = (string)type.InvokeMember("CallVirtual5", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
        Assert.Equal("a" + "%%%", callVirtual5);



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
