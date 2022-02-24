
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Dict.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class DictAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public int DictId { get; set; }
	          
	    /// <summary>
	    /// 名称
	    /// </summary>		
	    public string Name { get; set; }
	          
	    /// <summary>
	    /// 父级Id
	    /// </summary>		
	    public int ParentId { get; set; }
	          
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
