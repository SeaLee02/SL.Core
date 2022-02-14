using SL.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Auth.Jwt
{
    public class JwtHelper
    {
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
