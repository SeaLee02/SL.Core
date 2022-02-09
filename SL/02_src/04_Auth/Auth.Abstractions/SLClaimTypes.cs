using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Auth.Abstractions
{
    /// <summary>
    /// 账户信息声明名称
    /// </summary>
    public static class SLClaimTypes
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        public const string TENANT_ID = "td";

        /// <summary>
        /// 账户编号
        /// </summary>
        public const string USER_ID = "id";

        /// <summary>
        /// 账户名称
        /// </summary>
        public const string USER_NAME = "an";

        /// <summary>
        /// 平台类型
        /// </summary>
        public const string PLATFORM = "pf";

        /// <summary>
        /// 登录时间
        /// </summary>
        public const string LOGIN_TIME = "lt";

        /// <summary>
        /// 客户IP
        /// </summary>
        public const string IP = "ip";
    }
}