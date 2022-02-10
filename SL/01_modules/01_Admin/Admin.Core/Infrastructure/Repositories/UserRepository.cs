using SL.Data.Abstractions;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.User;
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

        public Task<UserEntity> GetByUserName(string userName)
        {
            return Find(a => a.UserName == userName).SingleAsync();
        }
    }
}
