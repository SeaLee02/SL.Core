using Microsoft.Extensions.DependencyInjection;
using SL.Auth.Abstractions;
using SL.Mkh.Admin.Core.Infrastructure.Defaults;
using SL.Module.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure
{
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(ModuleConfigureContext context)
        {
            var services = context.Services;
            //注入自定义接口和实现      
            services.AddScoped<IPermissionValidateHandler, DefaultPermissionValidateHandler>();
            services.AddScoped<IUserPermissionResolver, DefaultUserPermissionResolver>();

        }
    }
}
