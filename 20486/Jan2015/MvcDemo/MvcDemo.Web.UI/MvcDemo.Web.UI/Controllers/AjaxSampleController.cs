using MvcDemo.Framework.Cache;
using MvcDemo.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcDemo.Web.UI.Controllers
{
    public class AjaxSampleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchRequestModel searchModel)
        {
            return PartialView(new SearchResponseModel
            {
                Name = searchModel.Name,
                CurrentDateTime = DateTime.Now,
                Numbers = searchModel.Numbers.ToList()
            });
        }
        public PartialViewResult GetServerTime()
        {
            ViewBag.ToLongTimeString = DateTime.Now.ToLongTimeString();

            return PartialView();
        }

        public ActionResult GetAllEstados()
        {
            IList<Estado> estados_ = CacheManager.GetOrAdd<IList<Estado>>("Estados", () => FindAllEstados());

            ViewData["Ufs"] = new SelectList(estados_, "Sigla", "Nome");

            return PartialView();
        }

        private IList<Estado> FindAllEstados()
        {
            IList<Estado> estados = new List<Estado>();

            estados.Add(new Estado()
            {
                Nome = "Sao Paulo",
                Sigla = "SP",
                Cidades = new List<Cidade>() 
                    { 
                        new Cidade() { Nome = "Sao Paulo" },
                        new Cidade() { Nome = "Sorocaba" },
                        new Cidade() { Nome = "Campinas" } 
                    }
            });

            estados.Add(new Estado()
            {
                Nome = "Rio de Janeiro",
                Sigla = "RJ",
                Cidades = new List<Cidade>() 
                    { 
                        new Cidade() { Nome = "Saquarema" },
                        new Cidade() { Nome = "Rio de Janeiro" },
                        new Cidade() { Nome = "Niteroi" } 
                    }
            });

            return estados;
        }

        public JsonResult GetCidadeBySigla(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new JsonException("Some bad JSON thingy happened");
            else
            {
                IList<Estado> estados_ = CacheManager.GetOrAdd<IList<Estado>>("Estados", () => FindAllEstados());

                IList<Cidade> cidades = CacheManager.GetOrAdd<IList<Cidade>>(id, () => estados_.Where(f => f.Sigla.Equals(id)).FirstOrDefault().Cidades);

                return Json(cidades, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
