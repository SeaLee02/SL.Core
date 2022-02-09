using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Module.Abstractions.Options
{
    public class ModuleDbOptions
    {
        /// <summary>
        /// 数据库标识
        /// </summary>
        public dynamic ConfigId { get; set; }
        /// <summary>
        /// 数据库类型
        ///   MySql=0
        /// SqlServer,
        ///Sqlite,
        ///Oracle,
        ///PostgreSQL,
        /// </summary>
        public int DbType { get; set; }=1;

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 是否启用过滤
        /// </summary>
        public bool IsFilter { get; set; } = true;

        /// <summary>
        /// 是否打印SQL
        /// </summary>
        public bool IsLogSql { get; set; } = true;
    }
}
