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
    
    public partial class vw_Payroll_Slip_Employee
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<System.DateTime> Tax_Period { get; set; }
        public Nullable<System.DateTime> Period_Start_Date { get; set; }
        public Nullable<System.DateTime> Period_End_Date { get; set; }
        public string Generation_Number { get; set; }
        public string File_Name_Payslip { get; set; }
        public string Payslip_Distribution { get; set; }
        public Nullable<bool> Exclude_Zero_Value { get; set; }
        public Nullable<int> Run_From { get; set; }
        public Nullable<int> Run_To { get; set; }
        public string Payslip_Status { get; set; }
        public string Payslip_Url { get; set; }
        public Nullable<System.DateTime> Generate_Date { get; set; }
    }
}
