using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APP_MODEL.ModelData;
using APP_COMMON;
using APP_NOTIFICATION;
using PAYROLLPORTAL.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using PAYROLLPORTAL.Controllers;
using APP_CORE.GetData;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.IO;
using System.Globalization;

namespace PAYROLLPORTAL.Controllers
{
    public class TermsPrivacyController : Controller
    {
        // GET: Terms of Use
        public ActionResult TermsOfUse()
        {
            return View();
        }
        // GET: Privacy Policy
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
     
    }
}