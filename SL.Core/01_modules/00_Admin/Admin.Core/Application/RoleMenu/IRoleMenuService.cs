using Microsoft.AspNetCore.Http;
using SL.Mkh.Admin.Core.Application.RoleMenu.Dto;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RoleMenu
{
     /// <summary>
    /// 角色菜单关系
    /// </summary>
    public interface IRoleMenuService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> GetList(RoleMenuQueryDto dto);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid primaryKey);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(RoleMenuAddDto dto);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Update(RoleMenuAddDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<IResultModel> Del(Guid primaryKey);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
        Task<byte[]> ExportExcel(RoleMenuQueryDto dto);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<IResultModel> ImportExcel(IFormFile formFile);
    }
}
