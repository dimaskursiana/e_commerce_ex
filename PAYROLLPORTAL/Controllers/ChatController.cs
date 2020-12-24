using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using APP_MODEL.ModelData;
using APP_NOTIFICATION;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace PAYROLLPORTAL.Controllers
{
    public class ChatController : Controller
    {
        public string CsNickNameChat = ConfigurationManager.AppSettings["CsNickNameChat"];
        public string UserNameChat = ConfigurationManager.AppSettings["UserNameChat"];
        public string WelcomeChat = ConfigurationManager.AppSettings["WelcomeChat"];
        public string CsNameChat = ConfigurationManager.AppSettings["CsNameChat"];
        private ModelEntities db = new ModelEntities();
        private string TEMPLATE_REQUEST_DEMO = "1LG004-RES00";

        public PartialViewResult _Index()
        {
            ViewBag.UserNameChat = UserNameChat;
            ViewBag.CsNameChat = CsNameChat;
            ViewBag.CsNickNameChat = CsNickNameChat;
            ViewBag.UserId = Guid.NewGuid();
            return PartialView();
        }

        public JsonResult welcomeChat(string id)
        {
            ModelEntities db = new ModelEntities();
            var Logchat = new List<vw_Chat_Log>();

            Logchat.Add(new vw_Chat_Log()
            {
                Name_From = CsNickNameChat,
                Chat = WelcomeChat
            });
            var log = db.vw_Chat_Log.Where(p => p.Version_ID == id).OrderBy(p => p.Log_dt).ToList();
            if (log.Count() > 0)
            {
                Logchat.AddRange(log);
                List<Guid> updateChat = Logchat.Select(s => s.id).ToList();
                var tbl_Logchat = db.tbl_Chat_Log.Where(p => updateChat.Contains(p.id) && p.Chat_Status == 0).ToList();
                foreach (var item in tbl_Logchat)
                {
                    item.Chat_Status = 1;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            foreach (var item in Logchat)
            {
                if (item.Type == "R")
                    item.Name_From = UserNameChat;
            }
            var jsonData = JsonConvert.SerializeObject(Logchat);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getChat(string id, string tm, int? rd)
        {
            ModelEntities db = new ModelEntities();
            var Logchat = new List<vw_Chat_Log>();
            var dt = Convert.ToDateTime(tm);
            if (rd == 1)
            {
                Logchat = db.vw_Chat_Log.Where(p => p.Version_ID == id).OrderBy(p => p.Log_dt).ToList();
                List<Guid> updateChat = Logchat.Select(s => s.id).ToList();
                var tbl_Logchat = db.tbl_Chat_Log.Where(p => updateChat.Contains(p.id) && p.Chat_Status == 0).ToList();
                foreach (var item in tbl_Logchat)
                {
                    item.Chat_Status = 1;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            else
            {
                Logchat = db.vw_Chat_Log.Where(p => p.Version_ID == id && p.User_From != id && p.Chat_Status == 0).OrderBy(p => p.Log_dt).ToList();
                List<Guid> updateChat = Logchat.Select(s => s.id).ToList();
                var tbl_Logchat = db.tbl_Chat_Log.Where(p => updateChat.Contains(p.id)).ToList();
                foreach (var item in tbl_Logchat)
                {
                    item.Chat_Status = 1;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            var jsonData = JsonConvert.SerializeObject(Logchat);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setChat(string id, string mg, int? ml)
        {
            ModelEntities db = new ModelEntities();
            db.tbl_Chat_Log.Add(new tbl_Chat_Log()
            {
                id = Guid.NewGuid(),
                Chat = mg,
                Type = "R",
                Log_dt = DateTime.Now,
                User_From = id,
                Version_ID = id,
                Chat_Status = 0
            });
            db.SaveChanges();

            if (ml == 1)
            {
                NotificationMail(id);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setName(string id, string nm)
        {
            ModelEntities db = new ModelEntities();
            var log_chat_unamed = db.tbl_Chat_Log.Where(p => p.User_From == id).ToList();
            foreach (var item in log_chat_unamed)
            {
                item.User_From = nm;
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult createAccount(string id, string nm, string em, string cm, string pn)
        {
            ModelEntities db = new ModelEntities();
            bool status = false;
            try
            {
                tbl_Chat_Account NewChatAccount = new tbl_Chat_Account()
                {
                    id = Guid.Parse(id),
                    Account_Status = 1,
                    Created_DateTime = DateTime.Now,
                    Email = em,
                    Name = nm,
                    Company_Name = cm,
                    Phone_No = pn
                };
                db.tbl_Chat_Account.Add(NewChatAccount);
                db.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkAccount(string id)
        {
            ModelEntities db = new ModelEntities();
            string response = "0";
            try
            {
                Guid GuidId = Guid.Parse(id);
                var Account = db.tbl_Chat_Account.Where(p => p.id == GuidId).FirstOrDefault();
                response = Account.Name;
                Account.Account_Status = 1;
                Account.Updated_DateTime = DateTime.Now;
                db.Entry(Account).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                response = "0";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public void NotificationMail(string id)
        {
            try
            {
                #region Email Notification
                List<User_Trial_Data_Email> listUserTrialDataEmail = new List<User_Trial_Data_Email>();
                var userTrialDataEmailModels = new User_Trial_Data_Email();

                #region Selected List Data Chat Account
                Guid GuidId = Guid.Parse(id);
                var dbChatAccount = db.tbl_Chat_Account.Where(p => p.id == GuidId).FirstOrDefault();
                if (dbChatAccount != null)
                {
                    userTrialDataEmailModels.Name = dbChatAccount.Name;
                    userTrialDataEmailModels.Email = dbChatAccount.Email;
                    userTrialDataEmailModels.Company = dbChatAccount.Company_Name;
                    userTrialDataEmailModels.Phone_Number = dbChatAccount.Phone_No;
                    userTrialDataEmailModels.Url_Chat_Web_Cs = Common.getSysParam("URL_CHAT_WEB_CS");
                    listUserTrialDataEmail.Add(userTrialDataEmailModels);
                }
                #endregion

                #region Add Body Email
                var dbBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_REQUEST_DEMO).FirstOrDefault();
                //using streamreader for reading my htmltemplate
                string Body = null;
                using (StreamReader reader = new StreamReader(Server.MapPath(dbBODYTEMPLATE.Body)))
                {
                    Body = reader.ReadToEnd();
                }
                #endregion

                #region Sender Msg
                var senderMsgSupport = Common.getSysParam("SUPPORT_HARIGAJIAN");
                var senderMsgSales = Common.getSysParam("SALES_HARIGAJIAN");
                Notification.Sender(listUserTrialDataEmail, TEMPLATE_REQUEST_DEMO, senderMsgSupport, Body);
                Notification.Sender(listUserTrialDataEmail, TEMPLATE_REQUEST_DEMO, senderMsgSales, Body);
                #endregion
                #endregion
            }
            catch (Exception ex)
            {

            }
        }


    }
}