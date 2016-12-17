using MvcDemo.Framework.Cache;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcDemo.Framework.Secutiry
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] RolesArray { get; set; }

        public string Permission { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isDenied = false;
            #region Body
            if (!filterContext.HttpContext.Request.IsAuthenticated)
                base.OnAuthorization(filterContext);
            else
            {
                CurrentUser.Permissions = CacheManager.Get<List<Permission>>(CurrentUser.UserId.ToString());
                if (null != RolesArray)
                {
                    if (!CurrentUser.IsInRole(RolesArray))
                    {
                        isDenied = true;
                    }
                }
                else if (!String.IsNullOrEmpty(Roles))
                {
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        isDenied = true;
                    }
                }

                if (null != Permission)
                {
                    string controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                    string action = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                    if (!CurrentUser.HasPermission(Permission, controller, action))
                    {
                        isDenied = true;
                    }
                }

                if (isDenied)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        var viewResult = new JsonResult();
                        viewResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        viewResult.Data = (new
                        {
                            Successfull = "Unauthorized",
                            Message = "Sorry, you do not have the required permission to perform this action."
                        });
                        filterContext.Result = viewResult;
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                                RouteValueDictionary(new
                                {
                                    controller = "Error",
                                    action = "AccessDenied"
                                }));
                    }
                }
            }
            #endregion
        }
    }
}
