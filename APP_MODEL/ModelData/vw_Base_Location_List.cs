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
    
    public partial class vw_Base_Location_List
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> Organization_Id { get; set; }
        public string Work_Location { get; set; }
        public bool Is_Multiple { get; set; }
        public bool Is_Anywhere_Time_In { get; set; }
        public bool Is_Anywhere_Time_Out { get; set; }
        public string Map_Type { get; set; }
        public string Status_Code { get; set; }
        public string Authorize_Status { get; set; }
    }
}