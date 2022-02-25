using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.RolePermission.Dto;
using SL.Mkh.Admin.Core.Domain.RolePermission;
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

namespace SL.Mkh.Admin.Core.Application.RolePermission
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class RolePermissionService: IRolePermissionService
    {
        private readonly IMapper _mapper;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IMapper mapper, IRolePermissionRepository rolePermissionRepository)
        {
            this._mapper = mapper;
            this._rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(RolePermissionQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._rolePermissionRepository.Find().Filter("!=0").Select(a => new RolePermissionListDto
            {
                RolePermissionId = a.RolePermissionId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._rolePermissionRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<RolePermissionListDto> resultDto = _mapper.Map<QueryResultModel<RolePermissionListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetInfoById(Guid primaryKey)
        {
            var entity = await this._rolePermissionRepository.Get(primaryKey);
            var result = _mapper.Map<RolePermissionDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(RolePermissionAddDto dto)
        {
            var entity = _mapper.Map<RolePermissionEntity>(dto);
            await this._rolePermissionRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(RolePermissionAddDto dto)
        {
            var entity = await _rolePermissionRepository.Get(dto.RolePermissionId);
            _mapper.Map(dto, entity);
            await this._rolePermissionRepository.Update(entity);
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
            await this._rolePermissionRepository.SoftDelete(primaryKey);
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
            List<RolePermissionImportDto> data = new List<RolePermissionImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<RolePermissionImportDto>();
                data = dt.Mapper<RolePermissionImportDto>(true);
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

            var result = _mapper.Map<List<RolePermissionEntity>>(data);
            await this._rolePermissionRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(RolePermissionQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<RolePermissionListDto>> _cdata = (ResultModel<QueryResultModel<RolePermissionListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<RolePermissionExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}