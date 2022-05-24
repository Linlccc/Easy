# 依赖注入

- [x] 自动注册

  ~~~text
  1.普通类型注册,在类型上使用 [RegisterAttribute] 特性
  2.工厂注册,类型实现 IRegisterFactory<TService,TServiceLifetime> 接口
  3.<IServiceCollection>.AutoRegister([assemblies]);

  注：
   assemblies 是添加 [RegisterAttribute] 特性或者实现 IRegisterFactory 接口的程序集集合
  ~~~

- [x] 自动注入

  ~~~text
  1.属性自动注入
  2.字段自动注入

  在需要自动注入的属性或字段上使用 [InjectAttribute] 特性即可
  ~~~

- [x] 获取Easy服务提供商

  ~~~text
  <IServiceCollection>.BuildEasyServiceProvider();
  ~~~

- [x] 替换默认服务提供商

  ~~~text
  <IHostBuilder>.UseServiceProviderFactory(new EasyServiceProviderFactory([EasyServiceProviderOptions]));
  ~~~

## 更新日志

### 1.0.0

~~~text
发布第一个正式版
~~~

### 1.0.1-beta1

~~~text
1.为服务提供商工厂添加无参构造函数(默认从当前程序域中所有程序集扫描要注册的服务)
~~~

### 1.0.1-beta2

~~~text
1.解决当前项目依赖问题
~~~
