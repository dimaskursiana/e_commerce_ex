using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP_MODEL.ModelData;
using System.Web.Mvc;

namespace APP_COMMON
{
    public static class UICache
    {
        /// <summary>
        /// Created By : Ali Mubarokah
        /// Created Date : 20 Feb 2017
        /// Purpose : UI CACHE 

        #region Format UICache
        //public static void Name_Model_Set(CLASS_MODEL Model, string Key)
        //{
        //    System.Web.HttpContext.Current.Cache[Key] = Model;
        //}

        //public static Class_Model Name_Model_Get(string Key)
        //{
        //    var Model = (CLASS_MODEL)System.Web.HttpContext.Current.Cache[Key];
        //    if (Model == null)
        //    {
        //        Model = new CLASS_MODEL();
        //        Model.pageQuery = new PageQuery();
        //        Model.pageQuery.CheckedValue = "";
        //    }
        //    return Model;
        //}
        #endregion


        #region Funtion UICache

        #region String
        public static void String_Set(string strData, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = strData;
        }

        public static string String_Get(string Key)
        {
            var strData = (string)System.Web.HttpContext.Current.Cache[Key];
            if (strData == null)
            {
                strData = string.Empty;
            }
            return strData;
        }
        #endregion

        #region Parameter
        public static void Static_Parameters_Set(List<tbl_SysParam> Parameters, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Parameters;
        }

        public static List<tbl_SysParam> Static_Parameters_Get(string Key)
        {
            var Parameters = (List<tbl_SysParam>)System.Web.HttpContext.Current.Cache[Key];
            if (Parameters == null)
            {
                Parameters = new List<tbl_SysParam>();
            }
            return Parameters;
        }
        #endregion

        #region Cache Menu

        public static void menu_selected_Set(string Menu, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Menu;
        }

        public static string menu_selected_Get(string Key)
        {
            var Menu = (string)System.Web.HttpContext.Current.Cache[Key];
            if (Menu == null)
            {
                Menu = "";
            }
            return Menu;
        }

        public static void tbl_Menu_Set(tbl_Menu Model, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Model;
        }

        public static void vw_User_Menu_Set(vw_User_Menu Model, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Model;
        }

        public static tbl_Menu Tbl_Menu_Get(string Key)
        {
            var Model = (tbl_Menu)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new tbl_Menu();
            }
            return Model;
        }

