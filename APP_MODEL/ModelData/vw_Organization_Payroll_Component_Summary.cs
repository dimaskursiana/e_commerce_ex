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
    
    public partial class vw_Organization_Payroll_Component_Summary
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_id { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public string Taxable_Type { get; set; }
        public string Tax_Deduction { get; set; }
        public string Frequency { get; set; }
        public string Amount_Type { get; set; }
        public string Calculation_Basic { get; set; }
        public string Status { get; set; }
        public string Authorize_Status { get; set; }
        public Nullable<bool> Is_Generated { get; set; }
    }
}
