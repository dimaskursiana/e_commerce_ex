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
    
    public partial class vw_Calculation_Tax_History
    {
        public long Id { get; set; }
        public System.Guid Employee_ID { get; set; }
        public string Emp_No { get; set; }
        public string Name { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Name { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Payroll_Month { get; set; }
        public Nullable<double> Tax_Allowance { get; set; }
        public Nullable<double> Tax_Allowance_On_Bonus { get; set; }
        public Nullable<double> Tax_allowance_On_Final_Payment { get; set; }
        public Nullable<double> Gross_Salary { get; set; }
        public Nullable<double> Tax21PerMonthRegular { get; set; }
        public Nullable<double> Tax21PerMonth { get; set; }
        public Nullable<double> Tax_Per_Period { get; set; }
        public Nullable<double> Total_Gross_Final { get; set; }
        public Nullable<double> Tax_On_Final { get; set; }
        public Nullable<double> Tax_Regular_Borne_By_Company { get; set; }
        public Nullable<double> Tax_Irregular_Borne_By_Company { get; set; }
        public Nullable<double> Total_Tax_Borne_By_Company { get; set; }
        public Nullable<double> Tax_On_Final_Borne_By_Company { get; set; }
        public Nullable<double> Tax_Refund { get; set; }
        public Nullable<double> Net_Salary { get; set; }
        public Nullable<double> Total_Deduction { get; set; }
        public Nullable<double> Take_Home_Pay { get; set; }
    }
}
