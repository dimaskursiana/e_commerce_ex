using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_MODEL.ModelData
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
        //public System.Guid ID { get; set; }
        //[Display(Name = "Menu Name")]
        //public string MENU_NAME { get; set; }
        //[Display(Name = "Menu Level")]
        //public Nullable<int> MENU_LEVEL { get; set; }
        //[Display(Name = "Menu Position")]
        //public Nullable<int> MENU_POSITION { get; set; }
        //[Display(Name = "Parent Id")]
        //public Nullable<System.Guid> PARENT_ID { get; set; }
        //[Display(Name = "Menu Url")]
        //public string MENU_URL { get; set; }
        //[Display(Name = "Status Code")]
        //public Nullable<int> STATUS_CODE { get; set; }
    }

    public partial class META_VW_EMPLOYEE_LEAVE_ENTITLEMENT_SUMMARY
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_Id { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        [Display(Name = "Leave Type")]
        public string Leave_Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:n1}", ApplyFormatInEditMode = true)]
        public Nullable<float> Entitlement { get; set; }
        [Display(Name = "Period")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_Start { get; set; }
        [Display(Name = "Period End")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_End { get; set; }
        [Display(Name = "Remark")]
        public string Description { get; set; }
        public string Period { get; set; }

    }
    public partial class META_TBL_BASE_LOCATION_HEADER
    {
        public System.Guid id { get; set; }
        [Display(Name = "Organization")]
        public Nullable<System.Guid> Organization_Id { get; set; }
        public string Map_Type { get; set; }
        [Display(Name = "Work Location")]
        public Nullable<System.Guid> Reference_Id { get; set; }
        [Display(Name = "Multiple Time In and Time Out")]
        public bool Is_Multiple { get; set; }
        [Display(Name = "Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorized")]
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        [Display(Name = "Created By")]
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        [Display(Name = "Updated By")]
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        [Display(Name = "Authorized By")]
        public string Authorized_By { get; set; }
        [Display(Name = "Check Out Anywhere")]
        public bool Is_Anywhere_Time_Out { get; set; }
        [Display(Name = "Check In Anywhere")]
        public bool Is_Anywhere_Time_In { get; set; }
    }
    public partial class META_TBL_BASE_LOCATION_DETAIL
    {
        public System.Guid id { get; set; }
        public System.Guid Base_Location_Header_Id { get; set; }
        [Display(Name = "Location Name")]
        public string Location_Name { get; set; }
        [Display(Name = "Latitude")]
        public Nullable<double> Latitude { get; set; }
        [Display(Name = "Longitude")]
        public Nullable<double> Longitude { get; set; }
        [Display(Name = "Range in meter")]
        public Nullable<decimal> Range { get; set; }
        [Display(Name = "Start Date")]
        public Nullable<System.DateTime> Start_Date { get; set; }
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> End_Date { get; set; }
    }
    public partial class META_VW_BASE_LOCATION_LIST
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Organization")]
        public Nullable<System.Guid> Organization_Id { get; set; }
        [Display(Name = "Work Location")]
        public string Work_Location { get; set; }
        [Display(Name = "Single/Multiple Time In and Time Out")]
        public bool Is_Multiple { get; set; }
        [Display(Name = "Check In Anywhere")]
        public bool Is_Anywhere_Time_In { get; set; }
        [Display(Name = "Check Out Anywhere")]
        public bool Is_Anywhere_Time_Out { get; set; }
        [Display(Name = "Map Type")]
        public string Map_Type { get; set; }
        [Display(Name = "Status")]
        public string Status_Code { get; set; }
        [Display(Name = "Authorized")]
        public string Authorize_Status { get; set; }
    }

    public partial class META_TBL_EMPLOYEE_LEAVE_ENTITLEMENT
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_Id { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        [Display(Name = "Leave Type")]
        public string Leave_Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:n1}", ApplyFormatInEditMode = true)]
        public Nullable<float> Entitlement { get; set; }
        [Display(Name = "Period")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_Start { get; set; }
        [Display(Name = "Period End")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_End { get; set; }
        [Display(Name = "Remark")]
        public string Description { get; set; }

    }

    public partial class META_TBL_GENERATE_BANK_FILE
    {
        public System.Guid id { get; set; }
        [Display(Name = "Organization")]
        public Nullable<System.Guid> Organization_ID { get; set; }
        [Display(Name = "Source Account Bank")]
        public string Source_Account_Bank { get; set; }
        [Display(Name = "Debit Account Number")]
        public string Debit_Account_Number { get; set; }
        [Display(Name = "Debit Account Name")]
        public string Debit_Account_Name { get; set; }
        [Display(Name = "Debit Account Address")]
        public string Debit_Account_Address { get; set; }
        [Display(Name = "File Reference")]
        public string File_Reference { get; set; }
         [Display(Name = "Transfer Date")]
        public Nullable<System.DateTime> Transfer_Date { get; set; }
        [Display(Name = "Transfer Message")]
        public string Transfer_Message { get; set; }
        [Display(Name = "Total Employee")]
        public Nullable<int> Total_Employee { get; set; }
        [Display(Name = "Total Amount")]
        public Nullable<decimal> Total_Amount { get; set; }
        [Display(Name = "Output File Name")]
        public string Output_File_Name { get; set; }
        public string Output_File_Type { get; set; }
        public string Currency_Code { get; set; }
        public string Reference { get; set; }
        public string Payment_Set_Code { get; set; }
        public string Debit_Account_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        
    }


    public partial class META_TBL_LEAVE_TYPES
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_Id { get; set; }
        [Display(Name = "Code")]
        public string Leave_Code { get; set; }
        [Display(Name = "Description")]
        public string Leave_Description { get; set; }
        [Display(Name = "Entitlement Situational")]
        public Nullable<bool> Is_Entitlement_Situational { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
    }

    public partial class META_TBL_EXCHANGE_FLIE
    {
        public System.Guid id { get; set; }
        [Display(Name = "File Name")]
        public string File_Name { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        [Display(Name = "Upload By")]
        public string Created_By { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        public string File_Location { get; set; }
        public string File_Format { get; set; }
        public Nullable<int> File_Size { get; set; }
        [Display(Name = "Referensi Number")]
        public string Ref_Number { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Created_DateTime_Varchar { get; set; }
    }

    public partial class META_TBL_ORGANIZATION_GROUP
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public Nullable<System.Guid> Organization_Team_ID { get; set; }
        [Display(Name = "Code")]
        public string Group_Code { get; set; }
        [Display(Name = "Description")]
        public string Group_Description { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
    }

    public partial class META_TBL_ORGANIZATION_GROUP_DETAIL
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_Group_ID { get; set; }
        [Display(Name = "Organization")]
        public Nullable<System.Guid> Organization_Client_ID { get; set; }
        [Display(Name = "Relation")]
        public string Relationship_Type { get; set; }
    }

    public partial class META_TBL_GENERATE_BANK_FILE_DETAILS
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Generate_Bank_File_ID { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Bank_Code { get; set; }
        public string Employee_Bank_Account { get; set; }
        public Nullable<decimal> Employee_Salary_Transferred { get; set; }
        public string Currency_Code { get; set; }
        public string Account_Name { get; set; }
        public string Branch_Code { get; set; }
    }

    public partial class META_TBL_PAYROLL_CALCULATION
    { 
        public System.Guid id { get; set; }
        [Display(Name = "Payroll Period")]
        public Nullable<System.Guid> Payroll_Period_ID { get; set; } 

        [Display(Name = "Type")] 
        public string Calculation_Type { get; set; }
        [Display(Name = "Run")]
        public Nullable<int> Run { get; set; }
        [Display(Name = "Recalculate on history")]
        public Nullable<bool> Recalculate { get; set; }
        [Display(Name = "Employee")]
        public Nullable<bool> All_Employee { get; set; }
        [Display(Name = "Component")]
        public Nullable<bool> All_Component { get; set; }
         [Display(Name = "Calculation Status")]
        public Nullable<int> Calculate_Status { get; set; }
        [Display(Name = "Status")]
        public Nullable<int> Status_Code { get; set; } 
    }

    public partial class META_TBL_PAYROLL_CLOSING
    {
        public System.Guid id { get; set; }
        [Display(Name = "Calculation Type")]
        public string Calculation_Type { get; set; }
        public Nullable<System.Guid> Calculation_ID { get; set; }
        public string Batch { get; set; }
        [Display(Name = "Prev Batch")]
        public string Prev_Batch { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Pembetulan { get; set; }
    }

    public partial class META_TBL_PAYROLL_TAX_CORRECTION
    {
        public System.Guid ID { get; set; }
        public System.Guid Calculation_ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Tax_Period { get; set; }
        public string Tax_ID { get; set; }
        public Nullable<int> Pembetulan { get; set; }
        public Nullable<int> Run { get; set; }
    }

    public partial class META_TBL_EMPLOYEE_PAYMENT
    {
        public System.Guid id { get; set; }
        public System.Guid Employee_ID { get; set; }
         [Display(Name = "Payment Type")]
        public string Payment_Type_ID { get; set; }
        [Display(Name = "Payment From Bank Account")]
        public Nullable<System.Guid> Payment_Bank_ID { get; set; }
         [Display(Name = "Employee Bank Account")]
        public string Employee_Bank_Account { get; set; }
         [Display(Name = "Bank")]
        public Nullable<System.Guid> Employee_Bank_ID { get; set; }
         [Display(Name = "Bank Branch Code")]
        public Nullable<System.Guid> Employee_Bank_Branch_ID { get; set; }
         [Display(Name = "Currency Code")]
        public string Currency_Code { get; set; }
        public string Account_Name { get; set; }
        public Nullable<double> Percentage { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> Priority { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
         
    }
     
    public class META_TBL_ORGANIZATION_ACCOUNT_GROUP_MAINTENENCE
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Account Number")]
        public string Account_No { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }

    public class META_TBL_ROLE
    {
        public System.Guid id { get; set; }
        [Display(Name = "Role")]
        public string Role_Code { get; set; }
        [Display(Name = "Role Description")]
        public string Role_Description { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 

    }
    public class META_TBL_PAYROLL_VARIABLE
    {
        public System.Guid id { get; set; }
        [Display(Name = "Employee")]
        public Nullable<System.Guid> Employee_ID { get; set; }
         [Display(Name = "Payroll Period")]
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Working_Day { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Overtime_Day { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Absent_Day { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Weekday_Tier1 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Weekday_Tier2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Weekday_Tier3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Holiday_Tier1 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Holiday_Tier2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Holiday_Tier3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> HIS_Tier1 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> HIS_Tier2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> HIS_Tier3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Total_Overtime { get; set; }
        public string Upload_Number { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
        public Nullable<System.Guid> Organization_ID { get; set; }
        
    } 

    public  class META_VW_PAYROLL_VARIABLE
    { 
        public Nullable<System.Guid> Organization_ID { get; set; }
        [Display(Name = "Employee")]
        public Nullable<System.Guid> Employee_ID { get; set; }
        public string Employee_No { get; set; }
        [Display(Name = "Employee Name")]
        public string Full_Name { get; set; }
         [Display(Name = "Payroll Period")]
        public string Payroll_Period { get; set; }
        
         [Display(Name = "Upload No")]
        public string Upload_Number { get; set; }
        [Display(Name = "Record Status")]
        public string Record_Status { get; set; } 
    }
    public class META_TBL_ADDITIONAL_PAYROLL
    {
        public System.Guid id { get; set; }
         [Display(Name = "Employee")]
        public Nullable<System.Guid> Employee_ID { get; set; }
          [Display(Name = "Payroll Period")]
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
         [Display(Name = "Status Code")]
        public Nullable<int> Status_Code { get; set; } 
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Category { get; set; }
    }

    public class META_TBL_ADDITIONAL_PAYROLL_DETAIL
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Additional_Payroll_ID { get; set; }
         [Display(Name = "Component")]
        public string Additional_Component { get; set; }
         [Display(Name = "Currency")]
        public string Additional_Currency { get; set; }
        [Display(Name = "Amount")]
        public Nullable<decimal> Additional_Amount { get; set; }
        [Display(Name = "Remark")]
        public string Additional_Remark { get; set; }
        [Display(Name = "Upload No")]
        public string Additional_Upload_No { get; set; }
        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Additional_Transaction_Date { get; set; }
    }
    public  class META_VW_ADDITIONAL_PAYROLL
    {
        public System.Guid id { get; set; }
         [Display(Name = "Employee ID Number")]
        public Nullable<System.Guid> Employee_ID { get; set; }
         public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Employee_No { get; set; }
        [Display(Name = "Tax Period")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public Nullable<System.DateTime> Tax_Period { get; set; }
        [Display(Name = "Payroll Period")]
        public string Payroll_Period { get; set; }
        [Display(Name = "Employee Name")]
        public string Employee_Name { get; set; }
         [Display(Name = "Record Status")]
        public string Record_Status { get; set; } 
        public string Additional_Remark { get; set; }
        [Display(Name = "Upload Number")]
        public string Additional_Upload_No { get; set; }
    }
	public class META_VW_REPORT_SETTING
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Organization Id")]
        public Nullable<System.Guid> Organization_id { get; set; }
        [Display(Name = "Report Code")]
        public string Payroll_Report_Code { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Report Format")]
        public string Report_Format { get; set; }
        [Display(Name = "Record Status")]
        public string Record_Status { get; set; }
        [Display(Name = "Authorize Status")]
        public string Authorize_Status { get; set; }
    }
    public class META_TBL_REPORT_SETTING
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Report Code")]
        public string Payroll_Report_Code { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Report Layout")]
        public string Report_Layout { get; set; }
        [Display(Name = "Report Format")]
        public string Report_Format { get; set; }
        [Display(Name = "Status Code")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<System.Guid> Organization_id { get; set; }
    }
    public class META_TBL_POSITION
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Designation/Position Code")]
        public string Position_Code { get; set; }
        [Display(Name = "Designation/Position Description")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Position_Effective_Date { get; set; }
        public Nullable<decimal> Base_Salary { get; set; }
        public Nullable<int> Status_Code { get; set; } 
    }
    #region META_TBL_ORGANIZATION_PAYROLL_COMPONENT
    public class META_TBL_ORGANIZATION_PAYROLL_COMPONENT
    {
        public System.Guid id { get; set; }
        [Display(Name = "Code")]
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Component Group")]
        public string Component_Group { get; set; }
        [Display(Name = "Taxable Type")]
        public string Taxable_Type { get; set; }
        [Display(Name = "Tax Deduction")]
        public bool Tax_Deduction { get; set; }
        [Display(Name = "Tax Policy")]
        public string Tax_Policy { get; set; }
        public string Frequency { get; set; }
        [Display(Name = "Amount Type")]
        public string Amount_Type { get; set; }
        [Display(Name = "Calculation Basic")]
        public string Calculation_Basic { get; set; }
        [Display(Name = "Value/Formula")]
        public string Formula { get; set; }
        [Display(Name = "Prorate for Newly Join/Resign?")]
        public bool Is_New_Join { get; set; }
        [Display(Name = "Prorate Value?")]
        public bool Is_Prorate { get; set; }
        [Display(Name = "Prorate Base")]
        public string Prorate_Base { get; set; }
        public string Sign { get; set; }
        public string Account { get; set; }
        [Display(Name = "Db / Cr")]
        public string Db_or_Cr { get; set; }
        [Display(Name = "SPT Code")]
        public string SPT_Code { get; set; }
        public string Remark { get; set; }
        [Display(Name = "Status")]
        public int Status_Code { get; set; } 
    }
    #endregion

    public class META_VW_EMPLOYEE_PAYROLL_COMPONENT
    {
        public System.Guid id { get; set; }
        [Display(Name = "Employee Name")]
        public string Employee_Name { get; set; }
        [Display(Name = "Code")]
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Component Group")]
        public string Component_Group { get; set; }
        [Display(Name = "Taxable Type")]
        public string Taxable_Type { get; set; }
        [Display(Name = "Tax Deduction")]
        public Nullable<bool> Tax_Deduction { get; set; }
        [Display(Name = "Tax Policy")]
        public string Tax_Policy { get; set; }
        public string Frequency { get; set; }
        [Display(Name = "Amount Type")]
        public string Amount_Type { get; set; }
        [Display(Name = "Calculation Basic")]
        public string Calculation_Basic { get; set; }
        [Display(Name = "Prorate for Newly Join/Resign?")]
        public bool Is_New_Join { get; set; }
        [Display(Name = "Prorate Value?")]
        public bool Is_Prorate { get; set; }
        [Display(Name = "Prorate Base")]
        public string Prorate_Base { get; set; }
        public string Sign { get; set; }
        public string Account { get; set; }
        [Display(Name = "Db / Cr")]
        public string Db_or_Cr { get; set; }
        [Display(Name = "SPT Code")]
        public string SPT_Code { get; set; }
        public string Remark { get; set; }

        [Display(Name = "Status")]
        public int Status_Code { get; set; } 
    }

    #region META_TBL_EMPLOYEE_PAYROLL_COMPONENT
    public class META_TBL_EMPLOYEE_PAYROLL_COMPONENT
    {
        public System.Guid id { get; set; }
        public System.Guid Employee_id { get; set; }
        [Display(Name = "Code")]
        public string Organization_Payroll_Component_Code { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Tax Deduction")]
        public Nullable<bool> Tax_Deduction { get; set; }
        [Display(Name = "Tax Policy")]
        public string Tax_Policy { get; set; }
        
        public string Remark { get; set; }
        [Display(Name = "Status")]
        public int Status_Code { get; set; } 
    }
    #endregion

    #region
    public class META_TBL_EMPLOYEE_PAYROLL_COMPONENT_EFFECTIVE
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Employee_Payroll_Component { get; set; }
        [Display(Name = "Start Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Start_Date { get; set; }
        [Display(Name = "End Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> End_Date { get; set; }
        [Display(Name="Value/Formula")]
        public string Formula { get; set; }
    }
    #endregion

    #region Meta tbl_User
    public class META_TBL_USER
    {
        public System.Guid id { get; set; }
        public string LDAP { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login_Attempt { get; set; }
        public string Is_Locked { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 

    }
    #endregion

    #region Meta tbl_Organization_Team
    public class META_TBL_ORGANIZATION_TEAM
    {
        public System.Guid id { get; set; }
        [Display(Name = "Team")]
        public string Team_Code { get; set; }
        [Display(Name = "Team Description")]
        public string Team_Description { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 

    }
    #endregion

    #region Meta tbl_General_Parameter
    public class META_TBL_GENERAL_PARAMETER
    {
        public System.Guid id { get; set; }
        [Display(Name = "Type")]
        public string Table_Name { get; set; }
        [Display(Name = "Name")]
        public string Field_Name { get; set; }
        [Display(Name = "Value")]
        public string Field_Value { get; set; }
        public int Field_Seq { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> Parent_ID { get; set; } 
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region Meta Organization Maintenence
    public class META_TBL_ORGANIZATION
    {
        public System.Guid id { get; set; }
        [Display(Name = "Code")]
        public string Organization_Code { get; set; }
        [Display(Name = "Name")]
        public string Organization_Name { get; set; }
        [Display(Name = "Type")]
        public string Organization_Service { get; set; }
        public string Organization_Type { get; set; }
        public string Parent_Organization_Code { get; set; }
        public Nullable<System.Guid> HeadOffice_Branch_ID { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    public class META_TBL_BASE_LOCATION
    {
        public System.Guid id { get; set; }
        [Display(Name ="Organization")]
        public Nullable<System.Guid> Organization_Id { get; set; }
        [Display(Name = "Map Type")]
        public string Map_Type { get; set; }
        [Display(Name = "Location Name")]
        public string Location_Name { get; set; }
        [Display(Name = "Work Location")]
        public Nullable<System.Guid> Reference_Id { get; set; }
        [Display(Name = "Latitude")]
        public Nullable<double> Latitude { get; set; }
        [Display(Name = "Longitude")]
        public Nullable<double> Longitude { get; set; }
        [Display(Name = "Range Area (Tolerance)")]
        public Nullable<decimal> Range { get; set; }
        [Display(Name = "Start Date")]
        public Nullable<System.DateTime> Start_Date { get; set; }
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> End_Date { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    }
    public  class META_VW_WORK_LOCATION
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Organization")]
        public Nullable<System.Guid> Organization_Id { get; set; }
        [Display(Name = "Location Name")]
        public string Location_Name { get; set; }
        [Display(Name = "Work Location")]
        public string Location { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }

    }
    public class META_TBL_ORGANIZATION_STRUCTURE
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        [Display(Name = "Structure")]
        public string Struktur { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Position WLKP")]
        public string Position_WLKP { get; set; }
    }
    public class META_TBL_CONTACT_PERSON
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Designation / Position Code")]
        public string Position_Code { get; set; }
        [Display(Name = "Designation / Position Description")]
        public string Position_Description { get; set; }
         [Display(Name = "Office Phone Number")]
        public string Office_Phone { get; set; }
        [Display(Name = "Personal Phone Number")]
        public string Personal_Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
        public string Remarks { get; set; }
    }
    public class META_TBL_HEAD_OFFICE_BRANCH
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public Nullable<System.Guid> Parent_Head_Office_ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Tax Addres")]
        public string Tax_Address { get; set; }
        [Display(Name = "Tax District")]
        public string Tax_District { get; set; }
        [Display(Name = "Tax Village")]
        public string Tax_Village { get; set; }
        [Display(Name = "Tax Regency/City")]
        public string Tax_Regency_City { get; set; }
        [Display(Name = "Tax Province")]
        public string Tax_Province { get; set; }
        [Display(Name = "Tax Country")]
        public string Tax_Country { get; set; }
        [Display(Name = "Tax Post Code")]
        public string Tax_Post_Code { get; set; }
        [Display(Name = "Mail Address")]
        public string Mail_Address { get; set; }
        [Display(Name = "Mail District")]
        public string Mail_District { get; set; }
        [Display(Name = "Mail Village")]
        public string Mail_Village { get; set; }
        [Display(Name = "Mail Regency/City")]
        public string Mail_Regency_City { get; set; }
        [Display(Name = "Mail Province")]
        public string Mail_Province { get; set; }
        [Display(Name = "Mail Country")]
        public string Mail_Country { get; set; }
        [Display(Name = "Mail Post Code")]
        public string Mail_Post_Code { get; set; }
        public string Location { get; set; }
        [Display(Name = "Head Office/Branch Phone Number")]
        public string HO_Branch_Phone_Number { get; set; }
        [Display(Name = "Head Office/Branch Fax Number")]
        public string HO_Branch_Fax_Number { get; set; }
        [Display(Name = "Head Office/Branch Tax ID")]
        public string HO_Branch_TAX_ID { get; set; }
        [Display(Name = "Signer Name")]
        public string Signer_Name { get; set; }
        [Display(Name = "Signer Position Code")]
        public string Signer_Position_Code { get; set; }
        [Display(Name = "Signer Position Description")]
        public string Signer_Position_Description { get; set; }
        [Display(Name = "Signer Tax ID")]
        public string Signers_Tax_ID { get; set; }
        [Display(Name = "Signer Type")]
        public string Signatory_Type { get; set; }
        [Display(Name = "Tax e-Billing Login ID")]
        public string Tax_e_Billing_Login_ID { get; set; }
        [Display(Name = "Tax e-Billing Password")]
        public string Tax_e_Billing_Login_Password { get; set; }
        [Display(Name = "Tax Office Name")]
        public string Tax_Office_Name { get; set; }
        [Display(Name = "Tax Office Address")]
        public string Tax_Office_Address { get; set; }
        [Display(Name = "Tax Office AR")]
        public string Tax_Office_AR { get; set; }
        [Display(Name = "Phone/Mobile Number AR")]
        public string AR_Phone_Number { get; set; }
        [Display(Name = "Email AR")]
        public string AR_Email { get; set; }
        [Display(Name = "Record Status ")]
        public Nullable<int> Status_Code { get; set; } 
    
    }
  
    public class META_TBL_HOLIDAY_CALENDAR_ORGANIZATION
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public Nullable<System.Guid> Holiday_Calendar_ID { get; set; }
        public Nullable<bool> Is_default { get; set; }
        [Display(Name = "Holiday Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Holiday_Date { get; set; }
        [Display(Name = "Holiday Name")]
        public string Holiday_Name { get; set; }
         [Display(Name = "Record Status ")]
        public Nullable<int> Status_Code { get; set; } 
        [Display(Name = "Location")]
        public string Location { get; set; }
    
        public virtual tbl_Holiday_Calendar tbl_Holiday_Calendar { get; set; }
        public virtual ICollection<tbl_Holiday_Calendar_Organization_Details> tbl_Holiday_Calendar_Organization_Details { get; set; }
        public virtual tbl_Organization tbl_Organization { get; set; }
}
    public partial class META_TBL_HOLIDAY_CALENDAR_ORGANIZATION_DETAILS
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Holiday_Calendar_organziation_ID { get; set; }
        public string Location { get; set; }
        public Nullable<int> Status_Code { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Holiday_Date { get; set; }

        public virtual tbl_Holiday_Calendar_Organization tbl_Holiday_Calendar_Organization { get; set; }
    }

    //public class META_TBL_HOLIDAY_CALENDAR_ORGANIZATION
    //{
    //    public System.Guid id { get; set; }
    //    public System.Guid Organization_ID { get; set; }
    //    [Display(Name = "Location")]
    //    public string Location { get; set; }
    //    [Display(Name = "Holiday Date")]
    //    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    //    public System.DateTime Holiday_Date { get; set; }
    //    [Display(Name = "Holiday Name")]
    //    public string Holiday_Name { get; set; }
    //    [Display(Name = "Record Status ")] 
    //}
    public class META_TBL_BANK_INFORMATION
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Bank { get; set; }
        [Display(Name = "Account Number")]
        public string Account_Number { get; set; }
        [Display(Name = "Account Name")]
        public string Account_Name { get; set; }
        [Display(Name = "Debet Account ID")]
        public string Debet_Account_ID { get; set; }
        public string Reference { get; set; }
        [Display(Name = "Payment Set Code")]
        public string Payment_Set_Code { get; set; }
        [Display(Name = "Default")]
        public Nullable<bool> Def { get; set; }
        [Display(Name = "Currency Code")]
        public string Currency_Code { get; set; }
        public string Purpose { get; set; }
        [Display(Name = "Record Status ")]
        public Nullable<int> Status_Code { get; set; } 

    }
    public class META_TBL_BPJS_MANPOWER
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "NPP Number")]
        public string Npp_Number { get; set; }
        [Display(Name = "Office Address")]
        public string Office_Address { get; set; }
        [Display(Name = "Office Phone Number")]
        public string Office_Phone_Number { get; set; }
        [Display(Name = "Office Fax Number")]
        public string Office_Fax_Number { get; set; }
        [Display(Name = "SIPP Online login ID")]
        public string SIPP_Online_login_ID { get; set; }
        [Display(Name = "SIPP Online Password")]
        public string SIPP_Online_Password { get; set; }
        [Display(Name = "Default")]
        public Nullable<bool> Def { get; set; }
        [Display(Name = "RO")]
        public string Ro { get; set; }
        [Display(Name = "Phone Mobile Number")]
        public string Phone_Mobile_Number { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Record Status ")]
        public Nullable<int> Status_Code { get; set; } 
    }
    public class META_TBL_BPJS_HealthCare
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "NPP Number")]
        public string Npp_Number { get; set; }
        [Display(Name = "Office Address")]
        public string Office_Address { get; set; }
        [Display(Name = "Office Phone Number")]
        public string Office_Phone_Number { get; set; }
        [Display(Name = "Office Fax Number")]
        public string Office_Fax_Number { get; set; }
        [Display(Name = "eDABU login ID")]
        public string e_DABU_login_ID { get; set; }
        [Display(Name = "eDABU login Password")]
        public string e_DABU_login_ID_Password { get; set; }
        [Display(Name = "Default")]
        public Nullable<bool> Def { get; set; }
        [Display(Name = "eID login ID")]
        public string e_ID_Login_ID { get; set; }
         [Display(Name = "eID login Password")]
        public string e_ID_Password { get; set; }
         [Display(Name = "RO")]
        public string Ro { get; set; }
        [Display(Name = "Phone Mobile Number")]
        public string Phone_Mobile_Number { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        [Display(Name = "Record Status ")]
        public Nullable<int> Status_Code { get; set; } 
    }
    public class META_TBL_COST_CENTER
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    public class META_TBL_CHART_ACCOUNT
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Debet / Kredit")]
        public string Db_Cr { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }

    #region META_TBL_EXCHANGE_RATE
    public class META_TBL_EXCHANGE_RATE
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Effective_Date { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
    }
    #endregion

    #region META_TBL_EXCHANGE_RATE_DETAILS
    public partial class META_TBL_EXCHANGE_RATE_DETAILS
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Exchange_Rate_ID { get; set; }
        [Display(Name = "Currency From")]
        public string Currency_From { get; set; }
        [Display(Name = "Currency To")]
        public string Currency_To { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Rate { get; set; }
    }
    #endregion

    #endregion

    #region Meta tbl_Organization_Working_Time
    public class META_TBL_ORGANIATION_WORKING_TIME
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Code")]
        public string Working_Time_Code { get; set; }
        [Display(Name = "Description")]
        public string Working_Time_Description { get; set; }
        [Display(Name = "Working Day")]
        public string Working_Day { get; set; }
        [Display(Name = "Base Working Day")]
        public string Base_Working_Day { get; set; }
        [Display(Name = "Base Fix (in Days)")]
        public Nullable<int> Base_Fix { get; set; }
        [Display(Name = "Base Calendar Day (in Days)")]
        public Nullable<int> Base_Calendar_Day { get; set; }
        [Display(Name = "Record Status")]
        public int Status_Code { get; set; } 

    }
    #endregion

    #region META LIST OF BANK
    public class META_TBL_LIST_OF_BANK
    {
        public System.Guid id { get; set; }
        [Display(Name = "Code")]
        public string Bank_Code { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "BI Bank Code")]
        public string BI_Bank_Code { get; set; }
        [Display(Name = "Swift Code")]
        public string Swift_Code { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
        public Nullable<bool> Is_Mandatory { get; set; }
        public string Is_Length { get; set; }
        [Display(Name = "Online Payment")]
        public bool Online_Payment { get; set; }
    }
    #endregion

    #region META BRANCH LIST OF BANK
    public class META_TBL_BRANCH_LIST_OF_BANK
    {
        public System.Guid id { get; set; }
        public System.Guid List_Of_Bank_ID { get; set; }
        [Display(Name = "Branch Code")]
        public string Branch_Code { get; set; }
        [Display(Name = "Branch Name")]
        public string Branch_Name { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
    #endregion

    #region META HOLIDAY CALENDAR
    public partial class META_TBL_HOLIDAY_CALENDAR
    {
        public System.Guid id { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Holiday_Date { get; set; }
        [Display(Name = "Name")]
        public string Holiday_Name { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META NON TAXABLE INCOME PARAMETER
    public partial class META_TBL_NON_TAXABLE_INCOME_PARAMETER
    {
        public System.Guid id { get; set; }
        [Display(Name = "Code")]
        public string Code_Non_Taxable_Income { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Amount { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META TAX RATE PARAMETER
    public partial class META_TBL_TAX_RATE_PARAMETER
    {
        public System.Guid id { get; set; }
        [Display(Name = "Type")]
        public string Type_Tax_Rate_Parameter { get; set; }
        [Display(Name = "NPWP")]
        public Nullable<int> NPWP { get; set; }
        [Display(Name = "From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> From { get; set; }
        [Display(Name = "To")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> To { get; set; }
        [Display(Name = "Percentage")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n0}%")]
        public Nullable<decimal> Percentage { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META COST PARAMETER
    public partial class META_TBL_COST_PARAMETER
    {
        public System.Guid id { get; set; }
        [Display(Name = "Type")]
        public string Type_Cost_Parameter { get; set; }
        [Display(Name = "Percentage")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n0}%")]
        public Nullable<decimal> Percentage { get; set; }
        [Display(Name = "Max Cost per Month")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> Max_Cost_Month { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META PAYROLL SCHEDULE
    public partial class META_TBL_PAYROLL_SCHEDULE
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Payroll Advice Submission")]
        public Nullable<int> Payroll_Advice_Sub { get; set; }
        public string Payroll_Advice_Sub_Meas { get; set; }
        [Display(Name = "Payroll Report Submission")]
        public Nullable<int> Payroll_Report_Sub { get; set; }
        public string Payroll_Report_Sub_Meas { get; set; }
        [Display(Name = "Payroll Approval")]
        public Nullable<int> Payroll_Approval { get; set; }
        public string Payroll_Approval_Meas { get; set; }
        [Display(Name = "Jurnal")]
        public Nullable<int> Jurnal { get; set; }
        public string Jurnal_Meas { get; set; }
        [Display(Name = "Payslip")]
        public Nullable<int> Payslip { get; set; }
        public string Payslip_Meas { get; set; }
        [Display(Name = "SPT Report")]
        public Nullable<int> SPT_Report { get; set; }
        public string SPT_Report_Meas { get; set; }
        [Display(Name = "Tax Report")]
        public Nullable<int> Tax_Report { get; set; }
        public string Tax_Report_Meas { get; set; }
        [Display(Name = "Pay Day")]
        public Nullable<int> Pay_Day { get; set; }
        [Display(Name = "Holiday Treatment for Payroll Payment")]
        public string Holiday_Treatment { get; set; }
        [Display(Name = "Cut Off Start")]
        public Nullable<int> Cut_Off_Time_Start { get; set; }
        [Display(Name = "Cut Off End")]
        public Nullable<int> Cut_Off_Time_End { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
    }
    #endregion

    #region META EMPLOYEE
    public partial class META_TBL_EMPLOYEE
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Employee ID Number")]
        public string Employee_No { get; set; }
        [Display(Name = "Salutation")]
        public string Salutation { get; set; }
        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }
        [Display(Name = "Place of Birth")]
        public string Place_Of_Birth { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        [Display(Name = "Mother's Maiden Name")]
        public string Mother_Maiden_Name { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Religion")]
        public string Religion { get; set; }
        [Display(Name = "Blood Type")]
        public string Blood_Type { get; set; }
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Display(Name = "Marital Status")]
        public string Marital_Status { get; set; }
        [Display(Name = "Number of Children")]
        public Nullable<int> Number_Of_Children { get; set; }
        [Display(Name = "Education")]
        public string Education { get; set; }
        [Display(Name = "Last Education")]
        public string Last_Education { get; set; }
        [Display(Name = "BPJS TK NPP")]
        public string BPJS_TK_NPP { get; set; }
        [Display(Name = "BPJS Healthcare NPP")]
        public string BPJS_Health_NPP { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META EMPLOYEE FAMILY
    public partial class META_TBL_EMPLOYEE_FAMILY
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public string Employee_No { get; set; }
        public Nullable<System.Guid> Employee_ID { get; set; }
        [Display(Name = "Relationship")]
        public string Relationship { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "No KK")]
        public string No_KK { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Blood Type")]
        public string Blood_Type { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META EMPLOYEE ID NUMBER
    public partial class META_TBL_EMPLOYEE_ID_INFORMATION
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "ID Number")]
        public string ID_Number { get; set; }
        [Display(Name = "ID Type")]
        public string ID_Type { get; set; }
        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
        [Display(Name = "Expired Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Expired_Date { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META EMPLOYEE ADDRESS
    public partial class META_TBL_EMPLOYEE_ADDRESS
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Legal Address")]
        public string Address { get; set; }
        [Display(Name = "Village")]
        public string Village { get; set; }
        [Display(Name = "District")]
        public string District { get; set; }
        [Display(Name = "City/ Regency")]
        public string City_Regency { get; set; }
        [Display(Name = "State/ Province")]
        public string State_Province { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Zip Code")]
        public string Zip_Code { get; set; }
        [Display(Name = "Permanent Address")]
        public string Permanent_Address { get; set; }
        [Display(Name = "Village")]
        public string Permanent_Village { get; set; }
        [Display(Name = "District")]
        public string Permanent_District { get; set; }
        [Display(Name = "City/ Regency")]
        public string Permanent_City_Regency { get; set; }
        [Display(Name = "State/ Province")]
        public string Permanent_State_Province { get; set; }
        [Display(Name = "Country")]
        public string Permanent_Country { get; set; }
        [Display(Name = "Zip Code")]
        public string Permanent_Zip_Code { get; set; }
        [Display(Name = "Mailing Address")]
        public string Mailing_Address { get; set; }
        [Display(Name = "Village")]
        public string Mailing_Village { get; set; }
        [Display(Name = "District")]
        public string Mailing_District { get; set; }
        [Display(Name = "City/ Regency")]
        public string Mailing_City_Regency { get; set; }
        [Display(Name = "State/ Province")]
        public string Mailing_State_Province { get; set; }
        [Display(Name = "Country")]
        public string Mailing_Country { get; set; }
        [Display(Name = "Zip Code")]
        public string Mailing_Zip_Code { get; set; }
        [Display(Name = "Residential Phone Number")]
        public string Residential_Phone_No { get; set; }
        [Display(Name = "Office Phone Number")]
        public string Office_Phone_No { get; set; }
        [Display(Name = "Mobile Phone Number")]
        public string Mobile_Phone_No { get; set; }
        [Display(Name = "Coorporate Email Address")]
        public string Coorporate_Email_Address { get; set; }
        [Display(Name = "Personal Email Address")]
        public string Personal_Email_Address { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META EMPLOYEE APPOINTMENT
    public partial class META_TBL_EMPLOYEE_APPOINTMENT
    {
        public System.Guid id { get; set; }
        public System.Guid Employee_ID { get; set; }

        [Display(Name = "Date of Hire")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Date_Of_Hire { get; set; }

        [Display(Name = "Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Join_Date { get; set; }

        [Display(Name = "End Date Probation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> End_Date_of_Probation { get; set; }

        [Display(Name = "Old Employee Number")]
        public string Old_Employee_Number { get; set; }

        [Display(Name = "Cost Center")]
        public string Cost_Center { get; set; }

        [Display(Name = "Resign Reason")]
        public string Resign_Reason { get; set; }

        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }

    #endregion

    #region META VW EMPLOYEE APPOINTMENT
    public partial class META_vw_appointment_information
    {
        public System.Guid Employee_Appointment_Id { get; set; }

        [Display(Name = "Employment Id")]
        public System.Guid Employee_ID { get; set; }

        [Display(Name = "Employment Status")]
        public string Employment_Status { get; set; }

        [Display(Name = "Employee Status")]
        public string Employee_Status { get; set; }

        [Display(Name = "Status Information-Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }

        [Display(Name = "Date of Hire")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Date_Of_Hire { get; set; }

        [Display(Name = "Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Join_Date { get; set; }

        [Display(Name = "Old Employee Number")]
        public string Old_Employee_Number { get; set; }

        [Display(Name = "End Date of Probation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> End_Date_of_Probation { get; set; }

        [Display(Name = "Work Location")]
        public string Work_Location { get; set; }

        [Display(Name = "Work Location Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Work_Location_Effective_Date { get; set; }

        [Display(Name = "Record Status")]
        public string Record_Status { get; set; }

        [Display(Name = "Position Code")]
        public string Position_Code { get; set; }

        [Display(Name = "Position Description")]
        public string Position_Description { get; set; }

        [Display(Name = "Position Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Position_Effective_Date { get; set; }

        [Display(Name = "Division Code")]
        public string Division_Code { get; set; }

        [Display(Name = "Division Description")]
        public string Division_Description { get; set; }

        [Display(Name = "Divison Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Division_Effective_Date { get; set; }

        [Display(Name = "Department Code")]
        public string Department_Code { get; set; }

        [Display(Name = "Department Description")]
        public string Department_Description { get; set; }

        [Display(Name = "Department Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Department_Effective_Date { get; set; }

        [Display(Name = "Grade Code")]
        public string Grade_Code { get; set; }

        [Display(Name = "Grade Description")]
        public string Grade_Description { get; set; }

        [Display(Name = "Grade Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Grade_Effective_Date { get; set; }

        [Display(Name = "Cost Center")]
        public string Cost_Center { get; set; }
        public string Deputation { get; set; }

        [Display(Name = "Resign Reason")]
        public string Resign_Reason { get; set; }

        [Display(Name = "Working Time Code")]
        public string Working_Time_Code { get; set; }

        [Display(Name = "Working Time Description")]
        public string Working_Time_Description { get; set; }

        [Display(Name = "Working Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Working_Start_Date { get; set; }

        [Display(Name = "Working End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Working_End_Date { get; set; }

        [Display(Name = "Contract No")]
        public string Contract_No { get; set; }

        [Display(Name = "Contract Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Contract_Start_Date { get; set; }

        [Display(Name = "Contract End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Contract_End_Date { get; set; } 
    }

    #endregion

    #region META APPOINTMENT CONTRACT INFORMATION

    public partial class META_TBL_APPOINTMENT_CONTRACT_INFORMATION
    {
        public System.Guid id { get; set; }

        [Display(Name = "Contract No")]
        public string Contract_No { get; set; }

        [Display(Name = "Contract Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Contract_Start_Date { get; set; }

        [Display(Name = "Contract End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Contract_End_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT DEPARTMENT

    public partial class META_TBL_APPOINTMENT_DEPARTMENT
    {
        public System.Guid id { get; set; }

        [Display(Name = "Department Code")]
        public string Department_Code { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT DIVISION

    public partial class META_TBL_APPOINTMENT_DIVISION
    {
        public System.Guid id { get; set; }

        [Display(Name = "Division Code")]
        public string Division_Code { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT GRADE

    public partial class META_TBL_APPOINTMENT_GRADE
    {
        public System.Guid id { get; set; }

        [Display(Name = "Grade Code")]
        public string Grade_Code { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT POSITION

    public partial class META_TBL_APPOINTMENT_POSITION
    {
        public System.Guid id { get; set; }

        [Display(Name = "Position Code")]
        public string Position_Code { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT STATUS INFORMATION

    public partial class META_TBL_APPOINTMENT_STATUS_INFORMATION
    {
        public System.Guid id { get; set; }

        [Display(Name = "Employment Status")]
        public string Employment_Status { get; set; }

        [Display(Name = "Employee Status")]
        public string Employee_Status { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT WORK LOCATION

    public partial class META_TBL_APPOINTMENT_WORK_LOCATION
    {
        public System.Guid id { get; set; }

        [Display(Name = "Work Location")]
        public string Work_Location { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
    }

    #endregion

    #region META APPOINTMENT WORKING TIME

    public partial class META_TBL_APPOINTMENT_WORKING_TIME
    {
        public System.Guid id { get; set; }

        [Display(Name = "Working Time Code")]
        public string Working_Time_Code { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Start_Date { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> End_Date { get; set; }
    }

    #endregion

    #region META TAX PERIOD
    public partial class META_TBL_TAX_PERIOD
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public int def { get; set; }
        [Display(Name = "Tax Year")]
        public int Tax_Year { get; set; }
        [Display(Name = "Tax Period From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public System.DateTime Tax_Peroid_From { get; set; }
        [Display(Name = "Tax Period To")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public System.DateTime Tax_Peroid_To { get; set; }
        [Display(Name = "Tax Period Status")]
        public string Tax_Peroid_Status { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }  
    }
    #endregion

    #region META TAX MONTH
    public partial class META_TBL_TAX_PERIOD_MONTH
    {
        public System.Guid id { get; set; }
        public System.Guid Tax_Period_ID { get; set; }
        [Display(Name = "Tax Period Month")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public System.DateTime Tax_Period_Month { get; set; }
        [Display(Name = "Tax Status")]
        public string Tax_Status { get; set; }
        [Display(Name = "Closing Date Permanent")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Closing_Date_Permanent { get; set; }
        [Display(Name = "Closing Date NonPermanent")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Closing_Date_NonPermanent { get; set; } 
    }

    #endregion

    #region META EMPLOYEE TAX INFORMATION
    public partial class META_EMPLOYEE_TAX_INFO
    {
        public System.Guid id { get; set; }
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Tax ID")]
        public string Tax_No { get; set; }
        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Effective_Date { get; set; }
        [Display(Name = "Revoke Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Revoke_Date { get; set; }
        [Display(Name = "Salary Tax Policy")]
        public string Salary_Tax_Policy { get; set; }
        [Display(Name = "Previous Net Income")]
        public Nullable<decimal> Previous_Net_Income { get; set; }
        [Display(Name = "Previous Tax Paid")]
        public Nullable<decimal> Previous_Tax_Paid { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META EMPLOYEE TAX STATUS EFFECTIVE YEAR
    public partial class META_EMPLOYEE_TAX_STATUS_EFFECTIVE_YEAR
    {
        public System.Guid id { get; set; }
        [Display(Name = "Status")]
        public string Tax_Status { get; set; }
        [Display(Name = "Status Effective Year")]
        public string Year_Status { get; set; }
    }
    #endregion

    #region META_EMPLOYEE_PAYSLIP_INFORMATION
    public partial class META_EMPLOYEE_PAYSLIP_INFO
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Payslip Distribution")]
        public string Payslip_Distribution { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
        public string Remarks { get; set; }
        [Display(Name = "Is HR User")]
        public Nullable<bool> Is_HR_User { get; set; }
    }
    #endregion

    #region META PAYROLL PERIOD
    public partial class META_TBL_PAYROLL_PERIOD
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Tax_Period_ID { get; set; }
        [Display(Name = "Tax Period")]
        public string Tax_Period_Month { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }  
    }
    #endregion

    #region META PAYROLL PERIOD DETAIL
    public class META_TBL_PAYROLL_PERIOD_DETAIL
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        [Display(Name = "Tax Period")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public System.DateTime Tax_Period { get; set; }
        [Display(Name = "Period Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Period_Start_Date { get; set; }
        [Display(Name = "Period End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Period_End_Date { get; set; }
    }
    #endregion

    #region META META VW SUMMARY ORG HOLLIDAY
    public partial class META_VW_SUMMARY_ORG_HOLLIDAY
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Location { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Holiday_Date { get; set; }
        [Display(Name = "Holiday Name")]
        public string Holiday_Name { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META LOAN
    public partial class META_TBL_LOAN
    {
        public System.Guid id { get; set; }
        [Display(Name = "Deduction No")]
        public string Loan_No { get; set; }
        [Display(Name = "Employee ID")]
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Deduction Type")]
        public string Type_Loan { get; set; }
        [Display(Name = "Component Linkage")]
        public System.Guid Component_Linkage { get; set; }
        [Display(Name = "Currency")]
        public string Currency { get; set; }
        [Display(Name = "Deduction Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Loan_Amount { get; set; }
        [Display(Name = "Outstanding Deduction Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Outstanding_Loan_Amount { get; set; }
        [Display(Name = "Rate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n0}%")]
        public Nullable<decimal> Rate { get; set; }
        [Display(Name = "Installment Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Installment_Amount { get; set; }
        [Display(Name = "Tenor")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Tenor { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "Outstanding Tenor")]
        public decimal Outstanding_Tenor { get; set; }
        [Display(Name = "Upload Number")]
        public string Upload_Number { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META VW LOAN
    public partial class META_VW_LOAN
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Deduction No")]
        public string Loan_No { get; set; }
        public System.Guid Employee { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public string Employee_ID { get; set; }
        public string Type_Loan { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public System.Guid Component_Linkage_ID { get; set; }
        public string Component_Linkage { get; set; }
        public string Component_Group { get; set; }
        public string Currency { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Loan_Amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Outstanding_Loan_Amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n0}%")]
        public Nullable<decimal> Rate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public decimal Installment_Amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Tenor { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Outstanding_Tenor { get; set; }
        public string Upload_Number { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<System.DateTime> Duration_Start { get; set; }
        public Nullable<System.DateTime> Duration_End { get; set; }
        public System.DateTime Date { get; set; }
    }
    #endregion

    #region META COMPENSATION AND BENEFIT
    public partial class META_TBL_COMPENSATION_BENEFIT
    {
        public System.Guid id { get; set; }
        [Display(Name = "Employee ID")]
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Type")]
        public string Type_Compensation_Benefit { get; set; }
        [Display(Name = "Component Linkage")]
        public System.Guid Component_Linkage { get; set; }
        [Display(Name = "Currency")]
        public string Currency { get; set; }
        [Display(Name = "Budget")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Budget { get; set; }
        public string Period { get; set; }
        [Display(Name = "Upload Number")]
        public string Upload_Number { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META VW COMPENSATION AND BENEFIT
    public partial class META_VW_COMPENSATION_BENEFIT
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Employee { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public string Employee_ID { get; set; }
        public string Type_Compensation_Benefit { get; set; }
        public string Organization_Payroll_Component_Code { get; set; }
        public string Description { get; set; }
        public System.Guid Component_Linkage_ID { get; set; }
        public string Component_Linkage { get; set; }
        public string Component_Group { get; set; }
        public string Currency { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Budget { get; set; }
        public string Period { get; set; }
        public string Upload_Number { get; set; }
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META VW PAYROLL PERIOD SUMMARY
    public partial class META_VW_PAYROLL_PERIOD_SUMMARY
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        public System.Guid Tax_Period_ID { get; set; }
        public int Tax_Year { get; set; }
        public string Tax_Period_Month { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_Start_Date { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Period_End_Date { get; set; }
        public Nullable<int> Status_Code { get; set; } 
    }
    #endregion

    #region META VW TAX PERIOD SUMMARY
    public partial class META_VW_TAX_PERIOD_SUMMARY
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Tax Year")]
        public int Tax_Year { get; set; }
        [Display(Name = "Tax Period From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public Nullable<System.DateTime> Tax_Period_From { get; set; }
        [Display(Name = "Tax Period To")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-yyyy}")]
        public Nullable<System.DateTime> Tax_Period_To { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
    }
    #endregion
	#region META ORGANIZATION EMAIL SETUP
    public partial class META_TBL_ORGANIZATION_EMAIL_SETUP
    {
        public System.Guid id { get; set; }
        [Display(Name = "Organization")]
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "eMail Address")]
        public string Email_Address { get; set; }
        [Display(Name = "SMTP Server")]
        public string SMTP_Server { get; set; }
        [Display(Name = "Port")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Port { get; set; }
        [Display(Name = "SMTP User")]
        public string SMTP_User { get; set; }
        [Display(Name = "SMTP Password")]
        public string SMTP_Password { get; set; }
        [Display(Name = "eMail Subject")]
        public string Email_Subject { get; set; }
        [Display(Name = "eMail Note")]
        public string Email_Note { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
    }
    #endregion

    #region META VW ORGANIZATION EMAIL SETUP SUMMARY
    public partial class META_VW_ORGANIZATION_EMAIL_SETUP_SUMMARY
    {
        public System.Guid id { get; set; }
        public System.Guid Organization_ID { get; set; }
        [Display(Name = "Organization Code")]
        public string Organization_Code { get; set; }
        [Display(Name = "Organization Name")]
        public string Organization_Name { get; set; }
        [Display(Name = "eMail Address")]
        public string Email_Address { get; set; }
        [Display(Name = "SMTP Server")]
        public string SMTP_Server { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
    }
    #endregion

    #region META META VW PAYROLL SLIP EMPLOYEE
    public partial class META_VW_PAYROLL_SLIP_EMPLOYEE_SUMMARY
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        [Display(Name = "Employee")]
        public string Employee_No { get; set; }
        [Display(Name = "Employee")]
        public string Full_Name { get; set; }
        public Nullable<System.Guid> Payroll_Period_ID { get; set; }
        [Display(Name = "Month")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMMM yyy}")]
        public Nullable<System.DateTime> Tax_Period { get; set; }
        public Nullable<System.DateTime> Period_Start_Date { get; set; }
        public Nullable<System.DateTime> Period_End_Date { get; set; }
        [Display(Name = "Generation Number")]
        public string Generation_Number { get; set; }
        [Display(Name = "Name File")]
        public string File_Name_Payslip { get; set; }
        public string Payslip_Distribution { get; set; }
        public Nullable<bool> Exclude_Zero_Value { get; set; }
        public Nullable<int> Run_From { get; set; }
        public Nullable<int> Run_To { get; set; }
        public string Payslip_Status { get; set; }
        public string Payslip_Url { get; set; }
        public Nullable<System.DateTime> Generate_Date { get; set; }
    }
    #endregion

    #region META CHANGE PASSWORD
    public partial class META_CHANGE_PASSWORD
    {
        [Display(Name = "Current Password")]
        public string Current_Password { get; set; }
        [Display(Name = "New Password")]
        public string New_Password { get; set; }
        [Display(Name = "Confirm New Password")]
        public string Confirm_New_Password { get; set; }
    }
    #endregion

    #region META SYSTEM PARAMETER
    public partial class META_SYSTEM_PARAMETER
    {

        public System.Guid id { get; set; }
        [Display(Name = "Param Code")]
        public string Param_Code { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Created By")]
        public string Created_By { get; set; }
        [Display(Name = "Created DateTime")]
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        [Display(Name = "Updated By")]
        public string Updated_By { get; set; }
        [Display(Name = "Updated DateTime")]
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
    }
    #endregion

    #region META EMPLOYEE APPROVAL
    public partial class META_TBL_EMPLOYEE_APPROVAL
    {
        public System.Guid id { get; set; }

        [Display(Name = "Employee ID Number")]
        public string Employee_ID_Number { get; set; }

        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }

        [Display(Name = "Supervisor ID Number")]
        public string Supervisor_ID_Number { get; set; }

        [Display(Name = "Supervisor Name")]
        public string Supervisor_Name { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public System.Guid Organization_ID { get; set; }

        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
    }
    #endregion
	
	
	    #region META BLOG
    public partial class META_BLOG
    {
        public System.Guid id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Post Content")]
        public string Post_Content { get; set; }
        public Nullable<System.Guid> User_Id { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Display(Name = "Category Menu")]
        public Nullable<System.Guid> Category_Id { get; set; }
        [Display(Name = "View")]
        public Nullable<int> Frequence { get; set; }
        [Display(Name = "Image Title")]
        public string Image_Title { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        [Display(Name = "Created DateTime")]
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        [Display(Name = "Create By")]
        public string Created_By { get; set; }
        [Display(Name = "Updated DateTime")]
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        [Display(Name = "Updated By")]
        public string Updated_By { get; set; }
        [Display(Name = "Authorized DateTime")]
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        [Display(Name = "Authorized By")]
        public string Authorized_By { get; set; }
    }
    #endregion

    #region META VW_BLOG_SUMMARY
    public partial class META_VW_BLOG_SUMMARY
    {
        public System.Guid id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Image Title")]
        public string Image_Title { get; set; }
        [Display(Name = "Post Content")]
        public string Post_Content { get; set; }
        [Display(Name = "Tag")]
        public string Tags { get; set; }
        public System.Guid Category_Id { get; set; }
        [Display(Name = "Category")]
        public string Category_Menu { get; set; }
        [Display(Name = "View")]
        public Nullable<int> Frequence { get; set; }
        [Display(Name = "Record Status")]
        public Nullable<int> Status_Code { get; set; }
        [Display(Name = "Authorize Status")]
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        [Display(Name = "Created DateTime")]
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        [Display(Name = "Created By")]
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
    }
    #endregion
	
	#region META CHANGE PASSWORD
    public partial class META_USER_TRIAL
    {
        public System.Guid id { get; set; }
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Display(Name = "Mobile Phone 1")]
        public string Phone_1 { get; set; }
        [Display(Name = "Mobile Phone 2")]
        public string Phone_2 { get; set; }
        [Display(Name = "Office Phone No")]
        public string Office_Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Company Name")]
        public string Company_Name { get; set; }
        [Display(Name = "Company Address")]
        public string Company_Address { get; set; }
    }
    #endregion
    #region Meta MyAttendance 
    public partial class META_MYATTENDANCE
    {
        [Display(Name = "No")]
        public int id_Row { get; set; }
        public Nullable<System.Guid> id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Organization_Name { get; set; }
        public Nullable<System.Guid> Employee_Id { get; set; }
        public string Full_Name { get; set; }
        public Nullable<System.Guid> User_Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "Check In")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Day_Time_In { get; set; }
        [Display(Name = "Check Out")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Day_Time_Out { get; set; }
        public string Description_Time_In { get; set; }
        public string Description_Time_Out { get; set; }
        public Nullable<double> Longitude_Time_In { get; set; }
        public Nullable<double> Longitude_Time_Out { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        [Display(Name = "Day Type")]
        public string Day_Type { get; set; }
        public string Code { get; set; }
        [Display(Name = "Approver Remark")]
        public string Approver_Remarks { get; set; }
        [Display(Name = "Description")]
        public string Approval_Description { get; set; }
        [Display(Name = "Working Time In")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Schedule_Time_in { get; set; }
        [Display(Name = "Working Time Out")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Schedule_Time_Out { get; set; }
        
    }
    #endregion

    #region MyLeave
    public partial class META_MYLEAVE
    {
        public System.Guid Id { get; set; }
        public System.Guid Organization_Id { get; set; }
        public string Organization_Name { get; set; }
        public System.Guid Employee_Id { get; set; }
        public string Employee_No { get; set; }
        public string Full_Name { get; set; }
        public Nullable<System.DateTime> From_Date { get; set; }
        public Nullable<System.DateTime> To_Date { get; set; }
        [Display(Name = "Date")]
        public string strDate { get; set; }
        [Display(Name = "Leave Type")]
        public string Leave_Type { get; set; }
        [Display(Name = "Duration Type")]
        public string Duration_Type { get; set; }
        [Display(Name = "Duration (In Days)")]
        public Nullable<double> Duration { get; set; }
        public Nullable<System.TimeSpan> Duration_Time_Start { get; set; }
        public Nullable<System.TimeSpan> Duration_Time_End { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Status")]
        public string strStatus { get; set; }
        public Nullable<bool> Is_Reset { get; set; }
        [Display(Name = "Reviewed By")]
        public string Reviewed_By { get; set; }
        [Display(Name = "Approver Remarks")]
        public string Approver_Remarks { get; set; }
        public string Path_Document { get; set; }
        [Display(Name = "Request Date")]
        public Nullable<System.DateTime> Request_Date { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
    }
    #endregion
}
#endregion