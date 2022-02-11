using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.MenuGroup
{
    /// <summary>
    /// 菜单组
    /// </summary>
    [SugarTable("Sys_Menu_Group")]
    [TenantAttribute("Admin")]
    public partial class MenuGroupEntity : EntityBaseSoftDelete
    {
        /// <summary>
        /// 菜单组Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid MenuGroupId { get; set; }


        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }

    }
}
