using SL.Data.Abstractions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Abstractions.Query
{
    public abstract class QueryDto
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public QueryPagingDto Page { get; set; } = new QueryPagingDto();

        /// <summary>
        /// 转换成Paging分页类
        /// </summary>
        public Paging Paging
        {
            get
            {
                return new Paging(Page.Index, Page.Size);
            }
        }
    }
}
