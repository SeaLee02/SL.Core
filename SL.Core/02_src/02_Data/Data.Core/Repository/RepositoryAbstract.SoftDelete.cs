using SqlSugar;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using SL.Utils.Extensions;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 软删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class RepositoryAbstract<TEntity>
    {
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SoftDelete(dynamic id)
        {
            var entity = await Get(id);
            SetSoftDeleteInfo(entity);
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 软删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> SoftDeleteArry(dynamic id)
        {
            var listEntity = await GetList(id);
            foreach (var item in listEntity)
            {
                SetSoftDeleteInfo(item);
            }
            return await _db.Updateable(listEntity.ToArray()).ExecuteCommandHasChangeAsync();
        }


        /// <summary>
        /// 根据条实体删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> SoftDelete(Expression<Func<TEntity, bool>> whereExpression)
        {
            var listEntity = await _db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
            foreach (var item in listEntity)
            {
                SetSoftDeleteInfo(item);
            }
            return await _db.Updateable(listEntity.ToArray()).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 设置更新信息
        /// </summary>
        private void SetSoftDeleteInfo(TEntity entity)
        {
            //设置实体的添加人编号、添加人姓名、添加时间
            Type pp = typeof(TEntity);
            foreach (PropertyInfo pro in pp.GetProperties())
            {
                var colName = pro.Name;

                if (colName.Equals("Deleted"))
                {
                    pro.SetValue(entity, true);
                    continue;
                }
                if (colName.Equals("DeletedId"))
                {
                    var deletedBy = pro.GetValue(entity);
                    if (deletedBy == null || (Guid)deletedBy == Guid.Empty)
                    {
                        pro.SetValue(entity, _userResolver.UserId);
                    }
                    continue;
                }
                if (colName.Equals("DeletedName"))
                {
                    var deleter = pro.GetValue(entity);
                    if (deleter == null)
                    {
                        pro.SetValue(entity, _userResolver.UserName);
                    }
                    continue;
                }
                if (colName.Equals("DeletedTime"))
                {
                    var deletedTime = pro.GetValue(entity);
                    if (deletedTime == null)
                    {
                        pro.SetValue(entity, DateTime.Now);
                    }
                }
            }
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetTableName()
        {
            string tableName = typeof(TEntity).GetCustomAttribute<SugarTable>()?.TableName ?? "";
            if (tableName.IsNull())
            {
                throw new Exception("请在实体上面表示 映射表名 SugarTable");
            }
            return tableName;
        }
    }
}