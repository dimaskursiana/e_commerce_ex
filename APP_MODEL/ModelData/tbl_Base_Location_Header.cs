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
    
    public partial class tbl_Base_Location_Header
    {
        public tbl_Base_Location_Header()
        {
            this.tbl_Base_Location_Detail = new HashSet<tbl_Base_Location_Detail>();
        }
    
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_Id { get; set; }
        public string Map_Type { get; set; }
        public Nullable<System.Guid> Reference_Id { get; set; }
        public bool Is_Multiple { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public bool Is_Anywhere_Time_Out { get; set; }
        public bool Is_Anywhere_Time_In { get; set; }
    
        public virtual ICollection<tbl_Base_Location_Detail> tbl_Base_Location_Detail { get; set; }
    }
}
