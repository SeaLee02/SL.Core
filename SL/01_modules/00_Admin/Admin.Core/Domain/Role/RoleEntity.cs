using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.Role
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("sys_role")]
    [TenantAttribute("Admin")]
    public partial class RoleEntity 
	:EntityBaseSoftDelete,ISLTenant
    {
		/// <summary>
		/// 角色Id
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public Guid RoleId { get; set; }
		      
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public Guid TenantId { get; set; }
		      
		/// <summary>
		/// 角色名称
		/// </summary>	
		public string RoleName { get; set; }
		      
		/// <summary>
		/// 排序
		/// </summary>	
		public int SortNo { get; set; }
		      
		/// <summary>
		/// 是否启用
		/// </summary>	
		public bool IsEnabled { get; set; }
		      
		/// <summary>
		/// 创建人组织Id
		/// </summary>	
		public Guid CreatedOrg { get; set; }
		      
    }

    
}