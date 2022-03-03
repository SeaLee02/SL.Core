using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.Button.Dto;
using SL.Mkh.Admin.Core.Domain.Button;
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

namespace SL.Mkh.Admin.Core.Application.Button
{
    /// <summary>
    /// 按钮表
    /// </summary>
    public class ButtonService: IButtonService
    {
        private readonly IMapper _mapper;
        private readonly IButtonRepository _buttonRepository;

        public ButtonService(IMapper mapper, IButtonRepository buttonRepository)
        {
            this._mapper = mapper;
            this._buttonRepository = buttonRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(ButtonQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._buttonRepository.Find().Filter("!=0").Select(a => new ButtonListDto
            {
                ButtonId = a.ButtonId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._buttonRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<ButtonListDto> resultDto = _mapper.Map<QueryResultModel<ButtonListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._buttonRepository.Get(primaryKey);
            var result = _mapper.Map<ButtonDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(ButtonAddDto dto)
        {
            var entity = _mapper.Map<ButtonEntity>(dto);
            await this._buttonRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(ButtonAddDto dto)
        {
            var entity = await _buttonRepository.Get(dto.ButtonId);
            _mapper.Map(dto, entity);
            await this._buttonRepository.Update(entity);
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
            await this._buttonRepository.SoftDelete(primaryKey);
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
            List<ButtonImportDto> data = new List<ButtonImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<ButtonImportDto>();
                data = dt.Mapper<ButtonImportDto>(true);
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

            var result = _mapper.Map<List<ButtonEntity>>(data);
            await this._buttonRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(ButtonQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<ButtonListDto>> _cdata = (ResultModel<QueryResultModel<ButtonListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<ButtonExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}