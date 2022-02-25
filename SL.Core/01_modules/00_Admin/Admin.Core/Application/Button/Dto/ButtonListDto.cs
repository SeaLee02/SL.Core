
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Application.Button.Dto
{
    /// <summary>
    /// 详情
    /// </summary>
    public class ButtonListDto
    {
	    /// <summary>
	    /// 主键
	    /// </summary>		
	    public Guid ButtonId { get; set; }
	          
	    /// <summary>
	    /// 菜单Id
	    /// </summary>		
	    public Guid MenuId { get; set; }
	          
	    /// <summary>
	    /// 按钮名称
	    /// </summary>		
	    public string Name { get; set; }
	          
	    /// <summary>
	    /// 按钮编码
	    /// </summary>		
	    public string Code { get; set; }
	          
	    /// <summary>
	    /// 脚本
	    /// </summary>		
	    public string Func { get; set; }
	          
	    /// <summary>
	    /// 元素图标
	    /// </summary>		
	    public string Icon { get; set; }
	          
	    /// <summary>
	    /// 元素样式
	    /// </summary>		
	    public string Class { get; set; }
	          
	    /// <summary>
	    /// 是否隐藏
	    /// </summary>		
	    public bool IsHide { get; set; }
	          
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
