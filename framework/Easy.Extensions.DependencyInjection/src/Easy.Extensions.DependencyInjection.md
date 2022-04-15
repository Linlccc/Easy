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

  