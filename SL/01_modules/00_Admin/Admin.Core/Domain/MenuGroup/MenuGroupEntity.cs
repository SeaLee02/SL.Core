using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.MenuGroup
{
    /// <summary>
    /// 菜单组
    /// </summary>
    [SugarTable("sys_menu_group")]
    [TenantAttribute("Admin")]
    public partial class MenuGroupEntity 
	:EntityBaseSoftDelete
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public Guid MenuGroupId { get; set; }
		      
		/// <summary>
		/// 名称
		/// </summary>	
		public string Name { get; set; }
		      
    }

    
}