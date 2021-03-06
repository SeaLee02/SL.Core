
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.UserRole.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class UserRoleAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
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
