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
    
    public partial class tbl_Appointment_Contract_Information
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Appointment_Id { get; set; }
        public string Contract_No { get; set; }
        public Nullable<System.DateTime> Contract_Start_Date { get; set; }
        public Nullable<System.DateTime> Contract_End_Date { get; set; }
    
        public virtual tbl_Employee_Appointment tbl_Employee_Appointment { get; set; }
    }
}
