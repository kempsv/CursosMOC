using MvcDemo.Model.Entities;
using MvcDemo.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.AccountManagement
{
    public class BCRolesManagementImpl : BCRolesManagement
    {
        public BCRolesManagementImpl()
        {
            _persistence = new RepositoryDb<Role>(new RepositoryDbContext());
        }

        public override Role GetRoleByName(string roleName)
        {
            return _persistence.FindBy(f=>f.RoleName.Equals(roleName)).FirstOrDefault();
        }
    }
}
