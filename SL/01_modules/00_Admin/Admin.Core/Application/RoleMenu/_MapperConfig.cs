using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.RoleMenu;
using SL.Mkh.Admin.Core.Application.RoleMenu.Dto;

namespace SL.Mkh.Admin.Core.Application.RoleMenu
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
            //cfg.CreateMap<RoleMenuAddDto, RoleMenuEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<RoleMenuAddDto, RoleMenuEntity>();

            //导入
            cfg.CreateMap<RoleMenuImportDto, RoleMenuEntity>();

            //列表
            cfg.CreateMap<RoleMenuEntity,RoleMenuListDto>();
            //详情
            cfg.CreateMap<RoleMenuEntity, RoleMenuDto>();         

            //导出  
            cfg.CreateMap<RoleMenuListDto, RoleMenuExportDto>();

        }
    }
}
