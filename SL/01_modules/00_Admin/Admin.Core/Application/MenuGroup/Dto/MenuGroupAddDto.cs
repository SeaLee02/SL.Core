
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.MenuGroup.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class MenuGroupAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid MenuGroupId { get; set; }
	          
	    /// <summary>
	    /// 名称
	    /// </summary>		
	    public string Name { get; set; }
	          
    }
}
