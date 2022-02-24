using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.Privilege
{
    /// <summary>
    /// 数据权限设置
    /// </summary>
    [SugarTable("sys_privilege")]
    [TenantAttribute("Admin")]
    public partial class PrivilegeEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int PrivilegeId { get; set; }
		      
		/// <summary>
		/// 角色Id
		/// </summary>	
		public Guid RoleId { get; set; }
		      
		/// <summary>
		/// 权限Id
		/// </summary>	
		public Guid PermissionId { get; set; }
		      
		/// <summary>
		/// 数据过滤类型(1.私有;2.组织;3.租户)
		/// </summary>	
		public int PrivilegeType { get; set; }
		      
		/// <summary>
		/// 组织Id
		/// </summary>	
		public Guid? OrgId { get; set; }
		      
		/// <summary>
		/// 租户Id
		/// </summary>	
		public Guid? TenantId { get; set; }
		      
    }

    
}