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
    
    public partial class tbl_Menu
    {
        public tbl_Menu()
        {
            this.tbl_Menu_Function = new HashSet<tbl_Menu_Function>();
        }
    
        public System.Guid id { get; set; }
        public string Menu_Code { get; set; }
        public string Menu_Name { get; set; }
        public Nullable<int> Menu_Level { get; set; }
        public Nullable<int> Menu_Position { get; set; }
        public Nullable<System.Guid> Parent_ID { get; set; }
        public string Menu_Url { get; set; }
        public string Icon { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public bool Is_Tab { get; set; }
        public Nullable<bool> Show { get; set; }
    
        public virtual ICollection<tbl_Menu_Function> tbl_Menu_Function { get; set; }
    }
}
