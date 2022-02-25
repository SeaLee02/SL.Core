using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.Dict
{
    /// <summary>
    /// 字典表
    /// </summary>
    [SugarTable("sys_dict")]
    [TenantAttribute("Admin")]
    public partial class DictEntity 
	:EntityBaseSoftDelete
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int DictId { get; set; }
		      
		/// <summary>
		/// 名称
		/// </summary>	
		public string Name { get; set; }
		      
		/// <summary>
		/// 父级Id
		/// </summary>	
		public int ParentId { get; set; }
		      
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