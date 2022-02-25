using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SL.Utils.Annotations;

namespace SL.Utils.Helpers
{
    public static class DataTableHelper
    {
        /// <summary>
        /// dataTable转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="SF"></param>
        /// <returns></returns>
        public static List<T> Mapper<T>(this DataTable dt, bool SF = false)
        {
            List<T> result = new List<T>();
            List<string> ColumnNameList = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                ColumnNameList.Add(item.ColumnName);
            }
            foreach (DataRow item in dt.Rows)
            {
                T d = Activator.CreateInstance<T>();
                Type pp = typeof(T);
                PropertyInfo[] ppList = pp.GetProperties();
                foreach (PropertyInfo pro in pp.GetProperties())
                {
                    if (ColumnNameList.Contains(pro.Name) && item[pro.Name] != null && item[pro.Name] != DBNull.Value)
                    {
                        Type type = pro.PropertyType;
                        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            type = type.GetGenericArguments()[0];
                        }
                        if (!SF)
                        {
                            object value = Convert.ChangeType(item[pro.Name], type);
                            pro.SetValue(d, value, null);
                        }
                        else
                        {
                            if (item[pro.Name].ToString().Equals("否"))
                            {
                                object value = Convert.ChangeType(false, type);
                                pro.SetValue(d, value, null);
                            }
                            else if (item[pro.Name].ToString().Equals("是"))
                            {
                                object value = Convert.ChangeType(true, type);
                                pro.SetValue(d, value, null);
                            }
                            else if (item[pro.Name].ToString() == string.Empty)
                            {

                            }
                            else
                            {
                                object value = Convert.ChangeType(item[pro.Name], type);
                                pro.SetValue(d, value, null);
                            }

                        }
                    }
                    ////类型转化
                    //pro.SetValue(d, Convert.ChangeType(item[pro.Name], pro.PropertyType), null);//进行数据映射
                }
                result.Add(d);
            }
            return result;
        }



        /// <summary>
        /// 替换table列名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ReplaceColumnName<T>(this DataTable dt)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            T t = Activator.CreateInstance<T>();
            var p = t.GetType().GetProperties();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                pairs.Add(dt.Columns[i].ColumnName, p[i].Name);
            }
            foreach (DataColumn item in dt.Columns)
            {
                if (pairs.Keys.Contains(item.ColumnName))
                {
                    item.ColumnName = pairs[pairs.Keys.FirstOrDefault(x => x == item.ColumnName)];
                }

            }
            return dt;
        }


    }
}
