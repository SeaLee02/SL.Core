using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.RoleMenu
{

    /// <summary>
    /// 数据权限
    /// </summary>
    [SugarTable("Sys_Role_Menu")]
    [TenantAttribute("Admin")]
    public partial class RoleMenuEntity
    {
        /// <summary>
        /// 数据权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleMenuId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }

    }

}
