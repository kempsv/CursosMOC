using MvcDemo.Core.Common;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.ClientManagement
{
    public abstract class BCClientManagement : BCEntityPersistence<Cliente>
    {
        public abstract Cliente FindByEmail(string email);
    }
}
