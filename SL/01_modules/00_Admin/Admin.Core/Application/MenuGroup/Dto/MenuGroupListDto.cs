
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.MenuGroup.Dto
{
    /// <summary>
    /// 详情
    /// </summary>
    public class MenuGroupListDto
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
