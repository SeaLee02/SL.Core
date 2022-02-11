using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Role
{
    /// <summary>
    /// 角色
    /// </summary>
    [SugarTable("Sys_Role")]
    [TenantAttribute("Admin")]
    public partial class RoleEntity : EntityBaseSoftDelete, IOrg, ISLTenant
    {
        /// <summary>
        /// 字典信息
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 创建人组织
        /// </summary>
        public Guid CreatedOrg { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public Guid OrgId { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

    }

}
