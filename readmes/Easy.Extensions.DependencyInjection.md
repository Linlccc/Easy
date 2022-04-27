### 依赖注入

- [x] 自动注册

  ~~~
  1.普通类型注册,在类型上使用 [RegisterAttribute] 特性
  2.工厂注册,类型实现 IRegisterFactory<TService,TServiceLifetime> 接口
  3.<IServiceCollection>.AutoRegister([assemblies]);
  
  注：
  	assemblies 是添加 [RegisterAttribute] 特性或者实现 IRegisterFactory 接口的程序集集合
  ~~~

- [x] 自动注入

  ~~~
  1.属性自动注入
  2.字段自动注入
  
  在需要自动注入的属性或字段上使用 [InjectAttribute] 特性即可
  ~~~

- [x] 获取Easy服务提供商

  ~~~
  <IServiceCollection>.BuildEasyServiceProvider();
  ~~~

- [x] 替换默认服务提供商

  ~~~
  <IHostBuilder>.UseServiceProviderFactory(new EasyServiceProviderFactory());
  ~~~