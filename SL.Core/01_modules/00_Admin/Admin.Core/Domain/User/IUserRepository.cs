using SL.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Domain.User
{
    /// <summary>
    /// 用户仓储层
    /// </summary>
    public interface IUserRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<UserEntity> Login(string userName, string passWord);
    }
}
