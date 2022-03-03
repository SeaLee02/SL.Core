using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.UserRole.Dto;
using SL.Mkh.Admin.Core.Domain.UserRole;
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

namespace SL.Mkh.Admin.Core.Application.UserRole
{
    /// <summary>
    /// 用户角色关系表
    /// </summary>
    public class UserRoleService: IUserRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            this._mapper = mapper;
            this._userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(UserRoleQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._userRoleRepository.Find().Filter("!=0").Select(a => new UserRoleListDto
            {
                UserRoleId = a.UserRoleId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._userRoleRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<UserRoleListDto> resultDto = _mapper.Map<QueryResultModel<UserRoleListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._userRoleRepository.Get(primaryKey);
            var result = _mapper.Map<UserRoleDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(UserRoleAddDto dto)
        {
            var entity = _mapper.Map<UserRoleEntity>(dto);
            await this._userRoleRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(UserRoleAddDto dto)
        {
            var entity = await _userRoleRepository.Get(dto.UserRoleId);
            _mapper.Map(dto, entity);
            await this._userRoleRepository.Update(entity);
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
            await this._userRoleRepository.SoftDelete(primaryKey);
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
            List<UserRoleImportDto> data = new List<UserRoleImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<UserRoleImportDto>();
                data = dt.Mapper<UserRoleImportDto>(true);
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

            var result = _mapper.Map<List<UserRoleEntity>>(data);
            await this._userRoleRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(UserRoleQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<UserRoleListDto>> _cdata = (ResultModel<QueryResultModel<UserRoleListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<UserRoleExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }
    }
}