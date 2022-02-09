using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Auth.Core
{
    public class SLAuthBuilder
    {
        public IServiceCollection Services { get; set; }

        /// <summary>
        /// 配置属性
        /// </summary>
        public IConfiguration Configuration { get; set; }

        public SLAuthBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }
    }
}
