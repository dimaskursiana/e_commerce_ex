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
    
    public partial class vw_user_organization_service_role
    {
        public System.Guid User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Login_Attempt { get; set; }
        public string Is_Locked { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Code { get; set; }
        public System.Guid Organization_Service_Role_ID { get; set; }
        public string Organization_Service_Role_Code { get; set; }
    }
}
