﻿using System.Reflection;
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
        Type dynamicType = DynamicCreateType.CreateMyDynamicType();

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


}