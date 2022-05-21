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

    public static string StringAdd1(string P_0, string P_1)
    {
        return P_0 + P_1;
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

        DefineMethod_StringAdd1(typeBuilder);

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
        il.SetArrayValue(typeof(int));

        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadLocal(i2);
        il.SetArrayValue(typeof(int));

        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadLocal(i3);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(true);
        il.SetArrayValue(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathAdd(isUnsigned: true);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathSub(true);
        il.SetArrayValue(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathSub(isUnsigned: true);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));
        // 检查溢出添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathMul(true);
        il.SetArrayValue(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(2);
        il.LoadArg();
        il.LoadArg(1);
        il.MathMul(isUnsigned: true);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathDiv(isUnsigned: true);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));
        // 检查溢出无符号添加
        il.LoadLocal(arr);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.MathRem(isUnsigned: true);
        il.SetArrayValue(typeof(int));

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
        il.SetArrayValue(typeof(int));

        // 无符号右移位
        il.LoadLocal(l1);
        il.LoadInt(1);
        il.LoadArg();
        il.LoadArg(1);
        il.BitwiseShiftRight(true);
        il.SetArrayValue(typeof(int));

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
}
