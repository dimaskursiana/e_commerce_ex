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
    
    public partial class tbl_Generate_Bank_File
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> Organization_ID { get; set; }
        public string Source_Account_Bank { get; set; }
        public string Debit_Account_Number { get; set; }
        public string Debit_Account_Name { get; set; }
        public string Debit_Account_Address { get; set; }
        public string File_Reference { get; set; }
        public Nullable<System.DateTime> Transfer_Date { get; set; }
        public string Transfer_Message { get; set; }
        public Nullable<int> Total_Employee { get; set; }
        public Nullable<decimal> Total_Amount { get; set; }
        public string Output_File_Name { get; set; }
        public string Output_File_Type { get; set; }
        public string Currency_Code { get; set; }
        public string Reference { get; set; }
        public string Payment_Set_Code { get; set; }
        public string Debit_Account_ID { get; set; }
        public Nullable<System.DateTime> Created_DateTime { get; set; }
        public string Created_By { get; set; }
    }
}
