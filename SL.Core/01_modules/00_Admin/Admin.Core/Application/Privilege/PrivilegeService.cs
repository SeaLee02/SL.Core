using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.Privilege.Dto;
using SL.Mkh.Admin.Core.Domain.Privilege;
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

namespace SL.Mkh.Admin.Core.Application.Privilege
{
    /// <summary>
    /// 数据权限设置
    /// </summary>
    public class PrivilegeService: IPrivilegeService
    {
        private readonly IMapper _mapper;
        private readonly IPrivilegeRepository _privilegeRepository;

        public PrivilegeService(IMapper mapper, IPrivilegeRepository privilegeRepository)
        {
            this._mapper = mapper;
            this._privilegeRepository = privilegeRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(PrivilegeQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._privilegeRepository.Find().Filter("!=0").Select(a => new PrivilegeListDto
            {
                PrivilegeId = a.PrivilegeId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._privilegeRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<PrivilegeListDto> resultDto = _mapper.Map<QueryResultModel<PrivilegeListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._privilegeRepository.Get(primaryKey);
            var result = _mapper.Map<PrivilegeDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(PrivilegeAddDto dto)
        {
            var entity = _mapper.Map<PrivilegeEntity>(dto);
            await this._privilegeRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(PrivilegeAddDto dto)
        {
            var entity = await _privilegeRepository.Get(dto.PrivilegeId);
            _mapper.Map(dto, entity);
            await this._privilegeRepository.Update(entity);
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
            await this._privilegeRepository.SoftDelete(primaryKey);
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
            List<PrivilegeImportDto> data = new List<PrivilegeImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<PrivilegeImportDto>();
                data = dt.Mapper<PrivilegeImportDto>(true);
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

            var result = _mapper.Map<List<PrivilegeEntity>>(data);
            await this._privilegeRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(PrivilegeQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<PrivilegeListDto>> _cdata = (ResultModel<QueryResultModel<PrivilegeListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<PrivilegeExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}