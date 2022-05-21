using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Test;

public class Test1
{
    public Test1(int f1)
    {
        this.f1 = f1;
    }

    public int f1;

    public int m1() => f1;
}

public class Test2 : Test1
{
    public Test2(int f2, int f1) : base(f1)
    {
        this.f2 = f2;
    }

    public int f2;
}

/// <summary>
/// 下面使用代码动态创建的类和这个一样
/// </summary>
public class MyDynamicType
{
    private int m_number1;
    private string m_number2;

    public MyDynamicType() : this(42) { }
    public MyDynamicType(int initNumber)
    {
        m_number1 = initNumber;
    }

    public MyDynamicType(int initNumber, string str)
    {
        m_number1 = initNumber;
        m_number2 = str;
    }

    public MyDynamicType(string str, int initNumber) : this(initNumber, str) { }

    public int Number
    {
        get { return m_number1; }
        set { m_number1 = value; }
    }

    public int MyMethod(int multiplier)
    {
        return m_number1 * multiplier;
    }
}

public class DynamicCreateType
{
    /// <summary>
    /// 动态程序集名称
    /// </summary>
    private const string DynamicAssemblyName = "DynamicAssemblyExample";

    /// <summary>
    /// 创建MyDynamicTypes类型
    /// </summary>
    public static Type CreateMyDynamicType1()
    {
        // 创建一个DynamicAssemblyName名称的可执行并可由垃圾回收器自动回收的动态程序集
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(DynamicAssemblyName), AssemblyBuilderAccess.RunAndCollect);
        // 创建一个模块，在.NET Core 和 .NET 5+中一个动态程序集只能有一个动态模块
        ModuleBuilder module = assembly.DefineDynamicModule(DynamicAssemblyName);

        // 创建一个类型
        TypeBuilder dynamicType = module.DefineType("MyDynamicType", TypeAttributes.Public);

        // 添加一个int类型的私有字段
        FieldBuilder fbNumber1 = dynamicType.DefineField("m_number1", typeof(int), FieldAttributes.Private);
        FieldBuilder fbNumber2 = dynamicType.DefineField("m_number2", typeof(string), FieldAttributes.Private);

