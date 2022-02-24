using Microsoft.AspNetCore.Http;
using SL.Mkh.Admin.Core.Application.Role.Dto;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Role
{
     /// <summary>
    /// 角色表
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> GetList(RoleQueryDto dto);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<IResultModel> GetInfoById(Guid primaryKey);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(RoleAddDto dto);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Update(RoleAddDto dto);

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
        Task<byte[]> ExportExcel(RoleQueryDto dto);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<IResultModel> ImportExcel(IFormFile formFile);
    }
}
