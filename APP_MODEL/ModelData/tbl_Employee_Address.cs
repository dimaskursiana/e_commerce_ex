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
    
    public partial class tbl_Employee_Address
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        public string Address { get; set; }
        public string Village { get; set; }
        public string District { get; set; }
        public string City_Regency { get; set; }
        public string State_Province { get; set; }
        public string Country { get; set; }
        public string Zip_Code { get; set; }
        public string Permanent_Address { get; set; }
        public string Permanent_Village { get; set; }
        public string Permanent_District { get; set; }
        public string Permanent_City_Regency { get; set; }
        public string Permanent_State_Province { get; set; }
        public string Permanent_Country { get; set; }
        public string Permanent_Zip_Code { get; set; }
        public string Mailing_Address { get; set; }
        public string Mailing_Village { get; set; }
        public string Mailing_District { get; set; }
        public string Mailing_City_Regency { get; set; }
        public string Mailing_State_Province { get; set; }
        public string Mailing_Country { get; set; }
        public string Mailing_Zip_Code { get; set; }
        public string Residential_Phone_No { get; set; }
        public string Office_Phone_No { get; set; }
        public string Mobile_Phone_No { get; set; }
        public string Coorporate_Email_Address { get; set; }
        public string Personal_Email_Address { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<bool> Permanent_Address_Same_Legal_Address { get; set; }
        public Nullable<bool> Mailing_Address_Same_Legal_Address { get; set; }
        public Nullable<bool> Mailing_Address_Same_Permanen_Address { get; set; }
    }
}
