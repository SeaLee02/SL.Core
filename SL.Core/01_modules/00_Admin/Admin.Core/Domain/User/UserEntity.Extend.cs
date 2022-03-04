using SL.Mkh.Admin.Core.Domain.Role;
using SL.Mkh.Admin.Core.Domain.RolePermission;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.User
{
   /// <summary>
    /// 账号表
    /// </summary>
    public partial class UserEntity
    {
        /// <summary>
        /// 角色主表ID集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<RoleEntity> RoleEntities { get; set; }

    }
}
