using Microsoft.AspNetCore.Mvc.Filters;
using SL.Auth.Abstractions;
using SL.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Host.Web.Filter
{
    /// <summary>
    /// 接口过滤
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();        

        }
    }
}
