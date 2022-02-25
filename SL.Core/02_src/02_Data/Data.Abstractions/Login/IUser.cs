using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Login
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        Guid? TenantId { get; }

        /// <summary>
        /// 账户编号
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 账户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 获取当前用户IP(包含IPv和IPv6)
        /// </summary>
        string IP { get; }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        string IPv4 { get; }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        string IPv6 { get; }

        /// <summary>
        /// 登录时间戳
        /// </summary>
        long LoginTime { get; }

        /// <summary>
        /// 获取UA
        /// </summary>
        string UserAgent { get; }
    }
}
