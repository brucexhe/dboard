using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Dboard
{
    public static class AutowiredExtension
    {

        public static IServiceCollection AddAutowired(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();


            var fields = types
             .SelectMany(t => t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
             .Where(f => f.GetCustomAttribute<AutowiredAttribute>() != null)
             .ToList();

            foreach (var field in fields)
            {
                // 检查是否存在对应的服务
                var serviceType = field.FieldType;
                var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == serviceType);
                if (serviceDescriptor != null)
                {
                    var serviceProvider = services.BuildServiceProvider();
                    // 创建字段所在类型的实例
                    var declaringType = field.DeclaringType;
                    var instance = ActivatorUtilities.CreateInstance(serviceProvider, declaringType);
                    // 由于是Transient生命周期，每次注入都需要新实例
                    field.SetValue(instance, serviceProvider.GetRequiredService(serviceType));
                }
            }
            return services;
        }
    }
}
