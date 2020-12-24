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
    
    public partial class tbl_Role
    {
        public tbl_Role()
        {
            this.tbl_Role_Menu = new HashSet<tbl_Role_Menu>();
            this.tbl_User_Role = new HashSet<tbl_User_Role>();
        }
    
        public System.Guid id { get; set; }
        public string Role_Code { get; set; }
        public string Role_Description { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<bool> Is_Mobile_Phone { get; set; }
    
        public virtual tbl_Organization tbl_Organization { get; set; }
        public virtual ICollection<tbl_Role_Menu> tbl_Role_Menu { get; set; }
        public virtual ICollection<tbl_User_Role> tbl_User_Role { get; set; }
    }
}