using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.Permission;
using SL.Mkh.Admin.Core.Domain.Role;
using SL.Mkh.Admin.Core.Domain.RolePermission;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryAbstract<RoleEntity>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork, IUserResolver userResolver) : base(unitOfWork, userResolver)
        {
        }

       
    }
}
