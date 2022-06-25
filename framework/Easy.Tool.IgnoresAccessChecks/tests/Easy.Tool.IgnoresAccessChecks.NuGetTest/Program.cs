// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.ServiceLookup;

Console.WriteLine("使用DI的内部类型");
ServiceProvider service = new ServiceCollection().BuildServiceProvider();
ServiceProviderEngineScope root = service.Root;
Console.WriteLine($"这里直接在访问不可访问的 {nameof(ServiceProviderEngineScope)} 类型,是否时root：{root.IsRootScope}");
Console.WriteLine($"这里直接在访问不可访问的 {nameof(ServiceProviderEngineScope)} 类型,是否时root：{new ServiceProviderEngineScope(service, false).IsRootScope}");
Console.ReadLine();
