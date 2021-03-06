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
    
    public partial class tbl_Organization_Payroll_Component
    {
        public tbl_Organization_Payroll_Component()
        {
            this.tbl_Payroll_Calculation_Component = new HashSet<tbl_Payroll_Calculation_Component>();
            this.tbl_Payroll_Calculation_Component_Ex = new HashSet<tbl_Payroll_Calculation_Component_Ex>();
            this.tbl_Payroll_Slip_Component = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component1 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component2 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component3 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component4 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component5 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component6 = new HashSet<tbl_Payroll_Slip_Component>();
            this.tbl_Payroll_Slip_Component7 = new HashSet<tbl_Payroll_Slip_Component>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid Organization_id { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Component_Group { get; set; }
        public string Taxable_Type { get; set; }
        public Nullable<bool> Tax_Deduction { get; set; }
        public string Tax_Policy { get; set; }
        public string Frequency { get; set; }
        public string Amount_Type { get; set; }
        public string Calculation_Basic { get; set; }
        public string Formula { get; set; }
        public bool Is_New_Join { get; set; }
        public bool Is_Prorate { get; set; }
        public string Prorate_Base { get; set; }
        public string Sign { get; set; }
        public string Account { get; set; }
        public string Db_or_Cr { get; set; }
        public string SPT_Code { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public Nullable<bool> Is_Generated { get; set; }
    
        public virtual ICollection<tbl_Payroll_Calculation_Component> tbl_Payroll_Calculation_Component { get; set; }
        public virtual ICollection<tbl_Payroll_Calculation_Component_Ex> tbl_Payroll_Calculation_Component_Ex { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component1 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component2 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component3 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component4 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component5 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component6 { get; set; }
        public virtual ICollection<tbl_Payroll_Slip_Component> tbl_Payroll_Slip_Component7 { get; set; }
    }
}
