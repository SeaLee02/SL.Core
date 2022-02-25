using SL.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SL.Data.Abstractions
{

    public interface IRepository 
    {
        /// <summary>
        /// SqlsugarClient实体
        /// </summary>
        ISqlSugarClient Db { get; }

    }




    /// <summary>
    /// 仓储层
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> :IRepository where TEntity : class, new() //TEntity 不约束为：IEntity,契合SqlSuagr框架
    {
      

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null);

        /// <summary>
        /// 批量插入实体(1万条以下)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        Task<int> AddList(List<TEntity> listEntity);

        /// <summary>
        /// 批量插入大数据实体(1万条以上)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        Task<int> BulkCopyAsync(List<TEntity> listEntity);

        #endregion 新增

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// 更新指定的列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lstColumns"></param>
        /// <param name="lstIgnoreColumns"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> Update(
        TEntity entity,
        List<string> lstColumns = null,
        List<string> lstIgnoreColumns = null,
        string strWhere = ""
          );

        /// <summary>
        /// 批量更新数据实体(1万条以下)
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateList(List<TEntity> listEntity);

        /// <summary>
        /// 批量更新大数据实体(1万条以上)
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        Task<bool> BulkUpdateAsync(List<TEntity> listEntity);

        #endregion 更新

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(dynamic id);

        /// <summary>
        /// 删除指定ID集合的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<bool> DeleteArry(dynamic id);

        /// <summary>
        /// 根据条实体删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> SoftDelete(dynamic id);

        /// <summary>
        /// 软删除指定ID集合的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteArry(dynamic id);

        /// <summary>
        /// 根据条实体删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> SoftDelete(Expression<Func<TEntity, bool>> whereExpression);
        #endregion

        #region 查询
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Get(dynamic id);

        /// <summary>
        /// 根据主键集合查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetList(dynamic id);

        /// <summary>
        /// 根据主键判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(dynamic id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<TEntity> Find();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        ISugarQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        #endregion


    }
}