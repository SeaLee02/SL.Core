using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.UserRole;
using SL.Mkh.Admin.Core.Application.UserRole.Dto;

namespace SL.Mkh.Admin.Core.Application.UserRole
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
            //cfg.CreateMap<UserRoleAddDto, UserRoleEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<UserRoleAddDto, UserRoleEntity>();

            //导入
            cfg.CreateMap<UserRoleImportDto, UserRoleEntity>();

            //列表
            cfg.CreateMap<UserRoleEntity,UserRoleListDto>();
            //详情
            cfg.CreateMap<UserRoleEntity, UserRoleDto>();         

            //导出  
            cfg.CreateMap<UserRoleListDto, UserRoleExportDto>();

        }
    }
}
