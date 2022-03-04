using SL.Auth.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Defaults
{
    /// <summary>
    /// 默认权限验证处理器
    /// </summary>
    internal class DefaultPermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly IUserPermissionResolver _userPermissionResolver;
        private readonly IUser _user;
        private readonly IPermissionRepository _permissionRepository;

        public DefaultPermissionValidateHandler(IUserPermissionResolver userPermissionResolver, IUser user, IPermissionRepository permissionRepository)
        {
            _userPermissionResolver = userPermissionResolver;
            _user = user;
            this._permissionRepository = permissionRepository;
        }

        public async Task<bool> Validate(IDictionary<string, object> routeValues, HttpMethod httpMethod)
        {
            var permissions = await _userPermissionResolver.Resolve(_user.Id);
            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return true;
            return permissions.Any(m => m.EqualsIgnoreCase($"{area}_{controller}_{action}_{httpMethod}"));
            //
        }

    }
}
