using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APP_MODEL.ModelData
{
    /// <summary>
    /// Created By : Ali Mubarokah
    /// Created Date : 20 Feb 2017
    /// Purpose : Global Class Model 
    /// 
    /// Purpose : Pagging Data
    public class PageQuery
    {
        public string idcache { get; set; }
        public Guid id { get; set; }
        public string filterType { get; set; }
        public string filterField { get; set; }
        public string filterField2 { get; set; }
        public string filterField3 { get; set; }
        public string filterField4 { get; set; }
        public string filterString { get; set; }
        public string filterString2 { get; set; }
        public string filterString3 { get; set; }
        public string filterString4 { get; set; }
        public string filterString5 { get; set; }
        public string sortOrder { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string strFrom { get; set; }
        public string strTo { get; set; }
        public string CurrentSort { get; set; }
        public string SortDirection { get; set; }
        public int CurrentPageSize { get; set; }
        public int Page { get; set; }
        //public SelectList PageSize { get; set; }
        public int SkipPage { get; set; }
        public int PageCount { get; set; }
        public string CheckedValue { get; set; }
        public string unCheckedValue { get; set; }

        public PageQuery()
        {
            CheckedValue = "";
            unCheckedValue = "";

        }


    }

    public partial class tbl_Attendance_History_Upload
    {
        public string str_Status { get; set; }
    }

    //recap in attendence
    public class Attendance_List
    {
        public int cnt_Recap { get; set; }
        public DateTime? date_param_1 { get; set; }
        public DateTime? date_param_2 { get; set; }
        public DateTime? date_param_3 { get; set; }
        public DateTime? date_param_4 { get; set; }
        public DateTime? date_param_5 { get; set; }
        public DateTime? date_param_6 { get; set; }
        public DateTime? date_param_7 { get; set; }
        public DateTime? date_param_8 { get; set; }
        public DateTime? date_param_9 { get; set; }
        public DateTime? date_param_10 { get; set; }
    }

    public class Attendance_Recap
    {
        public int cnt_Recap { get; set; }

        public int nr { get; set; }
        public string NIK { get; set; }
        public string Employee_Name { get; set; }

        public TimeSpan in_1 { get; set; }
        public TimeSpan out_1 { get; set; }
        public TimeSpan diff_1 { get; set; }

        public TimeSpan in_2 { get; set; }
        public TimeSpan out_2 { get; set; }
        public TimeSpan diff_2 { get; set; }

        public TimeSpan in_3 { get; set; }
        public TimeSpan out_3 { get; set; }
        public TimeSpan diff_3 { get; set; }

        public TimeSpan in_4 { get; set; }
        public TimeSpan out_4 { get; set; }
        public TimeSpan diff_4 { get; set; }

        public TimeSpan in_5 { get; set; }
        public TimeSpan out_5 { get; set; }
        public TimeSpan diff_5 { get; set; }

        public TimeSpan in_6 { get; set; }
        public TimeSpan out_6 { get; set; }
        public TimeSpan diff_6 { get; set; }

        public TimeSpan in_7 { get; set; }
        public TimeSpan out_7 { get; set; }
        public TimeSpan diff_7 { get; set; }

        public TimeSpan in_8 { get; set; }
        public TimeSpan out_8 { get; set; }
        public TimeSpan diff_8 { get; set; }

        public TimeSpan in_9 { get; set; }
        public TimeSpan out_9 { get; set; }
        public TimeSpan diff_9 { get; set; }

        public TimeSpan in_10 { get; set; }
        public TimeSpan out_10 { get; set; }
        public TimeSpan diff_10 { get; set; }

    }




    public class Global_Chat
    {
        public string UserLogin { get; set; }
        public bool isAdmin { get; set; }
        public tbl_Chat_Account Account { get; set; }
        public List<tbl_Chat_Log> List_Chat { get; set; }
        public List<vw_Chat_Account> List_Contact { get; set; }
    }

    public class ChatContact
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool isActive { get; set; }
        public string Email { get; set; }
        public string lastChat { get; set; }
    }

    public class Global_User_Trial
    {
        public Guid OrganizationID { get; set; }
        public Guid OrganizationTeamID { get; set; }
        public Guid RoleID { get; set; }
        public Guid UserID { get; set; }

        public string Mode { get; set; }

        public string selectedOrganizationName { get; set; }
        public tbl_User_Trial userTrialModel { get; set; }
        //organization
        public tbl_Organization organizationModel { get; set; }
        //organization Admin
        public tbl_Organization organizationAdminModel { get; set; }
        //organization Team
        public tbl_Organization_Team organizationTeamModel { get; set; }
        //organization contact person
        public tbl_Contact_Person contactPersonOrganizationModels { get; set; }
        public List<tbl_Client_Organization_Team> clientOrganizationTeamList { get; set; }
        //User
        public tbl_User userModel { get; set; }
        public tbl_Organization_User organizationUserModel { get; set; }
        public tbl_User_Role userRoleModel { get; set; }
        public List<tbl_User_Role> userRoleList { get; set; }
        public tbl_User_Organization_Team userOrganizationTeamModels { get; set; }

    }

    public class GlobalAudit
    {
        public List<vw_AuditTrails> ListAudit { get; set; }
        public string Record_Id { get; set; }
        public List<string> ListRecordId { get; set; }
        public List<tbl_Current_Version> ListCurrentVersion { get; set; }
        public string Authorizedby { get; set; }
        public string Authorizeddate { get; set; }
        public string Modifiedby { get; set; }
        public string Modifieddate { get; set; }
        public string Version { get; set; }
        public string AuthorizedStatus { get; set; }

        public string CurentData_Version { get; set; }
        public string CurentData_Authorizedby { get; set; }
        public string CurentData_AuthorizedStatus { get; set; }
        public string CurentData_Authorizeddate { get; set; }
        public string CurentData_Modifiedby { get; set; }
        public string CurentData_Modifieddate { get; set; }

        public bool isApproveReject { get; set; }
        public bool isReject { get; set; }
        public bool isApprove { get; set; }
    }

    public class _Organization
    {
        public Guid selectedOrganization { get; set; }
        public string selectedOrganizationName { get; set; }
        public List<tbl_Organization> organizationList { get; set; }
    }



    public class SumUnpivot
    {
        public string EmployeeIDNumber { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }
        public string Remark { get; set; }
        public string Run { get; set; }
    }


    public class RuleOfButton
    {
        public int? Status { get; set; }
        public string AddRow { get; set; }
        public string DeleteRow { get; set; }
        public string Upload { get; set; }
        public string Save { get; set; }
        public string Submit { get; set; }
        public string CheckBox { get; set; }
        public bool isRegulator { get; set; }
    }

    public class Global_Report_Employee_Tax_Calculation
    {
        public List<tbl_Organization_Structure> departmentList { get; set; }
        public List<tbl_Employee> employeeList { get; set; }
        public List<vw_TaxPeriodMonth> payrollPeriodList { get; set; }
        public List<vw_Employee_Appointment_Work_Location> vwEmployeeAppointmentWorkLocation { get; set; }
        public List<tbl_Report_Employee> ListData { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string OrganizationSelectedCode { get; set; }
        public PageQuery pageQuery { get; set; }
        public string Tax_Period { get; set; }
        public int Correction { get; set; }
        public string Location { get; set; }
        public string[] Department { get; set; }
        public string[] Employee { get; set; }
        public string typeFileDownload { get; set; }
        public string IdChace { get; set; }
        public string Format_Type { get; set; }
    }

    public class PayrollComparative_Result
    {
        public int Id { get; set; }
        public string type { get; set; }
        public Guid Organization_ID { get; set; }
        public Guid Payroll_Period_ID { get; set; }
        public Guid Employee_ID { get; set; }
        public int RN { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public string Component_Code { get; set; }
        public string Description_After { get; set; }
        public Nullable<double> Value_After { get; set; }
        public string TaxPolicy_After { get; set; }
        public string Description_Before { get; set; }
        public Nullable<double> Value_Before { get; set; }
        public string TaxPolicy_Before { get; set; }
        public string Employment_Status { get; set; }
    }

    public class Comparative_Seq
    {
        public int Seq { get; set; }
        public List<tbl_Organization_Payroll_Component> List_tbl_Organization_Payroll_Component { get; set; }
    }

    public class Global_Base_Location
    {
        public List<tbl_Base_Location> BaseLocationList { get; set; }
        public tbl_Base_Location_Header BaseLocationHeaderModel { get; set; }
        public tbl_Base_Location_Detail BaseLocationDetailModel { get; set; }
        public List<tbl_Base_Location_Detail> ListBaseLocationDetailModel { get; set; }
        public List<vw_Base_Location_List> VwBaseLocationList { get; set; }
        public vw_Base_Location_List VwBaseLocationModel { get; set; }
        public System.Guid idCache { get; set; }
        public PageQuery pageQuery { get; set; }
        public string[] strIdDetail { get; set; }
        public string[] strLocationName { get; set; }
        public string[] strLatitude { get; set; }
        public string[] strLongitude { get; set; }
        public string[] strStartDt { get; set; }
        public string[] strEndDt { get; set; }
        public string[] strRange { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public string OrganizationSelected { get; set; }
    }

    public class Global_Organization
    {
        public List<tbl_Organization> organizationList { get; set; }
        public tbl_Organization organizationModels { get; set; }
        public List<tbl_General_Parameter> OrganizationTypeList { get; set; }
        public List<tbl_General_Parameter> OrganizationServiceList { get; set; }
        public string serviceParentOrganization { get; set; }
        public string IdChace { get; set; }

        public bool isDETAILS { get; set; }
        public string strUploadLogo { get; set; }
        public FileResult Organization_img { get; set; }



        public List<Global_Error_Code> errMessage { get; set; }

        public PageQuery pageQuery { get; set; }
    }

    public class Global_Leave_Types
    {
        public List<tbl_Leave_Types> leaveTypesList { get; set; }
        public tbl_Leave_Types leaveTypesModels { get; set; }
        public string IdChace { get; set; }
        public string strEntitlementSituational { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }



    public class Global_Exchange_File
    {
        public List<tbl_Exchange_File> exchangeFileList { get; set; }
        public tbl_Exchange_File exchangeFileModels { get; set; }
        public string IdChace { get; set; }
        public bool isDETAILS { get; set; }
        public string strFile { get; set; }
        public FileResult File { get; set; }
        public string filterField { get; set; }
        public string filterString { get; set; }
        public string sortOrder { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }

        public int MyProperty { get; set; }
        public string FilesToBeUploaded { get; set; }
    }

    public class Global_Employee_Document
    {
        public List<tbl_Employee_Document> employeeDocumentList { get; set; }
        public tbl_Employee_Document employeeDocumentModels { get; set; }
        public string IdChace { get; set; }
        public bool isDETAILS { get; set; }
        public string strFile { get; set; }
        public FileResult File { get; set; }
        public string filterField { get; set; }
        public string filterString { get; set; }
        public string sortOrder { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }

        public int MyProperty { get; set; }
        public string FilesToBeUploaded { get; set; }
    }

    public class Global_Employee_Payroll_Component
    {
        public System.Guid idChace { get; set; }
        public tbl_Organization_Payroll_Component organizationPayrollComponent { get; set; }

        public List<tbl_Organization_Payroll_Component> organizationPayrollComponentList { get; set; }
        public tbl_Employee_Payroll_Component EmployeePayrollComponent { get; set; }
        public tbl_Employee_Payroll_Component EmployeePayrollComponentPair { get; set; }
        public List<tbl_Employee_Payroll_Component> EmployeePayrollComponentList { get; set; }
        public tbl_Employee_Payroll_Component_Effective EmployeePayrollComponentEffective { get; set; }
        public tbl_Employee_Payroll_Component_Effective EmployeePayrollComponentEffectivePair { get; set; }
        public List<tbl_Employee_Payroll_Component_Effective> EmployeePayrollComponentEffectiveList { get; set; }
        public List<tbl_Employee_Payroll_Component_Effective> EmployeePayrollComponentEffectiveListPair { get; set; }
        public vw_Employee_Payroll_Component viewEmployeePayrollComponent { get; set; }
        public List<vw_Employee_Payroll_Component> viewEmployeePayrollComponentList { get; set; }
        public List<vw_Download_Employee_Payroll_Component> ListDownloadEmployeePayrollComponent { get; set; }
        public string[] EmployeIdEffective { get; set; }
        public string[] StartDate { get; set; }
        public string[] Remark { get; set; }
        public string[] EndDate { get; set; }
        public DateTime? CurrentPeriodStartDate { get; set; }
        public DateTime? CurrentPeriodEndDate { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string Employee_Name { get; set; }
    }
    public class Global_Organization_Payroll_Component
    {
        public System.Guid idChace { get; set; }
        public tbl_Organization_Payroll_Component organizationPayrollComponent { get; set; }
        public tbl_Organization_Payroll_Component organizationPayrollComponentTemp { get; set; }
        public List<tbl_Organization_Payroll_Component> organizationPayrollComponentList { get; set; }
        public List<vw_Organization_Payroll_Component_Summary> vwOganizepayroll { get; set; }



        public string filterType { get; set; }


        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }
    public class Global_Payroll_Variable
    {
        public System.Guid idChace { get; set; }
        public tbl_Payroll_Variable payrollVariable { get; set; }
        public tbl_Payroll_Period_Detail payrollPeriod { get; set; }
        public vw_Payroll_Variable vwpayrollVariable { get; set; }
        public tbl_Payroll_Variable_Detail payrollVariableDetail { get; set; }
        public List<tbl_Payroll_Variable> payrollVariableList { get; set; }
        public List<vw_Payroll_Variable> vwpayrollVariableList { get; set; }
        public List<tbl_Payroll_Variable_Detail> payrollVariableDetailList { get; set; }
        public string[] IdDetail { get; set; }
        public string[] Variable { get; set; }
        public decimal?[] Variable_Value { get; set; }


        public string filterType { get; set; }


        public int Working_Days_Count { get; set; }
        public decimal CalculationTotal { get; set; }
        public bool CalculationErr { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }
    public class ReportCell
    {
        public int RowId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }

        public static List<ReportCell> ConvertTableToCells(DataTable table)
        {
            List<ReportCell> cells = new List<ReportCell>();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    ReportCell cell = new ReportCell
                    {
                        ColumnName = col.Caption,
                        RowId = table.Rows.IndexOf(row),
                        Value = row[col.ColumnName].ToString()
                    };

                    cells.Add(cell);
                }
            }

            return cells;
        }
    }

    public class OrgReportCell
    {
        public Guid OrgId { get; set; }
        public int RowId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }

    public class DAO
    {
        public class Purchase
        {
            public string PayrollComponent { get; set; }
            public string EmployeeID { get; set; }
            public int Amount { get; set; }
        }
        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            //table.Columns.Add("Organization", typeof(string));
            table.Columns.Add("PayrollComponent", typeof(string));
            table.Columns.Add("EmployeeID", typeof(string));
            table.Columns.Add("Amount", typeof(int));

            table.Rows.Add("001", "E001", 1000);
            table.Rows.Add("002", "E001", 1000);
            table.Rows.Add("003", "E001", 1000);
            table.Rows.Add("004", "E001", 1000);

            table.Rows.Add("001", "E002", 1000);
            table.Rows.Add("004", "E002", 1000);

            return table;
        }

        public List<Purchase> GetPurchases()
        {
            var table = GetTable();
            var purchases = table.AsEnumerable().Select(m => new Purchase
            {
                PayrollComponent = m.Field<string>("PayrollComponent"),
                EmployeeID = m.Field<string>("EmployeeID"),
                Amount = m.Field<int>("Amount"),
            }).ToList();

            return purchases;
        }

        public List<ReportCell> GetReportCells(DataTable table)
        {
            return ReportCell.ConvertTableToCells(table);
        }
        public List<OrgReportCell> OrgReportCells { get; set; }
    }
    public class Global_Report_Employee_Generation
    {
        public string ReportFormat { get; set; }
        public List<SelectListItem> ListEmployee { get; set; }
        //public string ReportLayout { get; set; }
        //public List<SelectListItem> ListReportSettingDetail { get; set; }
    }
    public class Global_Payroll_Report_Generation
    {
        public List<Guid?> ListOrganizationID { get; set; }
        public List<Guid> ListPayrollPeriodID { get; set; }
        public string Organization_Group { get; set; }
        public List<Guid> ListCalculationGenerateID { get; set; }
        public string idCache { get; set; }
        public List<OrgReportCell> ListReportResult { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Get_Report_Employee_Information_Result> ListReportEmployee { get; set; }
        public List<Get_Report_Employee_Information_Result> SaveFilterReportEmployee { get; set; }
        public List<Guid> ListEmployeeCoverageSelected { get; set; }
        public List<tbl_Report_Payroll_Calculation> ListReportPayrollCalculation { get; set; }
        public List<tbl_Report_Payroll_Calculation_Details> ListReportPayrollCalculationDetails { get; set; }
        public string Payroll_Period { get; set; }
        public List<SelectListItem> ListRun { get; set; }
        public string Run { get; set; }
        public string[] HeadOfficeBranch { get; set; }
        public List<SelectListItem> ListLocation { get; set; }
        public string[] Location { get; set; }
        public string[] Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public string[] Deputation { get; set; }
        public string[] Division { get; set; }
        public string[] Department { get; set; }
        public string[] Grade { get; set; }
        public string[] Position { get; set; }
        public bool Employee_Coverage { get; set; }
        public string Payroll_Report_Code { get; set; }
        public string Report_Format { get; set; }
        public string Report_Group { get; set; }
        public string Target { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Tax_Summary_Report
    {
        public tbl_Report_Tax_Summary ReportTaxSummary { get; set; }
        public vw_Payroll_Period ViewPayrollPeriod { get; set; }
        public vw_Employee_Appointment_Status EmployeeAppointmentStatus { get; set; }
        public string OrganizationSelectedCode { get; set; }
        public string OrganizationSelectedName { get; set; }
        public string Head_Office_Branch_Tax_Id { get; set; }
        public string Head_Office_Branch_Tax_Id_Text { get; set; }
        public string Tax_Period { get; set; }
        public string Correction { get; set; }
        public string Type { get; set; }
        //public System.Guid idChace { get; set; }

        //public string filterType { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string typeFileDownload { get; set; }
        public string IdChace { get; set; }
    }
    public class Global_Report_Setting
    {
        public System.Guid idChace { get; set; }
        public tbl_Report_Setting PayrollReportSetting { get; set; }
        public tbl_Report_Setting_Detail PayrollReportSettingDetail { get; set; }
        public List<tbl_Report_Setting_Detail> ListPayrollReportSettingDetail { get; set; }
        public List<vw_Report_Setting> ListVWPayrollReport { get; set; }
        public List<tbl_Report_Column> ListReportColumn { get; set; }
        public List<string> ListComponentSelected { get; set; }
        public List<string> ListComponentSelectedDscp { get; set; }
        public int Setting_Type { get; set; }
        public List<string> ListSettingGroup { get; set; }
        public List<string> ListSettingGroupDelete { get; set; }
        public string Name_Alias { get; set; }
        public string filterType { get; set; }


        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<selectMulti> selectComponentList { get; set; }
        public class selectMulti
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public List<MultiGroupAdd> ListSettingMultiGroupAdd { get; set; }
        public class MultiGroupAdd
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public List<MultiGroupDel> ListSettingMultiGroupDel { get; set; }
        public class MultiGroupDel
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
    public class Global_Additional_Payroll
    {
        public System.Guid idChace { get; set; }
        public tbl_Additional_Payroll AdditionalPayroll { get; set; }
        public tbl_Additional_Payroll_Detail AdditionalPayrollDetail { get; set; }
        public vw_Additional_Payroll vwadditionalPayroll { get; set; }
        public tbl_Payroll_Period_Detail payrollPeriod { get; set; }
        public List<vw_Additional_Payroll> vwadditionalPayrollList { get; set; }
        public List<tbl_Additional_Payroll_Detail> AdditionalPayrollDetailList { get; set; }
        public List<vw_Payroll_Aditional> ListPayrollAdditional { get; set; }
        public List<SelectListItem> selectComponentList { get; set; }
        public tbl_Employee_Payroll_Component payrollComponent { get; set; }
        public string[] IdDetail { get; set; }
        public string[] ComponentDetail { get; set; }
        public string[] Currency { get; set; }
        public decimal?[] Amount { get; set; }
        public string[] TransDate { get; set; }
        public string[] Remark { get; set; }
        public string[] UploadNo { get; set; }


        public string filterType { get; set; }


        public bool Is_Calculation { get; set; }
        public List<SelectListItem> ListEmployeeComponentPayroll { get; set; }
        public bool IsPermanent { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string Category { get; set; }
    }
    public class Global_User
    {
        public tbl_User userModels { get; set; }
        public string Exist_Username { get; set; }
        public List<tbl_User> UserList { get; set; }
        public tbl_User_Role userRoleModels { get; set; }
        public List<tbl_User_Role> UserRoleList { get; set; }
        public tbl_Role roleModels { get; set; }
        public List<tbl_Role> roleList { get; set; }
        public tbl_Organization_User organizationUserModels { get; set; }
        public List<tbl_Organization_User> organizationUserList { get; set; }
        public tbl_User_Organization_Team userOrganizationTeamModels { get; set; }
        public List<tbl_User_Organization_Team> userOrganizationTeamList { get; set; }
        public tbl_Organization_Team organizationTeamModels { get; set; }
        public List<tbl_Organization_Team> organizationTeamList { get; set; }

        public String[] organization_role_array { get; set; }
        public String[] organization_team_array { get; set; }




        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }


    public class Global_RoleMenuFunction
    {
        public tbl_Role RoleMenu { get; set; }
        public List<tbl_Role> listRoleMenu { get; set; }
        public List<vw_Role_Menu> listvwRoleMenu { get; set; }

        //for create and edit 
        public tbl_Role RoleModels { get; set; }
        public List<tbl_Menu> ListMenu { get; set; }
        public List<tbl_Function> ListFunction { get; set; }
        public List<tbl_Menu_Function> ListMenuFunction { get; set; }

        public List<tbl_Organization_Menu> ListOrganizationMenu { get; set; }
        //

        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }


    public class Global_Employee_Leave
    {
        public tbl_Employee_Leave_Entitlement employeeLeaveMaster { get; set; }
        public tbl_Employee_Leave_Entitlement_Detail employeeLeaveDetail { get; set; }
        public vw_Employee_Leave_Entitlement_Summary vw_employeeLeaveMaster { get; set; }

        public List<tbl_Employee_Leave_Entitlement> listEmployeeLeaveMaster { get; set; }
        public List<tbl_Employee_Leave_Entitlement_Detail> listEmployeeLeaveDetail { get; set; }
        public List<vw_Employee_Leave_Entitlement_Summary> listvwEmployeeLeaveMaster { get; set; }

        public List<List_Employee> ListEmployee { get; set; }
        public string[] ListEmployeeSelected { get; set; }
		public List<SelectListItem> ListModelEmployeeSelecteds { get; set; }
        public List<tbl_Employee_Appointment> EmpAppointment { get; set; }
        public string Structure { get; set; }
        public string Structure_Value { get; set; }
        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }

        public string Strat { get; set; }
        public string End { get; set; }
    }


    public class Global_BPJS_Manpower
    {
        public List<tbl_BPJS_Manpower> BPJSManpowerList { get; set; }
        public tbl_BPJS_Manpower BPJSManpowerModels { get; set; }
        public string Default { get; set; }
        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public List<tbl_BPJS_Manpower> BPJSManpowerListSIPPLoginID { get; set; }
    }

    public class Global_BPJS_Healthcare
    {
        public List<tbl_BPJS_Healthcare> BPJSHealthcareList { get; set; }
        public tbl_BPJS_Healthcare BPJSHealthcareModels { get; set; }
        public string Default { get; set; }





        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public List<tbl_BPJS_Healthcare> BPJSHealthcareList_eDabuID { get; set; }
        public List<tbl_BPJS_Healthcare> BPJSHealthcareList_eID { get; set; }
    }

    public class Global_Cost_Center
    {
        public List<tbl_Cost_Center> costCenterList { get; set; }
        public tbl_Cost_Center costCenterModels { get; set; }






        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }

    }

    public class Global_Error_Code
    {
        public System.Guid id { get; set; }
        public string Error_Code { get; set; }
        public string Error_Language { get; set; }
        public string Error_Description { get; set; }
        public Nullable<int> Status_Code { get; set; }
    }

    public class Global_General_Parameter
    {
        public PageQuery pageQuery { get; set; }

        public tbl_General_Parameter generalParameterModels { get; set; }
        public List<tbl_General_Parameter> generalParameterList { get; set; }
        public List<tbl_General_Parameter> generalParameterListTblName { get; set; }
        public List<tbl_General_Parameter> generalParameterListFieldName { get; set; }
        public List<tbl_General_Parameter> generalParameterListFieldValue { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class User_Data
    {
        //public string CsNickNameChat = ConfigurationManager.AppSettings["CsNickNameChat"];
        public bool Valid_Login { get; set; }
        public Guid UserId { get; set; }
        public Guid UserOrganizationId { get; set; }
        public Guid UserOrganizationTeamId { get; set; }
        public string Username { get; set; }
        public string Language { get; set; }
        public string OrganizationSelectedName { get; set; }
        public string OrganizationService { get; set; }
        public Guid OrganizationSelected_Id { get; set; }
        public Guid? Tax_Period_Id { get; set; }
        public Guid? Tax_Period_Id_NP { get; set; }
        public DateTime? Tax_Period_Date { get; set; }
        public DateTime? Tax_Period_Date_NP { get; set; }
        public int? Year_Tax_Period { get; set; }
        public int? Year_Tax_Period_NP { get; set; }
        public Guid? Payroll_Period_Id { get; set; }
        public Guid? Payroll_Period_Id_NP { get; set; }
        public DateTime? PayrollStartDate { get; set; }
        public DateTime? PayrollStartDate_NP { get; set; }
        public DateTime? PayrollEndDate { get; set; }
        public DateTime? PayrollEndDate_NP { get; set; }
        public int? Last_Run_Permanent { get; set; }
        public tbl_Organization OrganizationSelected { get; set; }
        public List<tbl_Menu> UserMenu { get; set; }
        public List<tbl_Menu_Function> UserMenuFunction { get; set; }
        public List<tbl_Organization> UserOrganisation { get; set; }
        public tbl_Employee EmployeeSelected { get; set; }
        public vw_User_Mobile UserMobie { get; set; }
        public tbl_Appointment_Status_Information EmployeeStatus { get; set; }
        public List<vw_Login_Epayslip> UserLoginEpayslip { get; set; }
        public List<vw_Login_Epayslip> LoginEpayslip { get; set; }
        public List<tbl_Payslip> UserPayslip { get; set; }
        public List<tbl_Payslip> Payslip { get; set; }
        public List<tbl_Employee_Approval> ListEmployeeApproval { get; set; }
        public Guid Random_Key1 { get; set; }
        public Guid EmployeeId { get; set; }
        public List<tbl_Menu_Ess> UserMenuEss { get; set; }
        public Nullable<bool> Is_HR_User_Portal { get; set; }
        public List<tbl_Menu_Icon_Mobile> ListMenuIconMobile { get; set; }

    }

    public class User_Role
    {
        public string Username { get; set; }
        public string RandomKey { get; set; }
    }

    public class NotificationModel
    {
        public Guid? Sender { get; set; }
        public string message_to { get; set; }
        public string NPK { get; set; }
        public string message_body { get; set; }
        public string is_privateMessage { get; set; }
        public string is_email { get; set; }

    }

    public class NotificationData
    {
        public List<string> cost_center { get; set; }
        public string Beneficiary { get; set; }
        public string template_code { get; set; }
        //public List<TBL_ACTIVITY> notifWorkshopFinal { get; set; }
        public DataSet dsNotification { get; set; }
    }

    #region GlobalBankSetting
    public class Global_Bank_Setting
    {
        public tbl_List_Of_Bank bankSettingModels { get; set; }
        public List<tbl_List_Of_Bank> bankSettingList { get; set; }
        public tbl_Branch_List_Of_Bank branchOfBankModels { get; set; }
        public List<tbl_Branch_List_Of_Bank> branchOfBankList { get; set; }

        public string[] branchId { get; set; }
        public string[] branchCode { get; set; }
        public string[] branchName { get; set; }
        public string[] remarks { get; set; }
        public bool Online_Payment { get; set; }

        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalHolidayCalendar
    public class Global_Holiday_Calendar
    {
        public tbl_Holiday_Calendar holidayCalendarModels { get; set; }
        public List<tbl_Holiday_Calendar> holidayCalendarList { get; set; }

        public string strHolidayDate { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalPayrollCalculation
    public class Global_Payroll_Calculation
    {
        public List<vw_Calculation_Summary> ListCalculationSummary { get; set; }
        public List<tbl_Payroll_Calculation> ListCalculation { get; set; }
        public tbl_Payroll_Calculation DataModel { get; set; }
        public tbl_Payroll_Calculation DataModelSeverance { get; set; } 
        public Get_Payroll_Period_Result OrgPayroll_Period { get; set; }
        public DateTime TaxPeriod { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<tbl_Organization_Payroll_Component> ListComponent { get; set; }
        public List<tbl_Payroll_Calculation_Employee_Ex> ListEmployeeEx { get; set; }
        public List<tbl_Payroll_Calculation_Component_Ex> ListComponentEx { get; set; }
        public List<tbl_Payroll_Calculation_Employee> ListEmployeeSelectedDetail { get; set; }
        public List<tbl_Payroll_Calculation_Component> ListComponentSelectedDetail { get; set; }
        public List<Guid> ListComponentSelected { get; set; }
        public List<Guid> ListEmployeeSelected { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }

        public Guid? Deleted_Id { get; set; }
        public Guid? Deleted_Id_Severance { get; set; }

        public bool PartialRecalculated { get; set; }
    }

    public class CalculationEmployee
    {
        public List<tbl_Employee> tbl_Employee { get; set; }
        public List<tbl_Employee_Appointment> tbl_Employee_Appointment { get; set; }
        public List<tbl_Appointment_Status_Information> tbl_Appointment_Status_Information { get; set; }
        public List<tbl_Appointment_Working_Time> tbl_Appointment_Working_Time { get; set; }
        public List<tbl_Tax> tbl_Tax { get; set; }
        public List<tbl_Tax_Status_Effective_Year> tbl_Tax_Status_Effective_Year { get; set; }
    }

    #endregion

    #region GlobalPayrollClosing
    public class Global_Payroll_Closing
    {
        public List<tbl_Payroll_Closing> ListPayrollClosing { get; set; }
        public tbl_Payroll_Closing tbl_Payroll_Closing { get; set; }
        public Payroll_Closing DataModel { get; set; }
        public List<tbl_Payroll_Tax_Correction> ListPayrollTaxCorrection { get; set; }
        public List<tbl_Payroll_Tax_Correction> ListLastData { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string EmpUncalculated_ { get; set; }
    }
    public class Payroll_Closing
    {
        public Guid? id { get; set; }
        public Guid? Calculation_ID { get; set; }
        public Guid? Tax_Period_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string Current_Tax_Period { get; set; }
        public string Current_Payroll_Period { get; set; }
        public string Last_Tax_Period { get; set; }
        public string Last_Payroll_Period { get; set; }
        public int? Current_Run { get; set; }
        public int? Last_Run { get; set; }
        public int? Current_Pembetulan { get; set; }
        public int? Last_Pembetulan { get; set; }
        public string Run_Status { get; set; }
        public string Tax_Period_Status { get; set; }
        public string prev_batch { get; set; }
        public string Authorize_Status { get; set; }
        public string Status_Code { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion
    #region GlobalPayrollSlip
    public class Global_Payroll_Slip
    {
        public List<vw_Employee_Payslip_Summary> ListPayslip { get; set; }
        public vw_Employee_Payslip_Summary ViewModel { get; set; }
        public tbl_Payroll_Slip DataModel { get; set; }
        public Get_Payroll_Period_Result OrgPayroll_Period { get; set; }
        public DateTime TaxPeriod { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<tbl_Organization_Payroll_Component> ListComponent { get; set; }
        public List<tbl_Payroll_Slip_Employee> ListEmployeeSelectedDetail { get; set; }
        public List<tbl_Payroll_Slip_Component> ListComponentSelectedDetail { get; set; }
        public List<Guid> ListComponentSelected { get; set; }
        public List<Guid> ListEmployeeSelected { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion
    public class Global_Non_Taxable_Income_Parameter
    {
        public tbl_Non_Taxable_Income_Parameter nonTaxableIncomeParameterModel { get; set; }
        public List<tbl_Non_Taxable_Income_Parameter> nonTaxableIncomeParameterList { get; set; }

        public PageQuery pageQuery { get; set; }



    }

    public class Global_Tax_Rate_Parameter
    {
        public tbl_Tax_Rate_Parameter taxRateParameterModel { get; set; }
        public List<tbl_Tax_Rate_Parameter> taxRateParameterList { get; set; }




    }

    public class Global_Cost_Parameter
    {
        public tbl_Cost_Parameter costParameterModel { get; set; }
        public List<tbl_Cost_Parameter> costParameterList { get; set; }





    }

    public class Global_Chart_Of_Account
    {
        public tbl_Chart_Account chartOfAccountModels { get; set; }
        public List<tbl_Chart_Account> chartOfAccountList { get; set; }






        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Contact_Person
    {
        public tbl_Contact_Person contactPersonModels { get; set; }
        public List<tbl_Contact_Person> contactPersonList { get; set; }




        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Organization_Group
    {
        public tbl_Organization_Group organizationGroup { get; set; }
        public List<tbl_Organization_Group> organizationGroupList { get; set; }
        public tbl_Organization_Group_Detail organizationGroupDetail { get; set; }
        public List<tbl_Organization_Group_Detail> organizationGroupDetailList { get; set; }

        public List<tbl_Organization> organizationList { get; set; }

        public string[] IdDetail { get; set; }
        public string[] M_Organization_Client_ID { get; set; }
        public string[] M_Relationship_Type { get; set; }

        public string IdChace { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Exchange_Rate
    {
        public List<tbl_Exchange_Rate> excangeCurrancyRateList { get; set; }

        public tbl_Exchange_Rate excangeRateModels { get; set; }
        public List<tbl_Exchange_Rate> excangeRateList { get; set; }

        public tbl_Exchange_Rate_Details excangeRateDetailModels { get; set; }
        public List<tbl_Exchange_Rate_Details> excangeRateDetailList { get; set; }

        public System.Guid idChace { get; set; }
        public string[] IdDetail { get; set; }

        //public string IDCache { get; set; }
        //public Guid[] IDArray { get; set; }
        public string[] Cfrom { get; set; }
        public string[] Cto { get; set; }
        public string[] Rate { get; set; }

        public string strEffectivedate { get; set; }



        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Bank_Information
    {
        public string Default { get; set; }
        public tbl_Bank_Information bankInformationModels { get; set; }
        public List<tbl_Bank_Information> bankInformationList { get; set; }
        public List<tbl_List_Of_Bank> listOfBankList { get; set; }

        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Employee_Payment_Information
    {
        public tbl_Employee_Payment DataModel { get; set; }
        public List<tbl_Employee_Payment> List { get; set; }
        //public List<tbl_Employee_Payment> EmptyList { get; set; }
        public List<string> ListEditNumber { get; set; }
        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Employee_Appointment
    {
        public bool CheckedTaxPeriod { get; set; }
        public tbl_Employee_Appointment employeeAppointmentModel { get; set; }
        public List<tbl_Employee_Appointment> listEmployeeAppointment { get; set; }
        public vw_appointment_information VWemployeeAppointmentModel { get; set; }
        public List<tbl_Appointment_Status_Information> ListData { get; set; }
        public List<Get_Payroll_Period_Result> Get_Payroll_Period_Result { get; set; }
        public tbl_Appointment_Contract_Information employeeAppointmentContractInformationModel { get; set; }
        public List<tbl_Appointment_Contract_Information> listAppointmentContractInformation { get; set; }

        public tbl_Appointment_Department employeeAppointmentDepartmentModel { get; set; }
        public List<tbl_Appointment_Department> listAppointmentDepartment { get; set; }


        public tbl_Appointment_Division employeeAppointmentDivisionModel { get; set; }
        public List<tbl_Appointment_Division> listAppointmentDivision { get; set; }


        public tbl_Appointment_Grade employeeAppointmentGradeModel { get; set; }
        public List<tbl_Appointment_Grade> listAppointmentGrade { get; set; }


        public tbl_Appointment_Position employeeAppointmentPositionModel { get; set; }
        public List<tbl_Appointment_Position> listAppointmentPosition { get; set; }


        public tbl_Appointment_Status_Information employeeAppointmentStatusInformationModel { get; set; }
        public List<tbl_Appointment_Status_Information> listAppointmentStatusInformation { get; set; }


        public tbl_Appointment_Work_Location employeeAppointmentWorkLocationModel { get; set; }
        public List<tbl_Appointment_Work_Location> listAppointmentWorkLocation { get; set; }


        public tbl_Appointment_Working_Time employeeAppointmentWorkingTimeModel { get; set; }
        public List<tbl_Appointment_Working_Time> listAppointmentWorkingTime { get; set; }

        public tbl_Organization_Structure orgStructureModel { get; set; }
        public List<tbl_Organization_Structure> listOrgStructure { get; set; }

        public tbl_Organization_Working_Time orgWorkingTimeModel { get; set; }
        public List<tbl_Organization_Working_Time> listOrgWorkingTime { get; set; }
        public tbl_Employee EmployeeModel { get; set; }
        public List<tbl_Employee> listEmployee { get; set; }




        public PageQuery pageQuery { get; set; }

        public string[] EmploymentStatus { get; set; }
        public string[] EmployeeStatus { get; set; }
        public string[] effectiveDateStatusInfo { get; set; }

        public string[] WorkLocation { get; set; }
        public string[] effectiveDateWorkLocation { get; set; }

        public string[] ContractNo { get; set; }
        public string[] ContractStartDate { get; set; }
        public string[] ContractEndDate { get; set; }

        public string[] WorkingTimeCode { get; set; }
        public string[] WorkingTimeDescription { get; set; }
        public string WorkingTimeDesc { get; set; }
        public string[] StartDateWorkingTime { get; set; }
        public string[] EndDateWorkingTime { get; set; }

        public string[] PositionCode { get; set; }
        public string[] PositionDescription { get; set; }
        public string PositionDesc { get; set; }
        public string[] effectiveDatePosition { get; set; }

        public string[] DivisionCode { get; set; }
        public string[] DivisionDescription { get; set; }
        public string DivisionDesc { get; set; }
        public string[] effectiveDateDivision { get; set; }

        public string[] DepartmentCode { get; set; }
        public string[] DepartmentDescription { get; set; }
        public string DepartmentDesc { get; set; }
        public string[] effectiveDateDepartment { get; set; }

        public string[] GradeCode { get; set; }
        public string[] GradeDescription { get; set; }
        public string GradeDesc { get; set; }
        public string[] effectiveDateGrade { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public string strDate_Of_Hire { get; set; }
        public string strJoin_Date { get; set; }
        public string strEnd_Date_of_Probation { get; set; }
    }

    public class Global_Organization_Team
    {
        public tbl_Organization organizationModels { get; set; }
        public List<tbl_Organization> organizationList { get; set; }

        public tbl_Organization_Team organizationTeamModels { get; set; }
        public List<tbl_Organization_Team> organizationTeamList { get; set; }

        public tbl_Client_Organization_Team clientOrganizationTeamModels { get; set; }
        public List<tbl_Client_Organization_Team> clientOrganizationTeamList { get; set; }
        public List<vw_Team> vwTeamList { get; set; }


        public String[] organization_id_array { get; set; }






        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_HOBRANCH
    {

        public tbl_HeadOffice_Branch headOfficeBranch { get; set; }
        public List<tbl_HeadOffice_Branch> headOfficeBranchList { get; set; }
        public List<tbl_HeadOffice_Branch_Location> headOfficeBranchLocation { get; set; }
        public bool SameCheckBox { get; set; }




        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string[] locationIdarray { get; set; }
        public String[] locationarray { get; set; }
        public String srtlocation { get; set; }

    }
    public class Global_AddRemove
    {
        public bool strReturn { get; set; }
        public int checkboxrow { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Organization_Structure
    {
        public tbl_Organization_Structure organizationStructureModels { get; set; }
        public List<tbl_Organization_Structure> organizationStructureList { get; set; }


        public string filterType { get; set; }


        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }

    }

    public class Global_Holiday_Calender_Organization
    {
        public tbl_Holiday_Calendar_Organization holdayOrganizationModels { get; set; }
        public tbl_Holiday_Calendar_Organization_Details holdayOrganizationDetailsModels { get; set; }
        public List<tbl_Holiday_Calendar_Organization> holdayOrganizationList { get; set; }
        public List<tbl_Holiday_Calendar_Organization_Details> holdayOrganizationDetailsList { get; set; }
        public List<vw_summary_org_holliday> VwSummaryOrgHollidayList { get; set; }

        public tbl_HeadOffice_Branch headOfficeBranchModels { get; set; }
        public List<tbl_HeadOffice_Branch> headOfficeBranchList { get; set; }
        public string strHoliday { get; set; }
        public String[] Location_Holiday { get; set; }
        public string National { get; set; }


        public string filterType { get; set; }


        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Payroll_Schedule
    {
        public tbl_Payroll_Schedule payrollScheduleModel { get; set; }
        public List<tbl_Payroll_Schedule> payrollScheduleList { get; set; }





        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class Global_Employee_Personal_Info
    {
        public tbl_Employee employeeModel { get; set; }
        public tbl_Employee_ID_Information employeeIDNumModel { get; set; }
        public tbl_Employee_Family employeeFamilyModel { get; set; }
        public List<tbl_Employee> employeeList { get; set; }
        public List<tbl_Employee_ID_Information> employeeIDList { get; set; }
        public List<tbl_Employee_Family> employeeFamilyList { get; set; }
        public PageQuery pageQuery { get; set; }

        public string strDateofBirth { get; set; }

        public string[] idNumber { get; set; }
        public string[] idType { get; set; }
        public string[] effectiveDate { get; set; }
        public string[] expiredDate { get; set; }

        public string[] relationship { get; set; }
        public string[] name { get; set; }
        public string[] noKK { get; set; }
        public string[] dateofBirthFamily { get; set; }
        public string[] gender { get; set; }
        public string[] bloodType { get; set; }






        public string oldEmployeeNo { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public List<Get_Employee_Master_Data_Result> ListEmployeeMasterData { get; set; }

    }


    public class Global_Organization_Working_Time
    {
        public tbl_Organization_Working_Time organzationWorkingTeamModels { get; set; }
        public List<tbl_Organization_Working_Time> organzationWorkingTeamList { get; set; }

        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string[] strWorkingDay { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public List<CheckDay> checkDay { get; set; }
        public PageQuery pageQuery { get; set; }
        public string strScheduleTimeIn { get; set; }
        public string strScheduleTimeOut { get; set; }
    }


    public class CheckDay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }

    public class Global_Employee_Address
    {
        public tbl_Employee_Address employeeAdressModel { get; set; }
        public List<tbl_Employee_Address> listEmployeeAdress { get; set; }

        public string Exist_Email { get; set; }



        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }

        public string strPermanent_Address_Same_Legal_Address { get; set; }
        public string strMailing_Address_Same_Legal_Address { get; set; }
        public string strMailing_Address_Same_Permanen_Address { get; set; }
    }

    public class Global_Organization_Account_Group_Maintenance
    {
        public tbl_Organization_Account_Group_Maintenence organizationAccountGroupMaintenenceModels { get; set; }
        public string LastAccount { get; set; }
        public List<tbl_Organization_Account_Group_Maintenence> organizationAccountGroupMaintenenceModelsList { get; set; }

        public bool Detail { get; set; }





        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Tax_Period
    {
        public tbl_Tax_Period taxPeriodModels { get; set; }
        public tbl_Tax_Period_Month taxPeriodMonthModels { get; set; }
        public List<tbl_Tax_Period> taxPeriodList { get; set; }
        public List<tbl_Tax_Period_Month> taxPeriodMonthList { get; set; }
        public List<tbl_Tax_Period_Month> taxPeriodMonthListTEMP { get; set; }

        public vw_Tax_Period_Summary vwTaxPeriodModels { get; set; }
        public List<vw_Tax_Period_Summary> vwTaxPeriodListList { get; set; }

        public string strTaxFrom { get; set; }
        public string strTaxTo { get; set; }
        public string strClosingDate { get; set; }
        public List<string> bulanList { get; set; }

        public string tax_period_id { get; set; }
        public string[] tax_period_month_id { get; set; }
        public string[] tax_period { get; set; }
        public string[] status_tax { get; set; }
        public string[] closing_date { get; set; }
        public string[] closing_date_permanen { get; set; }
        public string[] closing_date_non_permanen { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Employee_Tax_Info
    {
        public bool CheckedTaxPeriod { get; set; }
        public tbl_Tax taxInfoModel { get; set; }
        public List<tbl_Tax> listTaxInfo { get; set; }
        public List<tbl_Tax_Status_Effective_Year> ListData { get; set; }

        public tbl_Tax_Status_Effective_Year taxEffectiveYearModel { get; set; }
        public List<tbl_Tax_Status_Effective_Year> listTaxEffectiveYear { get; set; }

        public string strEffDate { get; set; }

        public string strRevDate { get; set; }

        public string[] taxStatus { get; set; }
        public string[] statusEffectiveYear { get; set; }






        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
    }

    public class Global_Employee_Payslip_Info
    {
        public tbl_Payslip employeePayslipInfoModel { get; set; }
        public List<tbl_Payslip> employeePayslipInfoList { get; set; }





        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }

    #region GlobalPayrollPeriod
    public class Global_Payroll_Period
    {
        public tbl_Payroll_Period payrollPeriodModels { get; set; }
        public List<tbl_Payroll_Period> payrollPeriodList { get; set; }

        public tbl_Payroll_Period_Detail payrollPeriodDetailModels { get; set; }
        public List<tbl_Payroll_Period_Detail> payrollPeriodDetailList { get; set; }

        public vw_Payroll_Period_Summary vwPayrollPeriodModels { get; set; }
        public List<vw_Payroll_Period_Summary> vwPayrollPeriodList { get; set; }

        public tbl_Tax_Period taxPeriodModels { get; set; }
        public List<tbl_Tax_Period> taxPeriodList { get; set; }

        public tbl_Tax_Period_Month taxPeriodMonthModels { get; set; }
        public List<tbl_Tax_Period_Month> taxPeriodMonthList { get; set; }


        public System.Guid idChace { get; set; }

        public System.Guid taxPeriodID { get; set; }
        public string taxPeriodMonth { get; set; }

        public string[] IdDetail { get; set; }
        public string[] payrollPeriodDetailID { get; set; }
        public string[] taxPeriod { get; set; }
        public string[] periodStart { get; set; }
        public string[] periodEnd { get; set; }

        public PageQuery pageQuery { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    public class Global_Upload_Data
    {
        public string idcache { get; set; }
        public string Organization_ID { get; set; }
        public ListColoumnUpload ListColoumnUpload { get; set; }
        public List<tbl_Upload_Staging> generalUploadDataList { get; set; }
        public List<checkUploadedFile_Result> checkUploadedFile_Result_List { get; set; }
        public _uploadData _uploadData { get; set; }
        public PageQuery pageQuery { get; set; }
        public string url { get; set; }
        public string urlInsertToBeDownload { get; set; }
        public string FileName { get; set; }
        public string UploadType { get; set; }
        public int relativeRow { get; set; }
        public int relativeColumn { get; set; }
        public string ErrorUploadHeader { get; set; }
        public bool IsNewUpload { get; set; }
        public bool IsAutoSubmit { get; set; }
    }
    public class ListColoumnUpload
    {

        public List<ListColoumnUpload> ListDataUpload { get; set; }
        public List<ListColoumnUpload> ListColoumnUploadHeader { get; set; }
        public System.Guid id { get; set; }
        public int Col_Index { get; set; }
        public string Target_Table { get; set; }
        public string Col_ID { get; set; }
        public string Col_Name { get; set; }
        public string Is_Mandatory { get; set; }
        public bool Is_Mandatory_Bool { get; set; }
        public string Record_Id { get; set; }
        public string Value { get; set; }
        public bool Upload_Key { get; set; }
        public string Table_Mapping { get; set; }
        public string Data_Type { get; set; }
        public int Row_Line { get; set; }

    }
    public class _uploadData
    {
        public tbl_Upload_Staging UploadStaging { get; set; }
        public tbl_Upload_Staging_Header UploadStagingHeader { get; set; }
        public List<tbl_Upload_Staging_Detail> UploadStagingDetail { get; set; }
        public List<ErrorNewUpload> ListErrorNewUpload { get; set; }
        public ErrorNewUpload Error { get; set; }
        public string FileName { get; set; }
        public System.Guid Upload_ID { get; set; }
        public System.Guid Upload_Header_ID { get; set; }
        public string UploadNumber { get; set; }
        public int SuccesssUpload { get; set; }
        public int FailedUpload { get; set; }
        public int WarningUpload { get; set; }
        public int TotalRow { get; set; }
        public string CannotBeSubmit { get; set; }
    }
    public class ErrorUploadHeader
    {
        public string ERROR_MESSAGE_HEADER { get; set; }
    }
    public class ErrorNewUpload
    {
        public string ID { get; set; }
        public int ERROR_LINE { get; set; }
        public string COLUMN_NAME { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public int STATUS { get; set; }
    }

    #region AdditionalCheck
    public class AdditionalCheck
    {
        public string Employee_no { get; set; }
        public string Employee_name { get; set; }
        public string Component { get; set; }
    }
    #endregion

    #region GlobalNewRegisterBPJSManpower
    public class Global_New_Register_BPJS_Manpower
    {
        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }

        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public string typeFileDownload { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalInactiveBPJSManpower
    public class Global_Inactive_BPJS_Manpower
    {
        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }

        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public string typeFileDownload { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalLoan
    public class Global_Loan
    {
        public tbl_Loan loanModels { get; set; }
        public List<tbl_Loan> loanList { get; set; }
        public vw_Loan viewLoanModels { get; set; }
        public List<vw_Loan> viewLoanList { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalCompensationBenefit
    public class Global_Compensation_Benefit
    {
        public tbl_Compensation_Benefit compensationBenefitModels { get; set; }
        public List<tbl_Compensation_Benefit> compensationBenefitList { get; set; }
        public vw_Compensation_Benefit viewCompensationBenefitModels { get; set; }
        public List<vw_Compensation_Benefit> viewCompensationBenefitList { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalGenerateBankFile
    public class Global_GenerateBankFile
    {

        public DataSet DataSetReport { get; set; }
        public List<ReportCell> ListReportHeaderResult { get; set; }
        public List<ReportCell> ListReportDetailResult { get; set; }
        public tbl_Generate_Bank_File generateBankFileModels { get; set; }
        public tbl_Generate_Bank_File_Details generateBankFileDetailsModels { get; set; }
        public List<tbl_Generate_Bank_File_Details> generateBankFileDetailsList { get; set; }
        public List<vw_Generate_Bank_File_Details> vwGenerateBankFileDetailsList { get; set; }
        public List<vw_Generate_Bank_File_Details_Summary> vwGenerateBankFileDetailsSummaryList { get; set; }

        public Nullable<System.Guid> Bank_InformationID { get; set; }
        public List<SelectListItem> SelectList_Employee_Payroll_Period { get; set; }


        public string IdChace { get; set; }
        public string strRun { get; set; }
        public string strDate { get; set; }

        public Guid? Payroll_PeriodID { get; set; }
        public string OutputType { get; set; }

        public string Organization_Name { get; set; }
        public List<long> ListEmployeeSelected { get; set; }
        public string SourceAccountBankID { get; set; }
        public string strTransferDate { get; set; }

        public tbl_Bank_Information BankInformationModel { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }

    }
    public class GBF_Organization
    {
        public string organizationID { get; set; }
        public string organizationName { get; set; }
    }
    public class GBF_TotalAmountEmployee
    {
        public decimal totalAmount { get; set; }
        public int totalEmployee { get; set; }
    }
    public class GBF_Bank
    {
        public string bankInformationID { get; set; }
        public string bankInfromaitonName { get; set; }
        public string organizationID { get; set; }
    }

    public class GBF_Payroll_Period
    {
        public string PayrollPeriodID { get; set; }
        public string PayrollPeriod { get; set; }
        public string organizationID { get; set; }
    }

    public class GBF_Bank_Data
    {
        public string Account_Number { get; set; }
        public string Account_Name { get; set; }
        public string Account_Address { get; set; }
        public List<GBF_File_Type> FileType { get; set; }

    }

    public class GBF_File_Type
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class List_Structure_value
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class List_Employee
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class User_Payslip
    {
        public Guid User_Payslip_ID { get; set; }
        public string Full_Name { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Verification_Code { get; set; }
        public string Path_Setting { get; set; }
        public string Url_Setting_Ess { get; set; }
        public DateTime? Get_Date { get; set; }
    }
    #endregion

    #region GlobalOrganizationEmailSetup
    public class Global_Organization_Email_Setup
    {
        public Guid selectedOrganization { get; set; }

        public tbl_Organization_Email_Setup organizationEmailSetupModels { get; set; }
        public List<tbl_Organization_Email_Setup> organizationEmailSetupList { get; set; }

        public vw_Organization_Email_Setup_Summary vwOrgEmailSetModels { get; set; }
        public List<vw_Organization_Email_Setup_Summary> vwOrgEmailSetList { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalVWPayrollSlipEmployee
    public class Global_VW_Payroll_Slip_Employee_Summary
    {
        public vw_Payroll_Slip_Employee_Summary vwPayrollSlipEmployeeModels { get; set; }
        public List<vw_Payroll_Slip_Employee_Summary> vwPayrollSlipEmployeeList { get; set; }
        public List<vw_Payroll_Slip_Employee_Summary> ListPayslipDownload { get; set; }
        public Nullable<bool> IsHRUserPortal { get; set; }

        //public PageQuery pageQuery { get; set; }
        //public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion
	
	#region GlobalVWPayrollSlipEmployeeEpayslip
    public class Global_VW_Payroll_Slip_Employee_Summary_Epayslip
    {
        public vw_Payroll_Slip_Employee_Summary vwPayrollSlipEmployeeModels { get; set; }
        public List<vw_Payroll_Slip_Employee_Summary> vwPayrollSlipEmployeeList { get; set; }
        public List<vw_Payroll_Slip_Employee_Summary> ListPayslipDownload { get; set; }
        public Nullable<bool> IsHRUserPortal { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion


    public class Global_Report_Payslip
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Addition_Component { get; set; }
        public double Addition_Amount { get; set; }
        public string Deduction_Component { get; set; }
        public double Deduction_Amount { get; set; }
        public string Group_Name { get; set; }
    }

    #region GlobalHistoryPayrollReport
    public class Global_History_Report
    {
        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<vw_Report_Employee_Payroll_History> ListDataEmployeePayrollHistory { get; set; }
        public List<vw_Report_Summary_History> ListDataSummaryHistory { get; set; }
        public string yearPeriod { get; set; }
        public Guid? selectedPayrollPeriod { get; set; }
        public List<Guid> ListEmployeeSelected { get; set; }
        public string selectedHistoryReport { get; set; }
        public string typeFileDownload { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
        public string OrganizationSelectedCode { get; set; }
        public DateTime PayrollPeriod { get; set; }
        public string UsernameLogin { get; set; }
    }
    #endregion

    #region GlobalTaxReporting_A1
    public class Global_Tax_Reporting_A1
    {
        public string selectedOrganizationTaxID { get; set; }
        public DateTime? selectedFromPayrollPeriod { get; set; }
        public DateTime? selectedToPayrollPeriod { get; set; }
        public string[] selectedEmployeeStatus { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Guid> ListEmployeeSelected { get; set; }
        public string dateSelected { get; set; }
        public string typeFileDownload { get; set; }
        public bool Employee_Coverage { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdCache { get; set; }
        public string employeeStatusSelectedString { get; set; }
        public string employeeSelectedString { get; set; }
        public string OrganizationSignatureLogo { get; set; }
        public bool CheckIncludeSignature { get; set; }
        public List<SelectListItem> ListFilterEmployee { get; set; }
    }
    #endregion

    #region GlobalReport1721Final
    public class Global_Report_1721_Final
    {
        public string selectedOrganizationTaxID { get; set; }
        public Guid selectedPayrollPeriod { get; set; }
        public string typeFileDownload { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalReport1721NotFinal
    public class Global_Report_1721_Not_Final
    {
        public string IdCache { get; set; }
        public string selectedOrganizationTaxID { get; set; }
        public Guid selectedPayrollPeriod { get; set; }
        public string typeFileDownload { get; set; }
        public string OrganizationSignatureLogo { get; set; }
        public bool CheckIncludeSignature { get; set; }
        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalSalaryJournalReport
    public class Global_Salary_Journal_Report
    {
        public string selectedTaxPeriodID { get; set; }
        public string selectedPayrollPeriodID { get; set; }
        public string selectedEmploymentStatus { get; set; }
        public string selectedCalculationRun { get; set; }
        public string typeFileDownload { get; set; }

        public List<SelectListItem> listPayrollPeriod { get; set; }
        public List<SelectListItem> listCalculationRun { get; set; }


        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalFundRequisition
    public class Global_Fund_Requisition
    {
        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }
        public Guid selectedPayrollPeriod { get; set; }
        public int run { get; set; }
        public string[] selectedEmploymentStatus { get; set; }
        public string clientPICName { get; set; }
        public float bankCharges { get; set; }
        public string transferTo { get; set; }
        public string typeFileDownload { get; set; }


        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalPayrollComparativeReport
    public class Global_Payroll_Comparative_Report
    {
        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }
        public Guid selectedPayrollPeriod { get; set; }
        public bool excludeZeroDifference { get; set; }
        public bool excludeZeroValue { get; set; }
        public string selectedComparativeType { get; set; }
        public int run { get; set; }
        public string typeFileDownload { get; set; }


        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalPayrollComparativeReport
    public class Global_Generate_Template_Additional
    {
        public string selectedEmployeementStatus { get; set; }
        public Guid? selectedPayrollPeriod { get; set; }
        public String[] multiComponent { get; set; }
        public String[] multiStatus { get; set; }

        public List<List_Additional_Template> ListTemplateAdditional { get; set; }
        public vw_Payroll_Period period { get; set; }
        public List<tbl_Appointment_Status_Information> ListAppointment { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }

        public Guid selectedOrganization { get; set; }
        public List<tbl_Organization> organizationList { get; set; }
        public int run { get; set; }
        public string typeFileDownload { get; set; }

    }

    public class List_Additional_Template
    {
        public Guid Employee_ID { get; set; }
        public string uniq { get; set; }
        public string empNo { get; set; }
        public string empName { get; set; }
        public string component { get; set; }
        public string value { get; set; }
        public string Remark { get; set; }
        public string Transaction_date { get; set; }
        public string Employee_Ststus { get; set; }
    }

    #endregion

    #region GlobalReport1721Bulanan
    public class Global_Report_1721_Bulanan
    {
        public string selectedOrganizationTaxID { get; set; }
        public string selectedPayrollPeriod { get; set; }
        public string selectedCorrection { get; set; }
        public string typeFileDownload { get; set; }
        public Nullable<bool> selected_All_Employee { get; set; }

        public List<SelectListItem> listCorrection { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalChangePassword
    public class Global_Change_Password
    {
        public tbl_User tblUserModels { get; set; }
        public tbl_Payslip tblPayslipModels { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalReportEmployeePayslipTemplate
    public class Global_Report_Employee_Payslip_Template
    {
        public string idCache { get; set; }
        public tbl_Payslip_Template PayslipTemplate { get; set; }
        public tbl_Payslip_Setting PayslipSetting { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string ReportName { get; set; }
    }
    #endregion

    #region GlobalEmpDeductionScheduleReport
    public class Employee_Deduction_Schedule_Report_Result
    {
        public int Id_Number { get; set; }
        public DateTime Due_Date { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> Interest_Rate { get; set; }
        public Nullable<decimal> Principal_Payment { get; set; }
        public Nullable<decimal> Interest_Payment { get; set; }
        public Nullable<decimal> Installment { get; set; }
        public string Period { get; set; }
        public Nullable<double> PR_Deduction { get; set; }
    }

    public class Global_Employee_Deduction_Schedule_Report
    {
        public string empNoID { get; set; }
        public string comLinkageID { get; set; }
        public string typeFileDownload { get; set; }


        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalTemplateConversion
    public class Global_Template_Conversion
    {
        public string idCache { get; set; }
        public string Template_Type { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public ColoumTemplateConversion ColoumTemplateConversion { get; set; }
        public List<ColoumTemplateConversion> ListDataUpload { get; set; }
        public string FileName { get; set; }
    }

    public class ColoumTemplateConversion
    {
        public Guid id { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Currency { get; set; }
        public string Tax_Policy { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public string Formula { get; set; }
        public string Remark { get; set; }
    }
    #endregion

    #region GlobalSystemParameter
    public class Global_System_Parameter
    {
        public tbl_SysParam systemParameterModels { get; set; }
        public List<tbl_SysParam> systemParameterList { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    #endregion

    #region GlobalBlog
    public class Global_Blog
    {
        public tbl_Blog_Posts blogModels { get; set; }
        public vw_Blog_Summary vwBlogModels { get; set; }
        public List<vw_Blog_Summary> vwBlogList { get; set; }
        public List<tbl_Blog_Posts> blogList { get; set; }

        public FileResult Article_img { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion


    #region Global_Approval_Hierarchy
    public class Global_Approval_Hierarchy
    {
        public System.Guid idcache { get; set; }
        public tbl_Employee_Approval EmployeeApproval { get; set; }
        public vw_Employee_Approval ViewEmployeeApproval { get; set; }
        public List<vw_Employee_Approval> ListViewEmployeeApproval { get; set; }
        public string filterType { get; set; }
        public PageQuery pageQuery { get; set; }
    }
    #endregion

    public class Global_Summary_Record
    {
        public List<Get_Summary_Record_Result> GetSummaryRecordResult_List { get; set; }
        public string Record_Type { get; set; }
        public Guid Menu_Selected { get; set; }
        public string Menu_Name { get; set; }
        public string Url_Link { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }

    }

    public partial class Get_EmpPayCompAsTaxDeduction_Result
    {
        public Guid Employee_ID { get; set; }
    }

    public partial class Get_Tax_Bracket_Result
    {
        public Guid Employee_ID { get; set; }
    }
    #region subreport employee tax calculation
    public class dsTotalComponent
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public string Payroll_Component_Code { get; set; }
        public double Amount { get; set; }



    }
    public class dsTotalComponentSeverance
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public double Amount { get; set; }
    }
    public class dsTotalComponentFinal
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public double Amount { get; set; }
    }
    public class dsTaxRefund
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public double Amount { get; set; }
    }
    public class dsNetSalary
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public double Amount { get; set; }
    }
    public class dsTakeHomepay
    {
        public Guid id { get; set; }
        public Guid Employee_ID { get; set; }
        public string Payroll_Component_Description { get; set; }
        public double Amount { get; set; }
    }
    #endregion

    #region Global_Detail_Report_Attendance
    public class Global_Detail_Report_Attendance
    {
        public Get_Report_Attendance_Result Get_Report_Attendance { get; set; }
        public List<Get_Report_Attendance_Result> ListReportAttendance { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public List<string> ListPhoto { get; set; }
        public PageQuery pageQuery { get; set; }
        public string Date { get; set; }
        public string ImageZoom { get; set; }
    }
    #endregion

    #region Global_Report_Attendance
    public class Global_Report_Employee_Attendance
    {
        public List<SelectListItem> ListEmployee { get; set; }
    }
    public class Global_Report_Attendance
    {
        public List<Get_Report_Attendance_Result> ListReportAttendance { get; set; }
        public string idCache { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Get_Employee_Report_Attendance_Result> ListReportEmployee { get; set; }
        public List<Guid?> ListEmployeeCoverageSelected { get; set; }
        public string[] HeadOfficeBranch { get; set; }
        public List<SelectListItem> ListLocation { get; set; }
        public string[] Location { get; set; }
        public string[] Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public string[] Deputation { get; set; }
        public string[] Division { get; set; }
        public string[] Department { get; set; }
        public string[] Grade { get; set; }
        public string[] Position { get; set; }
        public string[] CostCenter { get; set; }
        public string ReportType { get; set; }
        public string FileType { get; set; }
        public bool Employee_Coverage { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string StartFrom { get; set; }
        public string StartTo { get; set; }
        public Guid Organization_Id { get; set; }
    }
    #endregion

    #region Global_Report_Apply_Leave
    public class Global_Report_Employee_Leave
    {
        public List<SelectListItem> ListEmployee { get; set; }
    }
    public class Global_Report_Leave
    {
        public List<Get_Report_Leave_Result> ListReportLeave { get; set; }
        public bool Employee_Coverage { get; set; }
        public string idCache { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Get_Employee_Report_Leave_Result> ListReportEmployee { get; set; }
        public List<Guid?> ListEmployeeCoverageSelected { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string StartFrom { get; set; }
        public string StartTo { get; set; }
        public string[] HeadOfficeBranch { get; set; }
        public string[] Leave_Type { get; set; }
        public string[] Leave_Status { get; set; }
        public List<SelectListItem> ListLocation { get; set; }
        public string[] Location { get; set; }
        public string[] Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public string[] Deputation { get; set; }
        public string[] Division { get; set; }
        public string[] Department { get; set; }
        public string[] Grade { get; set; }
        public string[] Position { get; set; }
        public string Generateby { get; set; }
    }

    #endregion

    #region Global_Report_Claim
    public class Global_Report_Employee_Claim
    {
        public List<SelectListItem> ListEmployee { get; set; }
    }
    public class Global_Report_Claim
    {
        public List<Get_Report_Claim_Result> ListReportClaim { get; set; }
        public string idCache { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Guid?> ListEmployeeCoverageSelected { get; set; }
        public List<Get_Employee_Report_Claim_Result> ListReportEmployee { get; set; }
        public bool Employee_Coverage { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public List<SelectListItem> ListLocation { get; set; }
        public string[] Claim_Status { get; set; }
        public string StartFrom { get; set; }
        public string StartTo { get; set; }
        public double AmountFrom { get; set; }
        public double AmountTo { get; set; }
        public string[] Deputation { get; set; }
        public string[] Division { get; set; }
        public string[] Department { get; set; }
        public string[] Grade { get; set; }
        public string[] Position { get; set; }
        public string[] HeadOfficeBranch { get; set; }
        public string[] Location { get; set; }
        public string[] Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public string[] CostCenter { get; set; }
        public string FileType { get; set; }
        public string Generateby { get; set; }
    }

    #endregion

    #region Global_Report_Overtime
    public class Global_Report_Employee_Overtime
    {
        public List<SelectListItem> ListEmployee { get; set; }
    }
    public class Global_Report_Overtime
    {
        public List<Get_Report_Overtime_Result> ListReportOvertime { get; set; }
        public string idCache { get; set; }
        public List<tbl_Employee> ListEmployee { get; set; }
        public List<Guid?> ListEmployeeCoverageSelected { get; set; }
        public List<Get_Employee_Report_Overtime_Result> ListReportEmployee { get; set; }
        public bool Employee_Coverage { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public List<SelectListItem> ListLocation { get; set; }
        public string StartFrom { get; set; }
        public string StartTo { get; set; }
        public string[] Overtime_Status { get; set; }
        public string[] HeadOfficeBranch { get; set; }
        public string[] Location { get; set; }
        public string[] Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public string[] Deputation { get; set; }
        public string[] Division { get; set; }
        public string[] Department { get; set; }
        public string[] Grade { get; set; }
        public string[] Position { get; set; }
        public string Generateby { get; set; }
    }
    #endregion

    #region GLobal_Generate_Online_Payment
    public class Global_Generate_Online_Payment
    {
        public DataSet DataSetReport { get; set; }
        public tbl_Trx_Bank_Transfer TrxBankTransfer { get; set; }
        public List<tbl_Trx_Bank_Transfer> ListTrxBankTransfer { get; set; }
        public vw_Trx_Bank_Transfer vwTrxBankTransfer { get; set; }
        public List<vw_Trx_Bank_Transfer> ListViewTrxBankTransfer { get; set; }
        public tbl_Trx_Bank_Transfer_Detail TrxBankTransferDetail { get; set; }
        public List<tbl_Trx_Bank_Transfer_Detail> ListTrxBankTransferDetail { get; set; }
        public List<vw_Payroll_Payment_Summary> ListViewPayrollPaymentSummary { get; set; }
        public List<vw_Payroll_Payment_Details> ListViewPayrollPaymentDetails { get; set; }
        //public List<ReportCell> ListReportHeaderResult { get; set; }
        //public List<ReportCell> ListReportDetailResult { get; set; }
        //public tbl_Generate_Bank_File generateBankFileModels { get; set; }
        //public tbl_Generate_Bank_File_Details generateBankFileDetailsModels { get; set; }
        //public List<tbl_Generate_Bank_File_Details> generateBankFileDetailsList { get; set; }
        //public List<vw_Generate_Bank_File_Details> vwGenerateBankFileDetailsList { get; set; }
        //public List<vw_Generate_Bank_File_Details_Summary> vwGenerateBankFileDetailsSummaryList { get; set; }
        public Nullable<System.Guid> Bank_InformationID { get; set; }
        public List<SelectListItem> SelectList_Employee_Payroll_Period { get; set; }
        public string IdCache { get; set; }
        public string strRun { get; set; }
        public string strDate { get; set; }
        public Guid? Payroll_PeriodID { get; set; }
        public string[] Employee_Bank { get; set; }
        public List<SelectListItem> SelectListEmployeeBank { get; set; }
        //public string OutputType { get; set; }
        public string Organization_Name { get; set; }
        public List<long> ListEmployeeSelected { get; set; }
        public string SourceAccountBankID { get; set; }
        public string strTransferDate { get; set; }
        public tbl_Bank_Information BankInformationModel { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string Action_Type { get; set; }
        public string WarningInsufficientBalance { get; set; }
        public string Transfer_Date { get; set; }
        public string TotalAmount { get; set; }
    }
    public class ParameterAPIPost
    {
        public string Batch_Number { get; set; }
        public string Action_Type { get; set; }
    }
    public class GOP_Organization
    {
        public string organizationID { get; set; }
        public string organizationName { get; set; }
    }
    public class GOP_TotalAmountEmployee
    {
        public decimal totalAmount { get; set; }
        public int totalEmployee { get; set; }
    }
    public class GOP_Bank
    {
        public string bankInformationID { get; set; }
        public string bankInfromaitonName { get; set; }
        public string organizationID { get; set; }
    }

    public class GOP_Payroll_Period
    {
        public string PayrollPeriodID { get; set; }
        public string PayrollPeriod { get; set; }
        public string organizationID { get; set; }
    }

    public class GOP_Bank_Data
    {
        public Guid? Source_Account_Bank_ID { get; set; }
        public string Account_Number { get; set; }
        public string Account_Name { get; set; }
        public string Account_Address { get; set; }
        //public List<GBF_File_Type> FileType { get; set; }

    }
    public class GOP_File_Type
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region GlobalPayrollPortal
    public class User_Trial_Data_Email
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Trial_Date { get; set; }
        public string Company { get; set; }
        public string Phone_Number { get; set; }
        public string Url_User_Trial { get; set; }
        public string Url_Chat_Web_Cs { get; set; }
    }

    public class Contact_Support_Data_Email
    {
        public string Name { get; set; }
        public string Email_From { get; set; }
        public string Message_Body { get; set; }
        public string Phone_No { get; set; }
    }

    public class Global_Payroll_Portal
    {
        #region List Request Demo
        public tbl_User_Trial UserTrialModels { get; set; }
        public List<tbl_User_Trial> UserTrialList { get; set; }
        #endregion

        #region List Contact Support
        public tbl_Contact_Support ContactSupportModels { get; set; }
        public List<tbl_Contact_Support> ContactSupportList { get; set; }
        #endregion

        #region Request Demo Models
        public string fullName { get; set; }
        public string userName { get; set; }
        public string mobilePhone1 { get; set; }
        public string mobilePhone2 { get; set; }
        public string officePhoneNo { get; set; }
        public string email { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public int? emailStatus { get; set; }
        #endregion

        #region Contact Support Models
        public string cs_FullName { get; set; }
        public string cs_Send_Email { get; set; }
        public string cs_Email { get; set; }
        public string cs_Body_Email { get; set; }
        public string cs_Phone_No { get; set; }
        #endregion

        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }

    public class Global_Customer_Notice
    {
        public tbl_Customer_Notice customerNoticeModels { get; set; }
        public List<tbl_Customer_Notice> customerNoticeList { get; set; }

        #region Post_Data_Email
        public string Company_Name { get; set; }
        public string Number_Employee { get; set; }
        public string Company_Website { get; set; }
        public string Contact_Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Meeting_Schedule { get; set; }
        public string Meeting_Location { get; set; }
        public string Many_Customers { get; set; }
        public string Curently_Partner { get; set; }
        public string Primary_Competitors { get; set; }
        public string Partnership_Interest { get; set; }
        public string Type_Form { get; set; }
        public string Remark { get; set; }
        public string Message { get; set; }
        public string Pricing { get; set; }
        #endregion

        public List<Global_Error_Code> errMessage { get; set; }
    }

    public partial class tbl_Customer_Notice
    {
        public string Format_Meeting_Schedule { get; set; }
    }

    #endregion

    #region GlobalBlogPost
        public class Global_Blog_Post
    {
        #region List Blog
        public tbl_Blog_Posts BlogPostsModels { get; set; }
        public List<tbl_Blog_Posts> BlogPostsList { get; set; }
        public List<tbl_Blog_Posts> BlogArticlePopulerList { get; set; }
        public List<tbl_Blog_Posts> BlogNewsArticleList { get; set; }

        public string pathBlog { get; set; }
        #endregion

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }
    #endregion

    #region GlobalPaymentOrder
    public class Global_Payment_Order
    {
        #region List Payment
        public tbl_User_Order userOrderModels { get; set; }
        public List<tbl_User_Order> userOrderList { get; set; }
        public tbl_Order orderPaymentModels { get; set; }
        public List<tbl_Order> orderPaymentList { get; set; }
        public tbl_Order_Promotion_Detail orderPromotionDetailModels { get; set; }
        public List<tbl_Order_Promotion_Detail> orderPromotionDetailList { get; set; }
        public tbl_Order_Tiering_Detail orderTieringDetailModels { get; set; }
        public List<tbl_Order_Tiering_Detail> orderTieringDetailList { get; set; }
        #endregion

        #region String Payment Order
        #region OrderPayment
        public string formatDatePayment { get; set; }
        public string packagedescription { get; set; }
        public string strStartPeriod { get; set; }
        public Nullable<double> fltTotalPackagePrice { get; set; }
        public Nullable<double> fltOnTimeImplementation { get; set; }
        public Nullable<double> fltSubtotal { get; set; }
        public Nullable<double> fltDiscount { get; set; }
        public Nullable<double> fltTax { get; set; }
        public Nullable<double> fltTotalPayment { get; set; }
        public string strTotalPayment { get; set; }
        #endregion

        #region OrderPaymentPackageDetails
        public Nullable<double> fltDetailsTotalPackagePrice { get; set; }
        public string strDetailsDescTotalPackagePrice { get; set; }
        public Nullable<double> fltDetailsTiering { get; set; }
        public string strDetailsDescTiering { get; set; }
        public string strDetailsDescTax { get; set; }
        #endregion
        #endregion

        public List<tbl_Promotion> CountPromotionList { get; set; }

        public List<Global_Promotion_Code_Order> strPromotionCodeList { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }

    public class Global_Promotion_Code_Order
    {
        public string strDetailsDescPromotion { get; set; }
        public Nullable<double> strDetailsPromotion { get; set; }
    }
    #endregion

    #region PaymentMidtrans
    public class GlobalAPIOnlinePaymentMidtrans
    {
        public RequestAPIPaymentMidtrans RequestWebToMidtrans { get; set; }
        public ResponseAPIPaymentMidtrans ResponseMidtransToWeb { get; set; }
        public ResponseStatusPaymentMidtrans StatusPaymentMidtrans { get; set; }

        public SendEmailNotifMidtrans SendMailMidtrans { get; set; }

        public PageQuery pageQuery { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string IdChace { get; set; }
    }

    #region API Request
    public class RequestAPIPaymentMidtrans
    {
        public RequestTransactionDetails transaction_details { get; set; }
        public List<RequestItemDetails> item_details { get; set; }
        public RequestCustomerDetails customer_details { get; set; }
        public String[] enabled_payments { get; set; }
        public RequestCreditCard credit_card { get; set; }
        public RequestBcaVa bca_va { get; set; }
        public RequestBniVa bni_va { get; set; }
        public RequestPermataVa permata_va { get; set; }
        public RequestCallbacks callbacks { get; set; }
        public RequestExpiry expiry { get; set; }
        public string custom_field1 { get; set; }
        public string custom_field2 { get; set; }
        public string custom_field3 { get; set; }

    }
    public class RequestTransactionDetails
    {
        public string order_id { get; set; }
        public Nullable<int> gross_amount { get; set; }
    }
    public class RequestItemDetails
    {
        public string id { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string merchant_name { get; set; }
    }
    public class RequestCustomerDetails
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public RequestBillingAddress billing_address { get; set; }
        public RequestShippingAddress shipping_address { get; set; }
    }
    public class RequestBillingAddress
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }
    public class RequestShippingAddress
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }
    public class RequestCreditCard
    {
        public bool secure { get; set; }
        public string channel { get; set; }
        public string bank { get; set; }
        public RequestInstallment installment { get; set; }
        public string[] whitelist_bins { get; set; }
    }
    public class RequestInstallment
    {
        public bool required { get; set; }
        public RequestTerms terms { get; set; }
    }
    public class RequestTerms
    {
        public int[] bni { get; set; }
        public int[] mandiri { get; set; }
        public int[] cimb { get; set; }
        public int[] bca { get; set; }
        public int[] offline { get; set; }
    }
    public class RequestBcaVa
    {
        public string va_number { get; set; }
        public string sub_company_code { get; set; }
        public RequestFreeText free_text { get; set; }
    }
    public class RequestFreeText
    {
        public List<RequestInquiry> inquiry { get; set; }
        public List<RequestPayment> payment { get; set; }
    }
    public class RequestInquiry
    {
        public string en { get; set; }
        public string id { get; set; }
    }
    public class RequestPayment
    {
        public string en { get; set; }
        public string id { get; set; }
    }
    public class RequestBniVa
    {
        public string va_number { get; set; }
    }
    public class RequestPermataVa
    {
        public string va_number { get; set; }
        public string recipient_name { get; set; }
    }
    public class RequestCallbacks
    {
        public string finish { get; set; }
    }
    public class RequestExpiry
    {
        public string start_time { get; set; }
        public string unit { get; set; }
        public int duration { get; set; }
    }
    #endregion

    #region API Response
    public class ResponseAPIPaymentMidtrans
    {
        public string client_key { get; set; }
        public string token { get; set; }
        public string redirect_url { get; set; }
        public string error_messages { get; set; }
    }
    #endregion

    #region ResponseStatus
    public class ResponseStatusPaymentMidtrans
    {
        public ResponsePaymentMidtransResult response_payment_midtrans_result { get; set; }
    }
    public class ResponsePaymentMidtransResult
    {
        public string name { get; set; }
        public string package_description { get; set; }
        public string status_code { get; set; }
        public string status_message { get; set; }
        public string transaction_id { get; set; }
        public string masked_card { get; set; }
        public string order_id { get; set; }
        public string gross_amount { get; set; }
        public string payment_type { get; set; }
        public string transaction_time { get; set; }
        public string transaction_status { get; set; }
        public string fraud_status { get; set; }
        public string approval_code { get; set; }
        public string bank { get; set; }
        public string card_type { get; set; }
        public string bill_key { get; set; }
        public string biller_code { get; set; }
        public string pdf_url { get; set; }
        public string[] status_message_error { get; set; }

        public string IdChace { get; set; }
    }
    #endregion

    #region SendEmailNotifMidtrans
    public class SendEmailNotifMidtrans
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string url_user_subscribe { get; set; }
        public string description_order_id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string company_name { get; set; }
        public string email { get; set; }
        public string phone_no { get; set; }
        public string total_payment { get; set; }
        public string account_payment_transfer { get; set; }
        public string account_no_payment_transfer { get; set; }
        public string payment_type { get; set; }
        public string status_messages { get; set; }
    }
    #endregion
    #endregion

    #region UserSubscribe
    public class Global_User_Subscribe
    {
        public Guid OrganizationID { get; set; }
        public Guid OrganizationTeamID { get; set; }
        public Guid RoleID { get; set; }
        public Guid UserID { get; set; }
        public Guid TaxPeriodID { get; set; }
        public Guid PayrollPeriodID { get; set; }
        public string Mode { get; set; }
        public string selectedOrganizationName { get; set; }

        //user subscribe
        public tbl_User_Order userSubscribeModel { get; set; }
        public tbl_Order orderSubscribeModel { get; set; }

        //organization
        public tbl_Organization organizationModel { get; set; }

        //organization Admin
        public tbl_Organization organizationAdminModel { get; set; }

        //organization Team
        public tbl_Organization_Team organizationTeamModel { get; set; }

        //organization contact person
        public tbl_Contact_Person contactPersonOrganizationModels { get; set; }
        public List<tbl_Client_Organization_Team> clientOrganizationTeamList { get; set; }

        //user
        public tbl_User userModel { get; set; }
        public tbl_Organization_User organizationUserModel { get; set; }
        public tbl_User_Role userRoleModel { get; set; }
        public List<tbl_User_Role> userRoleList { get; set; }
        public tbl_User_Organization_Team userOrganizationTeamModels { get; set; }

        //tax period
        public tbl_Tax_Period taxPeriodModels { get; set; }
        public tbl_Tax_Period_Month taxPeriodMonthModels { get; set; }
        public List<tbl_Tax_Period> taxPeriodList { get; set; }
        public List<tbl_Tax_Period_Month> taxPeriodMonthList { get; set; }
        public List<tbl_Tax_Period_Month> taxPeriodMonthListTEMP { get; set; }

        //payroll period
        public tbl_Payroll_Period payrollPeriodModels { get; set; }
        public List<tbl_Payroll_Period> payrollPeriodList { get; set; }
        public tbl_Payroll_Period_Detail payrollPeriodDetailModels { get; set; }
        public List<tbl_Payroll_Period_Detail> payrollPeriodDetailList { get; set; }

        public System.Guid taxPeriodID { get; set; }
        public string taxPeriodMonth { get; set; }
        public string[] IdDetail { get; set; }
        public string[] payrollPeriodDetailID { get; set; }
        public string[] taxPeriod { get; set; }
        public string[] periodStart { get; set; }
        public string[] periodEnd { get; set; }

    }
    #endregion

    #region Global_Report_Payroll_Variable
    public class Global_Report_Payroll_Variable
    {
        public string idCache { get; set; }
        public Guid Payroll_Period { get; set; }
        public string[] Variable { get; set; }
        public string Employement_Status { get; set; }
        public string[] Employee_Status { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public List<Report_Payroll_Variable_Template> ReportPayrollVariableTemplate { get; set; }
        public List<ReportApplyLeavePayrollVariable> ReportApplyLeave { get; set; }
    }

    public class Report_Payroll_Variable_Template
    {
        //public string rowId { get; set; }
        public Guid Employee_Id { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public decimal Working_Day { get; set; }
        public decimal Absent_Day { get; set; }
        public decimal Overtime_Day { get; set; }
        public decimal Total_Overtime { get; set; }
        public decimal Weekday_Tier1 { get; set; }
        public decimal Weekday_Tier2 { get; set; }
        public decimal Weekday_Tier3 { get; set; }
        public decimal Holiday_Tier1 { get; set; }
        public decimal Holiday_Tier2 { get; set; }
        public decimal Holiday_Tier3 { get; set; }
        public decimal HIS_Tier1 { get; set; }
        public decimal HIS_Tier2 { get; set; }
        public decimal HIS_Tier3 { get; set; }
        public decimal Total_Leave_Day { get; set; }
    }

    public class ReportApplyLeavePayrollVariable
    {
        public string Employee_No { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
    #endregion

    public class Global_Dashboard
    {
        public List<tbl_Third_Party> ThirdParty { get; set; }
        public List<tbl_Third_Party_Content> ThirdPartyContent { get; set; }

        public Global_VW_Payroll_Slip_Employee_Summary globalVwPayrollSlipEmployeeSummary { get; set; }
        public tbl_Third_Party_Content tblThirdPartyContent { get; set; }
        public List<tbl_Third_Party_Content> List_ThirdPartyContent { get; set; }
        public tbl_Third_Party_Log tblThirdPartyLog { get; set; }
        public tbl_Third_Party_Employee tblThirdPartyEmployee { get; set; }
        public Global_Data_Dashboard DataDashboardModel { get; set; }
        public bool Regisration_Success { get; set; }

    }
        public class Global_Data_Dashboard
        {
        public List<tbl_User_Attendance> ListUserAttendance { get; set; }
        public List<tbl_Apply_Leave> ListEmployeeLeave { get; set; }
        public List<tbl_Employee_Claim> ListEmployeeClaim { get; set; }
        public List<tbl_Employee_Overtime> ListEmployeeOvertime { get; set; }
        public List<tbl_Timeline_Mobile> ListTimelineMobile { get; set; }
    }

        public class GlobalMyAttendance
    {
        public List<Get_Employee_Attendance_Result> ListGetEmployeeAttendance_Result { get; set; }
        public Get_Employee_Attendance_Result GetEmployeeAttendance_Result { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class GlobalMyLeave
    {
        public List<vw_Employee_Leave> ListEmployeeLeave { get; set; }
        public vw_Employee_Leave VwEmployeeLeave { get; set; }
        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }
        public System.Guid Id { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string[] FilterLeaveType { get; set; }
    }

    public class GlobalEmployeAttendance
    {
        public List<tbl_Attendance_Request> ListAttendanceRequest { get; set; }
        public tbl_Attendance_Request ModelAttendanceRequest { get; set; }
        public List<AttendanceRequest> ListRequest { get; set; }
        public List<AttendanceRequestApproval> ListRequestApproval { get; set; }
        public vw_Attendance_Request_Details ModelVwAttendanceRequest { get; set; }
        public List<vw_Attendance_Request_Details> ListVwAttendanceRequest { get; set; }
        public string CutOff { get; set; }
        public string[] ArrayEmployeeNo { get; set; }
        public string[] ArrayEmployeeName { get; set; }
        public string[] ArrayDate { get; set; }
        public string[] ArrayTimeCheckIn { get; set; }
        public string[] ArrayTimeCheckOut { get; set; }
        public string[] ArrayDescription { get; set; }
        public string[] ArrayStatus { get; set; }

        public string[] EmployeeFilter { get; set; }
        public string[] StatusFilter { get; set; }

        public PageQuery pageQuery { get; set; }
        public System.Guid idChace { get; set; }
        public Boolean Is_Success { get; set; }

        //R619
        public string DateTime { get; set; }
        public string DateTimeDayPlusOne { get; set; }
        public string DatetimeMinutePlus { get; set; }

        public List<Global_Error_Code> errMessage { get; set; }
    }

    public class GlobalEmployeLeave
    {
        public List<tbl_Apply_Leave> ListLeaveRequest { get; set; }
        public tbl_Apply_Leave ModelLeaveRequest { get; set; }
    }
    public class AttendanceRequest
    {
        public Guid id { get; set; }
        public DateTime? DateRequest { get; set; }
        public string DateString { get; set; }
        public string TimeInRequest { get; set; }
        public string TimeOutRequest { get; set; }
        public string DescriptionRequest { get; set; }
        public string DateColor { get; set; }
    }

    public class AttendanceRequestApproval
    {
        public Guid id { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? Date { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public DateTime? TimeInRequest { get; set; }
        public DateTime? TimeOutRequest { get; set; }
        public string DescriptionRequest { get; set; }
        public string RemarkApproval { get; set; }
        public string Status { get; set; }
    }

    #region
    public class Global_Cache_Key
    {
        public Guid Employee_Id { get; set; }
        public DateTime Valid_Until { get; set; }
        public string Key { get; set; }
    }
    #endregion

    #region Global_Attendance_Synchronization
    public class Global_Attendance_Synchronization
    {
        public Guid Organization_ID { get; set; }
        public string Username { get; set; }
        public string Language { get; set; }
        public string fileExtension { get; set; }
        public string fileLocationOri { get; set; }
        public string fileLocationInfoError { get; set; }
        public int lengthFile { get; set; }
        public string IdCache { get; set; }
        public string Type_Device { get; set; }
        public string Cut_Off_Time { get; set; }
        public DateTime? Cut_Off_Time_Start { get; set; }
        public DateTime? Cut_Off_Time_End { get; set; }
        public List<ErrorNewUpload> ListErrorNewUpload { get; set; }
        public List<Get_Organization_Attendance_Result> ListGetOrganizationAttendanceResult { get; set; }
        public List<vw_Attendance_Summary_Sync> ListViewAttendanceSummarySync { get; set; }
        public tbl_Attendance_Device AttendanceDevice { get; set; }
        public List<tbl_Attendance_History_Upload> ListAttendanceHistoryUpload { get; set; }
        public tbl_Attendance_Summary_Sync AttendanceSummarySync { get; set; }
        public List<tbl_Attendance_Summary_Sync> ListAttendanceSummarySync { get; set; }
        public List<vw_Attendance_Leave> ListvwAttendanceLeave { get; set; }
        public tbl_Cut_Off_Period CutOffPeriod { get; set; }
        public List<tbl_Attendance_Priority> ListAttendancePriority { get; set; }
        public tbl_User_Attendance UserAttendance { get; set; }
        public List<tbl_User_Attendance> ListUserAttendance { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
        public PageQuery pageQuery { get; set; }
        public string urlOri { get; set; }
        public string urlInfoError { get; set; }
        public string FileName { get; set; }
        //public int relativeColumn { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public int Warning { get; set; }
        public int TotalRow { get; set; }
        public Guid?[] Edited_Attendance { get; set; }
    }
    #endregion

    public class ErrorInfo
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string Description_2 { get; set; }
    }

    public class GlobalApprovalLeave
    {
        public List<vw_Employee_Leave> ListViewEmployeeLeave { get; set; }
        public vw_Employee_Leave vwEmployeeLeave { get; set; }
        public PageQuery pageQuery { get; set; }
        public string[] FilterEmployee { get; set; }
        public string[] FilterLeaveType { get; set; }
        public List<Global_Error_Code> errMessage { get; set; }
    }
    
	#region
    public class Global_Payroll_Detail_Calculation
    {
        public tbl_Payroll_Calculation DataModel { get; set; }
        public tbl_Payroll_Calculation DataModelSeverance { get; set; }
        public Guid id { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public Guid Employee_Id { get; set; }
        public vw_Payroll_Detail_Calculation vWpayrollDetailCalculation { get; set; }
        public List<vw_Payroll_Detail_Calculation> ListvWpayrollDetailCalculation { get; set; }
        public System.Guid idChace { get; set; }
        public PageQuery pageQuery { get; set; }
    }
    #endregion

    public class Global_SessionKey
    {
        public Nullable<System.Guid> User_Id { get; set; }
        public string KeyPush { get; set; }
        public string Salt { get; set; }
        public System.Guid Employee_Id { get; set; }
        public Nullable<bool> Is_login { get; set; }
        public string Device { get; set; }
    }

    public class PayrollHistoryYearDate_Result
    {
        public int Id { get; set; }
        public string type { get; set; }
        public Guid Organization_ID { get; set; }
        public Guid Employee_ID { get; set; }
        public int RN { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public string Gender { get; set; }
        public string Tax_Status { get; set; }
        public string NPWP { get; set; }
        public string Payment_Type_ID { get; set; }
        public string Component_Code { get; set; }
        public string Description_Component { get; set; }
        public Nullable<double> Value_Amount { get; set; }
        public string TaxPolicy { get; set; }
        public string Employment_Status { get; set; }
    }

}