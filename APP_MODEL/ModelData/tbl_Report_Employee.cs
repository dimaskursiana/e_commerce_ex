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
    
    public partial class tbl_Report_Employee
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        public Nullable<System.Guid> Tax_Period_Month_ID { get; set; }
        public Nullable<int> Correction { get; set; }
        public string Full_Name { get; set; }
        public string Department { get; set; }
        public string Family_Status { get; set; }
        public Nullable<System.DateTime> Date_Of_Hire { get; set; }
        public Nullable<System.DateTime> Date_Of_Resign { get; set; }
        public string NPWP { get; set; }
        public string Salary_Tax_Policy { get; set; }
        public Nullable<decimal> Tax_Allowance { get; set; }
        public Nullable<decimal> Tax_Allowance_On_Bonus { get; set; }
        public Nullable<decimal> Tax_Allowance_Final { get; set; }
        public Nullable<decimal> Monthly_Income { get; set; }
        public Nullable<decimal> Income_Year { get; set; }
        public Nullable<decimal> Total_Income { get; set; }
        public Nullable<decimal> Functional_Cost { get; set; }
        public Nullable<decimal> Functional_Cost_On_Bonus { get; set; }
        public Nullable<decimal> Non_Taxable_Income { get; set; }
        public Nullable<decimal> Taxable_Income { get; set; }
        public Nullable<decimal> Tax_Due_Up_To_Current_Period_Regular { get; set; }
        public Nullable<decimal> Tax_Due_Up_To_Current_Period_Non_Regular { get; set; }
        public Nullable<decimal> Tax_Payment_Regular { get; set; }
        public Nullable<decimal> Tax_Payment_Non_Regular { get; set; }
        public Nullable<decimal> Tax_Due_On_Current_Regular { get; set; }
        public Nullable<decimal> Tax_Due_On_Current_Non_Regular { get; set; }
        public string Position { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Status { get; set; }
        public Nullable<decimal> Bonus_or_Non_Regular_Income { get; set; }
        public Nullable<decimal> Gross_Salary { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Tax_on_THR_or_Bonus { get; set; }
        public Nullable<decimal> Tax_Regular_Borne_By_Company { get; set; }
        public Nullable<decimal> Tax_I_Regular_Borne_By_Company { get; set; }
        public Nullable<decimal> Tax_On_Final_Payment { get; set; }
        public Nullable<decimal> Tax_On_Final_Borne_By_Company { get; set; }
        public Nullable<decimal> Tax_Refund { get; set; }
        public Nullable<decimal> Net_Salary { get; set; }
        public Nullable<bool> Is_Cash { get; set; }
        public Nullable<decimal> Take_Home_Pay { get; set; }
        public Nullable<decimal> Up_To_Current_Period { get; set; }
        public Nullable<decimal> Total_Deduction { get; set; }
        public string Location { get; set; }
        public string Batch_No { get; set; }
        public string Calculation_Type { get; set; }
        public string Month { get; set; }
        public string End_Payroll_Month { get; set; }
        public string Tax_ID_Status_Change { get; set; }
        public Nullable<bool> Status_Code { get; set; }
        public Nullable<bool> Authorize_Status { get; set; }
        public Nullable<decimal> Tax_Per_Period { get; set; }
        public Nullable<decimal> Tax_Severance { get; set; }
        public Nullable<decimal> Total_Gross_Final { get; set; }
        public Nullable<decimal> Tax_Refund_Final { get; set; }
        public Nullable<decimal> Net_Income_Final { get; set; }
        public Nullable<decimal> Total_Deduction_Final { get; set; }
        public Nullable<decimal> Take_Home_Pay_Final { get; set; }
        public string First_Payroll_Month { get; set; }
        public string Current_Payroll_Month { get; set; }
        public string Annual_Factor { get; set; }
        public string Functional_Cost_Constant { get; set; }
        public Nullable<decimal> Total_Tax_Allowance_On_Final_Payment { get; set; }
        public Nullable<decimal> Cumulative_Gross_Final { get; set; }
        public Nullable<decimal> Tax_Due_Up_To_Current_Tax_Period_Month { get; set; }
        public Nullable<decimal> Tax_Paid { get; set; }
        public Nullable<decimal> Tax_On_Final_Payment_Per_Period { get; set; }
        public Nullable<decimal> Cumulative_Gross_Income { get; set; }
        public Nullable<decimal> Netto { get; set; }
        public Nullable<decimal> Netto_Annualized { get; set; }
    }
}
