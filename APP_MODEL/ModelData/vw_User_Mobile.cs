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
    
    public partial class vw_User_Mobile
    {
        public System.Guid id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.Guid Employee_Id { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Name { get; set; }
        public string Organization_Service { get; set; }
        public int Login_Attempt { get; set; }
        public Nullable<bool> Is_Locked { get; set; }
        public Nullable<System.DateTime> Last_Login_Date { get; set; }
        public Nullable<int> Invalid_Password { get; set; }
        public Nullable<bool> Is_HR_User { get; set; }
    }
}