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
    
    public partial class vw_Employee_Leave
    {
        public System.Guid Id { get; set; }
        public System.Guid Organization_Id { get; set; }
        public string Organization_Name { get; set; }
        public System.Guid Employee_Id { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public Nullable<System.DateTime> From_Date { get; set; }
        public Nullable<System.DateTime> To_Date { get; set; }
        public string strDate { get; set; }
        public string Leave_Type { get; set; }
        public string Duration_Type { get; set; }
        public Nullable<double> Duration { get; set; }
        public Nullable<System.TimeSpan> Duration_Time_Start { get; set; }
        public Nullable<System.TimeSpan> Duration_Time_End { get; set; }
        public string Description { get; set; }
        public string strStatus { get; set; }
        public Nullable<bool> Is_Reset { get; set; }
        public string Reviewed_By { get; set; }
        public string Approver_Remarks { get; set; }
        public string Path_Document { get; set; }
        public Nullable<System.DateTime> Request_Date { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
    }
}
