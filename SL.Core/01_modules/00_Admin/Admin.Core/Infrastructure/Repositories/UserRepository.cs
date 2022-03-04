using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Mkh.Admin.Core.Domain.Role;
using SL.Mkh.Admin.Core.Domain.User;
using SL.Mkh.Admin.Core.Domain.UserRole;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Repositories
{
    public class UserRepository : RepositoryAbstract<UserEntity>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IUserResolver userResolver) : base(unitOfWork, userResolver)
        {
        }


        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Task<UserEntity> Login(string userName, string passWord)
        {
            return Find().Where(a => a.UserName == userName && a.PassWord == passWord)
                .SingleAsync();
        }

        /// <summary>
        /// 根据账号id获取账号角色关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserEntity> GetUserRoleById(Guid id)
        {
            return Find().Mapper<UserEntity,RoleEntity,UserRoleEntity>(a => ManyToMany.Config(a.UserId,a.RoleId)).In(id).SingleAsync();

            //return Find().Mapper(it => it.RoleEntities, it => it.RoleEntities.First().RoleId).In(id).SingleAsync();
        }
    }
}
