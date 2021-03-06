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
    
    public partial class tbl_Payroll_Calculation
    {
        public tbl_Payroll_Calculation()
        {
            this.tbl_Payroll_Calculation_Component = new HashSet<tbl_Payroll_Calculation_Component>();
            this.tbl_Payroll_Calculation_Employee = new HashSet<tbl_Payroll_Calculation_Employee>();
            this.tbl_Payroll_Calculation_Component_Ex = new HashSet<tbl_Payroll_Calculation_Component_Ex>();
            this.tbl_Payroll_Calculation_Employee_Ex = new HashSet<tbl_Payroll_Calculation_Employee_Ex>();
            this.tbl_Payroll_Transaction = new HashSet<tbl_Payroll_Transaction>();
            this.tbl_Payroll_Tax_Correction = new HashSet<tbl_Payroll_Tax_Correction>();
            this.tbl_Payroll_Closing = new HashSet<tbl_Payroll_Closing>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public string Calculation_Type { get; set; }
        public string Batch { get; set; }
        public Nullable<int> Run { get; set; }
        public bool Recalculate { get; set; }
        public bool All_Employee { get; set; }
        public bool All_Component { get; set; }
        public Nullable<int> Calculate_Status { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<bool> YearAndCorrection { get; set; }
    
        public virtual ICollection<tbl_Payroll_Calculation_Component> tbl_Payroll_Calculation_Component { get; set; }
        public virtual ICollection<tbl_Payroll_Calculation_Employee> tbl_Payroll_Calculation_Employee { get; set; }
        public virtual ICollection<tbl_Payroll_Calculation_Component_Ex> tbl_Payroll_Calculation_Component_Ex { get; set; }
        public virtual ICollection<tbl_Payroll_Calculation_Employee_Ex> tbl_Payroll_Calculation_Employee_Ex { get; set; }
        public virtual ICollection<tbl_Payroll_Transaction> tbl_Payroll_Transaction { get; set; }
        public virtual ICollection<tbl_Payroll_Tax_Correction> tbl_Payroll_Tax_Correction { get; set; }
        public virtual tbl_Payroll_Period_Detail tbl_Payroll_Period_Detail { get; set; }
        public virtual ICollection<tbl_Payroll_Closing> tbl_Payroll_Closing { get; set; }
    }
}
