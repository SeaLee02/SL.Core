using SL.Cache.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Redis缓存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCacheHandler, MemoryCacheHandler>();

            return services;
        }
    }
}
