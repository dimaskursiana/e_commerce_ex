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
    
    public partial class tbl_Organization_Signature
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Logo_Signature { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
    
        public virtual tbl_Organization tbl_Organization { get; set; }
    }
}
