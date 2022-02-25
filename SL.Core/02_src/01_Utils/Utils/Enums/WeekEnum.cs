using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Utils.Enums
{
    /// <summary>
    /// 星期
    /// </summary>
    public enum WeekEnum
    {
        /// <summary>
        /// 星期一
        /// </summary>
        [Description("星期一")]
        Monday,
        /// <summary>
        /// 星期二
        /// </summary>
        [Description("星期二")]
        Tuesday,
        /// <summary>
        /// 星期三
        /// </summary>
        [Description("星期三")]
        Wednesday,
        /// <summary>
        /// 星期四
        /// </summary>
        [Description("星期四")]
        Thursday,
        /// <summary>
        /// 星期五
        /// </summary>
        [Description("星期五")]
        Friday,
        /// <summary>
        /// 星期六
        /// </summary>
        [Description("星期六")]
        Saturday,
        /// <summary>
        /// 星期日
        /// </summary>
        [Description("星期日")]
        Sunday
    }
}
