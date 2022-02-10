﻿using SL.Module.Abstractions;
using SL.Module.Abstractions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Module.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块核心功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IModuleCollection AddModulesCore(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            var moduleOptionsList = configuration.Get<List<ModuleOptions>>("SL:Modules");

            var modules = new ModuleCollection(environment);
            modules.Load(moduleOptionsList);

            services.AddSingleton<IModuleCollection>(modules);

            return modules;
        }

        /// <summary>
        /// 添加模块相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddModuleServices(this IServiceCollection services, IModuleCollection modules, IHostEnvironment environment, IConfiguration configuration)
        {
            foreach (var module in modules)
            {
                if (module == null)
                    continue;                      

                //加载模块初始化器
                if (module.ServicesConfigurator != null)
                {
                    var context = new ModuleConfigureContext
                    {
                        Modules = modules,
                        Services = services,
                        Environment = environment,
                        Configuration = configuration
                    };

                    module.ServicesConfigurator.Configure(context);
                }

                ////加载模块初始化器
                //module.ServicesConfigurator?.Configure(services, modules.HostEnvironment);

                services.AddApplicationServices(module);
            }

            return services;
        }

        /// <summary>
        /// 添加应用服务,注入服务模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="module"></param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ModuleDescriptor module)
        {
            var assembly = module.LayerAssemblies.Core;
            //按照约定，应用服务必须采用Service结尾
            var implementationTypes = assembly.GetTypes().Where(m => m.Name.EndsWith("Service") && !m.IsInterface).ToList();

            foreach (var implType in implementationTypes)
            {
                //按照约定，服务的第一个接口类型就是所需的应用服务接口
                var serviceType = implType.GetInterfaces()[0];
                services.AddScoped(implType);
                //services.Add(new ServiceDescriptor(serviceType, implType, ServiceLifetime.Scoped));
                module.ApplicationServices.Add(serviceType, implType);
            }
            return services;
        }
    }
}