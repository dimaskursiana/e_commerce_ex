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
    
    public partial class tbl_Employee_Payroll_Component_Effective
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Employee_Payroll_Component { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Formula { get; set; }
        public string Amount_Type { get; set; }
    
        public virtual tbl_Employee_Payroll_Component tbl_Employee_Payroll_Component { get; set; }
    }
}
