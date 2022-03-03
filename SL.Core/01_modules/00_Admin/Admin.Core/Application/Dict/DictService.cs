using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.Dict.Dto;
using SL.Mkh.Admin.Core.Domain.Dict;
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

namespace SL.Mkh.Admin.Core.Application.Dict
{
    /// <summary>
    /// 字典表
    /// </summary>
    public class DictService: IDictService
    {
        private readonly IMapper _mapper;
        private readonly IDictRepository _dictRepository;

        public DictService(IMapper mapper, IDictRepository dictRepository)
        {
            this._mapper = mapper;
            this._dictRepository = dictRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(DictQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._dictRepository.Find().Filter("!=0").Select(a => new DictListDto
            {
                DictId = a.DictId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._dictRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<DictListDto> resultDto = _mapper.Map<QueryResultModel<DictListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._dictRepository.Get(primaryKey);
            var result = _mapper.Map<DictDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(DictAddDto dto)
        {
            var entity = _mapper.Map<DictEntity>(dto);
            await this._dictRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(DictAddDto dto)
        {
            var entity = await _dictRepository.Get(dto.DictId);
            _mapper.Map(dto, entity);
            await this._dictRepository.Update(entity);
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
            await this._dictRepository.SoftDelete(primaryKey);
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
            List<DictImportDto> data = new List<DictImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<DictImportDto>();
                data = dt.Mapper<DictImportDto>(true);
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

            var result = _mapper.Map<List<DictEntity>>(data);
            await this._dictRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(DictQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<DictListDto>> _cdata = (ResultModel<QueryResultModel<DictListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<DictExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}