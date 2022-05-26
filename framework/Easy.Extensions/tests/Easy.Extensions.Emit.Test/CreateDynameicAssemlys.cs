﻿namespace Easy.Extensions.Emit.Test
{

    /// <summary>
    /// 创建动态程序集
    /// </summary>
    public class CreateDynameicAssemlys
    {
        #region 动态程序集基础
        /// <summary>
        /// 动态程序集名称
        /// </summary>
        private static readonly string _assemblyName = nameof(CreateDynameicAssemlys);
        /// <summary>
        /// 动态程序集构建器
        /// </summary>
        private static readonly AssemblyBuilder _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_assemblyName), AssemblyBuilderAccess.RunAndSave);
        /// <summary>
        /// 模块构建器
        /// </summary>
        private static readonly ModuleBuilder _moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName, $"{_assemblyName}.dll");
        /// <summary>
        /// 创建动态程序集
        /// </summary>
        private void CreateCynameicAssemlys() => _assemblyBuilder.Save($"{_assemblyName}.dll");
        #endregion

        /// <summary>
        /// 创建程序集
        /// </summary>
        [Fact]
        public void CreateAssemlys()
        {
            _moduleBuilder.DefineType_HelloWorld();
            _moduleBuilder.DefineType_EmitOpCodesVerify();
            CreateCynameicAssemlys();
        }

        /// <summary>
        /// 创建类型 HelloWorld
        /// </summary>
        [Fact]
        public void Create_HelloWorld()
        {
            Type type = _moduleBuilder.DefineType_HelloWorld();

            // 创建实例
            object instance = Activator.CreateInstance(type, 100, "abc");

            // 验证
            Assert.Equal(100, type.GetProperty("Number").GetValue(instance));
            Assert.Equal("abc", type.GetField("m_number2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance));
            Assert.Equal(20 * 100, type.InvokeMember("Mul", BindingFlags.InvokeMethod, null, instance, new object[] { 20 }));
        }

        /// <summary>
        /// 创建类型 EmitOpCodesVerify
        /// </summary>
        [Fact]
        public void Create_EmitOpCodesVerify()
        {
            Type type = _moduleBuilder.DefineType_EmitOpCodesVerify();

            // ** 数学
            // +
            int[] add1 = (int[])type.InvokeMember("Add1", BindingFlags.InvokeMethod, null, null, new object[] { 10, 2 });
            int[] add2 = (int[])type.InvokeMember("Add2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(12, add1[0]);
            Assert.Equal(125, add2[0]);
            // -
            int[] sub1 = (int[])type.InvokeMember("Sub1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(121, sub1[0]);
            // *
            int[] mul1 = (int[])type.InvokeMember("Mul1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(246, mul1[0]);
            // /
            int[] div1 = (int[])type.InvokeMember("Div1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(61, div1[0]);
            // %
            int[] rem1 = (int[])type.InvokeMember("Rem1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(1, rem1[0]);
            // -arg
            int neg1 = (int)type.InvokeMember("Neg1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(-123, neg1);

            // ** 按位计算
            // &
            int and1 = (int)type.InvokeMember("And1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 & 2, and1);
            // |
            int or1 = (int)type.InvokeMember("Or1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 | 2, or1);
            // ^
            int xor1 = (int)type.InvokeMember("Xor1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 ^ 2, xor1);
            // ~arg
            int not1 = (int)type.InvokeMember("Not1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(~123, not1);
            // <<
            int shiftLeft1 = (int)type.InvokeMember("ShiftLeft1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 << 2, shiftLeft1);
            // >>
            int[] shiftRight1 = (int[])type.InvokeMember("ShiftRight1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 >> 2, shiftRight1[0]);

            // ** 比较
            // ==
            int equal1 = (int)type.InvokeMember("Equal1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
            Assert.Equal(123 == 123 ? 1 : 0, equal1);
            bool equal2 = (bool)type.InvokeMember("Equal2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
            Assert.Equal(123 == 123, equal2);
            // >
            int greater1 = (int)type.InvokeMember("Greater1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
            Assert.Equal(123 > 123 ? 1 : 0, greater1);
            // >
            int less1 = (int)type.InvokeMember("Less1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 123 });
            Assert.Equal(123 < 123 ? 1 : 0, less1);

            // ** 特殊
            // 调用和定义包含有可变参数的方法
            object[] arglist_Invoke1 = (object[])type.InvokeMember("Arglist_Invoke1", BindingFlags.InvokeMethod, null, null, new object[] { 2, 3.1F, "5", typeof(int) });
            Assert.Equal(new object[] { 2, 3.1F, "5", typeof(int) }, arglist_Invoke1);

            // ** 类型转换
            // (object)int
            object box1 = (int)type.InvokeMember("Box1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(123, box1);
            // (int)obj
            int unBox1 = (int)type.InvokeMember("UnBox1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(123, unBox1);
            // (int)obj
            int unBox2 = (int)type.InvokeMember("UnBox2", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(123, unBox2);
            // float to int
            int convert1 = (int)type.InvokeMember("ConvertInteger1", BindingFlags.InvokeMethod, null, null, new object[] { 123.999f });
            Assert.Equal((int)123.999, convert1);
            // as
            object as1 = type.InvokeMember("AS1", BindingFlags.InvokeMethod, null, null, new object[] { typeof(int) });
            Assert.Equal(typeof(int), as1);


            // ** 调用方法
            // 普通指令调用虚方法
            string callVirtual1 = (string)type.InvokeMember("CallVirtual1", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
            Assert.Equal("a" + "###", callVirtual1);
            // 普通指令调用虚方法
            string callVirtual2 = (string)type.InvokeMember("CallVirtual2", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
            Assert.Equal("a" + "###", callVirtual2);
            // 使用调用虚方法的指令调用虚方法
            string callVirtual3 = (string)type.InvokeMember("CallVirtual3", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
            Assert.Equal("a" + "%%%", callVirtual3);
            string callVirtual4 = (string)type.InvokeMember("CallVirtual4", BindingFlags.InvokeMethod, null, null, new object[] { "a" });
            Assert.Equal("a" + "%%%", callVirtual3);


            // 获取数组元素
            object array1 = type.InvokeMember("Array1", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(8, array1);
            // 获取数组元素
            object array2 = type.InvokeMember("Array2", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal("8", array2);
            // 获取数组元素的地址的值
            object array3 = type.InvokeMember("Array3", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal("8", array3);
            // 获取数组长度
            object array4 = type.InvokeMember("Array4", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(99, array4);



            // ** 地址
            // SetValueToAddr
            object setValueToAddr1 = type.InvokeMember("SetValueToAddr1", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(10, setValueToAddr1);
            object setValueToAddr2 = type.InvokeMember("SetValueToAddr2", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(typeof(object), setValueToAddr2.GetType());


            // sizeof
            object sizeof1 = type.InvokeMember("SizeOf1", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(new int[] { 1, 2, 4, 8, 8, 16, 4, 4 }, sizeof1);


            // set field
            object field1 = type.InvokeMember("SetField1", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(3, field1);


            // typeof
            object typeof1 = type.InvokeMember("Typeof1", BindingFlags.InvokeMethod, null, null, new object[] { });
            Assert.Equal(typeof(string), typeof1);






            object test1 = type.InvokeMember("Test1", BindingFlags.InvokeMethod, null, null, new object[] { });
            //object test2 = type.InvokeMember("Test2", BindingFlags.InvokeMethod, null, null, new object[] { });
            object test3 = type.InvokeMember("Test3", BindingFlags.InvokeMethod, null, null, new object[] { });
        }
    }
}
