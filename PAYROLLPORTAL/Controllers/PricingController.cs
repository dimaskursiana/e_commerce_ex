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
    public class PricingController : Controller
    {
        private ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();
        private string TEMPLATE_CONTACT_SUPPORT = "1NB001-NEW00";

        #region Pricing Free
        // GET: Pricing
        public ActionResult Free()
        {
            Global_Customer_Notice globalPayrollPortalPricing = new Global_Customer_Notice();
            globalPayrollPortalPricing.errMessage = new List<Global_Error_Code>();
            ViewBag.SaveSuccessPopUp = "";
            return View(globalPayrollPortalPricing);
        }

        [HttpPost]
        public ActionResult Free(Global_Customer_Notice globalPayrollPortalPricing)
        {
            globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
            try
            {
                globalPayrollPortalPricing.Pricing = "Free";
                if (ModelValidate.ValidationSupport(globalPayrollPortalPricing, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage, "Pricing"))
                {
                    globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
                    globalPayrollPortalPricing.customerNoticeModels.id = Guid.NewGuid();
                    globalPayrollPortalPricing.customerNoticeModels.Type_Form = "Pricing";
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = string.IsNullOrEmpty(globalPayrollPortalPricing.Pricing) ? "-" : "Request " + globalPayrollPortalPricing.Pricing;
                    globalPayrollPortalPricing.customerNoticeModels.Company_Name = globalPayrollPortalPricing.Company_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Number_Employee = string.IsNullOrEmpty(globalPayrollPortalPricing.Number_Employee) ? 0 : int.Parse(globalPayrollPortalPricing.Number_Employee);
                    globalPayrollPortalPricing.customerNoticeModels.Contact_Name = globalPayrollPortalPricing.Contact_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Title = globalPayrollPortalPricing.Title;
                    globalPayrollPortalPricing.customerNoticeModels.Email = globalPayrollPortalPricing.Email;
                    globalPayrollPortalPricing.customerNoticeModels.Phone = globalPayrollPortalPricing.Phone;
                    globalPayrollPortalPricing.customerNoticeModels.Message = globalPayrollPortalPricing.Message;
                    globalPayrollPortalPricing.customerNoticeModels.Create_Date = DateTime.Now;
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = globalPayrollPortalPricing.Pricing;
                    db.tbl_Customer_Notice.Add(globalPayrollPortalPricing.customerNoticeModels);
                    db.SaveChanges();

                    #region Email Notification
                    globalPayrollPortalPricing.customerNoticeList = new List<tbl_Customer_Notice>();
                    globalPayrollPortalPricing.customerNoticeList.Add(globalPayrollPortalPricing.customerNoticeModels);

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
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_To, Body);
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_BackUp_To, Body);
                    #endregion

                    #endregion
                }
                globalPayrollPortalPricing.errMessage = errMessage;
                ViewBag.SaveSuccessPopUp = "success";
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "PricingController.Request");
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Pricing Advance
        // GET: Pricing
        public ActionResult Advance()
        {
            Global_Customer_Notice globalPayrollPortalPricing = new Global_Customer_Notice();
            globalPayrollPortalPricing.errMessage = new List<Global_Error_Code>();
            ViewBag.SaveSuccessPopUp = "";
            return View(globalPayrollPortalPricing);
        }

        [HttpPost]
        public ActionResult Advance(Global_Customer_Notice globalPayrollPortalPricing)
        {
            globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
            try
            {
                globalPayrollPortalPricing.Pricing = "Advance";
                if (ModelValidate.ValidationSupport(globalPayrollPortalPricing, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage, "Pricing"))
                {
                    globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
                    globalPayrollPortalPricing.customerNoticeModels.id = Guid.NewGuid();
                    globalPayrollPortalPricing.customerNoticeModels.Type_Form = "Pricing";
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = string.IsNullOrEmpty(globalPayrollPortalPricing.Pricing) ? "-" : "Request " + globalPayrollPortalPricing.Pricing;
                    globalPayrollPortalPricing.customerNoticeModels.Company_Name = globalPayrollPortalPricing.Company_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Number_Employee = string.IsNullOrEmpty(globalPayrollPortalPricing.Number_Employee) ? 0 : int.Parse(globalPayrollPortalPricing.Number_Employee);
                    globalPayrollPortalPricing.customerNoticeModels.Contact_Name = globalPayrollPortalPricing.Contact_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Title = globalPayrollPortalPricing.Title;
                    globalPayrollPortalPricing.customerNoticeModels.Email = globalPayrollPortalPricing.Email;
                    globalPayrollPortalPricing.customerNoticeModels.Phone = globalPayrollPortalPricing.Phone;
                    globalPayrollPortalPricing.customerNoticeModels.Message = globalPayrollPortalPricing.Message;
                    globalPayrollPortalPricing.customerNoticeModels.Create_Date = DateTime.Now;
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = globalPayrollPortalPricing.Pricing;
                    db.tbl_Customer_Notice.Add(globalPayrollPortalPricing.customerNoticeModels);
                    db.SaveChanges();

                    #region Email Notification
                    globalPayrollPortalPricing.customerNoticeList = new List<tbl_Customer_Notice>();
                    globalPayrollPortalPricing.customerNoticeList.Add(globalPayrollPortalPricing.customerNoticeModels);

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
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_To, Body);
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_BackUp_To, Body);
                    #endregion

                    #endregion
                }
                globalPayrollPortalPricing.errMessage = errMessage;
                ViewBag.SaveSuccessPopUp = "success";
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "PricingController.Request");
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Pricing Basic
        // GET: Pricing
        public ActionResult Basic()
        {
            Global_Customer_Notice globalPayrollPortalPricing = new Global_Customer_Notice();
            globalPayrollPortalPricing.errMessage = new List<Global_Error_Code>();
            ViewBag.SaveSuccessPopUp = "";
            return View(globalPayrollPortalPricing);
        }

        [HttpPost]
        public ActionResult Basic(Global_Customer_Notice globalPayrollPortalPricing)
        {
            globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
            try
            {
                globalPayrollPortalPricing.Pricing = "Basic";
                if (ModelValidate.ValidationSupport(globalPayrollPortalPricing, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage, "Pricing"))
                {
                    globalPayrollPortalPricing.customerNoticeModels = new tbl_Customer_Notice();
                    globalPayrollPortalPricing.customerNoticeModels.id = Guid.NewGuid();
                    globalPayrollPortalPricing.customerNoticeModels.Type_Form = "Pricing";
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = string.IsNullOrEmpty(globalPayrollPortalPricing.Pricing) ? "-" : "Request " + globalPayrollPortalPricing.Pricing;
                    globalPayrollPortalPricing.customerNoticeModels.Company_Name = globalPayrollPortalPricing.Company_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Number_Employee = string.IsNullOrEmpty(globalPayrollPortalPricing.Number_Employee) ? 0 : int.Parse(globalPayrollPortalPricing.Number_Employee);
                    globalPayrollPortalPricing.customerNoticeModels.Contact_Name = globalPayrollPortalPricing.Contact_Name;
                    globalPayrollPortalPricing.customerNoticeModels.Title = globalPayrollPortalPricing.Title;
                    globalPayrollPortalPricing.customerNoticeModels.Email = globalPayrollPortalPricing.Email;
                    globalPayrollPortalPricing.customerNoticeModels.Phone = globalPayrollPortalPricing.Phone;
                    globalPayrollPortalPricing.customerNoticeModels.Message = globalPayrollPortalPricing.Message;
                    globalPayrollPortalPricing.customerNoticeModels.Create_Date = DateTime.Now;
                    globalPayrollPortalPricing.customerNoticeModels.Pricing = globalPayrollPortalPricing.Pricing;
                    db.tbl_Customer_Notice.Add(globalPayrollPortalPricing.customerNoticeModels);
                    db.SaveChanges();

                    #region Email Notification
                    globalPayrollPortalPricing.customerNoticeList = new List<tbl_Customer_Notice>();
                    globalPayrollPortalPricing.customerNoticeList.Add(globalPayrollPortalPricing.customerNoticeModels);

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
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_To, Body);
                    Notification.Sender(globalPayrollPortalPricing.customerNoticeList, TEMPLATE_CONTACT_SUPPORT, Email_BackUp_To, Body);
                    #endregion

                    #endregion
                }
                globalPayrollPortalPricing.errMessage = errMessage;
                ViewBag.SaveSuccessPopUp = "success";
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "PricingController.Request");
                return Json(globalPayrollPortalPricing, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}