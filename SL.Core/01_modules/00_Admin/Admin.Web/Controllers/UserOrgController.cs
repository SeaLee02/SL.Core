using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Mkh.Admin.Core.Application.UserOrg;
using SL.Mkh.Admin.Core.Application.UserOrg.Dto;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    
    [SwaggerTag("账号组织关系表管理")]
    public class UserOrgController : BaseController
    {
        private readonly IUserOrgService _userOrgService;
        public UserOrgController(IUserOrgService userOrgService)
        {
            _userOrgService = userOrgService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultModel> GetList([FromQuery] UserOrgQueryDto dto)
        {
            return await _userOrgService.GetList(dto);
        }

        /// <summary>
        ///  详情
        /// </summary>
        /// <param name="primaryKey">主键Id</param>
        /// <returns></returns>
        [HttpGet("{primaryKey}")]
        public async Task<IResultModel> GetInfoById(Guid primaryKey)
        {
            return await this._userOrgService.GetInfoById(primaryKey);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultModel> Add(UserOrgAddDto dto)
        {

            return await _userOrgService.Add(dto);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResultModel> Update(UserOrgAddDto dto)
        {

            return await _userOrgService.Update(dto);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{primaryKey}")]
        public async Task<IResultModel> Del(Guid primaryKey)
        {
            return await _userOrgService.Del(primaryKey);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileContentResult> ExportExcel(UserOrgQueryDto dto)
        {
            var buff = await _userOrgService.ExportExcel(dto);
            FileContentResult returnFile = new FileContentResult(buff, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            returnFile.FileDownloadName = "账号列表.xlsx";
            return returnFile;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultModel> ImportExcel(IFormFile formFile)
        {
            return await _userOrgService.ImportExcel(formFile);
        }



    }
}
