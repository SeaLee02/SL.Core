using Lh.Mkh.Admin.Core.Application.Authorize.Dto;
using SL.Auth.Abstractions;
using SL.Auth.Jwt;
using SL.Mkh.Admin.Core.Domain.User;
using SL.Utils.Auth;
using SL.Utils.Extensions;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lh.Mkh.Admin.Core.Application.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICredentialBuilder _credentialBuilder;
        private readonly JwtOptions _options;

        public AuthorizeService(IUserRepository userRepository, ICredentialBuilder credentialBuilder, JwtOptions options)
        {
            this._userRepository = userRepository;
            this._credentialBuilder = credentialBuilder;
            this._options = options;
        }

        public async Task<IResultModel> Login(LoginDto dto)
        {
            var accountEntity = await _userRepository.GetByUserName(dto.UserName);
            if (accountEntity == null)
                return ResultModel.Failed("账号或者密码不对");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  accountEntity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,accountEntity.UserId.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(_options.Expires).ToString()) ,
                new (SLClaimTypes.TENANT_ID, accountEntity.TenantId != null ? accountEntity.TenantId.ToString() : ""),
                new (SLClaimTypes.USER_ID, accountEntity.UserId.ToString()),
                new (SLClaimTypes.USER_NAME, accountEntity.UserName),
                new (SLClaimTypes.ORG_ID, accountEntity.OrgId.ToString())
                    //角色
                    //公司/部门
            };
            var tokenJwt = await _credentialBuilder.Build(claims);
            return tokenJwt;
        }


        public async Task<IResultModel> RefreshToken(string token) 
        {
            if (token.IsNull())
                return ResultModel.Failed("token无效，请重新登录！");

            var jwtHandler = new JwtSecurityTokenHandler();
            var userId = JwtHelper.SerializeJwt(token);
            var accountEntity = await _userRepository.Get(userId);
            if (accountEntity == null)
                return ResultModel.Failed("账号或者密码不对");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  accountEntity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,accountEntity.UserId.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(_options.Expires).ToString()) ,
                new (SLClaimTypes.TENANT_ID, accountEntity.TenantId != null ? accountEntity.TenantId.ToString() : ""),
                new (SLClaimTypes.USER_ID, accountEntity.UserId.ToString()),
                new (SLClaimTypes.USER_NAME, accountEntity.UserName),
                new (SLClaimTypes.ORG_ID, accountEntity.OrgId.ToString())
                    //角色
                    //公司/部门
            };

            var tokenJwt = await _credentialBuilder.Build(claims);
            return tokenJwt;
        }


    }
}
