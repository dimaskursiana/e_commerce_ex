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
    
    public partial class vw_Generate_Bank_File_Template
    {
        public long Id { get; set; }
        public string Bank_Code { get; set; }
        public Nullable<bool> Fixed_Length { get; set; }
        public string File_Format { get; set; }
        public string Delimiter { get; set; }
        public string Section { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> Length { get; set; }
        public string Value { get; set; }
        public Nullable<bool> Is_Numeric { get; set; }
    }
}