        public static Global_GenerateBankFile generateBankFile_Get(string Key)
        {
            var Model = (Global_GenerateBankFile)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_GenerateBankFile();
            }
            return Model;
        }
        public static void generateBankFile_Set(Global_GenerateBankFile Model, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Model;
        }


        #endregion




        #region Cache Notification
        public static void Notification_Set(NotificationData Model, string Key)
        {
            System.Web.HttpContext.Current.Cache[Key] = Model;
        }

        public static NotificationData Notification_Get(string Key)
        {
            var Model = (NotificationData)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new NotificationData();
            }
            return Model;
        }
        #endregion

        #region Cache User

        //public static List<vw_user> VW_USER_GET(string Key)
        //{
        //    return (List<vw_user>)System.Web.HttpContext.Current.Cache[key];
        //}

        //public static void VW_USER_SET(List<vw_user> vw_user, string key)
        //{
        //    System.Web.HttpContext.Current.Cache[key] = vw_user;
        //}

        #endregion

        #region Cache General Parameter

        public static void LIST_TBL_GENERAL_PARAMETER_SET(List<tbl_General_Parameter> tbl_generalparameterlist, string key)
        {
            System.Web.HttpContext.Current.Cache[key] = tbl_generalparameterlist;
        }

        public static List<tbl_General_Parameter> LIST_TBL_GENERAL_PARAMETER_GET(string Key)
        {
            return (List<tbl_General_Parameter>)System.Web.HttpContext.Current.Cache[Key];
        }

        #endregion

        #region Cache vw_Generate_Bank_File_Details_Summary

        public static void LIST_VW_GENERATE_BANK_FILE_DETAILS_SUMMARY_SET(List<vw_Generate_Bank_File_Details_Summary> vw_Generate_Bank_File_Details_Summary, string key)
        {
            System.Web.HttpContext.Current.Cache[key] = vw_Generate_Bank_File_Details_Summary;
        }

        public static List<vw_Generate_Bank_File_Details_Summary> LIST_VW_GENERATE_BANK_FILE_DETAILS_SUMMARY_GET(string Key)
        {
            return (List<vw_Generate_Bank_File_Details_Summary>)System.Web.HttpContext.Current.Cache[Key];
        }

        #endregion


        #region DataCache
        public static void DataCache_GlobalgeneralParameterModels_Set(Global_General_Parameter model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_General_Parameter DataCache_GlobalgeneralParameterModels_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_General_Parameter)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_General_Parameter();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_GlobalPayrollSchedule_Set(Global_Payroll_Schedule model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Payroll_Schedule DataCache_GlobalPayrollSchedule_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Schedule)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Schedule();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }



        public static void DataCache_globalBankInformation_Set(Global_Bank_Information model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Bank_Information DataCache_globalBankInformation_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Bank_Information)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Bank_Information();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }



        public static void DataCache_globalEmployeePayment_Set(Global_Employee_Payment_Information model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Payment_Information DataCache_globalEmployeePayment_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Payment_Information)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Payment_Information();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalEmployeeIndex_Set(Global_Employee_Personal_Info model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Personal_Info DataCache_globalEmployeeIndex_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Personal_Info)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Personal_Info();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalEmployeeTaxInfoIndex_Set(Global_Employee_Tax_Info model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Tax_Info DataCache_globalEmployeeTaxInfoIndex_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Tax_Info)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Tax_Info();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalPayslipInformaation_Set(Global_Employee_Payslip_Info model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Payslip_Info DataCache_globalPayslipInformaation_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Payslip_Info)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Payslip_Info();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalEmployeeAddressIndex_Set(Global_Employee_Address model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Address DataCache_globalEmployeeAddressIndex_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Address)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Address();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalEmployeeAppointment_Set(Global_Employee_Appointment model, string idcache)
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Appointment DataCache_globalEmployeeAppointment_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Appointment)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Appointment();
                Model.pageQuery = new PageQuery();
                Model.ListData = new List<tbl_Appointment_Status_Information>();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalPayrollReportGeneration_Set(Global_Payroll_Report_Generation model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }

        public static Global_Payroll_Report_Generation DataCache_globalPayrollReportGeneration_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Report_Generation)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Report_Generation();
                Model.ListEmployee = new List<tbl_Employee>();
                Model.ListReportEmployee = new List<Get_Report_Employee_Information_Result>();
                Model.SaveFilterReportEmployee = new List<Get_Report_Employee_Information_Result>();
                Model.ListOrganizationID = new List<Guid?>();
                Model.ListPayrollPeriodID = new List<Guid>();
            }
            return Model;
        }



        public static void DataCache_globalReportEmployeeTaxCalculation_Set(Global_Report_Employee_Tax_Calculation model, string idcache)
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Report_Employee_Tax_Calculation DataCache_globalReportEmployeeTaxCalculation_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Employee_Tax_Calculation)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Employee_Tax_Calculation();
                Model.pageQuery = new PageQuery();
                Model.ListData = new List<tbl_Report_Employee>();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalRoleMenuFunction_Set(Global_RoleMenuFunction model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_RoleMenuFunction DataCache_globalRoleMenuFunction_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_RoleMenuFunction)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_RoleMenuFunction();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalHolidayCalendar_Set(Global_Holiday_Calendar model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static Global_Holiday_Calendar DataCache_globalHolidayCalendar_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Holiday_Calendar)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Holiday_Calendar();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_payrollCalculation_Set(Global_Payroll_Calculation model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static Global_Payroll_Calculation DataCache_payrollCalculation_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Calculation)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Calculation();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_payrollClosing_Set(Global_Payroll_Closing model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static Global_Payroll_Closing DataCache_payrollClosing_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Closing)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Closing();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_payrollSlip_Set(Global_Payroll_Slip model, string idcache = "")
        {
            if (idcache != null)
            {
                if (model.pageQuery == null)
                    model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }

        public static Global_Payroll_Slip DataCache_payrollSlip_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Slip)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Slip();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalUser_Set(Global_User model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_User DataCache_globalUser_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_User)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_User();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_EMPLeaveMaster_Set(Global_Employee_Leave model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Leave DataCache_EMPLeaveMaster_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Leave)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Leave();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalGeneralSetting_Set(Global_General_Parameter model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static void DataCache_GlobalExchangeFile_Set(Global_Exchange_File model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Exchange_File DataCache_GlobalExchangeFile_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Exchange_File)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Exchange_File();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_GlobalEmployeeDocument_Set(Global_Employee_Document model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Document DataCache_GlobalEmployeeDocumente_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Document)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Document();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static Global_General_Parameter DataCache_globalGeneralSetting_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_General_Parameter)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_General_Parameter();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalExchangeRate_Set(Global_Exchange_Rate model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Exchange_Rate DataCache_globalExchangeRate_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Exchange_Rate)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Exchange_Rate();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationGroup_Set(Global_Organization_Group model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Group DataCache_globalOrganizationGroup_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Group)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Group();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalBaseLocation_Set(Global_Base_Location model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }
        public static Global_Base_Location DataCache_globalBaseLocation_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Base_Location)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Base_Location();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganization_Set(Global_Organization model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization DataCache_globalOrganization_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalLeave_Set(Global_Leave_Types model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Leave_Types DataCache_globalLeave_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Leave_Types)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Leave_Types();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }



        public static void DataCache_globalTaxPeriod_Set(Global_Tax_Period model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Tax_Period DataCache_globalTaxPeriod_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Tax_Period)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Tax_Period();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationStructure_Set(Global_Organization_Structure model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Structure DataCache_globalOrganizationStructure_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Structure)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Structure();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationPayrollComponent_Set(Global_Organization_Payroll_Component model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Payroll_Component DataCache_globalOrganizationPayrollComponent_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Payroll_Component)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Payroll_Component();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalEmployeePayrollComponent_Set(Global_Employee_Payroll_Component model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Payroll_Component DataCache_globalEmployeePayrollComponent_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Payroll_Component)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Payroll_Component();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_globalPayrollVariable_Set(Global_Payroll_Variable model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Payroll_Variable DataCache_globalPayrollVariable_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Variable)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Variable();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_globalAddPayroll_Set(Global_Additional_Payroll model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Additional_Payroll DataCache_globalAddPayroll_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Additional_Payroll)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Additional_Payroll();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalPayrollReportSetting_Set(Global_Report_Setting model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Report_Setting DataCache_globalPayrollReportSetting_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Setting)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Setting();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_globalReportTaxSummary_Set(Global_Tax_Summary_Report model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Tax_Summary_Report DataCache_globalReportTaxSummary_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Tax_Summary_Report)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Tax_Summary_Report();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalContactPerson_Set(Global_Contact_Person model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Contact_Person DataCache_globalContactPerson_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Contact_Person)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Contact_Person();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalHOBRANCH_Set(Global_HOBRANCH model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_HOBRANCH DataCache_globalHOBRANCH_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_HOBRANCH)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_HOBRANCH();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalBPJS_Healthcare_Set(Global_BPJS_Healthcare model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_BPJS_Healthcare DataCache_globalBPJS_Healthcare_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_BPJS_Healthcare)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_BPJS_Healthcare();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalBPJS_Manpower_Set(Global_BPJS_Manpower model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_BPJS_Manpower DataCache_globalBPJS_Manpower_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_BPJS_Manpower)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_BPJS_Manpower();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalHolidayCalendarOrganization_Set(Global_Holiday_Calender_Organization model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Holiday_Calender_Organization DataCache_globalHolidayCalendarOrganization_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Holiday_Calender_Organization)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Holiday_Calender_Organization();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationAccountGrubMaintenence_Set(Global_Organization_Account_Group_Maintenance model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Account_Group_Maintenance DataCache_globalOrganizationAccountGrubMaintenence_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Account_Group_Maintenance)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Account_Group_Maintenance();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationTeam_Set(Global_Organization_Team model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Team DataCache_globalOrganizationTeam_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Team)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Team();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalOrganizationWorkkingTime_Set(Global_Organization_Working_Time model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Working_Time DataCache_globalOrganizationWorkkingTime_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Working_Time)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Working_Time();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalBankSetting_Set(Global_Bank_Setting model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Bank_Setting DataCache_globalBankSetting_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Bank_Setting)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Bank_Setting();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalPayrollPeriod_Set(Global_Payroll_Period model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Payroll_Period DataCache_globalPayrollPeriod_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Period)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Period();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_NewUpload_Set(Global_Upload_Data model, string idcache = "")
        {
            //SynchronizationContext syncCtx = SynchronizationContext.Current
            if (idcache != null) { System.Web.HttpRuntime.Cache[idcache] = model; }
        }

        public static Global_Upload_Data DataCache_NewUpload_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Upload_Data)System.Web.HttpRuntime.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Upload_Data();
                Model._uploadData = new _uploadData();
                Model.ListColoumnUpload = new ListColoumnUpload();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_Global_GenerateBankFile_Set(Global_GenerateBankFile model, string idcache = "")
        {
            if (idcache != null) { System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_GenerateBankFile DataCache_Global_GenerateBankFile_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_GenerateBankFile)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_GenerateBankFile();
                Model.vwGenerateBankFileDetailsSummaryList = new List<vw_Generate_Bank_File_Details_Summary>();
            }
            return Model;
        }


        public static void DataCache_globalLoan_Set(Global_Loan model, string idcache = "")
        {
            if (idcache != null) { System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Loan DataCache_globalLoan_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Loan)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Loan();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        //public static void DataCache_globalPayrollPeriod_Set(Global_Payroll_Period model, string idcache = "")
        //{
        //    if (idcache != null) {             if (idcache != null) { if (model.pageQuery == null)model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        //}

        //public static Global_Payroll_Period DataCache_globalPayrollPeriod_Get(string Key)
        //{
        //    Key = Key == null ? "" : Key;
        //    var Model = (Global_Payroll_Period)System.Web.HttpContext.Current.Cache[Key];
        //    if (Model == null)
        //    {
        //        Model = new Global_Payroll_Period();
        //        Model.pageQuery = new PageQuery();
        //        Model.pageQuery.CheckedValue = "";
        //    }
        //    return Model;
        //}

        public static void DataCache_globalComAndBen_Set(Global_Compensation_Benefit model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Compensation_Benefit DataCache_globalComAndBen_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Compensation_Benefit)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Compensation_Benefit();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalInactiveBPJSDownload_Set(Global_Inactive_BPJS_Manpower model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Inactive_BPJS_Manpower DataCache_globalInactiveBPJSDownload_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Inactive_BPJS_Manpower)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Inactive_BPJS_Manpower();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalNewRegisterBPJSDownload_Set(Global_New_Register_BPJS_Manpower model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_New_Register_BPJS_Manpower DataCache_globalNewRegisterBPJSDownload_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_New_Register_BPJS_Manpower)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_New_Register_BPJS_Manpower();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalOrganizationEmailSetup_Set(Global_Organization_Email_Setup model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Organization_Email_Setup DataCache_globalOrganizationEmailSetup_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Organization_Email_Setup)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Organization_Email_Setup();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        //public static void DataCache_vwGlobalPayrollSlipEmployee_Set(Global_Epayslip_Banner model, string idcache = "")
        //{
        //    if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        //}

        //public static Global_VW_Payroll_Slip_Employee_Summary DataCache_vwGlobalPayrollSlipEmployee_Get(string Key)
        //{
        //    Key = Key == null ? "" : Key;
        //    var Model = (Global_VW_Payroll_Slip_Employee_Summary)System.Web.HttpContext.Current.Cache[Key];
        //    if (Model == null)
        //    {
        //        Model = new Global_VW_Payroll_Slip_Employee_Summary();
        //        Model..pageQuery = new PageQuery();
        //        Model.pageQuery.CheckedValue = "";
        //    }
        //    return Model;
        //}

        public static void DataCache_globalTaxReportingA1_Set(Global_Tax_Reporting_A1 model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Tax_Reporting_A1 DataCache_globalTaxReportingA1_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Tax_Reporting_A1)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Tax_Reporting_A1();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalHistoryReport_Set(Global_History_Report model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_History_Report DataCache_globalHistoryReport_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_History_Report)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_History_Report();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalReport1721Final_Set(Global_Report_1721_Final model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Report_1721_Final DataCache_globalReport1721Final_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_1721_Final)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_1721_Final();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalReport1721NotFinal_Set(Global_Report_1721_Not_Final model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Report_1721_Not_Final DataCache_globalReport1721NotFinal_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_1721_Not_Final)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_1721_Not_Final();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        public static void DataCache_globalSalaryJournalReport_Set(Global_Salary_Journal_Report model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Salary_Journal_Report DataCache_globalSalaryJournalReport_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Salary_Journal_Report)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Salary_Journal_Report();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalFundRequisition_Set(Global_Fund_Requisition model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Fund_Requisition DataCache_globalFundRequisition_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Fund_Requisition)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Fund_Requisition();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalPayrollComparativeReport_Set(Global_Payroll_Comparative_Report model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Payroll_Comparative_Report DataCache_globalPayrollComparativeReport_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Comparative_Report)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Comparative_Report();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalGenerateTemplateAdditional_Set(Global_Generate_Template_Additional model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Generate_Template_Additional DataCache_globalGenerateTemplateAdditional_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Generate_Template_Additional)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Generate_Template_Additional();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_globalReport1721Bulanan_Set(Global_Report_1721_Bulanan model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Report_1721_Bulanan DataCache_globalReport1721Bulanan_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_1721_Bulanan)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_1721_Bulanan();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_PayslipTemplate_Set(Global_Report_Employee_Payslip_Template model, string idcache = "")
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }

        public static Global_Report_Employee_Payslip_Template DataCache_PayslipTemplate_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Employee_Payslip_Template)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Employee_Payslip_Template();
                Model.PayslipSetting = new tbl_Payslip_Setting();
                Model.PayslipTemplate = new tbl_Payslip_Template();
            }
            return Model;
        }

        public static void DataCache_globalEmployeeDeductionScheduleController_Set(Global_Employee_Deduction_Schedule_Report model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Employee_Deduction_Schedule_Report DataCache_globalEmployeeDeductionScheduleController_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Employee_Deduction_Schedule_Report)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Employee_Deduction_Schedule_Report();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }


        public static void DataCache_globalSystemParameter_Set(Global_System_Parameter model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static Global_System_Parameter DataCache_globalSystemParameter_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_System_Parameter)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_System_Parameter();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        #endregion


        #region Cache Template Conversion
        public static void DataCache_globalTemplateConversion_Set(Global_Template_Conversion model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Template_Conversion DataCache_globalTemplateConversion_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Template_Conversion)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Template_Conversion();
                Model.ColoumTemplateConversion = new ColoumTemplateConversion();
                Model.ListDataUpload = new List<ColoumTemplateConversion>();
            }
            return Model;
        }
        #endregion Cache Template Conversion

        #region Cache Approva Hierarchy
        public static void DataCache_globalApprovalHierarchy_Set(Global_Approval_Hierarchy model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }
        public static Global_Approval_Hierarchy DataCache_globalApprovalHierarchy_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Approval_Hierarchy)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Approval_Hierarchy();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
        #endregion

        public static void DataCache_globalBlog_Set(Global_Blog model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Blog DataCache_globalBlog_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Blog)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Blog();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        


        #region Cache Report Attendance
        public static void DataCache_globalReportAttendance_Set(Global_Report_Attendance model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Report_Attendance DataCache_globalReportAttendance_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Attendance)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Attendance();
                Model.ListEmployee = new List<tbl_Employee>();
                Model.ListReportEmployee = new List<Get_Employee_Report_Attendance_Result>();
                Model.ListReportAttendance = new List<Get_Report_Attendance_Result>();
            }
            return Model;
        }
        #endregion

        #region Cache Report Apply Leave
        public static void DataCache_globalReportLeave_Set(Global_Report_Leave model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Report_Leave DataCache_globalReportLeave_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Leave)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Leave();
                Model.ListEmployee = new List<tbl_Employee>();
                Model.ListReportLeave = new List<Get_Report_Leave_Result>();
            }
            return Model;
        }
        #endregion

        #region Cache Report Claim
        public static void DataCache_globalReportClaim_Set(Global_Report_Claim model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Report_Claim DataCache_globalReportClaim_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Claim)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Claim();
                Model.ListEmployee = new List<tbl_Employee>();
                Model.ListReportClaim = new List<Get_Report_Claim_Result>();
            }
            return Model;
        }
        #endregion

        #region Cache Report Overtime
        public static void DataCache_globalReportOvertime_Set(Global_Report_Overtime model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Report_Overtime DataCache_globalReportOvertime_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Overtime)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Overtime();
                Model.ListEmployee = new List<tbl_Employee>();
                Model.ListReportOvertime = new List<Get_Report_Overtime_Result>();
            }
            return Model;
        }
        #endregion

        #region Cache Generate Online Payment
        public static void DataCache_globalGenerateOnlinePayment_Set(Global_Generate_Online_Payment model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Generate_Online_Payment DataCache_globalGenerateOnlinePayment_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Generate_Online_Payment)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Generate_Online_Payment();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
                Model.ListViewPayrollPaymentSummary = new List<vw_Payroll_Payment_Summary>();
            }
            return Model;
        }

        #region Cache Payroll Portal
        public static void DataCache_Global_User_Trial_Set(Global_Payroll_Portal model, string idcache = "")
        {
            if (idcache != null) { System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Payroll_Portal DataCache_Global_User_Trial_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payroll_Portal)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payroll_Portal();
                Model.UserTrialList = new List<tbl_User_Trial>();
            }
            return Model;
        }
        #endregion

        #region Cache Blog
        public static void DataCache_globalBlogPost_Set(Global_Blog_Post model, string idcache = "")
        {
            if (idcache != null)
            {
                if (idcache != null)
                {
                    if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model;
                }
            }
        }

        public static Global_Blog_Post DataCache_globalBlogPost_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Blog_Post)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Blog_Post();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_RequestPayment_Set(Global_Payment_Order model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static Global_Payment_Order DataCache_RequestPayment_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Payment_Order)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Payment_Order();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_APIPaymentMidtrans_Set(GlobalAPIOnlinePaymentMidtrans model, string idcache = "")
        {
            if (idcache != null) { if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; } }
        }

        public static GlobalAPIOnlinePaymentMidtrans DataCache_APIPaymentMidtrans_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (GlobalAPIOnlinePaymentMidtrans)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new GlobalAPIOnlinePaymentMidtrans();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        #endregion

        #endregion

        #region Generate Payroll Variable Report
        public static void DataCache_globalReportPayrollVariable_Set(Global_Report_Payroll_Variable model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Report_Payroll_Variable DataCache_globalReportPayrollVariable_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Report_Payroll_Variable)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Report_Payroll_Variable();
            }
            return Model;
        }
        #endregion

        #region CacheEmailkey
        public static void DataCache_globalEmailkey_Set(Global_Cache_Key model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_Cache_Key DataCache_globalEmailkey_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Cache_Key)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Cache_Key();
                Model.Employee_Id = Guid.Empty;
            }
            return Model;
        }
        #endregion

        #region Cache Attendance Sync
        public static void DataCache_globalAttendanceSynchronization_Set(Global_Attendance_Synchronization model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static Global_Attendance_Synchronization DataCache_globalAttendanceSynchronization_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_Attendance_Synchronization)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_Attendance_Synchronization();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }
 #endregion
        public static void DataCache_globalMyAttendance_Set(GlobalMyAttendance model,string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static GlobalMyAttendance DataCache_globalMyAttendance_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (GlobalMyAttendance)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new GlobalMyAttendance();
            }
            return Model;
        }

       
 public static void DataCache_globalAttendanceRequet_Set(GlobalEmployeAttendance model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static GlobalEmployeAttendance DataCache_globalAttendanceRequet_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (GlobalEmployeAttendance)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new GlobalEmployeAttendance();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
            }
            return Model;
        }

        public static void DataCache_Selectlistitem_Set(List<SelectListItem> model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static List<SelectListItem> DataCache_Selectlistitem_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (List<SelectListItem>)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new List<SelectListItem>();
            }
            return Model;
        }

        public static void DataCache_SesionMobile_Set(tbl_Session_Mobile model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static tbl_Session_Mobile DataCache_SesionMobile_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (tbl_Session_Mobile)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new tbl_Session_Mobile();
            }
            return Model;
        }

        public static void DataCache_globalMyLeave_Set(GlobalMyLeave model, string idcache)
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static GlobalMyLeave DataCache_globalMyLeave_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (GlobalMyLeave)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new GlobalMyLeave();
            }
            return Model;
        }
		
		public static void DataCache_globalApprovalLeave_Set(GlobalApprovalLeave model, string idcache = "")
        {
            if (idcache != null) { if (model.pageQuery == null) model.pageQuery = new PageQuery(); model.pageQuery.CheckedValue = UICommonFunction.MergeCheckedUnchecked(model.pageQuery.CheckedValue, model.pageQuery.unCheckedValue); System.Web.HttpContext.Current.Cache[idcache] = model; }
        }

        public static GlobalApprovalLeave DataCache_globalApprovalLeave_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (GlobalApprovalLeave)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new GlobalApprovalLeave();
                Model.pageQuery = new PageQuery();
                Model.pageQuery.CheckedValue = "";
                Model.ListViewEmployeeLeave = new List<vw_Employee_Leave>();
            }
            return Model;
        }

        public static void DataCache_SessionKey_Set (Global_SessionKey model, string idcache="")
        {
            if (idcache != null)
            {
                System.Web.HttpContext.Current.Cache[idcache] = model;
            }
        }
        public static Global_SessionKey DataCache_SessionKey_Get(string Key)
        {
            Key = Key == null ? "" : Key;
            var Model = (Global_SessionKey)System.Web.HttpContext.Current.Cache[Key];
            if (Model == null)
            {
                Model = new Global_SessionKey();
            }
            return Model;
        }
		
        #endregion
    }
}