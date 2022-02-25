using SL.Data.Abstractions.Pagination;
using SL.Data.Abstractions.Query;
using SL.Utils.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class ISugarQueryableExtensions
    {
        /// <summary>
        /// 分页扩展类
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sugarQueryable"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static async Task<IResultModel> ToPaginationAsync<TResult>(this ISugarQueryable<TResult> sugarQueryable, Paging paging)
        {
            RefAsync<int> totalCount = 0;
            var rows = await sugarQueryable.ToPageListAsync(paging.Index, paging.Size, totalCount);
            paging.TotalCount = totalCount;

            return ResultModel.Success(new QueryResultModel<TResult>(rows, paging.TotalCount));
        }



    }
}
