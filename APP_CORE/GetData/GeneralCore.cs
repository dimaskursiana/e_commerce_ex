using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APP_MODEL.ModelData;
using System.Threading;
using System.Threading.Tasks;

namespace APP_CORE.GetData
{

    public class GeneralCore
    {
        public static IQueryable<tbl_Base_Location> BaseLocationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Base_Location> Data = null;
            Data = db.tbl_Base_Location.Where(p => p.Organization_Id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<vw_Base_Location_List> VwBaseLocationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Base_Location_List> Data = null;
            Data = db.vw_Base_Location_List.Where(p => p.Organization_Id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STR_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<tbl_Organization> OrganizationByUserQuery(Guid UserId)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Organization> Data = null;
            List<string> Organization_Team = db.tbl_User_Organization_Team.Where(p => p.User_ID == UserId && !(p.tbl_Organization_Team.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Organization_Team.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.Organization_Team_ID.ToString()).ToList();
            List<string> Organization_In_Team = db.tbl_Client_Organization_Team.Where(p => Organization_Team.Contains(p.Organization_Team_ID.ToString()) && !(p.tbl_Organization_Team.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Organization_Team.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.tbl_Organization.Organization_Code).ToList();
            Data = db.tbl_Organization.Where(p => Organization_In_Team.Contains(p.Organization_Code) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<vw_Report_Setting> VWPayrollReportSettingQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Report_Setting> Data = null;
            Data = db.vw_Report_Setting.Where(p => p.Organization_id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Record_Status == CoreVariable.CONST_STR_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Exchange_File> ExchangeFileQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Exchange_File> Data = null;
            Data = db.tbl_Exchange_File.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE);
            return Data;
        }

        public static IQueryable<tbl_Employee_Document> EmployeeDocumentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Document> Data = null;
            Data = db.tbl_Employee_Document.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Employee_ID == UserData.EmployeeSelected.id && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE);
            return Data;
        }

        public static IQueryable<tbl_Report_Setting> PayrollReportSettingQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Report_Setting> Data = null;
            Data = db.tbl_Report_Setting.Where(p => p.Organization_id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<vw_Employee_Appointment_WorkingTime> AppointmentWorkingTimeQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Employee_Appointment_WorkingTime> Data = null;
            Data = db.vw_Employee_Appointment_WorkingTime.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_Role> RoleByOrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Role> Data = null;
            Data = db.tbl_Role.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_Employee_Address> EmployeeAddressQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Address> Data = null;
            Data = db.tbl_Employee_Address.Where(p => p.Employee_ID == UserData.EmployeeSelected.id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Upload_Staging> UploadDataSummary()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Upload_Staging> Data = null;
            Data = db.tbl_Upload_Staging.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            //Data = Data.Where(p => (p.Status_Code == 1 || p.Status_Code == 2));
            return Data;
        }

        public static IQueryable<tbl_Upload_Staging> UploadDataSummary(Guid? OrganizationSelected_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Upload_Staging> Data = null;
            Data = db.tbl_Upload_Staging.Where(p => p.Organization_ID == OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            //Data = Data.Where(p => (p.Status_Code == 1 || p.Status_Code == 2));
            return Data;
        }

        public static IQueryable<tbl_Payslip> PayslipQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payslip> Data = null;
            Data = db.tbl_Payslip.Where(p => p.Employee_ID == UserData.EmployeeSelected.id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Tax> TaxQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Tax> Data = null;
            Data = db.tbl_Tax.Where(p => p.Employee_ID == UserData.EmployeeSelected.id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Working_Time> WorkingTimeSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Working_Time> Data = null;
            Data = db.tbl_Organization_Working_Time.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Structure> PositionSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Structure> Data = null;
            Data = db.tbl_Organization_Structure.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Struktur == CoreVariable.CONST_POSITIONS && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Structure> DivisionSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Structure> Data = null;
            Data = db.tbl_Organization_Structure.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Struktur == CoreVariable.CONST_DIVISIONS && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Structure> DepartmentSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Structure> Data = null;
            Data = db.tbl_Organization_Structure.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Struktur == CoreVariable.CONST_DEPARTMENTS && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Structure> GradeSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Structure> Data = null;
            Data = db.tbl_Organization_Structure.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Struktur == CoreVariable.CONST_GRADES && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Employee_Appointment> CostCenterSelectListQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            List<Guid> ListEmployeeID = new List<Guid>();
            ListEmployeeID = db.tbl_Employee.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(s => s.id).ToList();
            IQueryable<tbl_Employee_Appointment> Data = null;
            Data = db.tbl_Employee_Appointment.Where(p => ListEmployeeID.Contains(p.Employee_ID) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Tax_Status_Effective_Year> TaxStatusEffectiveYearQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Tax_Status_Effective_Year> Data = null;
            Data = db.tbl_Tax_Status_Effective_Year.Where(p => p.tbl_Tax.Employee_ID == UserData.EmployeeSelected.id && !(p.tbl_Tax.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Tax.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
         

        public static IQueryable<tbl_Employee> PersonalInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee> Data = null;
            Data = db.tbl_Employee.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.Full_Name);
            return Data;
        }

        public static IQueryable<tbl_Employee> PersonalInformationQuery(Guid? OrganizationSelected_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Employee> Data = null;
            Data = db.tbl_Employee.Where(p => p.Organization_ID == OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.Full_Name);
            return Data;
        }

        public static IQueryable<tbl_Employee_ID_Information> EmployeeIDInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_ID_Information> Data = null;
            Data = db.tbl_Employee_ID_Information.Where(p => p.tbl_Employee.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_Employee_Family> EmployeeFamillyInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Family> Data = null;
            Data = db.tbl_Employee_Family.Where(p => p.tbl_Employee.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_Employee> EmployeeActiveAuthorizedQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee> Data = null;
            Data = db.tbl_Employee.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.Full_Name);
            return Data;
        }

        public static IQueryable<Get_Employee_By_Period_Result> EmployeeQuery(bool isPermanent)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<Get_Employee_By_Period_Result> Data = null;
            if (isPermanent)
                Data = db.Get_Employee_By_Period(UserData.OrganizationSelected_Id).Where(p=>p.Employee_Status.Contains("Non Permanent"));
            else
                Data = db.Get_Employee_By_Period(UserData.OrganizationSelected_Id).Where(p => !p.Employee_Status.Contains("Non Permanent"));
            return Data;
        }


        public static IQueryable<tbl_User_Role> UserRoleByOrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User_Role> Data = null;
            Data = db.tbl_User_Role.Where(p => p.tbl_Role.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_Organization_Team> TeamByOrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Team> Data = null;
            Data = db.tbl_Organization_Team.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Holiday_Calendar> NationalHolidayCalender()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Holiday_Calendar> Data = null;
            Data = db.tbl_Holiday_Calendar.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Holiday_Calendar> NationalHolidayCalenderActiveApprove()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Holiday_Calendar> Data = null;
            Data = NationalHolidayCalender().Where(p => p.Holiday_Date.Year == DateTime.Now.Year && p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE);
            return Data;
        }


        public static IQueryable<tbl_Payroll_Calculation> PayrollCalculation()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Payroll_Calculation> Data = null;
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.tbl_Payroll_Calculation.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Calculation_Summary> PayrollSummaryCalculation()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<vw_Calculation_Summary> Data = null;
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.vw_Calculation_Summary.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Payroll_Closing> PayrollClosing()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Payroll_Closing> Data = null;
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.tbl_Payroll_Closing.Where(p => p.tbl_Payroll_Calculation.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
		
        public static IQueryable<vw_Employee_Payslip_Summary> EmployeePayslip()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<vw_Employee_Payslip_Summary> Data = null;
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.vw_Employee_Payslip_Summary.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_User_Organization_Team> UserTeamByOrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User_Organization_Team> Data = null;
            Data = db.tbl_User_Organization_Team.Where(p => p.tbl_Organization_Team.Organization_ID == UserData.OrganizationSelected_Id);
            return Data;
        }

        public static IQueryable<tbl_User_Organization_Team> UserTeamByUserQuery(Guid UserID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User_Organization_Team> Data = null;
            Data = db.tbl_User_Organization_Team.Where(p => p.User_ID == UserID);
            return Data;
        }

        public static IQueryable<tbl_User> UserQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User> Data = null;
            var ListUserID = db.tbl_Organization_User.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id).Select(p => p.User_ID).ToList();
            Data = db.tbl_User.Where(p => ListUserID.Contains(p.id) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<tbl_User> UserQueryValidasi()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User> Data = null;
            Data = db.tbl_User.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        //public static IQueryable<tbl_Employee_Leave_Entitlement> EmployeeLeaveMasterQuery()
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    CoreVariable CoreUserVariable = new CoreVariable();
        //    User_Data UserData = CoreUserVariable.CoreUserLogin();
        //    IQueryable<tbl_Employee_Leave_Entitlement> Data = null;
        //    Data = db.tbl_Employee_Leave_Entitlement.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
        //    return Data;
        //}
        public static IQueryable<vw_Employee_Leave_Entitlement_Summary> EmployeeLeaveMasterQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Employee_Leave_Entitlement_Summary> Data = null;
            Data = db.vw_Employee_Leave_Entitlement_Summary.Where(p => p.Organization_Id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Leave_Types>LeaveTypesQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Leave_Types> Data = null;
            Data = db.tbl_Leave_Types.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization> OrganizationByRoleUserQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization> Data = null;
            string Role = UserData.OrganizationSelected.Organization_Service;
            List<string> Organization_Team = db.tbl_User_Organization_Team.Where(p => p.User_ID == UserData.UserId && !(p.tbl_Organization_Team.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Organization_Team.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.Organization_Team_ID.ToString()).ToList();
            List<string> Organization_In_Team = db.tbl_Client_Organization_Team.Where(p => Organization_Team.Contains(p.Organization_Team_ID.ToString()) && !(p.tbl_Organization_Team.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Organization_Team.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.tbl_Organization.Organization_Code).ToList();
            //IQueryable<tbl_Organization> OrganizationQuery = db.tbl_Organization;
            IQueryable<tbl_Organization> OrganizationQuery =  db.tbl_Organization.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
           

            if (UserData.OrganizationSelected.Organization_Service.Trim() == CoreVariable.CONST_END_CLIENT)
            {
                OrganizationQuery = OrganizationQuery.Where(p => p.id == UserData.OrganizationSelected_Id);
            }
            else
            {
                OrganizationQuery = OrganizationQuery.Where(p => p.Parent_Organization_Code == UserData.OrganizationSelected.Organization_Code || Organization_In_Team.Contains(p.Organization_Code));
            }
            OrganizationQuery = OrganizationQuery.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));

            if (Role == CoreVariable.CONST_ADMIN)
            {
                List<string> ListOrganizationCodeAdmin = OrganizationQuery.Where(p => p.Organization_Code == UserData.OrganizationSelected.Organization_Code).Select(p => p.Organization_Code).ToList();
                List<string> ListOrganizationCodeOutsorcing = OrganizationQuery.Where(p => ListOrganizationCodeAdmin.Contains(p.Parent_Organization_Code)).Select(p => p.Organization_Code).ToList();
                List<string> ListOrganizationCodeUser = OrganizationQuery.Where(p => ListOrganizationCodeOutsorcing.Contains(p.Parent_Organization_Code)).Select(p => p.Organization_Code).ToList();

                Data = OrganizationQuery.Where(p => ListOrganizationCodeAdmin.Contains(p.Organization_Code) || ListOrganizationCodeOutsorcing.Contains(p.Organization_Code) || ListOrganizationCodeUser.Contains(p.Organization_Code));

                return Data;
            }
            else if (Role == CoreVariable.CONST_PAYROL_OUTSOURCING)
            {
                Data = OrganizationQuery.Where(p => p.Organization_Code == UserData.OrganizationSelected.Organization_Code || p.Parent_Organization_Code == UserData.OrganizationSelected.Organization_Code);
                return Data;
            }
            else
            {
                Data = OrganizationQuery;
                return Data;
            }
        }

        public static IQueryable<tbl_HeadOffice_Branch> HeadOfficeBranchQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_HeadOffice_Branch> Data = null;
            Data = db.tbl_HeadOffice_Branch.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_HeadOffice_Branch_Location> LocationHeadOfficeBranchQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_HeadOffice_Branch_Location> Data = null;
            Data = db.tbl_HeadOffice_Branch_Location.Where(p => p.tbl_HeadOffice_Branch.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_HeadOffice_Branch.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_HeadOffice_Branch.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        //public static IQueryable<vw_summary_org_holliday> HolidayCalendarOrganizationQuery()
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    CoreVariable CoreUserVariable = new CoreVariable();
        //    User_Data UserData = CoreUserVariable.CoreUserLogin();
        //    IQueryable<vw_summary_org_holliday> Data = null;
        //    Guid Holiday_National = Guid.Parse("00000000-0000-0000-0000-000000000000");
        //    Data = db.vw_summary_org_holliday.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id || p.Organization_ID == Holiday_National && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
        //    return Data;
        //}

        public static IQueryable<tbl_Holiday_Calendar_Organization> HolidayCalendarOrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Holiday_Calendar_Organization> Data = null;
            Data = db.tbl_Holiday_Calendar_Organization.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Structure> OrganizationStructurQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Structure> Data = null;
            Data = db.tbl_Organization_Structure.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Contact_Person> ContactPersonQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Contact_Person> Data = null;
            Data = db.tbl_Contact_Person.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Cost_Parameter> CostParameterQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Cost_Parameter> Data = null;
            Data = db.tbl_Cost_Parameter.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Bank_Information> BankInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Bank_Information> Data = null;
            Data = db.tbl_Bank_Information.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Bank_Information> BankInformationQuery(Guid OrganizationSelected_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Bank_Information> Data = null;
            Data = db.tbl_Bank_Information.Where(p => p.Organization_ID == OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Exchange_Rate> ExchangeRateQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Exchange_Rate> Data = null;
            Data = db.tbl_Exchange_Rate.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Group> OrganizationGroupQuery()
        {
            IQueryable<tbl_Organization> modelQuery = GeneralCore.OrganizationByRoleUserQuery();
            List<string> organiazationByTeam = modelQuery.Select(p => p.id.ToString()).ToList();

            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var UserOrganiztaionID = db.tbl_Organization_User.Where(p => p.User_ID == UserData.UserId).FirstOrDefault();
            IQueryable<tbl_Organization_Group> Data = null;
            Data = db.tbl_Organization_Group.Where(p => p.Organization_ID == UserOrganiztaionID.Organization_ID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            Data = Data.Where(p => organiazationByTeam.Contains(p.Organization_ID.ToString()));
            return Data;
        }

        public static IQueryable<tbl_Organization_Group> OrganizationGroupSummaryQuery()
        {
            IQueryable<tbl_Organization> modelQuery = GeneralCore.OrganizationByRoleUserQuery();
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            List<string> idHeader = db.tbl_Organization_Group_Detail.Where(p => p.Organization_Client_ID == UserData.OrganizationSelected.id).Select(p => p.Organization_Group_ID.ToString()).ToList();
            IQueryable<tbl_Organization_Group> Data = null;
            Data = db.tbl_Organization_Group.Where(p => idHeader.Contains(p.id.ToString()) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Exchange_Rate_Details> ExchangeRateDetailQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Exchange_Rate_Details> Data = null;
            Data = db.tbl_Exchange_Rate_Details.Where(p => p.tbl_Exchange_Rate.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_Exchange_Rate.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Exchange_Rate.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_BPJS_Healthcare> BPJSHealthcareQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_BPJS_Healthcare> Data = null;
            Data = db.tbl_BPJS_Healthcare.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<tbl_BPJS_Manpower> BPJSManpowerQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_BPJS_Manpower> Data = null;
            Data = db.tbl_BPJS_Manpower.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<tbl_Organization_Working_Time> OrganizationWorkingTimeQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Working_Time> Data = null;
            Data = db.tbl_Organization_Working_Time.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_General_Parameter> GeneralParameterQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_General_Parameter> Data = null;
            Data = db.tbl_General_Parameter.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Role> RoleQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Role> Data = null;
            Data = db.tbl_Role.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_List_Of_Bank> ListBankQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_List_Of_Bank> Data = null;
            Data = db.tbl_List_Of_Bank.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Account_Group_Maintenence> OrganizationAccountGroupMaintenenceQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Account_Group_Maintenence> Data = null;
            Data = db.tbl_Organization_Account_Group_Maintenence.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Branch_List_Of_Bank> ListBankBranchQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Branch_List_Of_Bank> Data = null;
            Data = db.tbl_Branch_List_Of_Bank;
            return Data;
        }

        public static IQueryable<tbl_SysParam> SystemParameterQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite(); 
            IQueryable<tbl_SysParam> Data = null;
            Data = db.tbl_SysParam;
            return Data;
        }

        public static IQueryable<tbl_Menu> Menu()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Menu> Data = null;
            Data = db.tbl_Menu;
            return Data;
        }

        public static IQueryable<tbl_Organization_Team> OrganizationTeamQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Team> Data = null;
            Data = db.tbl_Organization_Team.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization> OrganizationByUserTeamQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization> Data = null;
            var TeamID = db.tbl_User_Organization_Team.Where(p => p.User_ID == UserData.UserId).FirstOrDefault();
            Data = db.tbl_Client_Organization_Team.Where(p => p.Organization_Team_ID == TeamID.Organization_Team_ID).Select(p => p.tbl_Organization).Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderBy(o=>o.Organization_Name);
            return Data;
        }


        public static IQueryable<tbl_Organization> OrganizationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization> Data = null;
            Data = db.tbl_Organization.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Employee_Payment> EmployeePaymentInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Payment> Data = null;
            Data = db.tbl_Employee_Payment.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        } 
        public static IQueryable<tbl_Employee_Payroll_Component> EmployeePayrollComponentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Payroll_Component> Data = null;
            Data = db.tbl_Employee_Payroll_Component.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            Data = Data.Where(p => p.Employee_id == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<vw_Employee_Payroll_Component> VWEmployeePayrollComponentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Employee_Payroll_Component> Data = null;
            Data = db.vw_Employee_Payroll_Component.Where(p => !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STR_STATUS_DELETED));
            Data = Data.Where(p => p.Employee_id == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<vw_Employee_Payroll_Component> VWListAllEmployeePayrollComponentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Employee_Payroll_Component> Data = null;
            Data = db.vw_Employee_Payroll_Component.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STR_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Non_Taxable_Income_Parameter> NonTaxableIncomeParameterQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Non_Taxable_Income_Parameter> Data = null;
            Data = db.tbl_Non_Taxable_Income_Parameter.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
             
            return Data;
        }

        public static IQueryable<vw_Organization_Payroll_Component_Summary> OrganizationPayrollComponentSummaryQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<vw_Organization_Payroll_Component_Summary> Data = null;
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.vw_Organization_Payroll_Component_Summary.Where(p => p.Organization_id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Status == CoreVariable.CONST_STR_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Payroll_Component> OrganizationPayrollComponentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Organization_Payroll_Component> Data = null;
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.tbl_Organization_Payroll_Component.Where(p => p.Organization_id == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.Description);
            return Data;
        }

        public static IQueryable<vw_Additional_Payroll> AdditionalPayrollQuery()
        {
            
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Additional_Payroll> Data = null;
             Data = db.vw_Additional_Payroll.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id  && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Record_Status == CoreVariable.CONST_STR_STATUS_DELETED));
            //Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<tbl_Additional_Payroll> AdditionalPayrollTableQuery()
        {

            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Additional_Payroll> Data = null;
            Data = db.tbl_Additional_Payroll.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            //Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<tbl_Additional_Payroll_Detail> AdditionalPayrollDetailQuery()
        {

            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Additional_Payroll_Detail> Data = null;
            Data = db.tbl_Additional_Payroll_Detail.Where(p => p.tbl_Additional_Payroll.Organization_ID == UserData.OrganizationSelected_Id && p.tbl_Additional_Payroll.Status_Code == CoreVariable.CONST_STATUS_ACTIVE && !(p.tbl_Additional_Payroll.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Additional_Payroll.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            //Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<tbl_Payroll_Variable> PayrollVariableTblQuery()
        {

            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Variable> Data = null;
            Data = db.tbl_Payroll_Variable.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            //Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        }

        public static IQueryable<vw_Payroll_Variable> PayrollVariableQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Payroll_Variable> Data = null;
            Data = db.vw_Payroll_Variable.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Record_Status == CoreVariable.CONST_STR_STATUS_DELETED));
            // Data = Data.Where(p => p.Employee_ID == UserData.EmployeeSelected.id);
            return Data;
        }
        public static IQueryable<tbl_Payroll_Period_Detail> PayrollPeriodForPayrollVariableQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Period_Detail> Data = null;
            Data = db.tbl_Payroll_Period_Detail.Where(p => p.tbl_Payroll_Period.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_Payroll_Period.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            Data = Data.Where(p => p.Tax_Period.Month == DateTime.Now.Month);
            return Data;
        }

        public static IQueryable<tbl_Payroll_Closing> Payroll_Closing()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Closing> Data = null;
            Data = db.tbl_Payroll_Closing.Where(p => p.tbl_Payroll_Calculation.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_Payroll_Calculation.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Payroll_Calculation.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Payroll_Period> PayrollPeriodQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Period> Data = null;
            Data = db.tbl_Payroll_Period.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Payroll_Period_Detail> PayrollPeriodDetailQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Period_Detail> Data = null;
            Data = db.tbl_Payroll_Period_Detail.Where(p => p.tbl_Payroll_Period.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_Payroll_Period.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Payroll_Period_Detail> PayrollPeriodDetailQuery(Guid? OrganizationSelected_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Payroll_Period_Detail> Data = null;
            Data = db.tbl_Payroll_Period_Detail.Where(p => p.tbl_Payroll_Period.Organization_ID == OrganizationSelected_Id && !(p.tbl_Payroll_Period.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Payroll_Period_Summary> vwPayrollPeriodQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Payroll_Period_Summary> Data = null;
            Data = db.vw_Payroll_Period_Summary.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Payroll_Schedule> PayrollScheduleQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Payroll_Schedule> Data = null;
            Data = db.tbl_Payroll_Schedule.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Tax_Period> TaxPeriodQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Tax_Period> Data = null;
            Data = db.tbl_Tax_Period.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Tax_Period> TaxPeriodQuery( Guid? OrganizationSelected_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Tax_Period> Data = null;
            Data = db.tbl_Tax_Period.Where(p => p.Organization_ID == OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Tax_Period_Summary> vwTaxPeriodQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Tax_Period_Summary> Data = null;
            Data = db.vw_Tax_Period_Summary.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_appointment_information> AppointmentInformationQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<vw_appointment_information> Data = null;
            Data = db.vw_appointment_information.Where(p => !(p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Record_Status == CoreVariable.CONST_STR_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Employee_Appointment> AppointmentQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin(); 
            IQueryable<tbl_Employee_Appointment> Data = null;
            Data = db.tbl_Employee_Appointment.Where(p => p.tbl_Employee.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Appointment_Status_Information> AppointmentInformationStatusQuery(Guid Employee_ID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Appointment_Status_Information> Data = null;
            Data = db.tbl_Appointment_Status_Information.Where(p => p.tbl_Employee_Appointment.Employee_ID == Employee_ID && !(p.tbl_Employee_Appointment.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Employee_Appointment.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Appointment_Status_Information> AppointmentInformationStatusByOrganization()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<tbl_Appointment_Status_Information> Data = null;
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            Data = db.tbl_Appointment_Status_Information.Where(p => p.tbl_Employee_Appointment.tbl_Employee.Organization_ID == UserData.OrganizationSelected_Id && !(p.tbl_Employee_Appointment.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Employee_Appointment.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Loan> vwLoanQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Loan> Data = null;
            Data = db.vw_Loan.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Compensation_Benefit> vwCompensationAndBenefitQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Compensation_Benefit> Data = null;
            Data = db.vw_Compensation_Benefit.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Organization_Email_Setup_Summary> vwOrganizationEmailSetup()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Organization_Email_Setup_Summary> Data = null;
            Data = db.vw_Organization_Email_Setup_Summary.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<tbl_Organization_Email_Setup> OrganizationEmailSetup()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Organization_Email_Setup> Data = null;
            Data = db.tbl_Organization_Email_Setup.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }

        public static IQueryable<vw_Payroll_Slip_Employee_Summary> vwPayrollSlipEmployeeQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Payroll_Slip_Employee_Summary> Data = null;
            Data = db.vw_Payroll_Slip_Employee_Summary.OrderBy(o => o.Tax_Period);
            return Data;
        }

        public static IQueryable<tbl_Blog_Posts> blogQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Blog_Posts> Data = null;
            Data = db.tbl_Blog_Posts.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(o=>o.Created_DateTime);
            return Data;
        }

        public static IQueryable<vw_Blog_Summary> vwBlogQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<vw_Blog_Summary> Data = null;
            Data = db.vw_Blog_Summary.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(o => o.Created_DateTime);
            return Data;
        }

        public static IQueryable<tbl_Menu_Blog> CategoryBlogMenuQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Menu_Blog> Data = null;
            Data = db.tbl_Menu_Blog.Where(p => p.Show == true).OrderBy(o => o.Menu_Position);
            return Data;
        }
		
		public static IQueryable<vw_Employee_Approval> ApprovalHierarchyQuery(Guid OrganizationID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            IQueryable<vw_Employee_Approval> Data = null;
            Data = db.vw_Employee_Approval.Where(p => p.Organization_ID == OrganizationID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
		
        public static IQueryable<Get_Summary_Record_Result> SummaryRecordQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<Get_Summary_Record_Result> Data = null;
            Data = db.Get_Summary_Record(UserData.OrganizationSelected_Id);
            return Data;
        }
		
		        public static IQueryable<tbl_Blog_Posts> BlogPost()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<tbl_Blog_Posts> Data = null;
            Data = db.tbl_Blog_Posts.Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Created_DateTime);
            return Data;
        }

        public static IQueryable<tbl_User_Attendance> UserAttendanceQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_User_Attendance> Data = null;
            Data = db.tbl_User_Attendance.Where(p=>p.User_Id == UserData.UserId);
            return Data;
        }
        public static IQueryable<tbl_Apply_Leave> EmployeeLeaveQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Apply_Leave> Data = null;
            Data = db.tbl_Apply_Leave.Where(p => p.Employee_Id == UserData.EmployeeId);
            return Data;
        }
        public static IQueryable<tbl_Employee_Claim> EmployeeClaimQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Claim> Data = null;
            Data = db.tbl_Employee_Claim.Where(p => p.Employee_ID == UserData.UserId);
            return Data;
        }
        public static IQueryable<tbl_Employee_Overtime> EmployeeOvertimeQuery()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee_Overtime> Data = null;
            Data = db.tbl_Employee_Overtime.Where(p => p.Employee_ID == UserData.UserId);
            return Data;
        }
        public static IQueryable<vw_Attendance_Request_Details> AttendanceRequestDetailsQuery(User_Data UserData)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<vw_Attendance_Request_Details> Data = null;
            Data = db.vw_Attendance_Request_Details.Where(p => p.Reportto_ID == UserData.EmployeeSelected.id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            return Data;
        }
        public static IQueryable<Get_Employee_Attendance_Result> MyAttendanceQuery (User_Data UserData,DateTime? Cutstart,DateTime? CutEnd)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<Get_Employee_Attendance_Result> Data = null;
            if (Cutstart == null && CutEnd == null)
            {
               
                Cutstart = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
                CutEnd = Convert.ToDateTime("12/31/" + DateTime.Now.Year);
                Data = db.Get_Employee_Attendance(UserData.EmployeeId, Cutstart, CutEnd).OrderBy(s => s.Date);
            }
            else
                Data = db.Get_Employee_Attendance(UserData.EmployeeId, Cutstart, CutEnd).OrderBy(s => s.Date);
            return Data;
        }
        public static IQueryable<vw_Employee_Leave> MyLeaveQuery(User_Data UserData, DateTime? Cutstart, DateTime? CutEnd)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<vw_Employee_Leave> Data = null;
            if (Cutstart == null && CutEnd == null)
            {

                Cutstart = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
                CutEnd = Convert.ToDateTime("12/31/" + DateTime.Now.Year);
                //Data = db.vw_Employee_Leave.Where(s => s.Employee_Id == UserData.EmployeeId && s.From_Date >= Cutstart && s.To_Date <= CutEnd).OrderBy(s => s.From_Date);
               var tmp = db.tbl_Apply_Leave_Detail.Where(s => s.tbl_Apply_Leave.Employee_Id == UserData.EmployeeId && (s.Leave_Date.Value.Year >= Cutstart.Value.Year && s.Leave_Date.Value.Month >= Cutstart.Value.Month && s.Leave_Date.Value.Day >= Cutstart.Value.Day) && (s.Leave_Date.Value.Year <= CutEnd.Value.Year && s.Leave_Date.Value.Month <= CutEnd.Value.Month && s.Leave_Date.Value.Day <= CutEnd.Value.Day)).Select(s => s.Leave_Id);
                Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
            }
            else
            {
                if (Cutstart == CutEnd)
                {
                    var tmp = db.tbl_Apply_Leave_Detail.Where(s => s.Leave_Date.Value.Year == Cutstart.Value.Year && s.Leave_Date.Value.Month == Cutstart.Value.Month && s.Leave_Date.Value.Day == Cutstart.Value.Day && s.tbl_Apply_Leave.Employee_Id == UserData.EmployeeId).Select(s => s.Leave_Id);
                    if (tmp != null)
                        Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
                }
                else
                {
                    var tmp = db.tbl_Apply_Leave_Detail.Where(s => (s.Leave_Date.Value.Year >= Cutstart.Value.Year && s.Leave_Date.Value.Month >= Cutstart.Value.Month && s.Leave_Date.Value.Day >= Cutstart.Value.Day) && (s.Leave_Date.Value.Year <= CutEnd.Value.Year && s.Leave_Date.Value.Month <= CutEnd.Value.Month && s.Leave_Date.Value.Day <= CutEnd.Value.Day) && s.tbl_Apply_Leave.Employee_Id == UserData.EmployeeId).Select(s => s.Leave_Id);
                    if (tmp != null)
                        Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
                }
            }
            //Data = db.vw_Employee_Leave.Where(s => s.Employee_Id == UserData.EmployeeId && s.From_Date >= Cutstart && s.To_Date <= CutEnd).OrderBy(s => s.From_Date);
            return Data;
        }
		
		public static IQueryable<vw_Employee_Leave> ApprovalLeaveQuery(User_Data UserData, DateTime? Cutstart, DateTime? CutEnd)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            IQueryable<vw_Employee_Leave> Data = null;
            var EmpApproval = db.tbl_Employee_Approval.Where(p => p.Report_To == UserData.EmployeeId).Select(s => s.Employee_Id).ToList();
            if (Cutstart == null && CutEnd == null)
            {
                Cutstart = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
                CutEnd = Convert.ToDateTime("12/31/" + DateTime.Now.Year);
                //Data = db.vw_Employee_Leave.Where(s => EmpApproval.Contains(s.Employee_Id) && s.From_Date >= Cutstart && s.To_Date <= CutEnd).OrderBy(s => s.From_Date);
                var tmp = db.tbl_Apply_Leave_Detail.Where(s => EmpApproval.Contains(s.tbl_Apply_Leave.Employee_Id) && (s.Leave_Date.Value.Year >= Cutstart.Value.Year && s.Leave_Date.Value.Month >= Cutstart.Value.Month && s.Leave_Date.Value.Day >= Cutstart.Value.Day) && (s.Leave_Date.Value.Year <= CutEnd.Value.Year && s.Leave_Date.Value.Month <= CutEnd.Value.Month && s.Leave_Date.Value.Day <= CutEnd.Value.Day)).Select(s => s.Leave_Id);
                Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
            }
            else
            {
                if (Cutstart == CutEnd)
                {
                    var tmp = db.tbl_Apply_Leave_Detail.Where(s => s.Leave_Date.Value.Year == Cutstart.Value.Year && s.Leave_Date.Value.Month == Cutstart.Value.Month && s.Leave_Date.Value.Day == Cutstart.Value.Day && EmpApproval.Contains(s.tbl_Apply_Leave.Employee_Id)).Select(s => s.Leave_Id);
                    if (tmp != null)
                        Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
                }
                else
                {
                    var tmp = db.tbl_Apply_Leave_Detail.Where(s => (s.Leave_Date.Value.Year >= Cutstart.Value.Year && s.Leave_Date.Value.Month >= Cutstart.Value.Month && s.Leave_Date.Value.Day >= Cutstart.Value.Day) && (s.Leave_Date.Value.Year <= CutEnd.Value.Year && s.Leave_Date.Value.Month <= CutEnd.Value.Month && s.Leave_Date.Value.Day <= CutEnd.Value.Day) && EmpApproval.Contains(s.tbl_Apply_Leave.Employee_Id)).Select(s => s.Leave_Id);
                    if (tmp != null)
                        Data = db.vw_Employee_Leave.Where(s => tmp.Contains(s.Id)).OrderBy(s => s.From_Date);
                }
            }
            return Data;
        }

    }
}