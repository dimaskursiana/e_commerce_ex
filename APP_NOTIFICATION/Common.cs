using APP_COMMON;
using APP_MODEL.ModelData; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_NOTIFICATION
{
    public static class Common
    {
        public static string strMessage = string.Empty;
        public static string getSysParam(string PARAM_CODE)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string VALUE = string.Empty;
            var sysParam = db.tbl_SysParam.Where(p => p.Param_Code == PARAM_CODE).FirstOrDefault();
            if (sysParam != null)
                VALUE = sysParam.Value;
            return VALUE;
        } 
          
        //public static void SetNotificationData<T>(this IList<T> list, string Template_code, List<string> CostCenter, string NPK, string key)
        //{
        //    NotificationData dataModel = new NotificationData();
        //    dataModel.cost_center = CostCenter;
        //    dataModel.NPK = NPK;
        //    dataModel.template_code = Template_code; 
        //    Type elementType = typeof(T);
        //    DataSet ds = new DataSet();
        //    DataTable t = new DataTable();
        //    int Count = 0;
        //    try
        //    {
        //        ds.Tables.Add(t);

        //        //add a column to table for each public property on T
        //        foreach (var propInfo in elementType.GetProperties())
        //        {
        //            Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

        //            t.Columns.Add(propInfo.Name, ColType);
        //        }

        //        //go through each property on T and add each value to the table
        //        foreach (T item in list)
        //        {
        //            DataRow row = t.NewRow();

        //            foreach (var propInfo in elementType.GetProperties())
        //            {
        //                row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
        //            }

        //            t.Rows.Add(row);
        //        }
        //        Count = ds.Tables[0].Rows.Count;
        //    }
        //    catch(Exception ex)
        //    {
        //        UIException.LogException(ex, "public static DataSet ToDataSet<T>(this IList<T> list,out int Count)", out strMessage);
        //        ds = new DataSet();
        //    }
        //    dataModel.dsNotification = ds;
        //    UICache.NOTIFICATION_DATA_SET(dataModel, key + "NOTIFICATION"); 
        //}

        //public static void SetNotificationWorkshopFinal(List<VW_ACTIVITY> list, string Template_code, List<string> CostCenter, string NPK, string key)
        //{
        //    NotificationData dataModel = new NotificationData();
        //    dataModel.cost_center = CostCenter;
        //    dataModel.NPK = NPK; 
        //    dataModel.template_code = Template_code;
        //    try
        //    {
        //        dataModel.notifWorkshopFinal = list;
        //    }
        //    catch (Exception ex)
        //    {
        //        UIException.LogException(ex, "SetNotificationWorkshop", out strMessage);
               
        //    } 
        //    UICache.NOTIFICATION_DATA_SET(dataModel, key);
        //} 


        public static DataSet ConvertToDataset<T>(this IList<T> list)
        { 
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            int Count = 0;
            try
            {
                ds.Tables.Add(t);

                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                    t.Columns.Add(propInfo.Name, ColType);
                }

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    DataRow row = t.NewRow();

                    foreach (var propInfo in elementType.GetProperties())
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }

                    t.Rows.Add(row);
                }
                Count = ds.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static DataSet ToDataSet<T>(this IList<T> list,out int Count)");
                ds = new DataSet();
            }
             return ds; 
        }


        public static bool isSendingEmail()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            bool NotifStatus = false;
            var sysParam = db.tbl_SysParam.Where(p => p.Param_Code == "EMAIL_STATUS").FirstOrDefault();
            if (sysParam != null)
            {
                if (sysParam.Value == "1")
                    NotifStatus = true;
                else
                    NotifStatus = false; 
            }

            return NotifStatus;
        }

        public static bool isSendingPrivateMessage()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            bool NotifStatus = false;
            var sysParam = db.tbl_SysParam.Where(p => p.Param_Code == "PRIVATE_MESSAGE_STATUS").FirstOrDefault();
            if (sysParam != null)
            {
                if (sysParam.Value == "1")
                    NotifStatus = true;
                else
                    NotifStatus = false;
            }

            return NotifStatus;
        }

        public static void AuditLogNotification(List<tbl_Notification_Log> listDataLog)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string message = string.Empty;
            try
            {
                db.tbl_Notification_Log.AddRange(listDataLog);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public void Notification(string TO,string SUBJECT,string TEMPLATE_TYPE,List<string> DATA)");
            }
        } 
    }

}
