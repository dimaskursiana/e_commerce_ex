//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APP_MODEL.ModelData
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Notification_Log
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public Nullable<System.DateTime> Log_Date { get; set; }
        public string Notification_Type { get; set; }
        public string Message_To { get; set; }
        public string Message_Subject { get; set; }
        public string Message_Body { get; set; }
        public string Message_Attachment { get; set; }
        public Nullable<int> Status { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
    }
}
