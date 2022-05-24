# 依赖注入（抽象）

- [x] key服务注册

  ~~~text
  // 瞬时
  <IServiceCollection>.AddTransient(serviceType,implementationType,key);
  ...
  // 范围
  <IServiceCollection>.AddScoped(serviceType,implementationType,key);
  ...
  // 单例
  <IServiceCollection>.AddSingleton(serviceType,implementationType,key);
  ...
  ~~~

- [x] key服务获取

  ~~~text
  IServiceProvider.GetService<TService>(key);
  IServiceProvider.GetRequiredService<TService>(key);
  IServiceProvider.GetServices<TService>(key);
  ~~~

- [x] 接口形式的服务生命周期

  ~~~text
  // 瞬时
  ILifetimeTransient
  // 范围
  ILifetimeScoped
  // 单例
  ILifetimeSingleton

  三个接口都继承自 IServiceLifetime

  可使用 ServiceLifetimeExtension.GetLifetime(Type); 获取到 ServiceLifetime 类型数据
  ~~~

- [x] 服务类型面具

  ~~~text
  // 创建类型面具
  Type.WearMask(string);
  ServiceTypeMaskExtensions.WearMask(Type,string);

  // 创建微软key的面具
  Type.WearMicrosoftMask();
  ServiceTypeMaskExtensions.WearMicrosoftMask(Type);
  ~~~

## 更新日志

### 1.0.0

~~~text
发布第一个正式版
~~~

### 1.0.1-beta1

~~~text
1.解决 Easy.Extensions.DependencyInjection 依赖问题
~~~

### 1.0.1

~~~text
1.ServiceTypeProxy 类修改成 ServiceTypeMask ,服务类型面具的意思
2.ServiceTypeProxyExtensions 类修改成 ServiceTypeMaskExtensions
3.增加服务类型面具功能
~~~
