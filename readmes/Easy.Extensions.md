# 系统类型拓展

- [x] String

  ~~~text
  判断字符串是否为空 <string>.IsNullOrEmpty();
  ~~~

- [x] Type

  ~~~text
  判断类型的接口定义是否包含执行类型  <type>.IsInterfaceDefinitionInclude(type);

  判断类型是否继承自指定类型  <type>.IsInheritFrom(type);

  判断是否是开放泛型  <type>.IsOpenGeneric();

  获取类型定义 <type>.GetTypeDefinition();

  获取类型的名称 <type>.Name();

  获取类型的完全限定名 <type>.FullName();
  ~~~

- [x] IEnumerable<T>

  ~~~text
  判读集合是否为空 <IEnumerable<T>>.IsNullOrEmpty();
  ~~~

- [x] ICustomAttributeProvider

  ~~~text
  获取指定类型特性  <ICustomAttributeProvider>.GetAttribute<attributeType>(bool);

  获取指定类型特性集合  <ICustomAttributeProvider>.GetAttributes<attributeType>(bool);

  检查指定类型特性是否存在,如果存在则获取 <ICustomAttributeProvider>.IsExistAttribute<attributeType>(bool,out attributeType);

  设置属性值  <PropertyInfo>.SetPropertyValue(object,object);
  ~~~

## 更新日志

### 1.0.0

~~~text
发布第一个正式版
~~~

### 1.0.0-beta1

~~~text
1.解决 Easy.Extensions.DependencyInjection 依赖问题
~~~
