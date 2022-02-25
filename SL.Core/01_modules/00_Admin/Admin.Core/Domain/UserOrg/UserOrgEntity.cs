using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.UserOrg
{
    /// <summary>
    /// 账号组织关系表
    /// </summary>
    [SugarTable("sys_user_org")]
    [TenantAttribute("Admin")]
    public partial class UserOrgEntity 
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public int UserOrgId { get; set; }
		      
		/// <summary>
		/// 账号Id
		/// </summary>	
		public Guid UserId { get; set; }
		      
		/// <summary>
		/// 组织Id
		/// </summary>	
		public Guid OrgId { get; set; }
		      
    }

    
}