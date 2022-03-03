using Microsoft.AspNetCore.Http;
using SL.Mkh.Admin.Core.Application.RolePermission.Dto;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RolePermission
{
     /// <summary>
    /// 角色权限表
    /// </summary>
    public interface IRolePermissionService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> GetList(RolePermissionQueryDto dto);

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
        Task<IResultModel> Add(RolePermissionAddDto dto);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Update(RolePermissionAddDto dto);

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
        Task<byte[]> ExportExcel(RolePermissionQueryDto dto);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<IResultModel> ImportExcel(IFormFile formFile);
    }
}
