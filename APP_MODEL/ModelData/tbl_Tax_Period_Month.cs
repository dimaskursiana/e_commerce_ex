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
    
    public partial class tbl_Tax_Period_Month
    {
        public System.Guid id { get; set; }
        public System.Guid Tax_Period_ID { get; set; }
        public System.DateTime Tax_Period_Month { get; set; }
        public string Tax_Status { get; set; }
        public Nullable<System.DateTime> Closing_Date_Permanent { get; set; }
        public Nullable<System.DateTime> Closing_Date_NonPermanent { get; set; }
        public Nullable<System.DateTime> Closing_Date { get; set; }
        public Nullable<int> Last_Pembetulan { get; set; }
    
        public virtual tbl_Tax_Period tbl_Tax_Period { get; set; }
    }
}
