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
    
    public partial class vw_Employee_Payroll_Component
    {
        public System.Guid id { get; set; }
        public System.Guid Employee_id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Employee_Name { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Component_Group { get; set; }
        public string Taxable_Type { get; set; }
        public string Tax_Deduction { get; set; }
        public string Tax_Policy { get; set; }
        public string Frequency { get; set; }
        public string Amount_Type { get; set; }
        public string Calculation_Basic { get; set; }
        public string Prorate_Base { get; set; }
        public bool Is_New_Join { get; set; }
        public bool Is_Prorate { get; set; }
        public string Sign { get; set; }
        public string Account { get; set; }
        public string Db_or_Cr { get; set; }
        public string SPT_Code { get; set; }
        public string Remark { get; set; }
        public string Status_Code { get; set; }
        public string Authorize_Status { get; set; }
        public Nullable<bool> Is_Generated { get; set; }
    }
}
