using MvcDemo.Core.Common.WcfExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.Common
{
    [ServiceContract]
    public interface ICrudCommonService<TEntity>
    {
        [OperationContract]
        void Add(TEntity entity);
        
        [OperationContract]
        void Update(TEntity entity);
        [OperationContract]
        void Delete(TEntity entity);
        
        [OperationContract]
        [ApplyDataContractResolver]
        IEnumerable<TEntity> FindAll();

        [OperationContract(Name = "FindByGuid")]
        [ApplyDataContractResolver]
        TEntity FindByKey(string guid);
        
        [OperationContract(Name = "FindById")]
        [ApplyDataContractResolver]
        TEntity FindByKey(int id);
    }
}
