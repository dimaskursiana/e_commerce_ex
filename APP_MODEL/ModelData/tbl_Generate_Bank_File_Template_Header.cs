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
    
    public partial class tbl_Generate_Bank_File_Template_Header
    {
        public tbl_Generate_Bank_File_Template_Header()
        {
            this.tbl_Generate_Bank_File_Template_Detail = new HashSet<tbl_Generate_Bank_File_Template_Detail>();
        }
    
        public System.Guid id { get; set; }
        public string Bank_Code { get; set; }
        public string File_Format { get; set; }
        public string Delimiter { get; set; }
    
        public virtual ICollection<tbl_Generate_Bank_File_Template_Detail> tbl_Generate_Bank_File_Template_Detail { get; set; }
    }
}