using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.RoleMenu.Dto;
using SL.Mkh.Admin.Core.Domain.RoleMenu;
using SL.Utils.Helpers;
using SL.Utils.Map;
using SL.Utils.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RoleMenu
{
    /// <summary>
    /// 角色菜单关系
    /// </summary>
    public class RoleMenuService: IRoleMenuService
    {
        private readonly IMapper _mapper;
        private readonly IRoleMenuRepository _roleMenuRepository;

        public RoleMenuService(IMapper mapper, IRoleMenuRepository roleMenuRepository)
        {
            this._mapper = mapper;
            this._roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(RoleMenuQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._roleMenuRepository.Find().Filter("!=0").Select(a => new RoleMenuListDto
            {
                RoleMenuId = a.RoleMenuId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._roleMenuRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<RoleMenuListDto> resultDto = _mapper.Map<QueryResultModel<RoleMenuListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetInfoById(Guid primaryKey)
        {
            var entity = await this._roleMenuRepository.Get(primaryKey);
            var result = _mapper.Map<RoleMenuDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(RoleMenuAddDto dto)
        {
            var entity = _mapper.Map<RoleMenuEntity>(dto);
            await this._roleMenuRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(RoleMenuAddDto dto)
        {
            var entity = await _roleMenuRepository.Get(dto.RoleMenuId);
            _mapper.Map(dto, entity);
            await this._roleMenuRepository.Update(entity);
            return ResultModel.Success("更新成功");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IResultModel> Del(Guid primaryKey)
        {
            await this._roleMenuRepository.SoftDelete(primaryKey);
            return ResultModel.Success("删除成功");
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<IResultModel> ImportExcel(IFormFile formFile)
        {
            DataTable dt = new DataTable();
            List<RoleMenuImportDto> data = new List<RoleMenuImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<RoleMenuImportDto>();
                data = dt.Mapper<RoleMenuImportDto>(true);
            }
            catch
            {
                throw new Exception($"请上传正确的模板数据");
            }

            //验证必填问题
            var _va = data.ValidationNotNull();
            if (!_va.isOk)
            {
                return ResultModel.Failed(_va.errorMsg);
            }

            var result = _mapper.Map<List<RoleMenuEntity>>(data);
            await this._roleMenuRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(RoleMenuQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<RoleMenuListDto>> _cdata = (ResultModel<QueryResultModel<RoleMenuListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<RoleMenuExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}