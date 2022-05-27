namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// 这是以下代码动态创建的类
/// </summary>
public class EmitOpCodesVerify
{
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
        ArgIterator argIterator = new ArgIterator(__arglist);
        object[] array = new object[argIterator.GetRemainingCount()];
        while (argIterator.GetRemainingCount() > 0)
        {
            array[num] = TypedReference.ToObject(argIterator.GetNextArg());
            num++;
        }
        return array;
    }
}


/// <summary>
/// Emit 指令验证
/// </summary>
public static class EmitOpCodesVerifyCreator
{
    public static Type DefineType_EmitOpCodesVerify(this ModuleBuilder moduleBuilder)
    {
        // 定义类型构建器
        TypeBuilder typeBuilder = moduleBuilder.DefineType("EmitOpCodesVerify", TypeAttributes.Public);
        // +
        DefineMethod_Add1(typeBuilder);
        DefineMethod_Add2(typeBuilder);
        // -
        DefineMethod_Sub1(typeBuilder);
        // *
        DefineMethod_Mul1(typeBuilder);
        // /
        DefineMethod_Div1(typeBuilder);
        // %
        DefineMethod_Rem1(typeBuilder);
        // -arg
        DefineMethod_Neg1(typeBuilder);

        // &
        DefineMethod_And1(typeBuilder);
        // |
        DefineMethod_Or1(typeBuilder);
        // ^
        DefineMethod_Xor1(typeBuilder);
        // ~arg
        DefineMethod_Not1(typeBuilder);
        // <<
        DefineMethod_ShiftLeft1(typeBuilder);
        // >>
        DefineMethod_ShiftRight1(typeBuilder);

        // ==
        DefineMethod_Equal1(typeBuilder);
        DefineMethod_Equal2(typeBuilder);
        // >
        DefineMethod_Greater1(typeBuilder);
        // <
        DefineMethod_Less1(typeBuilder);

        // 字符串拼接
        DefineMethod_StringAdd1(typeBuilder);


        // ** 特殊
        // 定义一个执行可变参数的方法，里面包含定义一个有可变参数的方法
        // Arglist 个数不定，类型不定参数
        DefineMethod_Arglist_Invoke1(typeBuilder);


        // ** 类型转换
        // (object)int
        DefineMethod_Box1(typeBuilder);
        // (int)obj
        DefineMethod_UnBox1(typeBuilder);
        // (int)obj
        DefineMethod_UnBox2(typeBuilder);
        // float to int
        DefineMethod_ConvertInteger1(typeBuilder);
        // as 
        DefineMethod_As1(typeBuilder);



        // ** 调用方法
        // 普通指令调用虚方法
        DefineMethod_CallVirtual1(typeBuilder);
        // 普通指令调用虚方法
        DefineMethod_CallVirtual2(typeBuilder);
        // 使用调用虚方法的指令调用虚方法
        DefineMethod_CallVirtual3(typeBuilder);
        DefineMethod_CallVirtual4(typeBuilder);


        // ** 数组
        // 获取数组元素
        DefineMethod_Array1(typeBuilder);
        // 获取数组元素
        DefineMethod_Array2(typeBuilder);
        // 返回数组中某个索引的值的地址的值
        DefineMethod_Array3(typeBuilder);
        // 返回数组长度
        DefineMethod_Array4(typeBuilder);



        // ** 地址
        // SetValueToAddr
        DefineMethod_SetValueToAddr1(typeBuilder);
        DefineMethod_SetValueToAddr2(typeBuilder);


        // sizeof
        DefineMethod_SizeOf1(typeBuilder);


        // set field
        DefineMethod_SetField1(typeBuilder);


        // typeof
        DefineMethod_Typeof1(typeBuilder);



        // CopyAddrValueToAddr1
        DefineMethod_CopyAddrValueToAddr1(typeBuilder);







        DefineMethod_Test1(typeBuilder);
        DefineMethod_Test2(typeBuilder);
        DefineMethod_Test3(typeBuilder);
        DefineMethod_Test4(typeBuilder);


        return typeBuilder.CreateType();
    }

