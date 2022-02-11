using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Privilege
{
    /// <summary>
    /// 数据权限
    /// </summary>
    [SugarTable("Sys_Privilege")]
    [TenantAttribute("Admin")]
    public partial class PrivilegeEntity 
    {
        /// <summary>
        /// 数据权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PrivilegeId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 接口Id
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// 数据过滤类型(1.私有;2.组织;3.租户)
        /// </summary>
        public PrivilegeTypeEnum PrivilegeType { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public Guid? OrgId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

    }

    /// <summary>
    /// 数据过滤类型(1.私有;2.组织;3.租户)
    /// </summary>
    public enum PrivilegeTypeEnum 
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,

        /// <summary>
        /// 私有
        /// </summary>
        [Description("私有")]
        Private = 1,

        /// <summary>
        /// 组织
        /// </summary>
        [Description("组织")]
        Org = 2,

        /// <summary>
        /// 租户
        /// </summary>
        [Description("租户")]
        Tenant = 3
    }
}