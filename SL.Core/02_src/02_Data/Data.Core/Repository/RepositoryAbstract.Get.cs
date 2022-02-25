using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 查询单条数据
    /// </summary>
    public abstract partial class RepositoryAbstract<TEntity>
    {
        /// <summary>
        /// 查询指定Id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TEntity> Get(dynamic id)
        {
            return await _db.Queryable<TEntity>().In(id).SingleAsync();
        }

        /// <summary>
        /// 查询指定Id的集合数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TEntity>> GetList(dynamic id)
        {
            return await _db.Queryable<TEntity>().In(id).ToListAsync();
        }
    }
}
