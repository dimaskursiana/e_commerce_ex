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
    
    public partial class tbl_Tax_Status_Effective_Year
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Tax_ID { get; set; }
        public string Tax_Status { get; set; }
        public string Year_Status { get; set; }
    
        public virtual tbl_Tax tbl_Tax { get; set; }
    }
}