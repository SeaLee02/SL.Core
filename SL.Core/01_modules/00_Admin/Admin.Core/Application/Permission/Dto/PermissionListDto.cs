
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Permission.Dto
{
    /// <summary>
    /// 详情
    /// </summary>
    public class PermissionListDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int PermissionId { get; set; }
	          
	    /// <summary>
	    /// 权限名称
	    /// </summary>		
	    public string Name { get; set; }
	          
	    /// <summary>
	    /// 后台接口地址
	    /// </summary>		
	    public string Code { get; set; }
	          
	    /// <summary>
	    /// 是否启用
	    /// </summary>		
	    public bool IsEnabled { get; set; }
	          
    }
}
