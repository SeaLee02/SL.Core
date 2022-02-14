using Lh.Mkh.Admin.Core.Application.Authorize;
using Lh.Mkh.Admin.Core.Application.Authorize.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lh.Mkh.Admin.Web.Controllers
{
    [SwaggerTag("身份认证")]
    public class AuthorizeController : BaseController
    {
        private readonly IAuthorizeService _authorizeService;
        public AuthorizeController(IAuthorizeService authorizeService)
        {
            this._authorizeService = authorizeService;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Task<IResultModel> Login(LoginDto dto)
        {
            return _authorizeService.Login(dto);
        }

        [HttpGet]
        public Task<IResultModel> Test()
        {
            return Task.FromResult(ResultModel.Success());
        }
    }
}
