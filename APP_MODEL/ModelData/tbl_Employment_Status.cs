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
    
    public partial class tbl_Employment_Status
    {
        public tbl_Employment_Status()
        {
            this.tbl_Employee_Appointment = new HashSet<tbl_Employee_Appointment>();
        }
    
        public System.Guid id { get; set; }
        public string Employment_Status_Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status_Code { get; set; }
    
        public virtual ICollection<tbl_Employee_Appointment> tbl_Employee_Appointment { get; set; }
    }
}
