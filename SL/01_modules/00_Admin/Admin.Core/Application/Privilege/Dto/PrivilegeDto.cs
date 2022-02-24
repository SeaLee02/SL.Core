using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Privilege.Dto
{
     /// <summary>
    /// 详情
    /// </summary>
    public class PrivilegeDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int PrivilegeId { get; set; }
	          
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid RoleId { get; set; }
	          
	    /// <summary>
	    /// 权限Id
	    /// </summary>		
	    public Guid PermissionId { get; set; }
	          
	    /// <summary>
	    /// 数据过滤类型(1.私有;2.组织;3.租户)
	    /// </summary>		
	    public int PrivilegeType { get; set; }
	          
	    /// <summary>
	    /// 组织Id
	    /// </summary>		
	    public Guid? OrgId { get; set; }
	          
	    /// <summary>
	    /// 租户Id
	    /// </summary>		
	    public Guid? TenantId { get; set; }
	          
    }
}
