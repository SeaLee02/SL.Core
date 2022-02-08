namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SL.Utils.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 从服务集合中获取服务实例，需确保服务一定存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceCollection services)
        {
            return (T)services.LastOrDefault(m => m.ServiceType == typeof(T))!.ImplementationInstance;
        }


        /// <summary>
        /// 扫描并注入所有使用特性注入的服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesFromAttribute(this IServiceCollection services)
        {
            var assemblies = new AssemblyHelper().Load();
            foreach (var assembly in assemblies)
            {
                try
                {
                    services.AddServicesFromAssembly(assembly);
                }
                catch
                {
                    //此处防止第三方库抛出一场导致系统无法启动，所以需要捕获异常来处理一下
                }
            }
            return services;
        }

        /// <summary>
        /// 从指定程序集中注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {

            }
            return services;
        }
    }
}
