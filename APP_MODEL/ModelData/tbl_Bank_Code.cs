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
    
    public partial class tbl_Bank_Code
    {
        public tbl_Bank_Code()
        {
            this.tbl_Oganization_Bank_Account = new HashSet<tbl_Oganization_Bank_Account>();
        }
    
        public System.Guid id { get; set; }
        public string Bank_Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string BI_Bank_Code { get; set; }
        public string Swift_Code { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorized_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    
        public virtual ICollection<tbl_Oganization_Bank_Account> tbl_Oganization_Bank_Account { get; set; }
    }
}