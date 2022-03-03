using CSRedis;
using Microsoft.Extensions.Configuration;
using SL.Utils.Extensions;
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
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection UseRedis(this IServiceCollection services, IConfiguration cfg)
        {
            if (cfg["Redis:IsEnable"].ToBool())
            {   
                //注册成全局单例,使用请解析
                var rds = new CSRedisClient(cfg["RedisStr"]);
                services.AddSingleton(rds);
            }

            return services;
        }
    }
}
