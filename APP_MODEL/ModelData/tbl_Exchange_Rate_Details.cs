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
    
    public partial class tbl_Exchange_Rate_Details
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Exchange_Rate_ID { get; set; }
        public string Currency_From { get; set; }
        public string Currency_To { get; set; }
        public Nullable<decimal> Rate { get; set; }
    
        public virtual tbl_Exchange_Rate tbl_Exchange_Rate { get; set; }
    }
}
