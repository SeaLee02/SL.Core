using System;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体软删除扩展
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 已删除的
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人账户编号
        /// </summary>
        Guid? DeletedId { get; set; }

        /// <summary>
        /// 删除人名称
        /// </summary>
        string DeletedName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletedTime { get; set; }
    }
}
