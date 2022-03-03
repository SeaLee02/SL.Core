using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.UserOrg.Dto;
using SL.Mkh.Admin.Core.Domain.UserOrg;
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

namespace SL.Mkh.Admin.Core.Application.UserOrg
{
    /// <summary>
    /// 账号组织关系表
    /// </summary>
    public class UserOrgService: IUserOrgService
    {
        private readonly IMapper _mapper;
        private readonly IUserOrgRepository _userOrgRepository;

        public UserOrgService(IMapper mapper, IUserOrgRepository userOrgRepository)
        {
            this._mapper = mapper;
            this._userOrgRepository = userOrgRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(UserOrgQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._userOrgRepository.Find().Filter("!=0").Select(a => new UserOrgListDto
            {
                UserOrgId = a.UserOrgId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._userOrgRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<UserOrgListDto> resultDto = _mapper.Map<QueryResultModel<UserOrgListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._userOrgRepository.Get(primaryKey);
            var result = _mapper.Map<UserOrgDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(UserOrgAddDto dto)
        {
            var entity = _mapper.Map<UserOrgEntity>(dto);
            await this._userOrgRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(UserOrgAddDto dto)
        {
            var entity = await _userOrgRepository.Get(dto.UserOrgId);
            _mapper.Map(dto, entity);
            await this._userOrgRepository.Update(entity);
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
            await this._userOrgRepository.SoftDelete(primaryKey);
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
            List<UserOrgImportDto> data = new List<UserOrgImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<UserOrgImportDto>();
                data = dt.Mapper<UserOrgImportDto>(true);
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

            var result = _mapper.Map<List<UserOrgEntity>>(data);
            await this._userOrgRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(UserOrgQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<UserOrgListDto>> _cdata = (ResultModel<QueryResultModel<UserOrgListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<UserOrgExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}