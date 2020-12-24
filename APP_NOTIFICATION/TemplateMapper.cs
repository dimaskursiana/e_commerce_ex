using APP_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP_MODEL.ModelData;
using System.Data;

namespace APP_NOTIFICATION
{
    public static class TemplateMapper
    {
        public static string strMessage = string.Empty;

        public static void MergeWithGlobalData(ref DataSet dsDataTable,tbl_User_Trial USER)
        {      
            const string CONST_USER_NAME_LOGIN = "USER_NAME_LOGIN";

            if (dsDataTable.Tables[0].Columns.Contains(CONST_USER_NAME_LOGIN))
            {
                if (dsDataTable.Tables.Count > 0)
                {
                    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                    var gl_trx = db.tbl_User_Trial;
                    for (int i = 0; i < dsDataTable.Tables[0].Rows.Count; i++)
                    {
                        string Value = dsDataTable.Tables[0].Rows[i][CONST_USER_NAME_LOGIN].ToString();
                        if (!string.IsNullOrEmpty(Value))
                            dsDataTable.Tables[0].Rows[i][CONST_USER_NAME_LOGIN] = Value + "-" + gl_trx.Where(p => p.Email == Value).FirstOrDefault().Email;
                    }
                }
            } 

        }

        public static List<NotificationModel> GetNotificationModelData(int Index, string NOTIFICATION_CODE, tbl_Notification_Template dbTEMPLATE, DataSet data, string Email_To, DataSet dataDetail = null)
        {
            List<NotificationModel> model = new List<NotificationModel>();
            string Beneficiary = Common.getSysParam("BENEFICIARY_EMAIL");
            string Message = Mapper(Index, NOTIFICATION_CODE, dbTEMPLATE, data, dataDetail); 

            List<string> ListBeneficiary = new List<string>();
            
            string strMessage = Message;  

            strMessage = strMessage.Replace(Beneficiary, Email_To);
            model.Add(new NotificationModel { message_body = strMessage, message_to = Email_To}); 
            
            
            //cleansing and add Variable Global
            //List<string> ListVariable = new List<string>();
            //if (dbTEMPLATE.MAPPING_VALUE.Contains("|"))
            //    ListVariable = dbTEMPLATE.MAPPING_VALUE.Split(new char[] { '|' }).ToList<string>();
            //else
            //    ListVariable.Add(dbTEMPLATE.MAPPING_VALUE);
            //for (int i = 0; i < ListVariable.Count(); i++)
            //{
            //    foreach (var item in model)
            //    {  
            //        item.message_body = item.message_body.Replace("/" + ListVariable[i], "");
            //        item.message_body = item.message_body.Replace("/"+ListVariable[i], "");
            //        item.message_body = item.message_body.Replace(ListVariable[i]+"/", "");
            //    }
            //    i = i + 1;
            //}
            return model;
        }

        public static string Mapper(int Index, string NOTIFICATION_CODE, tbl_Notification_Template dbTEMPLATE, DataSet data, DataSet dataDetail = null)
        {
            string MESSAGE = dbTEMPLATE.Body; 
            string strIndexReplace = string.Empty;

            if (string.IsNullOrEmpty(MESSAGE))
                return MESSAGE;
            try
            { 
                List<string> coa = new List<string>();
                if (data.Tables.Count > 0)
                {
                    DataTable dtNotif = data.Tables[0];
                    if (dtNotif.Rows.Count > 0)
                    {
                        DataRow drNotif = dtNotif.Rows[Index];  
                        List<string> ListVariable = new List<string>();
                        if (dbTEMPLATE.Mapping_Value.Contains("|"))
                            ListVariable = dbTEMPLATE.Mapping_Value.Split('|').ToList<string>();
                        else
                            ListVariable.Add(dbTEMPLATE.Mapping_Value);

                        for(int i=0; i< ListVariable.Count();i++)
                        {
                            if (ListVariable[i].ToString().ToUpper().Contains("TABLE"))
                            {
                                DataTable dtTable = dataDetail.Tables[0];
                                MESSAGE = MESSAGE.Replace(ListVariable[i], GenerateTable(dtTable, ListVariable[i + 1]));
                            }
                            else if (dtNotif.Columns.Contains(ListVariable[i+1]))
                            {
                                MESSAGE = MESSAGE.Replace(ListVariable[i], drNotif[ListVariable[i + 1]].ToString());
                            } 
                            //else
                            //{ 
                            //    MESSAGE = MESSAGE.Replace(ListVariable[i], "");
                            //}
                            i = i + 1;
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static string Mapper()");
            }
            return MESSAGE;
        }

        private static string GenerateTable(DataTable dtTable, string tableConfig)
        {
            string strTableTemplate = UICommonFunction.GetSysParam("NOTIFICATION_TABLE").FirstOrDefault().Value;
             
            try
            {
                List<string> ListTblConfig = new List<string>();  //index ganjil Nama Colom dan index genap value
               
                if (tableConfig.Contains(";"))
                    ListTblConfig = tableConfig.Split(';').ToList<string>();
                else
                    ListTblConfig.Add(tableConfig); 

                string column = "";
                for (int i = 0; i < ListTblConfig.Count(); i+=2)
                { 
                     column = column + "<th>" + ListTblConfig[i] + "</th>";   
                }
                strTableTemplate = strTableTemplate.Replace("@HEAD", column);

                string row = "";
                for (int Index = 0; Index < dtTable.Rows.Count; Index++)
                {
                    row = row + "<tr style=\"border: 2px solid #15c;font-size: 8pt;\">";
                    DataRow drTable = dtTable.Rows[Index];  
                    for (int i = 1; i < ListTblConfig.Count(); i += 2)
                    { 
                        string columnName = ListTblConfig[i].ToString().Replace("@", string.Empty);
                        if (dtTable.Columns.Contains(columnName))
                        {
                            var isAmount = UICommonFunction.IsDecimal(drTable[columnName].ToString());
                            if (isAmount)
                                row = row + "<td style=\"border: 1px solid #15c;font-size: 8pt;\"> Rp." + UICommonFunction.FormatMoney(drTable[columnName].ToString()) + "</td>";
                            else
                                row = row + "<td style=\"border: 1px solid #15c;font-size: 8pt;\">" + drTable[columnName].ToString() + "</td>";
                        }
                    }
                    row = row +"</tr>";
                }
                strTableTemplate = strTableTemplate.Replace("@BODY", row);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static string GenerateTable()");
            }
            return strTableTemplate;
        }
         
        private static string PrivateMessage(string TEMPLATE, List<Object> DATA)
        {
            string message = TEMPLATE;
            string strIndexReplace = string.Empty;
            tbl_Message MODEL = (tbl_Message)DATA.FirstOrDefault();
            if (string.IsNullOrEmpty(message))
                return message;
            try
            {
                message.Replace("Penerima", MODEL.Beneficiary.ToString());
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static string TemplateNotification(string TEMPLATE, List<string> DATA)");
            }
            return message;
        }
    }
}
