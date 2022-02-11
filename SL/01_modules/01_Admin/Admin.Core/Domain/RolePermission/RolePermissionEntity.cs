using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.RolePermission
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [SugarTable("Sys_Role_Permission")]
    [TenantAttribute("Admin")]
    public partial class RolePermissionEntity
    {
        /// <summary>
        /// 数据权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RolePermissionId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 按钮id
        /// </summary>
        public Guid PermissionId { get; set; }

    }
}
