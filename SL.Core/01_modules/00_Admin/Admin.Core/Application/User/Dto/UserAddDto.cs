
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.User.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class UserAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
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
