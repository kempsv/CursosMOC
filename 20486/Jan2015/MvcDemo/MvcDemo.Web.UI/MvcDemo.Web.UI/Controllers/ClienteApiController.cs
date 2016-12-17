using MvcDemo.Framework.Wcf.ServiceManager;
using MvcDemo.Model.Entities;
using MvcDemo.Services.ClientManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcDemo.Web.UI.Controllers
{
    public class ClienteApiController : ApiController
    {
        private IClientManagementService _service = null;

        public ClienteApiController() 
        {
            _service = ServiceLocator.GetInstance<IClientManagementService>();
        }
        
        // Forma 1 de Implementa o Verbo GET do HTTP
        // http://localhost:36170/api/ClienteApi
        public IEnumerable<Cliente> Get() 
        {
            return _service.FindAll();
        }

        // http://localhost:36170/api/ClienteApi/10
        public Cliente Get(int id) 
        {
            return _service.FindByKey(id);
        }

        //[HttpPost]
        // http://localhost:36170/api/ClienteApi
        // Content-Type: application/json;
        // { "Nome" : "Test"  , "Endereco" : "Test"  , "Email" : "Test@teste.com.br" , "Idade" : 10  }
        public void Post([FromBody]Cliente newCliente) 
        {
            _service.Add(newCliente);
        }

        //[HttpPut]
        // http://localhost:36170/api/ClienteApi/10
        // Content-Type: application/json;
        // { "Nome" : "Test"  , "Endereco" : "Test"  , "Email" : "Test@teste.com.br" , "Idade" : 10  }
        public void Put(int id, [FromBody]Cliente cliente) 
        {
            _service.Update(cliente);
        }

        //[HttpDelete]
        // http://localhost:36170/api/ClienteApi/10
        public void Delete(int id) 
        {
            _service.Delete(new Cliente() { Id = id });
        }
    }
}
