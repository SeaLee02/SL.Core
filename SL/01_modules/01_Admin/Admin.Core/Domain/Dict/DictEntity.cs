using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.Dict
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [SugarTable("Sys_Dict")]
    [TenantAttribute("Admin")]
    public partial class DictEntity : EntityBaseSoftDelete
    {
        /// <summary>
        /// 字典信息
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
        public int DictId { get; set; }

        /// <summary>
        /// 字典名称
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
        public bool IsEnabled { get; set; } = true;

    }
}
