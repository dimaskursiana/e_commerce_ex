using AegisImplicitMail;
using APP_COMMON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace APP_NOTIFICATION
{
    public class Email
    {
        public static string strMessage = string.Empty;
        public static int? SendNotification(string EmailTo, string Subject, string MessageContent, bool sending)
        {
            int? status = 1;
            try
            {
                if (sending)
                {
                    var dbMAILCONFIG = Common.getSysParam("MAIL_CONFIG");
                    string[] config = dbMAILCONFIG.Split(new char[] { '|' });
                    int port = Convert.ToInt16(config[1]);
                    string Security = config[5];
                    if (!string.IsNullOrEmpty(EmailTo))
                    {
                        var mymessage = new MimeMailMessage();
                        mymessage.From = new MimeMailAddress(config[2]);
                        mymessage.To.Add(EmailTo);
                        mymessage.Subject = Subject;
                        mymessage.Body = MessageContent;
                        var mailer = new MimeMailer(config[0], port);
                        mailer.User = config[3];
                        mailer.Password = UICommonFunction.Decrypt(config[4]); ;
                        if (Security == "TLS")
                            mailer.SslType = SslMode.Tls;
                        else if (Security == "SSL")
                            mailer.SslType = SslMode.Ssl;
                        else if (Security == "NONE")
                            mailer.SslType = SslMode.Ssl;
                        else
                            mailer.SslType = SslMode.Auto;

                        if (mailer.SslType == SslMode.None)
                            mailer.AuthenticationMode = AuthenticationType.PlainText;
                        else
                            mailer.AuthenticationMode = AuthenticationType.Base64;
                        mailer.SendCompleted += compEvent;
                        mailer.SendMail(mymessage);
                        status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                status = -1;
                UIException.LogException(ex, "public static long SendNotification(string EmailTo,string Subject,string MessageContent)");
            }
            return status;
        }
        public static int? SendNotificationGlobal(string EmailTo, string Subject, string MessageContent, string ParamConfig)
        {
            int? status = 1;

            try
            {
                var dbMAILCONFIG = Common.getSysParam(ParamConfig);
                string[] config = dbMAILCONFIG.Split(new char[] { '|' });
                int port = Convert.ToInt16(config[1]);
                string Security = config[5];
                if (!string.IsNullOrEmpty(EmailTo))
                {
                    var mymessage = new MimeMailMessage();
                    mymessage.From = new MimeMailAddress(config[2]);
                    mymessage.To.Add(EmailTo);
                    mymessage.Subject = Subject;
                    mymessage.Body = MessageContent;
                    var mailer = new MimeMailer(config[0], port);
                    mailer.User = config[3];
                    mailer.Password = UICommonFunction.Decrypt(config[4]); ;
                    if (Security == "TLS")
                        mailer.SslType = SslMode.Tls;
                    else if (Security == "SSL")
                        mailer.SslType = SslMode.Ssl;
                    else if (Security == "NONE")
                        mailer.SslType = SslMode.Ssl;
                    else
                        mailer.SslType = SslMode.Auto;

                    if (mailer.SslType == SslMode.None)
                        mailer.AuthenticationMode = AuthenticationType.PlainText;
                    else
                        mailer.AuthenticationMode = AuthenticationType.Base64;
                    mailer.SendCompleted += compEvent;
                    mailer.SendMail(mymessage);
                    status = 0;
                }
            }
            catch (Exception ex)
            {
                status = -1;
                UIException.LogException(ex, "public static long SendNotification(string EmailTo,string Subject,string MessageContent)");
            }
            return status;
        }

        public int? SendNotification(string EmailTo, string Subject, string MessageContent, string Attachment, string EmailFrom, string MailServer, int Port, string SmtpUser, string SmtpPassword, string Security)
        {
            int? status = 1;
            try
            {
                if (!string.IsNullOrEmpty(EmailTo))
                {
                    var mymessage = new MimeMailMessage();
                    mymessage.From = new MimeMailAddress(EmailFrom);
                    mymessage.To.Add(EmailTo);
                    mymessage.Subject = Subject;
                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(EmailTo);
                    //mail.From = new MailAddress(EmailFrom);
                    //mail.Subject = Subject;
                    try
                    {
                        //System.Net.Mail.Attachment attachment;
                        //attachment = new System.Net.Mail.Attachment(Attachment);
                        //mail.Attachments.Add(attachment);
                        MimeAttachment attachment = new MimeAttachment(Attachment);
                        mymessage.Attachments.Add(attachment);
                    }
                    catch
                    {
                        UIException.LogException("Gagal Attach file to Email", "public static long SendNotification(string EmailTo,string Subject,string MessageContent)");

                    }
                    //string Body = MessageContent;
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = MailServer;
                    //smtp.Port = Port;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword);// Enter seders User name and password
                    //smtp.EnableSsl = true;
                    //smtp.Send(mail);
                    //status = 0;
                    mymessage.Body = MessageContent;
                    var mailer = new MimeMailer(MailServer, Port);
                    mailer.User = SmtpUser;
                    mailer.Password = SmtpPassword;
                    if (Security == "TLS")
                        mailer.SslType = SslMode.Tls;
                    else if (Security == "SSL")
                        mailer.SslType = SslMode.Ssl;
                    else if (Security == "NONE")
                        mailer.SslType = SslMode.Ssl;
                    else
                        mailer.SslType = SslMode.Auto;

                    if (mailer.SslType == SslMode.None)
                        mailer.AuthenticationMode = AuthenticationType.PlainText;
                    else
                        mailer.AuthenticationMode = AuthenticationType.Base64;

                    mailer.SendCompleted += compEvent;
                    mailer.SendMail(mymessage);
                    status = 0;
                }
            }
            catch (Exception ex)
            {
                status = -1;
                UIException.LogException(ex, "public static long SendNotification(string EmailTo,string Subject,string MessageContent)");
            }
            return status;
        }

        public int? TestConnection(string email, string smtpServer, int port, string smtpUser, string smtpPassword, string Security)
        {
            int? status = -1;
            try
            {
                #region comment
                //MailMessage mail = new MailMessage();
                //mail.To.Add(email);
                //mail.From = new MailAddress(email);
                //mail.Subject = "Test Connection";
                //string Body = "Connection Success";
                //mail.Body = Body;
                //mail.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = smtpServer;
                //smtp.Port = Convert.ToInt16(port);
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);// Enter seders User name and password
                //smtp.EnableSsl = false;
                //smtp.Send(mail);
                //status = true;
                #endregion
                var mymessage = new MimeMailMessage();
                mymessage.From = new MimeMailAddress(email);
                mymessage.To.Add(email);
                mymessage.Subject = "Test Connection";
                mymessage.Body = "Connection Success";

                //Create Smtp Client
                var mailer = new MimeMailer(smtpServer, port);
                mailer.User = smtpUser;
                mailer.Password = smtpPassword;
                if (Security == "TLS")
                    mailer.SslType = SslMode.Tls;
                else if (Security == "SSL")
                    mailer.SslType = SslMode.Ssl;
                else if (Security == "NONE")
                    mailer.SslType = SslMode.Ssl;
                else
                    mailer.SslType = SslMode.Auto;

                if (mailer.SslType == SslMode.None)
                    mailer.AuthenticationMode = AuthenticationType.PlainText;
                else
                    mailer.AuthenticationMode = AuthenticationType.Base64;

                //Set a delegate function for call back
                mailer.SendCompleted += compEvent;

                //mailer.SendMailAsync(mymessage);
                mailer.TestConnection();
                //mailer.SendMail(mymessage);
                status = 0;
            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }
        private static void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                Console.Out.WriteLine(e.UserState.ToString());
            if (e.Error != null)
            {
                // MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UIException.LogException(e.Error.Message, "public static long SendNotification(string EmailTo,string Subject,string MessageContent)");
                throw new Exception(e.Error.Message);
            }
            else if (!e.Cancelled)
            {
                //MessageBox.Show("Send successfull!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
