using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP_MODEL.ModelData;
using System.Data.Entity.Infrastructure;


namespace APP_CORE.GetData
{
    public class Validate
    {
       public static string DeleteUnuthorized(Guid id)
       {
           ModelEntitiesWebsite db = new ModelEntitiesWebsite();
           string deleteStatus = "0";
           string strId = id.ToString();
           var Audit = db.tbl_Audit_Trail.Where(p => p.Record_ID == strId && p.Column_Name == "Authorized_Status" || p.Column_Name == "Authorize_Status").OrderByDescending(p => p.Created_DateTime).FirstOrDefault();
           return deleteStatus;
       }
         
        public static bool ValidApprove(List<int?> ListStatus, List<int?> ListAuthorized,string strLanguage,out string Status)
        {
            Status = "0";
            bool validStatus = true;
           
            if (ListStatus.Contains(CoreVariable.CONST_STATUS_REJECT))
            {
                Status = GetErrorDescription(CoreVariable.CONST_ERR_RULE_14, strLanguage);
                validStatus = false;
            }
            if (ListAuthorized.Contains(CoreVariable.CONST_AUTHORIZED))
            {
                Status = GetErrorDescription(CoreVariable.CONST_ERR_RULE_02, strLanguage);
                validStatus = false;
            } 
             
            return validStatus;
        }

        public static bool ValidReject(List<int?> ListStatus, List<int?> ListAuthorized, string strLanguage, out string Status)
        {
            Status = "0";
            bool validStatus = true;

            if (ListStatus.Contains(CoreVariable.CONST_STATUS_REJECT))
            {
                Status = GetErrorDescription(CoreVariable.CONST_ERR_RULE_14, strLanguage);
                validStatus = false;
            }
            else if (ListAuthorized.Contains(CoreVariable.CONST_AUTHORIZED))
            {
                Status = GetErrorDescription(CoreVariable.CONST_ERR_RULE_02, strLanguage);
                validStatus = false;
            }

            return validStatus;
        }
         

        ///
        public static string GetErrorDescription(string strErrorCode, string strLanguage)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string strErrorDescription = string.Empty;
            string ErrorMessage = string.Empty;

            tbl_Error_Code errorCodeModels = db.tbl_Error_Code.Where(p => p.Error_Code == strErrorCode && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE && p.Error_Language == strLanguage.ToUpper()).FirstOrDefault();
            if (errorCodeModels != null)
            {
                strErrorDescription = errorCodeModels.Error_Description;
            }
            return strErrorDescription;
        }

        public static int? EditRejectStatus(int? StatusCode)
        {
            if (StatusCode == CoreVariable.CONST_STATUS_REJECT)
                StatusCode = CoreVariable.CONST_STATUS_ACTIVE;
            return StatusCode;
        }

        public static string EditRejectStatus(string StatusCode)
        {
            if (StatusCode == CoreVariable.CONST_STR_STATUS_REJECT)
                StatusCode = CoreVariable.CONST_STR_STATUS_ACTIVE;
            return StatusCode;
        }

        public static int? AuthorizeStatusForDeleted(string id,int? AuthorizeStatus)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var CurentVersion = db.tbl_Current_Version.Where(p => p.Record_ID == id).FirstOrDefault();
            if (CurentVersion != null)
            {
                if (AuthorizeStatus == CoreVariable.CONST_UNAUTHORIZED)
                {
                    if (CurentVersion.Curr_Version == 1 || CurentVersion.Curr_Version == null)
                        return CoreVariable.CONST_AUTHORIZED;
                } 
            }
            AuthorizeStatus = CoreVariable.CONST_UNAUTHORIZED;
            return AuthorizeStatus;
        }

        public static int? DeletedReject(string id, int? AuthorizeStatus)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var CurentVersion = db.tbl_Current_Version.Where(p => p.Record_ID == id).FirstOrDefault();
            if (CurentVersion != null)
            {
                if (AuthorizeStatus == CoreVariable.CONST_UNAUTHORIZED)
                {
                    if (CurentVersion.Curr_Version == 1 || CurentVersion.Curr_Version == null)
                        return CoreVariable.CONST_AUTHORIZED;
                }
            }
            AuthorizeStatus = CoreVariable.CONST_UNAUTHORIZED;
            return AuthorizeStatus;
        }
    } 
}
