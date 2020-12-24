
using APP_COMMON;
using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_NOTIFICATION
{
    public class PrivateMessageer
    {
        public static string strMessage = string.Empty;
        public static long SendNotification(Guid? User_id,string SUBJECT,string MESSAGE)
        {
            long status = 0;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string message = string.Empty;
            try
            {
                tbl_Message msg = new tbl_Message()
                {
                    id = Guid.NewGuid(),
                    Created_DateTime = DateTime.Now,
                    Message_Title = SUBJECT,
                    Is_Read = false,
                    Beneficiary = User_id,
                    Message = MESSAGE, 
                };
                db.tbl_Message.Add(msg);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                status = -1;
                UIException.LogException(ex, "public void Notification(string TO,string SUBJECT,string TEMPLATE_TYPE,List<string> DATA)");
            }
            return status;
        }

    }
}
