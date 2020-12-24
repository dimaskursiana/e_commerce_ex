using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_MODEL.ModelData
{
    public class User_Login
    {
        public bool Valid_Login { get; set; }
        public System.Guid User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Full_Name { get; set; }
        public Nullable<short> Login_Attempt { get; set; }
        public DateTime Login_Time { get; set; }
        public Nullable<short> Is_Locked { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Organization_Code { get; set; }
        public System.Guid Organization_Service_Role_ID { get; set; }
        public string Organization_Service_Role_Code { get; set; }

        public virtual ICollection<tbl_Organization_User> tbl_Organization_User { get; set; }
        public virtual ICollection<tbl_User_Role> tbl_User_Role { get; set; }
        public virtual ICollection<tbl_User_Organization_Team> tbl_User_Organization_Team { get; set; }
    }
     
}
