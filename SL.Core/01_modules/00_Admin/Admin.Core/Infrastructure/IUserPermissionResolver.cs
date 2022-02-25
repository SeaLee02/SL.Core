using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure
{
    public interface IUserPermissionResolver
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="userId">账户编号</param>
        /// <returns></returns>
        Task<IList<string>> Resolve(Guid userId);
    }
}
