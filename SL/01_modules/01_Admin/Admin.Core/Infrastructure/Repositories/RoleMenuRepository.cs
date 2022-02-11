﻿using SL.Data.Abstractions;
using SL.Data.Abstractions.Login;
using SL.Data.Core.Repository;
using SL.Mkh.Admin.Core.Domain.RoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Repositories
{
    public class RoleMenuRepository : RepositoryAbstract<RoleMenuEntity>, IRoleMenuRepository
    {
        public RoleMenuRepository(IUnitOfWork unitOfWork, IUserResolver userResolver) : base(unitOfWork, userResolver)
        {

        }

    }


}
