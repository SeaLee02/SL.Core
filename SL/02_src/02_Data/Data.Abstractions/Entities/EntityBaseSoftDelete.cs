using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 软删除基类实体
    /// </summary>
    public class EntityBaseSoftDelete<TKey> : EntityBase<TKey>, ISoftDelete
    {
        /// <summary>
        /// 已删除的
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人编号
        /// </summary>
        public virtual Guid? DeletedId { get; set; }

        /// <summary>
        /// 删除人名称
        /// </summary>
        public virtual string DeletedName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletedTime { get; set; }
    }

    /// <summary>
    /// 软删除基类实体
    /// </summary>
    public class EntityBaseSoftDelete : EntityBaseSoftDelete<Guid>
    {

    }
}
