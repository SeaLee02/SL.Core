using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Mkh.Admin.Core.Domain.Role;
using SL.Mkh.Admin.Core.Domain.RolePermission;
using SL.Mkh.Admin.Core.Domain.User;
using SL.Utils.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Defaults
{
    public class DefaultUserPermissionResolver : IUserPermissionResolver
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public DefaultUserPermissionResolver(IUserRepository userRepository, IRolePermissionRepository rolePermissionRepository)
        {
            this._userRepository = userRepository;
            this._rolePermissionRepository = rolePermissionRepository;
        }
        /// <summary>
        /// 获取登陆人权限(:todo 需要做缓存处理)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> Resolve(Guid userId)
        {
            List<string> permissionCode = new List<string>();
            if (userId.IsEmpty())
                return new List<string>();

            //根据账号id获取权限集合
            var userEntity = await this._userRepository.GetUserRoleById(userId);
            if (userEntity.RoleEntities.NotNull())
            {
                var roleIds = userEntity.RoleEntities.Select(a => a.RoleId).ToList();
                permissionCode =await _rolePermissionRepository.Find().Where(a => roleIds.Contains(a.RoleId)).Select(a => a.PermissionCode).ToListAsync();
            }
            return permissionCode;

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


        }
    }
}
