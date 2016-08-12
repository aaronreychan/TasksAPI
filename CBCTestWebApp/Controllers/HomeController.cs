using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CBCTestWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