    #region 数学
    #region Add
    /// <summary>
    /// 定义 Add1 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Add1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Add1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(3);
        il.NewArray(typeof(int));
        il.SetLocal(arr);
        // 声明本地变量
        LocalBuilder i1 = il.DeclareLocal(typeof(int));
        LocalBuilder i2 = il.DeclareLocal(typeof(int));
        LocalBuilder i3 = il.DeclareLocal(typeof(int));

        // 正常添加
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd();
        il.SetLocal(i1);
        // 检查溢出添加
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(true);
        il.SetLocal(i2);
        // 检查溢出无符号添加
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(isUnsigned: true);
        il.SetLocal(i3);

        // 给数组赋值
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadLocal(i1);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadLocal(i2);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadLocal(i3);
        il.SetArray(typeof(int));

        // 返回数组
        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }

    /// <summary>
    /// 定义 Add2 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Add2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Add2", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(3);
        il.NewArray(typeof(int));
        il.SetLocal(arr);

        // 正常添加
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd();
        il.SetArray(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(true);
        il.SetArray(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(isUnsigned: true);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Sub
    /// <summary>
    /// 定义 Sub1 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Sub1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Sub1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(3);
        il.NewArray(typeof(int));
        il.SetLocal(arr);

        // 正常添加
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.MathSub();
        il.SetArray(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathSub(true);
        il.SetArray(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathSub(isUnsigned: true);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Mul
    /// <summary>
    /// 定义 Mul1 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Mul1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Mul1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(3);
        il.NewArray(typeof(int));
        il.SetLocal(arr);

        // 正常添加
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.MathMul();
        il.SetArray(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathMul(true);
        il.SetArray(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathMul(isUnsigned: true);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Div
    /// <summary>
    /// 定义 Div1 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Div1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Div1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(2);
        il.NewArray(typeof(int));
        il.SetLocal(arr);

        // 正常添加
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.MathDiv();
        il.SetArray(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathDiv(isUnsigned: true);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region Rem
    /// <summary>
    /// 定义 Rem1 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_Rem1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Rem1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();
        // 声明本地变量数组
        LocalBuilder arr = il.DeclareLocal(typeof(int[]));
        il.LoadInt(2);
        il.NewArray(typeof(int));
        il.SetLocal(arr);

        // 正常添加
        il.LoadLocal(arr);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.MathRem();
        il.SetArray(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathRem(isUnsigned: true);
        il.SetArray(typeof(int));

        il.LoadLocal(arr);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 求反
    // -arg
    public static MethodBuilder DefineMethod_Neg1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Neg1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.MathNeg();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }
    #endregion
    #endregion

    #region 计算
    // &
    public static MethodBuilder DefineMethod_And1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("And1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseAnd();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // |
    public static MethodBuilder DefineMethod_Or1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Or1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseOr();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // ^
    public static MethodBuilder DefineMethod_Xor1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Xor1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseXor();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // ~arg
    public static MethodBuilder DefineMethod_Not1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Not1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.BitwiseNot();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // <<
    public static MethodBuilder DefineMethod_ShiftLeft1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("ShiftLeft1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseShiftLeft();
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // >>
    public static MethodBuilder DefineMethod_ShiftRight1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("ShiftRight1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int[]));
        il.LoadInt(2);
        il.NewArray(typeof(int));
        il.SetLocal(l1);

        // 正常右移位
        il.LoadLocal(l1);
        il.LoadInt(0);
        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseShiftRight();
        il.SetArray(typeof(int));

        // 无符号右移位
        il.LoadLocal(l1);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseShiftRight(true);
        il.SetArray(typeof(int));

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 比较
    // ==
    public static MethodBuilder DefineMethod_Equal1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Equal1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.CompareEqual();
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // ==
    public static MethodBuilder DefineMethod_Equal2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Equal2", MethodAttributes.Public | MethodAttributes.Static, typeof(bool), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(bool));

        il.LoadArg();
        il.LoadArg(1);
        il.CompareEqual();
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // >
    public static MethodBuilder DefineMethod_Greater1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Greater1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.CompareGreater();
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }

    // <
    public static MethodBuilder DefineMethod_Less1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Less1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(int), typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadArg();
        il.LoadArg(1);
        il.CompareLess();
        il.SetLocal(l1);

        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 字符串相加
    /// <summary>
    /// 定义 StringAdd1(字符串相加) 方法
    /// </summary>
    /// <returns></returns>
    public static MethodBuilder DefineMethod_StringAdd1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("StringAdd1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string), typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(string));

        il.LoadArg();
        il.LoadArg(1);
        il.Call(typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string) }));
        il.SetLocal(0);

        il.LoadLocal(0);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 特殊
    // 使用 Arglist 示例
    public static MethodBuilder DefineMethod_Arglist1(TypeBuilder typeBuilder)
    {
        // 需要定义 CallingConventions.VarArgs 就会在方法参数默认添加一个 __arglist 参数
        // 该方法不能使用反射调用所以写不了测试,如需测试引用生成的程序集然后调用 EmitOpCodesVerify.Arglist1(__arglist(1, 2, "aa", "d", 4.6, new { A = 1 }));

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Arglist1", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.VarArgs, typeof(object[]), Type.EmptyTypes);
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
        il.LoadLocalAddr(lb_varargs.LocalIndex); // 因为构造 ArgIterator 类型返回的是地址，所以加载地址去接收
        il.LoadVarArgs();
        il.Call(typeof(ArgIterator).GetConstructor(new Type[] { typeof(RuntimeArgumentHandle) }));

        // lb_result = new object[lb_varargs.GetRemainingCount()];
        il.LoadLocalAddr(lb_varargs.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod("GetRemainingCount"));
        il.NewArray(typeof(object));
        il.SetLocal(lb_result);

        // if(lb_havearg = lb_varargs.GetRemainingCount() < 0) return lb_result;
        il.MarkLabel(haveValueL);
        il.LoadLocalAddr(lb_varargs.LocalIndex);
        il.Call(typeof(ArgIterator).GetMethod("GetRemainingCount"));
        il.LoadInt(0);
        il.CompareGreater();
        il.GotoIfFalse(returnL);

        // lb_result[lb_currentIndex] = lb_varargs.GetNextArg();
        il.Nop();
        il.LoadLocal(lb_result);
        il.LoadLocal(lb_currentIndex);
        il.LoadLocalAddr(lb_varargs.LocalIndex);
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
    public static MethodBuilder DefineMethod_Arglist_Invoke1(TypeBuilder typeBuilder)
    {
        // 有可变参数的方法
        MethodBuilder varargMethod = DefineMethod_Arglist1(typeBuilder);

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Arglist_Invoke1", MethodAttributes.Public | MethodAttributes.Static, typeof(object[]), new Type[] { typeof(object), typeof(object), typeof(object), typeof(object) });
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

    #region 类型转换
    // box
    public static MethodBuilder DefineMethod_Box1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Box1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(int) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadArg(0);
        il.Box(typeof(int));
        il.Return();

        return methodBuilder;
    }
    //unbox_any
    public static MethodBuilder DefineMethod_UnBox1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("UnBox1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(object) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadArg(0);
        il.UnBox(typeof(int));
        il.Return();

        return methodBuilder;
    }
    //unbox
    public static MethodBuilder DefineMethod_UnBox2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("UnBox2", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(object) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadArg(0);
        il.UnBoxThenLoadPointer(typeof(int));
        il.LoadAddrValue(typeof(int));
        il.Return();

        return methodBuilder;
    }

    //float to int
    public static MethodBuilder DefineMethod_ConvertInteger1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("ConvertInteger1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), new Type[] { typeof(float) });
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadArg(0);
        il.ConvertInteger(typeof(int));
        il.Return();

        return methodBuilder;
    }

    // as
    public static MethodBuilder DefineMethod_As1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("AS1", MethodAttributes.Public | MethodAttributes.Static, typeof(Type), new Type[] { typeof(object) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(Type));

        //il.BreakPoint();
        il.LoadArg(0);
        il.As(typeof(Type));
        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 调用方法
    // 使用调用普通方法的指令调用虚方法 编译时类型 EmitTest1 (调用定义的虚方法而不是类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("CallVirtual1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.Call(typeof(EmitTest1).GetMethod("T1", new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 使用调用普通方法的指令调用虚方法 编译时类型 EmitTest2(调用定义的虚方法而不是类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("CallVirtual2", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.Call(typeof(EmitTest1).GetMethod("T1", new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 使用调用虚方法的指令调用虚方法 编译时类型 EmitTest1(调用类型重写的方法)
    public static MethodBuilder DefineMethod_CallVirtual3(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("CallVirtual3", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));
        LocalBuilder l2 = il.DeclareLocal(typeof(string));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // l2 = l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.CallVirtual(typeof(EmitTest1).GetMethod("T1", new Type[] { typeof(string) }));
        il.SetLocal(l2);
        // return l2;
        il.LoadLocal(l2);
        il.Return();

        return methodBuilder;
    }

    // 将方法标记为最后的方法
    public static MethodBuilder DefineMethod_CallVirtual4(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("CallVirtual4", MethodAttributes.Public | MethodAttributes.Static, typeof(string), new Type[] { typeof(string) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest1));

        // l1 = new EmitTest2();
        il.NewObject(typeof(EmitTest2).GetConstructor(Type.EmptyTypes));
        il.SetLocal(l1);

        // return l1.T1(arg1);
        il.LoadLocal(l1);
        il.LoadArg(0);
        il.TailCall();
        il.CallVirtual(typeof(EmitTest1).GetMethod("T1", new Type[] { typeof(string) }));
        il.Return();

        return methodBuilder;
    }
    #endregion

    #region 数组
    // 返回数组中某个索引的值
    public static MethodBuilder DefineMethod_Array1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Array1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
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
        //il.LoadArrayIndexValue(typeof(int));
        il.GetArrayIndexInteger(typeof(int));
        il.Box(typeof(int));
        il.Return();
        return methodBuilder;
    }

    // 返回数组中某个索引的值
    public static MethodBuilder DefineMethod_Array2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Array2", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
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
    public static MethodBuilder DefineMethod_Array3(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Array3", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
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
    public static MethodBuilder DefineMethod_Array4(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Array4", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
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
    public static MethodBuilder DefineMethod_SetValueToAddr1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("SetValueToAddr1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int));

        il.LoadLocalAddr(l1.LocalIndex);
        il.LoadInt(10);
        il.SetValueToAddr(typeof(int));

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // SetValueToAddr
    public static MethodBuilder DefineMethod_SetValueToAddr2(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("SetValueToAddr2", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(object));

        il.LoadLocalAddr(l1.LocalIndex);
        il.NewObject(typeof(object).GetConstructor(Type.EmptyTypes));
        il.SetValueToAddr(typeof(object));

        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }
    #endregion

    // sizeof
    public static MethodBuilder DefineMethod_SizeOf1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("SizeOf1", MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(int[]));

        il.LoadInt(8);
        il.NewArray(typeof(int));

        il.Copy();
        il.LoadInt(0);
        il.SizeOf(typeof(byte));
        il.SetArray(typeof(int));

        il.Copy();
        il.LoadInt(1);
        il.SizeOf(typeof(Int16));
        il.SetArray();

        il.Copy();
        il.LoadInt(2);
        il.SizeOf(typeof(int));
        il.SetArray();

        il.Copy();
        il.LoadInt(3);
        il.SizeOf(typeof(Int64));
        il.SetArray();

        il.Copy();
        il.LoadInt(4);
        il.SizeOf(typeof(double));
        il.SetArray();

        il.Copy();
        il.LoadInt(5);
        il.SizeOf(typeof(decimal));
        il.SetArray();

        il.Copy();
        il.LoadInt(6);
        il.SizeOf(typeof(string));
        il.SetArray();

        il.Copy();
        il.LoadInt(7);
        il.SizeOf(typeof(object));
        il.SetArray();

        il.SetLocal(l1);
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }

    // set Field
    public static MethodBuilder DefineMethod_SetField1(TypeBuilder typeBuilder)
    {
        FieldBuilder fb = typeBuilder.DefineField("field1", typeof(int), FieldAttributes.Public | FieldAttributes.Static);
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("SetField1", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
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
    public static MethodBuilder DefineMethod_Typeof1(TypeBuilder typeBuilder)
    {
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Typeof1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
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
    public static MethodBuilder DefineMethod_CopyAddrValueToAddr1(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("CopyAddrValueToAddr1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(EmitTest2) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));

        // l2 = arg1;
        il.LoadLocalAddr(l1.LocalIndex);
        il.LoadArgAddr(0);
        il.SizeOf(typeof(EmitTest2));
        il.Emit(OpCodes.Cpblk);

        // return l2; 
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }




    public static MethodBuilder DefineMethod_Test1(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test1", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadDouble(10.0);
        il.LoadDouble(3.0);
        il.MathDiv();
        il.Emit(OpCodes.Ckfinite);
        il.Box(typeof(double));
        il.Return();
        return methodBuilder;
    }


    public static MethodBuilder DefineMethod_Test2(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test2", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder lb1 = il.DeclareLocal(typeof(string));
        LocalBuilder lb2 = il.DeclareLocal(typeof(Exception));
        Label l1 = il.DefineLabel();

        try
        {
            // lb1 = EmitTest2.T2();
            il.Call(typeof(EmitTest2).GetMethod("T2"));
            il.SetLocal(lb1);
            il.GotoLeave(l1);
        }
        catch (Exception)
        {
            il.SetLocal(lb2);
            il.Emit(OpCodes.Rethrow);
        }

        //return lb1;
        il.MarkLabel(l1);
        il.LoadLocal(lb1);
        il.Return();
        return methodBuilder;
    }

    public static MethodBuilder DefineMethod_Test3(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test3", MethodAttributes.Public | MethodAttributes.Static, typeof(object), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        il.LoadDouble(10.0);
        il.LoadDouble(3.0);
        il.MathDiv();
        il.Emit(OpCodes.Ckfinite);
        il.Box(typeof(double));

        il.LoadDouble(10.0);
        il.Box(typeof(double));
        il.Pop();

        il.Return();
        return methodBuilder;
    }

    public static MethodBuilder DefineMethod_Test4(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test4", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(EmitTest2) });
        ILGenerator il = methodBuilder.GetILGenerator();

        LocalBuilder l1 = il.DeclareLocal(typeof(EmitTest2));

        // l2 = arg1;
        il.LoadLocalAddr(l1.LocalIndex);
        il.LoadArgAddr(0);
        il.Emit(OpCodes.Cpobj, typeof(int));

        // return l2; 
        il.LoadLocal(l1);
        il.Return();
        return methodBuilder;
    }



    public static int SF1(object o)
    {
        return sizeof(int);
    }

    public static string SF2(object o)
    {
        if (o is string s) return s;
        else return "";
    }

    public static Type SF2_1(object o)
    {
        Type type = o as Type;
        return type;
    }

    public static object SF3()
    {
        List<string> list1 = new List<string>() { "1", "2", "3", "4", "5" };
        IEnumerator<string> list2 = list1.GetEnumerator();

        string a = "9";
        while (list2.MoveNext())
        {
            a = list2.Current;
        }
        return a;
    }

    public static object SF4(string a)
    {
        try
        {
            return SF5();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static object SF5()
    {
        List<string> list1 = new List<string>() { "1", "2", "3", "4", "5" };

        string a = "9";
        foreach (string? item in list1)
        {
            a = item;
        }
        return a;
    }
}

public class EmitTest1
{
    public virtual string T1(string s) => s + "###";
}

public class EmitTest2 : EmitTest1
{
    public int MyProperty { get; set; }
    public override string T1(string s) => s + "%%%";

    public static string T2() => throw new Exception("CCCCCCCs");
}
