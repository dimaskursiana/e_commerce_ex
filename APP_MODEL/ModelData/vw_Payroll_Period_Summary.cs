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
    
    public partial class vw_Payroll_Period_Summary
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Tax_Period_ID { get; set; }
        public int Tax_Year { get; set; }
        public string Tax_Period_Month { get; set; }
        public Nullable<System.DateTime> Tax_Period { get; set; }
        public Nullable<System.DateTime> Period_Start_Date { get; set; }
        public Nullable<System.DateTime> Period_End_Date { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
    }
}
