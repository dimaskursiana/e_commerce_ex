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
    
    public partial class tbl_Generate_Bank_File_Template_Detail
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Generate_Bank_Template_ID { get; set; }
        public string Section { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> Length { get; set; }
        public string Value { get; set; }
        public Nullable<bool> Is_Numeric { get; set; }
        public Nullable<bool> Fixed_Length { get; set; }
    
        public virtual tbl_Generate_Bank_File_Template_Header tbl_Generate_Bank_File_Template_Header { get; set; }
    }
}
