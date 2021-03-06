using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.RoleButton.Dto;
using SL.Mkh.Admin.Core.Domain.RoleButton;
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

namespace SL.Mkh.Admin.Core.Application.RoleButton
{
    /// <summary>
    /// 角色按钮关系
    /// </summary>
    public class RoleButtonService: IRoleButtonService
    {
        private readonly IMapper _mapper;
        private readonly IRoleButtonRepository _roleButtonRepository;

        public RoleButtonService(IMapper mapper, IRoleButtonRepository roleButtonRepository)
        {
            this._mapper = mapper;
            this._roleButtonRepository = roleButtonRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(RoleButtonQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._roleButtonRepository.Find().Filter("!=0").Select(a => new RoleButtonListDto
            {
                RoleButtonId = a.RoleButtonId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._roleButtonRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<RoleButtonListDto> resultDto = _mapper.Map<QueryResultModel<RoleButtonListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._roleButtonRepository.Get(primaryKey);
            var result = _mapper.Map<RoleButtonDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(RoleButtonAddDto dto)
        {
            var entity = _mapper.Map<RoleButtonEntity>(dto);
            await this._roleButtonRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(RoleButtonAddDto dto)
        {
            var entity = await _roleButtonRepository.Get(dto.RoleButtonId);
            _mapper.Map(dto, entity);
            await this._roleButtonRepository.Update(entity);
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
            await this._roleButtonRepository.SoftDelete(primaryKey);
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
            List<RoleButtonImportDto> data = new List<RoleButtonImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<RoleButtonImportDto>();
                data = dt.Mapper<RoleButtonImportDto>(true);
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

            var result = _mapper.Map<List<RoleButtonEntity>>(data);
            await this._roleButtonRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(RoleButtonQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<RoleButtonListDto>> _cdata = (ResultModel<QueryResultModel<RoleButtonListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<RoleButtonExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}