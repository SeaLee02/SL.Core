using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.RoleButton
{
    /// <summary>
    /// 角色按钮关系
    /// </summary>
    [SugarTable("sys_role_button")]
    [TenantAttribute("Admin")]
    public partial class RoleButtonEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int RoleButtonId { get; set; }
		      
		/// <summary>
		/// 角色Id
		/// </summary>	
		public Guid RoleId { get; set; }
		      
		/// <summary>
		/// 按钮Id
		/// </summary>	
		public Guid ButtonId { get; set; }
		      
    }

    
}