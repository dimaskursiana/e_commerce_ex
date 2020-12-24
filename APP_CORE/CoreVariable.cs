/// --------------------------------------------------------------------------------------------------------------
/// Developer		Version		Date			    Purpose
/// -------------	-------		--------------		--------------------------------------------------------------
/// Herry Sutedja	1.0.0		25 November 2016	Store public variable / constant that commonly used
/// --------------------------------------------------------------------------------------------------------------

using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_CORE
{
    public class CoreVariable
    {
        public void SetUserLogin(User_Data Model, string key)
        { 
            System.Web.HttpContext.Current.Cache[key] = Model;
        }

        #region Global Server Name 
        public static string CONST_GLOBAL_SERVER_NAME = "";
        #endregion

        public User_Data CoreUserLogin()
        {
            User_Data Model = new User_Data()
            {
                Valid_Login = false
            };
            try
            { 
                if (System.Web.HttpContext.Current.Session[CONST_GLOBAL_SERVER_NAME] != null)
                {
                    string key = System.Web.HttpContext.Current.Session[CONST_GLOBAL_SERVER_NAME].ToString();
                    Model = (User_Data)System.Web.HttpContext.Current.Cache[key];
                    if (Model == null)
                    {
                        Model = new User_Data();
                        Model.Valid_Login = false;
                    }
                    else
                    {
                        Model.Valid_Login = true;
                    }
                }
            }
            catch (DbUpdateException E)
            {
                var db = new ModelEntitiesWebsite(); 
                tbl_Error_Log ErrorLog = new tbl_Error_Log()
                {
                    id = Guid.NewGuid(),
                    Created_By = "CORE",
                    Error_Source = "Core Variable",
                    Error_Description = E.ToString(),
                    Log_Date = DateTime.Now
                };
                db = new ModelEntitiesWebsite();
                db.tbl_Error_Log.Add(ErrorLog);
                db.SaveChanges();
                Model = new User_Data();
                Model.Valid_Login = false;
            }
            catch (Exception E)
            {
                var db = new ModelEntitiesWebsite(); 
                tbl_Error_Log ErrorLog = new tbl_Error_Log()
                {
                    id = Guid.NewGuid(),
                    Created_By = "CORE",
                    Error_Source = "Core Variable",
                    Error_Description = E.ToString(),
                    Log_Date = DateTime.Now
                };
                db = new ModelEntitiesWebsite();
                db.tbl_Error_Log.Add(ErrorLog);
                db.SaveChanges();
                Model = new User_Data();
                Model.Valid_Login = false;
            }
            return Model;
        }

        public List<tbl_SysParam> GetStaticParameters()
        {
            List<tbl_SysParam> ListStaticParams = new List<tbl_SysParam>();
            if (System.Web.HttpContext.Current.Session[CONST_GLOBAL_SERVER_NAME] != null)
            {
                string key = System.Web.HttpContext.Current.Session[CONST_GLOBAL_SERVER_NAME].ToString();
                ListStaticParams = (List<tbl_SysParam>)System.Web.HttpContext.Current.Cache[key + "Static_Parameters"]; 
            }
            return ListStaticParams;
        }


        #region Common
        public const string CONST_LANG_EN = "EN";
        public const string CONST_LANG_ID = "ID";
        
        public const string CONST_EDIT = "EDIT";
        public const string CONST_CREATE = "CREATE";
        public const string CONST_UPLOAD = "UPLOAD";

        public const int CONST_STATUS_DELETED = 999;
        public const int CONST_STATUS_ACTIVE = 1;
        public const int CONST_STATUS_INACTIVE = 0;
        public const int CONST_STATUS_REJECT = 2;

        public const int CONST_UPLOAD_REJECTED = 2;
        public const int CONST_UPLOAD_INACTIVE = 0;

        public const string CONST_STR_STATUS_DELETED = "Deleted";
        public const string CONST_STR_STATUS_ACTIVE = "Active";
        public const string CONST_STR_STATUS_REJECT = "Rejected";
        public const string CONST_STR_STATUS_INACTIVE = "Inactive";

        public const string CONST_STR_UPLOAD_REJECTED = "Rejected";
        public const string CONST_STR_UPLOAD_INACTIVE = "Inactive";

        public const int CONST_AUTHORIZED = 1;
        public const int CONST_UNAUTHORIZED = 0;

        public const string CONST_STR_AUTHORIZED = "Authorize";
        public const string CONST_STR_UNAUTHORIZED = "Unauthorize"; 

        public const string CONST_ADMIN = "Service Admin";
        public const string CONST_PAYROL_OUTSOURCING = "Payroll Outsourcing";
        public const string CONST_END_CLIENT = "End Client";

        public const string CONST_HEAD_OFFICE = "Head Office";
        public const string CONST_BRANCH = "Branch Office";

        public const string CONST_POSITIONS = "Positions";
        public const string CONST_DIVISIONS = "Divisions";
        public const string CONST_DEPARTMENTS = "Departments";
        public const string CONST_GRADES = "Grades";
        public const string CONST_EMPLOYEMENT_STATUS_PERMANENT = "Permanent";
        public const string CONST_EMPLOYEMENT_STATUS_NONPERMANENT = "Non Permanent";

        public const string CONST_SUPERVISOR = "Supervisor";
        public const string CONST_STAFF = "Staff";
        public const string CONST_ADMIN_ORGANIZATION = "AdminOrganization";

        public const string CONST_YEARLY = "Yearly";
        public const string CONST_MONTHLY = "Monthly";
        #endregion

        #region User
        public const int CONST_MAX_LOGIN_ATTEMPT = 3;
        public const int CONST_MAX_PASSWORD_LENGTH = 8;
        #endregion

        #region Rule
        public const string CONST_ERR_RULE_06 = "ERR_RULE_06";
        public const string CONST_ERR_RULE_09 = "ERR_RULE_09";
        public const string CONST_ERR_RULE_12 = "ERR_RULE_12";
        public const string CONST_ERR_RULE_04 = "ERR_RULE_04";
        public const string CONST_ERR_RULE_08 = "ERR_RULE_08";
        public const string CONST_ERR_RULE_03 = "ERR_RULE_03";
        public const string CONST_ERR_RULE_07 = "ERR_RULE_07";
        public const string CONST_ERR_RULE_05 = "ERR_RULE_05";
        public const string CONST_ERR_RULE_02 = "ERR_RULE_02";
        public const string CONST_ERR_RULE_00 = "ERR_RULE_00";
        public const string CONST_ERR_RULE_01 = "ERR_RULE_01";
        public const string CONST_ERR_RULE_13 = "ERR_RULE_13";
        public const string CONST_ERR_RULE_10 = "ERR_RULE_10";
        public const string CONST_ERR_RULE_11 = "ERR_RULE_11";
        public const string CONST_ERR_RULE_14 = "ERR_RULE_14"; //One of data already Reject cannot continue process

        #endregion

        #region Rule ESS
        public const string CONST_ERR_RULE_ESS_01 = "ERR_RULE_ESS_01";
        public const string CONST_ERR_RULE_ESS_03 = "ERR_RULE_ESS_03";
        #endregion

    }

}