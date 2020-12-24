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
    
    public partial class tbl_Employee_Education
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee_No { get; set; }
        public Nullable<System.Guid> Last_Education { get; set; }
        public Nullable<System.DateTime> Education_Start { get; set; }
        public Nullable<System.DateTime> Education_End { get; set; }
        public Nullable<System.DateTime> Certificate_Expire { get; set; }
        public string Qualification { get; set; }
        public string Institute { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    
        public virtual tbl_Education tbl_Education { get; set; }
        public virtual tbl_Employee tbl_Employee { get; set; }
        public virtual tbl_Organization tbl_Organization { get; set; }
    }
}
