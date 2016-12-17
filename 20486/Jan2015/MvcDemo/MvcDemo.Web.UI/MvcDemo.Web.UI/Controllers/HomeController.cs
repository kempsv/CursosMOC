using MvcDemo.Services.ClientManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        private IClientManagementService _service = null;
        public HomeController(IClientManagementService service_) 
        {
            _service = service_;
        }

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Index_Partial()
        {
            var clientes = _service.FindAll();

            return View(clientes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
