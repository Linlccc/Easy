namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// HelloWorld 类型创建器
/// </summary>
public static class HelloWorldCreator
{
    // 类型
    private static TypeBuilder _type_HelloWorld;
    // 字段
    private static FieldBuilder _field_num;
    private static FieldBuilder _field_text;
    // 属性
    private static PropertyBuilder _prop_Txt;
    // 构造函数
    private static ConstructorBuilder _ctor_2arg;
    private static ConstructorBuilder _ctor_1arg;
    private static ConstructorBuilder _ctor_0arg;

    /// <summary>
    /// 添加HelloWorld类
    /// </summary>
    /// <param name="moduleBuilder">模块构建器</param>
    /// <returns>HelloWorld类型</returns>
    public static Type DefineType_HelloWorld(this ModuleBuilder moduleBuilder)
    {
        // 定义类型
        _type_HelloWorld = moduleBuilder.DefineType("HelloWorld", TypeAttributes.Public);
        // 定义字段
        _field_num = _type_HelloWorld.DefineField("_num", typeof(int), FieldAttributes.Private);
        _field_text = _type_HelloWorld.DefineField("_text", typeof(string), FieldAttributes.Private);
        // 定义属性
        _prop_Txt = DefineAutoProp(typeof(string), "Txt");
        // 定义构造函数
        _ctor_2arg = DefineCtor_2Arg();
        _ctor_1arg = DefineCtor_1Arg();
        _ctor_0arg = DefineCtor_0Arg();
        // WriteHello 方法
        DefineMethod_WriteHello();
        // WriteTxt 方法
        DefineMethod_WriteTxt();
        // Mul 方法
        DefineMethod_Mul();

        //// 生成类型,并返回
        return _type_HelloWorld.CreateType();
    }

    /// <summary>
    /// 定义两个参数的构造函数
    /// </summary>
    private static ConstructorBuilder DefineCtor_2Arg()
    {
        // 定义一个 公开，两个参的构造函数
        ConstructorBuilder ctor = _type_HelloWorld.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.Standard, [typeof(int), typeof(string)]);
        ILGenerator il = ctor.GetILGenerator();

        // object.ctor
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
        // this._num = arg1;
        il.LoadArg(0);
        il.LoadArg(1);
        il.SetField(_field_num);
        // this._text = arg2;
        il.LoadArg(0);
        il.LoadArg(2);
        il.SetField(_field_text);
        // this.Txt = arg2 + " - " + arg1;
        il.LoadArg(0);
        il.LoadArg(2);
        il.LoadString(" - ");
        il.LoadArgAddr(1);
        il.Call(typeof(int).GetMethod("ToString", Type.EmptyTypes));
        il.Call(typeof(string).GetMethod("Concat", [typeof(string), typeof(string), typeof(string)]));
        il.Call(_prop_Txt.GetSetMethod());
        // return;
        il.Return();

        return ctor;
    }
    /// <summary>
    /// 定义一个参数的构造函数,直接去调用另一个构造函数
    /// </summary>
    private static ConstructorBuilder DefineCtor_1Arg()
    {
        ConstructorBuilder ctor = _type_HelloWorld.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, [typeof(int)]);
        ILGenerator il = ctor.GetILGenerator();

        // this(arg1,"aa");
        il.LoadArg(0);
        il.LoadArg(1);
        il.LoadString("aa");
        il.Call(_ctor_2arg);
        il.Return();

        return ctor;
    }
    /// <summary>
    /// 定义无参数的构造函数,直接去调用另一个构造函数
    /// </summary>
    private static ConstructorBuilder DefineCtor_0Arg()
    {
        ConstructorBuilder ctor = _type_HelloWorld.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
        ILGenerator il = ctor.GetILGenerator();

        // this(100);
        il.LoadArg(0);
        il.LoadInt(100);
        il.Call(_ctor_1arg);
        il.Return();

        return ctor;
    }

    /// <summary>
    /// 定义自动属性
    /// </summary>
    /// <param name="typeBuilder">类型构建器</param>
    /// <param name="type">属性类型</param>
    /// <param name="propName">属性名称</param>
    /// <returns>属性使用的字段</returns>
    private static PropertyBuilder DefineAutoProp(Type type, string propName)
    {
        // 定义属性
        PropertyBuilder p1 = _type_HelloWorld.DefineProperty(propName, PropertyAttributes.HasDefault, type, Type.EmptyTypes);
        // 定义属性字段
        FieldBuilder p_f1 = _type_HelloWorld.DefineField($"<{propName}>k__BackingField", type, FieldAttributes.Private);

        // get
        MethodBuilder m_Get = _type_HelloWorld.DefineMethod($"get_{propName}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, type, Type.EmptyTypes);
        ILGenerator il_Get = m_Get.GetILGenerator();
        il_Get.LoadArg(0);
        il_Get.LoadField(p_f1);
        il_Get.Return();
        p1.SetGetMethod(m_Get);

        // set
        MethodBuilder m_Set = _type_HelloWorld.DefineMethod($"set_{propName}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, [type]);
        ILGenerator il_Set = m_Set.GetILGenerator();
        il_Set.LoadArg(0);
        il_Set.LoadArg(1);
        il_Set.SetField(p_f1);
        il_Set.Return();
        p1.SetSetMethod(m_Set);

        return p1;
    }

    /// <summary>
    /// 定义一个名为 WriteHello 的方法
    /// </summary>
    private static MethodBuilder DefineMethod_WriteHello()
    {
        MethodBuilder method = _type_HelloWorld.DefineMethod("WriteHello", MethodAttributes.Public | MethodAttributes.Static, typeof(void), Type.EmptyTypes);
        ILGenerator il = method.GetILGenerator();

        // Console.WriteLine("Hello World!");
        il.LoadString("Hello World!");
        il.Call(typeof(Console).GetMethod("WriteLine", [typeof(string)]));
        il.Return();

        return method;
    }
    /// <summary>
    /// 定义一个名为 WriteTxt 的方法
    /// </summary>
    private static MethodBuilder DefineMethod_WriteTxt()
    {
        MethodBuilder method = _type_HelloWorld.DefineMethod("WriteTxt", MethodAttributes.Public, typeof(void), Type.EmptyTypes);
        ILGenerator il = method.GetILGenerator();

        // Console.WriteLine(Txt);
        il.LoadArg(0);
        il.Call(_prop_Txt.GetGetMethod());
        il.Call(typeof(Console).GetMethod("WriteLine", [typeof(string)]));
        il.Return();

        return method;
    }
    /// <summary>
    /// 定义一个名为 Mul 的方法
    /// </summary>
    private static MethodBuilder DefineMethod_Mul()
    {
        MethodBuilder methodBuilder = _type_HelloWorld.DefineMethod("Mul", MethodAttributes.Public, typeof(int), [typeof(int)]);
        ILGenerator il = methodBuilder.GetILGenerator();

        // return _field_num * arg1;
        il.LoadArg(0);
        il.LoadField(_field_num);
        il.LoadArg(1);
        il.MathMul();
        il.Return();

        return methodBuilder;
    }
}
