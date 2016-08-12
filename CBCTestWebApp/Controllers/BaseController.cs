using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBCTestWebApp.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //if not authorized
            //log 
            string ipAddress = GenericHelper.GetIPAddress();
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            //log exception and ipAddress
        }
    }
}