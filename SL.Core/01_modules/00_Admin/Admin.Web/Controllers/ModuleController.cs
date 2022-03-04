using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SL.Auth.Abstractions;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    [SwaggerTag("模块管理")]
   
    public class ModuleController : BaseController
    {
        private readonly IPermissionResolver _permissionResolver;

        public ModuleController(IPermissionResolver permissionResolver)
        {
            _permissionResolver = permissionResolver;
        }

        /// <summary>
        /// 获取指定模块的权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IResultModel Permissions([BindRequired] string moduleCode)
        {
            return ResultModel.Success(_permissionResolver.GetPermissions(moduleCode));
        }

    }
}