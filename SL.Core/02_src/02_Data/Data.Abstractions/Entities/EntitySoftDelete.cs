using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 软删除实体
    /// </summary>
    public class EntitySoftDelete<TKey> : Entity<TKey>, ISoftDelete
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
    /// 
    /// </summary>
    public class EntitySoftDelete : EntitySoftDelete<Guid>
    {

    }
}
