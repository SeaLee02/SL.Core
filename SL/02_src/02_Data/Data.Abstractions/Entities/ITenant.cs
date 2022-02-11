using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体租户扩展
    /// </summary>
    public interface ISLTenant<TKey>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        TKey TenantId { get; set; }
    }

    /// <summary>
    /// 实体租户扩展
    /// </summary>
    public interface ISLTenant : ISLTenant<Guid>
    {

    }
}
