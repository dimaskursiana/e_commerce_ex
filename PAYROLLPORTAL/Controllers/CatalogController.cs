using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAYROLLPORTAL.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Shop()
        {
            return View();
        }
    }
}