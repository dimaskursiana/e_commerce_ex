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
    
    public partial class tbl_Payroll_Schedule
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<int> Payroll_Advice_Sub { get; set; }
        public string Payroll_Advice_Sub_Meas { get; set; }
        public Nullable<int> Payroll_Report_Sub { get; set; }
        public string Payroll_Report_Sub_Meas { get; set; }
        public Nullable<int> Payroll_Approval { get; set; }
        public string Payroll_Approval_Meas { get; set; }
        public Nullable<int> Jurnal { get; set; }
        public string Jurnal_Meas { get; set; }
        public Nullable<int> Payslip { get; set; }
        public string Payslip_Meas { get; set; }
        public Nullable<int> SPT_Report { get; set; }
        public string SPT_Report_Meas { get; set; }
        public Nullable<int> Tax_Report { get; set; }
        public string Tax_Report_Meas { get; set; }
        public Nullable<int> Pay_Day { get; set; }
        public string Holiday_Treatment { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<int> Cut_Off_Time_Start { get; set; }
        public Nullable<int> Cut_Off_Time_End { get; set; }
    
        public virtual tbl_Organization tbl_Organization { get; set; }
    }
}
