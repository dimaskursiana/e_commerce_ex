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
    public class RequestDemoController : Controller
    {
        private ModelEntities db = new ModelEntities();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();
        private string TEMPLATE_REQUEST_DEMO = "1LG002-RES00";
        private string TEMPLATE_NOTIF_REQUEST_DEMO = "1LG005-RES00";

        #region Send Notif User Trial
        public JsonResult GetListUserTrialTable(Global_Payroll_Portal globalPayrollPortal)
        {
            try
            {
                globalPayrollPortal.UserTrialModels = new tbl_User_Trial();
                if (ModelValidate.ValidationRequestDemo(globalPayrollPortal, GlobalVariable.CONST_LANG_EN, GlobalVariable.CONST_CREATE, out errMessage))
                {
                    globalPayrollPortal.UserTrialModels = db.tbl_User_Trial.Where(q => q.Email == globalPayrollPortal.email).FirstOrDefault();

                    if (globalPayrollPortal.UserTrialModels == null)
                    {
                        globalPayrollPortal.UserTrialModels = new tbl_User_Trial();
                        globalPayrollPortal.UserTrialModels.id = Guid.NewGuid();
                        globalPayrollPortal.UserTrialModels.Name = globalPayrollPortal.fullName;
                        globalPayrollPortal.UserTrialModels.User_Name = globalPayrollPortal.email;
                        globalPayrollPortal.UserTrialModels.Phone_1 = globalPayrollPortal.mobilePhone1;
                        globalPayrollPortal.UserTrialModels.Phone_2 = globalPayrollPortal.mobilePhone2;
                        globalPayrollPortal.UserTrialModels.Office_Phone = globalPayrollPortal.officePhoneNo;
                        globalPayrollPortal.UserTrialModels.Email = globalPayrollPortal.email;
                        globalPayrollPortal.UserTrialModels.Company_Name = globalPayrollPortal.companyName;
                        globalPayrollPortal.UserTrialModels.Company_Address = globalPayrollPortal.companyAddress;
                        globalPayrollPortal.UserTrialModels.Trial_Date = DateTime.Now;
                        globalPayrollPortal.UserTrialModels.Created_DateTime = DateTime.Now;
                        globalPayrollPortal.UserTrialModels.Status_Code = GlobalVariable.CONST_STATUS_ACTIVE;
                        globalPayrollPortal.UserTrialModels.Authorize_Status = GlobalVariable.CONST_AUTHORIZED;
                        globalPayrollPortal.UserTrialModels.Authorized_DateTime = DateTime.Now;
                        db.tbl_User_Trial.Add(globalPayrollPortal.UserTrialModels);
                        db.SaveChanges();
                    }
                    else
                    {
                        globalPayrollPortal.UserTrialModels.Name = globalPayrollPortal.fullName;
                        globalPayrollPortal.UserTrialModels.User_Name = globalPayrollPortal.email;
                        globalPayrollPortal.UserTrialModels.Phone_1 = globalPayrollPortal.mobilePhone1;
                        globalPayrollPortal.UserTrialModels.Phone_2 = globalPayrollPortal.mobilePhone2;
                        globalPayrollPortal.UserTrialModels.Office_Phone = globalPayrollPortal.officePhoneNo;
                        globalPayrollPortal.UserTrialModels.Email = globalPayrollPortal.email;
                        globalPayrollPortal.UserTrialModels.Company_Name = globalPayrollPortal.companyName;
                        globalPayrollPortal.UserTrialModels.Company_Address = globalPayrollPortal.companyAddress;
                        globalPayrollPortal.UserTrialModels.Trial_Date = DateTime.Now;
                        globalPayrollPortal.UserTrialModels.Created_DateTime = DateTime.Now;
                        globalPayrollPortal.UserTrialModels.Authorized_DateTime = DateTime.Now;
                        db.Entry(globalPayrollPortal.UserTrialModels).State = EntityState.Modified;

                        db.SaveChanges();
                    }

                    #region Email Notification
                    List<User_Trial_Data_Email> listUserTrialDataEmail = new List<User_Trial_Data_Email>();
                    var userTrialDataEmailModels = new User_Trial_Data_Email();
                    var EncryptID = UICommonFunction.Encrypt(globalPayrollPortal.UserTrialModels.id.ToString());

                    userTrialDataEmailModels.id = EncryptID;
                    userTrialDataEmailModels.Name = globalPayrollPortal.UserTrialModels.Name;
                    userTrialDataEmailModels.User_Name = globalPayrollPortal.UserTrialModels.Email;
                    userTrialDataEmailModels.Password = Common.getSysParam("DEF_PASSWORD_USER_TRIAL");
                    userTrialDataEmailModels.Email = globalPayrollPortal.UserTrialModels.Email;
                    userTrialDataEmailModels.Company = globalPayrollPortal.UserTrialModels.Company_Name;
                    userTrialDataEmailModels.Phone_Number = globalPayrollPortal.UserTrialModels.Phone_1;
                    userTrialDataEmailModels.Trial_Date = globalPayrollPortal.UserTrialModels.Trial_Date.Value.ToString("dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
                    userTrialDataEmailModels.Url_User_Trial = Common.getSysParam("URL_USER_TRIAL");
                    listUserTrialDataEmail.Add(userTrialDataEmailModels);

                    #region Add Body Email
                    var dbBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_REQUEST_DEMO).FirstOrDefault();
                    //using streamreader for reading my htmltemplate
                    string Body = null;
                    using (StreamReader reader = new StreamReader(Server.MapPath(dbBODYTEMPLATE.Body)))
                    {
                        Body = reader.ReadToEnd();
                    }
                    #endregion

                    #region Add Body Notif Email
                    var dbNOTIFBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_NOTIF_REQUEST_DEMO).FirstOrDefault();
                    //using streamreader for reading my htmltemplate
                    string NotifBody = null;
                    using (StreamReader notifReader = new StreamReader(Server.MapPath(dbNOTIFBODYTEMPLATE.Body)))
                    {
                        NotifBody = notifReader.ReadToEnd();
                    }
                    #endregion

                    #region Sender Msg
                    Notification.Sender(listUserTrialDataEmail, TEMPLATE_REQUEST_DEMO, globalPayrollPortal.UserTrialModels.Email, Body);

                    string sendNotifUserTrial = Common.getSysParam("SALES_HARIGAJIAN");
                    Notification.Sender(listUserTrialDataEmail, TEMPLATE_NOTIF_REQUEST_DEMO, sendNotifUserTrial, NotifBody);
                    //int? emailStatus = 0;
                    //emailStatus = db.tbl_Notification_Log.Where(d => d.Message_To == globalPayrollPortal.UserTrialModels.Email && d.Notification_Type == "EMAIL").OrderByDescending(o => o.Log_Date).Select(s => s.Status).FirstOrDefault();
                    //globalPayrollPortal.emailStatus = emailStatus;
                    #endregion
                    #endregion

                }
                globalPayrollPortal.errMessage = errMessage;
                return Json(globalPayrollPortal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "RequestDemo.Index");
                return Json(globalPayrollPortal.UserTrialList, JsonRequestBehavior.AllowGet);
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