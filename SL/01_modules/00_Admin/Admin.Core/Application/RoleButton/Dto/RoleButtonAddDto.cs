
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.RoleButton.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class RoleButtonAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int RoleButtonId { get; set; }
	          
	    /// <summary>
	    /// 角色Id
	    /// </summary>		
	    public Guid RoleId { get; set; }
	          
	    /// <summary>
	    /// 按钮Id
	    /// </summary>		
	    public Guid ButtonId { get; set; }
	          
    }
}
