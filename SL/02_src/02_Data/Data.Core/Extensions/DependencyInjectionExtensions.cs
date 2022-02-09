//using Castle.DynamicProxy;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lh.Data.Core.Extensions
//{
//    public static class DependencyInjectionExtensions
//    {
//        public static IServiceCollection AddService<TService, TImplementation>(this IServiceCollection services,
//            ServiceLifetime lifetime, params Type[] interceptorTypes)
//        {
//            return services.AddService(typeof(TService), typeof(TImplementation), lifetime, interceptorTypes);
//        }
//        public static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implType,
//            ServiceLifetime lifetime, params Type[] interceptorTypes)
//        {
//            services.Add(new ServiceDescriptor(implType, implType, lifetime));
//            Func<IServiceProvider, object> factory = (provider) =>
//            {
//                var target = provider.GetService(implType);
//                List<IInterceptor> interceptors = interceptorTypes.ToList().ConvertAll<IInterceptor>(interceptorType =>
//                {
//                    return provider.GetService(interceptorType) as IInterceptor;
//                });
//                var proxy = DynamicProxyExtensions.Generator.CreateInterfaceProxyWithTarget(serviceType, target, interceptors.ToArray());
//                return proxy;
//            };
//            var serviceDescriptor = new ServiceDescriptor(serviceType, factory, lifetime);
//            services.Add(serviceDescriptor);
//            return services;
//        }
//    }
//}