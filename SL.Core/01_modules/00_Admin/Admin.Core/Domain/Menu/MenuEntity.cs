using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.Menu
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [SugarTable("sys_menu")]
    [TenantAttribute("Admin")]
    public partial class MenuEntity 
	:EntityBaseSoftDelete
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public Guid MenuId { get; set; }
		      
		/// <summary>
		/// 菜单组Id
		/// </summary>	
		public Guid MenuGroupId { get; set; }
		      
		/// <summary>
		/// 父级Id
		/// </summary>	
		public Guid? ParentId { get; set; }
		      
		/// <summary>
		/// 级联Code
		/// </summary>	
		public string CascadeCode { get; set; }
		      
		/// <summary>
		/// 菜单名称
		/// </summary>	
		public string Name { get; set; }
		      
		/// <summary>
		/// 模块地址
		/// </summary>	
		public string Module { get; set; }
		      
		/// <summary>
		/// 路由地址
		/// </summary>	
		public string RouteName { get; set; }
		      
		/// <summary>
		/// 路由地址
		/// </summary>	
		public string Url { get; set; }
		      
		/// <summary>
		/// 菜单类型(1.节点;2.路由)
		/// </summary>	
		public int MenuType { get; set; }
		      
		/// <summary>
		/// 图标
		/// </summary>	
		public string Icon { get; set; }
		      
		/// <summary>
		/// 是否隐藏
		/// </summary>	
		public bool IsHide { get; set; }
		      
		/// <summary>
		/// 排序
		/// </summary>	
		public int SortNo { get; set; }
		      
		/// <summary>
		/// 是否启用
		/// </summary>	
		public bool IsEnabled { get; set; }
		      
    }

    
}