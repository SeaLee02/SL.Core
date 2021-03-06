
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RoleMenu.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class RoleMenuAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int RoleMenuId { get; set; }
	          
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid RoleId { get; set; }
	          
	    /// <summary>
	    /// 菜单Id
	    /// </summary>		
	    public Guid MenuId { get; set; }
	          
    }
}
