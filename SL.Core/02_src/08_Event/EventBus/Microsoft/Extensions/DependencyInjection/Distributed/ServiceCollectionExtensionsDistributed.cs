using Microsoft.Extensions.Configuration;
using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensionsDistributed
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            bool isEnable = configuration["RabbitMQ:IsEnable"].ToBool();
            if (isEnable)
            {
                services.AddCap(options =>
                {
                    options.UseSqlServer("");//数据库链接
                    options.UseRabbitMQ(options =>
                    {
                        configuration.GetSection("RabbitMQ").Bind(options);
                    });
                    options.UseDashboard();
                });
            }
            return services;
        }
    }
}
