using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Mkh.Admin.Core.Application.Permission.Dto;

namespace SL.Mkh.Admin.Core.Application.Permission
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
            //cfg.CreateMap<PermissionAddDto, PermissionEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<PermissionAddDto, PermissionEntity>();

            //导入
            cfg.CreateMap<PermissionImportDto, PermissionEntity>();

            //列表
            cfg.CreateMap<PermissionEntity,PermissionListDto>();
            //详情
            cfg.CreateMap<PermissionEntity, PermissionDto>();         

            //导出  
            cfg.CreateMap<PermissionListDto, PermissionExportDto>();

        }
    }
}
