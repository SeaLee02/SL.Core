using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Menu;
using SL.Mkh.Admin.Core.Application.Menu.Dto;

namespace SL.Mkh.Admin.Core.Application.Menu
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
            //cfg.CreateMap<MenuAddDto, MenuEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<MenuAddDto, MenuEntity>();

            //导入
            cfg.CreateMap<MenuImportDto, MenuEntity>();

            //列表
            cfg.CreateMap<MenuEntity,MenuListDto>();
            //详情
            cfg.CreateMap<MenuEntity, MenuDto>();         

            //导出  
            cfg.CreateMap<MenuListDto, MenuExportDto>();

        }
    }
}
