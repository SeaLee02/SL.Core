using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.Permission
{
    /// <summary>
    /// 接口权限地址
    /// </summary>
    [SugarTable("sys_permission")]
    [TenantAttribute("Admin")]
    public partial class PermissionEntity 
	:EntityBaseSoftDelete
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
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