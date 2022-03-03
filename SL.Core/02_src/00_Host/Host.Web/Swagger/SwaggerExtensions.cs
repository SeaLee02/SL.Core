using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SL.Host.Web.Swagger.Filter;
using SL.Module.Abstractions;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostOptions = SL.Host.Web.Options.HostOptions;

namespace SL.Host.Web.Swagger
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <param name="hostOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IModuleCollection modules, HostOptions hostOptions)
        {
            if (Check(modules, hostOptions))
            {
                services.AddSwaggerGen(c =>
                {
                    if (modules != null)
                    {
                        foreach (var module in modules)
                        {
                            c.SwaggerDoc(module.Code.ToLower(), new OpenApiInfo
                            {
                                Title = $"{module.Name}({module.Code})",
                                Version = module.Version,
                                Description = module.Description
                            });

                            //启用注解
                            c.EnableAnnotations();

                            //加载xml文档
                            var xmlPath = module.LayerAssemblies.Web.Location.Replace(".dll", ".xml", StringComparison.OrdinalIgnoreCase);
                            if (File.Exists(xmlPath))
                            {
                                c.IncludeXmlComments(xmlPath);
                            }
                        }
                    }

                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    };

                    //添加设置Token的按钮
                    c.AddSecurityDefinition("Bearer", securityScheme);

                    //添加Jwt验证设置,如果说无授权则不加锁
                    c.OperationFilter<AddAuthHeaderOperationFilter>();                   
                });
            }
            return services;
        }

        /// <summary>
        /// 启用Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="modules"></param>
        /// <param name="hostOptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IModuleCollection modules, HostOptions hostOptions)
        {
            //手动开启或者开发模式下才会启用swagger功能
            if (Check(modules, hostOptions))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    if (modules == null) return;

                    foreach (var module in modules)
                    {
                        var code = module.Code.ToLower();
                        var url = $"{code}/swagger.json";

                        c.SwaggerEndpoint(url, $"{module.Name}({module.Code})");
                        //启用过滤
                        c.EnableFilter();
                        //是否展开
                        c.DocExpansion(DocExpansion.None);
                        //model删除
                        c.DefaultModelsExpandDepth(-1);
                    }
                });
            }

            return app;
        }

        /// <summary>
        /// 检测是否开启Swagger
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="hostOptions"></param>
        /// <returns></returns>
        private static bool Check(IModuleCollection modules, HostOptions hostOptions)
        {
            //手动开启或者开发模式以及本地模式下才会启用swagger功能
            return hostOptions.Swagger || modules.HostEnvironment.IsDevelopment() ||
               modules.HostEnvironment.EnvironmentName.Equals("Local");
        }
    }
}
