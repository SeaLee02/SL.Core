using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SL.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Host.Web.Middleware
{
    public class IPLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger _logger;
        private readonly DateTimeHelper _dateTimeHelper;

        public IPLogMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<IPLogMiddleware> logger, DateTimeHelper dateTimeHelper)
        {
            _next = next;
            _env = env;
            _logger = logger;
            this._dateTimeHelper = dateTimeHelper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 过滤，只有接口
            if (context.Request.Path.Value.Contains("api"))
            {
                var request = context.Request;
                dynamic dy = new ExpandoObject();
                dy.Ip = GetClientIP(context);
                dy.Url = $"{request.Path}";
                dy.Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dy.Date = DateTime.Now.ToString("yyyy-MM-dd");
                dy.Week = _dateTimeHelper.GetWeek();
                string requestInfo = JsonConvert.SerializeObject(dy);
                ConsoleHelper.WriteSuccessLine(requestInfo);
            }
            await _next(context);

        }


        public static string GetClientIP(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }


    }
}
