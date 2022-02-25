using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SL.Utils.Web
{
    /// <summary>
    /// 操作描述符
    /// </summary>
    public class ActionDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 方法信息
        /// </summary>
        [JsonIgnore]
        public MethodInfo MethodInfo { get; set; }
    }

}