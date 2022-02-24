using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.RolePermission
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [SugarTable("sys_role_permission")]
    [TenantAttribute("Admin")]
    public partial class RolePermissionEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int RolePermissionId { get; set; }
		      
		/// <summary>
		/// 角色Id
		/// </summary>	
		public Guid RoleId { get; set; }
		      
		/// <summary>
		/// 权限Id
		/// </summary>	
		public Guid PermissionId { get; set; }
		      
    }

    
}