using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SL.Host.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHost
{
    /// <summary>
    /// 项目启动
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 项目入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            new HostBootstrap(args).Run<Startup>();
        }
    }
}
