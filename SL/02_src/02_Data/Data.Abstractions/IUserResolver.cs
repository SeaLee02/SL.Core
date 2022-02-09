using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions
{

    /// <summary>
    /// 账户信息解析器
    /// </summary>
    public interface IUserResolver
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        public Guid? TenantId { get; }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid? UserId { get; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string UserName { get; }
    }
}
