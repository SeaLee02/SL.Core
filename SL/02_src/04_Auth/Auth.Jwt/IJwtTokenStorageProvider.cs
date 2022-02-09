﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SL.Auth.Jwt
{
    /// <summary>
    /// JWT令牌存储提供器,需要存表的时候使用
    /// </summary>
    public interface IJwtTokenStorageProvider
    {
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="model"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        Task Save(JwtResultModel model, List<Claim> claims);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="accountId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<bool> Check(string refreshToken, Guid accountId, int platform);
    }
}