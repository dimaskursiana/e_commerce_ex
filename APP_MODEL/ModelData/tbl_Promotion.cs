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
    
    public partial class tbl_Promotion
    {
        public System.Guid id { get; set; }
        public string Promotion_Code { get; set; }
        public string Name { get; set; }
        public string Reward_Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity_Promotion { get; set; }
        public string Provision_Promotion { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<bool> Is_Amount { get; set; }
        public Nullable<System.DateTime> Start_Period_Promotion { get; set; }
        public Nullable<System.DateTime> End_Period_Promotion { get; set; }
        public string Promotion_Type { get; set; }
    }
}
