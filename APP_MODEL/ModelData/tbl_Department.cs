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
    
    public partial class tbl_Department
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Department_Code { get; set; }
        public string Description { get; set; }
        public string Resp_Center { get; set; }
        public string Resp_Contra { get; set; }
        public string Head_Employee_No { get; set; }
        public string Head_Name { get; set; }
        public string Head_Job_Desc { get; set; }
        public string Address { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    
        public virtual tbl_Organization tbl_Organization { get; set; }
    }
}
