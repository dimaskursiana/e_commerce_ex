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
    
    public partial class Get_Report_Attendance_Result
    {
        public int ID { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Employee { get; set; }
        public Nullable<System.DateTime> Date_Present { get; set; }
        public Nullable<System.DateTime> Day_Time_In { get; set; }
        public string Day_Time_In_Location { get; set; }
        public Nullable<System.DateTime> Day_Time_Out { get; set; }
        public string Day_Time_Out_Location { get; set; }
        public string Status { get; set; }
        public string Photo { get; set; }
        public string Color_Style { get; set; }
    }
}