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
    
    public partial class tbl_Third_Party_Content
    {
        public System.Guid id { get; set; }
        public System.Guid Third_Party_ID { get; set; }
        public string Content_Link { get; set; }
        public bool Have_Term_Condition { get; set; }
        public bool Is_Priority { get; set; }
        public string Content_Header { get; set; }
        public string Content_Detail { get; set; }
        public System.DateTime Valid_Until { get; set; }
        public string Term_and_Condition { get; set; }
        public string Content_Detail_String { get; set; }
    }
}
