using System.Reflection.Emit;
using System.Reflection;

namespace Easy.Extensions.DynamicProxy.Test;

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

    public MyDynamicType(int initNumber,string str)
    {
        m_number1 = initNumber;
        m_number2 = str;
    }

    public MyDynamicType(string str,int initNumber):this(initNumber, str) { }

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
        ConstructorBuilder ctor2 = dynamicType.DefineConstructor(MethodAttributes.Public,CallingConventions.Standard,new Type[] { typeof(int),typeof(string) });
        ILGenerator ctor2IL = ctor2.GetILGenerator();
        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Call,typeof(object).GetConstructor(Type.EmptyTypes)!);

        ctor2IL.DebugBreakPoint();

        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Ldarg_1);
        ctor2IL.Emit(OpCodes.Stfld, fbNumber1);

        ctor2IL.Emit(OpCodes.Ldarg_0);
        ctor2IL.Emit(OpCodes.Ldarg_2);
        ctor2IL.Emit(OpCodes.Stfld, fbNumber2);
        ctor2IL.Emit(OpCodes.Ret);

        // 定义两个参数的构造函数
        ConstructorBuilder ctor2_1 = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(string),typeof(int) });
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
            methIL1.DefineArray(typeof(int));
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
            methIL1.DefineArray(typeof(int));
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
            MethodBuilder meth1 = dynamicType.DefineMethod("TestLoaclVar2", MethodAttributes.Public, typeof(float), new Type[] { typeof(int), typeof(int) });
            ILGenerator methIL1 = meth1.GetILGenerator();
            // 定义一个局部变量
            LocalBuilder localBuilder = methIL1.DeclareLocal(typeof(int[]));
            //methIL1.LoadArg(0);
            methIL1.LoadArg(1);
            methIL1.ConvertFloat();
            methIL1.LoadArg(2);
            methIL1.ConvertFloat();
            methIL1.Emit(OpCodes.Div);
            methIL1.Box(typeof(float));
            methIL1.ConvertFloat();
            methIL1.Return();
        }

        // 完成
        return dynamicType.CreateType()!;
    }
}
