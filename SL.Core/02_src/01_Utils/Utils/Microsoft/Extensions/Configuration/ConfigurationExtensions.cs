using Microsoft.Extensions.Configuration;
using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cfg"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IConfiguration cfg, string key) where T : class, new()
        {
            if (cfg == null || key.IsNull())
                return new T();

            //Microsoft.Extensions.Configuration.Binder Get<T> 扩展方法
            var t = cfg.GetSection(key).Get<T>();
            if (t == null)
                return new T();

            return t;
        }
    }
}