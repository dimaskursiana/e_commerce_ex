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
    
    public partial class vw_Role_Organization_Menu_Function
    {
        public long ID { get; set; }
        public System.Guid Role_Id { get; set; }
        public string Role_Code { get; set; }
        public string Role_Description { get; set; }
        public System.Guid Organization_Menu_Function { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Code { get; set; }
        public string Organization_Name { get; set; }
        public System.Guid Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Function_Description { get; set; }
    }
}
