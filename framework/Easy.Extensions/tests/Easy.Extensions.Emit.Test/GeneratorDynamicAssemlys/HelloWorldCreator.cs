namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// 这是以下代码动态创建的类
/// </summary>
public class HelloWorld
{
    private int m_number1;

    private string m_number2;

    public int Number
    {
        get
        {
            return m_number1;
        }
        set
        {
            m_number1 = value;
        }
    }

    public HelloWorld(int P_0)
    {
        m_number1 = P_0;
    }

    public HelloWorld()
        : this(42)
    {
    }


    public HelloWorld(int P_0, string P_1)
        : this(P_0)
    {
        m_number2 = P_1;
    }

    public static void Main()
    {
        Console.WriteLine("Hello World!");
    }

    public int Mul(int P_0)
    {
        return m_number1 * P_0;
    }
}

/// <summary>
/// HelloWorld 类型创建器
/// </summary>
public static class HelloWorldCreator
{
    /// <summary>
    /// 添加HelloWorld类
    /// </summary>
    /// <param name="moduleBuilder">模块构建器</param>
    /// <returns>HelloWorld类型</returns>
    public static Type DefineType_HelloWorld(this ModuleBuilder moduleBuilder)
    {
        // 定义类型构建器
        TypeBuilder typeBuilder = moduleBuilder.DefineType("HelloWorld", TypeAttributes.Public);

        // 定义字段
        FieldBuilder fbNumber1 = typeBuilder.DefineField("m_number1", typeof(int), FieldAttributes.Private);
        FieldBuilder fbNumber2 = typeBuilder.DefineField("m_number2", typeof(string), FieldAttributes.Private);

        // 定义构造函数
        ConstructorBuilder ctor1 = DefineCtor_1();
        ConstructorBuilder ctor2 = DefineCtor_2();
        ConstructorBuilder ctor3 = DefineCtor_3();

        // 定义属性
        DefineProperty_Number();

        // 定义方法
        DefineMethod_Main();
        DefineMethod_Mul();

        // 生成类型,并返回
        return typeBuilder.CreateType();

        // 定义构造函数
        ConstructorBuilder DefineCtor_1()
        {
            // 定义一个 公开，一个参数的构造函数
            ConstructorBuilder ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(int) });
            ILGenerator il = ctor.GetILGenerator();
            // 将 this 推送到堆栈上
            il.Emit(OpCodes.Ldarg_0);
            // 调用基类(object)构造方法
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes)!);
            // 加载实例
            il.Emit(OpCodes.Ldarg_0);
            // 加载第一个参数
            il.Emit(OpCodes.Ldarg_1);
            // 向 fbNumber 字段赋值
            il.Emit(OpCodes.Stfld, fbNumber1);
            // 方法结束
            il.Emit(OpCodes.Ret);

            return ctor;
        }

        // 定义构造函数
        ConstructorBuilder DefineCtor_2()
        {
            // 定义一个 公开，无参的构造函数
            ConstructorBuilder ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4_S, 42);
            il.Emit(OpCodes.Call, ctor1);
            il.Emit(OpCodes.Ret);
            return ctor;
        }

        // 定义构造函数
        ConstructorBuilder DefineCtor_3()
        {
            // 定义一个 公开，两个参的构造函数
            ConstructorBuilder ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(int), typeof(string) });
            ILGenerator il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, ctor1);
            //il.DebugBreakPoint();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Stfld, fbNumber2);
            il.Emit(OpCodes.Ret);
            return ctor;
        }

        // 定义 Number 属性
        PropertyBuilder DefineProperty_Number()
        {
            // 定义一个 Number 属性
            PropertyBuilder property = typeBuilder.DefineProperty("Number", PropertyAttributes.HasDefault, typeof(int), null);

            // 属性“set”和属性“get”方法需要一组特殊的属性
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // 定义 get 方法
            MethodBuilder getMethod = typeBuilder.DefineMethod("get_Number", getSetAttr, typeof(int), Type.EmptyTypes);
            ILGenerator getIl = getMethod.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fbNumber1);
            getIl.Emit(OpCodes.Ret);

            // 定义 set 方法
            MethodBuilder setMethod = typeBuilder.DefineMethod("set_Number", getSetAttr, null, new Type[] { typeof(int) });
            ILGenerator setIl = setMethod.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fbNumber1);
            setIl.Emit(OpCodes.Ret);

            // 将 get 方法和 set 方法加入到属性中
            property.SetGetMethod(getMethod);
            property.SetSetMethod(setMethod);

            return property;
        }

        // 定义 Main 方法
        MethodBuilder DefineMethod_Main()
        {
            // 定义一个 公开,无参数的静态方法
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { });
            // 获取中间语言指令生成器
            ILGenerator il = methodBuilder.GetILGenerator();
            // 推送一个字符串到堆栈
            il.Emit(OpCodes.Ldstr, "Hello World!");
            // 调用Console.WriteLine方法
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            // 方法结束
            il.Emit(OpCodes.Ret);
            return methodBuilder;
        }

        // 定义 Mul 方法
        MethodBuilder DefineMethod_Mul()
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Mul", MethodAttributes.Public, typeof(int), new Type[] { typeof(int) });
            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fbNumber1);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);
            return methodBuilder;
        }
    }
}
