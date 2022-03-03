using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SL.Module.Abstractions;
using SL.Utils.Json.Converters;
using SL.Module.Web;
using Microsoft.Extensions.Primitives;
using SL.Utils.Helpers;

namespace SL.Host.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加MVC功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <param name="hostOptions"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvc(this IServiceCollection services, IModuleCollection modules, Options.HostOptions hostOptions, IHostEnvironment env)
        {
            services.AddMvc(c =>
            {
                //if (hostOptions!.Swagger || env.IsDevelopment())
                //{
                //    //API分组约定
                //    c.Conventions.Add(new ApiExplorerGroupConvention());
                //}
            })
                .AddJsonOptions(options =>
                {
                    //不区分大小写的反序列化
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    //属性名称使用 camel 大小写
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    //最大限度减少字符转义
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    //添加日期转换器
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                })
                //添加模块
                .AddModules(modules);

            return services;
        }

        /// <summary>
        /// 添加CORS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hostOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, Options.HostOptions hostOptions)
        {
            services.AddCors(options =>
            {
                /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                        所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                        第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                        Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
                var preflightMaxAge = hostOptions.PreflightMaxAge > 0 ? new TimeSpan(0, 0, hostOptions.PreflightMaxAge) : new TimeSpan(0, 30, 0);

                options.AddPolicy("Default",
                    builder => builder.SetIsOriginAllowed(_ => true)
                        .SetPreflightMaxAge(preflightMaxAge)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
            });

            return services;
        }


        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration cfg)
        {
            //内存
            var builder = services.AddCache();
            //Redis缓存
            services.UseRedis(cfg);
            return services;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection AddEvent(this IServiceCollection services, IConfiguration cfg)
        {
            //中介者模式
            var builder = services.AddMediatRService();
            services.AddRabbitMQ(cfg);
            return services;
        }

        /// <summary>
        /// 监听配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void CheckChangeToken(this IServiceCollection services, IConfiguration configuration)
        {
            ChangeToken.OnChange(() => configuration.GetReloadToken(), () =>
            {
                ConsoleHelper.WriteColorLine("配置发生变化", ConsoleColor.Blue);
            });
        }
    }
}
