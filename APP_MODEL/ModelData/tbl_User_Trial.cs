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
    
    public partial class tbl_User_Trial
    {
        public System.Guid id { get; set; }
        public string User_Name { get; set; }
        public string Name { get; set; }
        public string Phone_1 { get; set; }
        public string Phone_2 { get; set; }
        public string Office_Phone { get; set; }
        public string Email { get; set; }
        public string Company_Name { get; set; }
        public string Company_Address { get; set; }
        public Nullable<System.DateTime> Trial_Date { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    }
}
