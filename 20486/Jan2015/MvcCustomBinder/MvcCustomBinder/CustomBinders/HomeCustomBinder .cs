using MvcCustomBinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCustomBinder.CustomBinders
{
    public class HomeCustomBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            HttpRequestBase request = 
                controllerContext.HttpContext.Request;

            if (string.IsNullOrEmpty(request.Form.Get("Title")))
                throw new CustomBinderException("Titulo nao informado");

            string title = request.Form.Get("Title");
            string day = request.Form.Get("Day");
            string month = request.Form.Get("Month");
            string year = request.Form.Get("Year");

            return new HomePageModels
            {
                Title = title,
                Date = day + "/" + month + "/" + year
            };
        }
    } 
}