using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 新增
    /// </summary>
    public abstract partial class RepositoryAbstract<TEntity>
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> Add(TEntity entity)
        {
            SetCreateInfo(entity);
            var insert = _db.Insertable(entity);
            return await insert.ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null)
        {
            var insert = _db.Insertable(entity);
            if (insertColumns == null)
            {
                return await insert.ExecuteReturnIdentityAsync();
            }
            else
            {
                return await insert.InsertColumns(insertColumns).ExecuteReturnIdentityAsync();
            }
        }

        /// <summary>
        /// 批量插入实体(1万条以下)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> AddList(List<TEntity> listEntity)
        {
            foreach (var item in listEntity)
            {
                SetCreateInfo(item);
            }
            return await _db.Insertable(listEntity.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量插入大数据实体(1万条以上)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> BulkCopyAsync(List<TEntity> listEntity)
        {
            foreach (var item in listEntity)
            {
                SetCreateInfo(item);
            }
            return await _db.Fastest<TEntity>().BulkCopyAsync(listEntity);
        }



        /// <summary>
        /// 设置创建信息
        /// </summary>
        private void SetCreateInfo(TEntity entity)
        {
            //设置实体的添加人编号、添加人姓名、添加时间
            Type pp = typeof(TEntity);
            foreach (PropertyInfo pro in pp.GetProperties())
            {
                var colName = pro.Name;
                if (colName.Equals("CreatedId"))
                {
                    var createdBy = pro.GetValue(entity);
                    if (createdBy == null || (Guid)createdBy == Guid.Empty)
                    {
                        pro.SetValue(entity, _userResolver.UserId);
                    }
                    continue;
                }
                if (colName.Equals("CreatedName"))
                {
                    var creator = pro.GetValue(entity);
                    if (creator == null)
                    {
                        pro.SetValue(entity, _userResolver.UserName);
                    }
                    continue;
                }
                if (colName.Equals("CreatedTime"))
                {
                    var createdTime = pro.GetValue(entity);
                    if (createdTime == null)
                    {
                        pro.SetValue(entity, DateTime.Now);
                    }
                    continue;
                }
                if (colName.Equals("TenantId"))
                {
                    var tenantId = pro.GetValue(entity);
                    if (tenantId == null || (Guid)tenantId == Guid.Empty)
                    {
                        pro.SetValue(entity, _userResolver.TenantId);
                    }
                    continue;
                }               
                if (pro.Name.Equals("CreatedOrg"))
                {
                    var manageOrg = (Guid?)pro.GetValue(entity);
                    if (manageOrg == null || manageOrg == Guid.Empty)
                    {
                        pro.SetValue(entity, this._userResolver.OrgId);
                    }
                }
            }
        }



    }
}
