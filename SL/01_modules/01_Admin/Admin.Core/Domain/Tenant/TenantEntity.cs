using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Tenant
{
    /// <summary>
    /// 租户信息
    /// </summary>
    [SugarTable("Sys_Tenant")]
    [TenantAttribute("Admin")]
    public partial class TenantEntity : EntityBaseSoftDelete
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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
        public bool IsEnabled { get; set; } = true;

      
    }

}
