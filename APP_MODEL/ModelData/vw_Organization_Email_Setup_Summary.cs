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
    
    public partial class vw_Organization_Email_Setup_Summary
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Code { get; set; }
        public string Organization_Name { get; set; }
        public string Email_Address { get; set; }
        public string SMTP_Server { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<int> Status_Code { get; set; }
    }
}