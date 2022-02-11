using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Permission
{
    /// <summary>
    /// 接口权限
    /// </summary>
    [SugarTable("Sys_Permission")]
    [TenantAttribute("Admin")]
    public partial class PermissionEntity : EntityBaseSoftDelete
    {
        /// <summary>
        /// 接口权限Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int PermissionId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 后台接口地址
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

    }
}