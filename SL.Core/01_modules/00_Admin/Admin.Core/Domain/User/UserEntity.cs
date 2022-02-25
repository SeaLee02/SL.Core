using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.ComponentModel;

namespace SL.Mkh.Admin.Core.Domain.User
{
    /// <summary>
    /// 账号表
    /// </summary>
    [SugarTable("sys_user")]
    [TenantAttribute("Admin")]
    public partial class UserEntity 
	:EntityBaseSoftDelete,IOrg,ISLTenant
    {
		/// <summary>
		/// 主键
		/// </summary>	
		[SugarColumn(IsPrimaryKey = true)]
		public Guid UserId { get; set; }
		      
		/// <summary>
		/// 租户Id
		/// </summary>	
		public Guid TenantId { get; set; }
		      
		/// <summary>
		/// 主组织Id
		/// </summary>	
		public Guid OrgId { get; set; }
		      
		/// <summary>
		/// 用户名
		/// </summary>	
		public string UserName { get; set; }
		      
		/// <summary>
		/// 密码
		/// </summary>	
		public string PassWord { get; set; }
		      
		/// <summary>
		/// 明文密码
		/// </summary>	
		public string Clear { get; set; }
		      
		/// <summary>
		/// 密码
		/// </summary>	
		public string Phone { get; set; }
		      
		/// <summary>
		/// 邮件
		/// </summary>	
		public string Email { get; set; }
		      
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