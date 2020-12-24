using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Data;

namespace APP_MODEL.ModelData
{

    public class NOTIFICATION_MODEL
    {
        public string MessageTo { get; set; }
        public Guid? UserId { get; set; }
        public string MessageBody { get; set; }
        public string IsPrivateMessage { get; set; }
        public string IsEmail { get; set; }

    }

    public class NOTIFICATION_DATA
    { 
        public string Sender { get; set; }
        public string Template_Code { get; set; } 
        public DataSet DsNotification { get; set; }
    }

    public class PRIVATE_MESSAGE
    {
        public List<tbl_Message> ListMessage { get; set; }
        public int Page { get; set; }
        public string Filter { get; set; }
        public int PageCount { get; set; }
        public bool Next { get; set; }
        public bool Prev { get; set; }
    }

}
