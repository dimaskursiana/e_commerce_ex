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
    public class PartnersController : Controller
    {
        private ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();
        private string TEMPLATE_CONTACT_SUPPORT = "1NB001-NEW00";

        #region Become a Partner
        // GET: Partners/BecomeAPartner/5
        public ActionResult BecomeAPartner()
        {
            Global_Customer_Notice globalPayrollPortal = new Global_Customer_Notice();
            globalPayrollPortal.errMessage = new List<Global_Error_Code>();
            ViewBag.Region = UISelectlist.ListPhoneRegion();
            ViewBag.SaveSuccess = "";
            return View(globalPayrollPortal);
        }

        [HttpPost]
        public ActionResult BecomeAPartner(Global_Customer_Notice globalPayrollPortal)
        {
            tbl_Customer_Notice model = new tbl_Customer_Notice();
            model.Message = null;
            try
            {
                ViewBag.Region = UISelectlist.ListPhoneRegion();
                if (ModelValidate.ValidationSupport(globalPayrollPortal, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage, "BecomeAPartner"))
                {
                    model.id = Guid.NewGuid();
                    model.Type_Form = "Request Become a Partner";
                    model.Company_Name = globalPayrollPortal.Company_Name;
                    model.Company_Website = string.IsNullOrEmpty(globalPayrollPortal.Company_Website) ? "-" : globalPayrollPortal.Company_Website;
                    model.Contact_Name = globalPayrollPortal.Contact_Name;
                    model.Title = globalPayrollPortal.Title;
                    model.Email = globalPayrollPortal.Email;
                    model.Phone = globalPayrollPortal.Remark + globalPayrollPortal.Phone;
                    globalPayrollPortal.Remark = "";
                    model.Many_Customers = string.IsNullOrEmpty(globalPayrollPortal.Many_Customers) ? "-" : globalPayrollPortal.Many_Customers;
                    model.Partnership_Interest = string.IsNullOrEmpty(globalPayrollPortal.Partnership_Interest) ? "-" : globalPayrollPortal.Partnership_Interest;
                    model.Curently_Partner = string.IsNullOrEmpty(globalPayrollPortal.Curently_Partner) ? "-" : globalPayrollPortal.Curently_Partner;
                    model.Primary_Competitors = string.IsNullOrEmpty(globalPayrollPortal.Primary_Competitors) ? "-" : globalPayrollPortal.Primary_Competitors;
                    model.Create_Date = DateTime.Now;
                    db.tbl_Customer_Notice.Add(model);
                    db.SaveChanges();
                    #region Email Notification
                    globalPayrollPortal.customerNoticeList = new List<tbl_Customer_Notice>();
                    globalPayrollPortal.customerNoticeList.Add(model);

                    #region Add Body Email, Email To and Email From
                    var dbBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_CONTACT_SUPPORT).FirstOrDefault();
                    //using streamreader for reading my htmltemplate
                    string Body = null;
                    using (StreamReader reader = new StreamReader(Server.MapPath(dbBODYTEMPLATE.Body)))
                    {
                        Body = reader.ReadToEnd();
                    }

                    string Email_To = UICommonFunction.ModelSysParam("EMAIL_SALES_SUPPORT").Value;
                    string Email_BackUp_To = UICommonFunction.ModelSysParam("EMAIL_SALES_BACKUP_SUPPORT").Value;
                    #endregion

                    #region Sender Msg
                    Notification.Sender(globalPayrollPortal.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_To, Body);
                    Notification.Sender(globalPayrollPortal.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_BackUp_To, Body);
                    #endregion

                    #endregion
                    model.Message = "SUCESS";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                model.Message = "FAILED";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "OrganizationContactController.Index");
                model.Message = "FAILED";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        // GET: Partners/WhyWithUs/5
        public ActionResult WhyWithUs()
        {
            return View();
        }

        // GET: Partners/OurPartners/5
        public ActionResult OurPartners()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
