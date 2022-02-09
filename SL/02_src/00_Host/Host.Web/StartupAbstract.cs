using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SL.Auth.Jwt;
using SL.Host.Web.Middleware;
using SL.Host.Web.Swagger;
using SL.Module.Abstractions;
using SL.Module.Core;
using SL.Module.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostOptions = SL.Host.Web.Options.HostOptions;

namespace SL.Host.Web
{
    public class StartupAbstract
    {
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _cfg;

        public StartupAbstract(IHostEnvironment env, IConfiguration cfg)
        {
            _env = env;
            _cfg = cfg;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var hostOptions = services.GetService<HostOptions>();

            //注入特性服务
            services.AddServicesFromAttribute();

            //添加模块
            var modules = services.AddModulesCore(_env, _cfg);

            //添加Swagger
            services.AddSwagger(modules, hostOptions);

            //添加缓存
            services.AddCache(_cfg);

            //添加对象映射
            services.AddMappers(modules);

            //添加MVC配置
            services.AddMvc(modules, hostOptions, _env);

            //添加CORS
            services.AddCors(hostOptions);


            //解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            //添加HttpClient相关,服务里面可以发起HTTP请求
            services.AddHttpClient();

            //添加模块的自定义特有的服务
            services.AddModuleServices(modules, _env, _cfg);

            //添加身份认证和授权
            services.AddMkhAuth(_cfg).UseJwt();

            //添加数据库            
            services.AddSqlSugarDb(_env,modules);
        }


        public virtual void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            var hostOptions = app.ApplicationServices.GetService<HostOptions>();
            var modules = app.ApplicationServices.GetRequiredService<IModuleCollection>();

            //使用全局异常处理中间件
            app.UseMiddleware<ExceptionHandleMiddleware>();

            //路由
            app.UseRouting();

            //CORS
            app.UseCors("Default");

            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            //配置端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //启用Swagger
            app.UseSwagger(modules, hostOptions);

            //使用模块化
            app.UseModules(modules);

            appLifetime.ApplicationStarted.Register(() =>
            {
                //显示启动Banner
                var options = app.ApplicationServices.GetService<HostOptions>();
                ConsoleBanner(options);
            });
        }


        /// <summary>
        /// 启动后打印Banner图案
        /// </summary>
        private void ConsoleBanner(HostOptions options)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(@" ***************************************************************************************************************");
            Console.WriteLine(@" *                                                                                                             *");
            Console.WriteLine(@" *                           ______   __             ______                                              *");
            Console.WriteLine(@" *                          /      \ /  |           /      \                                                   *");
            Console.WriteLine(@" *                         /$$$$$$  |$$ |          /$$$$$$  |  ______    ______    ______                      *");
            Console.WriteLine(@" *                         $$ \__$$/ $$ |          $$ |  $$/  /      \  /      \  /      \                     *");
            Console.WriteLine(@" *                         $$      \ $$ |          $$ |      /$$$$$$  |/$$$$$$  |/$$$$$$  |                    *");
            Console.WriteLine(@" *                          $$$$$$  |$$ |          $$ |   __ $$ |  $$ |$$ |  $$/ $$    $$ |                    *");
            Console.WriteLine(@" *                         /  \__$$ |$$ |_____  __ $$ \__/  |$$ \__$$ |$$ |      $$$$$$$$/                     *");
            Console.WriteLine(@" *                         $$    $$/ $$       |/  |$$    $$/ $$    $$/ $$ |      $$       |                    *");
            Console.WriteLine(@" *                          $$$$$$/  $$$$$$$$/ $$/  $$$$$$/   $$$$$$/  $$/        $$$$$$$/                     *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.Write(@" *                                      ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@$"启动成功，欢迎使用 SAAS平台~~");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                             *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.Write(@" *                                      ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"接口地址：" + options.Urls);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                                                *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.WriteLine(@" ***************************************************************************************************************");
            Console.WriteLine();
        }
    }
}
