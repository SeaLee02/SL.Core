using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.UserRole
{
    /// <summary>
    /// 用户角色关系表
    /// </summary>
    [SugarTable("sys_user_role")]
    [TenantAttribute("Admin")]
    public partial class UserRoleEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int UserRoleId { get; set; }
		      
		/// <summary>
		/// 账号Id
		/// </summary>	
		public Guid UserId { get; set; }
		      
		/// <summary>
		/// 角色Id
		/// </summary>	
		public Guid RoleId { get; set; }
		      
    }

    
}