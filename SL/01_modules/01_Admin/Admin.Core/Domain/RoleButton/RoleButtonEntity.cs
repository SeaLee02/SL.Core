using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.RoleButton
{
    /// <summary>
    /// 角色按钮
    /// </summary>
    [SugarTable("Sys_Role_Button")]
    [TenantAttribute("Admin")]
    public partial class RoleButtonEntity
    {
        /// <summary>
        /// 数据权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleButtonId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 按钮id
        /// </summary>
        public Guid ButtonId { get; set; }

    }

}
