/// --------------------------------------------------------------------------------------------------------------
/// Developer		Version		Date			    Purpose
/// -------------	-------		--------------		--------------------------------------------------------------
/// Herry Sutedja	1.0.0		25 November 2016	Store public variable / constant that commonly used
/// --------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace APP_COMMON
{
    public class GlobalVariable
    {
        #region Common
        public const string CONST_LANG_EN = "EN";
        public const string CONST_LANG_ID = "ID";
        
        public const string CONST_EDIT = "EDIT";
        public const string CONST_CREATE = "CREATE";
        public const string CONST_UPLOAD = "UPLOAD";

        public const bool CONST_UNLOCK_USER = false;
        public const bool CONST_LOCK_USER = true;

        public const int CONST_STATUS_SUCCES_SAVE = 0;
        public const int CONST_STATUS_FAILED_SAVE = 999;


        public const string CONST_STR_AUTHORIZED = "Authorize";
        public const string CONST_STR_UNAUTHORIZED = "Unauthorize" ;

        public const string CONST_STR_STATUS_DELETED = "Deleted";
        public const string CONST_STR_STATUS_ACTIVE = "Active";
        public const string CONST_STR_STATUS_REJECT = "Rejected";
        public const string CONST_STR_STATUS_INACTIVE = "Inactive";
        
        //Mobile
        public const string CONST_STR_STATUS_PENDING = "Pending";
        public const string CONST_STR_STATUS_APPROVED = "Approved";
        public const string CONST_STR_STATUS_CANCEL = "Cancel";
        //public const string CONST_STR_STATUS_REJECT = "Rejected";

        public const string CONST_STR_UPLOAD_REJECTED = "Rejected";
        public const string CONST_STR_UPLOAD_INACTIVE = "Inactive";

        public const int CONST_STATUS_DELETED = 999;
        public const int CONST_STATUS_ACTIVE = 1;
        public const int CONST_STATUS_INACTIVE = 0;
        public const int CONST_STATUS_REJECT = 2;

        public const int CONST_FUNC_FAILED = -1;
        public const int CONST_FUNC_SUCCESS = 0;

        public const string CONST_CALCULATION_FIX_AMOUNT_TYPE = "FX";

        public const int CONST_CALCULATION_STATUS_IN_PROGRESS = 0;
        public const int CONST_CALCULATION_STATUS_SUCCESS = 1;
        public const int CONST_CALCULATION_STATUS_FAIL = 2;

        public const string CONST_CALCULATION_STR_STATUS_IN_PROGRESS = "In Progress";
        public const string CONST_CALCULATION_STR_STATUS_SUCCESS = "Success";
        public const string CONST_CALCULATION_STR_STATUS_FAIL = "Fail";

        public const int CONST_UPLOAD_REJECTED = 2;
        public const int CONST_UPLOAD_INACTIVE = 0;
         
        public const string CONST_UPLOAD_ATTENDANCE_STR_STATUS_IN_PROGRESS = "In Progress";
        public const string CONST_UPLOAD_ATTENDANCE_STR_STATUS_COMPLETED = "Completed";
        public const string CONST_UPLOAD_ATTENDANCE_STR_STATUS_FAIL = "Fail";
        public const string CONST_UPLOAD_ATTENDANCE_STR_STATUS_INVALID_TEMPLATE = "Invalid Template";

        public const int CONST_UPLOAD_ATTENDANCE_STATUS_IN_PROGRESS = 0;
        public const int CONST_UPLOAD_ATTENDANCE_STATUS_SUCCESS = 1;
        public const int CONST_UPLOAD_ATTENDANCE_STATUS_FAILED = 2;
        public const int CONST_UPLOAD_ATTENDANCE_INVALID_TEMPLATE = 3;

        public const int CONST_AUTHORIZED = 1;
        public const int CONST_UNAUTHORIZED = 0;

        public const int CONST_UPLOAD_TYPE_INSERT = 0;
        public const int CONST_UPLOAD_TYPE_APPROVE_OR_REJECT = 1; 
        public const int CONST_UPLOAD_TYPE_DELETE = 2;

        public const string CONST_ADMIN = "Service Admin";
        public const string CONST_PAYROL_OUTSOURCING = "Payroll Outsourcing";
        public const string CONST_END_CLIENT = "End Client";

        public const string CONST_HEAD_OFFICE = "Head Office";
        public const string CONST_BRANCH = "Branch Office";

        public const string CONST_ID_TYPE_KTP = "KTP";
        public const string CONST_ID_TYPE_KK = "KK";
        public const string CONST_ID_TYPE_SIM = "SIM";
        public const string CONST_ID_TYPE_PASSPORT = "PASSPORT";
        public const string CONST_ID_TYPE_KITAS = "KITAS";

        public const string CONST_ID_TYPE_NATIONALITY_INDONESIA = "INDONESIA";

        public const string CONST_POSITIONS = "Positions";
        public const string CONST_DIVISIONS = "Divisions";
        public const string CONST_DEPARTMENTS = "Departments";
        public const string CONST_GRADES = "Grades";
        public const string CONST_EMPLOYEE_STATUS = "Leave Of Absent (LOA)";
        public const string CONST_EMPLOYEE_STATUS_ACTIVE = "Active";
        public const string CONST_EMPLOYMENT_STATUS_PROBATION = "Probation";
        public const string CONST_EMPLOYMENT_STATUS_CONTRACT = "Contract";

        public const string CONST_PERMANENT = "Permanent - Permanent";
        public const string CONST_CONTRACT = "Permanent - Contract";
        public const string CONST_PROBATION = "Permanent - Probation";
        public const string CONST_PERMANENT_EXPATRIATE_PERMANENT = "Permanent - Expatriate Permanent";
        public const string CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI = "Non Permanent - Wajib Pajak Luar Negeri";
        public const string CONST_NON_PERMANENT_KOMISARIS = "Non Permanent - Komisaris";
        public const string CONST_NON_PERMANENT_TENAGA_AHLI = "Non Permanent - Tenaga Ahli";
        public const string CONST_NON_PERMANENT_MANTAN_PEGAWAI = "Non Permanent - Mantan Pegawai";
        public const string CONST_NON_PERMANENT_BUKAN_PEGAWAI_BERKESINAMBUNGAN = "Non Permanent - Bukan Pegawai yang Berkesinambungan";
        public const string CONST_NON_PERMANENT_BUKAN_PEGAWAI_TIDAK_BERKESINAMBUNGAN = "Non Permanent - Bukan Pegawai yang Tidak Berkesinambungan";
        
        public const string CONST_EMPLOYEMENT_STATUS_PERMANENT="Permanent";
        public const string CONST_EMPLOYEMENT_STATUS_NONPERMANENT = "Non Permanent";
        public const string CONST_SEVERANCE = "Severance";
        
        //Additional Payroll
        public const string CONST_UPD_ADDITIONAL_PERMANENT = "AdditionalPayrollPermanent";
        public const string CONST_UPD_ADDITIONAL_NON_PERMANENT = "AdditionalPayrollNonPermanent";
      
        public const string CONST_PAYSLIP_DISTIBUTION_HARDCOPY = "Hardcopy";
        public const string CONST_PAYSLIP_DISTIBUTION_EMAIL = "Email";
        public const string CONST_PAYSLIP_DISTIBUTION_EPAYSLIP = "e-Payslip";

        public const string CONST_PAYSLIP_STATUS_IN_PROGRESS = "In Progress";
        public const string CONST_PAYSLIP_STATUS_SUCCEED = "Succeed";
        public const string CONST_PAYSLIP_STATUS_FAILED = "Failed";
        public const string CONST_PAYSLIP_STATUS_TRANSFERRED = "Transferred";
      
        public const string CONST_ERR_RULES_NOTFOUND =  "ERR_RULES_NOTFOUND";
       public const string CONST_ERR_NOTFOUND_PROMOTION_CODE = "ERR_NOTFOUND_PROMOTION_CODE";

        public const string CONST_RUN_CLOSING_CODE = "Permanent";
        public const string CONST_TAX_PERIOD_CLOSING_CODE = "NonPermanent";
      
  	  	public const string CONST_ERR_FIELD_IS_MANDATORY = "ERR_FIELD_IS_MANDATORY";

        public const string CONST_WAJIB_PAJAK_21_100_01 = "21-100-01";
        public const string CONST_WAJIB_PAJAK_21_100_02 = "21-100-02";
        public const string CONST_WAJIB_PAJAK_21_100_03 = "21-100-03";
        public const string CONST_WAJIB_PAJAK_21_100_04 = "21-100-04";
        public const string CONST_WAJIB_PAJAK_21_100_05 = "21-100-05";
        public const string CONST_WAJIB_PAJAK_21_100_06 = "21-100-06";
        public const string CONST_WAJIB_PAJAK_21_100_07 = "21-100-07";
        public const string CONST_WAJIB_PAJAK_21_100_08 = "21-100-08";
        public const string CONST_WAJIB_PAJAK_21_100_09 = "21-100-09";
        public const string CONST_WAJIB_PAJAK_21_100_10 = "21-100-10";
        public const string CONST_WAJIB_PAJAK_21_100_11 = "21-100-11";
        public const string CONST_WAJIB_PAJAK_21_100_12 = "21-100-12";
        public const string CONST_WAJIB_PAJAK_21_100_13 = "21-100-13";
        public const string CONST_WAJIB_PAJAK_21_100_99 = "21-100-99";
        public const string CONST_WAJIB_PAJAK_21_401_01 = "21-401-01";
        public const string CONST_WAJIB_PAJAK_21_401_02 = "21-401-02";
        public const string CONST_WAJIB_PAJAK_21_402_01 = "21-402-01";
        public const string CONST_WAJIB_PAJAK_21_499_99 = "21-499-99";
        public const string CONST_WAJIB_PAJAK_27_100_99 = "27-100-99";

        public const string CONST_SYSTEM_TIMEOUT = "ERR_SYSTEM_TIMEOUT";
        public const string CONST_SYSTEM_INVALID_USER = "ERR_SYSTEM_INVALID_USER";
        public const string CONST_SYSTEM_INVALID_USERPASSWORD = "ERR_SYSTEM_INVALID_USERPASSWORD";
        public const string CONST_SYSTEM_INVALID_USERINACTIVE = "ERR_SYSTEM_INVALID_USERINACTIVE";
        public const string CONST_SYSTEM_INVALID_USER_N_PASSWORD = "ERR_SYSTEM_INVALID_USER_N_PASSWORD";
        public const string CONST_SYSTEM_INVALID_TRIAL_DAYS = "ERR_SYSTEM_INVALID_TRIAL_DAYS";

        public const string CONST_MENU_MODULE_MOBILE_LEAVE = "LEAVE";
        public const string CONST_MENU_MODULE_MOBILE_CLAIM = "CLAIM";
        public const string CONST_MENU_MODULE_MOBILE_PAYSLIP = "PAYSLIP";
        public const string CONST_MENU_MODULE_MOBILE_ATTENDANCE = "ATTENDANCE";
        public const string CONST_MENU_MODULE_MOBILE_DOWNLOAD = "DOWNLOAD";

        public const string CONST_MENU_MOBILE_DETAILSLEAVEAPPROVE = "DetailsLeaveApprove";
        public const string CONST_MENU_MOBILE_DETAILMYLEAVE = "DetailMyLeave";
        public const string CONST_MENU_MOBILE_EDITLEAVEAPPROVE = "EditLeaveApprove";
        public const string CONST_MENU_MOBILE_CANCELLEAVEAPPROVE = "CancelLeaveApprove";
        public const string CONST_MENU_MOBILE_DETAILMYCLAIM = "DetailMyClaim";
        public const string CONST_MENU_MOBILE_DETAILSCLAIMAPPROVE = "DetailsClaimApprove";
        public const string CONST_MENU_MOBILE_EDITCLAIMAPPROVE = "EditClaimApprove";
        public const string CONST_MENU_MOBILE_CANCELCLAIMAPPROVE = "CancelClaimAppove";
        public const string CONST_MENU_MOBILE_DETAILMYOVERTIME = "DetailMyOvertime";
        public const string CONST_MENU_MOBILE_DETAILSOVERTIMEAPPROVE = "DetailsOvertimeApprove";
        public const string CONST_MENU_MOBILE_EDITOVERTIMEAPPROVE = "EditOvertimeApprove";
        public const string CONST_MENU_MOBILE_CANCELOVERTIMEAPPROVE = "CancelOvertimeAppove";
        public const string CONST_MENU_MOBILE_PAYSLIP = "Payslip";
        public const string CONST_MENU_MOBILE_ATTENDANCE = "Attendance";
        public const string CONST_MENU_MOBILE_DOWNLOAD = "Download";
        #endregion

        #region User
        public const int CONST_MAX_LOGIN_ATTEMPT = 3;
        public const int CONST_MAX_PASSWORD_LENGTH = 8;
        public const string CONST_ERR_DELETED_USER_LOGIN = "ERR_DELETED_USER_LOGIN";
        #endregion

        #region Error Code
        public const string CONST_STR_SYSTEM = "SYSTEM";
        public const string CONST_STR_TRIAL = "TRIAL";

        #region User Trial
        public const string CONST_PARAM_PASSOWRD_TRIAL = "DEF_PASSWORD_USER_TRIAL";
        #endregion

        #region Bank Information
        public const string CONST_ERR_EMP_PAYMENT_INFORMATION_PAYMENT_TYPE_NOT_VALID = "ERR_EMP_PAYMENT_INFORMATION_PAYMENT_TYPE_NOT_VALID ";  //Please Select Bank
        public const string CONST_ERR_EMP_PAYMENT_INFORMATION_PAYMENT_TYPE_EXIST = "ERR_EMP_PAYMENT_INFORMATION_PAYMENT_TYPE_EXIST ";  //Please Input Account Number is Empty
        public const string CONST_ERR_EMP_PAYMENT_INFORMATION_CURRENCY_NOT_VALID = "ERR_EMP_PAYMENT_INFORMATION_CURRENCY_NOT_VALID ";  // Account Number Already Exist
        public const string CONST_ERR_EMP_PAYMENT_INFORMATION_LIST_NOT_VALID = "ERR_EMP_PAYMENT_INFORMATION_LIST_NOT_VALID "; // Please input Account Number with valid number
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
        public const string CONST_ERR_RULE_15 = "ERR_RULE_15";
        public const string CONST_ERR_RULE_16 = "ERR_RULE_16";
        #endregion

        #region Rule ESS
        public const string CONST_ERR_RULE_ESS_01 = "ERR_RULE_ESS_01";
        public const string CONST_ERR_RULE_ESS_03 = "ERR_RULE_ESS_03";
        #endregion

        #region Common
        public const string CONST_ERR_DB_CONNECTION = "Error when connect to database";
        public const string CONST_ERR_TIMEOUT = "408"; 
        public const string CONST_ERR_STATUSCODE_IS_EMPTY = "ERR_STATUSCODE_IS_EMPTY";  //Status is Empty! Please Select Status!
        #endregion

        #region User Login
        public const string CONST_ERR_USERNAME_IS_EMPTY = "ERR_USERNAME_IS_EMPTY";  //Please Input Username
        public const string CONST_ERR_PASSWORD_IS_EMPTY = "ERR_PASSWORD_IS_EMPTY"; //Please Input Password
        public const string CONST_ERR_PASSWORD_LENGTH = "ERR_PASSWORD_LENGTH"; //The Password must be at least 8 characters long
        public const string CONST_ERR_USERPASS_IS_WRONG = "ERR_USERPASS_IS_WRONG";  //Username or Password is wrong or inactive
        public const string CONST_ERR_USERNAME_IS_LOCKED = "ERR_USERNAME_IS_LOCKED";  //Username is locked! Please contact administrator!
        #endregion

        #region Bank Setting
        public const string CONST_ERR_BI_BANK_CODE_IS_EMPTY = "ERR_BI_BANK_CODE_IS_EMPTY";
        public const string CONST_ERR_BANK_CODE_IS_EMPTY = "ERR_BANK_CODE_IS_EMPTY";
        public const string CONST_ERR_BANK_CODE_AND_BI_BANK_CODE_IS_ALREADY_EXIST = "ERR_BANK_CODE_AND_BI_BANK_CODE_IS_ALREADY_EXIST";
        public const string CONST_ERR_BI_BANK_CODE_IS_NOT_NUMERIC = "ERR_BI_BANK_CODE_IS_NOT_NUMERIC";
        public const string CONST_ERR_BRANCH_CODE_IS_EMPTY = "ERR_BRANCH_CODE_IS_EMPTY";
        public const string CONST_ERR_BRANCH_CODE_IS_NOT_NUMERIC = "ERR_BRANCH_CODE_IS_NOT_NUMERIC";
        public const string CONST_ERR_BRANCH_CODE_IS_ALREADY_EXIST = "ERR_BRANCH_CODE_IS_ALREADY_EXIST";
        #endregion

        #region Exchange Rate
        public const string CONST_ERR_EFFECTIVE_DATE_ALREADY_EXISTS = "ERR_EFFECTIVE_DATE_ALREADY_EXISTS";
        public const string CONST_ERR_EFFECTIVE_DATE_NOT_VALID = "ERR_EFFECTIVE_DATE_NOT_VALID";
        public const string CONST_ERR_EC_EFFECTIVE_DATE_IS_EMPTY = "ERR_EFFECTIVE_DATE_IS_EMPTY"; // Please Input Effective Date
        public const string CONST_ERR_EC_C_FROM_IS_EMPTY = "ERR_EC_C_FROM_IS_EMPTY"; // Currency From Can't Empty
        public const string CONST_ERR_EC_C_TO_IS_EMPTY = "ERR_EC_C_TO_IS_EMPTY"; // Currency To Can't Empty
        public const string CONST_ERR_EC_RATE_IS_EMPTY = "ERR_EC_RATE_IS_EMPTY"; // Rate Can't Empty
        public const string CONST_ERR_ECF_ECT_SAME = "ERR_ECF_ECT_SAME"; // Currency From & Currency To can't Same
        public const string CONST_ERR_EC_IS_ALREADY_EXIST = "ERR_EC_IS_ALREADY_EXIST";// Currency From And Currency To For Effective Date Already Exist
        #endregion

        #region Personal Information

        public const string CONST_ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST = "ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST";
        public const string CONST_ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY = "ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY";
        public const string CONST_ERR_FULL_NAME_IS_EMPTY = "ERR_FULL_NAME_IS_EMPTY";
        public const string CONST_ERR_GENDER_IS_EMPTY = "ERR_GENDER_IS_EMPTY";
        public const string CONST_ERR_NATIONALITY_IS_EMPTY = "ERR_NATIONALITY_IS_EMPTY";
        public const string CONST_ERR_IDNUMBER_IS_EMPTY = "ERR_IDNUMBER_IS_EMPTY";
        public const string CONST_ERR_DUPLICATE_IDTYPE = "ERR_DUPLICATE_IDTYPE";
        public const string CONST_ERR_IDTYPE_IS_EMPTY = "ERR_IDTYPE_IS_EMPTY";
        public const string CONST_ERR_IDTYPE_KTP_IS_MANDATORY = "ERR_IDTYPE_KTP_IS_MANDATORY";
        public const string CONST_ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY = "ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY";
        public const string CONST_ERR_DUPLICATE_RELATIONSHIP = "ERR_DUPLICATE_RELATIONSHIP";
        public const string CONST_ERR_EMPLOYEE_HAVE_CALCULATION_OR_LOAN_OR_BENEFIT = "ERR_EMPLOYEE_HAVE_CALCULATION_OR_LOAN_OR_BENEFIT";
        public const string CONST_ERR_DUPLICATE_FINGER_ID = "ERR_DUPLICATE_FINGER_ID";

        #endregion

        #region Organization Group

        public const string CONST_ERR_ORG_GROUP_ORGANIZATION_EMPTY = "ERR_ORG_GROUP_ORGANIZATION_EMPTY";
        public const string CONST_ERR_ORG_GROUP_RELATIONSHIP_EMPTY = "ERR_ORG_GROUP_RELATIONSHIP_EMPTY";
        public const string CONST_ERR_ORG_GROUP_ALREADY_HOLDING = "ERR_ORG_GROUP_ALREADY_HOLDING";
        public const string CONST_ERR_ORG_GROUP_HOLDING_MORE_ONE = "ERR_ORG_GROUP_HOLDING_MORE_ONE";
        public const string CONST_ERR_ORG_GROUP_MIN_ORGANIZQATION = "ERR_ORG_GROUP_MIN_ORGANIZQATION";

        #endregion

        #region Appointment Information

        public const string CONST_ERR_DATE_OF_HIRE_IS_EMPTY = "ERR_DATE_OF_HIRE_IS_EMPTY";
        public const string CONST_ERR_JOIN_DATE_IS_EMPTY = "ERR_JOIN_DATE_IS_EMPTY";
        public const string CONST_ERR_JOIN_DATE_IS_NOT_VALID = "ERR_JOIN_DATE_IS_NOT_VALID";
        public const string CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY = "ERR_EMPLOYMENT_STATUS_IS_EMPTY";
        public const string CONST_ERR_EMPLOYEE_STATUS_IS_EMPTY = "ERR_EMPLOYEE_STATUS_IS_EMPTY";
        public const string CONST_ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID = "ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID";
        public const string CONST_ERR_EMPLOYMENT_STATUS_ISNOT_ENTITLED_TO_HAVE_TAX_STATUS = "ERR_EMPLOYMENT_STATUS_ISNOT_ENTITLED_TO_HAVE_TAX_STATUS";
        public const string CONST_ERR_EMPLOYEE_STATUS_IS_NOT_VALID = "ERR_EMPLOYEE_STATUS_IS_NOT_VALID";
        public const string CONST_ERR_STATUS_INFO_EFF_DATE_IS_EMPTY = "ERR_STATUS_INFO_EFF_DATE_IS_EMPTY";
        public const string CONST_ERR_STATUS_INFO_IS_ALREADY_EXIST = "ERR_STATUS_INFO_IS_ALREADY_EXIST";
        public const string CONST_ERR_WORK_LOCATION_IS_EMPTY = "ERR_WORK_LOCATION_IS_EMPTY";
        public const string CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY = "ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY";
        public const string CONST_ERR_WORKING_TIME_IS_EMPTY = "ERR_WORKING_TIME_IS_EMPTY";
        public const string CONST_ERR_CONTRACT_END_DATE_IS_NOT_VALID = "ERR_CONTRACT_END_DATE_IS_NOT_VALID";
        public const string CONST_ERR_WORKING_TIME_END_DATE_IS_NOT_VALID = "ERR_WORKING_TIME_END_DATE_IS_NOT_VALID";
        public const string CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID = "ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID";
        public const string CONST_ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_END_DATE_OF_PROBATION_IS_NOT_VALID = "ERR_END_DATE_OF_PROBATION_IS_NOT_VALID";
        public const string CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID = "ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID";
        public const string CONST_ERR_CONTRACT_DATE_IS_NOT_VALID = "ERR_CONTRACT_DATE_IS_NOT_VALID";
        public const string CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID = "ERR_WORKING_TIME_START_DATE_IS_NOT_VALID";
        public const string CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID = "ERR_POSITION_EFF_DATE_IS_NOT_VALID";
        public const string CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID = "ERR_DIVISION_EFF_DATE_IS_NOT_VALID";
        public const string CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID = "ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID";
        public const string CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID = "ERR_GRADE_EFF_DATE_IS_NOT_VALID";
        public const string CONST_CHEECK_TAX_PERIOD = "CONST_CHEECK_TAX_PERIOD";
        public const string CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA = "ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA";
        public const string CONST_ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY = "ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY = "ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY = "ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY = "ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY = "ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY = "ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY = "ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY = "ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY = "ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY = "ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY";
        public const string CONST_ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME = "ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME";
        public const string CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY = "ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY";

        
        #endregion

        #region Tax Information

        public const string CONST_ERR_TAX_STATUS_IS_EMPTY = "ERR_TAX_STATUS_IS_EMPTY";
        public const string CONST_ERR_MUST_BE_CREATE_APPOINTMENT = "ERR_MUST_BE_CREATE_APPOINTMENT";
        public const string CONST_ERR_YEAR_STATUS_IS_EMPTY = "ERR_YEAR_STATUS_IS_EMPTY";
        public const string CONST_ERR_SALARY_TAX_POLICY_IS_EMPTY = "ERR_SALARY_TAX_POLICY_IS_EMPTY";
        public const string CONST_ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR = "ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR";
        public const string CONST_ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI = "ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI";
        public const string CONST_ERR_TAX_STATUS_CANNOT_BE_ASSIGNED = "ERR_TAX_STATUS_CANNOT_BE_ASSIGNED";
        public const string CONST_ERR_APPOINMENT_NOT_VALID = "ERR_APP_EMPLOYEE_STATUS_IS_NOT_VALID";
        public const string CONST_ERR_EFFECTIVE_DATE_MUST_BE_EMPTY = "ERR_EFFECTIVE_DATE_MUST_BE_EMPTY";
        public const string CONST_ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE = "ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE";


        #endregion

        #region Address Contact Information

        public const string CONST_ERR_LEGAL_ADDRESS_IS_EMPTY = "ERR_LEGAL_ADDRESS_IS_EMPTY";
        public const string CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST = "ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST";
        public const string CONST_ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE = "ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE";

        #endregion

        #region Holiday Calendar
        public const string CONST_ERR_HOLIDAY_DATE_IS_NOT_AVAILABLE = "ERR_HOLIDAY_DATE_IS_NOT_AVAILABLE";
        public const string CONST_ERR_HOLIDAY_DATE_ALREADY_PASS = "ERR_HOLIDAY_DATE_ALREADY_PASS";
        public const string CONST_ERR_HOLIDAY_DATE_IS_EMPTY = "ERR_HOLIDAY_DATE_IS_EMPTY";
        public const string CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST = "ERR_HOLIDAY_DATE_ALREADY_EXIST";
        public const string CONST_ERR_HOLIDAY_DATE_ALREADY_USED_PAYROLL_CALCULATION = "ERR_HOLIDAY_DATE_ALREADY_USED_PAYROLL_CALCULATION";
        #endregion

        #region Payroll Calculation
        public const string CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_EMPTY = "ERR_CALCULATION_SETUP_PAYROLL_PERIOD_EMPTY"; //Please select payroll period
        public const string CONST_ERR_CALCULATION_SETUP_MUST_EDIT = "ERR_CALCULATION_SETUP_MUST_EDIT";
        public const string CONST_ERR_CALCULATION_NOT_ALLOWED = "E3_CPC00001";
        public const string CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_CLOSED = "ERR_CALCULATION_SETUP_PAYROLL_PERIOD_CLOSED"; //Payroll Period Already Closed
        public const string CONST_ERR_CALCULATION_SETUP_NOT_SEVERANCE = "ERR_CALCULATION_SETUP_NOT_SEVERANCE";
        public const string CONST_ERR_CALCULATION_SETUP_PAYROLL_HAS_APPROVED = "ERR_CALCULATION_SETUP_PAYROLL_HAS_APPROVED"; //Payroll Period Already Closed
        public const string CONST_ERR_CALCULATION_SETUP_MUST_CLOSED_BEFORE = "ERR_CALCULATION_SETUP_MUST_CLOSED_BEFORE"; //Payroll Period Already Closed
        public const string CONST_ERR_CALCULATION_EMPLOYEE_HAS_PEMBETULAN = "ERR_CALCULATION_EMPLOYEE_HAS_PEMBETULAN"; //Payroll Period Already Closed
        public const string CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_ALREADY_RUNING = "E1_CPC00001"; //Period and run already closed’
        public const string CONST_ERR_CALCULATION_FAILED_APPROVE = "ERR_CALCULATION_FAILED_APPROVE"; //Period and run already closed’
        public const string CONST_ERR_CALCULATION_SETUP_PAYROLL_SEVERENCE_ALREADY_PROGRESS = "ERR_SEVERENCE_ALREADY_PROGRESS"; //Period and run already closed’
        public const string CONST_ERR_CALCULATION_SETUP_RUN_EMPTY = "ERR_CALCULATION_SETUP_RUN_EMPTY"; //Please Input Valid Run
        public const string CONST_ERR_CALCULATION_SETUP_EMPLOYEE_EMPTY = "ERR_CALCULATION_SETUP_EMPLOYEE_EMPTY"; //Please Include Employee 
        public const string CONST_ERR_CALCULATION_SETUP_COMPONENT_EMPTY = "ERR_CALCULATION_SETUP_COMPONENT_EMPTY"; //Please Include Component 
        public const string CONST_ERR_CALCULATION_SETUP_EXCHANGE_RATE_MISSING = "E2_CPC00001"; //Payroll Period Already Closed
        public const string CONST_ERR_PTKP_NOT_CHANGED = "ERR_PTKP_NOT_CHANGE"; //Please Include Component 
        public const string CONST_ERR_EMPLOYEE_EMAIL_EMPTY = "ERR_EMPLOYEE_EMAIL_EMPTY"; //Corporate email is Empty 
     

        #endregion
          
        #region Role Menu
        public const string CONST_ERR_ROLE_MENU_ALREADY_EXISTS = "ERR_ROLE_MENU_ALREADY_EXISTS"; //Role Menu is Already Exists
        #endregion

        #region Organization Menu
        public const string CONST_ERR_CLIENT_ORGANIZATION_IS_EMPTY = "ERR_CLIENT_ORGANIZATION_IS_EMPTY"; //Please Select Client Organization
        public const string CONST_ERR_ORGANIZATION_MENU_ALREADY_EXISTS = "ERR_ORGANIZATION_MENU_ALREADY_EXISTS"; //Organization Menu is Already Exists
        public const string CONST_ERR_MENU_IS_EMPTY = "ERR_MENU_IS_EMPTY"; //Please Select Menu
        #endregion

        #region Field Name
        #region Common
        public const string CONST_FIELD_ID = "id";
        #endregion

        #region User
        public const string CONST_FIELD_USER_ID = "User_ID";
        public const string CONST_FIELD_USERNAME = "Username";
        public const string CONST_FIELD_USER_FULLNAME = "Full_Name";
        #endregion

        #region Organization
        public const string CONST_FIELD_ORGANIZATION_ID = "Organization_ID";
        public const string CONST_FIELD_ORGANIZATION_CODE = "Organization_Code";
        #endregion

        #region Organization Service Role
        public const string CONST_FIELD_ORGANIZATION_SERVICE_ROLE_CODE = "Organization_Service_Role_Code";
        #endregion
        #endregion

        #region Session
        public const string CONST_SESSION_LANGUAGE = "SelectedLanguage";
        public const string CONST_SESSION_USERLOGIN = "UserLoginData";
        public const string CONST_SESSION_ORGANIZATION_CLIENT_SELECTED = "SelectedOrganizationAndClientID";
        public const string CONST_SESSION_ORGANIZATION_CLIENT_LIST = "ListOrganizationAndClient";
        #endregion

        #region Data
        public const string CONST_DATA_SYSADMIN = "SYSADMIN";
        public const string CONST_DATA_SVCPROVIDER = "SVCPROVIDER";
        public const string CONST_DATA_ENDUSER = "ENDUSER";

        public const string ERR_IN_PROGRESS = "ERR_STATUS_INPROGRESS";
        public const string ERR_UPLOAD_ON_PROGRESS = "ERR_UPLOAD_ON_PROGRESS";
        public const string ERR_UPLOAD_ON_INVALID = "ERR_UPLOAD_ON_INVALID";

        #endregion

        #region tbl_Organization (Service)
        public const string CONST_ORGANIZATION_SERIVCE_SYSADMIN = "Service Admin";
        public const string CONST_ORGANIZATION_SERIVCE_OUTCORSING = "Payroll Outsourcing";
        public const string CONST_ORGANIZATION_SERIVCE_END = "End Client";
        #endregion

        #region Contact Team
        public const string CONST_ERR_TEAM_CODE_IS_EMPTY = "ERR_TEAM_CODE_IS_EMPTY";  //Please Input Team Code
        public const string CONST_ERR_TEAM_CODE_ALREADY_EXISTS = "ERR_TEAM_CODE_ALREADY_EXISTS";  //Team Code Already Exists
        public const string CONST_ERR_TEAM_DESCRIPTION_IS_EMPTY = "ERR_TEAM_DESCRIPTION_IS_EMPTY";  //Please Input Team Descrription
        #endregion

        #region User
        public const string CONST_ERR_EMAIL_IS_EMPTY = "ERR_EMAIL_IS_EMPTY";  //Please Input Email
        public const string CONST_ERR_EMAIL_ALREADY_EXISTS = "ERR_EMAIL_ALREADY_EXISTS";  //Email Already Exists
        public const string CONST_ERR_USER_INVALID_EMAIL = "ERR_USER_INVALID_EMAIL";  //Invallid Email
        public const string CONST_ERR_ROLECODE_IS_EMPTY = "ERR_ROLECODE_IS_EMPTY";  //Please Select Role
        public const string CONST_ERR_USER_ALREADY_EXISTS = "ERR_USER_ALREADY_EXISTS";  //Username is Already Exists
        public const string CONST_ERR_USER_INVALID_USERNAME = "ERR_USER_INVALID_USERNAME";  //Invallid Username
        public const string CONST_ERR_SAVE_SUCCESSFUL = "ERR_SAVE_SUCCESSFUL"; //Save is Successful
        public const string CONST_ERR_SAVE_FAILED = "ERR_SAVE_FAILED"; //Save Failed
        #endregion

        #region Geeneral Parameter
        public const string CONST_ERR_TABLE_NAME_IS_EMPTY = "ERR_TABLE_NAME_IS_EMPTY"; //Please input Kategori
        public const string CONST_ERR_TABLE_NAME_IS_ALREADY_EXISTS = "ERR_TABLE_NAME_IS_ALREADY_EXISTS"; //Kategori Already Exists

        public const string CONST_ERR_FIELD_NAME_IS_EMPTY = "ERR_FIELD_NAME_IS_EMPTY"; //Please input Field Name
        public const string CONST_ERR_FIELD_NAME_IS_ALREADY_EXISTS = "ERR_FIELD_NAME_IS_ALREADY_EXISTS"; //Field Name Already Exists

        public const string CONST_ERR_FIELD_VALUE_IS_EMPTY = "ERR_FIELD_VALUE_IS_EMPTY"; //Field Value Is Empty
        public const string CONST_ERR_FIELD_VALUE_IS_ALREADY_EXISTS = "ERR_FIELD_VALUE_IS_ALREADY_EXISTS"; //Value and Name Already Exists

        public const string CONST_ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS = "ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS"; //Value and Name Already Exists

        #endregion

        #region Geeneral Parameter

        public const string CONST_ERR_LEAVE_CODE_IS_EMPTY = "ERR_LEAVE_CODE_IS_EMPTY"; //Code Is Empty
        public const string CONST_ERR_LEAVE_DESCRIPTION_IS_EMPTY = "ERR_LEAVE_DESCRIPTION_IS_EMPTY"; //Description Is Empty
        public const string CONST_ERR_LEAVE_CODE_IS_ALREADY_EXISTS = "ERR_LEAVE_CODE_IS_ALREADY_EXISTS"; //Code Already Exists

        #endregion

        #region Payroll Variable
        public const string ERR_PAYROLL_VARIABLE_PERIOD_EMPTY = "ERR_PAYROLL_VARIABLE_PERIOD_EMPTY";  //Sorry, Payroll Peroid Must be Create
        public const string ERR_PAYROLL_VARIABLE_EXIST = "ERR_PAYROLL_VARIABLE_EXIST"; //Payroll Variable Data exist
        public const string ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY = "ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY"; //Sorry,Employee Must be Select
         public const string ERR_PAYROLL_VARIABLE_DATA_INVALID="ERR_PAYROLL_VARIABLE_DATA_INVALID"; //Data is Invalid
       public const string ERR_PAYROLL_VARIABLE_DUPLICATE="ERR_PAYROLL_VARIABLE_DUPLICATE";//Duplicate Payroll Variable
       public const string ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL = "ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL"; //Sorry, Variable Value Must be Fill
       public const string ERR_CREATE_WORKING_TIME = "ERR_CREATE_WORKING_TIME"; //Please Create Working Time
       public const string ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL = "ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL"; //Sorry,  Working Day or Overtime Day or Absent Day or Total Overtime Must be fill
        public const string ERR_PAYROLL_VARIABLE_SELECTED_EMPTY = "ERR_PAYROLL_VARIABLE_SELECTED_EMPTY"; //Variable must be selected if the Value is filled
        #endregion

        #region Additional payroll
        public const string ERR_ADDITIONAL_PAYROLL = "ERR_ADDITIONAL_PAYROLL";
       public const string ERR_ADDITIONAL_PAYROLL_DUPLICATE = "ERR_ADDITIONAL_PAYROLL_DUPLICATE"; //Duplicate Additional Payroll
       public const string ERR_ADDITIONAL_PAYROLL_EXIST = "ERR_ADDITIONAL_PAYROLL_EXIST"; //Additional Payroll Data exist
         public const string ERR_ADDITIONAL_PAYROLL_EXIST_GO_EDIT="ERR_ADDITIONAL_PAYROLL_EXIST_GO_EDIT";//Employee data exist. Please go to Edit
         public const string ERR_ADDITIONAL_PAYROLL_MONTH_DIFF = "ERR_ADDITIONAL_PAYROLL_MONTH_DIFF";//Transaction date is invalid. Only one tax period month is allowed
         public const string ERR_ADDITIONAL_FUTURE_DATE = "ERR_ADDITIONAL_FUTURE_DATE";//Future date transaction is not allowed
         public const string ERR_ADDITIONAL_TRANS_DATE = "ERR_ADDITIONAL_TRANS_DATE"; //Transaction date must be fill
         public const string ERR_ADDITIONAL_EMPTY_APPOINTMENT = "ERR_ADDITIONAL_EMPTY_APPOINTMENT"; //Please Create Appointment
         public const string ERR_ADDITIONAL_NON_PERMANENT_AMOUNT = "ERR_ADDITIONAL_NON_PERMANENT_AMOUNT"; //Amount must be Input
         public const string ERR_ADDITIONAL_USED_CALCULATE = "ERR_ADDITIONAL_USED_CALCULATE"; //Component Has Been Calculated
        public const string ERR_ADDITIONAL_PAYROLL_HEADER = "ERR_ADDITIONAL_PAYROLL_HEADER"; //Invalid Additional Category
        public const string ERR_ADDITIONAL_PAYROLL_EMPLOYEE = "ERR_ADDITIONAL_PAYROLL_EMPLOYEE";//Please select Employee
        public const string ERR_ADDITIONAL_EMPTY_COMPONENT = "ERR_ADDITIONAL_EMPTY_COMPONENT";//Component Cannot be Empty, Please Input Component
        public const string ERR_ADDITIONAL_EFFECTIVE_APPOINTMENT = "ERR_ADDITIONAL_EFFECTIVE_APPOINTMENT"; // Please check effective date on the appointment
        #endregion

        #region Organization Payroll Employee Information
        public const string ERR_EMP_PAYROLL_VALID_PAYMENT_TYPE = "ERR_VALID_PAYMENT_TYPE";  //Please Select Payment Type
        public const string ERR_EMP_PAYROLL_VALID_PAYMENT_BANK = "ERR_VALID_PAYMENT_BANK";  //Please Select Payment Bank
        public const string ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK = "ERR_VALID_EMPLOYEE_BANK";  //Please Select bank
        public const string ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK_BRANCH = "ERR_VALID_EMPLOYEE_BANK_BRANCH";  //Please Select Bank Branch 

        public const string ERR_EMP_PAYROLL_VALID_CURRENCY = "ERR_EMP_PAYROLL_VALID_CURRENCY"; 
        public const string ERR_EMP_PAYROLL_ALREADY_EXIST = "ERR_EMP_PAYROLL_ALREADY_EXIST";  //employee can only have 1 (one) payroll payment type, either bank transfer or cash
        public const string ERR_EMP_PAYROLL_MUST_HAVE_DETAIL = "ERR_EMP_PAYROLL_MUST_HAVE_DETAIL";  //employee should have at least 1 (one) bank account
        //public const string ERR_EMP_PAYROLL_DIFFRENT_CURRENCY = "ERR_EMP_PAYROLL_DIFFRENT_CURRENCY";  //The employee bank accounts can have the same currency code or different currency code
        public const string ERR_EMP_PAYROLL_VALID_ACCOUNT = "ERR_EMP_PAYROLL_VALID_ACCOUNT";  //Please input Amount with valid number

        public const string ERR_EMP_PAYROLL_VALID_ACCOUNT_NAME = "ERR_VALID_ACCOUNT_NAME";  //Please input AccountName 
        public const string ERR_EMP_PAYROLL_VALID_AMOUNT = "ERR_VALID_AMOUNT";  //Please input Amount with valid number
        public const string ERR_EMP_PAYROLL_VALID_PERCENTAGE = "ERR_VALID_PERCENTAGE";  //Please input Percentage with valid number
        public const string ERR_EMP_PAYROLL_VALID_PRIORITY = "ERR_VALID_PRIORITY";  //Please input Priority with valid number

        public const string ERR_EMP_PAYROLL_PERCENTAGE_PAYROLL_100 = "ERR_EMP_PERCENTAGE_PAYROLL_100"; //Please input Percentage with total of Percentage must be 100%
        public const string ERR_EMP_PAYROLL_SAME_PRIORITY = "ERR_EMP_SAME_PRIORITY";  //Priority from one account with another account should be differentiated
        public const string ERR_EMP_PAYROLL_JUMP_PRIORITY = "ERR_EMP_JUMP_PRIORITY";  //Sorry, Priority of split Amount must be consecutive based on total of account number
        
        #endregion

        #region Organization Payroll Component
        public const string ERR_CALCULATION_BASIC_LOAN = "ERR_CALCULATION_BASIC_LOAN ";  //Amount Type’ must be = ‘FX – Fix Amount’ And ‘Calculation Basis’ must be = ‘FL - Full
        public const string ERR_ACTIVE_STATUS_COMPONENT_GROUP = "ERR_ACTIVE_STATUS_COMPONENT_GROUP";//There are still loan outstanding, can’t ‘In Active’ the component
        public const string ERR_CALCULATION_BASIC_COMPENSATION = "ERR_CALCULATION_BASIC_COMPENSATION"; //Amount Type’ must be = ‘FX – Fix Amount’ or ‘Calculation Basis’ must be = ‘FL - Full
        public const string ERR_TAX_DEDUCTION__IS_EMPTY = "ERR_TAX_DEDUCTION__IS_EMPTY"; //Tax Deduction must be input
        public const string ERR_TAX_POLICY_IS_EMPTY = "ERR_TAX_POLICY_IS_EMPTY";//Tax Policy must be input
        public const string ERR_COMPONENT_GROUP_IS_EMPTY = "ERR_COMPONENT_GROUP_IS_EMPTY";//Component Group must be input
        public const string ERR_TAXABLE_TYPE_IS_EMPTY = "ERR_TAXABLE_TYPE_IS_EMPTY";//Taxable Type must be input
        public const string ERR_AMOUNT_TYPE_IS_EMPTY = "ERR_AMOUNT_TYPE_IS_EMPTY";//Amount Type must be input
        public const string ERR_SPT_CODE_IS_EMPTY = "ERR_SPT_CODE_IS_EMPTY";//SPT Code Type must be input
        public const string ERR_CALCULATION_BASIC_IS_EMPTY = "ERR_CALCULATION_BASIC_IS_EMPTY";//Calculation Basic must be input
        public const string ERR_VALUE_FORMULA_IS_EMPTY = "ERR_VALUE_FORMULA_IS_EMPTY";//Value/Formula must be input
        public const string ERR_SIGN_IS_EMPTY = "ERR_SIGN_IS_EMPTY";//Sign must be input
        public const string ERR_FREQUENCY_IS_EMPTY = "ERR_FREQUENCY_IS_EMPTY"; //Frequency must be input
        public const string ERR_AMOUNT_TYPE_CALCULATION = "ERR_AMOUNT_TYPE_CALCULATION";//Amount Type = Fix - Amount then calculation basis must be ‘Full’
        public const string ERR_FORMULA = "ERR_FORMULA";//Value/Formula inputed is incorrect
        public const string ERR_DUPLICATE_GENERATE_CODE = "ERR_DUPLICATE_GENERATE_CODE"; //Sorry, this Code is already exist
        public const string ERR_COMPONENT_EMPTY_PAYROLL_PERIOD = "ERR_COMPONENT_EMPTY_PAYROLL_PERIOD";//Please Create Payroll Period
        public const string ERR_EMPLOYEE_NEWER_DATE = "ERR_EMPLOYEE_NEWER_DATE"; //Value/Formula - Start Date’ must be the same or newer than current payroll start date
        public const string ERR_EMPLOYEE_COMPONENT_START_DATE = "ERR_EMPLOYEE_COMPONENT_START_DATE"; //Start Date Required
        public const string ERR_EMPLOYEE_COMPONENT_END_DATE = "ERR_EMPLOYEE_COMPONENT_END_DATE"; //End Date Required
        public const string ERR_RULE_COMPONENT_GENERATED = "ERR_RULE_COMPONENT_GENERATED"; //Can edit or Deleted Generated Component 
        public const string ERR_EMPLOYEE_COMPONENT_DUPLICATE = "ERR_EMPLOYEE_COMPONENT_DUPLICATE"; //Component Code Duplicate
        public const string ERR_ORGANIZATION_ALREADY_USED = "ERR_ORGANIZATION_ALREADY_USED"; //Component Code Already Used with Employee
        public const string ERR_EMPLOYEE_COMPONENT_PRORATE_BASE = "ERR_EMPLOYEE_COMPONENT_PRORATE_BASE"; //Prorate Base Invalid
        public const string ERR_EMPLOYEE_END_DATE_OVERLAP = "ERR_EMPLOYEE_END_DATE_OVERLAP"; //End Date Cannot Overlap Period
        public const string ERR_EMPLOYEE_TAX_INFO = "ERR_EMPLOYEE_TAX_INFO";//Tax Information Must be Authorized
        public const string ERR_COMPONENT_CODE_ALREADY_USED = "ERR_COMPONENT_CODE_ALREADY_USED"; //Component Code Already Used
        public const string ERR_EMPLOYEE_NEWER_END_DATE = "ERR_EMPLOYEE_NEWER_END_DATE"; //Value/Formula - End Date’ must be  newer than Value/Formula - Start Date
        public const string ERR_CURRENCY = "ERR_CURRENCY"; //Please select Currency
        public const string ERR_ORGANIZATION_COMPONENT_PRORATE_BASE = "ERR_ORGANIZATION_COMPONENT_PRORATE_BASE"; //Prorate Base Must be Input
        public const string ERR_ORGANIZATION_ALREADY_USED_REPORT = "ERR_ORGANIZATION_ALREADY_USED_REPORT"; //Component Code Already Used by Report
        public const string ERR_ORGANIZATION_UPDATE_EMPLOYEE_COMPONENT = "ERR_ORGANIZATION_UPDATE_EMPLOYEE_COMPONENT"; //Please Update Employee Payroll Component Value to amount First
        public const string ERR_COMPONENT_CODE_ALREADY_CALCULATED = "ERR_COMPONENT_CODE_ALREADY_CALCULATED"; //Component Code Already Calculated
        #endregion

        #region Map Location
        public const string ERR_MAP_LOCATION_LATITUDE = "ERR_MAP_LOCATION_LATITUDE";
        public const string ERR_MAP_LOCATION_LONGITUDE = "ERR_MAP_LOCATION_LONGITUDE";
        public const string ERR_MAP_LOCATION_NAME = "ERR_MAP_LOCATION_NAME";
        public const string ERR_MAP_LOCATION_RANGE = "ERR_MAP_LOCATION_RANGE";
        public const string ERR_MAP_LOCATION_START_DATE = "ERR_MAP_LOCATION_START_DATE";
        public const string ERR_MAP_LOCATION_END_DATE = "ERR_MAP_LOCATION_END_DATE";
        public const string ERR_MAP_LOCATION_DUPLICATE_REF_ID = "ERR_MAP_LOCATION_DUPLICATE_REF_ID";
        public const string ERR_MAP_LOCATION_HO_BRANCH = "ERR_MAP_LOCATION_HO_BRANCH";
        #endregion

        #region
        public const string CONST_ERR_EXCHAGEFILE_FILE_EMPTY = "ERR_EXCHAGEFILE_FILE_EMPTY"; //File Can't Empty 0kb
        public const string CONST_ERR_EXCHAGEFILE_FILE_FORMAT = "ERR_EXCHAGEFILE_FILE_FORMAT"; //File Format only .DOC .DOCX .XLS .XLSX .PDF .RAR
        public const string CONST_ERR_EXCHAGEFILE_FILE_SIZE = "ERR_EXCHAGEFILE_FILE_SIZE"; //Max File Size 2mb
        #endregion

        #region Employee Document
        public const string CONST_ERR_EMP_DOC_FILE_EMPTY = "ERR_EXCHAGEFILE_FILE_EMPTY"; //File Can't Empty 0kb
        public const string CONST_ERR_EMP_DOC_FILE_FILE_FORMAT = "ERR_EXCHAGEFILE_FILE_FORMAT"; //File Format only .DOC .DOCX .XLS .XLSX .PDF .RAR
        public const string CONST_ERR_EMP_DOC_FILE_FILE_SIZE = "ERR_EXCHAGEFILE_FILE_SIZE"; //Max File Size 2mb
        public const string CONST_ERR_EMP_DOC_FILE_FILE_EXIST = "ERR_EMP_DOC_FILE_FILE_EXIST"; //File Type already exists, if you want to upload again you have to delete the file

        #endregion

        #region Orgnization Maintenence
        #region Organization
        public const string CONST_ERR_RULE_BASE_ORG = "ERR_RULE_BASE_ORG";
        public const string CONST_ERR_ORGANIZATION_CODE_IS_EMPTY = "ERR_ORGANIZATION_IS_EMPTY"; //Organization Code is Empty
        public const string CONST_ERR_ORGANIZATION_CODE_ALREADY_EXISTS = "ERR_ORGANIZATION_CODE_ALREADY_EXISTS"; //Organization Code Already Exist
        public const string CONST_ERR_ORGANIZATION_NAME_IS_EMPTY = "ERR_ORGANIZATION_NAME_IS_EMPTY"; //Organization Name is Empty
        public const string CONST_ERR_ORGANIZATION_SERVICE_IS_EMPTY = "ERR_ORGANIZATION_SERVICE_IS_EMPTY"; //Organization Service is Empty
        public const string CONST_ERR_ORG_NAME_MUST_ALPHA_NUMERIC = "ERR_ORG_NAME_MUST_ALPHA_NUMERIC"; //Organization Service is Empty
 
        #endregion
        #region Orgnization Structure
        public const string CONST_ERR_ORGANIZATION_STRUCTURE_TYPE_IS_EMPTY = "ERR_ORGANIZATION_STRUCTURE_TYPE_IS_EMPTY ";  //Please Select Organization Structure Type
        public const string CONST_ERR_ORGANIZATION_STRUCTURE_CODE_IS_EMPTY = "ERR_ORGANIZATION_STRUCTURE_CODE_IS_EMPTY ";  //Please Input Organization Structure Code
        public const string CONST_ERR_ORGANIZATION_STRUCTURE_CODE_ALREADY_EXISTS = "ERR_ORGANIZATION_STRUCTURE_CODE_ALREADY_EXISTS";  //Structure Already Exists
        public const string CONST_ERR_ORGANIZATION_STRUCTURE_DESCRIPTION_IS_EMPTY = "ERR_ORGANIZATION_STRUCTURE_DESCRIPTION_IS_EMPTY";  //Please Input Organization Structure Descrription
        #endregion
        #region Contact Person
        public const string CONST_ERR_NAME_IS_EMPTY = "ERR_NAME_IS_EMPTY";  //Please Input Name
        public const string CONST_ERR_POSITION_CODE_IS_EMPTY = "ERR_POSITION_CODE_IS_EMPTY";  //Please Input Designation / Position
        public const string CONST_ERR_POSITION_DESCRIPTION_IS_EMPTY = "ERR_POSITION_DESCRIPTION_IS_EMPTY";  //Please Input Designation / Position Description
        public const string CONST_ERR_VALID_PHONE_NUMBER = "ERR_VALID_NUMBER"; //Please input Office Phone Number with valid number
        #endregion
        #region Head Office Branch
        public const string CONST_ERR_HO_EMPTY = "ERR_HO_IS_EMPTY ";  //Please Create Head Office For Organization Selected Frist
        
        //public const string CONST_ERR_HO_BRANCH_TYPE_IS_EMPTY = "ERR_HO_BRANCH_TYPE_IS_EMPTY ";  //Please Select Organization Organization Type
        public const string CONST_ERR_HO_BRANCH_CODE_IS_EMPTY = "ERR_HO_BRANCH_CODE_IS_EMPTY ";  //Please Input Head Office/Branch Code
        public const string CONST_ERR_HO_BRANCH_CODE_ALREADY_EXISTS = "ERR_HO_BRANCH_CODE_ALREADY_EXISTS";  //Head Office/Branch Code Already Exists
        public const string CONST_ERR_HO_BRANCH_NAME_IS_EMPTY = "ERR_HO_BRANCH_NAME_IS_EMPTY";  //Please Input Head Office/Branch Name
        public const string CONST_ERR_HO_BRANCH_ADDRESS_IS_EMPTY = "ERR_HO_BRANCH_ADDRESS_IS_EMPTY";  //Please Input Address  
        
        public const string CONST_ERR_HO_BRANCH_TAX_DISTRICT_IS_EMPTY = "ERR_HO_BRANCH_TAX_DISTRICT_IS_EMPTY";  //Please Input District
        public const string CONST_ERR_HO_BRANCH_TAX_VILLAGE_IS_EMPTY = "ERR_HO_BRANCH_TAX_VILLAGE_IS_EMPTY";  //Please Input Village 
        public const string CONST_ERR_HO_BRANCH_TAX_CITY_REGENCY_IS_EMPTY = "ERR_HO_BRANCH_TAX_CITY_REGENCY_IS_EMPTY";  //Please Input City/Regency
        public const string CONST_ERR_HO_BRANCH_TAX_POST_IS_EMPTY = "ERR_HO_BRANCH_TAX_POST_IS_EMPTY";  //Please Input Post Code
        public const string CONST_ERR_CHOOSE_ONE = "ERR_CHOOSE_ONE";  //Please Input Organization Structure Descrription


        public const string CONST_ERR_HO_BRANCH_LOCATION_IS_EMPTY = "ERR_HO_BRANCH_LOCATION_IS_EMPTY";  //Please Input Office/Branch Location
        public const string CONST_ERR_HO_BRANCH_LOCATION_CANNOT_DELETE = "ERR_HO_BRANCH_LOCATION_CANNOT_DELETE"; //

        public const string CONST_ERR_HO_BRANCH_PHONE_NUMBER_IS_EMPTY = "ERR_HO_BRANCH_PHONE_NUMBER_IS_EMPTY";  //Please Input Head Office/Branch Phone Number  
        public const string CONST_ERR_HO_BRANCH_FAX_NUMBER_IS_EMPTY = "ERR_HO_BRANCH_FAX_NUMBER_IS_EMPTY";  //Please Input Head Office/Branch Fax Number
        public const string CONST_ERR_HO_BRANCH_TAXID_IS_EMPTY = "ERR_HO_BRANCH_TAXID_IS_EMPTY";  //Please Input Head Office/Branch Tax ID
        public const string CONST_ERR_HO_BRANCH_TAXID_MUST_SAME_HO = "ERR_HO_BRANCH_TAXID_ALREADY_EXISTS";  //Head Office/Branch Tax ID Already Exists
        public const string CONST_ERR_HO_BRANCH_SIGNER_IS_EMPTY = "ERR_HO_BRANCH_SIGNER_NAME_IS_EMPTY";  //Please Input Head Office/Branch Signer Name
        public const string CONST_ERR_HO_BRANCH_SIGNER_POSITION_IS_EMPTY = "ERR_HO_BRANCH_SIGNER_POSITION_IS_EMPTY";  //Please Input Head Office/Branch Signer Position
        public const string CONST_ERR_HO_BRANCH_SIGNER_TAXID_IS_EMPTY = "ERR_HO_BRANCH_SIGNER_TAXID_IS_EMPTY";  //Please Input Head Office/Branch Signer Tax ID
        public const string CONST_ERR_HO_BRANCH_SIGNER_TYPE_IS_EMPTY = "ERR_HO_BRANCH_SIGNER_TYPE_IS_EMPTY";  //Please Select Organization Signatory Type
        public const string CONST_ERR_HO_BRANCH_TAX_E_BILLING_LOGINID_IS_EMPTY = "ERR_HO_BRANCH_TAX_E_BILLING_LOGINID_IS_EMPTY";  //Please Input Head Office/Branch Tax e-Billing Login ID
        public const string CONST_ERR_HO_BRANCH_TAX_E_BILLING_PASSWORD_IS_EMPTY = "ERR_HO_BRANCH_TAX_E_BILLING_PASSWORD_IS_EMPTY";  //Please Input Head Office/Branch Tax e-Billing Password        
        public const string CONST_ERR_HO_BRANCH_TAX_OFFICE_NAME_IS_EMPTY = "ERR_HO_BRANCH_TAX_OFFICE_NAME_IS_EMPTY";  //Please Input Head Office/Branch Tax Office Name        
        public const string CONST_ERR_HO_BRANCH_TAX_OFFICE_ADDRESS_IS_EMPTY = "ERR_HO_BRANCH_TAX_OFFICE_ADDRESS_IS_EMPTY";  //Please Input Head Office/Branch Tax Office Address
        public const string CONST_ERR_HO_BRANCH_TAX_OFFICE_AR_IS_EMPTY = "ERR_HO_BRANCH_TAX_OFFICE_ADDRESS_IS_EMPTY";  //Please Input Head Office/Branch Tax Office Address
        public const string CONST_ERR_HO_BRANCH_TAX_OFFICE_AR_PHONE_IS_EMPTY = "ERR_HO_BRANCH_TAX_OFFICE_AR_PHONE_IS_EMPTY";  //Please Input Head Office/Branch Phone
        public const string CONST_ERR_HO_BRANCH_TAX_OFFICE_AR_EMAIL_IS_EMPTY = "ERR_HO_BRANCH_TAX_OFFICE_AR_EMAIL_IS_EMPTY";  //Please Input Head Office/Branch Email
        public const string CONST_ERR_HO_BRANCH_VALID_NUMBER = "ERR_VALID_NUMBER";  //Please input Valid Number

        public const string CONST_ERR_HO_BRANCH_CREATE_ROLE_1 = "ERR_HO_BRANCH_CREATE_ROLE_1";  //Sorry Head Office is Unauthorize
        public const string CONST_ERR_HO_BRANCH_CREATE_ROLE_2 = "ERR_HO_BRANCH_CREATE_ROLE_2";  //Sorry Head Office Can't Be Deleted
        public const string CONST_ERR_HO_BRANCH_MAP_LOCATION = "ERR_HO_BRANCH_MAP_LOCATION";


        #endregion
        #region Holiday Calendar Organization
        //public const string CONST_ERR_HCO_OH_BRANCH_CODE_IS_EMPTY = "ERR_HCO_OH_BRANCH_CODE_IS_EMPTY ";  //Please Select HO/Branch Code
        public const string CONST_ERR_HCO_LOCATION_IS_EMPTY = "ERR_HCO_LOCATION_IS_EMPTY ";  //Please Select Location Head Office/Branch
        public const string CONST_ERR_HCO_DATE_IS_EMPTY = "ERR_HCO_DATE_IS_EMPTY ";  //Please Head Office/Branch Code is Empty
        public const string CONST_ERR_HCO_NAME_IS_EMPTY = "ERR_HCO_NAME_IS_EMPTY";  //Please Input Noliday Name
        public const string CONST_ERR_HCO_ALREADY_EXISTS = "ERR_HCO_ALREADY_EXISTS";  //Holiday Calendar Already Exists

        public const string CONST_ERR_HCO_RULE_DELETE = "ERR_HCO_RULE_DELETE";
        #endregion
        #region Bank Information
        public const string CONST_ERR_BANK_INFORMATION_BANK_IS_EMPTY = "ERR_BANK_INFORMATION_BANK_IS_EMPTY ";  //Please Select Bank
        public const string CONST_ERR_BANK_INFORMATION_BANK_MAX_LEN = "ERR_BANK_INFORMATION_BANK_MAX_LEN ";  //Max Bank Code 15 Character
        public const string CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_IS_EMPTY = "ERR_BANK_INFORMATION_ACCOUNT_NUMBER_IS_EMPTY ";  //Please Input Account Number is Empty
        public const string CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_ALREADY_EXIST = "ERR_BANK_INFORMATION_ACCOUNT_NUMBER_ALREADY_EXIST ";  // Account Number Already Exist
        public const string CONST_ERR_BANK_INFORMATION_ACCOUNT_VALID_NUMBER = "ERR_ERR_BANK_INFORMATION_ACCOUNT_VALID_NUMBER "; // Please input Account Number with valid number
        public const string CONST_ERR_BANK_INFORMATION_ACCOUNT_NAME_IS_EMPTY = "ERR_BANK_INFORMATION_ACCOUNT_NAME_IS_EMPTY ";  //Please Input Account Name is Empty
        public const string CONST_ERR_BANK_INFORMATION_ACCOUNT_DEBET_ACCOUNT_ID_IS_EMPTY = "ERR_BANK_INFORMATION_ACCOUNT_DEBET_ACCOUNT_ID_IS_EMPTY ";  //Please Input Debet Account is Empty
        public const string CONST_ERR_BANK_INFORMATION_REFERENCE_IS_EMPTY = "ERR_BANK_INFORMATION_REFERENCE_IS_EMPTY ";  //Please Input Reference is Empty
        public const string CONST_ERR_BANK_INFORMATION_PAYMENT_SET_CODE_IS_EMPTY = "ERR_BANK_INFORMATION_PAYMENT_SET_CODE_IS_EMPTY ";  //Please Input Payment Set Code is Empty
        public const string CONST_ERR_BANK_INFORMATION_CURRENCY_CODE_IS_EMPTY = "ERR_BANK_INFORMATION_CURRENCY_CODE_IS_EMPTY ";  //Please Select Currency
        public const string CONST_ERR_BANK_INFORMATION_PURPOSE_IS_EMPTY = "ERR_BANK_INFORMATION_PURPOSE_IS_EMPTY";  //Please Input Purpose
        public const string CONST_ERR_REFF_BANK_INFORMATION_MAX_LEN = "ERR_REFF_BANK_INFORMATION_MAX_LEN"; //Max Length 15 Character  
        #endregion
        #region BPJS Manpower & BPJS Healthcare
        public const string CONST_ERR_NPP_NUMBER_IS_EMPTY = "ERR_NPP_NUMBER_IS_EMPTY";  //Please Input NPP Number
        public const string CONST_ERR_NPP_NUMBER_ALREADY_EXISTS = "ERR_NPP_NUMBER_ALREADY_EXISTS";  //NPP Number Already Exist
        public const string CONST_ERR_OFFICE_FAX_NUMBER_IS_EMPTY = "ERR_OFFICE_FAX_NUMBER_IS_EMPTY";  //Please Input Office Fax Number
        public const string CONST_ERR_OFFICE_FAX_NUMBER_NOT_VALID = "ERR_OFFICE_FAX_NUMBER_NOT_VALID";  //Please input Office Phone Number with valid number
        
        public const string CONST_ERR_OFFICE_ADDRESS_IS_EMPTY = "ERR_OFFICE_ADDRESS_IS_EMPTY";  //Please Input Office Adderss
        public const string CONST_ERR_OFFICE_PHONE_NUMBER_IS_EMPTY = "ERR_OFFICE_PHONE_NUMBER_IS_EMPTY";  //Please Office Phone
        public const string CONST_ERR_OFFICE_PHONE_NUMBER_NOT_VALID = "ERR_OFFICE_PHONE_NUMBER_NOT_VALID";  //Please input Office Phone Number with valid number
        
        public const string CONST_ERR_RO_IS_EMPTY = "ERR_RO_IS_EMPTY";  //Please Input RO
        public const string CONST_ERR_PHONE_MOBILE_NUMBER_IS_EMPTY = "ERR_PHONE_MOBILE_NUMBER_IS_EMPTY";  //Please Input Phone/Mobile Number
        public const string CONST_ERR_PHONE_MOBILE_NUMBER_NOT_VALID = "ERR_PHONE_MOBILE_NUMBER_NOT_VALID";  //Please Phone Number with valid number

        #region BPJS Manpower
        public const string CONST_ERR_SIPP_ONLINE_LOGIN_ID_IS_EMPTY = "ERR_SIPP_ONLINE_LOGIN_ID_IS_EMPTY";  //Please Input SIPP Online Login ID
        public const string CONST_ERR_SIPP_ONLINE_LOGIN_ID_ALREADY_EXISTS = "SIPP_ONLINE_LOGIN_ID_ALREADY_EXISTS";  //SIPP Online Login ID Already Exist
        public const string CONST_ERR_SIPP_ONLINE_PASSWORD_IS_EMPTY = "ERR_SIPP_ONLINE_PASSWORD_IS_EMPTY";  //Input SIPP Online Password
        #endregion
        #region BPJS Healthcare
        public const string CONST_ERR_E_DABU_LOGIN_ID_IS_EMPTY = "ERR_E_DABU_LOGIN_ID_IS_EMPTY";  //Please Input e-Dabu Login ID
        public const string CONST_ERR_E_DABU_LOGIN_ALREADY_EXISTS = "ERR_E_DABU_LOGIN_ALREADY_EXISTS";  //e-Dabu Login ID Already Exist
        public const string CONST_ERR_E_DABU_PASSWORD_IS_EMPTY = "ERR_E_DABU_PASSWORD_IS_EMPTY";  //Input e-Dabu Password
        public const string CONST_ERR_E_ID_LOGIN_ID_IS_EMPTY = "ERR_E_ID_LOGIN_ID_IS_EMPTY";  //Please e-ID Login ID
        public const string CONST_ERR_E_ID_LOGIN_ID_ALREADY_EXISTS = "ERR_E_ID_LOGIN_ID_ALREADY_EXISTS";  //e-ID Already Exist
        public const string CONST_ERR_E_ID_PASSWORD_IS_EMPTY = "ERR_E_ID_PASSWORD_IS_EMPTY";  //Input e-ID Password
        #endregion
        #endregion
        #region Cost Center
        public const string CONST_ERR_COST_CENTER_CODE_IS_EMPTY = "ERR_COST_CENTER_CODE_IS_EMPTY";  //Please Input Cost Center
        public const string CONST_ERR_COST_CENTER_CODE_ALREADY_EXISTS = "ERR_COST_CENTER_CODE_ALREADY_EXISTS";  //Cost Center Already Exist
        #endregion
        #region Chart of Account
        public const string CONST_ERR_CHART_ACCOUNT_CODE_IS_EMPTY = "ERR_CHART_ACCOUNT_CODE_IS_EMPTY";  //Please Input Chart Account Code
        public const string CONST_ERR_CHART_ACCOUNT_CODE_ALREADY_EXISTS = "ERR_CHART_ACCOUNT_CODE_ALREADY_EXISTS";  //Chart of Account CodeAlready Exist
        public const string CONST_ERR_CHART_ACCOUNT_DESCRIPTION_IS_EMPTY = "ERR_CHART_ACCOUNT_DESCRIPTION_IS_EMPTY";  //Please Input Chart Account Description
        public const string CONST_ERR_CHART_ACCOUNT_DB_CR_IS_EMPTY = "ERR_CHART_ACCOUNT_DB_CR_IS_EMPTY";  //Please Input Chart Account Db/Cr
        #endregion
        #endregion

        #region Organization Working Time
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_CODE_IS_EMPTY"; //Organization Working Time Code is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_ALREADY_EXISTS = "ERR_ORGANIZATION_WORKINGTIME_CODE_ALREADY_EXISTS"; //Organization Working Time Code Already Exist
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_DESCRIPTION_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_DESCRIPTION_IS_EMPTY"; //Organization Working Time Description is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_STARTDATE_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_STARTDATE_IS_EMPTY"; //Start Date is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_ENDDATE_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_ENDDATE_IS_EMPTY"; //End Date is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_IS_EMPTY"; //Please Select Select Working Day
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_MONTH_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_MONTH_EMPTY"; //Select Working Day Month
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_WORKIG_DAY_IS_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_BASE_WORKIG_DAY_IS_EMPTY"; //Select Base Working Day
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_FIX_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_BASE_FIX_EMPTY"; //Base Fix (in Day) is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_CALENDAR_EMPTY = "ERR_ORGANIZATION_WORKINGTIME_BASE_CALENDAR_EMPTY"; //Base Calendar (in Day) is Empty
        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_OVERLAP_SCHEDULE = "ERR_ORGANIZATION_WORKINGTIME_OVERLAP_SCHEDULE"; //Schedule Time Out Must be More Than Schedule Time In

        public const string CONST_ERR_ORGANIZATION_WORKINGTIME_VALID_NUMBER = "ERR_ORGANIZATION_WORKINGTIME_VALID_NUMBER"; //Invalid Number

        public const string CONST_ERR_RULE_01_WORKINGTIME_USED = "ERR_RULE_01_WORKINGTIME_USED";

        #endregion

        #region Payroll Report Generation
        public const string CONST_ERR_REPORT_GENERATION_PAYROLL_PERIOD = "ERR_REPORT_GENERATION_PAYROLL_PERIOD";
        public const string CONST_ERR_REPORT_GENERATION_RUN = "ERR_REPORT_GENERATION_RUN";
        public const string CONST_ERR_REPORT_GENERATION_HEAD_OFFICE_BRANCH = "ERR_REPORT_GENERATION_HEAD_OFFICE_BRANCH";
        public const string CONST_ERR_REPORT_GENERATION_LOCATION = "ERR_REPORT_GENERATION_LOCATION";
        public const string CONST_ERR_REPORT_GENERATION_EMPLOYEE_STATUS = "ERR_REPORT_GENERATION_EMPLOYEE_STATUS";
        public const string CONST_ERR_REPORT_GENERATION_COST_CENTER = "ERR_REPORT_GENERATION_COST_CENTER";
        public const string CONST_ERR_REPORT_GENERATION_EMPLOYEMENT_STATUS = "ERR_REPORT_GENERATION_EMPLOYEMENT_STATUS";
        public const string CONST_ERR_REPORT_GENERATION_DIVISION = "ERR_REPORT_GENERATION_DIVISION";
        public const string CONST_ERR_REPORT_GENERATION_DEPARTMENT = "ERR_REPORT_GENERATION_DEPARTMENT";
        public const string CONST_ERR_REPORT_GENERATION_POSITION = "ERR_REPORT_GENERATION_POSITION";
        public const string CONST_ERR_REPORT_GENERATION_GRADE = "ERR_REPORT_GENERATION_GRADE";
        public const string CONST_ERR_REPORT_GENERATION_DEPUTATION = "ERR_REPORT_GENERATION_DEPUTATION";
        public const string CONST_ERR_REPORT_GENERATION_PAYROLL_REPORT_CODE = "ERR_REPORT_GENERATION_PAYROLL_REPORT_CODE";
        public const string CONST_ERR_REPORT_GENERATION_REPORT_GROUP = "ERR_REPORT_GENERATION_REPORT_GROUP";
        #endregion

        #region Payroll Report Setting
        public const string ERR_PAYROLL_REPORT_SETTING_DUPLICATE = "ERR_PAYROLL_REPORT_SETTING_DUPLICATE"; //Duplicate Data
        public const string CONST_ERR_PAYROLL_REPORT_SETTING_COMPONENT_IS_EMPTY = "ERR_PAYROLL_REPORT_SETTING_COMPONENT_IS_EMPTY"; //Component is empty
        public const string CONST_ERR_PAYROLL_REPORT_SETTING_DESCRIPTION_IS_EMPTY = "ERR_PAYROLL_REPORT_SETTING_DESCRIPTION_IS_EMPTY"; //Description is empty
        #endregion

        #region Tax Period
        public const string CONST_ERR_TAX_PERIOD_UPDATE_IS_DELETE_PAYROLL_PERIOD = "ERR_TAX_PERIOD_UPDATE_IS_DELETE_PAYROLL_PERIOD";
        public const string CONST_ERR_TAX_PERIOD_ALREADY_EXISTS = "ERR_TAX_PERIOD_ALREADY_EXISTS";
        public const string CONST_ERR_TAX_PERIOD_AND_PAYROLL_PERIOD_EDIT = "ERR_TAX_PERIOD_AND_PAYROLL_PERIOD_EDIT";
        public const string CONST_ERR_TAX_YEAR_IS_EMPTY = "ERR_TAX_YEAR_IS_EMPTY";  //Please Select Tax Year
        public const string CONST_ERR_TAX_PERIOD_FROM_IS_EMPTY = "ERR_TAX_PERIOD_FROM_IS_EMPTY";  //Please Select Date
        public const string CONST_ERR_TAX_PERIOD_TO_IS_EMPTY = "ERR_TAX_PERIOD_FROM_IS_EMPTY";  //Please Select Date
        public const string ERR_MONTH_DUPLICATE = "ERR_MONTH_DUPLICATE"; //Month can't be duplicate
        public const string ERR_MONTH_INCREMENT = "ERR_MONTH_INCREMENT"; //Sorry, a month of tax period must be consecutive
        #endregion

        #region Organization Account Group Maintenence
        public const string CONST_ERR_ACCOUNT_NUMBBER_IS_EMPTY = "ERR_ACCOUNT_NUMBBER_IS_EMPTY";  // Please input Account Number
        public const string CONST_ERR_ACCOUNT_NUMBBER_ALREADY_EXISTS = "ERR_ACCOUNT_NUMBBER_ALREADY_EXISTS";  // Account Number Already Exist
        public const string CONST_ERR_ACCOUNT_NUMBBER_VALID_NUMBER = "ERR_ACCOUNT_NUMBBER_VALID_NUMBER"; //Please input Account Number with valid number
        public const string CONST_ERR_ACCOUNT_NUMBBER_DESCRIPTION_IS_EMPTY = "ERR_ACCOUNT_NUMBBER_DESCRIPTION_IS_EMPTY";  // Please Input Description
        public const string CONST_ERR_ACCOUNT_NUMBBER_STATUS_IS_EMPTY = "ERR_ACCOUNT_NUMBBER_STATUS_IS_EMPTY";  // Please Select Status
        #endregion

        #region Payslip Information
        public const string CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION_IS_EMPTY = "ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION_IS_EMPTY ";  //Please Select Payslip_Distribution
        public const string CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION = "ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION ";
        #endregion

        #region Payslip Information
        public const string CONST_ERR_TAX_PERIOD_MONTH_IS_EMPTY = "ERR_MONTH_IS_EMPTY ";  //Please Select Month
        public const string CONST_ERR_TAX_PERIOD_TAX_PERIOD_FROM = "ERR_TAX_PERIOD_TAX_PERIOD_FROM";  //Sorry, this Period can’t be less than Current Month
        public const string CONST_ERR_TAX_PERIOD_TAX_PERIOD_TO = "ERR_TAX_PERIOD_TAX_PERIOD_TO ";  //Sorry, this Period can’t be less than Tax Period From
        
        #endregion

        #region Role Menu Function
        public const string CONST_ERR_ROLE_CODE_IS_EMPTY = "ERR_ROLE_CODE_IS_EMPTY ";  //Please Select Bank
        public const string CONST_ERR_ROLE_DESCRIPTION_IS_EMPTY = "ERR_ROLE_DESCRIPTION_IS_EMPTY ";  //Please Input Account Number is Empty
        public const string CONST_ERR_ROLE_CODE_ALREADY_EXIST = "ERR_ROLE_CODE_ALREADY_EXIST ";  // Account Number Already Exist
        #endregion

        #region Payroll Period
        public const string CONST_ERR_MONTH_TAX_PERIOD_DUPLICATE = "ERR_MONTH_TAX_PERIOD_DUPLICATE";
        public const string CONST_ERR_MONTH_TAXPERIOD_INCREMENT = "ERR_MONTH_TAXPERIOD_INCREMENT";
        public const string CONST_ERR_PERIOD_START_DATE_LESS_THAN_CURRENT_DATE = "ERR_PERIOD_START_DATE_LESS_THAN_CURRENT_DATE";
        public const string CONST_ERR_PERIOD_END_DATE_LESS_THAN_PERIOD_START_DATE = "ERR_PERIOD_END_DATE_LESS_THAN_PERIOD_START_DATE";
        public const string CONST_ERR_PAYROLL_PERIOD_CANT_OVERLAP = "ERR_PAYROLL_PERIOD_CANT_OVERLAP";
        public const string CONST_ERR_INTERVAL_BETWEEN_PAYROLL_PERIOD_DATE = "ERR_INTERVAL_BETWEEN_PAYROLL_PERIOD_DATE";
        public const string CONST_ERR_PAYROLL_PERIOD_ALREADY_EXISTS = "ERR_PAYROLL_PERIOD_ALREADY_EXISTS";
        public const string CONST_ERR_TAX_PERIOD_ID_IS_EMPTY = "ERR_TAX_PERIOD_ID_IS_EMPTY";
        public const string CONST_ERR_PERIOD_START_IS_EMPTY = "ERR_PERIOD_START_IS_EMPTY";
        public const string CONST_ERR_PERIOD_END_IS_EMPTY = "ERR_PERIOD_END_IS_EMPTY";
        #endregion

        #region New Upload Error
        public const string CONST_ERR_ROW_IS_EMPTY_UPLOAD = "ERR_ROW_IS_EMPTY_UPLOAD";
        public const string CONST_ERR_UPLOAD_HEADER_NOT_EXIST_IN_DATABASE = "ERR_UPLOAD_HEADER_NOT_EXIST_IN_DATABASE";
        public const string CONST_ERR_UPLOAD_HEADER_PAYROLL_VARIABLE = "ERR_UPLOAD_HEADER_PAYROLL_VARIABLE";
        public const string CONST_ERR_ERR_DATA_MUST_BE_DATETIME = "ERR_MUST_BE_DATETIME";
        public const string CONST_ERR_DATA_MUST_BE_NUMERIC = "ERR_MUST_BE_NUMERIC";
        public const string CONST_ERR_DATA_MUST_BE_FILLED = "ERR_MUST_BE_FILLED";
        public const string CONST_ERR_SYSTEM_TEMPORARY_UNAVAILABLE = "ERR_SYSTEM_TEMPORARY_UNAVAILABLE";
        public const string CONST_ERR_BUSY_TRY_AGAIN = "ERR_BUSY_TRY_AGAIN";
        public const string CONST_ERR_ORG_MUST_HAVE_PAYROLL_PERIOD = "ERR_ORG_MUST_HAVE_PAYROLL_PERIOD";
        public const string CONST_ERR_EMP_APPOINTMENT_UNAUTHORIZED = "ERR_EMP_APPOINTMENT_UNAUTHORIZED";
        public const string CONST_ERR_VALUE_INVALID = "ERR_VALUE_INVALID";
        public const string CONST_ERR_ID_INVALID = "ERR_ID_INVALID"; //err Nationality
        public const string CONST_ERR_CALCULATION_ON_PROGRESS = "ERR_CALCULATION_ON_PROGRESS";
        public const string CONST_ERR_UPLOAD_HEADER_ADDITIONAL_PAYROLL_HEADER = "ERR_UPLOAD_HEADER_ADDITIONAL_PAYROLL_HEADER";
        public const string CONST_ERR_UPLOAD_HEADER_ADDITIONAL_PAYROLL_COMPONENT_HEADER = "ERR_UPLOAD_HEADER_ADDITIONAL_PAYROLL_COMPONENT_HEADER"; // invalid Header Component Code
        public const string CONST_ERR_UPLOAD_SHEET_NAME = "ERR_UPLOAD_SHEET_NAME";
        #endregion

        #region Payroll Schedule
        public const string CONST_ERR_PAYROLL_SCHEDULE_IS_ALREADY_EXIST = "ERR_PAYROLL_SCHEDULE_IS_ALREADY_EXIST";
        public const string CONST_ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH = "ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH";
        public const string CONST_ERR_PAYROLL_SCHEDULE_PERIOD_DOES_NOT_EXIST = "ERR_PAYROLL_SCHEDULE_PERIOD_DOES_NOT_EXIST";
        #endregion

        #region Loan
        public const string CONST_ERR_LOAN_EMPLOYEE_ID_EMPTY = "ERR_LOAN_EMPLOYEE_ID_EMPTY";
        public const string CONST_ERR_LOAN_MAXIMUM_TENOR = "ERR_LOAN_MAXIMUM_TENOR";
        public const string CONST_ERR_LOAN_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS = "ERR_LOAN_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS";
        public const string CONST_ERR_CURRENCY_EMPTY = "ERR_CURRENCY_EMPTY";
        public const string CONST_ERR_TYPE_LOAN_EMPTY = "ERR_TYPE_LOAN_EMPTY";
        public const string CONST_ERR_LOAN_COMPONENT_LINKAGE_EMPTY = "ERR_LOAN_COMPONENT_LINKAGE_EMPTY";
        public const string CONST_ERR_LOAN_AMOUNT_EMPTY = "ERR_LOAN_AMOUNT_EMPTY";
        public const string CONST_ERR_OUTSTANDING_LOAN_AMOUNT_EMPTY = "ERR_OUTSTANDING_LOAN_AMOUNT_EMPTY";
        public const string CONST_ERR_LOAN_INSTALLMENT_AMOUNT_EMPTY = "ERR_LOAN_INSTALLMENT_AMOUNT_EMPTY";
        public const string CONST_ERR_LOAN_TENOR_EMPTY = "ERR_LOAN_TENOR_EMPTY";
        public const string CONST_ERR_LOAN_OUSTANDING_TENOR_EMPTY = "ERR_LOAN_OUSTANDING_TENOR_EMPTY";
        public const string CONST_ERR_LOAN_OUSTANDING_TENOR_NOT_VALID = "ERR_LOAN_OUSTANDING_TENOR_NOT_VALID";
        public const string CONST_ERR_RULE_EMP_DEUCTION_CALCULATION = "ERR_RULE_EMP_DEUCTION_CALCULATION";
        #endregion

        #region ConpensationBenefit
        public const string CONST_ERR_BENEFIT_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS = "ERR_BENEFIT_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS";
        public const string CONST_ERR_BENEFIT_EMPLOYEE_ID_EMPTY = "ERR_BENEFIT_EMPLOYEE_ID_EMPTY";
        public const string CONST_ERR_TYPE_BENEFIT_EMPTY = "ERR_TYPE_BENEFIT_EMPTY";
        public const string CONST_ERR_BENEFIT_COMPONENT_LINKAGE_EMPTY = "ERR_BENEFIT_COMPONENT_LINKAGE_EMPTY";
        public const string CONST_ERR_BENEFIT_BUDGET_EMPTY = "ERR_BENEFIT_BUDGET_EMPTY";
        public const string CONST_ERR_PERIOD_BENEFIT_EMPTY = "ERR_PERIOD_BENEFIT_EMPTY";
        #endregion

        #region Generate Bank File
        public const string CONST_ERR_GBF_FILE_NAME = "ERR_GBF_FILE_NAME"; // File Name Can't Empty
        public const string CONST_ERR_GBF_FILE_TYPE = "ERR_GBF_FILE_TYPE"; // Select File Type
        public const string CONST_ERR_GBF_REFERENCE = "ERR_GBF_REFERENCE"; // Reference Can't Empty
        public const string CONST_ERR_GBF_TRANSFER_MESS = "ERR_GBF_TRANSFER_MESS"; // Transfer Message Can't Empty
        public const string CONST_ERR_TOTAL_AMOUNT_TOTAL_ACCOUNT = "ERR_TOTAL_AMOUNT_TOTAL_ACCOUNT"; // Transfer Message Can't Empty
        #endregion

        #region Payroll Closing

        public const string CONST_ERR_RUN_CLOSING_HAS_AUTHORIZED = "ERR_RUN_CLOSING_HAS_AUTHORIZED"; //Run Closing Already Authorized
        public const string CONST_ERR_TAX_PERIOD_CLOSING_HAS_AUTHORIZED = "ERR_TAX_PERIOD_CLOSING_HAS_AUTHORIZED"; //Tax Period Closing Already Authorized
        public const string CONST_WARNING_EMP_UNCALCULATED_EMPLOYEES = "WARNING_EMP_UNCALCULATED_EMPLOYEES"; //There are still uncalculated employees
        public const string CONST_ERR_DELETE_ONLY_ONE_LATEST_RECORD = "ERR_DELETE_ONLY_ONE_LATEST_RECORD"; //Please delete only one latest record
        #endregion

        #region Report Employee Payslip
        public const string CONST_ERR_PAYSLIP_FILTER_CRITERIA = "ERR_PAYSLIP_FILTER_CRITERIA";
        #endregion

        #region Organization Email Setup
        public const string CONST_ERR_ORGANIZATION_EMAIL_SETUP_EXISTS = "ERR_ORGANIZATION_EMAIL_SETUP_EXISTS";
        #endregion

        #region Change Password
        public const string CONST_ERR_CURRENTPASSWORD = "ERR_CURRENTPASSWORD";
        public const string CONST_ERR_CONFIRMNEWPASSWORD = "ERR_CONFIRMNEWPASSWORD";
        #endregion

        #region Payroll Payslip Setting
        public const string CONST_ERR_REPORT_NAME_EMPTY = "CONST_ERR_REPORT_NAME_EMPTY";
        #endregion

        #region Employee Leave Entitlement
        public const string CONST_ERR_LEAVE_TYPE_EMPTY = "ERR_LEAVE_TYPE_EMPTY";
        public const string CONST_ERR_ENTITLEMENT_EMPTY = "ERR_ENTITLEMENT_EMPTY";
        public const string CONST_ERR_PERIOD_START_EMPTY = "ERR_PERIOD_START_EMPTY";
        public const string CONST_ERR_PERIOD_END_EMPTY = "ERR_PERIOD_END_EMPTY";
        public const string CONST_ERR_PERIOD_GRETER = "ERR_PERIOD_GRETER"; //Period End grather than Period Start
        public const string CONST_ERR_STRUCTURE_FILTER_EMPTY = "ERR_STRUCTURE_FILTER_EMPTY"; // Please select Structure Employee
        public const string CONST_ERR_EMPLOYEE_EMPTY = "ERR_EMPLOYEE_EMPTY";
        public const string CONST_ERR_LEAVE_ALREADY_EXIST = "ERR_LEAVE_ALREADY_EXIST";
        #endregion

        #region Blog
        public const string CONST_ERR_TITLE_ALREADY_EXIST = "ERR_TITLE_ALREADY_EXIST";
        #endregion
        #region Template Conversion
        public const string CONST_ERR_TEMPLATE_TYPE_EMPTY = "ERR_TEMPLATE_TYPE_EMPTY";
        public const string CONST_ERR_FILE_DOWNLOAD_EMPTY = "ERR_FILE_DOWNLOAD_EMPTY";
        public const string CONST_ERR_TEMP_COMP_CODE_EMPTY = "ERR_TEMP_COMP_CODE_EMPTY";
        public const string CONST_ERR_TEMP_DATE_OF_HIRE_EMPTY = "ERR_TEMP_DATE_OF_HIRE_EMPTY";
        public const string CONST_ERR_TEMP_TEMPLATE_TYPE_DOESNT_EXIST = "ERR_TEMP_TEMPLATE_TYPE_DOESNT_EXIST";
        public const string CONST_ERR_TEMP_EMPLOYEE_ID_DOESNT_EXIST = "ERR_TEMP_EMPLOYEE_ID_DOESNT_EXIST";
        public const string CONST_ERR_INCONSISTENT_EMPLOYEE_NAME = "ERR_INCONSISTENT_EMPLOYEE_NAME";

        #endregion

        #region Report 1721 A1
        public const string CONST_ERR_1721_A1_SIGNATURE_IS_EMPTY = "ERR_1721_A1_SIGNATURE_IS_EMPTY";
        public const string CONST_ERR_TAX_PERIOD_TO_LESS_THAN_TAX_PERIOD_FROM = "ERR_TAX_PERIOD_TO_LESS_THAN_TAX_PERIOD_FROM";
        public const string CONST_ERR_TAX_PERIOD_FROM_AND_TAX_PERIOD_TO_SHOULD_BE_SAME_YEAR = "ERR_TAX_PERIOD_FROM_AND_TAX_PERIOD_TO_SHOULD_BE_SAME_YEAR";
        #endregion

        #region Report Attendance
        public const string CONST_ERR_START_TO_LESS_THAN_START_FROM = "ERR_START_TO_LESS_THAN_START_FROM";
        public const string CONST_ERR_START_FROM_IS_EMPTY = "ERR_START_FROM_IS_EMPTY";
        public const string CONST_ERR_START_TO_IS_EMPTY = "ERR_START_TO_IS_EMPTY";
        #endregion

        #region Generate Online Payment
        public const string CONST_ERR_GOP_FILE_NAME = "ERR_GOP_FILE_NAME"; // File Name Must Be Filled
        public const string CONST_ERR_GOP_FILE_TYPE = "ERR_GOP_FILE_TYPE"; // Select File Type
        public const string CONST_ERR_GOP_REFERENCE = "ERR_GOP_REFERENCE"; // Reference Must Be Filled
        public const string CONST_ERR_GOP_TRANSFER_MESS = "ERR_GOP_TRANSFER_MESS"; // Transfer Message Must Be Filled
        public const string CONST_ERR_GOP_SKN_MIN_MAX = "ERR_GOP_SKN_MIN_MAX"; // Transfer SKN Invalid Value
        public const string CONST_ERR_GOP_RTGS_MIN_MAX = "ERR_GOP_RTGS_MIN_MAX"; // Transfer RTGS Invalid Value
        public const string CONST_ERR_GOP_TRANSFER_TYPE_EMPTY = "ERR_GOP_TRANSFER_TYPE_EMPTY"; // Please Select Transfer Type
        public const string CONST_ERR_SOURCE_BANK_EMPTY = "ERR_SOURCE_BANK_EMPTY"; // Please Select Employee Bank
        public const string CONST_ERR_EMPLOYEE_BANK_EMPTY = "EMPLOYEE_BANK_EMPTY"; // Please Select Employee Bank
        public const string CONST_ERR_GOP_WARNING_INSUFFICIENT_BALANCE = "ERR_GOP_WARNING_INSUFFICIENT_BALANCE";
        public const string CONST_ERR_GOP_WARNING_INSUFFICIENT_BALANCE_FAILED_APPROVE = "ERR_GOP_WARNING_INSUFFICIENT_BALANCE_FAILED_APPROVE";
        public const string CONST_ERR_GOP_WARNING_INSUFFICIENT_BALANCE_FAILED = "ERR_GOP_WARNING_INSUFFICIENT_BALANCE_FAILED";
        public const string CONST_ERR_GOP_CUT_OFF_TIME_SKN = "ERR_GOP_CUT_OFF_TIME_SKN"; // Cut Off Time SKN
        public const string CONST_ERR_GOP_CUT_OFF_TIME_RTGS = "ERR_GOP_CUT_OFF_TIME_RTGS"; // Cut Off Time SKN
        public const string CONST_ERR_GOP_REJECTED = "ERR_GOP_REJECTED"; //Rejected

        public const string CONST_GOP_CODE_STATUS_SUCCESS = "API-000";
        public const string CONST_GOP_CODE_STATUS_TRANSACTION_FAILURE = "API-001";
        public const string CONST_GOP_CODE_STATUS_GENERAL_FAILURE = "API-002";
        public const string CONST_GOP_CODE_STATUS_CUT_OFF = "API-003";
        public const string CONST_GOP_CODE_STATUS_TIMEOUT = "GatewayTimeout";

        public const string CONST_GOP_DESC_SUCCESS = "Success";
        public const string CONST_GOP_DESC_TRANSACTION_FAILURE = "Transaction Failure";
        public const string CONST_GOP_DESC_GENERAL_FAILURE = "General Failure";

        #region Status Per Account
        public const string CONST_GOP_PAYMENT_PER_ACCOUNT_PENDING_SUBMIT = "Pending Submit"; //Action Save
        public const string CONST_GOP_PAYMENT_PER_ACCOUNT_CANCEL_SUBMIT = "Cancel Submit"; //Action Reject (can reproccessed)
        public const string CONST_GOP_PAYMENT_PER_ACCOUNT_SUBMIT = "Submitted"; //Action Approve OtherBank
        public const string CONST_GOP_PAYMENT_PER_ACCOUNT_SUCCESS_TRANSFER = "Successfully Transferred"; //Action Approve Overbooking and Otherbank(info success BI)
        public const string CONST_GOP_PAYMENT_PER_ACCOUNT_RETUR_FAILED = "Failed"; //Action Approve (can reproccessed)
        #endregion Status Per Account

        #region Status Per Batch
        public const string CONST_GOP_PAYMENT_PER_BACTH_REJECTED = "Rejected";
        public const string CONST_GOP_PAYMENT_PER_BACTH_SUCCESS = "Success";
        public const string CONST_GOP_PAYMENT_PER_BACTH_PARTIAL_SUCCESS = "Need Action";
        public const string CONST_GOP_PAYMENT_PER_BACTH_RETUR_FAILED = "Failed";
        #endregion Status Per Batch

        #endregion Generate Online Payment

        #region Error Page
        public const string CONST_ERR_FUNCTION_PAGE = "ERR_FUNCTION_PAGE";
        public const string CONST_ERR_BADREQUEST_PAGE = "ERR_BADREQUEST_PAGE";
        public const string CONST_ERR_NOTFOUND_PAGE = "ERR_NOTFOUND_PAGE";
        public const string CONST_ERR_FORBIDDEN_PAGE = "ERR_FORBIDDEN_PAGE";
        public const string CONST_ERR_URLTOOLONG_PAGE = "ERR_URLTOOLONG_PAGE";
        public const string CONST_ERR_SERVICEUNAVAILABLE_PAGE = "ERR_SERVICEUNAVAILABLE_PAGE";
        public const string CONST_ERR_UPLOAD_ATTENDANCE_SELECT_TEMPLATE = "ERR_UPLOAD_ATTENDANCE_SELECT_TEMPLATE";
        public const string CONST_ERR_UPLOAD_ATTENDANCE_CUT_PERIOD = "ERR_UPLOAD_ATTENDANCE_CUT_PERIOD";
        #endregion

        #region
        public const string CONST_ERR_REPORT_EMPLOYEE_TAX_CALCULATION = "ERR_REPORT_EMPLOYEE_TAX_CALCULATION";//When you select specific Location and Department, it will be filtered based on actual location and department when calculation was processed.
        #endregion
		
		#region Attendance Synchronization
        public const string CONST_ERR_UPLOAD_ATTENDANCE_IN_PROGRESS_EXIST = "ERR_UPLOAD_ATTENDANCE_IN_PROGRESS_EXIST";
        #endregion

        #region MyAttendance
        public const string CONST_ERR_ESS_ATTENDANCE_LIST_EMPTY_FILTER = "ERR_ESS_ATTENDANCE_LIST_EMPTY_FILTER";
        public const string CONST_ERR_ESS_ATTENDANCE_LIST_TO_LESS_FROM = "ERR_ESS_ATTENDANCE_LIST_TO_LESS_FROM";
        public const string CONTS_ERR_ATTENDANCE_CUT_PERIOD_NOT_FOUND = "ERR_ATTENDANCE_CUT_PERIOD_NOT_FOUND";
        #endregion

        #region MyLeave
        public const string CONST_ERR_ESS_LEAVE_EXIST = "ERR_ESS_LEAVE_EXIST";
		  public const string CONST_ERR_ESS_LEAVE_LIST_FROM_GREATER_TO = "ERR_ESS_LEAVE_LIST_FROM_GREATER_TO";
        #endregion
		
		#region ApprovalLeave
        public const string CONST_ERR_ESS_APPROVAL_LEAVE_FROM_GREATER_TO = "ERR_ESS_LEAVE_LIST_FROM_GREATER_TO";
        #endregion

        #region Generate Payroll Variable
        public const string CONST_ERR_REPORT_VARIABLE_CUT_OFF_CANNOT_BE_EMPTY = "ERR_REPORT_VARIABLE_CUT_OFF_CANNOT_BE_EMPTY";
        #endregion Generate Payroll Variable

        #endregion

        #region Mobile Constan
        public const string CONST_STR_STATUS_LEAVE_PENDING = "Pending";
        public const string CONST_STR_STATUS_LEAVE_APPROVED = "Approved";
        public const string CONST_STR_STATUS_LEAVE_CANCEL = "Cancel";
        public const string CONST_STR_STATUS_LEAVE_REJECT = "Reject";
        #endregion

        #region Mobile Constan TL
        public const string CONST_STR_STATUS_TIMELINE_PENDING = "pending";
        public const string CONST_STR_STATUS_TIMELINE_APPROVED = "approved";
        public const string CONST_STR_STATUS_TIMELINE_CANCEL = "canceled";
        public const string CONST_STR_STATUS_TIMELINE_REJECT = "rejected";
        #endregion

    }

}