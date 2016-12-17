using MvcCustomBinder.CustomBinders;
using MvcCustomBinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCustomBinder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        [HttpPost]
        //public ActionResult Index(HomePageModels home)
        public ActionResult Index([ModelBinder(typeof(HomeCustomBinder))] 
                                   HomePageModels home)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = home.Title;
                ViewBag.Message = home.Date;
            }
            return View();
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
