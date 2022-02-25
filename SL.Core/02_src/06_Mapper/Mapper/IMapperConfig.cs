using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mapper
{
    public interface IMapperConfig
    {
        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="cfg"></param>
        void Bind(IMapperConfigurationExpression cfg);
    }
}
