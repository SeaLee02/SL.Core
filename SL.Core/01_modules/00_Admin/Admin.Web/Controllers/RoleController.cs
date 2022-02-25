using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Mkh.Admin.Core.Application.Role;
using SL.Mkh.Admin.Core.Application.Role.Dto;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    
    [SwaggerTag("角色表管理")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultModel> GetList([FromQuery] RoleQueryDto dto)
        {
            return await _roleService.GetList(dto);
        }

        /// <summary>
        ///  详情
        /// </summary>
        /// <param name="primaryKey">主键Id</param>
        /// <returns></returns>
        [HttpGet("{primaryKey}")]
        public async Task<IResultModel> GetInfoById(Guid primaryKey)
        {
            return await this._roleService.GetInfoById(primaryKey);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultModel> Add(RoleAddDto dto)
        {

            return await _roleService.Add(dto);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResultModel> Update(RoleAddDto dto)
        {

            return await _roleService.Update(dto);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{primaryKey}")]
        public async Task<IResultModel> Del(Guid primaryKey)
        {
            return await _roleService.Del(primaryKey);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileContentResult> ExportExcel(RoleQueryDto dto)
        {
            var buff = await _roleService.ExportExcel(dto);
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
            return await _roleService.ImportExcel(formFile);
        }



    }
}
