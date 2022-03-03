using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.MenuGroup.Dto;
using SL.Mkh.Admin.Core.Domain.MenuGroup;
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

namespace SL.Mkh.Admin.Core.Application.MenuGroup
{
    /// <summary>
    /// 菜单组
    /// </summary>
    public class MenuGroupService: IMenuGroupService
    {
        private readonly IMapper _mapper;
        private readonly IMenuGroupRepository _menuGroupRepository;

        public MenuGroupService(IMapper mapper, IMenuGroupRepository menuGroupRepository)
        {
            this._mapper = mapper;
            this._menuGroupRepository = menuGroupRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(MenuGroupQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._menuGroupRepository.Find().Filter("!=0").Select(a => new MenuGroupListDto
            {
                MenuGroupId = a.MenuGroupId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._menuGroupRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<MenuGroupListDto> resultDto = _mapper.Map<QueryResultModel<MenuGroupListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._menuGroupRepository.Get(primaryKey);
            var result = _mapper.Map<MenuGroupDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(MenuGroupAddDto dto)
        {
            var entity = _mapper.Map<MenuGroupEntity>(dto);
            await this._menuGroupRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(MenuGroupAddDto dto)
        {
            var entity = await _menuGroupRepository.Get(dto.MenuGroupId);
            _mapper.Map(dto, entity);
            await this._menuGroupRepository.Update(entity);
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
            await this._menuGroupRepository.SoftDelete(primaryKey);
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
            List<MenuGroupImportDto> data = new List<MenuGroupImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<MenuGroupImportDto>();
                data = dt.Mapper<MenuGroupImportDto>(true);
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

            var result = _mapper.Map<List<MenuGroupEntity>>(data);
            await this._menuGroupRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(MenuGroupQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<MenuGroupListDto>> _cdata = (ResultModel<QueryResultModel<MenuGroupListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<MenuGroupExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}