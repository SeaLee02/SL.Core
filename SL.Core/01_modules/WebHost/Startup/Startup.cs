using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SL.Host.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHost
{
    /// <summary>
    /// ¿ªÊ¼³ÌÐò
    /// </summary>
    public class Startup : StartupAbstract
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="cfg"></param>
        public Startup(IHostEnvironment env, IConfiguration cfg) : base(env, cfg)
        {

        }
    }
}
