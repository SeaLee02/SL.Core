
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Role.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class RoleAddDto
    {
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid? RoleId { get; set; }
	          
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
	          
    }
}
