﻿using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 代理类型生成器
/// </summary>
internal class ProxyTypeGeneratorUtils
{
    /// <summary>
    /// 代理类型程序集名称
    /// </summary>
    private const string ProxyAssemblyName = "Easy.DynamicProxy.Generator";
    /// <summary>
    /// 模块构建器
    /// </summary>
    private readonly ModuleBuilder _moduleBuilder;
    /// <summary>
    /// 代理类型地图
    /// </summary>
    private readonly Dictionary<string, Type> _proxyTypeMap;
    /// <summary>
    /// 代理类型名称提供程序
    /// </summary>
    private readonly ProxyTypeNameUtils _proxyTypeNameUtils;

    private readonly object _lock = new();


    public ProxyTypeGeneratorUtils()
    {
        // 创建模块构建器，因为在.net core和.net 5+ 中态程序集只能包含一个动态模块，所以直接创建模块,https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.emit.assemblybuilder?view=net-6.0#remarks
        _moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(ProxyAssemblyName), AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule(ProxyAssemblyName);
        _proxyTypeMap = new();
        _proxyTypeNameUtils = new();
    }

    internal Type CreateInterfaceProxyType(Type interfaceType, Type classType)
    {
        lock (_lock)
        {
            // 得到代理类型名称
            string proxyTypeName = _proxyTypeNameUtils.GetProxyTypeName(interfaceType, classType);
            if(_proxyTypeMap.TryGetValue(proxyTypeName,out Type? proxyType))return proxyType;

            // 获取代理类型描述器
            ProxyTypeDescriptor proxyTypeDescriptor = ProxyTypeDescriptorUtils.Describe(_moduleBuilder, proxyTypeName, interfaceType, typeof(object), new Type[] { interfaceType });
            // 定义构造函数
            ConstructorUtils.SetConstuctor(proxyTypeDescriptor, interfaceType);


            return proxyType!;
        }
    }


    internal Type CreateInterfaceProxyType1(Type interfaceType, Type classType)
    {
        string typeName = $"{interfaceType.FullName}Proxy";

        TypeBuilder typeBuild = _moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed, classType, new[] { interfaceType });

        DefineGenericParameter(typeBuild, interfaceType);

        // 字段
        FieldBuilder fieldBuilder1 = typeBuild.DefineField("_instance", typeof(IInterface), FieldAttributes.Private);
        FieldBuilder _target = typeBuild.DefineField("_target", interfaceType, FieldAttributes.Private);

        // 构造函数
        ConstructorBuilder constructorBuilder = typeBuild.DefineConstructor(MethodAttributes.Public, typeof(object).GetTypeInfo().DeclaredConstructors.Single().CallingConvention, new Type[] { typeof(IInterface), interfaceType });
        constructorBuilder.DefineParameter(1, ParameterAttributes.None, "_instance");
        constructorBuilder.DefineParameter(2, ParameterAttributes.None, "_target");

        ILGenerator iLGenerator = constructorBuilder.GetILGenerator();
        iLGenerator.Emit(OpCodes.Ldarg_0);
        iLGenerator.Emit(OpCodes.Call, typeof(object).GetTypeInfo().DeclaredConstructors.Single());

        iLGenerator.Emit(OpCodes.Ldarg_0);
        iLGenerator.Emit(OpCodes.Ldarg_1);
        iLGenerator.Emit(OpCodes.Stfld, fieldBuilder1);

        iLGenerator.Emit(OpCodes.Ldarg_0);
        iLGenerator.Emit(OpCodes.Ldarg_2);
        iLGenerator.Emit(OpCodes.Stfld, _target);

        iLGenerator.Emit(OpCodes.Ret);

        // 方法
        // 绑定属性的方法
        List<MethodInfo?> bindMethodOfProperty = interfaceType.GetProperties().Where(p => p.CanRead || p.CanWrite).Select(p => p.CanRead ? p.GetMethod : p.SetMethod).ToList();
        List<MethodInfo> methodInfos = interfaceType.GetTypeInfo().DeclaredMethods.Where(m => !bindMethodOfProperty.Contains(m)).ToList();
        foreach (MethodInfo methodInfo in methodInfos)
        {

        }



