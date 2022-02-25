using SL.Utils.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Utils.Models
{
    /// <summary>
    /// 选项集合模型
    /// </summary>
    public class OptionCollectionResultModel<T> : CollectionAbstract<OptionResultModel<T>>
    {
    }

    /// <summary>
    /// 选项集合模型
    /// </summary>
    public class OptionCollectionResultModel : OptionCollectionResultModel<object>
    {

    }
}

