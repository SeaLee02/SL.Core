using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.UserOrg.Dto
{
     /// <summary>
    /// 详情
    /// </summary>
    public class UserOrgDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
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
