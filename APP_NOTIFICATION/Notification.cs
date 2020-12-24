using APP_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP_MODEL;
using System.Data;
using APP_MODEL.ModelData;
using Newtonsoft.Json;

namespace APP_NOTIFICATION
{
    public static class Notification
    {
        public static string strMessage = "";
         
        public async static Task Sender<T>(this IList<T> DataNotifHeader, string Notification_Code,string Email_To, string Body = null, DataSet DataNotifDetail = null)
        { 
            try
            {
                await Task.Run(() => { NotificationExcecute(DataNotifHeader, Notification_Code, Email_To, Body, DataNotifDetail); }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "Notification ERROR");
            } 
        }

        public static void NotificationExcecute<T>(this IList<T> DataNotifHeader, string Notification_Code, string Email_To, String Body = null, DataSet DataNotifDetail = null)
        {
            long status = -1;
            int? Emailstatus = -1;
            long PrivateMsgStatus = -1;
            int CountData = 0;
            strMessage = "";
            List<string> MASSAGE_TO = new List<string>();
            string MESSAGE_SUBJECT = "";
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            try
            {
                List<tbl_Notification_Log> ListAuditLog = new List<tbl_Notification_Log>();
                tbl_Notification_Template dbTEMPLATE = new tbl_Notification_Template();
                if (Body != null)
                {
                    dbTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == Notification_Code).FirstOrDefault();
                    dbTEMPLATE.Body = Body;
                }
                else
                {
                    dbTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == Notification_Code).FirstOrDefault();
                }
                List<NotificationModel> Model = new List<NotificationModel>();
                DataSet datasetDataNotif = Common.ConvertToDataset(DataNotifHeader);
                TemplateMapper.MergeWithGlobalData(ref datasetDataNotif, db.tbl_User_Trial.FirstOrDefault());
                CountData = datasetDataNotif.Tables[0].Rows.Count;
                for (int i = 0; i < CountData; i++)
                {
                    try
                    {
                        if (dbTEMPLATE != null)
                        {
                            Model = TemplateMapper.GetNotificationModelData(i, Notification_Code, dbTEMPLATE, datasetDataNotif, Email_To, DataNotifDetail);
                            foreach (var itemMessage in Model)
                            { 
                                MESSAGE_SUBJECT = dbTEMPLATE.Description;
                                if (dbTEMPLATE.Is_Email == 1 && !string.IsNullOrEmpty(itemMessage.message_to))
                                {
                                    Emailstatus = Email.SendNotification(itemMessage.message_to, MESSAGE_SUBJECT, itemMessage.message_body,true);
                                    ListAuditLog.Add(new tbl_Notification_Log
                                    {
                                        id = Guid.NewGuid(),
                                        Message_To = itemMessage.message_to,
                                        Message_Subject = MESSAGE_SUBJECT,
                                        Status = Emailstatus,
                                        Message_Body = Body != null ? JsonConvert.SerializeObject(DataNotifHeader[i]) : itemMessage.message_body,
                                        Notification_Type = "EMAIL",
                                        Log_Date = DateTime.Now,
                                        Created_By = Email_To,
                                        Created_DateTime = DateTime.Now
                                    });
                                }
                                if (dbTEMPLATE.Is_Private_Message == 1)
                                { 
                                    //PrivateMsgStatus = PrivateMessageer.SendNotification(itemMessage.Email_To, MESSAGE_SUBJECT, itemMessage.message_body);
                                    ListAuditLog.Add(new tbl_Notification_Log
                                    {
                                        id = Guid.NewGuid(),
                                        Message_To = itemMessage.message_to,
                                        Message_Subject =   MESSAGE_SUBJECT,
                                        Status = 1,
                                        Message_Body = Body != null ? JsonConvert.SerializeObject(DataNotifHeader[i]) : itemMessage.message_body,
                                        Notification_Type = "PRIVATEMESSAGE",
                                        Log_Date = DateTime.Now,
                                        Created_By = Email_To,
                                        Created_DateTime = DateTime.Now
                                    });
                                }
                            }
                        }
                        else
                        {
                            Emailstatus = -1;
                            PrivateMsgStatus = -1;
                        }
                        if (Model.Count() == 0)
                        {
                            status = 2;
                            strMessage = "Notification Address is Empty !";
                        }
                        else
                        {
                            if (Emailstatus == 0 && PrivateMsgStatus == 0)
                            {
                                status = 0;
                                strMessage = "Notification Success";
                            }
                            else if (Emailstatus == 0 || PrivateMsgStatus == 0)
                            {
                                status = 1;
                                strMessage = "Notification Email or Private Message Sending Failed";
                            }
                            else
                            {
                                status = -1;
                                strMessage = "Notification Sending Failed";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strMessage = "Notification System Error";
                        UIException.LogException(ex, "Notification Sender" + strMessage);
                    }
                }
                try
                {
                    db.tbl_Notification_Log.AddRange(ListAuditLog);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    UIException.LogException(ex, "AuditLog Notification" + strMessage);
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "Notification ERROR" + strMessage);
            }

        }
    }
}
