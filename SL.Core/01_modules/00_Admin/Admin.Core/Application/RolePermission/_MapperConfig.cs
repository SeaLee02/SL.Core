using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.RolePermission;
using SL.Mkh.Admin.Core.Application.RolePermission.Dto;

namespace SL.Mkh.Admin.Core.Application.RolePermission
{
    public class MapperConfig : IMapperConfig
    {
        /// <summary>
        /// 进行映射设置
        /// </summary>
        /// <param name="cfg"></param>
        public void Bind(IMapperConfigurationExpression cfg)
        {
            //走注解映射
            //cfg.CreateMap<RolePermissionAddDto, RolePermissionEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<RolePermissionAddDto, RolePermissionEntity>();

            //导入
            cfg.CreateMap<RolePermissionImportDto, RolePermissionEntity>();

            //列表
            cfg.CreateMap<RolePermissionEntity,RolePermissionListDto>();
            //详情
            cfg.CreateMap<RolePermissionEntity, RolePermissionDto>();         

            //导出  
            cfg.CreateMap<RolePermissionListDto, RolePermissionExportDto>();

        }
    }
}
