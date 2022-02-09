using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    /// <summary>
    /// 修改
    /// </summary>
    public abstract partial class RepositoryAbstract<TEntity>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            SetUpdateInfo(entity);
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 更新指定的列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lstColumns"></param>
        /// <param name="lstIgnoreColumns"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            IUpdateable<TEntity> up = _db.Updateable(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
            }
            var list = SetUpdateInfo(entity);
            if (list.Count > 0)
            {
                if (lstColumns == null)
                {
                    lstColumns = list;
                }
                else
                {
                    lstColumns.AddRange(list);
                }
            }

            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = up.UpdateColumns(lstColumns.ToArray());
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = up.Where(strWhere);
            }

            return await up.ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量更新数据实体(1万条以下)
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateList(List<TEntity> listEntity)
        {
            foreach (var item in listEntity)
            {
                SetUpdateInfo(item);
            }
            return await _db.Updateable(listEntity.ToArray()).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量更新大数据实体(1万条以上)
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public async Task<bool> BulkUpdateAsync(List<TEntity> listEntity)
        {
            foreach (var item in listEntity)
            {
                SetUpdateInfo(item);
            }
            int i = await _db.Fastest<TEntity>().BulkUpdateAsync(listEntity);
            return i == listEntity.Count();
        }



        /// <summary>
        /// 设置更新信息
        /// </summary>
        private List<string> SetUpdateInfo(TEntity entity)
        {
            List<string> list = new List<string>() { };
            //设置实体的添加人编号、添加人姓名、添加时间
            Type pp = typeof(TEntity);
            foreach (PropertyInfo pro in pp.GetProperties())
            {
                var colName = pro.Name;
                if (colName.Equals("ModifiedId"))
                {
                    var modifiedBy = pro.GetValue(entity);
                    if (modifiedBy == null || (Guid)modifiedBy == Guid.Empty)
                    {
                        pro.SetValue(entity, _userResolver.UserId);
                        list.Add("ModifiedId");
                    }
                    continue;
                }
                if (colName.Equals("ModifiedName"))
                {
                    var modifier = pro.GetValue(entity);
                    if (modifier == null)
                    {
                        pro.SetValue(entity, _userResolver.UserName);
                        list.Add("ModifiedName");
                    }
                    continue;
                }           
                if (colName.Equals("ModifiedTime"))
                {
                    var modifiedTime = pro.GetValue(entity);
                    if (modifiedTime == null)
                    {
                        pro.SetValue(entity, DateTime.Now);
                        list.Add("ModifiedTime");
                    }
                }
            }
            return list;
        }
    }
}
