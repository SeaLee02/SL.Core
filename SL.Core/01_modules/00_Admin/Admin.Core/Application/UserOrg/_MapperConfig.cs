using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.UserOrg;
using SL.Mkh.Admin.Core.Application.UserOrg.Dto;

namespace SL.Mkh.Admin.Core.Application.UserOrg
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
            //cfg.CreateMap<UserOrgAddDto, UserOrgEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<UserOrgAddDto, UserOrgEntity>();

            //导入
            cfg.CreateMap<UserOrgImportDto, UserOrgEntity>();

            //列表
            cfg.CreateMap<UserOrgEntity,UserOrgListDto>();
            //详情
            cfg.CreateMap<UserOrgEntity, UserOrgDto>();         

            //导出  
            cfg.CreateMap<UserOrgListDto, UserOrgExportDto>();

        }
    }
}
