using System.Reflection;
using System.Reflection.Emit;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Test;

public class DynamicCreateTypeTest
{
    /// <summary>
    /// 创建动态类型测试
    /// </summary>
    [Fact]
    public void CreateDynamicTypeTest()
    {
        Type dynamicType = DynamicCreateType.CreateMyDynamicType1();

        // 获取构造函数
        ConstructorInfo[] constructorInfos = dynamicType.GetConstructors();
        // 获取属性
        PropertyInfo propertyInfo = dynamicType.GetProperty("Number")!;
        // 获取方法
        MethodInfo methodInfo = dynamicType.GetMethod("MyMethod")!;

        Assert.Equal(4, constructorInfos.Length);
        Assert.NotNull(propertyInfo);
        Assert.NotNull(methodInfo);

        // 使用默认构造函数创建 MyDynamicType 的实例
        object o1 = Activator.CreateInstance(dynamicType)!;
        // 测试属性
        Assert.Equal(42, propertyInfo.GetValue(o1));
        propertyInfo.SetValue(o1, 99);
        Assert.Equal(99, propertyInfo.GetValue(o1));
        // 测试方法
        Assert.Equal(99 * 100, methodInfo.Invoke(o1, new object[] { 100 }));


        // 使用一个参数的构造函数创建实例
        object o2 = Activator.CreateInstance(dynamicType, new object[] { 100 })!;
        // 测试属性
        Assert.Equal(100, propertyInfo.GetValue(o2));

        // 使用一个参数的构造函数创建实例
        object o3 = Activator.CreateInstance(dynamicType, new object[] { 101,"123" })!;
        // 测试属性
        Assert.Equal(101, propertyInfo.GetValue(o3));

        // 使用一个参数的构造函数创建实例
        object o4 = Activator.CreateInstance(dynamicType, new object[] { "122133", 102 })!;
        // 测试属性
        Assert.Equal(102, propertyInfo.GetValue(o4));
    }

    /// <summary>
    /// 本地参数参数
    /// </summary>
    [Fact]
    public void LocalVarTest()
    {
        Type dynamicType = DynamicCreateType.CreateMyDynamicType2();

        // 获取方法
        MethodInfo methodInfo1 = dynamicType.GetMethod("TestLoaclVar1")!;
        MethodInfo methodInfo2 = dynamicType.GetMethod("TestLoaclVar2")!;
        MethodInfo methodInfo3 = dynamicType.GetMethod("TestLoaclVar3")!;
        MethodInfo methodInfo4 = dynamicType.GetMethod("TestLoaclVar4")!;
        MethodInfo methodInfo5 = dynamicType.GetMethod("TestLoaclVar5")!;
        MethodInfo methodInfo6 = dynamicType.GetMethod("TestLoaclVar6")!;
        MethodInfo methodInfo7 = dynamicType.GetMethod("TestLoaclVar7")!;
        MethodInfo methodInfo8 = dynamicType.GetMethod("TestLoaclVar8")!;
        MethodInfo methodInfo9 = dynamicType.GetMethod("TestLoaclVar9")!;
        MethodInfo methodInfo10 = dynamicType.GetMethod("TestLoaclVar10")!;
        MethodInfo methodInfo11 = dynamicType.GetMethod("TestLoaclVar11")!;
        MethodInfo methodInfo12 = dynamicType.GetMethod("TestLoaclVar12")!;
        MethodInfo methodInfo13 = dynamicType.GetMethod("TestLoaclVar13")!;
        MethodInfo methodInfo14 = dynamicType.GetMethod("TestLoaclVar14")!;
        MethodInfo methodInfo15 = dynamicType.GetMethod("TestLoaclVar15")!;
        Assert.NotNull(methodInfo1);
        Assert.NotNull(methodInfo2);

        // 使用默认构造函数创建 MyDynamicType 的实例
        object o1 = Activator.CreateInstance(dynamicType)!;

        object v1 = methodInfo1.Invoke(o1, new object[] { 13, 22 })!;
        object v2 = methodInfo2.Invoke(o1, new object[] { 43, 22 })!;
        object v3 = methodInfo3.Invoke(o1, new object[] { 43, 22 })!;
        object v4 = methodInfo4.Invoke(o1, new object[] { 43, 22 })!;
        object v5 = methodInfo5.Invoke(o1, new object[] { 43, 22 })!;
        object v6 = methodInfo6.Invoke(o1, new object[] { 43, 22 })!;
        object v7 = methodInfo7.Invoke(o1, new object[] { 43, 22 })!;
        object v8 = methodInfo8.Invoke(o1, new object[] { 43, 22 })!;
        object v9 = methodInfo9.Invoke(o1, new object[] { 43, 22 })!;
        object v10 = methodInfo10.Invoke(o1, new object[] { 43, 22 })!;
        object v11 = methodInfo11.Invoke(o1, new object[] { typeof(int) })!;
        object v12 = methodInfo12?.Invoke(o1, new object[] { new IntPtr(5),new IntPtr(11) })!;
        object v13 = methodInfo13?.Invoke(o1, new object[] { new IntPtr(5),new IntPtr(11) })!;
        object v14 = methodInfo14?.Invoke(o1, new object[] { 5,"123" })!;
        object v15 = methodInfo15?.Invoke(o1, new object[] { (float)14,(float)9 })!;

        
        //Assert.Equal(13, v1.Length);
        //Assert.Equal(22, v1[0]);
    }


    /// <summary>
    /// 零时测试
    /// </summary>
    [Fact]
    public void LTest1()
    {
        ModuleBuilder moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("test"), AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule("test");
        TypeBuilder typeBuilder = moduleBuilder.DefineType("MyDynamicType", TypeAttributes.Public);
        
        MethodBuilder methodBuilder1 = typeBuilder.DefineMethod("Test1", MethodAttributes.Public);
        ILGenerator mbIl = methodBuilder1.GetILGenerator();

        mbIl.LoadInt(13);
        mbIl.LoadInt64(13);

        ILGenerator mbI2 = null!;
        Assert.Throws<ArgumentNullException>(() => mbI2.LoadInt64(12));
        Assert.Throws<ArgumentNullException>(() => mbI2.LoadInt(12));
        
    }

    [Fact]
    public void LTest2()
    {
        Type myType = DynamicCreateType.CreateMyDynamicType3();

        int theValue = 1;

        Object myInstance = Activator.CreateInstance(myType, new object[0]);

        var v1 = myType.InvokeMember("SwitchMe",BindingFlags.InvokeMethod, null,myInstance,new object[] { theValue });
    }    


}
