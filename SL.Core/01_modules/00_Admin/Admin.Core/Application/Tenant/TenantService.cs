using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.Tenant.Dto;
using SL.Mkh.Admin.Core.Domain.Tenant;
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

namespace SL.Mkh.Admin.Core.Application.Tenant
{
    /// <summary>
    /// 租户表Id
    /// </summary>
    public class TenantService: ITenantService
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository _tenantRepository;

        public TenantService(IMapper mapper, ITenantRepository tenantRepository)
        {
            this._mapper = mapper;
            this._tenantRepository = tenantRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(TenantQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._tenantRepository.Find().Filter("!=0").Select(a => new TenantListDto
            {
                TenantId = a.TenantId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._tenantRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<TenantListDto> resultDto = _mapper.Map<QueryResultModel<TenantListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._tenantRepository.Get(primaryKey);
            var result = _mapper.Map<TenantDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(TenantAddDto dto)
        {
            var entity = _mapper.Map<TenantEntity>(dto);
            await this._tenantRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(TenantAddDto dto)
        {
            var entity = await _tenantRepository.Get(dto.TenantId);
            _mapper.Map(dto, entity);
            await this._tenantRepository.Update(entity);
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
            await this._tenantRepository.SoftDelete(primaryKey);
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
            List<TenantImportDto> data = new List<TenantImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<TenantImportDto>();
                data = dt.Mapper<TenantImportDto>(true);
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

            var result = _mapper.Map<List<TenantEntity>>(data);
            await this._tenantRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(TenantQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<TenantListDto>> _cdata = (ResultModel<QueryResultModel<TenantListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<TenantExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}