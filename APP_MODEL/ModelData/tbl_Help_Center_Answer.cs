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
    
    public partial class tbl_Help_Center_Answer
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Help_Center_ID { get; set; }
        public string Answer { get; set; }
        public string Path { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
    }
}
