using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Org
{
    /// <summary>
    /// 租户信息
    /// </summary>
    [SugarTable("Sys_Org")]
    [TenantAttribute("Admin")]
    public partial class OrgEntity : EntityBaseSoftDelete, IOrg, ISLTenant
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid OrgId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 创建人组织Id
        /// </summary>
        public Guid CreatedOrg { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 级联Code .1/ .1.1
        /// </summary>
        public string CascadeCode { get; set; }

        /// <summary>
        /// 全路径|A集团/A公司/A部门
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 类型(1.公司;2.部门)
        /// </summary>
        public OrgTypeEnum Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string SortNo { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;


    }

    /// <summary>
    /// 组织类型
    /// </summary>
    public enum OrgTypeEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,

        /// <summary>
        /// 公司
        /// </summary>
        [Description("公司")]
        Company = 1,

        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department = 2

    }

}