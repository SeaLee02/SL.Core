using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Entities
{
    /// <summary>
    /// 通用实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityBase<TKey> : Entity<TKey>
    {
        /// <summary>
        /// 创建人编号
        /// </summary>
        public virtual Guid CreatedId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public virtual string CreatedName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人编号
        /// </summary>
        public virtual Guid? ModifiedId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public virtual string ModifiedName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifiedTime { get; set; }
    }

    /// <summary>
    /// 通用实体基类，主键类型采用自增长
    /// </summary>
    public class EntityBase : EntityBase<Guid>
    {

    }
}
