using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.MenuGroup;
using SL.Mkh.Admin.Core.Application.MenuGroup.Dto;

namespace SL.Mkh.Admin.Core.Application.MenuGroup
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
            //cfg.CreateMap<MenuGroupAddDto, MenuGroupEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<MenuGroupAddDto, MenuGroupEntity>();

            //导入
            cfg.CreateMap<MenuGroupImportDto, MenuGroupEntity>();

            //列表
            cfg.CreateMap<MenuGroupEntity,MenuGroupListDto>();
            //详情
            cfg.CreateMap<MenuGroupEntity, MenuGroupDto>();         

            //导出  
            cfg.CreateMap<MenuGroupListDto, MenuGroupExportDto>();

        }
    }
}
