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
    
    public partial class vw_Calculation_Summary
    {
        public System.Guid id { get; set; }
        public string Batch { get; set; }
        public string Tax_Period { get; set; }
        public string Payroll_Period { get; set; }
        public Nullable<int> Run { get; set; }
        public Nullable<int> Calculate_Status { get; set; }
        public string txtStatus_Code { get; set; }
        public string txtAuthorize_Status { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Calculation_Type { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Fail_Reason { get; set; }
    }
}
