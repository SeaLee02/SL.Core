
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Org.Dto
{
    /// <summary>
    /// 新增
    /// </summary>
    public class OrgAddDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid OrgId { get; set; }
	          
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid TenantId { get; set; }
	          
	    /// <summary>
	    /// 父级Id
	    /// </summary>		
	    public Guid ParentId { get; set; }
	          
	    /// <summary>
	    /// 组织名称
	    /// </summary>		
	    public string Name { get; set; }
	          
	    /// <summary>
	    /// 级联Code
	    /// </summary>		
	    public string CascadeCode { get; set; }
	          
	    /// <summary>
	    /// 全路径|A集团/A公司/A部门
	    /// </summary>		
	    public string FullName { get; set; }
	          
	    /// <summary>
	    /// 类型(1.公司;2.部门)
	    /// </summary>		
	    public int Type { get; set; }
	          
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