        return Type.GetType("")!;
    }

    private static MethodBuilder DefineMethod(MethodInfo methodInfo, MethodAttributes methodAttributes, Type classType, TypeBuilder typeBuild)
    {
        MethodBuilder methodBuilder = typeBuild.DefineMethod(methodInfo.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, methodInfo.CallingConvention, methodInfo.ReflectedType, methodInfo.GetParameters().Select(p => p.ParameterType).ToArray());

        // 继承目标方法的特性
        foreach (var item in methodInfo.CustomAttributes)
        {
            methodBuilder.SetCustomAttribute(DefineCustomAttribute(item));
        }

        DefineParameters(methodInfo, methodBuilder);

        //MethodInfo methodInfo1 = classType.

        return methodBuilder;
    }

    //private static MethodInfo GetMethodBySignature(Type type, MethodInfo method)
    //{
    //    var methods = type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


    //}



    private static void DefineParameters(MethodInfo methodInfo, MethodBuilder methodBuilder)
    {
        ParameterInfo[] parameterInfos = methodInfo.GetParameters();
        if (parameterInfos.Length > 0)
        {
            foreach (var item in parameterInfos)
            {
                ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(item.Position + 1, item.Attributes, item.Name);
                if ((item.Attributes & ParameterAttributes.HasDefault) != 0)
                {
                    try
                    {
                        object? defaultValue;
                        try
                        {
                            defaultValue = item.DefaultValue;
                        }
                        catch (FormatException) when (item.ParameterType == typeof(DateTime))
                        {
                            defaultValue = null;
                        }
                        catch (FormatException) when (item.ParameterType.IsEnum)
                        {
                            defaultValue = null;
                        }

                        if (defaultValue is Missing) return;

                        try
                        {
                            parameterBuilder.SetConstant(defaultValue);
                        }
                        catch (ArgumentException)
                        {
                            Type parameterType = item.ParameterType;
                            Type parameterNonNullType = parameterType;
                            if (defaultValue == null)
                            {
                                if (parameterType.GetTypeDefinition() == typeof(Nullable<>) || parameterType.IsValueType) return;
                            }
                            else if (parameterType.GetTypeDefinition() == typeof(Nullable<>))
                            {
                                parameterNonNullType = parameterType.GenericTypeArguments[0];
                                if (parameterNonNullType.IsEnum || parameterNonNullType.IsInstanceOfType(defaultValue)) return;
                            }
                            try
                            {
                                var coercedDefaultValue = Convert.ChangeType(defaultValue, parameterNonNullType, CultureInfo.InvariantCulture);
                                parameterBuilder.SetConstant(coercedDefaultValue);

                                return;
                            }
                            catch
                            {


                            }
                            throw;
                        }
                    }
                    catch
                    {
                    }
                }

                foreach (var ca in item.CustomAttributes)
                {
                    parameterBuilder.SetCustomAttribute(DefineCustomAttribute(ca));
                }
            }
        }

        ParameterInfo returnParameterInfo = methodInfo.ReturnParameter;
        var returnParameterBuilder = methodBuilder.DefineParameter(0, returnParameterInfo.Attributes, returnParameterInfo.Name);
        foreach (var attribute in returnParameterInfo.CustomAttributes)
        {
            returnParameterBuilder.SetCustomAttribute(DefineCustomAttribute(attribute));
        }
    }

    public static CustomAttributeBuilder DefineCustomAttribute(CustomAttributeData customAttributeData)
    {
        if (customAttributeData.NamedArguments != null)
        {
            var attributeTypeInfo = customAttributeData.AttributeType.GetTypeInfo();
            var constructor = customAttributeData.Constructor;
            //var constructorArgs = customAttributeData.ConstructorArguments.Select(c => c.Value).ToArray();
            var constructorArgs = customAttributeData.ConstructorArguments
                .Select(ReadAttributeValue)
                .ToArray();
            var namedProperties = customAttributeData.NamedArguments
                    .Where(n => !n.IsField)
                    .Select(n => attributeTypeInfo.GetProperty(n.MemberName))
                    .ToArray();
            var propertyValues = customAttributeData.NamedArguments
                     .Where(n => !n.IsField)
                     .Select(n => ReadAttributeValue(n.TypedValue))
                     .ToArray();
            var namedFields = customAttributeData.NamedArguments.Where(n => n.IsField)
                     .Select(n => attributeTypeInfo.GetField(n.MemberName))
                     .ToArray();
            var fieldValues = customAttributeData.NamedArguments.Where(n => n.IsField)
                     .Select(n => ReadAttributeValue(n.TypedValue))
                     .ToArray();
            return new CustomAttributeBuilder(customAttributeData.Constructor, constructorArgs
               , namedProperties!
               , propertyValues, namedFields!, fieldValues);
        }
        else
        {
            return new CustomAttributeBuilder(customAttributeData.Constructor, customAttributeData.ConstructorArguments.Select(c => c.Value).ToArray());
        }
    }

    private static object ReadAttributeValue(CustomAttributeTypedArgument argument)
    {
        var value = argument.Value;
        if (argument.ArgumentType.GetTypeInfo().IsArray == false)
        {
            return value!;
        }
        //special case for handling arrays in attributes
        //the actual type of "value" is ReadOnlyCollection<CustomAttributeTypedArgument>.
        var arguments = ((IEnumerable<CustomAttributeTypedArgument>)value!)
            .Select(m => m.Value)
            .ToArray();
        return arguments;
    }


    /// <summary>
    /// 定义泛型参数
    /// </summary>
    /// <param name="typeBuilder">类型构造器</param>
    /// <param name="targetType">目标类型</param>
    private static void DefineGenericParameter(TypeBuilder typeBuilder, Type targetType)
    {
        if (!targetType.IsGenericTypeDefinition) return;

        Type[] genericArgumentTypes = targetType.GetGenericArguments();
        GenericTypeParameterBuilder[] genericTypeParameterBuilders = typeBuilder.DefineGenericParameters(genericArgumentTypes.Select(gat => gat.Name).ToArray());
        // 配置泛型参数的约束信息
        foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in genericTypeParameterBuilders)
        {
            Type genericArgumentType = genericArgumentTypes[genericTypeParameterBuilder.GenericParameterPosition];
            genericTypeParameterBuilder.SetGenericParameterAttributes(genericArgumentType.GenericParameterAttributes);
            foreach (Type constraint in genericArgumentType.GetGenericParameterConstraints())
            {
                if (constraint.IsClass) genericTypeParameterBuilder.SetBaseTypeConstraint(constraint);
                if (constraint.IsInterface) genericTypeParameterBuilder.SetInterfaceConstraints(constraint);
            }
        }

    }
}

public interface IInterface
{
    void Invoke();
}

