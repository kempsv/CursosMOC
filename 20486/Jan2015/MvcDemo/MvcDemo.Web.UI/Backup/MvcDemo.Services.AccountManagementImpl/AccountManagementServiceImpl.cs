using MvcDemo.Core.AccountManagement;
using MvcDemo.Model.Entities;
using MvcDemo.Services.AccountManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace MvcDemo.Services.AccountManagementImpl
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AccountManagementServiceImpl : IAccountManagementService
    {
        private BCAccountManagement _bcAccount = null;
        private BCRolesManagement _bcRoles = null;

        public AccountManagementServiceImpl() 
        {
            _bcAccount = new BCAccountManagementImpl();
            _bcRoles = new BCRolesManagementImpl();
        }
        #region IAccountManagementService Members
        public UserAccount ValidateUser(string userName, string password)
        {
            return _bcAccount.ValidateUser(new Login()
            {
                UserName = userName,
                Password = password
            });
        }

        public IList<Permission> GetAllPermissions(int userId)
        {
            throw new NotImplementedException();
        }

        public Role GetRoleByName(string roleName) 
        {
            return _bcRoles.GetRoleByName(roleName);
        }

        #endregion

        #region ICrudCommonService<UserAccount> Members

        public void Add(UserAccount entity)
        {
            _bcAccount.Add(entity);
        }

        public void Update(UserAccount entity)
        {
            _bcAccount.Update(entity);
        }

        public void Delete(UserAccount entity)
        {
            _bcAccount.Delete(entity);
        }

        public IEnumerable<UserAccount> FindAll()
        {
            return _bcAccount.FindAll();
        }

        public UserAccount FindByKey(string guid)
        {
            return _bcAccount.FindBy(f => f.Id == 10).FirstOrDefault();
        }

        public UserAccount FindByKey(int id)
        {
            return _bcAccount.FindBy(f => f.Id == id).FirstOrDefault();
        }

        #endregion
    }
}
