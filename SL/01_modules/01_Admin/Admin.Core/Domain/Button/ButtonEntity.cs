using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Button
{

    /// <summary>
    /// 按钮信息
    /// </summary>
    [SugarTable("Sys_Button")]
    [TenantAttribute("Admin")]
    public partial class ButtonEntity : EntityBaseSoftDelete
    {
        /// <summary>
        /// 按钮Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid ButtonId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 组织名称
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
        public bool IsHide { get; set; } = false;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;


    }
}