        // 定义接收一个参数的构造函数,并使用参数为私有字段赋值
        ConstructorBuilder ctor1 = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(int) });
        // 得到构造函数的IL中间语言指令
        ILGenerator ctor1IL = ctor1.GetILGenerator();
        // 加载实例
        ctor1IL.LoadArg(0);
        // 调用基类(object)构造方法
        ctor1IL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes)!);
        // 加载实例
        ctor1IL.LoadArg(0);
        // 加载第一个参数
        ctor1IL.LoadArg(1);
        // 向fbNumber字段赋值
        ctor1IL.Emit(OpCodes.Stfld, fbNumber1);
        // 从当前方法返回（方法完成）
        ctor1IL.Emit(OpCodes.Ret);

        // 定义一个无参构造函数，会执行ctor1构造函数
        ConstructorBuilder ctor0 = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
        ILGenerator ctor0IL = ctor0.GetILGenerator();
        ctor0IL.Emit(OpCodes.Ldarg_0);
        ctor0IL.Emit(OpCodes.Ldc_I4_S, 42);
        ctor0IL.Emit(OpCodes.Call, ctor1);
        ctor0IL.Emit(OpCodes.Ret);

        // 定义两个参数的构造函数
        ConstructorBuilder ctor2 = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(int), typeof(string) });
        ILGenerator ctor2IL = ctor2.GetILGenerator();
        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes)!);

        ctor2IL.DebugBreakPoint();

        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Ldarg_1);
        ctor2IL.Emit(OpCodes.Stfld, fbNumber1);

        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Ldarg_2);
        ctor2IL.Emit(OpCodes.Stfld, fbNumber2);
        ctor2IL.Emit(OpCodes.Ret);

        // 定义两个参数的构造函数
        ConstructorBuilder ctor2_1 = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(string), typeof(int) });
        ILGenerator ctor2IL_1 = ctor2_1.GetILGenerator();
        ctor2IL_1.Emit(OpCodes.Ldarg_0);
        ctor2IL_1.Emit(OpCodes.Ldarg_2);
        ctor2IL_1.Emit(OpCodes.Ldarg_1);
        ctor2IL_1.Emit(OpCodes.Call, ctor2);
        ctor2IL_1.Emit(OpCodes.Ret);


        // 定义一个名为 Number 的属性，用于获取和设置私有字段
        //
        // DefineProperty 的最后一个参数为空，因为该属性没有参数。
        // （如果不指定 null，则必须指定 Type 对象数组。对于无参数属性，请使用不带元素的内置数组：Type.EmptyTypes）
        PropertyBuilder pbNumber = dynamicType.DefineProperty("Number", PropertyAttributes.HasDefault, typeof(int), null);
        // 属性“set”和属性“get”方法需要一组特殊的属性
        MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

        // 为 Number 定义“get”访问器方法。 该方法返回一个整数并且没有参数
        MethodBuilder mbNumberGetAccessor = dynamicType.DefineMethod("get_Number", getSetAttr, typeof(int), Type.EmptyTypes);
        ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();
        // 在实例属性上，Ldarg_0 就是当前实例
        // 
        // 在实例中查找引用fbNumber字段并返回
        numberGetIL.Emit(OpCodes.Ldarg_0);
        numberGetIL.Emit(OpCodes.Ldfld, fbNumber1);
        numberGetIL.Emit(OpCodes.Ret);

        // 为 Number 定义“set”访问器方法，它没有返回类型，并且接受一个 int 类型的参数（Int32）
        MethodBuilder mbNumberSetAccessor = dynamicType.DefineMethod("set_Number", getSetAttr, null, new Type[] { typeof(int) });
        ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
        // 加载实例，然后加载数字参数，然后将参数存储在字段中
        numberSetIL.Emit(OpCodes.Ldarg_0);
        numberSetIL.Emit(OpCodes.Ldarg_1);
        numberSetIL.Emit(OpCodes.Stfld, fbNumber1);
        numberSetIL.Emit(OpCodes.Ret);

        // 将set，get方法映射到属性上
        pbNumber.SetGetMethod(mbNumberGetAccessor);
        pbNumber.SetSetMethod(mbNumberSetAccessor);

        // 定义一个接受整数参数并返回该整数与私有字段 m_number 的乘积的方法
        // 这一次，参数类型数组是动态创建的
        MethodBuilder meth = dynamicType.DefineMethod("MyMethod", MethodAttributes.Public, typeof(int), new Type[] { typeof(int) });
        ILGenerator methIL = meth.GetILGenerator();
        // 要检索私有实例字段，请加载它所属的实例（参数零）。
        // 加载字段后，加载参数一，然后相乘。
        // 从执行堆栈上的返回值（两个数字的乘积）的方法返回。
        methIL.Emit(OpCodes.Ldarg_0);
        methIL.Emit(OpCodes.Ldfld, fbNumber1);
        methIL.Emit(OpCodes.Ldarg_1);
        // 将两个值相乘
        methIL.Emit(OpCodes.Mul);
        methIL.Emit(OpCodes.Ret);

        // 完成
        return dynamicType.CreateType()!;
    }

    /// <summary>
    /// 创建MyDynamicTypes类型
    /// </summary>
    public static Type CreateMyDynamicType2()
    {
        // 创建一个DynamicAssemblyName名称的可执行并可由垃圾回收器自动回收的动态程序集
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(DynamicAssemblyName), AssemblyBuilderAccess.RunAndCollect);
        // 创建一个模块，在.NET Core 和 .NET 5+中一个动态程序集只能有一个动态模块
        ModuleBuilder module = assembly.DefineDynamicModule(DynamicAssemblyName);

        // 创建一个类型
        TypeBuilder dynamicType = module.DefineType("MyDynamicType", TypeAttributes.Public);

        {
            // 定义一个 测试本地变量的方法
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar1", MethodAttributes.Public, typeof(int[]), new Type[] { typeof(int), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();
            // 定义一个局部变量
            LocalBuilder localBuilder = methIL1.DeclareLocal(typeof(int[]));
            // 定义一个数组,数组 new int[参数1]
            methIL1.LoadArg(1);
            methIL1.NewArray(typeof(int));
            // 将数组赋值给局部变量
            methIL1.SetLocal(localBuilder);
            // 设置索引0的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 0);
            methIL1.LoadArg(2);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 设置索引1的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 1);
            methIL1.LoadArg(1);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 设置索引7的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 7);
            methIL1.LoadInt(999);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 返回局部变量
            methIL1.LoadLocal(localBuilder);

            methIL1.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar3", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();
            // 定义一个局部变量
            LocalBuilder localBuilder = methIL1.DeclareLocal(typeof(int[]));
            // 定义一个数组,数组 new int[参数1]
            methIL1.LoadArg(1);
            methIL1.NewArray(typeof(int));
            // 将数组赋值给局部变量
            methIL1.SetLocal(localBuilder);
            // 设置索引0的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 0);
            methIL1.LoadArg(2);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 设置索引1的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 1);
            methIL1.LoadArg(1);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 设置索引7的值
            methIL1.SetLocalArrayValueBefore(localBuilder, 7);
            methIL1.LoadInt(999);
            methIL1.SetLocalArrayValueAfter(typeof(int));
            // 返回局部变量
            methIL1.LoadLocal(localBuilder);
            methIL1.Emit(OpCodes.Ldlen);

            methIL1.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar4", MethodAttributes.Public, typeof(string), new Type[] { typeof(int), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();

            methIL1.Emit(OpCodes.Ldstr, "hhhhh");

            methIL1.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar5", MethodAttributes.Public, typeof(float), new Type[] { typeof(float), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();

            methIL1.LoadArg(1);
            methIL1.Emit(OpCodes.Neg);

            methIL1.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar6", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) }).GetILGenerator(); ;

            il.LoadArg(1);
            il.BitwiseNot();

            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar7", MethodAttributes.Public, typeof(object), new Type[] { typeof(int), typeof(int) }).GetILGenerator(); ;

            il.NewObject(typeof(object).GetConstructor(Type.EmptyTypes));

            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar8", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) }).GetILGenerator(); ;

            il.LoadArg(1);
            il.LoadArg(2);
            il.MathRem();

            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar9", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) }).GetILGenerator(); ;

            il.LoadArg(1);
            il.LoadArg(2);
            il.BitwiseShiftLeft();

            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar10", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) }).GetILGenerator(); ;

            il.LoadArg(1);
            il.LoadArg(2);
            il.BitwiseShiftRight();

            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar11", MethodAttributes.Public, typeof(int[]), new Type[] { typeof(Type) }).GetILGenerator();

            // 定义一个局部变量
            LocalBuilder localBuilder = il.DeclareLocal(typeof(int[]));
            // 定义一个数组,数组 new int[10]
            il.LoadInt(10);
            il.NewArray(typeof(int));
            il.SetLocal(localBuilder);

            il.SetLocalArrayValueBefore(localBuilder, 0);
            il.SizeOf(typeof(byte));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 1);
            il.SizeOf(typeof(Int16));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 2);
            il.SizeOf(typeof(Int32));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 3);
            il.SizeOf(typeof(Int64));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 4);
            il.SizeOf(typeof(object));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 5);
            il.SizeOf(typeof(string));
            il.SetArrayValue(typeof(int));

            il.SetLocalArrayValueBefore(localBuilder, 6);
            il.SizeOf(typeof(MyDynamicType));
            il.SetArrayValue(typeof(int));

            il.LoadLocal(localBuilder);
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar12", MethodAttributes.Public, typeof(IntPtr[]), new Type[] { typeof(IntPtr), typeof(IntPtr) }).GetILGenerator();

            // 定义一个局部变量
            LocalBuilder localBuilder = il.DeclareLocal(typeof(IntPtr[]));
            // 定义一个数组,数组 new int[10]
            il.LoadInt(2);
            il.NewArray(typeof(IntPtr));
            il.SetLocal(localBuilder);

            il.SetLocalArrayValueBefore(localBuilder, 0);
            il.LoadArg(1);
            il.SetArrayValue(typeof(IntPtr));

            il.SetLocalArrayValueBefore(localBuilder, 1);
            il.LoadArg(2);
            il.SetArrayValue(typeof(IntPtr));

            il.LoadLocal(localBuilder);
            il.Return();


        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar13", MethodAttributes.Public, typeof(IntPtr), new Type[] { typeof(IntPtr), typeof(IntPtr) }).GetILGenerator();

            MethodInfo? a = typeof(object).GetMethod("ToString");

            il.NewObject(typeof(object).GetConstructor(Type.EmptyTypes));
            il.LoadMethodPointer(a);

            il.Return();
        }

        {
            FieldInfo f1 = dynamicType.DefineField("TestField1", typeof(string), FieldAttributes.Public);
            FieldInfo f2 = dynamicType.DefineField("TestField2", typeof(int), FieldAttributes.Public);
            FieldInfo f3 = dynamicType.DefineField("TestField2", typeof(int), FieldAttributes.Public | FieldAttributes.Static);

            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar14", MethodAttributes.Public, typeof(string), new Type[] { typeof(int), typeof(string) }).GetILGenerator();

            il.LoadArg(0);
            il.LoadArg(1);
            il.SetField(f2);

            il.LoadArg(1);
            il.SetField(f3);

            il.LoadArg(0);
            il.LoadArg(2);
            il.SetField(f1);

            il.LoadArg(2);
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar15", MethodAttributes.Public, typeof(float), new Type[] { typeof(float), typeof(float) }).GetILGenerator();

            il.LoadArg(1);
            il.LoadArg(2);
            il.MathSub();
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar16", MethodAttributes.Public, typeof(string), new Type[] { typeof(string), typeof(int) }).GetILGenerator();

            LocalBuilder l1 = il.DeclareLocal(typeof(string));

            il.Emit(OpCodes.Ldloca, l1.LocalIndex);
            il.Emit(OpCodes.Ldarga, 1);
            il.LoadArg(2);
            il.Emit(OpCodes.Cpblk);

            il.LoadLocal(l1);
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar17", MethodAttributes.Public, typeof(Test1[]), new Type[] { typeof(Test1), typeof(int) }).GetILGenerator();

            LocalBuilder l1 = il.DeclareLocal(typeof(Test1[]));
            il.LoadInt(2);
            il.NewArray(typeof(Test1));
            il.SetLocal(l1);

            il.LoadLocal(l1);
            il.LoadInt(0);
            il.LoadArg(1);
            il.SetArrayValue();

            il.LoadLocal(l1);
            il.LoadInt(1);
            il.Emit(OpCodes.Ldelema, typeof(Test1));
            il.Emit(OpCodes.Ldarga, 1);
            il.SizeOf(typeof(Test1));
            il.Emit(OpCodes.Cpblk);

            FieldInfo fieldInfo = typeof(Test1).GetField("f1")!;
            il.LoadArg(1);
            il.LoadArg(2);
            il.SetField(fieldInfo);

            il.LoadLocal(l1);
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar18", MethodAttributes.Public, typeof(object), new Type[] { typeof(object), typeof(object) }).GetILGenerator();

            il.LoadArgAddr(2);
            il.Emit(OpCodes.Ldind_Ref);
            //il.UnBox(typeof(string));
            il.Return();
        }

        {
            // 定义一个 测试本地变量的方法
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar19", MethodAttributes.Public, typeof(object), new Type[] { typeof(object), typeof(object) }).GetILGenerator();

            il.LoadRuntimeHandle(typeof(string));
            il.Return();
        }

        {
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar20", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) }).GetILGenerator();

            LocalBuilder l1 = il.DeclareLocal(typeof(int));
            il.LoadLocalAddr(l1.LocalIndex);
            il.LoadArg(1);
            il.Emit(OpCodes.Stind_I4);

            il.LoadLocal(l1);
            il.Return();
        }

        {
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar21", MethodAttributes.Public, typeof(object), new Type[] { typeof(object), typeof(object) }).GetILGenerator();

            LocalBuilder l1 = il.DeclareLocal(typeof(object));
            il.LoadLocalAddr(l1.LocalIndex);
            il.LoadArg(1);
            il.Emit(OpCodes.Stind_Ref);

            il.LoadLocal(l1);
            il.Return();
        }

        {
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar22", MethodAttributes.Public, typeof(object), new Type[] { typeof(object), typeof(object) }).GetILGenerator();

            LocalBuilder l1 = il.DeclareLocal(typeof(object));
            il.LoadLocalAddr(l1.LocalIndex);
            il.LoadArg(1);
            il.Emit(OpCodes.Stobj, typeof(object));

            il.LoadLocal(l1);
            il.Return();
        }

        {
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar23", MethodAttributes.Public, typeof(object), new Type[] { typeof(Test1), typeof(int) }).GetILGenerator();

            il.LoadArg(1);
            il.Return();
        }

        {
            ILGenerator il = dynamicType.DefineMethod("TestLoaclVar24", MethodAttributes.Public, typeof(int), new Type[] { typeof(Test1), typeof(object) }).GetILGenerator();

            MethodInfo m1 = typeof(Test1).GetMethod("m1")!;

            il.LoadArg(1);
            il.Call(m1);

            il.Return();
        }


        {
            // 定义一个 测试本地变量的方法
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar2", MethodAttributes.Public, typeof(float), new Type[] { typeof(int), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();
            // 定义一个局部变量
            LocalBuilder localBuilder = methIL1.DeclareLocal(typeof(int[]));
            //methIL1.LoadArg(0);
            methIL1.LoadArg(1);
            methIL1.ConvertFloat();
            methIL1.LoadArg(2);
            methIL1.ConvertFloat();
            methIL1.MathDiv();
            methIL1.Box(typeof(float));
            //methIL1.Emit(OpCodes.Unbox_Any,typeof(float));
            methIL1.Emit(OpCodes.Unbox, typeof(float));
            methIL1.Emit(OpCodes.Ldobj, typeof(float));

            methIL1.Return();
        }

        // 完成
        return dynamicType.CreateType()!;
    }


    public static Type CreateMyDynamicType3()
    {
        AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(DynamicAssemblyName), AssemblyBuilderAccess.RunAndCollect);
        ModuleBuilder myModBuilder = assembly.DefineDynamicModule("MyJumpTableDemo");

        TypeBuilder myTypeBuilder = myModBuilder.DefineType("JumpTableDemo", TypeAttributes.Public);
        MethodBuilder myMthdBuilder = myTypeBuilder.DefineMethod("SwitchMe", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(int) });

        ILGenerator myIL = myMthdBuilder.GetILGenerator();


        Label defaultCase = myIL.DefineLabel();
        Label endOfMethod = myIL.DefineLabel();


        // 我们正在初始化我们的跳转表。 请注意，稍后将使用 MarkLabel 方法放置标签。

        Label[] jumpTable = new Label[] {
            myIL.DefineLabel(),
            myIL.DefineLabel(),
            myIL.DefineLabel(),
            myIL.DefineLabel(),
            myIL.DefineLabel()
        };

        // arg0，我们传递的数字，被压入堆栈。
        // 在这种情况下，由于代码示例的设计，压入堆栈的值恰好与标签的索引匹配（在IL术语中，跳转表中的偏移量的索引）。
        // 如果不是这种情况，例如在基于非整数值进行切换时，则必须在 ILGenerator.Emit 调用之外建立可能的 case 值与偏移量的每个索引之间的对应关系的规则，就像编译器一样。

        myIL.Emit(OpCodes.Ldarg_0);
        myIL.Emit(OpCodes.Switch, jumpTable);

        // 默认情况下的分支
        myIL.Emit(OpCodes.Br_S, defaultCase);

        // Case arg0 = 0
        myIL.MarkLabel(jumpTable[0]);
        myIL.Emit(OpCodes.Ldstr, "不是香蕉");
        myIL.Emit(OpCodes.Br_S, endOfMethod);

        // Case arg0 = 1
        myIL.MarkLabel(jumpTable[1]);
        myIL.Emit(OpCodes.Ldstr, "一个香蕉");
        myIL.Emit(OpCodes.Br_S, endOfMethod);

        // Case arg0 = 2
        myIL.MarkLabel(jumpTable[2]);
        myIL.Emit(OpCodes.Ldstr, "两个香蕉");
        myIL.Emit(OpCodes.Br_S, endOfMethod);

        // Case arg0 = 3
        myIL.MarkLabel(jumpTable[3]);
        myIL.Emit(OpCodes.Ldstr, "三个香蕉");
        myIL.Emit(OpCodes.Br_S, endOfMethod);

        // Case arg0 = 4
        myIL.MarkLabel(jumpTable[4]);
        myIL.Emit(OpCodes.Ldstr, "四个香蕉");
        myIL.Emit(OpCodes.Br_S, endOfMethod);

        // Default case
        myIL.MarkLabel(defaultCase);
        myIL.Emit(OpCodes.Ldstr, "很多香蕉");

        myIL.MarkLabel(endOfMethod);
        myIL.Emit(OpCodes.Ret);

        return myTypeBuilder.CreateType();
    }
}
