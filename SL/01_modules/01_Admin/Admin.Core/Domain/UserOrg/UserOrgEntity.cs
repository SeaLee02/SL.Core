using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.UserOrg
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [SugarTable("Sys_Role_Org")]
    [TenantAttribute("Admin")]
    public partial class UserOrgEntity
    {
        /// <summary>
        /// 数据权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserOrgId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public Guid OrgId { get; set; }

    }
}
