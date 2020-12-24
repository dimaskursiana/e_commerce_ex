using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAYROLLPORTAL.Controllers
{
    public class MoreResourceController : Controller
    {
        //private ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        // GET: FAQ
        public ActionResult Faq()
        {
            return View();
        }
        // GET: Blog
        public ActionResult Help()
        {
            //Global_Help_Center content = new Global_Help_Center();
            //content.ListQuestion = db.tbl_Help_Center_Question.ToList();
            //content.ListAnswer = db.tbl_Help_Center_Answer.ToList();
            //return View(content);
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
    }
}