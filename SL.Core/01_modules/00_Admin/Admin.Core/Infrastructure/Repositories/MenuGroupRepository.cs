using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.MenuGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Repositories
{
    public class MenuGroupRepository : RepositoryAbstract<MenuGroupEntity>, IMenuGroupRepository
    {
        public MenuGroupRepository(IUnitOfWork unitOfWork, IUserResolver userResolver) : base(unitOfWork, userResolver)
        {
        }
    }
}
