using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Button;
using SL.Mkh.Admin.Core.Application.Button.Dto;

namespace SL.Mkh.Admin.Core.Application.Button
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
            //cfg.CreateMap<ButtonAddDto, ButtonEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<ButtonAddDto, ButtonEntity>();

            //导入
            cfg.CreateMap<ButtonImportDto, ButtonEntity>();

            //列表
            cfg.CreateMap<ButtonEntity,ButtonListDto>();
            //详情
            cfg.CreateMap<ButtonEntity, ButtonDto>();         

            //导出  
            cfg.CreateMap<ButtonListDto, ButtonExportDto>();

        }
    }
}
