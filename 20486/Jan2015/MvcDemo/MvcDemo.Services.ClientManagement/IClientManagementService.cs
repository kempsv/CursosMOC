using MvcDemo.Core.Common;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Services.ClientManagement
{
    [ServiceContract]
    public interface IClientManagementService : 
        ICrudCommonService<Cliente>
    {
        [OperationContract]
        Cliente FindByEmail(string email);
    }
}
