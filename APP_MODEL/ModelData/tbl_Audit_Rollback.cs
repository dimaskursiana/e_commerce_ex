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
    
    public partial class tbl_Audit_Rollback
    {
        public long id { get; set; }
        public Nullable<System.Guid> Record_Id { get; set; }
        public System.DateTime Reject_Date { get; set; }
        public Nullable<System.Guid> Version_Id { get; set; }
        public string RollBack_Script { get; set; }
    }
}
