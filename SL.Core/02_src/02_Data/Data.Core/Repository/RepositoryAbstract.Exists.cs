using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 是否存在
    /// </summary>
    public abstract partial class RepositoryAbstract<TEntity>
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists(dynamic id)
        {
            return await _db.Queryable<TEntity>().In(id).AnyAsync();
        }


    }
}
