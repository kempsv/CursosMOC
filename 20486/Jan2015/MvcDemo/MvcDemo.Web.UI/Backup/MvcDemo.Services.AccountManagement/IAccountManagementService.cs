using MvcDemo.Core.Common;
using MvcDemo.Core.Common.WcfExtension;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MvcDemo.Services.AccountManagement
{
    [ServiceContract]
    public interface IAccountManagementService : ICrudCommonService<UserAccount>
    {
        [OperationContract]
        [ApplyDataContractResolver]
        UserAccount ValidateUser(string userName, string password);

        [OperationContract]
        [ApplyDataContractResolver]
        IList<Permission> GetAllPermissions(int userId);

        [OperationContract]
        [ApplyDataContractResolver]
        Role GetRoleByName(string roleName);
    }
}
