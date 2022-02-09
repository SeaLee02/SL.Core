using Castle.DynamicProxy;
using SL.Data.Abstractions;
using SL.Data.Core;
using SL.Data.Core.Internal;
using SL.Module.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Utils.Extensions;
using SL.Utils.Helpers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 配置SqlSugar ORM框架
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarDb(this IServiceCollection services, IHostEnvironment env, IModuleCollection modules)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //注入仓储层
            //仓储统一放在Repositories目录中
            //尝试添加代理生成器
            services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();
            foreach (var module in modules)
            {                                         //AOP动态代理
                foreach (var dic in module.ApplicationServices)
                {
                 
                    //添加需要特性事务的服务
                    services.AddScoped(dic.Key, sp =>
                    {
                        var target = sp.GetService(dic.Value);
                        var generator = sp.GetService<IProxyGenerator>();
                        var unitOfWork = sp.GetService<IUnitOfWork>();
                        var interceptor = new TransactionInterceptor(unitOfWork);
                        var proxy = generator!.CreateInterfaceProxyWithTarget(dic.Key, target, interceptor);
                        return proxy;
                    });

                }



                var repositoryTypes = module.LayerAssemblies.Core.GetTypes()
                               .Where(m => !m.IsInterface && typeof(IRepository).IsImplementType(m))
                               //排除兼容仓储
                               .Where(m => m.FullName!.Split('.')[^2].EqualsIgnoreCase("Repositories"))
                               .ToList();

                foreach (var type in repositoryTypes)
                {
                    //按照框架约定，仓储的第三个接口类型就是所需的仓储接口
                    var interfaceType = type.GetInterfaces()[2];

                    services.AddScoped(type);
                    services.Add(new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped));
                    // module.ApplicationServices.Add(interfaceType, type);
                }




            }






            List<ConnectionConfig> connectionList = new List<ConnectionConfig>();
            foreach (var module in modules)
            {
                var dbOptions = module.Options!.Db;
                ConnectionConfig config = new ConnectionConfig();
                config.ConnectionString = dbOptions.ConnectionString;
                config.DbType = (DbType)dbOptions.DbType;
                config.ConfigId = dbOptions.ConfigId;
                config.IsAutoCloseConnection = true;
                if (config.DbType == DbType.SqlServer)
                {
                    config.MoreSettings = new ConnMoreSettings()
                    {
                        IsWithNoLockQuery = true//查询
                    };
                }
                connectionList.Add(config);
            }

            // 把多个连接对象注入服务，这里必须采用Scope，因为有事务操作
            services.AddScoped<ISqlSugarClient>(o =>
            {
                //var accountResolver = o.GetService<IUserResolver>();
                var db = new SqlSugarScope(connectionList,
                db =>
                {
                    foreach (var module in modules)
                    {
                        var dbOptions = module.Options!.Db;
                        if (dbOptions.IsFilter)
                        {
                            db.GetConnection(dbOptions.ConfigId).QueryFilter.Add(new SqlFilterItem
                            {
                                FilterValue = filter =>
                                {
                                    return new SqlFilterResult { Sql = " Deleted=0 " };
                                }
                            });

                            db.GetConnection(dbOptions.ConfigId).QueryFilter.Add(new SqlFilterItem
                            {
                                //多表过滤,多表统一设置 a,b.c...以此类推
                                FilterValue = filter =>
                                {
                                    return new SqlFilterResult { Sql = " a.Deleted=0 " };
                                },
                                IsJoinQuery = true
                            });
                        }
                                                                                     
                        if (env.IsDevelopment())
                        {
                            if (dbOptions.IsLogSql)
                            {

                                db.GetConnection((string)dbOptions.ConfigId).Aop.OnLogExecuting = (sql, p) =>
                                {
                                    ConsoleHelper.WriteSuccessLine(sql);
                                };
                            }
                        }
                    }
                });

                return db;
            });

            return services;
        }



    }
}
