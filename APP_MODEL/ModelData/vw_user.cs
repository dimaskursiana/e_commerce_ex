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
    
    public partial class vw_user
    {
        public System.Guid id { get; set; }
        public System.Guid Role_ID { get; set; }
        public System.Guid Organization_Team_ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Full_Name { get; set; }
        public string Role_Code { get; set; }
        public string Team_Code { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public System.Guid Org_ID { get; set; }
        public string Organization_Code { get; set; }
    }
}