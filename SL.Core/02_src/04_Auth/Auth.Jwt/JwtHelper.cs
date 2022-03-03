using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SL.Utils.DependencyInjection;
using SL.Utils.Extensions;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SL.Auth.Jwt
{
    public class JwtHelper: IScopedDependency
    {
        private readonly JwtOptions _options;
        private readonly ILogger<JwtCredentialBuilder> _logger;

        public JwtHelper(JwtOptions options, ILogger<JwtCredentialBuilder> logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public JwtResultModel Build(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(_options.Expires), signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            _logger.LogDebug("生成AccessToken：{token}", token);

            var result = new JwtResultModel
            {
                AccessToken = token,
                ExpiresIn = (_options.Expires < 0 ? 120 : _options.Expires) * 60,
            };

            return result;
        }



        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static Guid SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            Guid userId = Guid.Empty;
            // token校验
            if (jwtStr.NotNull() && jwtHandler.CanReadToken(jwtStr))
            {

                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                userId = jwtToken.Id.ToGuid();



            }
            return userId;
        }

    }
}
