using Lh.Mkh.Admin.Core.Application.Authorize.Dto;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lh.Mkh.Admin.Core.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Login(LoginDto dto);
    }
}
