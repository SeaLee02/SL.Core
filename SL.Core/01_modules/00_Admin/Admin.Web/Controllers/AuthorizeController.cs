using SL.Mkh.Admin.Core.Application.Authorize;
using SL.Mkh.Admin.Core.Application.Authorize.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    [SwaggerTag("身份认证")]
    [AllowAnonymous]
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
    
        public Task<IResultModel> Login(LoginDto dto)
        {
            return _authorizeService.Login(dto);
        }

        [HttpGet]
        public Task<IResultModel> RefreshToken([FromQuery] string token = "") 
        {
            return _authorizeService.RefreshToken(token);
        }



        [HttpGet]
        public Task<IResultModel> Test()
        {
            return Task.FromResult(ResultModel.Success());
        }
    }
}
