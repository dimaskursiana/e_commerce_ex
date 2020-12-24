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
    public class DemoController : Controller
    {
        private ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();
        private string TEMPLATE_CONTACT_SUPPORT = "1NB001-NEW00";

        #region GetListContactSupportTable
        public JsonResult GetListDemoTable(Global_Customer_Notice globalPayrollPortal)
        {
            try
            {
                if (ModelValidate.ValidationSupport(globalPayrollPortal, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage, "RequestDemo"))
                {
                    globalPayrollPortal.customerNoticeModels = new tbl_Customer_Notice();
                    globalPayrollPortal.customerNoticeModels.id = Guid.NewGuid();
                    globalPayrollPortal.customerNoticeModels.Type_Form = "Request Demo";
                    globalPayrollPortal.customerNoticeModels.Company_Name = globalPayrollPortal.Company_Name;
                    globalPayrollPortal.customerNoticeModels.Number_Employee = string.IsNullOrEmpty(globalPayrollPortal.Number_Employee) ? 0 : int.Parse(globalPayrollPortal.Number_Employee);
                    globalPayrollPortal.customerNoticeModels.Company_Website = globalPayrollPortal.Company_Website;
                    globalPayrollPortal.customerNoticeModels.Contact_Name = globalPayrollPortal.Contact_Name;
                    globalPayrollPortal.customerNoticeModels.Title = globalPayrollPortal.Title;
                    globalPayrollPortal.customerNoticeModels.Email = globalPayrollPortal.Email;
                    globalPayrollPortal.customerNoticeModels.Phone = globalPayrollPortal.Phone;
                    globalPayrollPortal.customerNoticeModels.Meeting_Schedule = globalPayrollPortal.Meeting_Schedule;
                    globalPayrollPortal.customerNoticeModels.Meeting_Location = globalPayrollPortal.Meeting_Location;
                    globalPayrollPortal.customerNoticeModels.Remark = string.IsNullOrEmpty(globalPayrollPortal.Remark) ? "-" : globalPayrollPortal.Remark;
                    globalPayrollPortal.customerNoticeModels.Create_Date = DateTime.Now;
                    //dateFormat
                    var dateFormat = globalPayrollPortal.Meeting_Schedule.ToString("dddd, dd MMMM yyy HH:mm:ss");
                    globalPayrollPortal.customerNoticeModels.Format_Meeting_Schedule = dateFormat;
                    db.tbl_Customer_Notice.Add(globalPayrollPortal.customerNoticeModels);
                    db.SaveChanges();

                    #region Email Notification
                    globalPayrollPortal.customerNoticeList = new List<tbl_Customer_Notice>();
                    globalPayrollPortal.customerNoticeList.Add(globalPayrollPortal.customerNoticeModels);

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

                }
                globalPayrollPortal.errMessage = errMessage;
                return Json(globalPayrollPortal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "Demo.Index");
                return Json(globalPayrollPortal.customerNoticeList, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

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