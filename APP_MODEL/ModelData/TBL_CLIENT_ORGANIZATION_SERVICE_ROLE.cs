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
    
    public partial class tbl_Client_Organization_Service_Role
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_Service_Role_ID { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    
        public virtual tbl_Organization_Service_Role tbl_Organization_Service_Role { get; set; }
        public virtual tbl_Organization tbl_Organization { get; set; }
    }
}
