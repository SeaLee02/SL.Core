using SL.Module.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lh.Mkh.Admin.Core.Infrastructure
{
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(ModuleConfigureContext context)
        {
            var services = context.Services;

            //注入自定义接口和实现


        }
    }
}
