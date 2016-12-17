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
    public static class ModelStateExtensions
    {
        public static IList<ErrorReasonInfo> GetAllErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<ErrorReasonInfo>();

            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count > 0)
                {
                    var err = new ErrorReasonInfo { PropertyName = state.Key };
                    state.Value.Errors.ForEach(x => err.Error += x.ErrorMessage);

                    errors.Add(err);
                }
            }

            return errors;
        }

        static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }
    }

    public class OperationResultInfo
    {
        public OperationResultInfo()
        {
            Errors = new List<ErrorReasonInfo>();
        }

        public bool Successfull { get; set; }

        public string Message { get; set; }

        public IList<ErrorReasonInfo> Errors { get; set; }
    }

    public class ErrorReasonInfo
    {
        public string PropertyName { get; set; }

        public string Error { get; set; }
    }

    [CustomAuthorize(RolesArray = new string[] { "Admin", "Cadastro" })]
    public class ClienteAjaxController : BaseController
    {
        private IClientManagementService _service = null;

        public ClienteAjaxController()
        {
            //_service = new GerenciamentoCliente();

            _service = ServiceLocator.GetInstance<IClientManagementService>();
        }

        public ClienteAjaxController(IClientManagementService service_)
        {
            _service = service_;
        }

        [CustomAuthorize(Permission = "Read")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPartial()
        {
            var clientes = _service.FindAll();

            return PartialView("IndexPartial", clientes);
        }

        [CustomAuthorize(Permission = "Read")]
        public ActionResult Details(int id)
        {
            Cliente cliente = _service.FindByKey(id);

            if (null == cliente)
                return RedirectToAction("Index");

            return PartialView(cliente);
        }

        [CustomAuthorize(Permission = "Write")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // Delete from database
                _service.Delete(new Cliente() { Id = id });

                // Return
                return Json(new OperationResultInfo
                {
                    Successfull = true,
                    Message = "Cliente excluido com sucesso"
                });
            }
            catch (InvalidOperationException fX)
            {
                return Json(new { ExceptionApp = fX.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ExceptionApp = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CustomAuthorize(Permission = "Write")]
        public ActionResult Create([Bind(Exclude = "Id")]Cliente novoCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Add(novoCliente);

                    return Json(new OperationResultInfo
                    {
                        Successfull = true,
                        Message = "Cliente adicionado com sucesso"
                    });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                    return Json(new OperationResultInfo
                    {
                        Successfull = false,
                        Message = "Error ao tentar adicionar novo cliente.",
                        Errors = ModelState.GetAllErrors()
                    });
                }
            }
            else
            {
                return Json(new OperationResultInfo
                {
                    Successfull = false,
                    Message = "Formulario invalido",
                    Errors = ModelState.GetAllErrors()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize(Permission = "Write")]
        public ActionResult Edit(int id)
        {
            Cliente cliente = _service.FindByKey(id);
            if (cliente == null)
            {
                return Json("Cliente not Found", JsonRequestBehavior.AllowGet);
            }
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Permission = "Write")]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(cliente);

                    return Json(new OperationResultInfo
                    {
                        Successfull = true,
                        Message = "Cliente Atualizado com sucesso"
                    });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                    return Json(new OperationResultInfo
                    {
                        Successfull = false,
                        Message = "Error ao tentar Atualizar cliente.",
                        Errors = ModelState.GetAllErrors()
                    });
                }
            }
            else
            {
                return Json(new OperationResultInfo
                {
                    Successfull = false,
                    Message = "Formulario invalido",
                    Errors = ModelState.GetAllErrors()
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
