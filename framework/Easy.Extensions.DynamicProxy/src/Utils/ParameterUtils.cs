using System;
using System.Globalization;
using System.Reflection;

namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 参数实用程序
/// </summary>
internal static class ParameterUtils
{
    public static void DefineParameters(this MethodBuilder methodBuilder,MethodInfo refMethod)
    {
        ParameterInfo[] parameters = refMethod.GetParameters();
        // 如果有参数设置参数
        if (!parameters.IsNullOrEmpty())
        {
            // 参数偏移量
            const int paramOffset = 1;
            foreach (ParameterInfo parameter in parameters)
            {
                ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(parameter.Position + paramOffset, parameter.Attributes, parameter.Name);
                // 设置默认值
                if (parameter.HasDefaultValue) parameterBuilder.SetParameterConstant(parameter);
                // 设置参数特性
                foreach (CustomAttributeData attributeData in parameter.CustomAttributes) parameterBuilder.SetCustomAttribute(CustomAttributeUtils.GetCustomAttributeBuilder(attributeData));
            }
        }

        // 设置返回值
        ParameterInfo returnParam = refMethod.ReturnParameter;
        ParameterBuilder returnParameterBuilder = methodBuilder.DefineParameter(0, returnParam.Attributes, returnParam.Name);
        // 设置参数特性
        foreach (CustomAttributeData attributeData in returnParam.CustomAttributes) returnParameterBuilder.SetCustomAttribute(CustomAttributeUtils.GetCustomAttributeBuilder(attributeData));
    }

    /// <summary>
    /// 设置参数常量(该方法后期需要实验)
    /// </summary>
    /// <param name="parameterBuilder">方法构建器</param>
    /// <param name="refParameter">参考参数</param>
    private static void SetParameterConstant(this ParameterBuilder parameterBuilder,ParameterInfo refParameter)
    {
        object? defaultValue;
        try
        {
            defaultValue = refParameter.DefaultValue;
        }
        catch (FormatException) when (refParameter.ParameterType == typeof(DateTime))
        {
            defaultValue = null;
        }
        catch (FormatException) when (refParameter.ParameterType.IsEnum)
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
            Type paramType = refParameter.ParameterType;
            Type nonNullParamType = paramType;
            bool isNullableType = paramType.IsNullableType();


            if (defaultValue is null)
            {
                if (isNullableType || refParameter.ParameterType.IsValueType) return;
            }
            else if (isNullableType)
            {
                nonNullParamType = paramType.GetNonNullableType();
                if (nonNullParamType.IsEnum || nonNullParamType.IsInstanceOfType(defaultValue)) return;
            }

            try
            {
                object? coercedDefault = Convert.ChangeType(defaultValue, nonNullParamType, CultureInfo.InvariantCulture);
                parameterBuilder.SetConstant(coercedDefault);
            }
            catch
            {

            }
            
        }
    }
}
