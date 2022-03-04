using Microsoft.AspNetCore.Http;
using SL.Auth.Abstractions;
using SL.Data.Abstractions.Annotations;
using SL.Data.Abstractions.Query;
using SL.Excel.Aspose;
using SL.Mkh.Admin.Core.Application.Permission.Dto;
using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Mkh.Admin.Core.Domain.RolePermission;
using SL.Utils.Extensions;
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

namespace SL.Mkh.Admin.Core.Application.Permission
{
    /// <summary>
    /// 接口权限地址
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionResolver _permissionResolver;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public PermissionService(IMapper mapper, IPermissionRepository permissionRepository, IPermissionResolver permissionResolver, IRolePermissionRepository rolePermissionRepository)
        {
            this._mapper = mapper;
            this._permissionRepository = permissionRepository;
            this._permissionResolver = permissionResolver;
            this._rolePermissionRepository = rolePermissionRepository; 
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetList(PermissionQueryDto dto)
        {
            //01直接走实体映射
            var result = await this._permissionRepository.Find().Select(a => new PermissionListDto
            {
                PermissionId = a.PermissionId.SelectAll()
            }).ToPaginationAsync(dto.Paging);
            return ResultModel.Success(result);

            //02或者mapper
            //var result = await this._permissionRepository.Find().Filter("!=0").ToPaginationAsync(dto.Paging);
            //QueryResultModel<PermissionListDto> resultDto = _mapper.Map<QueryResultModel<PermissionListDto>>(result);
            //return ResultModel.Success(resultDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid primaryKey)
        {
            var entity = await this._permissionRepository.Get(primaryKey);
            var result = _mapper.Map<PermissionDto>(entity);
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Add(PermissionAddDto dto)
        {
            var entity = _mapper.Map<PermissionEntity>(dto);
            await this._permissionRepository.Add(entity);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel> Update(PermissionAddDto dto)
        {
            var entity = await _permissionRepository.Get(dto.PermissionId);
            _mapper.Map(dto, entity);
            await this._permissionRepository.Update(entity);
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
            await this._permissionRepository.SoftDelete(primaryKey);
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
            List<PermissionImportDto> data = new List<PermissionImportDto>();
            try
            {
                using MemoryStream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                dt = ExcelHelper.Excel2Table(stream);
                dt.ReplaceColumnName<PermissionImportDto>();
                data = dt.Mapper<PermissionImportDto>(true);
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

            var result = _mapper.Map<List<PermissionEntity>>(data);
            await this._permissionRepository.AddList(result);
            return ResultModel.Success($"导入成功数据: {data.Count} ");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(PermissionQueryDto dto)
        {
            byte[] buff = null;
            dto.Page.Size = 999999;
            dto.Page.Index = 1;
            ResultModel<QueryResultModel<PermissionListDto>> _cdata = (ResultModel<QueryResultModel<PermissionListDto>>)await this.GetList(dto);
            if (_cdata != null)
            {
                if (_cdata.Data != null)
                {
                    var date = _mapper.Map<List<PermissionExportDto>>(_cdata.Data.Data);
                    buff = ExcelHelper.GetExcelForList(date);
                }
            }
            return buff;
        }

        /// <summary>
        /// 初始化权限表数据
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> InitPermission(string moduleCode)
        {
            string moduleCodeLower = moduleCode.ToLower();
            var oldCodeList = await this._permissionRepository.Find().Where(a => SqlFunc.StartsWith(moduleCodeLower, a.Code)).Select(a => a.Code).ToListAsync();
            List<string> newCodeList = _permissionResolver.GetPermissions(moduleCode).Where(a=>a.Mode== PermissionMode.Authorization).Select(a => a.Code).ToList();
            var delAndAdd = oldCodeList.GetDelAndAdd(newCodeList);
            var needDel = delAndAdd.delList;
            await _permissionRepository.Delete(a => needDel.Contains(a.Code));
            await _rolePermissionRepository.Delete(a => needDel.Contains(a.PermissionCode));
            if (delAndAdd.addList.NotNull())
            {
                var needAdd = delAndAdd.addList.Select(a => new PermissionEntity
                {
                    Code = a,
                    Name=String.Empty
                }).ToList();
                await this._permissionRepository.AddList(needAdd);
            }
            return ResultModel.Success("初始化权限成功");
        }

    }
}