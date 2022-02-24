using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Mapper;
using AutoMapper;
using SL.Mkh.Admin.Core.Domain.User;
using SL.Mkh.Admin.Core.Application.User.Dto;

namespace SL.Mkh.Admin.Core.Application.User
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
            //cfg.CreateMap<UserAddDto, UserEntity>()
            //.ForMember(dest => dest.xx, opt => opt.MapFrom(src => src.xx));;

            //新增 
            cfg.CreateMap<UserAddDto, UserEntity>();

            //导入
            cfg.CreateMap<UserImportDto, UserEntity>();

            //列表
            cfg.CreateMap<UserEntity,UserListDto>();
            //详情
            cfg.CreateMap<UserEntity, UserDto>();         

            //导出  
            cfg.CreateMap<UserListDto, UserExportDto>();

        }
    }
}
