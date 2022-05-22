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
        // Arglist 个数不定，类型不定参数
        DefineMethod_Arglist1(typeBuilder);



        DefineMethod_Test1(typeBuilder);


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
    #endregion


    public static MethodBuilder DefineMethod_Test1(TypeBuilder typeBuilder)
    {
        // 测试方法

        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test1", MethodAttributes.Public | MethodAttributes.Static, typeof(string), Type.EmptyTypes);
        ILGenerator il = methodBuilder.GetILGenerator();

        // 局部变量
        LocalBuilder lb_result = il.DeclareLocal(typeof(IntPtr));//结果
        il.LoadLocalAddr(lb_result.LocalIndex);
        il.LoadInt(3221);
        il.Call(typeof(IntPtr).GetConstructor(new Type[] { typeof(int) }));

        il.LoadLocalAddr(lb_result.LocalIndex);
        il.Call(typeof(IntPtr).GetMethod("ToString", Type.EmptyTypes));
        il.Return();
        return methodBuilder;
    }
}
