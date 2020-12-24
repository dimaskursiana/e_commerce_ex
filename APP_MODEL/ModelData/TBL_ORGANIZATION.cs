//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APP_MODEL.ModelData
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Organization
    {
        public tbl_Organization()
        {
            this.tbl_Client_Organization_Service_Role = new HashSet<tbl_Client_Organization_Service_Role>();
            this.tbl_Job_Code = new HashSet<tbl_Job_Code>();
            this.tbl_Job_Function = new HashSet<tbl_Job_Function>();
            this.tbl_Job_Title = new HashSet<tbl_Job_Title>();
            this.tbl_Organization_Account_Group_Maintenence = new HashSet<tbl_Organization_Account_Group_Maintenence>();
            this.tbl_Organization_Service_Role = new HashSet<tbl_Organization_Service_Role>();
            this.tbl_Organization_Team = new HashSet<tbl_Organization_Team>();
            this.tbl_Organization_User = new HashSet<tbl_Organization_User>();
            this.tbl_Role = new HashSet<tbl_Role>();
            this.tbl_Salary_Grade = new HashSet<tbl_Salary_Grade>();
            this.tbl_Payroll_Schedule = new HashSet<tbl_Payroll_Schedule>();
            this.tbl_Client_Organization_Team = new HashSet<tbl_Client_Organization_Team>();
            this.tbl_Tax_Period = new HashSet<tbl_Tax_Period>();
            this.tbl_Contact_Person = new HashSet<tbl_Contact_Person>();
            this.tbl_Holiday_Calendar_Organization = new HashSet<tbl_Holiday_Calendar_Organization>();
            this.tbl_Organization_Branch_NPWP = new HashSet<tbl_Organization_Branch_NPWP>();
            this.tbl_BPJS_Healthcare = new HashSet<tbl_BPJS_Healthcare>();
            this.tbl_BPJS_Manpower = new HashSet<tbl_BPJS_Manpower>();
            this.tbl_Exchange_Rate = new HashSet<tbl_Exchange_Rate>();
            this.tbl_Payroll_Period = new HashSet<tbl_Payroll_Period>();
            this.tbl_HeadOffice_Branch = new HashSet<tbl_HeadOffice_Branch>();
            this.tbl_Organization_Working_Time = new HashSet<tbl_Organization_Working_Time>();
            this.tbl_Organization_Group = new HashSet<tbl_Organization_Group>();
            this.tbl_Organization_Signature = new HashSet<tbl_Organization_Signature>();
            this.tbl_Employee = new HashSet<tbl_Employee>();
            this.tbl_Employee_Document = new HashSet<tbl_Employee_Document>();
            this.tbl_Organization_Structure = new HashSet<tbl_Organization_Structure>();
            this.tbl_Bank_Information = new HashSet<tbl_Bank_Information>();
        }
    
        public System.Guid id { get; set; }
        public string Organization_Code { get; set; }
        public string Organization_Name { get; set; }
        public string Organization_Service { get; set; }
        public string Organization_Type { get; set; }
        public string Parent_Organization_Code { get; set; }
        public Nullable<System.Guid> HeadOffice_Branch_ID { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public Nullable<System.Guid> Workflow_Status_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Updated_DateTime { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Authorized_DateTime { get; set; }
        public string Authorized_By { get; set; }
        public Nullable<int> Authorize_Status { get; set; }
        public Nullable<bool> Is_Authorized { get; set; }
    
        public virtual ICollection<tbl_Client_Organization_Service_Role> tbl_Client_Organization_Service_Role { get; set; }
        public virtual ICollection<tbl_Job_Code> tbl_Job_Code { get; set; }
        public virtual ICollection<tbl_Job_Function> tbl_Job_Function { get; set; }
        public virtual ICollection<tbl_Job_Title> tbl_Job_Title { get; set; }
        public virtual ICollection<tbl_Organization_Account_Group_Maintenence> tbl_Organization_Account_Group_Maintenence { get; set; }
        public virtual ICollection<tbl_Organization_Service_Role> tbl_Organization_Service_Role { get; set; }
        public virtual ICollection<tbl_Organization_Team> tbl_Organization_Team { get; set; }
        public virtual ICollection<tbl_Organization_User> tbl_Organization_User { get; set; }
        public virtual ICollection<tbl_Role> tbl_Role { get; set; }
        public virtual ICollection<tbl_Salary_Grade> tbl_Salary_Grade { get; set; }
        public virtual ICollection<tbl_Payroll_Schedule> tbl_Payroll_Schedule { get; set; }
        public virtual ICollection<tbl_Client_Organization_Team> tbl_Client_Organization_Team { get; set; }
        public virtual ICollection<tbl_Tax_Period> tbl_Tax_Period { get; set; }
        public virtual ICollection<tbl_Contact_Person> tbl_Contact_Person { get; set; }
        public virtual ICollection<tbl_Holiday_Calendar_Organization> tbl_Holiday_Calendar_Organization { get; set; }
        public virtual ICollection<tbl_Organization_Branch_NPWP> tbl_Organization_Branch_NPWP { get; set; }
        public virtual ICollection<tbl_BPJS_Healthcare> tbl_BPJS_Healthcare { get; set; }
        public virtual ICollection<tbl_BPJS_Manpower> tbl_BPJS_Manpower { get; set; }
        public virtual ICollection<tbl_Exchange_Rate> tbl_Exchange_Rate { get; set; }
        public virtual ICollection<tbl_Payroll_Period> tbl_Payroll_Period { get; set; }
        public virtual ICollection<tbl_HeadOffice_Branch> tbl_HeadOffice_Branch { get; set; }
        public virtual ICollection<tbl_Organization_Working_Time> tbl_Organization_Working_Time { get; set; }
        public virtual ICollection<tbl_Organization_Group> tbl_Organization_Group { get; set; }
        public virtual ICollection<tbl_Organization_Signature> tbl_Organization_Signature { get; set; }
        public virtual ICollection<tbl_Employee> tbl_Employee { get; set; }
        public virtual ICollection<tbl_Employee_Document> tbl_Employee_Document { get; set; }
        public virtual ICollection<tbl_Organization_Structure> tbl_Organization_Structure { get; set; }
        public virtual ICollection<tbl_Bank_Information> tbl_Bank_Information { get; set; }
    }
}
