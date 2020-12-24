using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_MODEL.MetaData
{
    /// <summary>
    /// Created By : Ali Mubarokah
    /// Created Date : 20 Feb 2017
    /// Purpose : Meta Data Class Validation 

    #region Format Partial Class Connector
    // Copy Class Data from EDMX (Variable only)
    #endregion

    #region Funtion Partial Class Data Annotations
    public class META_TBL_MENU
    {
        public System.Guid ID { get; set; }
        [Display(Name = "Menu Name")]
        public string MENU_NAME { get; set; }
        [Display(Name = "Menu Level")]
        public Nullable<int> MENU_LEVEL { get; set; }
        [Display(Name = "Menu Position")]
        public Nullable<int> MENU_POSITION { get; set; }
        [Display(Name = "Parent Id")]
        public Nullable<System.Guid> PARENT_ID { get; set; }
        [Display(Name = "Menu Url")]
        public string MENU_URL { get; set; }
        [Display(Name = "Status Code")]
        public Nullable<int> STATUS_CODE { get; set; }
    }
    #endregion

}
