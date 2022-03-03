using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Repositories
{
    public class UserRoleRepository : RepositoryAbstract<UserRoleEntity>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork unitOfWork, IUserResolver userResolver) : base(unitOfWork, userResolver)
        {
        }
    }
}
