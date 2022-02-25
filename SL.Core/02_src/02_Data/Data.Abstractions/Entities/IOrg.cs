using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 组织
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IOrg<TKey>
    {
        /// <summary>
        /// 组织Id
        /// </summary>
        TKey OrgId { get; set; }

        /// <summary>
        /// 创建人组织Id(过滤使用)
        /// </summary>
        TKey CreatedOrg { get; set; }


    }


    public interface IOrg : IOrg<Guid>
    {

    }
}