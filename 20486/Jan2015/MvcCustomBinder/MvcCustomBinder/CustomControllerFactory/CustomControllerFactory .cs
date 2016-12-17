using MvcCustomBinder.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace MvcCustomBinder.ControllerFactory
{
    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string ControllerName)
        {
            Type targetType = null;
            if (ControllerName == "Home")
            {
                targetType = typeof(HomeController);
            }
            else
            {
                //targetType = typeof(GeneralPurposeController);
            }
            return targetType == null ? null : (IController)Activator.CreateInstance(targetType);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            //throw new NotImplementedException();
        }
    }
}