using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.RoleMenu
{
    /// <summary>
    /// 角色菜单关系
    /// </summary>
    [SugarTable("sys_role_menu")]
    [TenantAttribute("Admin")]
    public partial class RoleMenuEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
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