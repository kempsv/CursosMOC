using MvcDemo.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Web.UI.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult List()
        {
            throw new InvalidOperationException ("Error ao tentar gerar Lista");
        }

        public JsonResult GetData()
        {
            throw new JsonException("Some bad JSON thingy happened");
        }

        public ActionResult HandleError(int statusCode, Exception exception, bool isAjaxRequet)
        {
            Response.StatusCode = statusCode;
            if (!isAjaxRequet)
            {
                ErrorModel model = new ErrorModel { 
                    HttpStatusCode = statusCode, Exception = exception };
                return View(model);
            }
            else
            {
                var errorObjet = new { message = exception.Message };
                return Json(errorObjet, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
