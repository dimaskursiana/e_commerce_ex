using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP_COMMON
{
    public class UIErrorMessage
    {
        #region Private Variable
        private string _strLanguage = HttpContext.Current.Session[GlobalVariable.CONST_SESSION_LANGUAGE] != null ? HttpContext.Current.Session[GlobalVariable.CONST_SESSION_LANGUAGE].ToString() : GlobalVariable.CONST_LANG_EN;
        #endregion

        #region Common
        public string ErrorDBConnection
        { get { return GlobalVariable.CONST_ERR_DB_CONNECTION; } }
        #endregion

        #region User Login
        public string UsernameIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USERNAME_IS_EMPTY, _strLanguage); } }

        public string PasswordIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PASSWORD_IS_EMPTY, _strLanguage); } }

        public string PasswordLength
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PASSWORD_LENGTH, _strLanguage); } }

        public string UserPassIsWrong
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USERPASS_IS_WRONG, _strLanguage); } }

        public string UsernameIsLocked
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USERNAME_IS_LOCKED, _strLanguage); } }
        #endregion

        #region Bank Setting
        public string biBankCode
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BI_BANK_CODE_IS_EMPTY, _strLanguage); } }
        public string bankCode
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_CODE_IS_EMPTY, _strLanguage); } }
        public string bankCodeandbiBankCodeAlreadyExist
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_CODE_AND_BI_BANK_CODE_IS_ALREADY_EXIST, _strLanguage); } }
        public string branchCode
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_EMPTY, _strLanguage); } }
        public string branchCodeAlreadyExist
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_ALREADY_EXIST, _strLanguage); } }
        #endregion

        #region Holiday Calendar
        public string HolidayDateisAlreadyPass
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_PASS, _strLanguage); } }
        public string HolidayDateisEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_IS_EMPTY, _strLanguage); } }
        public string HolidayDateisAlreadyExist
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST, _strLanguage); } }
        public string HolidayDateisAlreadyPayrollCalculation
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_USED_PAYROLL_CALCULATION, _strLanguage); } }
        #endregion

        #region User
        public string UsernameIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_ALREADY_EXISTS, _strLanguage); } }
        public string EmailIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_IS_EMPTY, _strLanguage); } }
        public string StatusCodeIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STATUSCODE_IS_EMPTY, _strLanguage); } }
        public string RoleCodeIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLECODE_IS_EMPTY, _strLanguage); } }
        public string SaveSuccessful
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SAVE_SUCCESSFUL, _strLanguage); } }
        public string SaveFailed
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SAVE_FAILED, _strLanguage); } }
    
        #endregion
         
        #region Team
        public string TeamCodeIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_IS_EMPTY, _strLanguage); } }
        public string TeamIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_ALREADY_EXISTS, _strLanguage); } }
        public string TeamDescriptionIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_DESCRIPTION_IS_EMPTY, _strLanguage); } }
        #endregion
		
		#region RoleMenu
        public string RoleMenuIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLE_MENU_ALREADY_EXISTS, _strLanguage); } }
        #endregion
		
		#region Organization & Menu

        public string ClientOrganizationIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CLIENT_ORGANIZATION_IS_EMPTY, _strLanguage); } }
        public string OrganizationMenuIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_MENU_ALREADY_EXISTS, _strLanguage); } }
        public string MenuIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MENU_IS_EMPTY, _strLanguage); } }
        #endregion

        #region General Parameter
        public string TblNameIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TABLE_NAME_IS_EMPTY, _strLanguage); } }
        public string TblNameIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TABLE_NAME_IS_ALREADY_EXISTS, _strLanguage); } }
        public string FieldNameIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_NAME_IS_EMPTY, _strLanguage); } }
        public string FieldNameIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_NAME_IS_ALREADY_EXISTS, _strLanguage); } }
        public string TblValueIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_VALUE_IS_EMPTY, _strLanguage); } }
       
        #endregion

        #region Cost Center
        public string CostCenterIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_IS_EMPTY, _strLanguage); } }
        public string CostCenterIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_ALREADY_EXISTS, _strLanguage); } }
        #endregion

        #region BPJS
        public string NppNumberIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_IS_EMPTY, _strLanguage); } }
        public string NppNumberIsAlreadyExists
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_ALREADY_EXISTS, _strLanguage); } }
        public string OfficeAdderssIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_ADDRESS_IS_EMPTY, _strLanguage); } }
        public string OfficePhoneIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_PHONE_NUMBER_IS_EMPTY, _strLanguage); } }
        public string RoIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_RO_IS_EMPTY, _strLanguage); } }
        public string PhoneMobileNumberIsEmpty
        { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PHONE_MOBILE_NUMBER_IS_EMPTY, _strLanguage); } }
            
            #region BPJS Manpower
            public string SIPPLoginIDIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SIPP_ONLINE_LOGIN_ID_IS_EMPTY, _strLanguage); } }
            public string SIPPLoginIDAlreadyExists
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SIPP_ONLINE_LOGIN_ID_ALREADY_EXISTS, _strLanguage); } }
            public string SIPPOnlinePasswordIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SIPP_ONLINE_PASSWORD_IS_EMPTY, _strLanguage); } }
            #endregion

            #region BPJS Healthcare
            public string EDabuLoginIDIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_DABU_LOGIN_ID_IS_EMPTY, _strLanguage); } }
            public string EDabuLoginIDAlreadyExists
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_DABU_LOGIN_ALREADY_EXISTS, _strLanguage); } }
            public string EDabuLoginIDPasswordIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_DABU_PASSWORD_IS_EMPTY, _strLanguage); } }
            public string eIDIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_ID_LOGIN_ID_IS_EMPTY, _strLanguage); } }
            public string eIDAlreadyExists
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_ID_LOGIN_ID_ALREADY_EXISTS, _strLanguage); } }
            public string eIDPasswordIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_E_ID_PASSWORD_IS_EMPTY, _strLanguage); } }
            #endregion

        #endregion

        #region Chart Of Account
            public string ChartAccountCodeIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_CODE_IS_EMPTY, _strLanguage); } }
            public string ChartAccountCodeExists
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_CODE_ALREADY_EXISTS, _strLanguage); } }
            public string ChartAccountDescriptionIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_DESCRIPTION_IS_EMPTY, _strLanguage); } }
            public string ChartAccounDbCrIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_DB_CR_IS_EMPTY, _strLanguage); } }
            #endregion

        #region Contact Person
            public string NameIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NAME_IS_EMPTY, _strLanguage); } }
            public string PositionCodeIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_CODE_IS_EMPTY, _strLanguage); } }
            public string PositionDescriptionIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_DESCRIPTION_IS_EMPTY, _strLanguage); } }
       
            #endregion

        #region Organization Structure
            public string OrganizationStructureTypeIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_TYPE_IS_EMPTY, _strLanguage); } }
            public string OrganizationStructureCodeIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_IS_EMPTY, _strLanguage); } }
            public string OrganizationStructureCodeAlreadyExists
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_ALREADY_EXISTS, _strLanguage); } }
            public string OrganizationStructureDescriptionIsEmpty
            { get { return UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_DESCRIPTION_IS_EMPTY, _strLanguage); } }

            #endregion



    }
}