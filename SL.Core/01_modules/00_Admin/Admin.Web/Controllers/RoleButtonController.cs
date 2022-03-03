using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Mkh.Admin.Core.Application.RoleButton;
using SL.Mkh.Admin.Core.Application.RoleButton.Dto;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    
    [SwaggerTag("角色按钮关系管理")]
    public class RoleButtonController : BaseController
    {
        private readonly IRoleButtonService _roleButtonService;
        public RoleButtonController(IRoleButtonService roleButtonService)
        {
            _roleButtonService = roleButtonService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultModel> GetList([FromQuery] RoleButtonQueryDto dto)
        {
            return await _roleButtonService.GetList(dto);
        }

        /// <summary>
        ///  详情
        /// </summary>
        /// <param name="primaryKey">主键Id</param>
        /// <returns></returns>
        [HttpGet("{primaryKey}")]
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            return await this._roleButtonService.Edit(primaryKey);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultModel> Add(RoleButtonAddDto dto)
        {

            return await _roleButtonService.Add(dto);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResultModel> Update(RoleButtonAddDto dto)
        {

            return await _roleButtonService.Update(dto);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{primaryKey}")]
        public async Task<IResultModel> Del(Guid primaryKey)
        {
            return await _roleButtonService.Del(primaryKey);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileContentResult> ExportExcel(RoleButtonQueryDto dto)
        {
            var buff = await _roleButtonService.ExportExcel(dto);
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
            return await _roleButtonService.ImportExcel(formFile);
        }



    }
}
