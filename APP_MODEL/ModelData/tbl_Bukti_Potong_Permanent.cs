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
    
    public partial class tbl_Bukti_Potong_Permanent
    {
        public string ID { get; set; }
        public string WorkLocation_Tax_ID { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        public Nullable<int> First_Payroll_Month { get; set; }
        public Nullable<int> End_Payroll_Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<int> RowNum { get; set; }
        public Nullable<System.Guid> Calculation_ID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public int Status_Code { get; set; }
        public string Organization_ID { get; set; }
    }
}