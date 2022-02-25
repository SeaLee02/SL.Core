using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Dict;
using SL.Mkh.Admin.Core.Application.Dict.Dto;

namespace SL.Mkh.Admin.Core.Application.Dict
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
            //cfg.CreateMap<DictAddDto, DictEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<DictAddDto, DictEntity>();

            //导入
            cfg.CreateMap<DictImportDto, DictEntity>();

            //列表
            cfg.CreateMap<DictEntity,DictListDto>();
            //详情
            cfg.CreateMap<DictEntity, DictDto>();         

            //导出  
            cfg.CreateMap<DictListDto, DictExportDto>();

        }
    }
}
