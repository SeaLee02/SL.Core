using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Role;
using SL.Mkh.Admin.Core.Application.Role.Dto;

namespace SL.Mkh.Admin.Core.Application.Role
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
            //cfg.CreateMap<RoleAddDto, RoleEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<RoleAddDto, RoleEntity>();

            //导入
            cfg.CreateMap<RoleImportDto, RoleEntity>();

            //列表
            cfg.CreateMap<RoleEntity,RoleListDto>();
            //详情
            cfg.CreateMap<RoleEntity, RoleDto>();         

            //导出  
            cfg.CreateMap<RoleListDto, RoleExportDto>();

        }
    }
}
