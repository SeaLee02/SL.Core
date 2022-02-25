using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Mkh.Admin.Core.Application.MenuGroup;
using SL.Mkh.Admin.Core.Application.MenuGroup.Dto;
using SL.Utils.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Web.Controllers
{
    
    [SwaggerTag("菜单组管理")]
    public class MenuGroupController : BaseController
    {
        private readonly IMenuGroupService _menuGroupService;
        public MenuGroupController(IMenuGroupService menuGroupService)
        {
            _menuGroupService = menuGroupService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultModel> GetList([FromQuery] MenuGroupQueryDto dto)
        {
            return await _menuGroupService.GetList(dto);
        }

        /// <summary>
        ///  详情
        /// </summary>
        /// <param name="primaryKey">主键Id</param>
        /// <returns></returns>
        [HttpGet("{primaryKey}")]
        public async Task<IResultModel> GetInfoById(Guid primaryKey)
        {
            return await this._menuGroupService.GetInfoById(primaryKey);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultModel> Add(MenuGroupAddDto dto)
        {

            return await _menuGroupService.Add(dto);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResultModel> Update(MenuGroupAddDto dto)
        {

            return await _menuGroupService.Update(dto);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{primaryKey}")]
        public async Task<IResultModel> Del(Guid primaryKey)
        {
            return await _menuGroupService.Del(primaryKey);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileContentResult> ExportExcel(MenuGroupQueryDto dto)
        {
            var buff = await _menuGroupService.ExportExcel(dto);
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
            return await _menuGroupService.ImportExcel(formFile);
        }



    }
}
