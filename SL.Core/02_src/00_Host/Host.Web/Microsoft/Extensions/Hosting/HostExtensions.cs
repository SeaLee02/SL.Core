using Com.Ctrip.Framework.Apollo;
using Microsoft.Extensions.Configuration;
using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public static class HostExtensions
    {
        public static IHostBuilder AddApolloConfig(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                //bool isEnable = configuration["RabbitMQ:IsEnable"].ToBool();
                if (configurationBuilder.Build()["Apollo:IsEnable"].ToBool())
                {
                    //注入配置
                    //把阿波罗的日志级别调整为最低
                    Com.Ctrip.Framework.Apollo.Logging.LogManager.UseConsoleLogging(Com.Ctrip.Framework.Apollo.Logging.LogLevel.Trace);
                    //从已加载的配置文件中读取，阿波罗的基础配置，并注入
                    configurationBuilder.AddApollo(configurationBuilder.Build().GetSection("Apollo"))
                    .AddDefault(Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Properties);
                }              
            });
            return hostBuilder;
        }
    }
}
