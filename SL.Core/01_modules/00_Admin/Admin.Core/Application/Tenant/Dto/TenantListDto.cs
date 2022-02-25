
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Tenant.Dto
{
    /// <summary>
    /// 详情
    /// </summary>
    public class TenantListDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid TenantId { get; set; }
	          
	    /// <summary>
	    /// 租户名称
	    /// </summary>		
	    public string Name { get; set; }
	          
	    /// <summary>
	    /// 秘钥|唯一值，用于多租户识别登陆使用
	    /// </summary>		
	    public string AppSecret { get; set; }
	          
	    /// <summary>
	    /// 开始时间
	    /// </summary>		
	    public DateTime StartDate { get; set; }
	          
	    /// <summary>
	    /// 结束时间
	    /// </summary>		
	    public DateTime EndDate { get; set; }
	          
	    /// <summary>
	    /// 是否启用
	    /// </summary>		
	    public bool IsEnabled { get; set; }
	          
    }
}
