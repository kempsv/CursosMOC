using MvcDemo.Model.Entities;
using MvcDemo.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.ClientManagement
{
    public class BCClientManagementImpl : BCClientManagement
    {
        public BCClientManagementImpl() 
        {
            _persistence = new RepositoryDb<Cliente>(new RepositoryDbContext());
        }

        public override Cliente FindByEmail(string email)
        {
            var cliente = _persistence.FindBy(c => c.Email.Equals(email)).FirstOrDefault();

            return cliente;
        }
    }
}
