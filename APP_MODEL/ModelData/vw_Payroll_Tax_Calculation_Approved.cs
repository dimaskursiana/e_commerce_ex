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
    
    public partial class vw_Payroll_Tax_Calculation_Approved
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Calculation_Type { get; set; }
        public string Batch { get; set; }
        public System.Guid Calculation_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        public string Value { get; set; }
        public string Calculation_Component { get; set; }
        public Nullable<int> Payroll_Month { get; set; }
        public Nullable<int> Order { get; set; }
        public string Tax_Policy { get; set; }
        public Nullable<int> Payroll_Year { get; set; }
        public Nullable<int> Run { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
    }
}