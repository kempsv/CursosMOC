using MvcDemo.Framework.Secutiry;
using MvcDemo.Framework.Wcf.ServiceManager;
using MvcDemo.Model.Entities;
using MvcDemo.Services.ClientManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Web.UI.Controllers
{
    [CustomAuthorize(RolesArray = new string[] { "Admin", "Cadastro" })]
    public class ClienteController : BaseController
    {
        private IClientManagementService _service = null;

        #region Constructors
        // Construtor Que recebe Interface do Service WCF
        // Sera utilizado para Testes Unitarios e para Mock<T>
        public ClienteController(IClientManagementService service_)
        {
            _service = service_;
        }

        // Construtor Default
        public ClienteController() 
        {
            _service = ServiceLocator.GetInstance<IClientManagementService>();
        }
        #endregion
        //
        // GET: /Cliente/
        [CustomAuthorize(Permission = "Read")]
        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult IndexPartial() 
        {
            var listaClientes = _service.FindAll();

            return PartialView(listaClientes);
        }

        [CustomAuthorize(Permission = "Write")]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Cliente entity) 
        {
            _service.Add(entity);

            return RedirectToAction("Index");
        }

        [CustomAuthorize(Permission = "Read")]
        public ActionResult Details(int id)
        {
            var cliente = _service.FindByKey(id);

            return View(cliente);
        }

        [CustomAuthorize(Permission = "Write")]
        public ActionResult Edit(int id)
        {
            var cliente = _service.FindByKey(id);

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente entity)
        {
            _service.Update(entity);

            return RedirectToAction("Index");
        }

        [CustomAuthorize(Permission = "Write")]
        public ActionResult Delete(int id)
        {
            var cliente = _service.FindByKey(id);

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.Delete(new Cliente() { Id = id });

            return RedirectToAction("Index");
        }
    }
}
