using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Role.Dto
{
     /// <summary>
    /// 详情
    /// </summary>
    public class RoleDto
    {
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid RoleId { get; set; }
	          
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid TenantId { get; set; }
	          
	    /// <summary>
	    /// 角色名称
	    /// </summary>		
	    public string RoleName { get; set; }
	          
	    /// <summary>
	    /// 排序
	    /// </summary>		
	    public int SortNo { get; set; }
	          
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
