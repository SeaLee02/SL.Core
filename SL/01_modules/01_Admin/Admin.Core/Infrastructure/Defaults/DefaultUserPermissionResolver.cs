using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Defaults
{
    public class DefaultUserPermissionResolver : IUserPermissionResolver
    {
        /// <summary>
        /// 获取登陆人权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> Resolve(Guid userId)
        {
            if (userId.IsEmpty())
                return new List<string>();

            //var key = _cacheKeys.AccountPermissions(accountId);
            //var list = await _cacheHandler.Get<IList<string>>(key);
            //if (list == null)
            //{
            //    var account = await _accountRepository.Get(accountId);
            //    if (account == null)
            //        return new List<string>();

            //    list = await _rolePermissionRepository
            //        .Find(m => m.RoleId == account.RoleId)
            //        .Select(m => m.PermissionCode)
            //        .ToList<string>();

            //    await _cacheHandler.Set(key, list);
            //}

            return new List<string>();
        }
    }
}
