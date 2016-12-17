using MvcDemo.Model.Entities;
using MvcDemo.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcDemo.Core.Common;

namespace MvcDemo.Core.AccountManagement
{
    public class BCAccountManagementImpl : BCAccountManagement
    {
        public BCAccountManagementImpl()
        {
            _persistence = new RepositoryDb<UserAccount>(new RepositoryDbContext());
        }

        public override void Add(UserAccount userAccount)
        {
            if (_persistence.Query.Any(c => c.UserName.Equals(userAccount.UserName)))
                throw new InvalidOperationException("Usuário existente.");

            base.Add(userAccount);
        }

        public override UserAccount ValidateUser(Login login)
        {
            UserAccount userAccount = null;

            bool hasExist = _persistence.Query.Any(f => f.UserName.Equals(login.UserName)
                                                    && f.Password.Equals(login.Password));

            if (hasExist)
            {
                userAccount = _persistence.Query.IncludeMultiple(c => c.Roles,
                                                            c => c.Permissions)
                                                .First(f => f.UserName.Equals(login.UserName)
                                                        && f.Password.Equals(login.Password));

            }

            return userAccount;
        }
    }
}
