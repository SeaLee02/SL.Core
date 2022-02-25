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
    /// ��Ŀ����
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            new HostBootstrap(args).Run<Startup>();
        }
    }
}
