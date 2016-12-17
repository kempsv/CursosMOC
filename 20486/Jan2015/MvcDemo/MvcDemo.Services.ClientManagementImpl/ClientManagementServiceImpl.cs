using MvcDemo.Core.ClientManagement;
using MvcDemo.Model.Entities;
using MvcDemo.Services.ClientManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MvcDemo.Services.ClientManagementImpl
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadUncommitted)]
    public class ClientManagementServiceImpl : IClientManagementService
    {
        private BCClientManagement _bcCliente = null;

        #region Construtor
        public ClientManagementServiceImpl()
        {
            _bcCliente = new BCClientManagementImpl();
        }
        public ClientManagementServiceImpl(BCClientManagement bcCliente_)
        {
            _bcCliente = bcCliente_;
        }
        #endregion

        #region IClientManagementService Members

        [OperationBehavior(TransactionScopeRequired=false, TransactionAutoComplete=true)]
        public Cliente FindByEmail(string email)
        {
            try
            {
                return _bcCliente.FindByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region ICrudCommonService<Cliente> Members
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Add(Cliente entity)
        {
            try
            {
                _bcCliente.Add(entity);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Update(Cliente entity)
        {
            try
            {
                _bcCliente.Update(entity);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
        [OperationBehavior(
        TransactionScopeRequired = true,
        TransactionAutoComplete = true)]
        public void Delete(Cliente entity)
        {
            try
            {
                _bcCliente.Delete(entity);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [OperationBehavior(TransactionScopeRequired = false, TransactionAutoComplete = true)]
        public IEnumerable<Cliente> FindAll()
        {
            try
            {
                return _bcCliente.FindAll();
            }
            catch (Exception)
            {   
                throw;
            }
        }

        [OperationBehavior(TransactionScopeRequired = false, TransactionAutoComplete = true)]
        public Cliente FindByKey(string guid)
        {
            try
            {
                return _bcCliente
                        .FindBy(filter => filter.Id == 10)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [OperationBehavior(TransactionScopeRequired = false, TransactionAutoComplete = true)]
        public Cliente FindByKey(int id)
        {
            try
            {
                return _bcCliente
                        .FindBy(filter => filter.Id == id)
                                .FirstOrDefault();
            }
            catch (Exception)
            {   
                throw;
            }
        }

        #endregion
    }
}
