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
    
    public partial class tbl_Tiering
    {
        public System.Guid id { get; set; }
        public string Tiering_Code { get; set; }
        public string Reward_Type { get; set; }
        public string Tiering_Type { get; set; }
        public string Description { get; set; }
        public string Provision_Tiering { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<System.DateTime> Start_Tiering { get; set; }
        public Nullable<System.DateTime> End_Tiering { get; set; }
        public Nullable<bool> Status_Tiering { get; set; }
        public Nullable<int> Tiering_Employee { get; set; }
    }
}
