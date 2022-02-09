using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 查询数据(还没有执行SQL)
    /// </summary>
    public abstract partial class RepositoryAbstract<TEntity>
    {
        public ISugarQueryable<TEntity> Find()
        {
            return _db.Queryable<TEntity>();
        }

        public ISugarQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _db.Queryable<TEntity>().Where(expression);
        }


    }
}