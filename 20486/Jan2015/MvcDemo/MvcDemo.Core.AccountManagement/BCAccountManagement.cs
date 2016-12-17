using MvcDemo.Core.Common;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.AccountManagement
{
    public abstract class BCAccountManagement : BCEntityPersistence<UserAccount>
    {
        public abstract UserAccount ValidateUser(Login login);
    }
}
