using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RolePermission.Dto
{
     /// <summary>
    /// 详情
    /// </summary>
    public class RolePermissionDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int RolePermissionId { get; set; }
	          
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid RoleId { get; set; }
	          
	    /// <summary>
	    /// 权限Id
	    /// </summary>		
	    public Guid PermissionId { get; set; }
	          
    }
}
