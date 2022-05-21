﻿namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

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

        DefineMethod_Add1(typeBuilder);
        DefineMethod_Add2(typeBuilder);
        DefineMethod_StringAdd1(typeBuilder);

        return typeBuilder.CreateType();
    }

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
