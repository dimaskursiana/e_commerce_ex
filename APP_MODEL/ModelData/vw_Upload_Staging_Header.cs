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
    
    public partial class vw_Upload_Staging_Header
    {
        public Nullable<System.Guid> ID { get; set; }
        public System.Guid ID_STAGING_HEADER { get; set; }
        public string Upload_Number { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Total_Record { get; set; }
        public string Url { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Category_Type { get; set; }
    }
}
