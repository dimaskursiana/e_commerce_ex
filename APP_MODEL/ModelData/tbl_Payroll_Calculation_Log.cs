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
    
    public partial class tbl_Payroll_Calculation_Log
    {
        public System.Guid id { get; set; }
        public Nullable<System.DateTime> Log_Date { get; set; }
        public string Calculation_ID { get; set; }
        public string Calculation_Details { get; set; }
        public Nullable<int> Calculate_Status { get; set; }
    }
}