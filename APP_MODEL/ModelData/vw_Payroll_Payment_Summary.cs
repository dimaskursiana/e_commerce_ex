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
    
    public partial class vw_Payroll_Payment_Summary
    {
        public long Id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<int> Run { get; set; }
        public string Title { get; set; }
        public string Payment_Bank_Code { get; set; }
        public string Employee_Bank_Code { get; set; }
        public string Employee_Bank_Account { get; set; }
        public Nullable<double> Employee_Salary_Transferred { get; set; }
    }
}
