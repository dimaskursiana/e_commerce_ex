using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using APP_CORE.GetData;
using APP_CORE;
using System.Globalization;


namespace APP_COMMON
{
    public class UISelectlist
    {
        /// <summary>
        /// Created By : Ali Mubarokah
        /// Created Date : 20 Feb 2017
        /// Purpose : UI Selectlist 
        ///  
        //public static ModelEntitiesWebsite ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        private static string strMessage = string.Empty;

        #region Format Get Selectlist
        //public static List<SelectListItem> FunctionName()
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> ListModel = new List<SelectListItem>();
        //    foreach (var item in db.MODEL)
        //    {
        //        ListModel.Add(new SelectListItem
        //        {
        //            Text = item.VALUE_TEXT.ToString(),  
        //            Value = item.VALUE_ID.ToString()
        //        });
        //    }
        //    return ListModel;
        //}
        public static List<SelectListItem> ListMapLocation(String str)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            if (str.ToUpper() == "COUNTRY")
            {
                //var query = (from o in db.tbl_Map_Location
                //             select new
                //             {
                //                 o.Country
                //             }).Distinct().OrderBy(x => x.Country).ToList();
                var query = db.tbl_Map_Location.GroupBy(s => s.Country).Select(x => new { Country = x.FirstOrDefault().Country }).ToList();
                foreach (var item in query.OrderBy(s => s.Country))
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Country.ToString(),
                        Value = item.Country.ToString()
                    });
                }
            }
            else if (str.ToUpper() == "PROVINCE")
            {
                //var query = (from o in db.tbl_Map_Location
                //             select new
                //             {
                //                 o.Province
                //             }).Distinct().OrderBy(x => x.Province).ToList();
                var query = db.tbl_Map_Location.GroupBy(s => s.Province).Select(x => new { Province = x.FirstOrDefault().Province }).ToList();
                foreach (var item in query.OrderBy(s => s.Province))
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Province.ToString(),
                        Value = item.Province.ToString()
                    });
                }
            }
            else if (str.ToUpper() == "CITY")
            {
                //var query = (from o in db.tbl_Map_Location
                //             select new
                //             {
                //                 o.City
                //             }).Distinct().OrderBy(x => x.City).ToList();
                var query = db.tbl_Map_Location.GroupBy(s => s.City).Select(x => new { City = x.FirstOrDefault().City }).ToList();
                foreach (var item in query.OrderBy(s => s.City))
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.City.ToString(),
                        Value = item.City.ToString()
                    });
                }
            }
            else if (str.ToUpper() == "DISTRICT")
            {
                //var query = (from o in db.tbl_Map_Location
                //             select new
                //             {
                //                 o.District
                //             }).Distinct().OrderBy(x=>x.District).ToList();
                var query = db.tbl_Map_Location.GroupBy(s => s.District).Select(x => new { District = x.FirstOrDefault().District }).ToList();
                foreach (var item in query.OrderBy(s => s.District))
                {

                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.District.ToString(),
                        Value = item.District.ToString()
                    });
                }
            }
            else if (str.ToUpper() == "VILLAGE")
            {
                //var query = (from o in db.tbl_Map_Location
                //             select new
                //             {
                //                 o.Village
                //             }).Distinct().OrderBy(x => x.Village).ToList();
                var query = db.tbl_Map_Location.GroupBy(s => s.Village).Select(x => new { Village = x.FirstOrDefault().Village }).ToList();
                foreach (var item in query.OrderBy(s => s.Village))
                {

                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Village.ToString(),
                        Value = item.Village.ToString()
                    });
                }
            }

            return generalParameterList;
        }
        #endregion

        #region List Type Office

        public static List<SelectListItem> TypeOffice(String Case)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();
            if (Case == GlobalVariable.CONST_HEAD_OFFICE)
            {
                listItems.Add(new SelectListItem { Text = "Head Office", Value = "Head Office" });
            }
            else if (Case == GlobalVariable.CONST_BRANCH)
            {
                listItems.Add(new SelectListItem { Text = "Branch Office", Value = "Branch Office" });
            }
            else
            {
                listItems.Add(new SelectListItem { Text = "Head Office", Value = "Head Office" });
                listItems.Add(new SelectListItem { Text = "Branch Office", Value = "Branch Office" });
            }

            return listItems;
        }
        #endregion


        #region GBF_MultiSelectList_EmployeeByPayroll_Period
        public static List<SelectListItem> SelectList_Employee_Payroll_Period_Generate_Bank_File(Guid? PeriodID, Guid? OrgID, int Run, string bankCode, string IdCache)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List_EmployeePayroll_Period = new List<SelectListItem>();
            var ListEmployee = db.vw_Generate_Bank_File_Details_Summary.Where(p => p.Payroll_Period_ID == PeriodID && p.Organization_ID == OrgID && p.Run == Run && p.Payment_Bank_Code == bankCode).ToList();
            if (ListEmployee != null)
            {
                UICache.LIST_VW_GENERATE_BANK_FILE_DETAILS_SUMMARY_SET(ListEmployee, IdCache + "vwGenerateBankFileDetailsSummary");
            }
            foreach (var item in ListEmployee)
            {
                string[] DataEmployee = item.Title.Split('|').ToArray();
                List_EmployeePayroll_Period.Add(new SelectListItem
                {
                    Text = DataEmployee[2] + "|" + DataEmployee[3] + "|" + DataEmployee[4], //+ "|" + DataEmployee[5],
                    Value = item.Id.ToString(),
                });
            }
            return List_EmployeePayroll_Period;
        }

        #endregion
        #region SelectList_Payroll_Period

        public static List<SelectListItem> SelectList_Payroll_Period()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPayroll_Period = new List<SelectListItem>();
            int lastYear = DateTime.Now.Year - 1;
            int CurrentYear = DateTime.Now.Year;
            int FutureYear = DateTime.Now.Year + 1;
            var ListPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == lastYear || p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == CurrentYear || p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == FutureYear).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();
            foreach (var item in ListPayrollPeriod)
            {
                ListPayroll_Period.Add(new SelectListItem
                {
                    Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + "-" + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                    Value = item.id.ToString()
                });
            }
            return ListPayroll_Period;
        }

        #endregion

        //public static List<SelectListItem> SelectList_Payroll_Period_Generate_Bank_File(Guid OrgID)
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> List = new List<SelectListItem>();
        //    CoreVariable CoreUserVariable = new CoreVariable();
        //    User_Data UserData = CoreUserVariable.CoreUserLogin();
        //    var ModelNew = from s in db.vw_Generate_Bank_File_Details.Where(r => r.Organization_ID == OrgID).Distinct()
        //                   select new
        //                   {
        //                       s.Payroll_Period,
        //                       s.Payroll_Period_ID
        //                   };
        //    foreach (var item in ModelNew.Distinct().OrderBy(s => s.Payroll_Period))
        //    {
        //        List.Add(new SelectListItem
        //        {
        //            Text = item.Payroll_Period,
        //            Value = item.Payroll_Period_ID.ToString()
        //        });
        //    }
        //    return List;
        //}

        #region LIST_FIELD_tbl_User
        public static List<SelectListItem> fieldTblUser()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "LDAP", Value = "ldap" });
            listItems.Add(new SelectListItem { Text = "Username", Value = "username" });
            listItems.Add(new SelectListItem { Text = "Email", Value = "email" });
            listItems.Add(new SelectListItem { Text = "Role", Value = "role" });
            listItems.Add(new SelectListItem { Text = "Team", Value = "team" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_User
        public static List<SelectListItem> fieldTblEmployeeLeaveMaster()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Leave Type", Value = "type" });
            listItems.Add(new SelectListItem { Text = "Remark", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Period", Value = "period" });
            listItems.Add(new SelectListItem { Text = "Entitlement", Value = "entitlement" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Bank_Information
        public static List<SelectListItem> fieldTblBankInformation()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Bank", Value = "bank" });
            listItems.Add(new SelectListItem { Text = "Account Number", Value = "account_number" });
            listItems.Add(new SelectListItem { Text = "Account Name", Value = "account_name" });
            listItems.Add(new SelectListItem { Text = "Currency Code", Value = "currency_code" });
            listItems.Add(new SelectListItem { Text = "Purpose", Value = "purpose" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_role
        public static List<SelectListItem> fieldRoleMenuFunction()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Role", Value = "role" });
            listItems.Add(new SelectListItem { Text = "Menu", Value = "menu" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_role
        public static List<SelectListItem> fieldTblEmployeePaymentInformation()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Payment Type", Value = "paymenttype" });
            listItems.Add(new SelectListItem { Text = "Payment From Bank Account", Value = "paymentbank" });
            listItems.Add(new SelectListItem { Text = "Employee Bank Account", Value = "employeebank" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Organization_Account_Group_Maintenence
        public static List<SelectListItem> fieldTblOrganizationAccountGroupMaintenence()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Account Numbber", Value = "account" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST FIELD tbl_General_Parameter
        public static List<SelectListItem> fieldTblGeneralParameter()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Type", Value = "Table_Name" });
            listItems.Add(new SelectListItem { Text = "Value", Value = "Field_Name" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "Field_Value" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region List Field tbl_Base_Location
        public static List<SelectListItem> fieldTblBaseLocation()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
           //listItems.Add(new SelectListItem { Text = "Location Name", Value = "locationName" });
            //listItems.Add(new SelectListItem { Text = "Map Type", Value = "mapType" });
            listItems.Add(new SelectListItem { Text = "Work Location", Value = "workLocation" });
            listItems.Add(new SelectListItem { Text = "Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region  LIST FIELD tbl_Organization
        public static List<SelectListItem> fieldTblOrganization()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "name" });
            listItems.Add(new SelectListItem { Text = "Organization Type", Value = "type" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region  LIST FIELD tbl_Organization
        public static List<SelectListItem> fieldTblLeaveTypes()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "leave" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST FIELD tbl_Contact_Person
        public static List<SelectListItem> contactPersonField()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "Name" });
            listItems.Add(new SelectListItem { Text = "Designation", Value = "Designation" });
            listItems.Add(new SelectListItem { Text = "Office Phone", Value = "Office_Phone" });
            listItems.Add(new SelectListItem { Text = "Personal Phone", Value = "Personal_Phone" });
            listItems.Add(new SelectListItem { Text = "Email", Value = "Email" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST FIELD tbl_BPJS_Manpower
        public static List<SelectListItem> fieldTblBPJSManpower()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "NPP Number", Value = "Npp_Number" });
            listItems.Add(new SelectListItem { Text = "Office Address", Value = "Office_Address" });
            listItems.Add(new SelectListItem { Text = "RO", Value = "Ro" });
            listItems.Add(new SelectListItem { Text = "Phone/Mobile Number", Value = "Phone" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_BPJS_Healthcare
        public static List<SelectListItem> fieldTblBPJSHealthcare()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "NPP Number", Value = "Npp_Number" });
            listItems.Add(new SelectListItem { Text = "Office Address", Value = "Office_Address" });
            listItems.Add(new SelectListItem { Text = "Office Phone Number", Value = "Office_Phone" });
            listItems.Add(new SelectListItem { Text = "RO", Value = "Ro" });
            listItems.Add(new SelectListItem { Text = "Phone/Mobile Number", Value = "Phone" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Chart_Account
        public static List<SelectListItem> fieldChartOfAccount()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Cost_Center
        public static List<SelectListItem> fieldTblCostCenter()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "Code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_List_Of_Bank
        public static List<SelectListItem> fieldTblListOfBank()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "Code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            listItems.Add(new SelectListItem { Text = "BI Code", Value = "BI_Code" });
            listItems.Add(new SelectListItem { Text = "Swift Code", Value = "Swift_Code" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Holiday_Calendar
        public static List<SelectListItem> fieldTblHolidayCalendar()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Date", Value = "Date" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "Name" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion
		
		        #region LIST_FIELD_tbl_Blog_Posts
        public static List<SelectListItem> fieldTblBlogPosts()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Title", Value = "Title" });
            listItems.Add(new SelectListItem { Text = "Category", Value = "Category_Menu" });
            listItems.Add(new SelectListItem { Text = "Create By", Value = "Created_By" });
            listItems.Add(new SelectListItem { Text = "View", Value = "Frequence" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_calculation
        public static List<SelectListItem> fieldTblCalculation()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Type", Value = "calculation_type" });
            listItems.Add(new SelectListItem { Text = "Batch", Value = "batch" });
            //listItems.Add(new SelectListItem { Text = "Tax Period up to", Value = "tax_period" });
            //listItems.Add(new SelectListItem { Text = "Payroll Period", Value = "payroll_period" });
            listItems.Add(new SelectListItem { Text = "Run", Value = "run" });
            listItems.Add(new SelectListItem { Text = "Calculation Status", Value = "calculate_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize_status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_run_closing
        public static List<SelectListItem> fieldRunClosing()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Tax Period", Value = "tax_period" });
            listItems.Add(new SelectListItem { Text = "Payroll Period", Value = "payroll_period" });
            listItems.Add(new SelectListItem { Text = "Run", Value = "run" });
            listItems.Add(new SelectListItem { Text = "Pembetulan", Value = "pembetulan" });
            //listItems.Add(new SelectListItem { Text = "Tax Period up to", Value = "tax_period" });
            //listItems.Add(new SelectListItem { Text = "Payroll Period", Value = "payroll_period" });
            listItems.Add(new SelectListItem { Text = "Tax Period Status", Value = "tax_period_status" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status_code" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize_status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tax_period_closing
        public static List<SelectListItem> fieldTaxPeriodClosing()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Tax Period", Value = "tax_period" });
            listItems.Add(new SelectListItem { Text = "Pembetulan", Value = "pembetulan" });
            listItems.Add(new SelectListItem { Text = "Tax Period Status", Value = "tax_period_status" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status_code" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize_status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_Tax_Period_Status_Closing
        public static List<SelectListItem> fieldTaxPeriodStatusClosing()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Open", Value = "open" });
            listItems.Add(new SelectListItem { Text = "Close", Value = "close" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_payroll_slip
        public static List<SelectListItem> fieldTblPayrollSlip()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Period", Value = "period" });
            listItems.Add(new SelectListItem { Text = "Generate Date", Value = "generate_date" });
            listItems.Add(new SelectListItem { Text = "Employee ID", Value = "employee_id" });
            listItems.Add(new SelectListItem { Text = "Email Address", Value = "email_address" });
            listItems.Add(new SelectListItem { Text = "Generation Number", Value = "generation_number" });
            listItems.Add(new SelectListItem { Text = "Version", Value = "version" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Non_Taxable_Income_Parameter
        public static List<SelectListItem> fieldTblNonTaxableIncomeParameter()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Code", Value = "Code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            listItems.Add(new SelectListItem { Text = "Amount", Value = "Amount" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "RecordStatus" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "AuthorizeStatus" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Tax_Rate_Parameter
        public static List<SelectListItem> fieldTblTaxRateParameter()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Type", Value = "Type" });
            listItems.Add(new SelectListItem { Text = "NPWP", Value = "NPWP" });
            listItems.Add(new SelectListItem { Text = "From", Value = "From" });
            listItems.Add(new SelectListItem { Text = "To", Value = "To" });
            listItems.Add(new SelectListItem { Text = "Percentage", Value = "Percentage" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "RecordStatus" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "AuthorizeStatus" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Cost_Parameter
        public static List<SelectListItem> fieldTblCostParameter()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Type", Value = "Type" });
            listItems.Add(new SelectListItem { Text = "Persentage", Value = "Persentage" });
            listItems.Add(new SelectListItem { Text = "Max Cost per Month", Value = "MaxCostperMonth" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Organization_Team
        public static List<SelectListItem> fieldTblOrganizationTeam()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Team", Value = "team" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_HeadOffice_Branch
        public static List<SelectListItem> fieldTblHeadOfficeBranch()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "all", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "name" });
            listItems.Add(new SelectListItem { Text = "Type", Value = "type" });
            listItems.Add(new SelectListItem { Text = "Province", Value = "province" });
            listItems.Add(new SelectListItem { Text = "Country", Value = "country" });
            listItems.Add(new SelectListItem { Text = "Head Office / Branch TAX ID", Value = "tax" });
            listItems.Add(new SelectListItem { Text = "Signer", Value = "signer" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Trx_Bank_Transfer
        public static List<SelectListItem> fieldTblTrxBankTransfer()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Period", Value = "Payroll_Period_ID" });
            listItems.Add(new SelectListItem { Text = "Generation Date", Value = "Generation_Date" });
            listItems.Add(new SelectListItem { Text = "Generation Number", Value = "Generation_Number" });
            listItems.Add(new SelectListItem { Text = "Transfer Date", Value = "Transfer_Date" });
            listItems.Add(new SelectListItem { Text = "Reference Number", Value = "Reference_Number" });
            //listItems.Add(new SelectListItem { Text = "Remarks", Value = "authorize_status" });
            return listItems;
        }
        #endregion

        #region ListMapBranchLocation
        public static List<SelectListItem> ListMapBranchLocation()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListModel = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var Data = (from HB in db.tbl_HeadOffice_Branch
                       join HBL in db.tbl_HeadOffice_Branch_Location on HB.id equals HBL.HeadOffice_Branch_ID
                       where HB.Organization_ID == UserData.OrganizationSelected_Id && HB.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && HB.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE
                       select new { id = HBL.id, Organization_ID = HB.Organization_ID, HO_Branch_TAX_ID = HB.HO_Branch_TAX_ID, Location = HBL.Location, HeadOffice_Branch_ID = HBL.HeadOffice_Branch_ID }).ToList(); // .ToList();
            // ListModel = UICommonFunction.GroupSelectList(Data);
            foreach (var item in Data)
            {
                ListModel.Add(new SelectListItem
                {
                    Text = item.Location,
                    Value = item.id.ToString()
                });
            }
            return ListModel;
        }
        #endregion

        public static List<SelectListItem> List_Template_Upload(string control, string ParamCode, out int count)
        {
            List<SelectListItem> List_Template = new List<SelectListItem>();
            count = 0;
            try
            {
                string ParamsCode = ParamCode + control;
                List<tbl_SysParam> TemplateParams = UICommonFunction.GetSysParam(ParamsCode);
                if (TemplateParams.Count() > 0)
                {
                    foreach (var item in TemplateParams)
                    {
                        List_Template.Add(new SelectListItem
                        {
                            Text = item.Description,
                            Value = item.Value
                        });
                        count += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static List<SelectListItem> List_Template_Upload");
            }

            return List_Template;
        }


        public static List<SelectListItem> List_Update_Category_Type(string UploadCategoryType, string ParamCode, out int count)
        {
            List<SelectListItem> List_Category_Type = new List<SelectListItem>();
            count = 0;
            try
            {
                var sysparam = GeneralCore.SystemParameterQuery().Where(p => p.Param_Code == ParamCode).FirstOrDefault();
                if (sysparam != null)
                {
                    if (sysparam.Value.Contains(";"))
                    {
                        string[] strType = sysparam.Value.Split(new char[] { ';' });
                        UploadCategoryType = UploadCategoryType.ToLower();
                        strType = Array.FindAll(strType, s => s.Contains(UploadCategoryType));
                        if (strType.Length > 0)
                        {
                            strType = strType[0].Split(new char[] { ':' });
                            strType = strType[1].Split(new char[] { '|' });
                            foreach (var item in strType)
                            {
                                count++;
                                List_Category_Type.Add(new SelectListItem
                                {
                                    Text = item,
                                    Value = item
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "public static List<SelectListItem> List_Update_Category_Type()");
            }

            return List_Category_Type;
        }
        #region LIST_FIELD_tbl_Organization_Structure
        public static List<SelectListItem> fieldTblORGANIZATIONStructure()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region Structure
        public static List<SelectListItem> Structure()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "All" });
            listItems.Add(new SelectListItem { Text = "Departments", Value = "Departments" });
            listItems.Add(new SelectListItem { Text = "Grades", Value = "Grades" });
            listItems.Add(new SelectListItem { Text = "Divisions", Value = "Divisions" });
            listItems.Add(new SelectListItem { Text = "Positions", Value = "Positions" });
            return listItems;
        }
        #endregion

        #region Structure
        public static List<SelectListItem> StructureValuesOrganization()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> listItems = new List<SelectListItem>();
            var model = GeneralCore.OrganizationStructurQuery().OrderBy(p=>p.Struktur);

            listItems.Add(new SelectListItem { Text = "All", Value = "All|All|All" });
            
            foreach(var item in model)
            {
                listItems.Add(new SelectListItem {
                    Text = item.Struktur +" "+item.Description,
                    Value = item.Struktur + "|"+ item.Code + "|" + item.Description });
            }

            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Organization_Payroll_Component
        public static List<SelectListItem> fieldTblOrganizationPayrollComponent()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Taxable Type", Value = "taxtype" });
            listItems.Add(new SelectListItem { Text = "Tax Deduction", Value = "taxdeduction" });
            listItems.Add(new SelectListItem { Text = "Frequency", Value = "frequency" });
            listItems.Add(new SelectListItem { Text = "Amount Type", Value = "amounttype" });
            listItems.Add(new SelectListItem { Text = "Calculation Basic", Value = "calculationbasic" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorizestatus" });
            return listItems;
        }
        #endregion
        #region LIST_FIELD_tbl_Payroll_Variable
        public static List<SelectListItem> fieldTblPayrollVariable()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID Number", Value = "employee" });
            listItems.Add(new SelectListItem { Text = "Employee Name", Value = "name" });
            listItems.Add(new SelectListItem { Text = "Payroll Period", Value = "period" });
            //listItems.Add(new SelectListItem { Text = "Run", Value = "run" });
            listItems.Add(new SelectListItem { Text = "Upload No", Value = "no" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        public static List<SelectListItem> fieldTblAdditionalPayroll()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID Number", Value = "employee" });
            listItems.Add(new SelectListItem { Text = "Employee Name", Value = "name" });
            listItems.Add(new SelectListItem { Text = "Tax Period", Value = "tax" });
            listItems.Add(new SelectListItem { Text = "Payroll Period", Value = "period" });
            //listItems.Add(new SelectListItem { Text = "Run", Value = "run" });
            listItems.Add(new SelectListItem { Text = "Upload No", Value = "no" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            listItems.Add(new SelectListItem { Text = "Category", Value = "category" });
            return listItems;
        }
        #endregion
        public static List<SelectListItem> fieldTaxDeduction()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Null", Value = "" });
            listItems.Add(new SelectListItem { Text = "YES", Value = "Yes" });
            listItems.Add(new SelectListItem { Text = "NO", Value = "No" });
            return listItems;
        }
        #region LIST_FIELD_tbl_Employee_Payroll_Component
        public static List<SelectListItem> fieldTblEmployeePayrollComponent()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Taxable Type", Value = "taxtype" });
            listItems.Add(new SelectListItem { Text = "Tax Deduction", Value = "taxdeduction" });
            listItems.Add(new SelectListItem { Text = "Frequency", Value = "frequency" });
            listItems.Add(new SelectListItem { Text = "Amount Type", Value = "amounttype" });
            listItems.Add(new SelectListItem { Text = "Calculation Basic", Value = "calculationbasic" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "auth" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_vw_Payroll_Report_Setting
        public static List<SelectListItem> fieldViewPayrollReportSetting()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Payroll Report Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "File Format", Value = "format" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorizeStatus" });
            return listItems;
        }
        #endregion
        #region


        #region LIST_FIELD_tbl_Holiday_Organization
        public static List<SelectListItem> fieldTblHolidayOrganization()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Location", Value = "location" });
            listItems.Add(new SelectListItem { Text = "Date", Value = "date" });
            listItems.Add(new SelectListItem { Text = "Name", Value = "name" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Exchange_Rate
        public static List<SelectListItem> fieldTblExchanngeRate()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Effective Date", Value = "effective_Date" });
            listItems.Add(new SelectListItem { Text = "Curency From", Value = "curency_from" });
            listItems.Add(new SelectListItem { Text = "Curency To", Value = "curency_to" });
            listItems.Add(new SelectListItem { Text = "Rate", Value = "rate" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Exchange_Rate
        public static List<SelectListItem> fieldTblOrganizationGroup()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Group Code", Value = "group_code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Employee
        public static List<SelectListItem> fieldTblEmployee()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID Number", Value = "employee_no" });
            listItems.Add(new SelectListItem { Text = "Full Name", Value = "full_name" });
            listItems.Add(new SelectListItem { Text = "Date of Birth", Value = "date_of_birth" });
            listItems.Add(new SelectListItem { Text = "Gender", Value = "gender" });
            listItems.Add(new SelectListItem { Text = "Nationality", Value = "nationality" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "record_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Payroll_Schedule
        public static List<SelectListItem> fieldTblPayrollSchedule()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Payroll Advice Submission", Value = "payroll_advice_submission" });
            listItems.Add(new SelectListItem { Text = "Payroll Report Submission", Value = "payroll_report_submission" });
            listItems.Add(new SelectListItem { Text = "Payroll Approval", Value = "payroll_approval" });
            listItems.Add(new SelectListItem { Text = "Pay Day", Value = "pay_day" });

            //listItems.Add(new SelectListItem { Text = "Jurnal", Value = "Jurnal" });
            //listItems.Add(new SelectListItem { Text = "Payslip", Value = "Payslip" });
            //listItems.Add(new SelectListItem { Text = "SPT Report", Value = "SPT_Report" });
            //listItems.Add(new SelectListItem { Text = "Tax Report", Value = "Tax_Report" });
            //listItems.Add(new SelectListItem { Text = "Holiday Treatment", Value = "Holiday_Treatment" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Organization_Working_Time
        public static List<SelectListItem> fieldTblOrganizationWorkingTime()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "code" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "description" });
            //listItems.Add(new SelectListItem { Text = "Start Date", Value = "startdate" });
            //listItems.Add(new SelectListItem { Text = "End Date", Value = "enddate" });
            listItems.Add(new SelectListItem { Text = "Working Day", Value = "workingday" });
            listItems.Add(new SelectListItem { Text = "Base Working Day", Value = "baseworkingday" });
            listItems.Add(new SelectListItem { Text = "Base Fix (in Day)", Value = "basefixinday" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorized" });
            return listItems;
        }


        #endregion

        #region LIST_FIELD_tbl_Employee_Address
        public static List<SelectListItem> fieldTblEmployeeAddress()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Legal Address", Value = "legal_address" });
            listItems.Add(new SelectListItem { Text = "Permanent Address", Value = "permanent_address" });
            listItems.Add(new SelectListItem { Text = "Mobile Phone Number", Value = "mobile_phone_number" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "record_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Tax_Period
        public static List<SelectListItem> fieldTblTaxPeriod()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Tax Year", Value = "Tax_Year" });
            listItems.Add(new SelectListItem { Text = "Tax Period From", Value = "Tax_From" });
            listItems.Add(new SelectListItem { Text = "Tax Period To", Value = "Tax_To" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Tax
        public static List<SelectListItem> fieldTblTax()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Tax ID", Value = "tax_no" });
            listItems.Add(new SelectListItem { Text = "Effective Date", Value = "effective_date" });
            listItems.Add(new SelectListItem { Text = "Revoke Date", Value = "revoke_date" });
            listItems.Add(new SelectListItem { Text = "Status", Value = "tax_status" });
            listItems.Add(new SelectListItem { Text = "Status Effective Year", Value = "status_effective_year" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "record_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Payslip
        public static List<SelectListItem> fieldTblPayslip()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "UserName", Value = "username" });
            listItems.Add(new SelectListItem { Text = "Password", Value = "password" });
            listItems.Add(new SelectListItem { Text = "Payslip Distribution", Value = "payslip_distribution" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "record_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region
        public static List<SelectListItem> fieldCategoryAdditional()
        {
            
         List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = CoreVariable.CONST_EMPLOYEMENT_STATUS_PERMANENT, Value = CoreVariable.CONST_EMPLOYEMENT_STATUS_PERMANENT });
            listItems.Add(new SelectListItem { Text = CoreVariable.CONST_EMPLOYEMENT_STATUS_NONPERMANENT, Value = CoreVariable.CONST_EMPLOYEMENT_STATUS_NONPERMANENT });
            return listItems;
        }
        #endregion

        #region -- LIST TABLE --


        #region list tbl_HeadOffice_Branch
        public static List<SelectListItem> tblHOBranch()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListModel = new List<SelectListItem>();
            foreach (var item in db.tbl_HeadOffice_Branch)
            {
                ListModel.Add(new SelectListItem
                {
                    Text = item.Code.ToString(),
                    Value = item.id.ToString()
                });
            }
            return ListModel;
        }

        public static List<SelectListItem> tblHOBranchLocation(Guid Organization_ID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListModel = new List<SelectListItem>();
            //get branch location
            List<string> Data = db.tbl_HeadOffice_Branch_Location.Where(p => p.tbl_HeadOffice_Branch.Organization_ID == Organization_ID).Select(p => p.Location).Distinct().ToList();
            //ListModel = UICommonFunction.GroupSelectList(Data);
            foreach (var item in Data)
            {
                ListModel.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return ListModel;
        }

        //public static List<SelectListItem> HOBranchLocation(Guid Organization_ID)
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> ListModel = new List<SelectListItem>();

        //    List<string> Data = GeneralCore.HeadOfficeBranchQuery().Where(p => p.Organization_ID == Organization_ID).Select(p=>p.Location).ToList();
        //   // ListModel = UICommonFunction.GroupSelectList(Data);
        //    foreach (var item in Data)
        //    {
        //        ListModel.Add(new SelectListItem
        //        {
        //            Text = item,
        //            Value = item
        //        });
        //    }
        //    return ListModel;
        //}

        public static List<SelectListItem> HOBranchLocation(Guid Organization_ID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListModel = new List<SelectListItem>();
            var Data = GeneralCore.LocationHeadOfficeBranchQuery().Where(p => (p.tbl_HeadOffice_Branch.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_HeadOffice_Branch.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).ToList();
            // ListModel = UICommonFunction.GroupSelectList(Data);
            foreach (var item in Data)
            {
                ListModel.Add(new SelectListItem
                {
                    Text = item.Location,
                    Value = item.Location
                });
            }
            return ListModel;
        }



        #endregion

        public static List<SelectListItem> FilterSelectedOrganizationTeam()
        {

            List<SelectListItem> ListRole = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.OrganizationTeamQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE);
            foreach (var item in Data)
            {
                ListRole.Add(new SelectListItem
                {
                    Text = item.Team_Code,
                    Value = item.id.ToString()
                });
            }
            return ListRole;
        }

        public static List<SelectListItem> FilterOrganizationTeamByUserID(Guid OrganizationID)
        {
            List<SelectListItem> ListOrganization = new List<SelectListItem>();
            //var Data = APP_CORE.GetData.GeneralCore.OrganizationByUserTeamQuery();
            var Data = APP_CORE.GetData.GeneralCore.OrganizationQuery().Where(p => p.id == OrganizationID);
            foreach (var item in Data)
            {
                ListOrganization.Add(new SelectListItem
                {
                    Text = item.Organization_Name,
                    Value = item.id.ToString()
                });
            }
            return ListOrganization;
        }



        public static List<SelectListItem> FilterSelectedRole()
        {
            List<SelectListItem> ListRole = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.RoleQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE);
            foreach (var item in Data)
            {
                ListRole.Add(new SelectListItem
                {
                    Text = item.Role_Code,
                    Value = item.id.ToString()
                });
            }
            return ListRole;
        }

        public static List<SelectListItem> FilterAttendanceStatus()
        {
            List<SelectListItem> ListStatus = new List<SelectListItem>();
            var Data = new List<string>() { "Approved", "Pending", "Rejected"};
            foreach (var item in Data)
            {
                ListStatus.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return ListStatus;
        }

        public static List<SelectListItem> FilterAttendanceEmployee(Guid OrganizationID, Guid EmployeeID)
        {
            List<SelectListItem> ListEmployee = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.ApprovalHierarchyQuery(OrganizationID).Where(p=>p.Supervisor_ID == EmployeeID);
            foreach (var item in Data)
            {
                ListEmployee.Add(new SelectListItem
                {
                    Text = item.Employee_ID_Number+" - "+item.Full_Name,
                    Value = item.Employee_ID_Number+" - "+item.Full_Name
                });
            }
            return ListEmployee;
        }

        public static List<SelectListItem> DataDefaultLeave()//LEAVE_TYPE
        {
            List<SelectListItem> ListDefLeave = new List<SelectListItem>();
            var Data = GeneralCore.GeneralParameterQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE);
            foreach (var item in Data.Where(p => p.Table_Name == "LEAVE_TYPE"))
            {
                ListDefLeave.Add(new SelectListItem
                {
                    Text = item.Field_Name,
                    Value = item.Field_Value
                });
            }
            return ListDefLeave;
        }

        public static List<SelectListItem> DataMyLeave()//LEAVE_TYPE
        {
            List<SelectListItem> ListDefLeave = new List<SelectListItem>();
            var Data = GeneralCore.GeneralParameterQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE);
            foreach (var item in Data.Where(p => p.Table_Name == "LEAVE_TYPE"))
            {
                ListDefLeave.Add(new SelectListItem
                {
                    Text = item.Field_Name,
                    Value = item.Field_Value
                });
            }
            ListDefLeave.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL"
            });
            return ListDefLeave;
        }
		
		public static List<SelectListItem> FilterEmployeeApproveLeave(Guid OrganizationID, Guid EmployeeID)
        {
            List<SelectListItem> ListEmployee = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.ApprovalHierarchyQuery(OrganizationID).Where(p => p.Supervisor_ID == EmployeeID);
            foreach (var item in Data)
            {
                ListEmployee.Add(new SelectListItem
                {
                    Text = item.Employee_ID_Number + " - " + item.Full_Name,
                    Value = item.Employee_ID.ToString()
                });
            }
            ListEmployee.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL"
            });
            return ListEmployee;
        }

        public static List<SelectListItem> Tbl_General_Parameter_tableName()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            var query = db.tbl_General_Parameter.GroupBy(s => s.Table_Name).Select(x => new { Table_Name = x.FirstOrDefault().Table_Name }).ToList();
            foreach (var item in query)
            {
                if (item.Table_Name != null)
                {
                    generalParameterList.Add(new SelectListItem
                    {

                        Text = item.Table_Name.ToString(),
                        Value = item.Table_Name.ToString()
                    });
                }
            }
            return generalParameterList;
        }

        public static List<SelectListItem> Tbl_General_Parameter(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        public static List<SelectListItem> getListAppointmentLocation()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in db.vw_Payroll_Period.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ToList())
            {
                List.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Period_Start_Date.ToString("dd/MM/yyyy")) + " - " + Convert.ToString(item.Period_End_Date.ToString("dd/MM/yyyy")),
                    Value = item.Payroll_Period_Id.ToString()
                });
            }
            return List;
        }

        public static List<SelectListItem> UserTeamOrganizationGroup(Guid UserID)
        {
            List<SelectListItem> orgList = new List<SelectListItem>();
            foreach (var item in GeneralCore.OrganizationByUserQuery(UserID).ToList().Where(p => p.Is_Authorized == true).OrderBy(o=>o.Organization_Name))  //OrganizationByUserQuery())
            {
                orgList.Add(new SelectListItem
                {
                    Text = item.Organization_Name,
                    Value = item.id.ToString()
                });
            }
            return orgList;
        }

        public static List<SelectListItem> Tbl_Generate_Bank_BankInformation(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }


        public static List<SelectListItem> Tbl_General_Parameter_Concate(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value + " - " + item.Field_Name,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }

        public static List<SelectListItem> Tbl_General_Parameter_CalculationBasic()
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == "CALCULATION_BASIC" && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value + " - " + item.Field_Name,
                    Value = item.Field_Value
                });
            }
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == "VARIABLE_INPUT_PAY" && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value + " - " + item.Field_Name,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }

        public static List<SelectListItem> Tbl_General_Parameter_PRORATEBASE(string tblName)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var baseWorking = db.vw_Employee_Appointment_WorkingTime.Where(s => s.Employee_Id == UserData.EmployeeSelected.id).FirstOrDefault();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                if (baseWorking.Base_Working_Day.ToLower() == "actual")
                {
                    if (item.Field_Seq == 1 || item.Field_Seq == 2)
                    {
                        generalParameterList.Add(new SelectListItem
                        {
                            Text = item.Field_Value + " - " + item.Field_Name,
                            Value = item.Field_Value
                        });
                    }
                }
                else
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Field_Value + " - " + item.Field_Name,
                        Value = item.Field_Value
                    });
                }
            }
            return generalParameterList;
        }
        public static List<SelectListItem> Tbl_General_Parameter_Desc(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Description,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        public static List<SelectListItem> Tbl_General_Parameter_Name(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Name,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }

        public static List<SelectListItem> Tbl_BPJS_TK_NPP()
        {
            List<SelectListItem> BPJSTKList = new List<SelectListItem>();
            foreach (var item in GeneralCore.BPJSManpowerQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE))
            {

                BPJSTKList.Add(new SelectListItem
                {
                    Text = item.Npp_Number.ToString(),
                    Value = item.Npp_Number.ToString()
                });
            }
            return BPJSTKList;
        }

        public static List<SelectListItem> Tbl_BPJS_HC_NPP()
        {
            List<SelectListItem> BPJSHCList = new List<SelectListItem>();
            foreach (var item in GeneralCore.BPJSHealthcareQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE))
            {
                BPJSHCList.Add(new SelectListItem
                {
                    Text = item.Npp_Number.ToString(),
                    Value = item.Npp_Number.ToString()
                });
            }
            return BPJSHCList;
        }



        public static List<SelectListItem> SelectedTblBankSetting(User_Data DataFilter)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.ListBankQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE);
            foreach (var item in Data)
            {
                List.Add(new SelectListItem
                {
                    Text = item.Bank_Code + " - " + item.Description,
                    Value = item.Bank_Code
                });
            }
            return List;
        }

        public static List<SelectListItem> SelectList_PayslipType()
        { 
            List<SelectListItem> ListSelectList = new List<SelectListItem>();
            ListSelectList.Add(new SelectListItem
            {
                Text = "Permanent",
                Value = "Permanent"
            });
            ListSelectList.Add(new SelectListItem
            {
                Text = "Non Permanent",
                Value = "NonPermanent"
            });
            return ListSelectList;
        }


        public static List<SelectListItem> Tbl_Bank_Information(Guid? Organization_ID)
        {
            List<SelectListItem> ddl = new List<SelectListItem>();
            foreach (var item in GeneralCore.BankInformationQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Organization_ID == Organization_ID && s.Status_Code == 1).OrderBy(s => s.Bank))
            {
                ddl.Add(new SelectListItem
                {
                    Text = item.Bank.ToString() + "-" + item.Account_Number.ToString(),
                    Value = item.id.ToString()
                });
            }
            return ddl;
        }

        public static List<SelectListItem> Tbl_List_Bank(Guid? Organization_ID)
        {
            List<SelectListItem> ddl = new List<SelectListItem>();
            foreach (var item in GeneralCore.ListBankQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.BI_Bank_Code))
            {
                ddl.Add(new SelectListItem
                {
                    Text = item.Bank_Code.ToString() + "-" + item.BI_Bank_Code.ToString(),
                    Value = item.id.ToString()
                });
            }
            return ddl;
        }

        public static List<SelectListItem> Tbl_List_Bank_Branch(Guid? Organization_ID)
        {
            List<SelectListItem> ddl = new List<SelectListItem>();
            var LstBankBranch = GeneralCore.ListBankBranchQuery().Where(p => (p.tbl_List_Of_Bank.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_List_Of_Bank.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderBy(s => s.Branch_Code).ToList();
            foreach (var item in LstBankBranch)
            {
                ddl.Add(new SelectListItem
                {
                    Text = item.tbl_List_Of_Bank.Bank_Code + "-" + item.Branch_Code.ToString(),
                    Value = item.id.ToString()
                });
            }
            var ListBank = GeneralCore.ListBankQuery().Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).ToList();
            foreach (var Bank in ListBank)
            {
                ddl.Add(new SelectListItem
                {
                    Text = Bank.Bank_Code,
                    Value = Bank.id.ToString()
                });
            }
            ddl.OrderBy(p => p.Text);
            return ddl;
        }

        public static List<SelectListItem> Tbl_General_ParameterHidden(string tblName)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in db.tbl_General_Parameter.Where(s => s.Table_Name == tblName && s.Status_Code == 0).OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Description.ToString(),
                    Value = item.Field_Value.ToString()
                });
            }
            return generalParameterList;
        }

        #region ReportRun
        public static List<SelectListItem> Report_Setting_Layout()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var ModelNew = from s in db.tbl_Report_Layout.Distinct()
                           select new
                           {
                               s.id,
                               s.Description
                           };

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "Select",
            //    Value = ""
            //});
            foreach (var item in ModelNew.Distinct().OrderBy(s => s.Description))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Description,
                    Value = item.id.ToString().ToUpper()
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportPayrollPeriod
        public static List<SelectListItem> Report_Payroll_Period()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            int lastYear = DateTime.Now.Year - 1;
            //var ModelNew = from s in db.vw_Calculation_Transaction.Where(r => r.Tax_Year >= lastYear && r.Organization_ID == UserData.OrganizationSelected.id && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct()

            var ModelNew = from s in db.vw_Payroll_Calculation_Period.Where(r => r.Organization_ID == UserData.OrganizationSelected.id).OrderBy(p => p.Period_Start_Date)
                           select new
                           {
                               s.Period_Start_Date,
                               //s.Period_End_Date,
                               s.Payroll_Period_ID,
                               s.Period,
                               s.Tax_Period,
                           };

            generalParameterList.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });

            var a = ModelNew.Distinct().ToList();
            foreach (var item in a.OrderBy(p => p.Period_Start_Date))
            {
                generalParameterList.Add(new SelectListItem
                {
                    //Text = Convert.ToString(item.Period_Start_Date.ToString("dd/MM/yyyy")) + " - " + Convert.ToString(item.Period_End_Date.ToString("dd/MM/yyyy")),
                    Text = item.Period.ToString(),
                    //Value = item.Payroll_Period_ID.ToString()//Payroll_Period_Id.ToString()
                    Value = item.Tax_Period.ToString()
                });
            }
            return generalParameterList;
        }
        #endregion

        //#region ReportPayrollPeriodFrom
        //public static List<SelectListItem> Report_Payroll_Period_From()
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> generalParameterList = new List<SelectListItem>();
        //    CoreVariable CoreUserVariable = new CoreVariable();
        //    User_Data UserData = CoreUserVariable.CoreUserLogin();

        //    generalParameterList.Add(new SelectListItem
        //    {
        //        Text = "Select",
        //        Value = ""
        //    });
        //    foreach (var item in db.vw_Payroll_Period.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ToList())
        //    {
        //        generalParameterList.Add(new SelectListItem
        //        {
        //            Text = Convert.ToString(item.Period_Start_Date.ToString("dd/MM/yyyy")) + " - " + Convert.ToString(item.Period_End_Date.ToString("dd/MM/yyyy")),
        //            Value = item.Payroll_Period_Id.ToString()
        //        });
        //    }
        //    return generalParameterList;
        //}
        //#endregion

        //#region ReportPayrollPeriodTo
        //public static List<SelectListItem> Report_Payroll_Period_To()
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> generalParameterList = new List<SelectListItem>();
        //    CoreVariable CoreUserVariable = new CoreVariable();
        //    User_Data UserData = CoreUserVariable.CoreUserLogin();

        //    generalParameterList.Add(new SelectListItem
        //    {
        //        Text = "Select",
        //        Value = ""
        //    });
        //    foreach (var item in db.vw_Payroll_Period.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ToList())
        //    {
        //        generalParameterList.Add(new SelectListItem
        //        {
        //            Text = Convert.ToString(item.Period_Start_Date.ToString("dd/MM/yyyy")) + " - " + Convert.ToString(item.Period_End_Date.ToString("dd/MM/yyyy")),
        //            Value = item.Payroll_Period_Id.ToString()
        //        });
        //    }
        //    return generalParameterList;
        //}
        //#endregion

        #region ReportRun
        public static List<SelectListItem> Report_Run(List<Guid> ListPayrollPeriodID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<Guid?> ListPayrollID = new List<Guid?>();

            foreach(var item in ListPayrollPeriodID)
            {
                ListPayrollID.Add(item);
            }

            var ModelNew = from s in db.vw_Payroll_Calculation_Period.Where(p => ListPayrollID.Contains(p.Payroll_Period_ID) && p.Organization_ID == UserData.OrganizationSelected_Id).Distinct()
                           select new
                           {
                               s.Run
                           };

            generalParameterList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL"
            });
            foreach (var item in ModelNew.Distinct().OrderBy(s => s.Run))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Run),// item.Run.ToString(),
                    Value = Convert.ToString(item.Run)
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportFormat
        public static List<SelectListItem> Report_Format()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            generalParameterList.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == "REPORT_FORMAT").OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportFormat_PayrollReportSetting
        public static List<SelectListItem> Report_Format_Payroll_Report_Code()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            generalParameterList.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in db.tbl_Report_Setting.Where(s => s.Organization_id == UserData.OrganizationSelected.id && s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Payroll_Report_Code).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Payroll_Report_Code + " - " + item.Description,
                    Value = item.Id.ToString()
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportGenerationEmployeeStatus
        public static List<SelectListItem> Report_Generation_Employee_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYEESTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportGenerationEmployementStatus
        public static List<SelectListItem> Report_Generation_Employement_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYMENTSTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportGenerationECostCenter
        public static List<SelectListItem> Report_Generation_Cost_Center()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> CostCenterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var query = GeneralCore.CostCenterSelectListQuery().GroupBy(s => s.Cost_Center).Select(x => new { Cost_Center = x.FirstOrDefault().Cost_Center }).ToList();
            foreach (var item in query.OrderBy(o => o.Cost_Center))
            {
                CostCenterList.Add(new SelectListItem
                {
                    Text = item.Cost_Center,
                    Value = item.Cost_Center
                });
            }
            return CostCenterList;
        }
        #endregion

        #region ReportGenerationReportTypeAttendence

        public static List<SelectListItem> ReportTypeAttendence()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Report Attendence", Value = "1" });
            listItems.Add(new SelectListItem { Text = "Recapitulation Report Attendance", Value = "2" });
            return listItems;
        }

        #endregion

        #region Dropdown_List_Search_Detail_Report_Attendance
        public static List<SelectListItem> fielddrpdwnsearchdetailreportattendance()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID", Value = "employee_id" });
            listItems.Add(new SelectListItem { Text = "Employee Name", Value = "employee_name" });
            return listItems;
        }
        #endregion

        #region ReportGenerationFileType


        public static List<SelectListItem> FileTypeAttendence  ()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = ".pdf", Value = "1" });
            listItems.Add(new SelectListItem { Text = ".xlsx", Value = "2" });
            return listItems;
        }

        #endregion

        #region ReportGenerationReport_Leave_Type

        public static List<SelectListItem> Report_Generation_Report_Leave_Type()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "LEAVE_TYPE" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }

        #endregion

        #region ReportGenerationReport_Leave_Status

        public static List<SelectListItem> GenerationReport_Status()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            listItems.Add(new SelectListItem { Text = "Cancel", Value = "Cancel" });
            listItems.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            listItems.Add(new SelectListItem { Text = "Reject", Value = "Reject" });
            return listItems;
        }

        #endregion

        #region ReportEmployementStatus
        public static List<SelectListItem> Report_Employement_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYMENTSTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportEmployeeStatus
        public static List<SelectListItem> Report_Employee_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYEESTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportDeputation
        public static List<SelectListItem> Report_Deputation()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "DEPUTATION" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportPosition
        public static List<SelectListItem> Report_Position()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});
            foreach (var item in GeneralCore.PositionSelectListQuery().Where(r => r.Struktur == "Positions" && r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Description,
                    Value = item.Code
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportDivision
        public static List<SelectListItem> Report_Division()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});
            foreach (var item in GeneralCore.DivisionSelectListQuery().Where(r => r.Struktur == "Divisions" && r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Description,
                    Value = item.Code
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportGrade
        public static List<SelectListItem> Report_Grade()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});
            foreach (var item in GeneralCore.GradeSelectListQuery().Where(r => r.Struktur == "Grades" && r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Description,
                    Value = item.Code
                });
            }
            return generalParameterList;
        }
        #endregion

        #region Report Claim Status

        public static List<SelectListItem> ReportClaimStatus()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Approved", Value = "APPROVED" });
            listItems.Add(new SelectListItem { Text = "Canceled", Value = "CANCELED" });
            listItems.Add(new SelectListItem { Text = "Rejected", Value = "REJECTED" });
            listItems.Add(new SelectListItem { Text = "Pending", Value = "PENDING" });
            return listItems;
        }

        #endregion

        #region ReportHeadOfficeBranch
        public static List<SelectListItem> Report_HeadOfficeBranch()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = " "
            //});
            foreach (var item in db.tbl_HeadOffice_Branch.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Name,
                    Value = item.id.ToString() //item.Code
                });
            }
            return generalParameterList;
        }
        #endregion

        #region ReportHeadOfficeBranchLocation
        public static List<SelectListItem> Report_HeadOfficeBranchLocation(string[] id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            if (id != null && id[0] != " " && id[0] != "")
            {
                //generalParameterList.Add(new SelectListItem
                //{
                //    Text = "ALL",
                //    Value = " "
                //});
                List<Guid> HeadOffice_Branch_ID = new List<Guid>();
                if (id != null)
                {
                    foreach (var item in id)
                    {
                        HeadOffice_Branch_ID.Add(Guid.Parse(item));
                    }
                }
                foreach (var item in db.tbl_HeadOffice_Branch_Location.Where(r => HeadOffice_Branch_ID.Contains(r.HeadOffice_Branch_ID)))//r.HeadOffice_Branch_ID == id).ToList())//UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Location,
                        Value = item.Location//item.id.ToString()
                    });
                }
            }
            else if (id[0] == "")
            {
                var ModelNew = from s in db.tbl_HeadOffice_Branch.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code)
                               select new { s.id };
                //generalParameterList.Add(new SelectListItem
                //{
                //    Text = "ALL",
                //    Value = " "
                //});
                List<Guid> HeadOffice_Branch_ID = new List<Guid>();
                if (ModelNew != null)
                {
                    foreach (var item in ModelNew)
                    {
                        HeadOffice_Branch_ID.Add(item.id);
                    }
                }
                foreach (var item in db.tbl_HeadOffice_Branch_Location.Where(r => HeadOffice_Branch_ID.Contains(r.HeadOffice_Branch_ID)))//(r.HeadOffice_Branch_ID)))//r.HeadOffice_Branch_ID == id).ToList())//UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.Location,
                        Value = item.Location//item.id.ToString()
                    });
                }
            }
            return generalParameterList;
        }
        #endregion

        #region Report_Group
        public static List<SelectListItem> Report_Group()
        {
            var str = GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == "REPORT_GENERATION_GROUP" && s.Status_Code == 1).OrderBy(s => s.Field_Seq).ToList();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();

            generalParameterList.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in str)
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Name.ToString(),
                    Value = item.Field_Value.ToString()
                });
            }
            return generalParameterList;
        }
        #endregion

        public static List<SelectListItem> Report_HeadOfficeBranchTaxID()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            generalParameterList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = " "
            });
            foreach (var item in db.tbl_HeadOffice_Branch.Where(r => r.Organization_ID == UserData.OrganizationSelected.id && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Code).ToList())
            {
                if (generalParameterList.Where(s => s.Value == item.HO_Branch_TAX_ID.ToString()).Count() == 0)
                {
                    generalParameterList.Add(new SelectListItem
                    {
                        Text = item.HO_Branch_TAX_ID,
                        Value = item.HO_Branch_TAX_ID.ToString()
                    });
                }
            }
            return generalParameterList;
        }
        public static List<SelectListItem> Report_Tax_Period()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListTaxPeriod = new List<SelectListItem>();
            //CR00 Report Employee ddl Ali
            //var ListCalculation = GeneralCore.PayrollCalculation().Where(p => p.Calculate_Status != GlobalVariable.CONST_CALCULATION_STATUS_IN_PROGRESS && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.tbl_Payroll_Period_Detail.Period_Start_Date).ToList();
            var PayrollPeriodDetail = GeneralCore.PayrollPeriodDetailQuery().ToList();
            var PayrollPeriodDetail_id = PayrollPeriodDetail.Select(p=>p.id).ToList();
            var Report_Employee_Period = db.tbl_Report_Employee.Where(p=> PayrollPeriodDetail_id.Contains(p.Tax_Period_Month_ID.Value)).Select(p=>p.Tax_Period_Month_ID).Distinct().ToList();

            PayrollPeriodDetail = PayrollPeriodDetail.Where(p => Report_Employee_Period.Contains(p.id)).OrderBy(p => p.Tax_Period.Year).ThenBy(p=>p.Tax_Period).ToList();
             
            foreach (var item in PayrollPeriodDetail)
            {
                if (ListTaxPeriod.Where(s => s.Value == item.Payroll_Period_ID.ToString()).Count() == 0)
                {
                    ListTaxPeriod.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMM yyyy}", item.Tax_Period),
                        Value = item.id.ToString()
                    });
                }

            }
            return ListTaxPeriod;
        }

        public static List<SelectListItem> OrganizationBank(Guid orgID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> employeBankList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in GeneralCore.BankInformationQuery().Where(s => s.Organization_ID == orgID).ToList())
            {
                employeBankList.Add(new SelectListItem
                {
                    Text = item.Bank,
                    Value = item.id.ToString()
                });
            }
            return employeBankList;
        }
        public static List<SelectListItem> Report_Tax_Period_Summary()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListTaxPeriod = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var ListCalculation = db.vw_Payroll_Period.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(p => p.Tax_Period).ToList();
            var listTaxSummary = db.tbl_Report_Tax_Summary.Where(p => p.Organization_Id == UserData.OrganizationSelected_Id).Select(s => s.Tax_Period_Id).ToList();
            ListCalculation = ListCalculation.Where(p => listTaxSummary.Contains(p.id)).ToList();
            foreach (var item in ListCalculation)
            {

                var strguid = item.id.ToString();
                if (ListTaxPeriod.Where(s => s.Value == strguid).Count() == 0)
                {
                    ListTaxPeriod.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMM yyyy}", item.Tax_Period),
                        Value = item.id.ToString()
                    });
                }


            }
            return ListTaxPeriod;
        }

        public static List<SelectListItem> EmployeBank(Guid orgID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> employeBankList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in db.vw_Generate_Bank_File.Where(s => s.Organization_ID == orgID).ToList())
            {
                employeBankList.Add(new SelectListItem
                {
                    Text = item.Source_Account_Bank,
                    Value = item.Organization_ID.ToString()
                });
            }
            return employeBankList;
        }

        public static List<SelectListItem> Report_Location()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var ModelNew = from s in db.vw_Employee_Appointment_Work_Location.Where(r => r.Organization_ID == UserData.OrganizationSelected.id).Distinct()
                           select new
                           {
                               s.Work_Location
                           };
            generalParameterList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = ""
            });
            foreach (var item in ModelNew.Distinct().OrderBy(s => s.Work_Location))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Work_Location,
                    Value = item.Work_Location
                });
            }
            return generalParameterList;
        }

        #region ReportGenerationDepartment
        public static List<SelectListItem> Report_Generation_Department()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            //generalParameterList.Add(new SelectListItem
            //{
            //    Text = "ALL",
            //    Value = ""
            //});
            foreach (var item in db.tbl_Organization_Structure.Where(s => s.Struktur == "Departments" && s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Organization_ID == UserData.OrganizationSelected_Id).OrderBy(s => s.Code))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Description,
                    Value = item.Code
                });
            }

            return generalParameterList;
        }
        #endregion
        public static List<SelectListItem> Report_Department()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            generalParameterList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = ""
            });
            foreach (var item in db.tbl_Organization_Structure.Where(s => s.Struktur == "Departments" && s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Organization_ID == UserData.OrganizationSelected_Id).OrderBy(s => s.Code))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Code + " - " + item.Description,
                    Value = item.id.ToString()
                });
            }

            return generalParameterList;
        }
        public static List<SelectListItem> Report_Employee(Guid PayrollPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> Employee_List = new List<SelectListItem>();
            //CoreVariable CoreUserVariable = new CoreVariable();
            //User_Data UserData = CoreUserVariable.CoreUserLogin();
            Employee_List.Add(new SelectListItem
            {
                Text = "ALL",
                Value = ""
            });
            List<Guid?> Employee_Id = db.tbl_Report_Employee.Where(p=>p.Tax_Period_Month_ID == PayrollPeriod && p.Employee_ID != null).Select(p => p.Employee_ID).Distinct().ToList();
            var Employee_Data = GeneralCore.EmployeeActiveAuthorizedQuery().Where(p => Employee_Id.Contains(p.id)).ToList();

            foreach (var item in Employee_Data)
            {
                Employee_List.Add(new SelectListItem
                {
                    Text = item.Employee_No + " - " + item.Full_Name,
                    Value = item.id.ToString()
                });
            }

            return Employee_List;
        }
        public static List<SelectListItem> Report_Correction()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in db.tbl_Report_Employee.Where(s => s.Organization_ID == UserData.OrganizationSelected_Id).Select(s => s.Correction).Distinct())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            //return generalParameterList;
            //List<SelectListItem> generalParameterList = new List<SelectListItem>();
            //for (int i = 0; i < 10; i++)
            //{
            //    generalParameterList.Add(new SelectListItem
            //    {
            //        Text = i.ToString(),
            //        Value = i.ToString()
            //    });
            //}
            return generalParameterList;
        }
        public static List<SelectListItem> Report_CorrectionTaxSummary(Guid PeriodId)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            //List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var data = db.tbl_Report_Tax_Summary.Where(s => s.Organization_Id == UserData.OrganizationSelected_Id && s.Tax_Period_Id== PeriodId).Select(s => s.Correction).ToList();
            //foreach (var item in db.tbl_Employee.Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE))
            //{
            //    generalParameterList.Add(new SelectListItem
            //    {
            //        Text = item.Employee_No + " - " + item.Full_Name,
            //        Value = item.id.ToString()
            //    });
            //}
            //return generalParameterList;
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in data)
            {
                if (item != null)
                {
                    if (generalParameterList.Where(s => s.Value == item.Value.ToString()).Count() == 0)
                    {
                        generalParameterList.Add(new SelectListItem
                        {
                            Text = item.ToString(),
                            Value = item.ToString()
                        });
                    }
                }
            }
            generalParameterList = generalParameterList.OrderBy(s => s.Value).ToList();
            return generalParameterList;
        }

        public static List<SelectListItem> Tbl_Organization_Structure_Position()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListRole = new List<SelectListItem>();
            foreach (var item in db.tbl_Organization_Structure.Where(s => s.Struktur == "Positions"))
            {
                ListRole.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.id.ToString()
                });
            }
            return ListRole;
        }
        //test Filter
        public static List<SelectListItem> TestTbl_Organization_Structure_Position(User_Data DataFilter)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListRole = new List<SelectListItem>();
            var Data = APP_CORE.GetData.GeneralCore.OrganizationStructurQuery();
            foreach (var item in Data.Where(s => s.Struktur == "Positions" && s.Organization_ID == DataFilter.OrganizationSelected_Id))
            {
                ListRole.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.id.ToString()
                });
            }
            return ListRole;
        }

        public static List<SelectListItem> Tbl_Organization_Account_Group_Maintenance(User_Data DataFilter)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> listAccount = new List<SelectListItem>();

            foreach (var item in GeneralCore.OrganizationAccountGroupMaintenenceQuery().Where(s => s.Organization_ID == DataFilter.OrganizationSelected_Id && s.Authorize_Status == 1 && s.Status_Code == 1).OrderBy(s => s.Account_No))
            {
                listAccount.Add(new SelectListItem
                {
                    Text = item.Account_No.ToString(),
                    Value = item.id.ToString()
                });
            }
            return listAccount;
        }

        public static List<SelectListItem> Tbl_Organization(string Organization_Cede)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListOrganiation = new List<SelectListItem>();
            foreach (var item in db.tbl_Organization.Where(p => p.Parent_Organization_Code == Organization_Cede))
            {
                ListOrganiation.Add(new SelectListItem
                {
                    Text = item.Organization_Code,
                    Value = item.id.ToString()
                });
            }
            return ListOrganiation;
        }

        public static List<SelectListItem> tbl_Organization_Parent(string Organization_Code)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListOrganiation = new List<SelectListItem>();

            //foreach (var item in db.tbl_Organization.Where(p => p.Parent_Organization_Code == Organization_Code || p.Organization_Code == Organization_Code))
            foreach (var item in GeneralCore.OrganizationByRoleUserQuery())
            {
                ListOrganiation.Add(new SelectListItem
                {
                    Text = item.Organization_Name,
                    Value = item.id.ToString()
                });
            }
            return ListOrganiation;
        }

        public static List<SelectListItem> Tbl_Organization_Payroll_Component()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListRole = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Organization_Payroll_Component> Data = null;
            Data = db.tbl_Organization_Payroll_Component.Where(p => p.Is_Generated == false && p.Organization_id == UserData.OrganizationSelected_Id && (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderBy(s => s.Organization_Payroll_Component_Code);

            foreach (var item in Data)
            {
                ListRole.Add(new SelectListItem
                {
                    Text = item.Organization_Payroll_Component_Code + " - " + item.Description,
                    Value = item.Organization_Payroll_Component_Code
                });
            }
            return ListRole;
        }

        public static List<SelectListItem> Tbl_Employee_Name_Organization_Payroll_Component()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListEmployee = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            IQueryable<tbl_Employee> Data = null;
            Data = db.tbl_Employee.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderBy(s => s.Employee_No);

            foreach (var item in Data)
            {
                ListEmployee.Add(new SelectListItem
                {
                    Text = item.Employee_No + " - " + item.Full_Name,
                    Value = item.id.ToString()
                });
            }
            return ListEmployee;
        }

        #endregion

        public static List<SelectListItem> Tbl_SysParam(string Param_Code)
        {
            var str = GeneralCore.SystemParameterQuery().Where(s => s.Param_Code == Param_Code).FirstOrDefault().Value;
            List<string> strList = new List<string>();
            strList = str.Split('|').ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in strList)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            return listItems;
        }

        public static List<SelectListItem> Tbl_SysParamSetting(string Param_Code)
        {
            var str = GeneralCore.SystemParameterQuery().Where(s => s.Param_Code == Param_Code).FirstOrDefault().Value;
            List<string> strList = new List<string>();
            strList = str.Split('|').ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in strList)
            {
                var temp =item.Split(',').ToList();
                listItems.Add(new SelectListItem
                {
                    Text = temp[1].ToString(),
                    Value = item[0].ToString()
                });
            }
            return listItems;
        }

        #endregion

        public static List<SelectListItem> Year()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<string> yearFunction = new List<string>();
            int yearNow = int.Parse(DateTime.Now.ToString("yyyy"));
            //current year -1th
            for (int i = 0; i <= 1; i++)
            {
                string year = (yearNow - i).ToString();
                yearFunction.Add(year);
            }

            //current year + 3th
            for (int i = 0; i < 3; i++)
            {
                string year = (yearNow + i).ToString();
                yearFunction.Add(year);
            }

            foreach (var item in yearFunction.Distinct().OrderBy(o => o))
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            return listItems;
        }

        public static List<CheckDay> DayCheckBox()
        {
            List<CheckDay> listItems = new List<CheckDay>();
            listItems.Add(new CheckDay { Id = 1, Name = "Monday", Checked = false });
            listItems.Add(new CheckDay { Id = 2, Name = "Tuesday", Checked = false });
            listItems.Add(new CheckDay { Id = 3, Name = "Wendesday", Checked = false });
            listItems.Add(new CheckDay { Id = 4, Name = "Thursday", Checked = false });
            listItems.Add(new CheckDay { Id = 5, Name = "Friday", Checked = false });
            listItems.Add(new CheckDay { Id = 6, Name = "Saturday", Checked = false });
            listItems.Add(new CheckDay { Id = 7, Name = "Sunday", Checked = false });
            return listItems;
        }
        public static List<SelectListItem> Status(string Code)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (Code == "4")
            {
                listItems.Add(new SelectListItem { Text = "Active", Value = "1" });
                listItems.Add(new SelectListItem { Text = "Inactive", Value = "0" });
                listItems.Add(new SelectListItem { Text = "Rejected", Value = "2" });
                listItems.Add(new SelectListItem { Text = "Deleted", Value = "999" });
            }
            else if (Code == "3")
            {
                listItems.Add(new SelectListItem { Text = "Active", Value = "1" });
                listItems.Add(new SelectListItem { Text = "Inactive", Value = "0" });
                listItems.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            }
            else
            {
                listItems.Add(new SelectListItem { Text = "Active", Value = "1" });
                listItems.Add(new SelectListItem { Text = "Inactive", Value = "0" });
            }
            return listItems;
        }
        public static List<SelectListItem> OrganizationServices(string Service, string type)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (type == GlobalVariable.CONST_CREATE)
            {
                if (Service == GlobalVariable.CONST_ORGANIZATION_SERIVCE_SYSADMIN)
                {
                    //listItems.Add(new SelectListItem { Text = "Service Admin", Value = "Service Admin" });
                    listItems.Add(new SelectListItem { Text = "Payroll Outsourcing", Value = "Payroll Outsourcing" });
                    listItems.Add(new SelectListItem { Text = "End Client", Value = "End Client" });
                }
                else if (Service == GlobalVariable.CONST_ORGANIZATION_SERIVCE_OUTCORSING)
                {
                    listItems.Add(new SelectListItem { Text = "End Client", Value = "End Client" });
                }
                else
                {
                    listItems.Add(new SelectListItem { Text = " ", Value = " " });
                }
            }

            if (type == GlobalVariable.CONST_EDIT)
            {
                if (Service == GlobalVariable.CONST_ORGANIZATION_SERIVCE_SYSADMIN)
                {
                    //listItems.Add(new SelectListItem { Text = "Service Admin", Value = "Service Admin" });
                    listItems.Add(new SelectListItem { Text = "Payroll Outsourcing", Value = "Payroll Outsourcing" });
                    listItems.Add(new SelectListItem { Text = "End Client", Value = "End Client" });
                }
                else
                {
                    listItems.Add(new SelectListItem { Text = "End Client", Value = "End Client" });
                }
            }

            return listItems;
        }

        public static List<SelectListItem> OrganizationParentList(string Service, Guid Organization_selectedID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<tbl_Organization> OrganizationQuery = db.tbl_Organization.ToList();
            if (Service == GlobalVariable.CONST_ORGANIZATION_SERIVCE_SYSADMIN)
            {
                var dataAdmin = GeneralCore.OrganizationQuery().Where(p => p.id == Organization_selectedID).FirstOrDefault();
                listItems.Add(new SelectListItem { Text = dataAdmin.Organization_Name, Value = dataAdmin.id.ToString() }); // ADMIN
                //listItems.Add(dataAdmin); // ADMIN
                var ChildAdmin = GeneralCore.OrganizationQuery().Where(p => p.Parent_Organization_Code == dataAdmin.Organization_Code).ToList();
                foreach (var ChildDataAdmin in ChildAdmin)
                {
                    listItems.Add(new SelectListItem { Text = ChildDataAdmin.Organization_Name, Value = ChildDataAdmin.id.ToString() }); // CHILD ADMIN
                    //listItems.Add(ChildDataAdmin); // CHILD ADMIN
                }
                foreach (var ChildDataursourcing in ChildAdmin)
                {
                    if (ChildDataursourcing.Organization_Service == GlobalVariable.CONST_PAYROL_OUTSOURCING)
                    {
                        var ChildOutSourcing = GeneralCore.OrganizationQuery().Where(p => p.Parent_Organization_Code == ChildDataursourcing.Organization_Code).ToList();
                        foreach (var ChildDataOutsourcing in ChildOutSourcing)
                        {
                            listItems.Add(new SelectListItem { Text = ChildDataOutsourcing.Organization_Name, Value = ChildDataOutsourcing.id.ToString() }); // CHILD OUTSOURCING
                            //listItems.Add(ChildDataOutsourcing); // CHILD OUTSOURCING
                        }

                    }
                }
            }
            else if (Service == GlobalVariable.CONST_ORGANIZATION_SERIVCE_OUTCORSING)
            {
                var dataOutsourcing = GeneralCore.OrganizationQuery().Where(p => p.id == Organization_selectedID).FirstOrDefault();
                listItems.Add(new SelectListItem { Text = dataOutsourcing.Organization_Name, Value = dataOutsourcing.id.ToString() }); // OUTSOURCING
                //listItems.Add(dataOutsourcing); // OUTSOURCING
                var ChildOutsourcing = GeneralCore.OrganizationQuery().Where(p => p.Parent_Organization_Code == dataOutsourcing.Organization_Code).ToList();
                foreach (var ChildOursourcing in ChildOutsourcing)
                {
                    listItems.Add(new SelectListItem { Text = ChildOursourcing.Organization_Name, Value = ChildOursourcing.id.ToString() }); // CHILD OUTSOURCING
                    //listItems.Add(ChildOursourcing); // CHILD OUTSOURCING
                }
            }
            else
            {
                var dataEnd = GeneralCore.OrganizationQuery().Where(p => p.id == Organization_selectedID).FirstOrDefault();
                listItems.Add(new SelectListItem { Text = dataEnd.Organization_Name, Value = dataEnd.id.ToString() }); // ENDCLIENT
                //listItems.Add(dataEnd); // ENDCLIENT
            }
            return listItems;
        }




        public static List<SelectListItem> MonthList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "January", Value = "1" });
            listItems.Add(new SelectListItem { Text = "February", Value = "2" });
            listItems.Add(new SelectListItem { Text = "March", Value = "3" });
            listItems.Add(new SelectListItem { Text = "April", Value = "4" });
            listItems.Add(new SelectListItem { Text = "May", Value = "5" });
            listItems.Add(new SelectListItem { Text = "June", Value = "6" });
            listItems.Add(new SelectListItem { Text = "July", Value = "7" });
            listItems.Add(new SelectListItem { Text = "August", Value = "8" });
            listItems.Add(new SelectListItem { Text = "September", Value = "9" });
            listItems.Add(new SelectListItem { Text = "October", Value = "10" });
            listItems.Add(new SelectListItem { Text = "November", Value = "11" });
            listItems.Add(new SelectListItem { Text = "December", Value = "12" });
            return listItems;
        }

        public static List<SelectListItem> pagesize()
        {
            List<SelectListItem> ListModel = new List<SelectListItem>();
            List<string> PageSizeData = UICommonFunction.ModelSysParam("Page_Size_Paging").Value.Split('|').ToList();
            foreach (var size in PageSizeData)
            {
                ListModel.Add(new SelectListItem
                {
                    Text = size,
                    Value = size
                });
            }
            return ListModel;
        }



        #region LIST_FIELD_tbl_Payroll_Period
        public static List<SelectListItem> fieldTblPayrollPeriod()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Tax Period", Value = "Tax_Period" });
            listItems.Add(new SelectListItem { Text = "Period Start Date", Value = "Period_Start_Date" });
            listItems.Add(new SelectListItem { Text = "Period End Date", Value = "Period_End_Date" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_FIELD_tbl_Employee_Appointment
        public static List<SelectListItem> fieldTblEmployeeAppointment()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employment Status", Value = "employment_status" });
            listItems.Add(new SelectListItem { Text = "Effective Date", Value = "effective_date" });
            listItems.Add(new SelectListItem { Text = "Date of Hire", Value = "date_of_hire" });
            listItems.Add(new SelectListItem { Text = "Old Employee Number", Value = "old_employee_number" });
            listItems.Add(new SelectListItem { Text = "End Date of Probation", Value = "end_date_of_probation" });
            listItems.Add(new SelectListItem { Text = "Records Status", Value = "record_status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "authorize" });
            return listItems;
        }
        #endregion

        #region SelectList_Employee_Status

        public static List<SelectListItem> SelectList_Employee_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListEmployee = new List<SelectListItem>();
            foreach (var item in db.tbl_Appointment_Status_Information.OrderBy(s => s.id))
            {
                ListEmployee.Add(new SelectListItem
                {
                    Text = item.Employee_Status,
                    Value = item.Employee_Status
                });
            }
            return ListEmployee;
        }

        #endregion

        #region SelectList_Work_Location

        public static List<SelectListItem> SelectList_Work_Location()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListWorkLocation = new List<SelectListItem>();
            foreach (var item in db.tbl_Appointment_Work_Location.OrderBy(s => s.id))
            {
                ListWorkLocation.Add(new SelectListItem
                {
                    Text = item.Work_Location,
                    Value = item.Work_Location
                });
            }
            return ListWorkLocation;
        }

        #endregion

        #region SelectList_Working_Time

        public static List<SelectListItem> SelectList_Working_Time()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListWorking_Time = new List<SelectListItem>();
            foreach (var item in GeneralCore.WorkingTimeSelectListQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Working_Time_Code))
            {
                ListWorking_Time.Add(new SelectListItem
                {
                    Text = item.Working_Time_Code,
                    Value = item.Working_Time_Code.ToString()
                });
            }
            return ListWorking_Time;
        }

        #endregion

        #region SelectList_Position

        public static List<SelectListItem> SelectList_Position()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPosition = new List<SelectListItem>();
            foreach (var item in GeneralCore.PositionSelectListQuery().OrderBy(p => p.Code))
            {
                ListPosition.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.Code
                });
            }
            return ListPosition;
        }

        #endregion

        #region SelectList_Division

        public static List<SelectListItem> SelectList_Division()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListDivision = new List<SelectListItem>();
            foreach (var item in GeneralCore.DivisionSelectListQuery().OrderBy(p => p.Code))
            {
                ListDivision.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.Code
                });
            }
            return ListDivision;
        }

        #endregion

        #region SelectList_Department

        public static List<SelectListItem> SelectList_Department()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.DepartmentSelectListQuery().OrderBy(p => p.Code))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.Code
                });
            }
            return ListDepartment;
        }

        #endregion

        #region SelectList_Department

        public static List<SelectListItem> SelectList_Employee_Name()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            IQueryable<vw_Employee_Appointment_Status> model = null;

            model = db.vw_Employee_Appointment_Status.Where(s => s.Organization_ID == UserData.OrganizationSelected_Id).OrderBy(s => s.Full_Name);
            foreach (var item in model)
            {
                if (!ListDepartment.Any(l => l.Value == item.id.ToString()))
                {
                    ListDepartment.Add(new SelectListItem
                    {
                        Text = item.Employee_No + " - " + item.Full_Name,
                        Value = item.id.ToString()
                    });
                }
            }
            return ListDepartment;
        }

        #endregion

        #region SelectList_Grade

        public static List<SelectListItem> SelectList_Grade()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListGrade = new List<SelectListItem>();
            foreach (var item in GeneralCore.GradeSelectListQuery().OrderBy(p => p.Code))
            {
                ListGrade.Add(new SelectListItem
                {
                    Text = item.Code,
                    Value = item.Code
                });
            }
            return ListGrade;
        }

        #endregion

        #region SelectList_Tax_Status

        public static List<SelectListItem> SelectList_Tax_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListTax_Status = new List<SelectListItem>();
            foreach (var item in db.tbl_Tax_Status_Effective_Year.OrderBy(s => s.id))
            {
                ListTax_Status.Add(new SelectListItem
                {
                    Text = item.Tax_Status,
                    Value = item.Tax_Status
                });
            }
            return ListTax_Status;
        }

        #endregion

        #region SelectList_Payroll_Period

        public static List<SelectListItem> SelectList_Payroll_Period(DateTime? CurrentTaxPeriod, string type, out DateTime Tax_Period_Max)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPayroll_Period = new List<SelectListItem>();
            DateTime TaxPeriod = new DateTime();
            Tax_Period_Max = new DateTime();

            if (CurrentTaxPeriod != null)
            {
                TaxPeriod = Convert.ToDateTime(CurrentTaxPeriod);
                int lastYear = TaxPeriod.Year - 1;
                int CurrentYear = TaxPeriod.Year;

                int CurrentMonth = TaxPeriod.Month;
                int FutureMonth = TaxPeriod.Month + 1;

                var DecPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == lastYear && p.Tax_Period.Month == 12).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();
                List<tbl_Payroll_Period_Detail> ListPayrollPeriod = new List<tbl_Payroll_Period_Detail>();
                if (type == "Permanent")
                {
                    if (TaxPeriod.Month == 12)
                    {
                        int FutureYear = CurrentYear + 1;
                        ListPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => (p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == CurrentYear && p.Tax_Period.Month == CurrentMonth) || (p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == FutureYear && p.Tax_Period.Month == 1)).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();
                    }
                    else
                    {
                        ListPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == CurrentYear && (p.Tax_Period.Month == CurrentMonth || p.Tax_Period.Month == FutureMonth)).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();
                    }
                    Tax_Period_Max = ListPayrollPeriod.Max(p => p.Period_End_Date);

                }
                else
                    ListPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == CurrentYear && p.Tax_Period.Month == CurrentMonth).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();

                foreach (var item in DecPayrollPeriod)
                {
                    ListPayroll_Period.Add(new SelectListItem
                    {
                        Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + "-" + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                        Value = item.id.ToString()
                    });
                }
                foreach (var item in ListPayrollPeriod)
                {
                    ListPayroll_Period.Add(new SelectListItem
                    {
                        Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + "-" + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                        Value = item.id.ToString()
                    });
                }
            }
            else
            {
                ListPayroll_Period.Add(new SelectListItem
                {
                    Text = "Tax Period Not Founds",
                    Value = ""
                });
            }
            return ListPayroll_Period;
        }

        #endregion

        #region SelectList_PayrollClosing_byCalculation
        public static List<SelectListItem> SelectList_PayrollClosing_ForClosing(string CalculationType)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListTaxPeriod = new List<SelectListItem>();
            var ListPayroll_Closing = GeneralCore.PayrollClosing().Where(p => p.Calculation_Type == CalculationType && !(p.Status_Code == GlobalVariable.CONST_STATUS_REJECT)).Select(p => p.Calculation_ID).ToList();
            var ListCalculation = GeneralCore.PayrollCalculation().Where(p => p.Calculation_Type == CalculationType && !ListPayroll_Closing.Contains(p.id) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && p.Calculate_Status == GlobalVariable.CONST_CALCULATION_STATUS_SUCCESS).OrderBy(p => p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.tbl_Payroll_Period_Detail.Period_Start_Date).ToList();
            string Batch = ListCalculation.Select(s => s.Batch).FirstOrDefault();
            var ListBatch = GeneralCore.PayrollCalculation().Where(p => p.Batch == Batch).ToList();
            var ListBatchAuthorized = GeneralCore.PayrollCalculation().Where(p => p.Batch == Batch && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED).ToList();

            if (ListBatchAuthorized.Count() == ListBatch.Count())
            {
                foreach (var item in ListCalculation)
                {
                    ListTaxPeriod.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMM yyyy}", item.tbl_Payroll_Period_Detail.Tax_Period),
                        Value = item.id.ToString()
                    });
                }
            }

            return ListTaxPeriod;
        }

        #endregion

        #region SelectList_Tax_Period

        public static List<SelectListItem> SelectList_Tax_Period(Guid? selected_current_payroll_period)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListTax_Period = new List<SelectListItem>();
            //int lastYear = DateTime.Now.Year - 1;
            //int CurrentYear = DateTime.Now.Year;
            //int FutureYear = DateTime.Now.Year + 1;
            var taxperiod = db.tbl_Payroll_Period_Detail.Where(p => p.Payroll_Period_ID == selected_current_payroll_period).ToList();
            foreach (var item in taxperiod)
            {
                ListTax_Period.Add(new SelectListItem
                {
                    Text = string.Format("{0:dd/MM/yyyy}", item.Tax_Period),
                    Value = string.Format("{0:dd/MM/yyyy}", item.Tax_Period)
                });
            }
            return ListTax_Period;
        }

        #endregion
        #region SelectList_Payslip_EmpLocation
        //public static List<SelectListItem> SelectList_Payslip_EmpLocation(Guid orgId)
        //{
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    List<SelectListItem> ListPayslip_EmpLocation = new List<SelectListItem>();

        //    var dbLocation = db.tbl_Organization.Where(p => p.id == orgId)
        //    var dbEmpLocation = db.tbl_Appointment_Work_Location
        //    foreach (var item in ListPayrollPeriod)
        //    {
        //        ListPayslip_EmpLocation.Add(new SelectListItem
        //        {
        //            Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + "-" + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
        //            Value = item.id.ToString()
        //        });
        //    }
        //    return ListPayslip_EmpLocation;
        //}
        #endregion

        #region SelectList_Payslip_EmpLocation
        public static List<SelectListItem> SelectList_Payslip_EmpDept()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPayroll_Period = new List<SelectListItem>();
            int lastYear = DateTime.Now.Year - 1;
            int CurrentYear = DateTime.Now.Year;
            int FutureYear = DateTime.Now.Year + 1;
            var ListPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == lastYear || p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == CurrentYear || p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == FutureYear).OrderBy(p => p.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).ThenBy(p => p.Period_End_Date).ToList();
            foreach (var item in ListPayrollPeriod)
            {
                ListPayroll_Period.Add(new SelectListItem
                {
                    Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + "-" + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                    Value = item.id.ToString()
                });
            }
            return ListPayroll_Period;
        }
        #endregion
        #region  SelectList_Employee_Component_Payroll
        public static List<SelectListItem> SelectList_Employee_Component_Payroll(string strID)
        {

            var strGuid = Guid.Parse(strID);
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            db.Configuration.ProxyCreationEnabled = false;

            List<SelectListItem> List = new List<SelectListItem>();
            List<string> strList = new List<string>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var ListEmployeeComponent = db.vw_Employee_Payroll_Component.Where(p => !p.Organization_Payroll_Component_Code.Contains("#9") && p.Employee_id == strGuid && (p.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STR_STATUS_ACTIVE)).ToList();
            if (ListEmployeeComponent.Count > 0)
            {
                foreach (var itemListEmployeeComponent in ListEmployeeComponent)
                {
                    if (!List.Any(l => l.Value == itemListEmployeeComponent.Organization_Payroll_Component_Code))
                    {
                        List.Add(new SelectListItem
                        {
                            Text = itemListEmployeeComponent.Organization_Payroll_Component_Code + " - " + itemListEmployeeComponent.Description,
                            Value = itemListEmployeeComponent.Organization_Payroll_Component_Code
                        });
                    }
                }
            }

            return List;
        }
        #endregion

        #region SelectList_Payslip_Distribution
        public static List<SelectListItem> SelectList_Payslip_Distribution()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "ALL", Value = "ALL" });
            listItems.Add(new SelectListItem { Text = "Hardcopy", Value = GlobalVariable.CONST_PAYSLIP_DISTIBUTION_HARDCOPY });
            listItems.Add(new SelectListItem { Text = "Email", Value = GlobalVariable.CONST_PAYSLIP_DISTIBUTION_EMAIL });
            listItems.Add(new SelectListItem { Text = "e-Payslip", Value = GlobalVariable.CONST_PAYSLIP_DISTIBUTION_EPAYSLIP });
            return listItems;
        }
        #endregion

        #region SelectList_ListRun
        public static List<SelectListItem> SelectList_ListRun()
        {
            // still testing
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "1", Value = "1" });
            listItems.Add(new SelectListItem { Text = "2", Value = "2" });
            listItems.Add(new SelectListItem { Text = "3", Value = "3" });
            return listItems;
        }
        #endregion

        #region
        public static List<SelectListItem> SelectList_Payroll_Tax_Period()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPayroll_Period = new List<SelectListItem>();
            foreach (var item in GeneralCore.PayrollPeriodDetailQuery().OrderBy(s => s.Tax_Period))
            {
                ListPayroll_Period.Add(new SelectListItem
                {
                    Text = string.Format("{0:MMM yyyy}", item.Tax_Period),
                    Value = item.Tax_Period.ToString()
                });
            }
            return ListPayroll_Period;
        }
        #endregion

        #region LIST FIELD tbl_Upload_Staging_Header
        public static List<SelectListItem> fieldUploadStagingHeader()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Upload Number", Value = "Upload_Number" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            listItems.Add(new SelectListItem { Text = "Created By", Value = "Created_By" });
            listItems.Add(new SelectListItem { Text = "Created Date", Value = "Created_DateTime" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize By", Value = "Authorized_By" });
            listItems.Add(new SelectListItem { Text = "Authorize Date", Value = "Authorized_DateTime" });
            listItems.Add(new SelectListItem { Text = "Total Record", Value = "Total_Record" });
            listItems.Add(new SelectListItem { Text = "Upload Status", Value = "Upload_Status" });

            return listItems;
        }
        #endregion

        #region LIST_Employee_Code_Name
        public static List<SelectListItem> SelectList_Employee_Code_Name()
        {
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.PersonalInformationQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Full_Name))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Employee_No + " - " + item.Full_Name,
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region List Loan
        #region LIST_FIELD_tbl_Loan
        public static List<SelectListItem> fieldTblLoan()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Loan No", Value = "Loan_NO" });
            listItems.Add(new SelectListItem { Text = "Employee ID", Value = "Employee_ID" });
            listItems.Add(new SelectListItem { Text = "Type", Value = "Type_Loan" });
            listItems.Add(new SelectListItem { Text = "Component Linkage", Value = "Component_Linkage" });
            listItems.Add(new SelectListItem { Text = "Currency", Value = "Currency" });
            listItems.Add(new SelectListItem { Text = "Loan Amount", Value = "Loan_Amount" });
            listItems.Add(new SelectListItem { Text = "Outstanding Loan Amount", Value = "Outstanding_Loan_Amount" });
            listItems.Add(new SelectListItem { Text = "Rate", Value = "Rate" });
            listItems.Add(new SelectListItem { Text = "Installment Amount", Value = "Installment_Amount" });
            listItems.Add(new SelectListItem { Text = "Tenor", Value = "Tenor" });
            listItems.Add(new SelectListItem { Text = "Outstanding Tenor", Value = "Outstanding_Tenor" });
            listItems.Add(new SelectListItem { Text = "Upload Number", Value = "Upload_Number" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_Component_Linkage
        public static List<SelectListItem> SelectList_Component_Linkage_LN(string selectedEmployee)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var compGroup = db.tbl_General_Parameter.Where(s => s.Field_Name == "Loan").ToList();
            var compGroupSelected = "";
            if (compGroup.Count() > 0)
            {
                compGroupSelected = compGroup.FirstOrDefault().Field_Value;
            }

            var orgCompGroup = db.tbl_Organization_Payroll_Component.Where(p => p.Organization_id == UserData.OrganizationSelected.id && p.Component_Group == compGroupSelected && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).ToList();
            List<string> orgCompGroupSelected = new List<string>();
            if (orgCompGroup.Count() > 0)
            {
                foreach (var item in orgCompGroup)
                {
                    orgCompGroupSelected.Add(item.Organization_Payroll_Component_Code);
                }
            }

            Global_Loan loanModels = new Global_Loan();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            if (selectedEmployee != "")
            {
                if (selectedEmployee != null)
                {
                    Guid guidEmployee = new Guid(selectedEmployee);
                    var Loan = UICommonFunction.GetParameter("TypeComponentGroup");
                    foreach (var item in db.vw_Employee_Payroll_Component.Where(s => s.Employee_id == guidEmployee && orgCompGroupSelected.Contains(s.Organization_Payroll_Component_Code) && s.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STR_STATUS_ACTIVE).OrderBy(s => s.id))
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = item.Organization_Payroll_Component_Code + " - " + item.Description,
                            Value = item.id.ToString()
                        });
                    }
                }
            }
            return ListDepartment;
        }
        #endregion
        #endregion

        #region List CompensationAndBenefit
        #region LIST_FIELD_tbl_Compensation_Benefit
        public static List<SelectListItem> fieldtblCompensationBenefit()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID", Value = "Employee_ID" });
            listItems.Add(new SelectListItem { Text = "Type", Value = "Type_Compensation_And_Benefit" });
            listItems.Add(new SelectListItem { Text = "Component Linkage", Value = "Component_Linkage" });
            listItems.Add(new SelectListItem { Text = "Currency", Value = "Currency" });
            listItems.Add(new SelectListItem { Text = "Budget", Value = "Budget" });
            listItems.Add(new SelectListItem { Text = "Upload Number", Value = "Upload_Number" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        #region LIST_Component_Linkage
        public static List<SelectListItem> SelectList_Component_Linkage_CB(string selectedEmployee)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var compGroup = db.tbl_General_Parameter.Where(s => s.Field_Name == "Compensation & Benefit").ToList();
            var compGroupSelected = "";
            if (compGroup.Count() > 0)
            {
                compGroupSelected = compGroup.FirstOrDefault().Field_Value;
            }

            var orgCompGroup = db.tbl_Organization_Payroll_Component.Where(p => p.Organization_id == UserData.OrganizationSelected.id && p.Component_Group == compGroupSelected && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).ToList();
            List<string> orgCompGroupSelected = new List<string>();
            if (orgCompGroup.Count() > 0)
            {
                foreach (var item in orgCompGroup)
                {
                    orgCompGroupSelected.Add(item.Organization_Payroll_Component_Code);
                }
            }

            Global_Compensation_Benefit compensationenefitModels = new Global_Compensation_Benefit();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            if (selectedEmployee != "")
            {
                if (selectedEmployee != null)
                {
                    Guid guidEmployee = new Guid(selectedEmployee);
                    foreach (var item in db.vw_Employee_Payroll_Component.Where(s => s.Employee_id == guidEmployee && orgCompGroupSelected.Contains(s.Organization_Payroll_Component_Code) && s.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STR_STATUS_ACTIVE).OrderBy(s => s.id))
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = item.Organization_Payroll_Component_Code + " - " + item.Description,
                            Value = item.id.ToString()
                        });
                    }
                }
            }
            return ListDepartment;
        }
        #endregion
        #endregion

        #region List YearPeriod
        public static List<SelectListItem> SelectList_Year_Period()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.TaxPeriodQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Year))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Tax_Year.ToString(),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Organization_Selected
        public static List<SelectListItem> SelectList_Organization(Guid selectedOrg)
        {
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.OrganizationQuery().OrderBy(s => s.Organization_Name))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Organization_Code + " - " + item.Organization_Name,
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region Generate Bank File

        //select list Bank
        public static List<SelectListItem> Bank_GBF(Guid Organization_ID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> BankList = new List<SelectListItem>();
            var query = db.tbl_Bank_Information.Where(p => p.Organization_ID == Organization_ID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();
            if (Organization_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                BankList.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Select..",
                });
            }
            else
            {
                foreach (var item in query)
                {
                    if (item != null)
                    {
                        BankList.Add(new SelectListItem
                        {
                            Value = item.id.ToString(),
                            Text = item.Bank.ToString(),
                        });
                    }
                }
            }
            return BankList;
        }
        //select list File type
        public static List<GBF_File_Type> FileType_GBF(string BankCode)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<GBF_File_Type> FileType = new List<GBF_File_Type>();
            var query = db.vw_Generate_Bank_File_Template.Where(p => p.Bank_Code == BankCode).Select(s => s.File_Format).ToList().Distinct();
            if (BankCode == null || query.Count() == null)
            {
                FileType.Add(new GBF_File_Type
                {
                    Value = "",
                    Text = "Select..",
                });
            }
            else
            {
                foreach (var item in query)
                {
                    if (item != null)
                    {
                        FileType.Add(new GBF_File_Type
                        {
                            Text = item,
                            Value = item,
                        });
                    }
                }
            }
            return FileType;
        }

        public static List<SelectListItem> FileType()
        {
            List<SelectListItem> FileType = new List<SelectListItem>();
            FileType.Add(new SelectListItem
            {
                Value = "EXCEL",
                Text = "EXCEL",
            });
            FileType.Add(new SelectListItem
            {
                Value = "PDF",
                Text = "PDF",
            });
            return FileType;
        }

        //select Period List
        public static List<SelectListItem> PayrollPeriod_GBF(Guid OrgID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var LPeriodID = db.vw_Generate_Bank_File_Details.Where(r => r.Organization_ID == OrgID).Select(p => p.Payroll_Period_ID).Distinct().ToList();
            var ModelNew = from s in db.vw_Payroll_Period.Where(p => LPeriodID.Contains(p.id)).Distinct()
                           select new
                           {
                               s.id,
                               s.Period_Start_Date,
                               s.Period_End_Date,
                               s.Tax_Period
                           };
            var a = ModelNew.Distinct();
            foreach (var item in ModelNew.OrderBy(s => s.Period_Start_Date))
            {
                List.Add(new SelectListItem
                {
                    Text = item.Tax_Period.ToString("dd/MM/yyyy") + " | " + item.Period_Start_Date.ToString("dd/MM/yyyy") + "-" + item.Period_End_Date.ToString("dd/MM/yyyy"),
                    Value = item.id.ToString()
                });
            }
            return List;
        }

        #region LIST_FIELD_tbl_Organization_Email_Setup
        public static List<SelectListItem> fieldTblOrganizationEmailSetup()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Organization Code", Value = "Organization_Code" });
            listItems.Add(new SelectListItem { Text = "Organization Name", Value = "Organization_Name" });
            listItems.Add(new SelectListItem { Text = "eMail Address", Value = "eMail_Address" });
            listItems.Add(new SelectListItem { Text = "SMTP Server", Value = "SMTP_Server" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion
        #region SecurityEmail
        public static List<SelectListItem> SecurityEmail()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var query = db.tbl_General_Parameter.Where(s => s.Table_Name == "SECURITY_EMAIL" && s.Status_Code == 1 && s.Authorize_Status == 1).ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in query)
            {
                listItems.Add(new SelectListItem { Text = item.Field_Name, Value = item.Field_Value });
            }

            return listItems;
        }
        #endregion
        #region LIST_Payroll_Period_Selected
        public static List<SelectListItem> SelectList_Payroll_Period(Guid selectedOrg)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            var Data = db.tbl_Payroll_Calculation.Where(p => p.Organization_ID == selectedOrg && !(p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.Status_Code == GlobalVariable.CONST_STATUS_DELETED) && !(p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_DELETED)).Select(s => s.tbl_Payroll_Period_Detail).OrderBy(o => o.Tax_Period).ToList().Distinct();
            //var Data = db.tbl_Payroll_Period_Detail.Where(p => p.tbl_Payroll_Period.Organization_ID == selectedOrg && !(p.tbl_Payroll_Period.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period.Status_Code == CoreVariable.CONST_STATUS_DELETED)).OrderBy(o=> o.Tax_Period).ToList();
            foreach (var item in Data)
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Tax_Period.ToString("MMMM yyyy"),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Payroll_Period__History_Selected
        public static List<SelectListItem> SelectList_Payroll_Period_History(Guid selectedOrg, string selectedYear)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            var Data = db.tbl_Payroll_Calculation.Where(p => p.Organization_ID == selectedOrg && !(p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.Status_Code == GlobalVariable.CONST_STATUS_DELETED) && !(p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_DELETED)).Select(s => s.tbl_Payroll_Period_Detail).OrderBy(o => o.Tax_Period).Distinct().ToList();
            Data = Data.Where(p => p.Tax_Period.Year.ToString() == selectedYear).OrderBy(o => o.Tax_Period).ToList();
            foreach (var item in Data)
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Tax_Period.ToString("MMMM yyyy"),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region List YearPeriodHistory
        public static List<SelectListItem> SelectList_Year_Period_History()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.TaxPeriodQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Year))
            {
                    ListDepartment.Add(new SelectListItem
                    {
                        Text = item.Tax_Year.ToString(),
                        Value = item.Tax_Year.ToString(),
                    });
            }
            return ListDepartment;
        }
        #endregion

        #region SelectList_Payroll_Comparative_Period
        public static List<SelectListItem> SelectList_Payroll_Comparative_Period(Guid orgSelected)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in db.vw_Payroll_Period.Where(p => p.Organization_ID == orgSelected && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ThenBy(s => s.Year))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Tax_Period_Calcualtion_A1
        public static List<SelectListItem> List_Tax_Period_Calculation_A1(Guid orgSelected, DateTime? CurrentTaxPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();

            List<SelectListItem> listTaxCalculation = new List<SelectListItem>();
            var listSelectedTaxPeriodID = GeneralCore.PayrollCalculation().Where(p => p.Organization_ID == orgSelected && p.Calculation_Type.ToLower() == "permanent" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Select(p => p.Payroll_Period_ID).ToList();
            var listTaxPeriodID = db.vw_Payroll_Period.Where(p => listSelectedTaxPeriodID.Contains(p.id) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Tax_Period).ToList();

            if (listTaxPeriodID.Count != 0)
            {
                var maxDate = listTaxPeriodID.Select(s => s.Tax_Period).Max();
                var minDate = maxDate.AddMonths(-24);

                var listTaxPeriodYear = listTaxPeriodID.Where(p => p.Tax_Period > minDate && p.Tax_Period <= maxDate).ToList();
                foreach (var item in listTaxPeriodYear)
                {
                    listTaxCalculation.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                        Value = item.Tax_Period.ToString()
                    });
                }
            }
            return listTaxCalculation;
        }
        #endregion

        #region LIST_Tax_Period_Calculation_1721_Final
        public static List<SelectListItem> List_Tax_Period_Calculation_Final(Guid orgSelected, DateTime? CurrentTaxPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();

            List<SelectListItem> listTaxCalculation = new List<SelectListItem>();
            var listSelectedTaxPeriodID = GeneralCore.PayrollCalculation().Where(p => p.Organization_ID == orgSelected && p.Calculation_Type.ToLower() == "severance" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Select(p => p.Payroll_Period_ID).ToList();
            var listTaxPeriodID = db.vw_Payroll_Period.Where(p => listSelectedTaxPeriodID.Contains(p.id) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Tax_Period).ToList();

            if (listTaxPeriodID.Count != 0)
            {
                var maxDate = listTaxPeriodID.Select(s => s.Tax_Period).Max();
                var minDate = maxDate.AddMonths(-24);

                var listTaxPeriodYear = listTaxPeriodID.Where(p => p.Tax_Period > minDate && p.Tax_Period <= maxDate).ToList();
                foreach (var item in listTaxPeriodYear)
                {
                    listTaxCalculation.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                        Value = item.id.ToString()
                    });
                }
            }
            return listTaxCalculation;
        }
        #endregion

        #region LIST_Tax_Period_Calculation_1721_Tidak_Final
        public static List<SelectListItem> List_Tax_Period_Calculation_Tidak_Final(Guid orgSelected, DateTime? CurrentTaxPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> listTaxCalculation = new List<SelectListItem>();
            var listSelectedTaxPeriodID = GeneralCore.PayrollCalculation().Where(p => p.Organization_ID == orgSelected && p.Calculation_Type.ToLower() == "nonpermanent" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Select(p => p.Payroll_Period_ID).ToList();
            var listTaxPeriodID = db.vw_Payroll_Period.Where(p => listSelectedTaxPeriodID.Contains(p.id) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Tax_Period).ToList();

            if (listTaxPeriodID.Count != 0)
            {
                var maxDate = listTaxPeriodID.Select(s => s.Tax_Period).Max();
                var minDate = maxDate.AddMonths(-24);

                var listTaxPeriodYear = listTaxPeriodID.Where(p => p.Tax_Period > minDate && p.Tax_Period <= maxDate).ToList();
                foreach (var item in listTaxPeriodYear)
                {
                    listTaxCalculation.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                        Value = item.id.ToString()
                    });
                }
            }
            return listTaxCalculation;
        }
        #endregion

        #region LIST_Org_Tax_ID
        public static List<SelectListItem> SelectList_Org_Tax_ID(Guid selectedOrg)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.HeadOfficeBranchQuery().Where(p => p.Organization_ID == selectedOrg).GroupBy(g => g.HO_Branch_TAX_ID).Select(x => new { HO_Branch_TAX_ID = x.FirstOrDefault().HO_Branch_TAX_ID }).ToList())
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.HO_Branch_TAX_ID,
                    Value = item.HO_Branch_TAX_ID.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Tax_Period
        public static List<SelectListItem> list_tax_period(Guid orgSelected)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in db.vw_Payroll_Period.Where(p => p.Organization_ID == orgSelected && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ThenBy(s => s.Year))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion



        #region LIST_Payroll_Period
        public static List<SelectListItem> listPayrollPeriod(string id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in db.vw_Payroll_Period.Where(p => p.id.ToString() == id && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + " - " + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_PIC_Name_Organization
        public static List<SelectListItem> listPICNameOrg(Guid selectedOrg)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.ContactPersonQuery().Where(p => p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Name))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Name.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Type_Download_Pdf_Excel
        public static List<SelectListItem> List_Type_Download_Pdf_Excel(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            List<string> listTypeDownload = new List<string>();
            List<tbl_General_Parameter> listTypeDownlod = new List<tbl_General_Parameter>();
            listTypeDownlod = GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq).ToList();
            foreach (var item in listTypeDownlod.Where(s => s.Field_Name.ToLower() == "pdf" || s.Field_Name.ToLower() == "excel"))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Name.ToUpper(),
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region LIST_Correction
        public static List<SelectListItem> listCorrection(string selectedTaxID, string selectedTaxPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();

            var selectedTaxPeriodDate = db.vw_Payroll_Period.Where(p => p.id.ToString() == selectedTaxPeriod && p.Organization_ID == UserData.OrganizationSelected_Id).Select(s => s.Tax_Period).FirstOrDefault();
            if (selectedTaxPeriodDate != null)
            {
                var listCorrectionActive = from ptc in db.tbl_Payroll_Tax_Correction
                                           join pc in db.tbl_Payroll_Calculation on ptc.Calculation_ID equals pc.id
                                           where pc.Organization_ID == UserData.OrganizationSelected_Id && pc.Status_Code == 1 && ptc.Tax_Period == selectedTaxPeriodDate && pc.Calculation_Type == "Permanent"
                                           select ptc;
                var listCorrection = listCorrectionActive.Where(p => p.Tax_ID == selectedTaxID).GroupBy(g => g.Pembetulan).Select(x => new { pembetulan = x.FirstOrDefault().Pembetulan }).ToList();
                ListDepartment.Add(new SelectListItem
                {
                    Text = "SELECT",
                    Value = ""
                });
                foreach (var item in listCorrection.OrderBy(o => o.pembetulan))
                {
                    ListDepartment.Add(new SelectListItem
                    {
                        Text = item.pembetulan == null ? "0" : item.pembetulan.ToString(),
                        Value = item.pembetulan == null ? "0" : item.pembetulan.ToString()
                    });
                }
            }

            return ListDepartment;
        }
        #endregion

        #region LIST_Tax_Payroll_Calculation_Bulanan
        public static List<SelectListItem> List_Tax_Period_Calculation_Bulanan(Guid orgSelected, DateTime? CurrentTaxPeriod)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> listTaxCalculation = new List<SelectListItem>();
            var listSelectedTaxPeriodID = GeneralCore.PayrollCalculation().Where(p => p.Organization_ID == orgSelected && p.Calculation_Type.ToLower() == "permanent" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Select(p => p.Payroll_Period_ID).ToList();
            var listTaxPeriodID = db.vw_Payroll_Period.Where(p => listSelectedTaxPeriodID.Contains(p.id) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(p => p.Tax_Period).ToList();

            if (listTaxPeriodID.Count != 0)
            {
                var maxDate = listTaxPeriodID.Select(s => s.Tax_Period).Max();
                var minDate = maxDate.AddMonths(-24);

                var listTaxPeriodYear = listTaxPeriodID.Where(p => p.Tax_Period > minDate && p.Tax_Period <= maxDate).ToList();
                foreach (var item in listTaxPeriodYear)
                {
                    listTaxCalculation.Add(new SelectListItem
                    {
                        Text = string.Format("{0:MMMM yyyy}", item.Tax_Period),
                        Value = item.id.ToString()
                    });
                }
            }
            return listTaxCalculation;
        }
        #endregion

        #region LIST_Employement_Status
        public static List<SelectListItem> employment_Status(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            generalParameterList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL"
            });
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        public static List<SelectListItem> emploeyeementStatus(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }

        #region LIST_Calculation_Run
        public static List<SelectListItem> listCalculationRun(string selectedPayrollPeriod, string selectedEmploymentStatus)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListDepartment = new List<SelectListItem>();

            if (selectedPayrollPeriod == "" || selectedPayrollPeriod == null || selectedEmploymentStatus == "" || selectedEmploymentStatus == null)
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = "ALL",
                    Value = "ALL"
                });
            }
            else
            {
                if (selectedEmploymentStatus == "ALL")
                {
                    var listCalculationRunAll = GeneralCore.PayrollCalculation().Where(p => p.Payroll_Period_ID.ToString() == selectedPayrollPeriod && p.Calculation_Type.ToLower() != "severance" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).GroupBy(g => g.Run).Select(x => new { run = x.FirstOrDefault().Run }).ToList();

                    ListDepartment.Add(new SelectListItem
                    {
                        Text = "ALL",
                        Value = "ALL"
                    });

                    if (listCalculationRunAll.Count != 0)
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = "ALL",
                            Value = "ALL"
                        });
                    }

                    foreach (var item in listCalculationRunAll.OrderBy(o => o.run))
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = item.run.ToString(),
                            Value = item.run.ToString()
                        });
                    }
                }
                else
                {
                    var listCalculationRunSelected = GeneralCore.PayrollCalculation().Where(p => p.Payroll_Period_ID.ToString() == selectedPayrollPeriod && p.Calculation_Type.ToLower() == selectedEmploymentStatus.ToLower() && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).GroupBy(g => g.Run).Select(x => new { run = x.FirstOrDefault().Run }).ToList();

                    ListDepartment.Add(new SelectListItem
                    {
                        Text = "ALL",
                        Value = "ALL"
                    });

                    if (listCalculationRunSelected.Count != 0)
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = "ALL",
                            Value = "ALL"
                        });
                    }

                    foreach (var item in listCalculationRunSelected.OrderBy(o => o.run))
                    {
                        ListDepartment.Add(new SelectListItem
                        {
                            Text = item.run.ToString(),
                            Value = item.run.ToString()
                        });
                    }
                }
            }
            return ListDepartment;
        }
        #endregion

        #endregion

        #region PayslipTemplate
        public static List<SelectListItem> Report_Name()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var PayslipSetting = db.tbl_Payslip_Setting.Where(p => p.Organization_ID == UserData.OrganizationSelected.id).Distinct().Select(s => s.Payslip_ID).ToList();
            var PayslipTemplate = db.tbl_Payslip_Template.Where(p => p.Is_Default == 1).Distinct().Select(s => s.ID).ToList();
            var UnionPayslip = (from s in PayslipSetting select s).Union(from s in PayslipTemplate select s);

            //var ModelNew = db.tbl_Payslip_Setting.Where(r => r.Organization_ID == UserData.OrganizationSelected.id).Distinct().Select(s => s.Payslip_ID).ToList();        
            foreach (var item in db.tbl_Payslip_Template.Where(p => UnionPayslip.Contains(p.ID)).OrderBy(s => s.Report_Name).Distinct())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Report_Name + " - " + item.Paper_Size,
                    Value = item.ID.ToString()
                });
            }
            return generalParameterList;
        }
        #endregion

        #region LIST_FIELD_tbl_SysParam
        public static List<SelectListItem> fieldTblSysParam()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Param Code", Value = "Param_Code" });
            listItems.Add(new SelectListItem { Text = "Value", Value = "Value" });
            listItems.Add(new SelectListItem { Text = "Description", Value = "Description" });
            listItems.Add(new SelectListItem { Text = "Update By", Value = "Update_By" });
            return listItems;
        }
        #endregion


        public static List<SelectListItem> organization_structure_value(Guid OrganizationID, string structurevalue)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            List<tbl_Organization_Structure> Model = new List<tbl_Organization_Structure>();
            Model = db.tbl_Organization_Structure.Where(p => !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();

            if (!string.IsNullOrEmpty(structurevalue))
            {
                Model = Model.Where(p => p.Organization_ID == OrganizationID && p.Struktur == structurevalue.Replace("\n","")).ToList();
                foreach (var item in Model)
                {
                    List.Add(new SelectListItem
                    {
                        Text = item.Code,
                        Value = item.Code
                    });
                }
            }
            else
            {
                Model = db.tbl_Organization_Structure.Where(p => p.Organization_ID == OrganizationID).ToList();
                foreach (var item in Model)
                {
                    List.Add(new SelectListItem
                    {
                        Text = item.Code,
                        Value = item.Code
                    });
                }
            }
            return List;
        }

        public static SelectList Employee_organization_structure(Guid OrganizationID, string structuredata, DateTime FilterFrom, DateTime FilterTo)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<List_Structure_value> ListEmployee = new List<List_Structure_value>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            List<tbl_Appointment_Status_Information> listAppointmenStatusInformation = new List<tbl_Appointment_Status_Information>();
            List<vw_Employee_Appointment_Status> vw_employee_appointmemnt_Status_Information = new List<vw_Employee_Appointment_Status>();

            try
            {
                //bukan delete authorize
                var listemployee = GeneralCore.PersonalInformationQuery();

                var employeeexist = listemployee.ToList();
                List<Guid> listemployeeID = listemployee.Select(p => p.id).ToList();

                var appointmenexist = db.tbl_Employee_Appointment.Where(p => listemployeeID.Contains(p.Employee_ID)).ToList();
                List<Guid> listemployeeAppointmenID = appointmenexist.Where(p => listemployeeID.Contains(p.Employee_ID)).Select(s => s.id).ToList();

                listAppointmenStatusInformation = db.tbl_Appointment_Status_Information.Where(p => listemployeeAppointmenID.Contains(p.Appointment_Id.Value)).ToList();
                
                #region join
                var employeeAppointmemntStatus = (  from stat in listAppointmenStatusInformation
                                                    join app in appointmenexist on stat.Appointment_Id equals app.id
                                                    join emp in employeeexist on app.Employee_ID equals emp.id
                                                    select new vw_Employee_Appointment_Status
                                                    {

                                                        id = app.id,
                                                        Employee_ID = emp.id,
                                                        Effective_Date = stat.Effective_Date,
                                                        Employee_No = emp.Employee_No,
                                                        Employee_Status = stat.Employee_Status,
                                                        Employment_Status = stat.Employment_Status,
                                                        Full_Name = emp.Full_Name,
                                                        Organization_ID = emp.Organization_ID,
                                                        Status = stat.Employment_Status.Split('-').First().Trim()

                                                    }).OrderByDescending(o=>o.Effective_Date).ToList();
                #endregion

                #region filter 
                var listEmpID = employeeAppointmemntStatus.Select(p => p.Employee_ID).Distinct().ToList();
                foreach (var empID in listEmpID)
                {
                    var data = employeeAppointmemntStatus.Where(p => p.Employee_ID == empID).ToList();
                
                    //multiple appointment
                    if (data.Count() > 1)
                    {
                        var lastStatusEmp = data.OrderByDescending(o => o.Effective_Date).FirstOrDefault();
                        if ((lastStatusEmp.Employee_Status == "Active" || lastStatusEmp.Employee_Status == "Leave Of Absent (LOA)"))
                        {
                            if (lastStatusEmp.Effective_Date <= FilterTo && lastStatusEmp.Employment_Status != "Non Permanent - Mantan Pegawai")
                                vw_employee_appointmemnt_Status_Information.Add(lastStatusEmp);
                        }
                        else
                        {
                            if (lastStatusEmp.Effective_Date >= FilterFrom)
                                vw_employee_appointmemnt_Status_Information.Add(lastStatusEmp);
                        }
                    }

                    //single appointment
                    else
                    {
                        var lastStatusEmp = data.FirstOrDefault();
                        if ((lastStatusEmp.Employee_Status == "Active" || lastStatusEmp.Employee_Status == "Leave Of Absent (LOA)"))
                        {
                            if (lastStatusEmp.Effective_Date <= FilterTo && lastStatusEmp.Employment_Status != "Non Permanent - Mantan Pegawai")
                                vw_employee_appointmemnt_Status_Information.Add(lastStatusEmp);
                        }
                        else
                        {
                            if (lastStatusEmp.Effective_Date >= FilterFrom)
                                vw_employee_appointmemnt_Status_Information.Add(lastStatusEmp);
                        }
                    }
                }
                #endregion
                
                List<string> EmployeeId = vw_employee_appointmemnt_Status_Information.Select(p => p.Employee_ID.ToString()).ToList();
                var modelAppointment = db.tbl_Employee_Appointment.Where(p => EmployeeId.Contains(p.Employee_ID.ToString())).ToList();

                List<string> AppointmentID = modelAppointment.Select(p => p.id.ToString()).ToList();
                var Position = db.tbl_Appointment_Position.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
                var Divisions = db.tbl_Appointment_Division.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
                var Grades = db.tbl_Appointment_Grade.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
                var Departments = db.tbl_Appointment_Department.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();

                var StructureData = structuredata.Split('|');

                string structure = StructureData[0];
                string Structurevalue = StructureData[1];

                if (structure == "All")
                {
                    var all = modelAppointment.Select(p => p.Employee_ID).ToList();
                    var alldata = db.tbl_Employee.Where(p => all.Contains(p.id)).ToList();

                    foreach (var item in alldata)
                    {
                        ListEmployee.Add(new List_Structure_value
                        {
                            Text = item.Employee_No + " - " + item.Full_Name,
                            Value = item.id.ToString()
                        });
                    }
                }

                else if (structure == "Positions")
                {
                    var Appointment = Position.Where(p => p.Position_Code == Structurevalue).Select(p => p.Appointment_Id).ToList();
                    var Employee = db.tbl_Employee_Appointment.Where(p => Appointment.Contains(p.id)).Select(p => p.Employee_ID).ToList();
                    var model = db.tbl_Employee.Where(p => Employee.Contains(p.id)).ToList();
                    
                    foreach (var item in model)
                    {
                        ListEmployee.Add(new List_Structure_value
                        {
                            Text = item.Employee_No + " - " + item.Full_Name,
                            Value = item.id.ToString()
                        });
                    }

                }

                else if (structure == "Divisions")
                {
                    var Appointment = Divisions.Where(p => p.Division_Code == Structurevalue).Select(p => p.Appointment_Id).ToList();
                    var Employee = db.tbl_Employee_Appointment.Where(p => Appointment.Contains(p.id)).Select(p => p.Employee_ID).ToList();
                    var model = db.tbl_Employee.Where(p => Employee.Contains(p.id)).ToList();

                    foreach (var item in model)
                    {
                        ListEmployee.Add(new List_Structure_value
                        {
                            Text = item.Employee_No + " - " + item.Full_Name,
                            Value = item.id.ToString()
                        });
                    }
                }

                else if (structure == "Grades")
                {
                    var Appointment = Grades.Where(p => p.Grade_Code == Structurevalue).Select(p => p.Appointment_Id).ToList();
                    var Employee = db.tbl_Employee_Appointment.Where(p => Appointment.Contains(p.id)).Select(p => p.Employee_ID).ToList();
                    var model = db.tbl_Employee.Where(p => Employee.Contains(p.id)).ToList();

                    foreach (var item in model)
                    {
                        ListEmployee.Add(new List_Structure_value
                        {
                            Text = item.Employee_No + " - " + item.Full_Name,
                            Value = item.id.ToString()
                        });
                    }

                }

                else if (structure == "Departments")
                {
                    var Appointment = Departments.Where(p => p.Department_Code == Structurevalue).Select(p => p.Appointment_Id).ToList();
                    var Employee = db.tbl_Employee_Appointment.Where(p => Appointment.Contains(p.id)).Select(p => p.Employee_ID).ToList();
                    var model = db.tbl_Employee.Where(p => Employee.Contains(p.id)).ToList();

                    foreach (var item in model)
                    {
                        ListEmployee.Add(new List_Structure_value
                        {
                            Text = item.Employee_No + " - " + item.Full_Name,
                            Value = item.id.ToString()
                        });
                    }

                }

                var Lists = new SelectList(ListEmployee.GroupBy(x => x.Value).Select(x => x.First()), "Value", "Text");

                return Lists;
            }

            catch (Exception ex)
            {
                UIException.LogException(ex, "UIselectlist.Employee_organization_structure");
                var Lists = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem { Selected = true, Text = "No Data Employee", Value = ""},
                    }, "Value", "Text");

                return Lists;
            }
            
        }
        public static SelectList Employee_organization_structureA(Guid OrganizationID, string structureA)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List = new List<SelectListItem>();
            List<List_Structure_value> ListEmployee = new List<List_Structure_value>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            
            var Model = db.tbl_Employee.Where(p => p.Organization_ID == OrganizationID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
            List<string> EmployeeId = Model.Select(p => p.id.ToString()).ToList();
            var modelAppointment = db.tbl_Employee_Appointment.Where(p => EmployeeId.Contains(p.Employee_ID.ToString())).ToList();
            List<string> AppointmentID = modelAppointment.Select(p => p.id.ToString()).ToList();
            var Position = db.tbl_Appointment_Position.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
            var Divisions = db.tbl_Appointment_Division.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
            var Grades = db.tbl_Appointment_Grade.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();
            var Departments = db.tbl_Appointment_Department.Where(p => AppointmentID.Contains(p.Appointment_Id.ToString())).ToList();

            //if (structure == "All") // set structurevalue jadi all jika structur all
            //{
            //    Structurevalue = "All";
            //}

            //if (structure == "All" && Structurevalue == "All")
            //{
              
            //    foreach (var item in modelAppointment)
            //    {
            //        ListEmployee.Add(new List_Structure_value
            //        {
            //            Text = item.tbl_Employee.Employee_No + " - " + item.tbl_Employee.Full_Name,
            //            Value = item.tbl_Employee.id.ToString()
            //        });
            //    }

            //}

            //else if (structure != "All")
            //    {
            //        if (structure == "Positions")
            //        {
            //            if (Structurevalue == "All")
            //            {
            //                foreach (var item in Position)
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }

            //            else
            //            {
            //                foreach (var item in Position.Where(p => p.Position_Code == Structurevalue))
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }
            //        }

            //        if (structure == "Divisions")
            //        {
            //            if (Structurevalue == "All")
            //            {
            //                foreach (var item in Divisions)
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }

            //            else
            //            {
            //                foreach (var item in Divisions.Where(p=>p.Division_Code == Structurevalue))
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }
            //        }

            //        if (structure == "Grades")
            //        {
            //            if (Structurevalue == "All")
            //            {
            //                foreach (var item in Grades)
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }

            //            else
            //            {
            //                foreach (var item in Grades.Where(p=>p.Grade_Code == Structurevalue))
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }
            //        }

            //        if (structure == "Departments")
            //        {
            //            if (Structurevalue == "All")
            //            {
            //                foreach (var item in Departments)
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }

            //            else
            //            {
            //                foreach (var item in Departments.Where(p=>p.Department_Code == Structurevalue))
            //                {
            //                ListEmployee.Add(new List_Structure_value
            //                    {
            //                        Text = item.tbl_Employee_Appointment.tbl_Employee.Employee_No + " - " + item.tbl_Employee_Appointment.tbl_Employee.Full_Name,
            //                        Value = item.tbl_Employee_Appointment.tbl_Employee.id.ToString()
            //                    });
            //                }
            //            }

            //        }
                
            //    }
            
            var Lists = new SelectList(ListEmployee.GroupBy(x=>x.Value).Select(x=>x.First()), "Value", "Text");
            return Lists;
        }
		
	    #region Organization Group
        public static List<SelectListItem> Organization_Group()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var ModelNew = db.tbl_Organization_Group_Detail.Where(r => r.Organization_Client_ID == UserData.OrganizationSelected.id && r.Relationship_Type == "HOLDING").Distinct().Select(s => s.Organization_Group_ID).ToList();

            generalParameterList.Add(new SelectListItem
            {
                Text = UserData.OrganizationSelectedName,
                Value = UserData.OrganizationSelected_Id.ToString()
            });

            foreach (var item in db.tbl_Organization_Group.Where(p => ModelNew.Contains(p.id) && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED).OrderBy(s => s.Group_Code).Distinct())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Group_Description,
                    Value = item.id.ToString()
                });
            }
            return generalParameterList;
        }
        #endregion
		
		#region LIST_Employee_Deduction_Code_Name
        public static List<SelectListItem> SelectList_Employee_Deduction_Code_Name(Guid orgSelectedID)
        {
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            var listLoanEmp = GeneralCore.vwLoanQuery().Where(p => p.Organization_ID == orgSelectedID && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).GroupBy(g => g.Employee).Select(x => new { x.FirstOrDefault().Employee, x.FirstOrDefault().Employee_No, x.FirstOrDefault().Full_Name }).ToList();
            foreach (var item in listLoanEmp.OrderBy(o=> o.Employee_No))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Employee_No + " - " + item.Full_Name,
                    Value = item.Employee.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Employee_Deduction_Loan_No
        public static List<SelectListItem> SelectList_Employee_Deduction_Loan_No(string empSelectedID)
        {
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            foreach (var item in GeneralCore.vwLoanQuery().Where(p => p.Employee.ToString() == empSelectedID && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Loan_No))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Loan_No,
                    Value = item.Component_Linkage_ID.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region LIST_Type_Download_Pdf_Excel_Xlsx
        public static List<SelectListItem> SelectList_Type_Download_Pdf_Excel_Xlsx(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            List<string> listTypeDownload = new List<string>();
            List<tbl_General_Parameter> listTypeDownlod = new List<tbl_General_Parameter>();
            listTypeDownlod = GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq).ToList();
            foreach (var item in listTypeDownlod.Where(s => s.Field_Name.ToLower() == "pdf" || s.Field_Name.ToLower() == "excel" || s.Field_Name.ToLower() == "xlsx"))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Name.ToUpper(),
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region LIST_Employee_Code_Loan
        public static List<SelectListItem> SelectList_Employee_Code_Loan(DateTime? periodStartDate)
        {
            List<SelectListItem> ListDepartment = new List<SelectListItem>();
            List<Guid> Employee_Id = GeneralCore.AppointmentInformationStatusByOrganization().Where(p => (!p.Employee_Status.Contains("Active") || p.Employment_Status.Contains("Non Permanent")) && p.Effective_Date <= periodStartDate).Select(p => p.tbl_Employee_Appointment.Employee_ID).ToList();
            List<tbl_Employee> ListEmployee = new List<tbl_Employee>();
            ListEmployee = GeneralCore.EmployeeActiveAuthorizedQuery().Where(p => !Employee_Id.Contains(p.id)).ToList();

            foreach (var item in ListEmployee.OrderBy(s => s.Full_Name))
            {
                ListDepartment.Add(new SelectListItem
                {
                    Text = item.Employee_No + " - " + item.Full_Name,
                    Value = item.id.ToString()
                });
            }
            return ListDepartment;
        }
        #endregion

        #region FormatTypeA1
        public static List<SelectListItem> Format_Type_A1()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == "REPORT_FORMAT" && (s.Field_Name == "PDF" || s.Field_Name == "CSV")).OrderBy(s => s.Field_Seq))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region LIST_FIELD_tbl_Employee_Approval
        public static List<SelectListItem> fieldTblEmployeeApproval()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Employee ID Number", Value = "Employee_ID_Number" });
            listItems.Add(new SelectListItem { Text = "Full Name", Value = "Full_Name" });
            listItems.Add(new SelectListItem { Text = "Supervisor ID Number", Value = "Supervisor_ID_Number" });
            listItems.Add(new SelectListItem { Text = "Supervisor Name", Value = "Supervisor_Name" });
            listItems.Add(new SelectListItem { Text = "Remarks", Value = "Remarks" });
            listItems.Add(new SelectListItem { Text = "Record Status", Value = "Record_Status" });
            listItems.Add(new SelectListItem { Text = "Authorize Status", Value = "Authorize_Status" });
            return listItems;
        }
        #endregion

        public static List<SelectListItem> SelectList_Position_WLKP(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq))
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
		
				#region LIST_Category_Blog_Menu
        public static List<SelectListItem> SelectList_Category_Blog_Menu()
        {
            List<SelectListItem> generalCategoryBlogMenu = new List<SelectListItem>();
            foreach (var item in GeneralCore.CategoryBlogMenuQuery())
            {

                generalCategoryBlogMenu.Add(new SelectListItem
                {
                    Text = item.Menu_Name,
                    Value = item.id.ToString()
                });
            }
            return generalCategoryBlogMenu;
        }
        #endregion

        #region Transfer Type Online Payment
        public static List<SelectListItem> TransferTypeOnlinePayment()
        {
            List<SelectListItem> ListTransferType = new List<SelectListItem>();
            var Data = GeneralCore.GeneralParameterQuery().Where(p => p.Table_Name == "TRANSFER_TYPE" && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s=>s.Field_Seq).ToList();
            foreach (var item in Data)
            {
                ListTransferType.Add(new SelectListItem
                {
                    Text = item.Field_Name,
                    Value = item.Field_Value
                });
            }
            return ListTransferType;
        }
        public static List<SelectListItem> EmployeeBankPayrollPayment(Guid Organization_ID, List<string> PayrollSummary)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> EmployeeBankList = new List<SelectListItem>();

            var SysParam = db.tbl_SysParam.Where(p => p.Param_Code == "Online_Payment_Source_Of_Bank").Select(s => s.Value).ToList();
            var ListOfBank = db.tbl_List_Of_Bank.Where(p => SysParam.Contains(p.Bank_Code) && p.Online_Payment == true && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(s => s.Bank_Code).FirstOrDefault();
            var bank = db.tbl_Bank_Information.Where(p => p.Bank == ListOfBank && p.Organization_ID == Organization_ID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(s => s.id).ToList();

            var employeeBank = db.tbl_Employee_Payment.Where(p => bank.Contains(p.Payment_Bank_ID.Value) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED))
                .Select(s => new
                {
                    Employee_ID = s.Employee_ID,
                    BI_Bank_Code = s.tbl_List_Of_Bank.BI_Bank_Code,
                    Bank_Code = s.tbl_List_Of_Bank.Bank_Code
                }).Distinct().ToList();

            if (PayrollSummary.Count() > 0)
            {
                var SummarryEmpID = db.vw_Payroll_Payment_Summary.Where(p => PayrollSummary.Contains(p.Id.ToString())).Select(s => s.Employee_ID).Distinct().ToList();
                if (SummarryEmpID.Count() > 0)
                {
                    employeeBank = employeeBank.Where(p => SummarryEmpID.Contains(p.Employee_ID)).ToList();
                    var EmpBank = employeeBank.Select(i => new { i.BI_Bank_Code, i.Bank_Code }).Distinct().ToList();
                    foreach (var Bank in EmpBank)
                    {
                        EmployeeBankList.Add(new SelectListItem
                        {
                            Text = Bank.BI_Bank_Code + "-" + Bank.Bank_Code,
                            Value = Bank.Bank_Code.ToString()
                        });
                    }
                }
            }
            //else
            //{
            //    var EmpBank = employeeBank.Select(i => new { i.BI_Bank_Code, i.Bank_Code }).Distinct().ToList();
            //    foreach (var Bank in EmpBank)
            //    {
            //        EmployeeBankList.Add(new SelectListItem
            //        {
            //            Text = Bank.BI_Bank_Code + "-" + Bank.Bank_Code,
            //            Value = Bank.Bank_Code.ToString()
            //        });
            //    }
            //}

            return EmployeeBankList;
        }
        public static List<SelectListItem> SourcheAccountBankPayrollPayment(Guid Organization_ID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> BankList = new List<SelectListItem>();
            var SysParam = db.tbl_SysParam.Where(p => p.Param_Code == "Online_Payment_Source_Of_Bank").Select(s => s.Value).ToList();
            var ListOfBank = db.tbl_List_Of_Bank.Where(p => SysParam.Contains(p.Bank_Code) && p.Online_Payment == true && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(s => s.Bank_Code).FirstOrDefault();

            if (ListOfBank !=null)
            {
                var query = db.tbl_Bank_Information.Where(p => p.Bank == ListOfBank && p.Organization_ID == Organization_ID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();

                if (Organization_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    BankList.Add(new SelectListItem
                    {
                        Value = "",
                        Text = "Select..",
                    });
                }
                else
                {
                    foreach (var item in query)
                    {
                        if (item != null)
                        {
                            BankList.Add(new SelectListItem
                            {
                                Value = item.id.ToString(),
                                Text = item.Bank + " - " + item.Account_Number,
                            });
                        }
                    }
                }
            }
            
            return BankList;
        }
        public static List<SelectListItem> SelectList_Employee_Payroll_Period_Generate_Online_Payment(Guid? PeriodID, Guid? OrgID, int Run, string[] EmployeeBankID, Guid? BankID, string Debit_Account_Number)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<vw_Payroll_Payment_Summary> ListEmployee = new List<vw_Payroll_Payment_Summary>();
            List<SelectListItem> List_EmployeePayroll_Period = new List<SelectListItem>();

            var ListBankInfo = db.tbl_Bank_Information.Where(p => p.id == BankID && p.Organization_ID == OrgID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();
            var EmpBankCode = ListBankInfo.Select(s => s.Bank).ToList();
            var SourceBankID = ListBankInfo.Select(s => s.id).ToList();

            var DataTransfer = db.vw_Trx_Bank_Transfer.Where(p => p.Organization_ID == OrgID && (p.Payroll_Period_ID == PeriodID && p.Debit_Account_Number == Debit_Account_Number && (p.Employee_Status_Transfer == GlobalVariable.CONST_GOP_PAYMENT_PER_ACCOUNT_PENDING_SUBMIT || p.Employee_Status_Transfer == GlobalVariable.CONST_GOP_PAYMENT_PER_ACCOUNT_SUBMIT || p.Employee_Status_Transfer == GlobalVariable.CONST_GOP_PAYMENT_PER_ACCOUNT_SUCCESS_TRANSFER))).Select(i => new { i.Payroll_Period_ID, i.Employee_Account_Name, i.Employee_Bank_Account, i.Employee_Bank_Code });
            //var TempPeriodID = DataTransfer.Select(s => s.Payroll_Period_ID).ToList();
            var TempEmpBankCode = DataTransfer.Select(s => s.Employee_Bank_Code).ToList();
            var TempEmpAccount = DataTransfer.Select(s => s.Employee_Bank_Account).ToList();

            if (DataTransfer.ToList().Count() > 0)
            {
                if (EmployeeBankID[0].ToString() != "")
                {
                    ListEmployee = db.vw_Payroll_Payment_Summary.Where(p => p.Payroll_Period_ID == PeriodID && p.Organization_ID == OrgID && p.Run == Run && EmpBankCode.Contains(p.Payment_Bank_Code) && EmployeeBankID.Contains(p.Employee_Bank_Code) && !(TempEmpBankCode.Contains(p.Employee_Bank_Code) && TempEmpAccount.Contains(p.Employee_Bank_Account))).ToList();
                }
                else
                {
                    ListEmployee = db.vw_Payroll_Payment_Summary.Where(p => p.Payroll_Period_ID == PeriodID && p.Organization_ID == OrgID && p.Run == Run && EmpBankCode.Contains(p.Payment_Bank_Code) && !(TempEmpBankCode.Contains(p.Employee_Bank_Code) && TempEmpAccount.Contains(p.Employee_Bank_Account))).ToList();
                } 
            }
            else
            {
                if (EmployeeBankID[0].ToString() != "")
                {
                    ListEmployee = db.vw_Payroll_Payment_Summary.Where(p => p.Payroll_Period_ID == PeriodID && p.Organization_ID == OrgID && p.Run == Run && EmpBankCode.Contains(p.Payment_Bank_Code) && EmployeeBankID.Contains(p.Employee_Bank_Code)).ToList();
                }
                else
                {
                    ListEmployee = db.vw_Payroll_Payment_Summary.Where(p => p.Payroll_Period_ID == PeriodID && p.Organization_ID == OrgID && p.Run == Run && EmpBankCode.Contains(p.Payment_Bank_Code)).ToList();
                }
            }

            var EmpPaymentBank = db.tbl_Employee_Payment.Where(p => SourceBankID.Contains(p.Payment_Bank_ID.Value) && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(s => s.Employee_ID).ToList();

            ListEmployee = ListEmployee.Where(p => EmpPaymentBank.Contains(p.Employee_ID.Value)).ToList();

            if (ListEmployee.Count() > 0)
            {
                foreach (var item in ListEmployee)
                {
                    string[] DataEmployee = item.Title.Split('|').ToArray();
                    List_EmployeePayroll_Period.Add(new SelectListItem
                    {
                        Text = DataEmployee[2] + "|" + DataEmployee[3] + "|" + DataEmployee[4] + "|" + DataEmployee[5] + "|" + DataEmployee[6], //+ "|" + DataEmployee[5],
                        Value = item.Id.ToString(),
                    });
                }
            }
            return List_EmployeePayroll_Period;
        }
        public static List<SelectListItem> PayrollPeriod_GOP(Guid OrgID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> List = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var LPeriodID = db.vw_Payroll_Payment_Details.Where(r => r.Organization_ID == OrgID).Select(p => p.Payroll_Period_ID).Distinct().ToList();
            var ModelNew = from s in db.vw_Payroll_Period.Where(p => LPeriodID.Contains(p.id)).Distinct()
                           select new
                           {
                               s.id,
                               s.Period_Start_Date,
                               s.Period_End_Date,
                               s.Tax_Period
                           };
            var a = ModelNew.Distinct();
            foreach (var item in ModelNew.OrderBy(s => s.Period_Start_Date))
            {
                List.Add(new SelectListItem
                {
                    Text = item.Tax_Period.ToString("dd/MM/yyyy") + " | " + item.Period_Start_Date.ToString("dd/MM/yyyy") + "-" + item.Period_End_Date.ToString("dd/MM/yyyy"),
                    Value = item.id.ToString()
                });
            }
            return List;
        }
        #endregion
		
		#region LIST_Type_Download_Pdf_Excel
        public static List<SelectListItem> List_Send_Email_Type(string tblName)
        {
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            List<tbl_General_Parameter> listSendEmailType = new List<tbl_General_Parameter>();
            listSendEmailType = GeneralCore.GeneralParameterQuery().Where(s => s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && s.Table_Name == tblName && s.Status_Code == 1).OrderBy(s => s.Field_Seq).ToList();
            foreach (var item in listSendEmailType)
            {

                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Name.ToLower(),
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion

        #region List_Price_Package_Midtrans
        public static List<SelectListItem> List_Price_Package_Midtrans(string paramPackageSelected)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ListPackage = new List<SelectListItem>();
            foreach (var item in db.tbl_Package_Pricing.OrderBy(s => s.Field_Seq).ToList())
            {
                ListPackage.Add(new SelectListItem
                {
                    Text = item.Package_Name,
                    Value = item.Value.ToString(),
                });
            }
            return ListPackage;
        }
        #endregion

        #region Dropdownlist Payroll Variable Report
        #region ReportVariablePayrollPeriod
        public static List<SelectListItem> Report_Variable_Payroll_Period(Guid orgSelected)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            List<SelectListItem> ListSelected = new List<SelectListItem>();
            foreach (var item in db.vw_Payroll_Period.Where(p => p.Organization_ID == orgSelected && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).OrderBy(s => s.Tax_Period).ThenBy(s => s.Year))
            {
                ListSelected.Add(new SelectListItem
                {
                    Text = string.Format("{0:dd/MM/yyyy}", item.Period_Start_Date) + " - " + string.Format("{0:dd/MM/yyyy}", item.Period_End_Date),
                    Value = item.id.ToString()
                });
            }
            return ListSelected;
        }
        #endregion ReportVariablePayrollPeriod

        #region ReportVariableEmployeeStatus
        public static List<SelectListItem> Report_Variable_Employee_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYEESTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion ReportVariableEmployeeStatus

        #region ReportVariableEmployementStatus
        public static List<SelectListItem> Report_Variable_Employement_Status()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            foreach (var item in GeneralCore.GeneralParameterQuery().Where(r => r.Table_Name == "EMPLOYMENTSTATUS" && r.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && r.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Distinct().OrderBy(s => s.Field_Seq).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Field_Value,
                    Value = item.Field_Value
                });
            }
            return generalParameterList;
        }
        #endregion ReportVariableEmployementStatus
        #endregion Dropdownlist Payroll Variable Report
        public static List<SelectListItem> ListComponentEffectiveMobile()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> ComponentList = new List<SelectListItem>();
            var SysComp = db.tbl_SysParam.Where(s => s.Param_Code == "FILTER_COMPONENT_MOBILE").FirstOrDefault().Value.Split('|');
            var CompOrganization = GeneralCore.OrganizationPayrollComponentQuery().Where(p => SysComp.Contains(p.Component_Group)).ToList();
            foreach (var comp in CompOrganization)
            {
                ComponentList.Add(new SelectListItem
                {
                    Text = comp.Description + " (" + comp.Organization_Payroll_Component_Code + ")",
                    Value = comp.Organization_Payroll_Component_Code
                });
            }
            return ComponentList;
        }

        #region LIST_FIELD_tbl_Attendance_Summary_Upload
        public static List<SelectListItem> fieldTblAttendanceSummarySync()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Cutoff Attendance", Value = "Cut_Off_Time_Start" });
            listItems.Add(new SelectListItem { Text = "Last Upload Number", Value = "Last_Upload_Number" });
            listItems.Add(new SelectListItem { Text = "Last Uploaded By", Value = "Last_Uploaded_By" });
            listItems.Add(new SelectListItem { Text = "Last Upload Date", Value = "Last_Uploaded_Datetime" });
            listItems.Add(new SelectListItem { Text = "Status", Value = "Last_Synced_Status" });
            return listItems;
        }

        public static List<SelectListItem> fieldTblAttendanceDetailSync()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "All", Value = "" });
            listItems.Add(new SelectListItem { Text = "Finger ID", Value = "Finger_ID" });
            listItems.Add(new SelectListItem { Text = "Date", Value = "Date" });
            listItems.Add(new SelectListItem { Text = "Employee ID", Value = "Employee_No" });
            listItems.Add(new SelectListItem { Text = "Employee Name", Value = "Full_Name" });
            listItems.Add(new SelectListItem { Text = "Code", Value = "Code" });
            return listItems;
        }
        #endregion

        #region AttendanceSynchronization_TypeDevice
        public static List<SelectListItem> TypeDevice()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var DeviceOrg = db.tbl_Attendance_Mapping_DeviceOrg.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id).Select(s => s.Attendance_Device_ID).ToList();

            foreach (var item in db.tbl_Attendance_Device.Where(p => DeviceOrg.Contains(p.id)).OrderBy(o => o.Device_Finger).ToList())
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Device_Finger,
                    Value = item.id.ToString()
                });
            }
            return generalParameterList;
        }

        public static List<SelectListItem> CutOffTime()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();

            var CutOffPeriod = db.tbl_Cut_Off_Period.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id).ToList();

            //List<string> dt = new List<string>();
            //var dtMonth = DateTime.Now.Month;
            //var dtYear = DateTime.Now.Year;
            //for (int start_month = 1; start_month <= dtMonth; start_month++)
            //{
            //    DateTime dtStart = new DateTime();
            //    DateTime dtEnd = new DateTime();
            //    dtStart = Convert.ToDateTime(start_month + "/" + DeviceOrg.Cut_Off_Time_Start + "/" + dtYear);
            //    dtEnd = Convert.ToDateTime((start_month + 1) + "/" + DeviceOrg.Cut_Off_Time_End + "/" + dtYear);

            //    var start = dtStart.ToString("dd/MM/yyyy");
            //    var end = dtEnd.ToString("dd/MM/yyyy");
            //    var StartEnd = start + " - " + end;
            //    dt.Add(StartEnd);
            //}
            foreach (var item in CutOffPeriod.Distinct().OrderBy(o => o.Cut_Off_Start))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Cut_Off_Start.Value.ToString("dd/MM/yyyy") + " - " + item.Cut_Off_End.Value.ToString("dd/MM/yyyy"),
                    Value = item.id.ToString()
                });
            }

            return generalParameterList;
        }
		public static List<SelectListItem> CutOffTime(Guid OrganizationID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> generalParameterList = new List<SelectListItem>();
            CoreVariable CoreUserVariable = new CoreVariable();
            User_Data UserData = CoreUserVariable.CoreUserLogin();
            var DeviceOrg = db.tbl_Cut_Off_Period.Where(p => p.Organization_ID == OrganizationID).ToList();
            foreach (var item in DeviceOrg.Distinct().OrderBy(o => o.Cut_Off_Start))
            {
                generalParameterList.Add(new SelectListItem
                {
                    Text = item.Cut_Off_Start.Value.ToString("dd/MM/yyyy") + " - " + item.Cut_Off_End.Value.ToString("dd/MM/yyyy"),
                    Value = item.id.ToString(),
                });
            }

            return generalParameterList;
        }

        #endregion

        #region List Region Phone
        public static List<SelectListItem> ListPhoneRegion()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<SelectListItem> BankList = new List<SelectListItem>();
            var query = GeneralCore.GeneralParameterQuery().Where(p => p.Table_Name == "RegionPhone").Select(s => s.Field_Value).FirstOrDefault().Split('|');
                foreach (var item in query)
                {
                    if (item != null)
                    {
                        BankList.Add(new SelectListItem
                        {
                            Value = item.ToString(),
                            Text = item.ToString(),
                        });
                    }
                }
            return BankList;
        }
        #endregion
        #region
        public static List<SelectListItem> ListEmployeeDetailCalculation(Guid? Calculation_id)
        {
            List<SelectListItem> EmployeeList = new List<SelectListItem>();
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var ListEmployee = db.tbl_Payroll_Calculation_Employee.Where(p => p.Payroll_Calculation_ID == Calculation_id).ToList();
            foreach (var temp in ListEmployee)
            {
                EmployeeList.Add(new SelectListItem
                {
                    Text = temp.tbl_Employee.Full_Name +" - "+ temp.tbl_Employee.Employee_No,
                    Value = temp.Employee_ID.ToString()
                });
            }
            return EmployeeList;
        }
        #endregion
    }
}

