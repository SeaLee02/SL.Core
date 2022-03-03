using MediatR;
using SL.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensionsLocal
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            var assemblies = new AssemblyHelper().Load(m => m.Name.EndsWith(".Core"));
            services.AddMediatR(assemblies.ToArray());
            return services;
        }
    }
}
