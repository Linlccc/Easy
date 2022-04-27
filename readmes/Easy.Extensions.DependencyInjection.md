### 依赖注入

- [x] 自动注册

  ~~~
  <IServiceCollection>.AutoRegister([assemblies]);
  ~~~

- [x] 获取Easy 服务提供商

  ~~~
  <IServiceCollection>.BuildEasyServiceProvider();
  ~~~

- [x] 替换服务提供商

  ~~~
  <IHostBuilder>.UseServiceProviderFactory(new EasyServiceProviderFactory());
  ~~~





  - [x] 自动注册

  ~~~
  在要注册的类上使用 [RegisterAttribute] 特性
  ~~~

- [x] 工厂注册

  ~~~
  要以工厂注册的类实现 IRegisterFactory<TService,TServiceLifetime> 接口
  ~~~

- [x] Key服务注册

  ~~~
  <IServiceCollection>.AddTransient(serviceType,implementationType,key);
  ~~~

- [x] Key服务获取对象

  ~~~
  IServiceProvider.GetService<TService>(key);
  ~~~

- [x] 属性注入

  ~~~
  在属性上使用 [InjectAttribute] 特性，获取对象将自动注入
  ~~~

- [x] 字段注入

  ~~~
  在字段上上使用 [InjectAttribute] 特性，获取对象将自动注入
  ~~~

  