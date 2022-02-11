using Microsoft.AspNetCore.Http;
using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Utils.Auth;
using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Login
{
                                         
    internal class UserResolver : IUserResolver
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserResolver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? TenantId
        {
            get
            {
                var tenantId = _contextAccessor?.HttpContext?.User.FindFirst(SLClaimTypes.TENANT_ID);

                if (tenantId != null && tenantId.Value.NotNull())
                {
                    return new Guid(tenantId.Value);
                }

                return null;
            }
        }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid? UserId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(SLClaimTypes.USER_ID);

                if (accountId != null && accountId.Value.NotNull())
                {
                    return new Guid(accountId.Value);
                }

                return Guid.Empty;
            }
        }

        public string UserName
        {
            get
            {
                var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(SLClaimTypes.USER_NAME);

                if (accountName == null || accountName.Value.IsNull())
                {
                    return "";
                }

                return accountName.Value;
            }
        }
    }

}