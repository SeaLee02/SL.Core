using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SL.Auth.Abstractions;
using SL.Auth.Abstractions.Options;
using SL.Auth.Core;
using SL.Data.Abstractions;
using SL.Utils.Const;
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
        /// 添加Mkh身份认证核心功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">配置属性</param>
        /// <returns></returns>
        public static SLAuthBuilder AddMkhAuth(this IServiceCollection services, IConfiguration configuration)
        {

            //添加身份认证配置项
            services.Configure<AuthOptions>(configuration.GetSection("SL:Auth"));



            //添加模块权限解析器
            services.AddSingleton<IPermissionResolver, PermissionResolver>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConst.TOKEN_POLICY, policy => policy.Requirements.Add(new PermissionRequirement()));
            });

            //自定义权限验证处理器
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();


            var builder = new SLAuthBuilder(services, configuration);

            return builder;
        }
    }
}
