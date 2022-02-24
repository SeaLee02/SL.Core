using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.Tenant;
using SL.Mkh.Admin.Core.Application.Tenant.Dto;

namespace SL.Mkh.Admin.Core.Application.Tenant
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
            //cfg.CreateMap<TenantAddDto, TenantEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<TenantAddDto, TenantEntity>();

            //导入
            cfg.CreateMap<TenantImportDto, TenantEntity>();

            //列表
            cfg.CreateMap<TenantEntity,TenantListDto>();
            //详情
            cfg.CreateMap<TenantEntity, TenantDto>();         

            //导出  
            cfg.CreateMap<TenantListDto, TenantExportDto>();

        }
    }
}
