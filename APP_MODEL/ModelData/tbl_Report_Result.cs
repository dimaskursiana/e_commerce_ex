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
    
    public partial class tbl_Report_Result
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Report_ID { get; set; }
        public string Col_ID { get; set; }
        public string Col_Description { get; set; }
        public string Value { get; set; }
        public Nullable<int> Column_Index { get; set; }
        public Nullable<int> Row_Index { get; set; }
        public Nullable<int> Type { get; set; }
        public string Group_Value { get; set; }
    }
}
