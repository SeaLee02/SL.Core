using SL.Mkh.Admin.Core.Domain.Permission;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Role
{
    /// <summary>
    /// 角色表
    /// </summary>
    public partial class RoleEntity
    {
        /// <summary>
        /// 角色主表ID集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<PermissionEntity> PermissionEntities { get; set; }
    }
}
