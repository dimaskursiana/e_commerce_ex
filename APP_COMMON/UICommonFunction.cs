using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using APP_MODEL.ModelData;
using System.Data.Entity; 
using APP_COMMON;
using APP_CORE.GetData;
using System.Globalization;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace APP_COMMON
{
    public class UICommonFunction
    {
        private const string CONST_DATETIME_EXCEPTION = "Formate Date Error.";
        public static string ErrorMessage = "";

        public static string GenerateComponentPayroll(List<tbl_Organization_Payroll_Component> OrganizationPayrollComponent, List<vw_Employee_Component_Formula> Employee_Payroll_Component, tbl_Payroll_Calculation Calculcation, tbl_Payroll_Transaction Transaction)
        {
            //ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            //Guid exId = Guid.Parse("9f597d82-9f2d-4a74-8610-98c623052179"); 
            //Guid Organization_Id = Guid.Parse("D7BBE72C-D5B6-437A-9484-5E811BB98860");

            //tbl_Payroll_Calculation Calculcation = db.tbl_Payroll_Calculation.FirstOrDefault();
            //tbl_Payroll_Transaction Transaction = db.tbl_Payroll_Transaction.Where(p => p.id == exId).FirstOrDefault();

            //List<tbl_Organization_Payroll_Component> OrganizationPayrollComponent = db.tbl_Organization_Payroll_Component.Where(p => p.Organization_id == Organization_Id && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && p.Authorized_Status == GlobalVariable.CONST_AUTHORIZED).ToList();
            string Component_Formula = Transaction.Formula;
            string Formula = Transaction.Formula;
            string Component_Payroll = Transaction.Formula;
            string Component_Value = Transaction.Component_Value;
            try
            {
                List<vw_Employee_Component_Formula> EmployeePayrollComponent = Employee_Payroll_Component.Where(p => p.Employee_id == Transaction.Employee_ID).ToList();


                #region Initialize Get data
                var FormulaParamter = GetFormulaParamter();
                string CompGroupOprator = FormulaParamter.Where(p => p.Param_Code == "FC#CompGroupOprator").FirstOrDefault().Value;
                string[] CompTag = FormulaParamter.Where(p => p.Param_Code == "FC#CompTag").FirstOrDefault().Value.Split(',');
                string[] Ignore = FormulaParamter.Where(p => p.Param_Code == "FC#Ignore").FirstOrDefault().Value.Split('.');
                string[] Operator = FormulaParamter.Where(p => p.Param_Code == "FC#Operator").FirstOrDefault().Value.Split('.');
                string Cleanser = FormulaParamter.Where(p => p.Param_Code == "FC#Spliter").FirstOrDefault().Value;
                bool WithValue = bool.Parse(FormulaParamter.Where(p => p.Param_Code == "FC#WithValue").FirstOrDefault().Value);
                string isFunction = FormulaParamter.Where(p => p.Param_Code == "FC#Function").FirstOrDefault().Value;
                string OriginalTag = FormulaParamter.Where(p => p.Param_Code == "FC#OriginalTag").FirstOrDefault().Value; 
                #endregion


                #region function Formula
                #region Prepare Data Converted
                List<string> EmptyString = new List<string>();
                EmptyString.Add("");
                foreach (string op in Operator)
                {
                    Formula = Formula.Replace(op, Cleanser);
                }
                char Split = Convert.ToChar(Cleanser);
                List<string> ListConvertedComponent = Formula.Split(Split).ToList();
                if (Formula.Contains(isFunction))
                    ListConvertedComponent.Remove(ListConvertedComponent[0]);
                ListConvertedComponent = ListConvertedComponent.Except(EmptyString).ToList();
                #endregion

                //is Original
                bool is_Original = false;
                string strOriginalTag = "";

            ReCalculate:
                bool FinishConverted = true;
                List<string> ModifiedComp = new List<string>();
                foreach (var Component in ListConvertedComponent)
                {
                    if (!Ignore.Any(Component.Trim().Contains))
                    { 
                        strOriginalTag = ""; 
                        string ComponentFormula = Component.Trim();

                        foreach(var item in CompTag)
                        {
                            if (ComponentFormula.Contains(OriginalTag))
                            {
                                strOriginalTag = OriginalTag;
                                ComponentFormula = ComponentFormula.Replace(OriginalTag, "");
                            }

                            ComponentFormula = ComponentFormula.Replace(item, "");
                        }
                        List<string> CompCodeFromGroup = EmployeePayrollComponent.Where(p => p.Component_Group == ComponentFormula).Select(p => p.Organization_Payroll_Component_Code).Distinct().ToList();
                        
 
                        if (CompCodeFromGroup.Count() > 0)
                        {
                            FinishConverted = false;
                            string Value = "(";
                            foreach (var EmpComponent in CompCodeFromGroup)
                            {
                                Value = Value + CompTag[1] + EmpComponent + strOriginalTag + CompTag[0] + "+";
                                ModifiedComp.Add(CompTag[1] + EmpComponent + strOriginalTag + CompTag[0]);
                            }
                            ComponentFormula = Value.Substring(0, Value.Length - 1) + ")";
                            Component_Payroll = Component_Payroll.Replace(Component.Trim(), ComponentFormula);
                        }
                        else
                        {
                            var EmployeeFormula = EmployeePayrollComponent.Where(p => p.Organization_Payroll_Component_Code == ComponentFormula && Calculcation.tbl_Payroll_Period_Detail.Period_Start_Date >= p.Start_Date && p.End_Date >= Calculcation.tbl_Payroll_Period_Detail.Period_End_Date).OrderByDescending(p => p.End_Date).FirstOrDefault();
                            if (EmployeeFormula != null)
                            {
                                if (WithValue)
                                {
                                    ComponentFormula = EmployeeFormula.Formula + strOriginalTag;
                                    Component_Payroll = Component_Payroll.Replace(Component.Trim(), ComponentFormula);
                                }
                            }
                            else
                            {
                                if (Component.Contains(CompTag[1]) && Component.Contains(CompTag[0]) && !Component.Contains("OPCC"))
                                {
                                    ComponentFormula = "0";
                                    Component_Payroll = Component_Payroll.Replace(Component.Trim(), ComponentFormula);
                                }
                            }
                        }

                    }
                }
                if (!FinishConverted)
                {
                    ListConvertedComponent = ModifiedComp;
                    goto ReCalculate;
                }
                #endregion
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
                Component_Payroll = Component_Formula;
            }
            return Component_Payroll;
        }

        public static void WriteTxtLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFileBatch.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteTxtLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFileBatch.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static double ConvertToDouble(string Value)
        {
            double Result = 0;
            Result = string.IsNullOrEmpty(Value) || Value == " " ? 0 : Convert.ToDouble(Value);
            return Result;
        }


        public tbl_General_Parameter ConvertGeneralParameter_Data(tbl_General_Parameter m)
        {
            string Result = string.Empty;
            switch (m.Table_Name)
            {
                case "ACCOUNT_FUND_REQUISITION":
                    m.Description = m.Description.Replace("\r\n", "|");
                    break;   
            }
            return m;
        }

        public tbl_General_Parameter ConvertGeneralParameter_View(tbl_General_Parameter m)
        { 
            switch (m.Table_Name)
            {
                case "ACCOUNT_FUND_REQUISITION":
                    m.Description = m.Description.Replace("|", "\r\n");
                    break; 
            }
            return m;
        }

        public static string ConvertStatusLeave(int? intStatusCode, int? intAuthorizeStatus)
        {
            string statusDescription = "";

            if (intStatusCode == 1 && intAuthorizeStatus == 0) // pending 1 0
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_LEAVE_PENDING;
            }

            else if (intStatusCode == 1 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_LEAVE_APPROVED;
            }

            else if (intStatusCode == 2 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_LEAVE_REJECT;
            }

            else if (intStatusCode == 0 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_LEAVE_CANCEL;
            }

            return statusDescription;
        }
        

        public static string ConvertStatusTimeline(int? intStatusCode, int? intAuthorizeStatus)
        {
            string statusDescription = "";

            if (intStatusCode == 1 && intAuthorizeStatus == 0) // pending 1 0
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_TIMELINE_PENDING;
            }

            else if (intStatusCode == 1 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_TIMELINE_APPROVED;
            }

            else if (intStatusCode == 2 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_TIMELINE_REJECT;
            }

            else if (intStatusCode == 0 && intAuthorizeStatus == 1)
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_TIMELINE_CANCEL;
            }

            return statusDescription;
        }
        
        private static List<tbl_SysParam> GetFormulaParamter()
        {
            List<tbl_SysParam> FormulaParam = new List<tbl_SysParam>();
            List<string> FormulaConverter = new List<string>();
            FormulaConverter.Add("FC#CompGroupOprator");
            FormulaConverter.Add("FC#CompTag");
            FormulaConverter.Add("FC#Ignore");
            FormulaConverter.Add("FC#Operator");
            FormulaConverter.Add("FC#Spliter");
            FormulaConverter.Add("FC#WithValue");
            FormulaConverter.Add("FC#Function");
            FormulaConverter.Add("FC#OriginalTag"); 
            return (FormulaParam = UICommonFunction.GetSysParam(FormulaConverter));
        }
        #region convert Str To float round 0000,00
        public static float ToFloat(string Value)
        {
            if (Value.Length == 0)
            { return 0; }
            else
            {
                decimal Rate = Decimal.Parse(Value);
                Rate = Math.Round(Rate, 3);
                string strRate = Rate.ToString();
                float NewValue = float.Parse(strRate);
                return (NewValue);
            }
        }
        #endregion

        #region GetErrorDescription
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 25 November 2016
        /// Purpose : To get error description from specified error code
        /// </summary>
        /// <param name="strErrorCode"></param>
        /// <param name="strLanguage"></param>
        /// <returns></returns>
        public static string GetErrorDescription(string strErrorCode, string strLanguage)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string strErrorDescription = string.Empty;
            try
            {
                tbl_Error_Code errorCodeModels = db.tbl_Error_Code.Where(p => p.Error_Code == strErrorCode && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && p.Error_Language == strLanguage.ToUpper()).FirstOrDefault();
                if (errorCodeModels != null)
                {
                    strErrorDescription = errorCodeModels.Error_Description;
                }

                return strErrorDescription;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return strErrorDescription;
        }

        public static string GetErrorDescriptionOnly(string strErrorCode, string strLanguage)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string strErrorDescription = string.Empty;
            try
            {
                tbl_Error_Code errorCodeModels = db.tbl_Error_Code.Where(p => p.Error_Code == strErrorCode && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE && p.Error_Language == strLanguage.ToUpper()).FirstOrDefault();
                if (errorCodeModels != null)
                {
                    strErrorDescription = errorCodeModels.Error_Description;
                }

                return strErrorDescription;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return strErrorDescription;
        }

        public static bool StringIsNullOrEmpty(string value)
        {
            if (value == null)
                return true;
            if (value.Trim() == "")
                return true;
            else
                return false;
        }

        public static bool? StatusPeriodPermanent(Guid? Payroll_Period_Id)
        {
            bool? Status = false;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var chkPeriod = db.Check_Payroll_Period(Payroll_Period_Id).FirstOrDefault();
            if (chkPeriod != null)
                Status = chkPeriod.PeriodPermanentStatus;
            return Status;
        }

        public static bool? StatusPeriodNonPermanent(Guid? Payroll_Period_Id)
        {
            bool? Status = false;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var chkPeriod = db.Check_Payroll_Period(Payroll_Period_Id).FirstOrDefault();
            if (chkPeriod != null)
                Status = chkPeriod.PeriodNonPermanentStatus;
            return Status;
        }

        public static string GetLastLogin(Guid userId)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string data = string.Empty;
            try
            {
                tbl_Log_System logSystem = db.tbl_Log_System.Where(p => p.User_ID == userId && p.Menu_Name == "Login").OrderByDescending(p => p.Access_DateTime).FirstOrDefault();
                if (logSystem != null)
                {
                    data = string.Format("{0:dd/MM/yyyy HH:mm}", logSystem.Access_DateTime);
                }
                else
                {
                    data = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                }

                return data;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return data;
        }



        public static string DisplayErorr(List<Global_Error_Code> inputMessage, string code = "")
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string errMessage = string.Empty;
            if (inputMessage == null || inputMessage.Count() == 0)
            {
                return errMessage;
            }
            else
            {
                //errMessage = "-";
                var modelerror = inputMessage.Where(p => p.Error_Code == code).ToList();
                foreach (var item in modelerror)
                {
                    errMessage = errMessage + "-" + item.Error_Description + " \n";
                }
            }
            return errMessage;
        }

        #endregion

        #region Auto generate Organization Code
        public static tbl_SysParam GetAutoGenerate(string Param_Code)
        {
            tbl_SysParam model = null;
            string FinalCode = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                model = db.tbl_SysParam.Where(p => p.Param_Code == "CODE" + Param_Code).FirstOrDefault();
                switch (Param_Code)
                {
                    case "_ORGANIZATION":
                        model.Value = (int.Parse(model.Value) + 1).ToString("0000");
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    case "_ORGANIZATION_TRIAL":
                        model.Value = (int.Parse(model.Value) + 1).ToString("0000");
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    case "_ORGANIZATION_GROUP":
                        model.Value = (int.Parse(model.Value) + 1).ToString("0000");
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                }
                return (model);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return model;
        }
        #endregion

        #region Auto Generate General Number
        public static string GetAutoGenerateGeneralNumber(string param)
        {
            string strAutoGenerate = null;
            string finalCode = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                tbl_General_Number model = db.tbl_General_Number.Where(p => p.Type == param).FirstOrDefault();
                if (model != null)
                {
                    if (model.Format_Number != null)
                    {
                        //  strAutoGenerate = (int.Parse(model.Number_Value) + 1).ToString(model.Format_Number);
                        var prefix = Regex.Match(model.Format_Number, "^\\D+").Value;
                        var number = Regex.Replace(model.Number_Value, "^\\D+", "");
                        var i = int.Parse(number) + 1;
                        strAutoGenerate = prefix + i.ToString(new string('0', number.Length));
                    }
                    else
                    {
                        strAutoGenerate = (int.Parse(model.Number_Value) + 1).ToString("00000");
                    }
                }
                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Auto Generate General Number
        public static string SetGeneralNumber(string param)
        {
            string strAutoGenerate = null;
            string finalCode = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                tbl_General_Number model = db.tbl_General_Number.Where(p => p.Type == param).FirstOrDefault();
                if (model != null)
                {
                    if (model.Format_Number != null)
                    {
                        //  strAutoGenerate = (int.Parse(model.Number_Value) + 1).ToString(model.Format_Number);
                        var prefix = Regex.Match(model.Format_Number, "^\\D+").Value;
                        var number = Regex.Replace(model.Number_Value, "^\\D+", "");
                        var i = int.Parse(number) + 1;
                        strAutoGenerate = prefix + i.ToString(new string('0', number.Length));
                        model.Number_Value = i.ToString(new string('0', number.Length));
                        db.SaveChanges();
                    }
                    else
                    {
                       var NumberPlus= int.Parse(model.Number_Value) + 1;
                       model.Number_Value = NumberPlus.ToString() ;
                        db.SaveChanges();
                        strAutoGenerate = (int.Parse(model.Number_Value) + 1).ToString("00000");
                    }
                }

                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Auto Generate Batch Number
        public static string SetBatchNumber()
        {
            string strAutoGenerate = null;
            string finalCode = string.Empty;
            try
            {
                ReNumber:
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                tbl_General_Number model = db.tbl_General_Number.Where(p => p.Type == "BATCH_NUMBER").FirstOrDefault();
                if (model != null)
                { 
                    strAutoGenerate = model.Format_Number;
                    strAutoGenerate = strAutoGenerate.Replace("YYYY", DateTime.Now.Year.ToString());
                    strAutoGenerate = strAutoGenerate.Replace("MM", DateTime.Now.Month.ToString("d2"));
                    strAutoGenerate = strAutoGenerate.Replace("DD", DateTime.Now.Day.ToString("d2"));
                    strAutoGenerate = strAutoGenerate.Replace("NO", (int.Parse(model.Number_Value) + 1).ToString("0000"));
                    model.Number_Value = (int.Parse(model.Number_Value) + 1).ToString("0000");
                    db.Entry(model).State = EntityState.Modified;
                    if (db.vw_Calculation_Summary.Where(p => p.Batch == strAutoGenerate && p.Calculation_Type != "Severance").Take(1).Count() > 0)
                        goto ReNumber; 
                    db.SaveChanges();
                }

                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Auto Generate Payslip Number
        public static string GeneratePayslipNumber()
        {
            string strAutoGenerate = null;
            string finalCode = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                tbl_General_Number model = db.tbl_General_Number.Where(p => p.Type == "PAYSLIP_NUMBER").FirstOrDefault();
                if (model != null)
                {
                    strAutoGenerate = model.Format_Number;
                    strAutoGenerate = strAutoGenerate.Replace("DD", DateTime.Now.Day.ToString("D2"));
                    strAutoGenerate = strAutoGenerate.Replace("MM", DateTime.Now.Month.ToString("D2"));
                    strAutoGenerate = strAutoGenerate.Replace("NO", (int.Parse(model.Number_Value) + 1).ToString("D10"));
                    model.Number_Value = (int.Parse(model.Number_Value) + 1).ToString("D10");
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Auto Generate Loan Number
        public static string GetAutoGenerateLoanNumber(Guid orgID, string empNo)
        {
            string strAutoGenerate = null;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                vw_Loan model = db.vw_Loan.Where(p => p.Organization_ID == orgID).OrderByDescending(o => o.Loan_No).FirstOrDefault();
                if (model != null)
                {
                    string[] splitValue = model.Loan_No.Split('-');
                    string stringNo = splitValue[splitValue.Length - 1];

                    var i = int.Parse(stringNo) + 1;
                    strAutoGenerate = empNo + '-' + i.ToString();
                }
                else
                {
                    var i = 1;
                    strAutoGenerate = empNo + '-' + i.ToString();
                }
                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion
		
		        #region Auto generate Payment OrderID Midtrans
        public static string GetAutoGeneratePaymentOrderIDMidtrans(string Param_Code)
        {
            tbl_SysParam model = null;
            string strAutoGenerate = null;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                model = db.tbl_SysParam.Where(p => p.Param_Code == Param_Code).FirstOrDefault();
                model.Value = (int.Parse(model.Value) + 1).ToString("0000");
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                string dateTimeNow = DateTime.Now.ToString("MM/dd/yy");
                string[] splitDate = dateTimeNow.Split('/');
                strAutoGenerate = splitDate[0] + model.Value + "/Q/BPO7/" + splitDate[2];
                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Auto Generate Payment OrderID Details Midtrans 
        public static string GetAutoGeneratePaymentOrderIDDetailsMidtrans(string value)
        {
            string strAutoGenerate = null;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                int intValue = 0;
                Int32.TryParse(value, out intValue);
                tbl_Package_Pricing model = db.tbl_Package_Pricing.Where(q => q.Value == intValue).FirstOrDefault();
                if (model != null)
                {
                    strAutoGenerate = model.Package_Name + '-' + model.Value;
                }
                return strAutoGenerate;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return strAutoGenerate;
        }
        #endregion

        #region Convert Array To List
        public static List<string> ConvertArrayToList(string[] inputArray)
        {
            List<string> getlist = new List<string>();
            foreach (var item in inputArray.ToList())
            {
                getlist.Add(item);
            }
            return getlist;
        }
        #endregion

        #region Convert List String "1","2" To Day "Senin","Selasa"
        public static List<string> ConvertStringToDay(List<string> inputStringHari)
        {
            List<string> getlist = new List<string>();
            foreach (var item in inputStringHari.ToList())
            {
                var hari = string.Empty;
                switch (item)
                {
                    case "1":
                        getlist.Add("Monday");
                        break;
                    case "2":
                        getlist.Add("Tuesday");
                        break;
                    case "3":
                        getlist.Add("Wednesday");
                        break;
                    case "4":
                        getlist.Add("Thursday");
                        break;
                    case "5":
                        getlist.Add("Friday");
                        break;
                    case "6":
                        getlist.Add("Saturday");
                        break;
                    default:
                        getlist.Add("Sunday");
                        break;
                }
            }
            return getlist;
        }
        #endregion

        #region Convert List String "1","2" To Month "Januari","Februari"
        public static List<string> ConvertStringToMonth(List<string> inputStringBulan)
        {
            List<string> getlist = new List<string>();
            foreach (var item in inputStringBulan.ToList())
            {
                var hari = string.Empty;
                switch (item)
                {
                    case "1":
                        getlist.Add("Januari");
                        break;
                    case "2":
                        getlist.Add("Februari");
                        break;
                    case "3":
                        getlist.Add("Maret");
                        break;
                    case "4":
                        getlist.Add("April");
                        break;
                    case "5":
                        getlist.Add("Mei");
                        break;
                    case "6":
                        getlist.Add("Juni");
                        break;
                    case "7":
                        getlist.Add("Juli");
                        break;
                    case "8":
                        getlist.Add("Agustus");
                        break;
                    case "9":
                        getlist.Add("September");
                        break;
                    case "10":
                        getlist.Add("Oktober");
                        break;
                    case "11":
                        getlist.Add("Novemver");
                        break;
                    default:
                        getlist.Add("Desember");
                        break;
                }
            }
            return getlist;
        }
        #endregion

        #region Convert DateTime Get int Month from Datetime"
        public static int ConvertNumberToMonth(DateTime date)
        {
            int Bln = date.Month;
            return Bln;
        }
        #endregion


        #region Search Day To "1","2"
        public static string RecordDayUpper(string StrDay)
        {
            if (StrDay == "MONDAY" || StrDay == "TUESDAY" || StrDay == "WEDNESDAY" || StrDay == "THURSDAY" || StrDay == "FRIDAY" || StrDay == "SATURDAY" || StrDay == "SUNDAY")
            {
                StrDay = SearchDay(StrDay);
                return StrDay;
            }
            return StrDay;
        }

        public static string SearchDay(string recordStatus)
        {

            switch (recordStatus)
            {
                case "MONDAY":
                    recordStatus = "1";
                    break;
                case "TUESDAY":
                    recordStatus = "2";
                    break;
                case "WEDNESDAY":
                    recordStatus = "3";
                    break;
                case "THURSDAY":
                    recordStatus = "4";
                    break;
                case "FRIDAY":
                    recordStatus = "5";
                    break;
                case "SATURDAY":
                    recordStatus = "6";
                    break;
                case "SUNDAY":
                    recordStatus = "7";
                    break;
                default:
                    return recordStatus;
            }
            return recordStatus;
        }
        #endregion

        #region Search MMM-yyyy
        public static string RecordMonthUpper(string Month)
        {
            Month = Month.ToUpper();
            if (Month == "JAN" || Month == "FEB" || Month == "MAR" || Month == "APR" || Month == "MAY" || Month == "JUN" || 
                Month == "JUL" || Month == "AUG" || Month == "SEP" || Month == "OCT" || Month == "NOV" || Month == "DEC")
            {
                Month = SearchMonth(Month);
                return Month;
            }
            return Month;
        }

        public static string SearchMonth(string Month)
        {

            switch (Month)
            {
                case "JAN":
                    Month = "1";
                    break;
                case "FEB":
                    Month = "2";
                    break;
                case "MAR":
                    Month = "3";
                    break;
                case "APR":
                    Month = "4";
                    break;
                case "MAY":
                    Month = "5";
                    break;
                case "JUN":
                    Month = "6";
                    break;
                case "JUL":
                    Month = "7";
                    break;
                case "AUG":
                    Month = "8";
                    break;
                case "SEP":
                    Month = "9";
                    break;
                case "OCT":
                    Month = "10";
                    break;
                case "NOV":
                    Month = "11";
                    break;
                case "DEC":
                    Month = "12";
                    break;
                default:
                    return Month;
            }
            return Month;
        }
        #endregion

        #region Search Status Code
        public static string RecordStatusUpper(string StrStatus)
        {
            if (StrStatus == "ACTIVE" || StrStatus == "INACTIVE" || StrStatus == "REJECTED" || StrStatus == "DELETED")
            {
                StrStatus = SearchRecordStatus(StrStatus);
                return StrStatus;
            }
            return StrStatus;
        }
        public static string SearchRecordStatus(string recordStatus)
        {
            if (("ACTIVE".Contains(recordStatus)))
            {
                recordStatus = "1";
            }
            if (("INACTIVE".Contains(recordStatus)))
            {
                recordStatus = "0";
            }
            if (("REJECTED".Contains(recordStatus)))
            {
                recordStatus = "2";
            }
            if (("DELETED".Contains(recordStatus)))
            {
                recordStatus = "999";
            }

            //switch (recordStatus)
            //{
            //    case "ACTIVE":
            //        recordStatus = "1";
            //        break;
            //    case "INACTIVE":
            //        recordStatus = "0";
            //        break;
            //    case "REJECTED":
            //        recordStatus = "2";
            //        break;
            //    case "DELETED":
            //        recordStatus = "999";
            //        break;
            //    default:
            //        return recordStatus;
            //}
            return recordStatus;
        }

        public static string SearchStatusMobile(string recordStatus)
        {
            if (("ACTIVE".Contains(recordStatus)))
            {
                recordStatus = "1";
            }
            if (("INACTIVE".Contains(recordStatus)))
            {
                recordStatus = "0";
            }
            if (("REJECTED".Contains(recordStatus)))
            {
                recordStatus = "2";
            }
            if (("DELETED".Contains(recordStatus)))
            {
                recordStatus = "999";
            }
            return recordStatus;
        }
        #endregion

        #region Search Authorize Status
        public static string AuthorizeUpper(string StrAuthorize)
        {
            if (StrAuthorize == "AUTHORIZE" || StrAuthorize == "UNAUTHORIZE")
            {
                StrAuthorize = SearchAuthorizeStatus(StrAuthorize);
                return StrAuthorize;
            }
            return StrAuthorize;
        }
        public static string SearchAuthorizeStatus(string AuthorizeStatus)
        {
            if (("AUTHORIZE".Contains(AuthorizeStatus)))
            {
                AuthorizeStatus = "1";
            }
            if (("UNAUTHORIZE".Contains(AuthorizeStatus)))
            {
                AuthorizeStatus = "0";
            }

            //switch (AuthorizeStatus)
            //{
            //    case "AUTHORIZE":
            //        AuthorizeStatus = "1";
            //        break;
            //    case "UNAUTHORIZE":
            //        AuthorizeStatus = "0";
            //        break;
            //    default:
            //        return AuthorizeStatus;
            //}
            return AuthorizeStatus;
        }

        public static int IntSearchAuthorizeStatus(string AuthorizeStatus)
        {
            int intAuthorizeStatus = 0;
            AuthorizeStatus = AuthorizeStatus.ToUpper();
            switch (AuthorizeStatus)
            {
                case "AUTHORIZE":
                    AuthorizeStatus = "1";
                    break;
                case "UNAUTHORIZE":
                    AuthorizeStatus = "0";
                    break;
                default:
                    return intAuthorizeStatus;
            }
            return intAuthorizeStatus;
        }

        public static int IntSearchCalculateStatus(string CalculateStatus)
        {
            int intCalculateStatus = 0;
            CalculateStatus = CalculateStatus.ToUpper();
            switch (CalculateStatus)
            {
                case "SUCCESS":
                    intCalculateStatus = 1;
                    break;
                case "FAIL":
                    intCalculateStatus = -1;
                    break;
                case "IN PROGRESS":
                    intCalculateStatus = 0;
                    break;
            }
            return intCalculateStatus;
        }


        #endregion

        public static string MergeCheckedUnchecked(string Checked, string Unchecked)
        {
            if (!(Checked == "" && Unchecked == ""))
            {
                if (string.IsNullOrEmpty(Checked))
                    Checked = ",";
                if (string.IsNullOrEmpty(Unchecked))
                    Unchecked = ",";

                string[] ListChecked = Checked.Split(',');
                string[] ListUnchecked = Unchecked.Split(',');
                foreach (var CheckedId in ListChecked)
                {
                    if (ListUnchecked.Contains(CheckedId) && CheckedId != "")
                    {
                        Checked = Checked.Replace("," + CheckedId, "");
                    }
                }
            }

            return Checked;
        }

        public static bool CountWorkingDay(DateTime StarDate, DateTime EndDate,string WorkingTime, List<DateTime> HolidayCalendar,out TimeSpan? Diffdate,out List<DateTime> ListLeaveDate)
        {
            bool Isholiday= false;
           int countday= 0;
            //DateTime? enddate = DateTime.Parse(EndDate);
            //DateTime? startdate = DateTime.Parse(StarDate);
            List<DateTime> ListDtLeave = new List<DateTime>();
            var arrayday = WorkingTime.Split(',');
           
            for (DateTime? date = StarDate; date <= EndDate; date = date.Value.AddDays(1))
            {
                var tempholiday = Array.IndexOf(HolidayCalendar.ToArray(), date);
                if (tempholiday > -1)
                {

                }
                else
                {
                    var TempDay = (int)date.Value.DayOfWeek;
                    int pos = Array.IndexOf(arrayday, TempDay.ToString());
                    if (pos > -1)
                    {
                        Isholiday = false;
                        ListDtLeave.Add(date.Value);
                        countday++;
                    }
                    else
                    {
                        Isholiday = true;
                    }
                }
            }
            Diffdate= TimeSpan.Parse(countday + ".00:00:00.0000");
            ListLeaveDate = ListDtLeave;
            return Isholiday;
        }

        public static int CountLeaveCross(List<vw_Leave_Employee> paramLeaveEmployee, DateTime startLeave, List<DateTime> dtLeave)
        {
            int countday = 0;
            for (DateTime? date = startLeave; date <= paramLeaveEmployee.FirstOrDefault().Period_End; date = date.Value.AddDays(1))
            {
                foreach (var item in dtLeave)
                {
                    if(date==item)
                    {
                        countday= countday+1;
                    }
                }
            }
            return countday;
        }

        public static string AutoGenerateNumberEF(Guid OrganizationID, string OrganizationCode)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string finalnumber = "";
            try
            {
                int lastnumber = int.Parse(db.tbl_SysParam.Where(p => p.Param_Code == "Default_RefNumber_ExchangeFile").FirstOrDefault().Value); // default 0001
                var LastRecord = db.tbl_Exchange_File.Where(p => p.Organization_ID == OrganizationID).OrderByDescending(p => p.Ref_Number.Replace(OrganizationCode + "_", "".ToString())).FirstOrDefault();

                if (LastRecord != null)
                {
                    lastnumber = (int.Parse(LastRecord.Ref_Number.Replace(OrganizationCode + "_", "")) + 1);
                    
                    if (lastnumber.ToString().Length == 1)
                    {
                        finalnumber = OrganizationCode + "_000" + (lastnumber).ToString();
                    }
                    else if (lastnumber.ToString().Length == 2)
                    {
                        finalnumber = OrganizationCode + "_00" + (lastnumber).ToString();
                    }
                    else if (lastnumber.ToString().Length == 3)
                    {
                        finalnumber = OrganizationCode + "_0" + (lastnumber).ToString();
                    }
                    else
                    {
                        finalnumber = OrganizationCode + "_" + (lastnumber).ToString();
                    }
                }
                else
                {
                    finalnumber = OrganizationCode + "_000" + (lastnumber).ToString();
                }
                return (finalnumber);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "UICommonFunction.AutoGenerateNumberEF");
                return (finalnumber);
            }

        }

        public static string AutoGenerateNumberOG()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string finalnumber = "";
            try
            {
                int lastnumber = int.Parse(db.tbl_SysParam.Where(p => p.Param_Code == "Default_Organization_Code").FirstOrDefault().Value.Replace("GRP_","")); // default GRP_0001
                var modelLastnumberlst = GeneralCore.OrganizationGroupSummaryQuery().OrderByDescending(p => p.Created_DateTime).ToList();
                var modelLastnumber = GeneralCore.OrganizationGroupSummaryQuery().OrderByDescending(p => p.Created_DateTime).FirstOrDefault();

                if(modelLastnumber != null)
                {
                    lastnumber = (int.Parse(modelLastnumber.Group_Code.Replace("GRP_",""))) + 1;
                }
                if (modelLastnumber != null)
                {
                    if (lastnumber.ToString().Length == 1)
                    {
                        finalnumber = "GRP_000" + (lastnumber).ToString();
                    }
                    else if (lastnumber.ToString().Length == 2)
                    {
                        finalnumber = "GRP_00" + (lastnumber).ToString();
                    }
                    else if (lastnumber.ToString().Length == 3)
                    {
                        finalnumber = "GRP_0" + (lastnumber).ToString();
                    }
                    else
                    {
                        finalnumber = "GRP_" + (lastnumber).ToString();
                    }
                }
                else
                {
                    finalnumber = "GRP_000" + (lastnumber).ToString();
                }
                return (finalnumber);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "UICommonFunction.AutoGenerateNumberEF");
                return (finalnumber);
            }

        }

        public static List<SelectListItem> GroupSelectList(List<string> Data)
        {
            List<SelectListItem> GroupSelectListData = new List<SelectListItem>();
            bool firstStep = true;
            foreach (var strData in Data)
            {
                if (strData.Contains("|"))
                {
                    string[] arryData = strData.Split('|');
                    foreach (string item in arryData)
                    {
                        if (firstStep)
                        {
                            firstStep = false;
                            GroupSelectListData.Add(new SelectListItem
                            {
                                Text = item.ToString(),
                                Value = item.ToString()
                            });
                        }
                        else if (GroupSelectListData.Where(p => p.Value == item).FirstOrDefault() == null)
                        {
                            firstStep = false;
                            GroupSelectListData.Add(new SelectListItem
                            {
                                Text = item.ToString(),
                                Value = item.ToString()
                            });
                        }
                    }

                }
                else
                {
                    if (firstStep)
                    {
                        firstStep = false;
                        GroupSelectListData.Add(new SelectListItem
                        {
                            Text = strData.ToString(),
                            Value = strData.ToString()
                        });
                    }
                    else if (GroupSelectListData.Where(p => p.Value == strData).FirstOrDefault() == null)
                    {
                        firstStep = false;
                        GroupSelectListData.Add(new SelectListItem
                        {
                            Text = strData.ToString(),
                            Value = strData.ToString()
                        });
                    }
                }
            }
            return GroupSelectListData;
        }
        public static string ConvertAuthorizeStatus(int? intAuthorizeStatus)
        {
            string AuthorizeStatusDescription = "";
            switch (intAuthorizeStatus)
            {
                case 0:
                    AuthorizeStatusDescription = "Unauthorize";
                    break;
                case 1:
                    AuthorizeStatusDescription = "Authorize";
                    break;
            }

            return AuthorizeStatusDescription;
        }

        public static bool CheckAlreadyUsedWorkingtime(string code)
        {
            List<vw_Employee_Appointment_WorkingTime> ListData = GeneralCore.AppointmentWorkingTimeQuery().Where(p => p.Working_Time_Code == code).Distinct().ToList();
            if (ListData.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static string ConvertAuthorizeStatus(string intAuthorizeStatus)
        {
            string AuthorizeStatusDescription = "";
            switch (intAuthorizeStatus)
            {
                case "0":
                    AuthorizeStatusDescription = GlobalVariable.CONST_STR_UNAUTHORIZED;
                    break;
                case "1":
                    AuthorizeStatusDescription = GlobalVariable.CONST_STR_AUTHORIZED;
                    break;
            }

            return AuthorizeStatusDescription;
        }



        public static string MappingUploadCategory(string Control)
        {
            string CategoryUpload = Control;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var Mapping_Upload_Category = db.tbl_SysParam.Where(p => p.Param_Code == "Mapping_Upload_Category").FirstOrDefault(); ;

            try
            { 
            if (Mapping_Upload_Category != null)
            {
                if(Mapping_Upload_Category.Value.Contains(Control) && Mapping_Upload_Category.Value.Contains(":") && Mapping_Upload_Category.Value.Contains("|"))
                {
                    string[] List_Category = Mapping_Upload_Category.Value.Split('|');
                    foreach (string Category in List_Category)
                    {
                        if (Category.Contains(Control))
                        {
                            string[] Upload_Category = Category.Split(':');
                            CategoryUpload = Upload_Category[1];
                        }
                    } 
                }
            }
            }
            catch(Exception E)
            {
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }

            return CategoryUpload;
        }

        public static string ConvertStatusCode(int? intStatusCode)
        {
            string statusDescription = "";
            switch (intStatusCode)
            {
                case 0:
                    statusDescription = GlobalVariable.CONST_STR_STATUS_INACTIVE;
                    break;
                case 1:
                    statusDescription = GlobalVariable.CONST_STR_STATUS_ACTIVE;
                    break;
                case 2:
                    statusDescription = GlobalVariable.CONST_STR_STATUS_REJECT;
                    break;
                case 999:
                    statusDescription = GlobalVariable.CONST_STR_STATUS_DELETED;
                    break;
            }

            return statusDescription;
        }

        public static string ConvertCalcuationStatus(int? intStatusCode)
        {
            string statusDescription = "";
            switch (intStatusCode)
            {
                case 0:
                    statusDescription = GlobalVariable.CONST_CALCULATION_STR_STATUS_IN_PROGRESS;
                    break;
                case 1:
                    statusDescription = GlobalVariable.CONST_CALCULATION_STR_STATUS_SUCCESS;
                    break;
                case 2:
                    statusDescription = GlobalVariable.CONST_CALCULATION_STR_STATUS_FAIL;
                    break;
            }

            return statusDescription;
        }

        public static int ConvertStringToInteger(string strValue)
        {
            int intValue = 0;

            if (IsInteger(strValue))
                intValue = Convert.ToInt32(strValue);

            return intValue;
        }

        public static string ConvertGeneralParameter(string strCode)
        {
            string description = "";
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var generalparam = db.tbl_General_Parameter.Where(p => p.Field_Value == strCode).FirstOrDefault();
            if (generalparam != null)
            {
                description = generalparam.Field_Value + "-" + generalparam.Field_Name;
            }
            return description;
        }

        #region Search LDAP
        public static string LDAPUserUpper(string StrLDAP)
        {
            if (StrLDAP == "NON LDAP USER" || StrLDAP == "LDAP USER" || StrLDAP == "NON")
            {
                StrLDAP = SearchLDAP(StrLDAP);
                return StrLDAP;
            }
            return StrLDAP;
        }
        public static string SearchLDAP(string LDAPStatus)
        {
            if (LDAPStatus == "LDAP USER")
            {
                LDAPStatus = "1";
            }
            else if (LDAPStatus == "NON LDAP USER" || LDAPStatus == "NON")
            {
                LDAPStatus = "0";
            }
            else
            {
                return LDAPStatus;
            }
            return LDAPStatus;
        }
        #endregion

        #region SearchTaxDeduction
        public static bool? SearchTaxDeduction(string taxdeduction)
        {
            bool? booltaxdeduction = null;
            switch (taxdeduction.ToUpper())
            {
                case "1":
                    booltaxdeduction = true;
                    break;
                case "0":
                    booltaxdeduction = false;
                    break;
                case "":
                    booltaxdeduction = null;
                    break;
            }
            return booltaxdeduction;
        }
        #endregion

        #region SearchFrequency
        public static string SearchFrequency(string freq)
        {
            string REGULAR = "REGULAR";
            string IRREGULAR = "IRREGULAR";
            bool regValue = false;
            bool iregValue = false;
            string retValue = "";
            if (REGULAR.IndexOf(freq.ToUpper()) != -1)
            {
                regValue = true;
            }
            else
            {
                regValue = false;
            }
            if (IRREGULAR.IndexOf(freq.ToUpper()) != -1)
            {
                iregValue = true;
            }
            else
            {
                iregValue = false;
            }
            if (iregValue == true && regValue == true)
            {
                retValue = "";
            }
            else if (iregValue == true && regValue == false)
            {
                retValue = "I";
            }
            else if (iregValue == false && regValue == true)
            {
                retValue = "R";
            }
            else
            {
                retValue = freq;
            }


            return retValue;
        }
        #endregion

        #region SearchFrequency
        public static string SearchAmountType(string AMT)
        {
            string FIX = "FIX AMOUNT";
            string FORMULA = "FORMULA";
            bool regValue = false;
            bool iregValue = false;
            string retValue = "";
            if (FIX.IndexOf(AMT.ToUpper()) != -1)
            {
                regValue = true;
            }
            else
            {
                regValue = false;
            }
            if (FORMULA.IndexOf(AMT.ToUpper()) != -1)
            {
                iregValue = true;
            }
            else
            {
                iregValue = false;
            }
            if (iregValue == true && regValue == true)
            {
                retValue = "";
            }
            else if (iregValue == true && regValue == false)
            {
                retValue = "I";
            }
            else if (iregValue == false && regValue == true)
            {
                retValue = "R";
            }
            else
            {
                retValue = AMT;
            }


            return retValue;
        }
        #endregion

        //public static List<string> ListYear()
        //{
        //    List<string> getlist = new List<string>();
        //    int yearNow = int.Parse(DateTime.Now.ToString("yyyy"));

        //    for (int i = 0; i < 3; i++)
        //    {
        //        string year = (yearNow + 1).ToString();
        //        getlist.Add(year);
        //    }
        //        return getlist;
        //}

        #region GetSessionLoginUser
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 15 December 2016
        /// Purpose : To get login user data (based on parameter) from session
        /// </summary>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        public static string GetSessionLoginUser(string strFieldName)
        {
            string strValue = string.Empty;

            try
            {
                //UserLoginModels userLogin = (UserLoginModels)HttpContext.Current.Session[GlobalVariable.CONST_SESSION_USERLOGIN];
                //if (userLogin != null)
                //{
                //    switch (strFieldName)
                //    {
                //        case GlobalVariable.CONST_FIELD_USER_ID:
                //            strValue = userLogin.User_ID.ToString();
                //            break;
                //        case GlobalVariable.CONST_FIELD_USERNAME:
                //            strValue = userLogin.Username;
                //            break;
                //        case GlobalVariable.CONST_FIELD_USER_FULLNAME:
                //            strValue = userLogin.Full_Name;
                //            break;
                //        case GlobalVariable.CONST_FIELD_ORGANIZATION_ID:
                //            strValue = userLogin.Organization_ID.ToString();
                //            break;
                //        case GlobalVariable.CONST_FIELD_ORGANIZATION_CODE:
                //            strValue = userLogin.Organization_Code;
                //            break;
                //        case GlobalVariable.CONST_FIELD_ORGANIZATION_SERVICE_ROLE_CODE:
                //            strValue = userLogin.Organization_Service_Role_Code;
                //            break;
                //    }
                //}

                return strValue;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return strValue;
        }
        #endregion

        #region GetOrganizationServiceRoleCode
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 30 December 2016
        /// Purpose : To get organization service role code
        /// </summary>
        /// <param name="strOrganizationCode"></param>
        /// <returns></returns>
        public static string GetOrganizationServiceRoleCode(string strOrganizationCode)
        {
            string strValue = string.Empty;

            try
            {
                //OrganizationServiceRoleRepository dbOrganizationServiceRole = new OrganizationServiceRoleRepository();
                //OrganizationServiceRoleViewModels orgServiceRoleView = new OrganizationServiceRoleViewModels();
                //orgServiceRoleView.organizationServiceRoleModels = new OrganizationServiceRoleModels();
                //orgServiceRoleView.organizationServiceRoleModels.Organization_Code = strOrganizationCode;
                //orgServiceRoleView.OrganizationServiceRoleList = new List<OrganizationServiceRoleModels>();
                //orgServiceRoleView.OrganizationServiceRoleList = dbOrganizationServiceRole.FindByCriteria(orgServiceRoleView).ToList();

                //if (orgServiceRoleView.OrganizationServiceRoleList != null && orgServiceRoleView.OrganizationServiceRoleList.Count > 0)
                //{
                //    foreach (var item in orgServiceRoleView.OrganizationServiceRoleList)
                //    {
                //        strValue = item.Service_Role_Code;
                //        break;
                //    }
                //}

                return strValue;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return strValue;
        }
        #endregion

        #region GetSelectedOrganization
        public static string GetSelectedOrganization(string strFieldName)
        {
            string strValue = string.Empty;

            try
            {
                if (HttpContext.Current.Session[GlobalVariable.CONST_SESSION_ORGANIZATION_CLIENT_LIST] != null)
                {
                    List<SelectListItem> lstOrganization = (List<SelectListItem>)HttpContext.Current.Session[GlobalVariable.CONST_SESSION_ORGANIZATION_CLIENT_LIST];

                    foreach (var item in lstOrganization)
                    {
                        if (item.Selected)
                        {
                            switch (strFieldName)
                            {
                                case GlobalVariable.CONST_FIELD_ORGANIZATION_ID:
                                    strValue = item.Value;
                                    break;
                                case GlobalVariable.CONST_FIELD_ORGANIZATION_CODE:
                                    strValue = item.Text;
                                    break;
                                case GlobalVariable.CONST_FIELD_ORGANIZATION_SERVICE_ROLE_CODE:
                                    strValue = GetOrganizationServiceRoleCode(item.Text);
                                    break;
                            }
                        }
                    }
                }

                return strValue;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return strValue;
        }
        #endregion

        #region GetSysParam
        public static List<tbl_SysParam> GetSysParam(string Param_Code)
        {
            List<tbl_SysParam> value = null;
            string strMessage = string.Empty;
            try
            { 
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                value = db.tbl_SysParam.Where(p => p.Param_Code == Param_Code).OrderBy(p => p.Value).ToList();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return value;
        }

        public static List<tbl_SysParam> GetSysParam(List<string> Param_Code)
        {
            List<tbl_SysParam> value = null;
            string strMessage = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                value = db.tbl_SysParam.Where(p => Param_Code.Contains(p.Param_Code)).OrderBy(p => p.Value).ToList();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return value;
        }
        #endregion

        #region ModelSysParam
        public static tbl_SysParam ModelSysParam(string Param_Code)
        {
            tbl_SysParam value = null;
            string strMessage = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                value = db.tbl_SysParam.Where(p => p.Param_Code == Param_Code).OrderBy(p => p.Value).FirstOrDefault();
                if (value == null)
                    value = new tbl_SysParam();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return value;
        }
        #endregion

        #region GetParameter
        public static string GetParameter(string Param_Code)
        {
            string value = null;
            string strMessage = string.Empty;
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                var Model = db.tbl_SysParam.Where(p => p.Param_Code == Param_Code).OrderBy(p => p.Value).FirstOrDefault();
                if (Model == null)
                    value = "";
                else
                    value = Model.Value;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return value;
        }
        #endregion

        #region GetSearchDateFromDB
        public static string GetSearchDateFromDB(string Filter) // Search Date From dd/MM/yyyy inDB Formated (yyyy/MM/dd)
        {

            DateTime Contain = DateTime.ParseExact(Filter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Filter = Contain.ToString("yyyy-MM-dd");
            return Filter;
        }
        #endregion

        #region Convertmmmyyyytodatetime
        public static string Convertmmmyyyytodatetime(string Filter)
        {
            if (Filter.Contains("JANUARY") || Filter.Contains("FEBRUARY") || Filter.Contains("MARCH") || Filter.Contains("APRIL") || Filter.Contains("MAY") ||
                Filter.Contains("JUNE") || Filter.Contains("JULY") || Filter.Contains("AUGUST") || Filter.Contains("SEPTEMBER") || Filter.Contains("OCTOBER") ||
                Filter.Contains("NOVEMBER") || Filter.Contains("DECEMBER"))
            {
                switch (Filter)
                {
                    case "JANUARY":
                        Filter = "1";
                        break;
                    case "FEBRUARY":
                        Filter = "0";
                        break;
                    case "MARCH":
                        Filter = "2";
                        break;
                    case "APRIL":
                        Filter = "999";
                        break;
                    case "MAY":
                        Filter = "1";
                        break;
                    case "JUNE":
                        Filter = "0";
                        break;
                    case "JULY":
                        Filter = "2";
                        break;
                    case "AUGUST":
                        Filter = "999";
                        break;
                    case "SEPTEMBER":
                        Filter = "1";
                        break;
                    case "OCTOBER":
                        Filter = "0";
                        break;
                    case "NOVEMBER":
                        Filter = "2";
                        break;
                    case "DECEMBER":
                        Filter = "999";
                        break;
                    default:
                        return Filter;
                }
            }
            DateTime Contain = DateTime.ParseExact(Filter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Filter = Contain.ToString("yyyy-MM-dd");
            return Filter;
        }
        #endregion

        #region PopulateListOrganization
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 30 December 2016
        /// Purpose : To populate list of organizations + client 
        /// </summary>
        /// <param name="strOrganizationID"></param>
        /// <param name="strOrganizationCode"></param>
        /// <returns></returns>
        public static List<SelectListItem> PopulateListOrganization(string strOrganizationID, string strOrganizationCode)
        {
            List<SelectListItem> lstResult = new List<SelectListItem>();
            try
            {
                //lstResult.Add(new SelectListItem { Text = strOrganizationCode, Value = strOrganizationID, Selected = true });

                //tbl_Client_Organization_Service_Role clientOrganizationServiceRole = new tbl_Client_Organization_Service_Role();
                //clientOrganizationServiceRole.clientOrganizationServiceRoleModels = new ClientOrganizationServiceRoleModels();
                //clientOrganizationServiceRole.tbl_Organization_Service_Role.Service_Organization_ID = Guid.Parse(strOrganizationID);

                //ClientOrganizationServiceRoleRepository dbClientOrganizationServiceRole = new ClientOrganizationServiceRoleRepository();
                //clientOrganizationServiceRole.ClientOrganizationServiceRoleList = new List<ClientOrganizationServiceRoleModels>();
                //clientOrganizationServiceRole.ClientOrganizationServiceRoleList = dbClientOrganizationServiceRole.FindByCriteria(clientOrganizationServiceRole).ToList();

                //if (clientOrganizationServiceRole.ClientOrganizationServiceRoleList != null && clientOrganizationServiceRole.ClientOrganizationServiceRoleList.Count > 0)
                //{
                //    foreach (var item in clientOrganizationServiceRole.ClientOrganizationServiceRoleList)
                //    {
                //        lstResult.Add(new SelectListItem { Text = item.Client_Organization_Code, Value = item.Client_Organization_ID.ToString() });
                //    }
                //}

                //if (HttpContext.Current.Session[GlobalVariable.CONST_SESSION_ORGANIZATION_CLIENT_SELECTED] != null)
                //{
                //    string strSelectedOrganizationID = HttpContext.Current.Session[GlobalVariable.CONST_SESSION_ORGANIZATION_CLIENT_SELECTED].ToString();

                //    foreach (var item in lstResult)
                //    {
                //        if (item.Value == strSelectedOrganizationID)
                //        {
                //            item.Selected = true;
                //            break;
                //        }
                //        else
                //        {
                //            item.Selected = false;
                //        }
                //    }
                //}

                //HttpContext.Current.Session[GlobalVariable.CONST_SESSION_ORGANIZATION_CLIENT_LIST] = lstResult;

                return lstResult;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return lstResult;
        }
        #endregion

        #region Encrypt
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 3 November 2016
        /// Purpose : For encrypt string
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            try
            {
                if (!string.IsNullOrEmpty(clearText))
                {
                    var SaltKey = "";
                    var SaltKeyPaarameter = GetSysParam("IndopayrollKey").FirstOrDefault();
                    if (SaltKeyPaarameter != null)
                        SaltKey = SaltKeyPaarameter.Value;
                    else
                        SaltKey = "Ind0P@yrO11-A5a";
                    string strEncryptionKey = SaltKey;

                    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(strEncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            clearText = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
                return clearText;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return clearText;
        }
        #endregion

        #region Decrypt
        /// <summary>
        /// Created By : Herry Sutedja
        /// Created Date : 3 November 2016
        /// Purpose : For decrypt string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            try
            {
                if (!string.IsNullOrEmpty(cipherText))
                {
                    var SaltKey = "";
                    var SaltKeyPaarameter = GetSysParam("IndopayrollKey").FirstOrDefault();
                    if (SaltKeyPaarameter != null)
                        SaltKey = SaltKeyPaarameter.Value;
                    else
                        SaltKey = "Ind0P@yrO11-A5a";
                    string strEncryptionKey = SaltKey;

                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(strEncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
                return cipherText;
            }
            catch (DbUpdateException E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {

                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return cipherText;
        }
        #endregion

        #region DataValidate
        /// <summary>
        /// public method used to Validate Date Field details
        /// </summary>
        /// <param name="v_intDay">Integer Day</param>
        /// <param name="v_intMonth">Integer Month</param>
        /// <param name="v_intYear">Integer Year</param>
        /// <returns>True if date is correct</returns>
        #region public static boolean : IsValidDate
        public static bool IsValidDate(int v_intDay, int v_intMonth, int v_intYear)
        {
            bool blnValidateStatus = false;

            if (v_intDay > 0 && v_intMonth > 0 && v_intMonth < 13 && v_intYear > 0)
            {
                int intDaysInMonth = DateTime.DaysInMonth(v_intYear, v_intMonth);
                if (v_intDay <= intDaysInMonth)
                {
                    blnValidateStatus = true;
                }
            }
            return blnValidateStatus;
        }
        #endregion

        #region
        public static bool IsValidMonthSorting(List<tbl_Tax_Period_Month> v_Month)
        {
            bool blnValidateStatus = false;
            var countMonth = v_Month.Count();
            for (int i = 0; i < countMonth; i++)
            {
                var max = countMonth - 1;
                if (i < max)
                {
                    int month = v_Month[i].Tax_Period_Month.Month;
                    int monthadd = v_Month[i + 1].Tax_Period_Month.Month;
                    //int IntSplit = Convert.ToInt16(strSplit[0])+1;
                    //int IntSplitCheck= Convert.ToInt16( strSplitcheck[0]);

                    if (month + 1 != monthadd)
                    {
                        blnValidateStatus = true;
                        break;
                    }
                }
            }
            return blnValidateStatus;
        }
        #endregion
        #region
        public static bool IsValidDuplicateMonth(List<tbl_Tax_Period_Month> v_Month)
        {
            HashSet<int> hs = new HashSet<int>();
            bool blnValidateStatus = false;
            foreach (var item in v_Month)
                if (!hs.Add(item.Tax_Period_Month.Month))
                {
                    blnValidateStatus = true;
                    break;
                }
            return blnValidateStatus;
        }
        #endregion


        #region IsValidMonthSorting PayrollPeriod
        public static bool IsValidMonthSortingPeriod(List<DateTime> v_Month)
        {
            bool blnValidateStatus = false;
            var countMonth = v_Month.Count();
            for (int i = 0; i < countMonth; i++)
            {
                var max = countMonth - 1;
                if (i < max)
                {
                    int month = v_Month[i].Month;
                    int monthadd = v_Month[i + 1].Month;

                    if (month + 1 != monthadd)
                    {
                        blnValidateStatus = true;
                        break;
                    }
                }
            }
            return blnValidateStatus;
        }
        #endregion

        #region IsValidDuplicateMonth PayrollPeriod
        public static bool IsValidDuplicateMonthPeriod(List<DateTime> v_Month)
        {
            HashSet<int> hs = new HashSet<int>();
            bool blnValidateStatus = false;
            foreach (var item in v_Month)
                if (!hs.Add(item.Month))
                {
                    blnValidateStatus = true;
                    break;
                }
            return blnValidateStatus;
        }
        #endregion


        /// <summary>
        /// Checks whether time is valid
        /// </summary>
        /// <param name="v_intHour">integer hour</param>
        /// <param name="v_intMinute">integer minute</param>
        /// <returns>True if it is valid</returns>
        #region public static bool: IsValidTime

        public static bool IsValidTime(int v_intHour, int v_intMinute)
        {
            bool blIsTime = true;

            if (v_intHour < 0 || v_intMinute < 0)
            {
                blIsTime = false;
            }
            else
            {
                if (v_intHour > 24)
                    blIsTime = false;

                if (v_intMinute > 60)
                    blIsTime = false;
            }
            return blIsTime;

        }
        #endregion

        /// <summary>
        /// Checks whether it is a valid formatted date (dd-mm-yyyy) or dd/MM/yyyy
        /// </summary>
        /// <param name="strFormatedDate">string in dd-mm-yyyy format or dd/MM/yyyy</param>
        /// <returns>True if it is a valid formatted date</returns>
        #region public static bool: IsValidFormattedDate

        public static bool IsValidFormattedDate(string strFormattedDate)
        {
            bool blIsValid = false;
            try
            {
                if (!Regex.IsMatch(strFormattedDate, @"^\d{1,2}/\d{1,2}/\d{2,4}$") && !Regex.IsMatch(strFormattedDate, @"^\d{1,2}-\d{1,2}-\d{2,4}$"))
                    return false;
                string[] arrDate = new string[2];

                string strDeliminator = "/";
                if (strFormattedDate.IndexOf("-", 0) > 0)
                    strDeliminator = "-";

                char[] delimiter = strDeliminator.ToCharArray();
                strFormattedDate = strFormattedDate.Trim();
                arrDate = strFormattedDate.Split(delimiter);

                //To check the input year against SqlDateTime which must be between 1/1/1753 and 31/12/9999
                int intYearLength = arrDate[2].ToString().Length;
                int intYear = int.Parse(arrDate[2].ToString());
                if (intYearLength > 2 || intYearLength < 2)
                {
                    if (intYear > 1752 && intYear <= 9999)
                    {
                        if (IsValidDate(Convert.ToInt16(arrDate[0].Trim()), Convert.ToInt16(arrDate[1].Trim()), Convert.ToInt16(arrDate[2].Trim())))
                            blIsValid = true;
                    }
                }
                //
                else
                {
                    if (IsValidDate(Convert.ToInt16(arrDate[0].Trim()), Convert.ToInt16(arrDate[1].Trim()), Convert.ToInt16(arrDate[2].Trim())))
                        blIsValid = true;
                }
            }
            catch
            {
            }
            return blIsValid;
        }

        #endregion

        /// <summary>
        /// public method used to format Date Field details
        /// </summary>
        /// <param name="strDate">Date string to be formatted (dd/mm/yyyy hh:mm:ss am)</param>
        /// <param name="strOldDeliminator">Old deliminator e.g. "-"</param>
        /// <param name="strNewDeliminator">New deliminator e.g. "/"</param>
        /// <returns>"Error" if there is errors. Else it will return the formatted string format (dd-mm-yyyy)</returns>
        #region public static string: FormatDate ( string strDate, string strOldDeliminator, string strNewDeliminator)
        public static string FormatDate(string strDate, string strOldDeliminator, string strNewDeliminator)
        {
            string formattedDate = "Error";
            string[] StrArray = new string[2];
            string strDelim = " ";
            char[] delimiter = strDelim.ToCharArray();
            //split the date and time
            StrArray = strDate.Split(delimiter);
            strDelim = strOldDeliminator;
            delimiter = strDelim.ToCharArray();
            //split the day, month and year
            StrArray = StrArray[0].Split(delimiter);
            //reformat the date to dd/mm/yyyy
            try
            {
                if (IsValidDate(Convert.ToInt32(StrArray[0].ToString()), Convert.ToInt32(StrArray[1].ToString()), Convert.ToInt32(StrArray[2].ToString())))
                {
                    DateTime dtDate = new DateTime(Convert.ToInt32(StrArray[2].ToString()), Convert.ToInt32(StrArray[1].ToString()), Convert.ToInt32(StrArray[0].ToString()));
                    strDate = dtDate.ToString("dd" + strNewDeliminator + "MM" + strNewDeliminator + "yyyy");
                    formattedDate = strDate;
                }
                else
                {
                    formattedDate = "Error";
                }
            }
            catch
            {
                formattedDate = "Error";
            }

            return formattedDate;
        }
        #endregion

        /// <summary>
        /// public method used to format Date Field details
        /// </summary>
        /// <param name="dt">DateTime to be formatted (dd/mm/yyyy hh:mm:ss am)</param>
        /// <returns>"Error" if there is errors. Else it will return the formatted string format (dd-mm-yyyy)</returns>
        #region public static string: FormatDate ( DateTime dt)
        public static string FormatDate(DateTime dt)
        {
            string formattedDate = "";
            try
            {
                if (IsValidDate(Convert.ToInt32(dt.Day), Convert.ToInt32(dt.Month), Convert.ToInt32(dt.Year)))
                {
                    if (IsValidTime(Convert.ToInt32(dt.Hour), Convert.ToInt32(dt.Minute)))
                    {
                        formattedDate = dt.ToString("dd/MM/yyyy");  //dd/MM/yyyy HH:mm
                    }
                    else
                    {
                        throw new Exception(CONST_DATETIME_EXCEPTION);
                    }
                }
                else
                {
                    throw new Exception(CONST_DATETIME_EXCEPTION);
                }
            }
            catch
            {
                throw new Exception(CONST_DATETIME_EXCEPTION);
            }

            return formattedDate;

        }
        #endregion

        /// <summary>
        /// public method used to format database Date Field details
        /// </summary>
        /// <param name="strDate">Date string to be formatted (mm/dd/yyyy hh:mm:ss am)</param>
        /// <param name="strOldDeliminator">Old deliminator e.g. "-"</param>
        /// <param name="strNewDeliminator">New deliminator e.g. "/"</param>
        /// <returns>"Error" if there is errors. Else it will return the formatted string format (dd-mm-yyyy)</returns>
        #region public static string: FormatDataBaseDate( string strDate, string strOldDeliminator, string strNewDeliminator)
        public static string FormatDataBaseDate(string strDate, string strOldDeliminator, string strNewDeliminator)
        {
            string formattedDate = "Error";
            string[] StrArray = new string[2];
            string strDelim = " ";
            char[] delimiter = strDelim.ToCharArray();
            //split the date and time
            StrArray = strDate.Split(delimiter);
            strDelim = strOldDeliminator;
            delimiter = strDelim.ToCharArray();
            //split the month, day and year
            StrArray = StrArray[0].Split(delimiter); //split into [mm],[dd],[yyyy]
            //reformat the date to dd/mm/yyyy
            try
            {
                if (IsValidDate(Convert.ToInt32(StrArray[1].ToString()), Convert.ToInt32(StrArray[0].ToString()), Convert.ToInt32(StrArray[2].ToString())))
                {
                    DateTime dtDate = new DateTime(Convert.ToInt32(StrArray[2].ToString()), Convert.ToInt32(StrArray[0].ToString()), Convert.ToInt32(StrArray[1].ToString()));
                    strDate = dtDate.ToString("dd" + strNewDeliminator + "MM" + strNewDeliminator + "yyyy");
                    formattedDate = strDate;
                }
                else
                {
                    formattedDate = "Error";
                }
            }
            catch
            {
                formattedDate = "Error";
            }

            return formattedDate;
        }
        #endregion

        /// <summary>
        /// Checks if string format is earlier than today's date. (dd-MM-yyyy)
        /// </summary>
        /// <param name="strFormattedDate">String in dd-MM-yyyy format</param>
        /// <returns>True if it is earlier.</returns>
        #region public static bool: IsEarlierThanToday
        public static bool IsEarlierThanToday(string strFormattedDate)
        {
            bool blIsEarlierThanToday = false;
            try
            {
                DateTime dt = ConvertToDateTime(strFormattedDate);
                if (dt.Date <= DateTime.Today.Date)
                    blIsEarlierThanToday = true;
            }
            catch
            {
            }
            return blIsEarlierThanToday;
        }
        #endregion

        /// <summary>
        /// Converts string date (dd-MM-yyyy) to datetime
        /// </summary>
        /// <param name="strFormattedDate">String in dd-MM-yyyy format</param>
        /// <returns>Datetime</returns>
        #region public static DateTime: ConvertToDateTime
        public static DateTime ConvertToDateTime(string strFormattedDate)
        {
            DateTime dt = new DateTime();
            try
            {
                if (IsValidFormattedDate(strFormattedDate))
                {
                    System.Globalization.CultureInfo ciEngGB = new System.Globalization.CultureInfo("en-GB", true);
                    dt = DateTime.Parse(strFormattedDate, ciEngGB);
                }
            }
            catch { }
            return dt;
        }
        #endregion

        /// <summary>
        /// Used to convert string format from "dd-MM-yyyy" to "MM-dd-yyyy". Please note that "-" must be used.
        /// </summary>
        /// <param name="strFormattedDate">dd-MM-yyyy</param>
        /// <returns>Formated string in MM-dd-yyyy</returns>
        #region public static string: ConvertToMMDDYYYY
        public static string ConvertToMMDDYYYY(string strFormattedDate)
        {
            string strDateTime = "";
            if (IsValidFormattedDate(strFormattedDate))
            {
                string[] arrDate = new string[2];
                //modified by Khoo Kay Joe 17/11/2003
                //string strDeliminator = "-";
                string strDeliminator = "/";
                //end modified by Khoo Kay Joe 17/11/2003
                char[] delimiter = strDeliminator.ToCharArray();
                strFormattedDate = strFormattedDate.Trim();
                arrDate = strFormattedDate.Split(delimiter);
                strDateTime = arrDate[1].Trim() + strDeliminator + arrDate[0].Trim() + strDeliminator + arrDate[2].Trim();
            }
            return strDateTime;
        }
        #endregion

        /// <summary>
        /// Used to convert string format from "MM-dd-yyyy" to "dd-MM-yyyy". Please note that "-" must be used.
        /// </summary>
        /// <param name="strFormattedDate">MM-dd-yyyy</param>
        /// <returns>Formated string in dd-MM-yyyy</returns>
        #region public static string: ConvertToDDMMYYYY
        public static string ConvertToDDMMYYYY(string strFormattedDate)
        {
            string strDateTime = "";
            string[] arrDate = new string[2];
            //modified by Khoo Kay Joe 17/11/2003
            //string strDeliminator = "-";
            string strDeliminator = "/";
            //end modified by Khoo Kay Joe 17/11/2003
            char[] delimiter = strDeliminator.ToCharArray();
            strFormattedDate = strFormattedDate.Trim();
            arrDate = strFormattedDate.Split(delimiter);
            strDateTime = arrDate[1].Trim() + strDeliminator + arrDate[0].Trim() + strDeliminator + arrDate[2].Trim();
            if (IsValidFormattedDate(strDateTime))
                return strDateTime;
            else
                return "";
        }
        #endregion

        /// <summary>
        /// Checks if formatted string (dd-MM-yyyy) is today's date.
        /// </summary>
        /// <param name="strFormattedDate">String in dd-MM-yyyy format</param>
        /// <returns>True if it is today.</returns>
        #region public static bool: IsToday
        public static bool IsToday(string strFormattedDate)
        {
            bool blIsToday = false;
            try
            {
                DateTime dt = ConvertToDateTime(strFormattedDate);
                if (dt.Date == DateTime.Today.Date)
                    blIsToday = true;
            }
            catch
            {
            }
            return blIsToday;
        }
        #endregion

        /// <summary>
        /// Checks if string format (in dd-MM-yyyy) is later than today's date.
        /// </summary>
        /// <param name="strFormattedDate">String in dd-MM-yyyy format</param>
        /// <returns>True if it is later than today.</returns>
        #region public static bool: IsLaterThanToday
        public static bool IsLaterThanToday(string strFormattedDate)
        {
            bool blIsLaterThanToday = false;
            try
            {
                DateTime dt = ConvertToDateTime(strFormattedDate);
                if (dt.Date > DateTime.Today.Date)
                {
                    blIsLaterThanToday = true;
                }
            }
            catch
            {
            }
            return blIsLaterThanToday;
        }
        #endregion

        /// <summary>
        /// Compare 2 date 
        /// </summary>
        /// <param name="strStartDate">String in dd-MM-yyyy format --  start date</param>
        /// <param name="strEndDate">String in dd-MM-yyyy format --  end date</param>
        /// <returns>1 if start date is greater then end date. -- -1 if start date is smaller then end date -- 0 if start date and end date is same</returns>
        #region public static int: CompareDate
        public static int CompareDate(string strStartDate, string strEndDate)
        {
            int intCompareDate = 0;
            try
            {
                DateTime dtStart = ConvertToDateTime(strStartDate);
                DateTime dtEnd = ConvertToDateTime(strEndDate);
                if (dtStart == dtEnd)
                    intCompareDate = 0;
                else if (dtStart < dtEnd)
                    intCompareDate = -1;
                else if (dtStart > dtEnd)
                    intCompareDate = 1;
            }
            catch
            {
            }
            return intCompareDate;
        }
        #endregion

        /// <summary>
        /// get the day
        /// </summary>
        /// <param name="strStartDate">String in dd-MM-yyyy format</param>
        /// <returns>return a int day</returns>
        /// 
        #region public static int: GetDay
        public static int GetDay(string strFormattedDate)
        {
            int intDay = 0;
            try
            {
                string[] arrDate = new string[2];
                //modified by Khoo Kay Joe 17/11/2003
                //string strDeliminator = "-";
                string strDeliminator = "/";
                //end modified by Khoo Kay Joe 17/11/2003
                char[] delimiter = strDeliminator.ToCharArray();
                strFormattedDate = strFormattedDate.Trim();
                arrDate = strFormattedDate.Split(delimiter);
                intDay = Convert.ToInt16(arrDate[0].Trim());
            }
            catch
            {
            }
            return intDay;
        }
        #endregion

        /// <summary>
        /// get the month
        /// </summary>
        /// <param name="strStartDate">String in dd-MM-yyyy format</param>
        /// <returns>return a int month</returns>
        /// 
        #region public static int: GetMonth
        public static int GetMonth(string strFormattedDate)
        {
            int intMonth = 0;
            try
            {
                string[] arrDate = new string[2];
                //modified by Khoo Kay Joe 17/11/2003
                //string strDeliminator = "-";
                string strDeliminator = "/";
                //end modified by Khoo Kay Joe 17/11/2003
                char[] delimiter = strDeliminator.ToCharArray();
                strFormattedDate = strFormattedDate.Trim();
                arrDate = strFormattedDate.Split(delimiter);
                intMonth = Convert.ToInt16(arrDate[1].Trim());
            }
            catch
            {
            }
            return intMonth;
        }
        #endregion

        /// <summary>
        /// get the year
        /// </summary>
        /// <param name="strStartDate">String in dd-MM-yyyy format</param>
        /// <returns>return a int year</returns>
        /// 
        #region public static int: GetYear
        public static int GetYear(string strFormattedDate)
        {
            int intYear = 0;
            try
            {
                string[] arrDate = new string[2];
                //modified by Khoo Kay Joe 17/11/2003
                //string strDeliminator = "-";
                string strDeliminator = "/";
                //end modified by Khoo Kay Joe 17/11/2003
                char[] delimiter = strDeliminator.ToCharArray();
                strFormattedDate = strFormattedDate.Trim();
                arrDate = strFormattedDate.Split(delimiter);
                intYear = Convert.ToInt16(arrDate[2].Trim());
            }
            catch
            {
            }
            return intYear;
        }
        #endregion



        /// <summary>
        /// Checks if string format (in dd-MM-yyyy) is later than today's date.
        /// </summary>
        /// <param name="strFormattedDate">String in dd-MM-yyyy format</param>
        /// <returns>True if it is later than today.</returns>
        #region public static bool: IsLaterOrEqualThanToday
        public static bool IsLaterOrEqualThanToday(string strFormattedDate)
        {
            bool blIsLaterThanToday = false;
            try
            {
                DateTime dt = ConvertToDateTime(strFormattedDate);
                if (dt.Date >= DateTime.Today.Date)
                    blIsLaterThanToday = true;
            }
            catch
            {
            }
            return blIsLaterThanToday;
        }
        #endregion

        // Author: Jacky Yong 18 Dec 2003
        /// <summary>
        /// Checks whether minimum age has been reached
        /// </summary>
        /// <param name="strDateOfBirth">Date of Birth in string</param>
        /// <param name="intMinimumAge">Minimum Age</param>
        /// <returns>Boolean</returns>
        #region public static bool IsMinimumAge - Overloaded
        public static bool IsMinimumAge(string strDateOfBirth, int intMinimumAge)
        {
            bool blIsValidAge = false;
            strDateOfBirth = strDateOfBirth.Trim();
            if (IsValidFormattedDate(strDateOfBirth) && intMinimumAge > 0)
            {
                blIsValidAge = IsMinimumAge(ConvertToDateTime(strDateOfBirth), intMinimumAge);
            }
            else
                blIsValidAge = false;
            return blIsValidAge;
        }
        #endregion

        // Author: Jacky Yong 18 Dec 2003
        /// <summary>
        /// Checks whether minimum age has been reached
        /// </summary>
        /// <param name="dtDateOfBirth">Date of Birth in DateTime</param>
        /// <param name="intMinimumAge">Minimum Age</param>
        /// <returns>Boolean</returns>
        #region public static bool IsMinimumAge - Overloaded
        public static bool IsMinimumAge(DateTime dtDateOfBirth, int intMinimumAge)
        {
            bool blIsValidAge = false;
            if (dtDateOfBirth < DateTime.Today && intMinimumAge > 0)
            {
                intMinimumAge = -1 * (intMinimumAge);
                DateTime thisDateYearsAgo = DateTime.Today.AddYears(intMinimumAge);
                if (dtDateOfBirth <= thisDateYearsAgo)
                    blIsValidAge = true;
                else
                    blIsValidAge = false;
            }
            else
                blIsValidAge = false;
            return blIsValidAge;
        }
        #endregion

        // Author: Jacky Yong 18 Dec 2003
        /// <summary>
        /// Checks whether maximum age has been reached
        /// </summary>
        /// <param name="strDateOfBirth">Date of Birth in string</param>
        /// <param name="intMaximumAge">Maximum Age</param>
        /// <returns>Boolean</returns>
        #region public static bool IsMaximumAge - Overloaded
        public static bool IsMaximumAge(string strDateOfBirth, int intMaximumAge)
        {
            bool blIsValidAge = false;
            strDateOfBirth = strDateOfBirth.Trim();
            if (IsValidFormattedDate(strDateOfBirth) && intMaximumAge > 0)
            {
                blIsValidAge = IsMaximumAge(ConvertToDateTime(strDateOfBirth), intMaximumAge);
            }
            else
                blIsValidAge = false;
            return blIsValidAge;
        }
        #endregion

        // Author: Jacky Yong 18 Dec 2003
        /// <summary>
        /// Checks whether Age is already reached maximum
        /// </summary>
        /// <param name="dtDateOfBirth">Date of Birth in DateTime</param>
        /// <param name="intMaximumAge">Maximum Age</param>
        /// <returns>boolean</returns>
        #region public static bool IsMaximumAge - Overloaded
        public static bool IsMaximumAge(DateTime dtDateOfBirth, int intMaximumAge)
        {
            bool blIsValidAge = false;
            if (dtDateOfBirth < DateTime.Today && intMaximumAge > 0)
            {
                intMaximumAge = -1 * (intMaximumAge);
                DateTime thisDateYearsAgo = DateTime.Today.AddYears(intMaximumAge);
                if (dtDateOfBirth > thisDateYearsAgo)
                    blIsValidAge = true;
                else
                    blIsValidAge = false;
            }
            else
                blIsValidAge = false;
            return blIsValidAge;
        }
        #endregion

        #endregion

        #region UIfunction Money
        /// <summary>
        /// Check if string is a decimal value. Will pass even if string value has "." or ","
        /// </summary>
        /// <param name="strNumber">string to be validated</param>
        /// <returns>String Money</returns>
        #region public static : FormatMoney ( string strMoney )
        public static string FormatMoney(string strMoney)
        {
            if (strMoney != "0")
            {
                decimal decMoney = Convert.ToDecimal(strMoney.ToString());
                return decMoney.ToString("##,##0.00");
                //END - [OSKAR] - 21 / 03 / 2005
            }
            else
            {
                //return "Error Formatting Money";
                return "0.00";
            }
        }
        #endregion

        #region public static : FormatMoney


        public static string FormatMoney(string strMoney, string strFormat, string strCultureInfo)
        {
            decimal decMoney = 0;
            string strTempMoney = string.Empty;
            string strMoneyDecimal = "00";  //Raymond 07/08/2008
            string strSign = string.Empty;  //Raymond 13/08/2008
            int j = 0;

            string[] arrMoney = null;   //Raymond 24/08/2008

            //kai yi 09092008 -- to cater format empty string
            if (strMoney == "" || strMoney == DBNull.Value.ToString())
            {
                return string.Empty;
            }
            else
            {
                if (strCultureInfo == "id-ID")
                {
                    //[BEGIN] Raymond 24/08/2008 - To cater formatted money will not go wrong when reformatted
                    //Example: 9.999.000
                    arrMoney = strMoney.Split('.');
                    if (arrMoney.Length >= 3)
                        return strMoney;

                    if (arrMoney.Length == 2)
                    {
                        //Example: 999.000 and Not 999.000,00
                        if (strMoney.IndexOf('.') == strMoney.Length - 4)
                            return strMoney + "," + strMoneyDecimal;
                    }

                    try
                    {
                        //Example: 999.999,00
                        decMoney = Convert.ToDecimal(strMoney);
                    }
                    catch
                    {
                        //If exception, means strMoney is already in Indonesian format, then return
                        return strMoney;
                    }

                    //Example: 999,0 or 999,00
                    //if (strMoney.IndexOf(',') == strMoney.Length - 2 || strMoney.IndexOf(',') == strMoney.Length - 3)
                    if (strMoney.IndexOf(',') != -1 && (strMoney.IndexOf(',') == strMoney.Length - 2 || strMoney.IndexOf(',') == strMoney.Length - 3)) // 2009-05-11 - Andreas - Exculde number without decimal
                    {
                        strMoneyDecimal = strMoney.Substring(strMoney.IndexOf(',') + 1);
                        if (strMoneyDecimal.Length == 1)
                        {
                            strMoney = strMoney + "0";
                        }
                        return strMoney;
                    }

                    strMoney = strMoney.Replace(",", "");   //Remove comma if strMoney contains English formatted
                    //[END] Raymond 24/08/2008

                    if (strMoney.IndexOf('.') > -1)
                    {
                        strMoneyDecimal = strMoney.Substring(strMoney.IndexOf('.') + 1);        //Raymond 07/08/2008
                        if (strMoneyDecimal.Length > 2)
                            strMoneyDecimal = strMoneyDecimal.Substring(0, 2);
                        else
                            strMoneyDecimal = strMoneyDecimal.PadRight(2, '0');

                        if (strMoney.IndexOf('-') >= 0)
                        {
                            strSign = strMoney.Substring(0, 1);     //If '-' exist, keep it
                            strMoney = strMoney.Substring(1);       //Remove '-' sign
                        }
                        strMoney = strMoney.Remove(strMoney.IndexOf('.'));
                    }
                    char[] chrMoney = strMoney.ToCharArray();
                    for (int i = chrMoney.Length - 1; i >= 0; i--)
                    {
                        if (j % 3 == 0 && j != 0)
                            strTempMoney = "." + strTempMoney;

                        strTempMoney = chrMoney[i].ToString() + strTempMoney;
                        j++;
                    }
                    strTempMoney = strSign + strTempMoney + "," + strMoneyDecimal;        //Raymond 07/08/2008
                }
                else
                {
                    decMoney = Convert.ToDecimal(strMoney.ToString());
                    return decMoney.ToString(strFormat, new System.Globalization.CultureInfo(strCultureInfo));
                }
            }
            return strTempMoney;
        }
        #endregion


        public static string FormatMoney(decimal decMoney, string strFormat, string strCultureInfo)
        {
            return decMoney.ToString(strFormat, new System.Globalization.CultureInfo(strCultureInfo));
        }
        #endregion

        #region Remove money format
        /// <summary>
        /// Remove all money format and display to format ##0.##
        /// </summary>
        /// <param name="strMoney">Money value in English</param>
        /// <param name="strCultureInfo"></param>
        /// <returns></returns>
        public static string RemoveMoneyFormat(string strMoney, string strCultureInfo)
        {
            // ssaputra - 2009/08/27 - Trim data to handle invalid character
            strMoney = strMoney.Trim();

            string strTempMoney = string.Empty;
            string strMoneyDecimal = "00";          //Raymond 13/08/2008
            //string[] arrMoney = null;               //Raymond 24/08/2008 // Kai Yi 05092008 ,remarked for cleaning warning error msg

            if (strCultureInfo == "id-ID")
            {
                // ssaputra - 2009/07/15 - Show money without "," and "."
                if (!strMoney.Contains(".") && !strMoney.Contains(","))
                {
                    strTempMoney = strMoney + "." + strMoneyDecimal;

                    return strTempMoney;
                }

                //[BEGIN] Raymond 24/08/2008 - To cater unformatted money will not go wrong when remove format again
                //Example: 9,999,999.00 Or 9,999,999.0000 (English format)
                // ssaputra - 2009/07/15 - Check money with 2 / 4 decimal digit
                if ((strMoney.IndexOf('.') == strMoney.Length - 3)
                                        || (strMoney.IndexOf('.') == strMoney.Length - 5))
                {
                    //Example: 1,00, will return -1 when strMoney.IndexOf('.') - Raymond Chow 10/09/2008
                    if (strMoney.Length == 4 && strMoney.IndexOf(',') == strMoney.Length - 3)
                    {
                        strMoney = strMoney.Replace(",", ".");
                        return strMoney;
                    }

                    strMoney = strMoney.Replace(",", "");
                    if (strMoney.IndexOf('.') == strMoney.Length - 5)
                        strMoney = strMoney.Substring(0, strMoney.Length - 2);
                    return strMoney;
                }

                //Example: 999,999 (English format)
                if (strMoney.IndexOf(',') == strMoney.Length - 4)
                {
                    strMoney = strMoney.Replace(",", "");
                    return strMoney + "." + strMoneyDecimal;
                }
                //[BEGIN] Raymond 24/08/2008

                if (strMoney.IndexOf(',') >= 0)
                {
                    strMoneyDecimal = strMoney.Substring(strMoney.IndexOf(',') + 1);
                    strMoney = strMoney.Remove(strMoney.IndexOf(','));
                }

                //[BEGIN] Raymond 24/08/2008 
                if (strMoneyDecimal.Length > 2)
                    strMoneyDecimal = strMoneyDecimal.Substring(0, 2);
                else
                    strMoneyDecimal = strMoneyDecimal.PadRight(2, '0');
                //[END] Raymond 24/08/2008
                strTempMoney = strMoney.Replace(".", "");
                strTempMoney = strTempMoney + "." + strMoneyDecimal;
                //strTempMoney = strTempMoney.Replace(",", "."); // Jonathan - 08/08/2008
            }
            else
            {
                strTempMoney = strMoney.Replace(",", "");
            }
            return strTempMoney;
        }

        #endregion

        public static string FormatNumber(string strInput, string strCultureInfo)
        {
            string strTempInput = string.Empty;
            //decimal decNumber = 0;
            int j = 0;

            //decNumber = Convert.ToDecimal(strInput.ToString());
            //return decNumber.ToString("N", new System.Globalization.CultureInfo(strCultureInfo));

            if (strCultureInfo == "id-ID")
            {
                if (strInput.IndexOf('.') > -1)
                    strInput = strInput.Remove(strInput.IndexOf('.'));

                char[] chrInput = strInput.ToCharArray();
                for (int i = chrInput.Length - 1; i >= 0; i--)
                {
                    if (j % 3 == 0 && j != 0)
                        strTempInput = "." + strTempInput;

                    strTempInput = chrInput[i].ToString() + strTempInput;
                    j++;
                }
            }
            else
            {
                if (strInput.IndexOf('.') > -1)
                    strInput = strInput.Remove(strInput.IndexOf('.'));

                char[] chrInput = strInput.ToCharArray();
                for (int i = chrInput.Length - 1; i >= 0; i--)
                {
                    if (j % 3 == 0 && j != 0)
                        strTempInput = "," + strTempInput;

                    strTempInput = chrInput[i].ToString() + strTempInput;
                    j++;
                }
            }
            return strTempInput;
        }

        #region IsValidDecimalLength
        public static bool IsValidDecimalLength(string strAmount, string strCultureInfo, int strMaxLengthBeforeDecimal, int strMaxLengthAfterDecimal)
        {
            #region Variables
            Boolean blnValid = false;
            string[] arrAmount = null;
            string strDelimiter = ".";
            //string strDelimiter = string.Empty;
            //System.Globalization.CultureInfo culture = null;
            char[] arrDelimiter = null;
            #endregion Variables

            //culture = new System.Globalization.CultureInfo(strCultureInfo);
            //strDelimiter = culture.NumberFormat.CurrencyDecimalSeparator;

            //strAmount = RemoveMoneyFormat(strAmount, strCultureInfo);

            arrDelimiter = strDelimiter.ToCharArray();
            arrAmount = strAmount.Split(arrDelimiter);

            if (arrAmount[0].ToString().Length <= strMaxLengthBeforeDecimal &&
                    arrAmount[1].ToString().Length <= strMaxLengthAfterDecimal)
                blnValid = true;

            return blnValid;
        }
        #endregion IsValidDecimalLength

        #region public static : FormatDateTime ( datetime dtDateTimeInput, string strFormat, string strCultureInfo)
        public static string FormatDateTime(DateTime dtDateTimeInput, string strFormat, string strCultureInfo)
        {
            DateTime dtNow = DateTime.Now;

            if (dtDateTimeInput != null && strFormat != "")
            {
                return dtDateTimeInput.ToString(strFormat, new System.Globalization.CultureInfo(strCultureInfo));
            }
            else
            {
                return dtNow.ToString(strFormat, new System.Globalization.CultureInfo(strCultureInfo));
            }
        }
        #endregion

        // Author : Yoshida
        // Date : 5 Nov 2010
        // Masking string with custom character used in CR109, CR103
        // Credit to : [Orison 20100503] CR-62 Mask the last 4 digits of account no with “*” char 
        public static string MaskingString(string strInput, int displayDigit, char mask_char, int totalLength)
        {
            if (strInput.Length > displayDigit)
            {
                string strOutput = null;
                int lengthLeft = strInput.Length - displayDigit;
                string mask = new string(mask_char, lengthLeft);
                strOutput = strInput.Remove(displayDigit) + mask;
                if (totalLength == 0) return strOutput;
                else return strOutput.Remove(totalLength);
            }
            else return strInput;
        }
        /// <summary>
        /// Mask only from the end of the string.
        /// </summary>
        /// <param name="strInput">String you want to mask</param>
        /// <param name="intHowMany">how many characters from the end to mask</param>
        /// <param name="chrMask">the character of the mask</param>
        /// <returns>masked input</returns>
        public static string MaskingString(string strInput, int intHowMany, char chrMask)
        {
            if (intHowMany <= 0) return strInput;
            string strMask = String.Empty;
            for (int i = 0; i < intHowMany; i++)
            {
                strMask += chrMask;
            }
            return strInput.Substring(0, strInput.Length - intHowMany) + strMask;
        }

        #region money format
        public static string FormatMoneyMobile(decimal decMoney, string strFormat, string strCultureInfo)
        {
            return decMoney.ToString(strFormat, new System.Globalization.CultureInfo(strCultureInfo));
        }
        #endregion

        #region public static bool: IsDecimal
        public static bool IsDecimal(string strNumber)
        {
            bool blIsDecimal = false;
            decimal decAmount = 0;
            char[] arrNumber;
            int intPosition = -1;

            try
            {
                // Check distance between commas
                arrNumber = strNumber.ToCharArray();
                for (int i = 0; i < arrNumber.Length; i++)
                {
                    //BEGIN - [Alex Wong] - 05 / 04 / 2005 - Added for DRIB P3 April Release
                    if (arrNumber[i].ToString().IndexOf("+") != -1)
                        return false;
                    //END - [Alex Wong] - 05 / 04 / 2005

                    //BEGIN - [OSKAR] - 24 / 03 / 2005 - Added for DRIB P3 April Release
                    if (arrNumber[i].ToString().IndexOf(@"^[a-zA-Z%_&@\#=\(\)\?/:\|']") != -1)
                        return false;
                    //END - [OSKAR] - 24 / 03 / 2005
                    if (arrNumber[i].CompareTo(',') == 0)
                    {
                        if (intPosition < 0)
                            intPosition = i;
                        else
                            if ((i - intPosition) < 4)
                                return false;
                            else
                                intPosition = i;
                    }
                    else if (arrNumber[i].CompareTo('.') == 0)
                    {
                        if (intPosition >= 0)
                        {
                            if ((i - intPosition) < 4)
                                return false;
                        }
                    }
                }

                decAmount = Convert.ToDecimal(strNumber);
                blIsDecimal = true;
            }
            catch (Exception ex)
            {
                string strExMessage = ex.Message;
                return blIsDecimal;
            }
            return blIsDecimal;
        }

        public static bool IsDecimal(string strNumber, string strFormat, string strCultureInfo)
        {
            bool blIsDecimal = false;
            decimal decAmount = 0;
            char[] arrNumber;
            int intPosition = -1;
            string strGroupSeparator;
            string strFractionMark;
            string strPrecision;

            try
            {
                // Get Currency Format
                getCurrencyFormat(strCultureInfo, strFormat, out strGroupSeparator, out strFractionMark, out strPrecision);
                char chrGroupSeparator = char.Parse(strGroupSeparator);
                char chrFractionMark = char.Parse(strFractionMark);

                // Check distance between commas
                arrNumber = strNumber.ToCharArray();
                for (int i = 0; i < arrNumber.Length; i++)
                {
                    //BEGIN - [Alex Wong] - 05 / 04 / 2005 - Added for DRIB P3 April Release
                    if (arrNumber[i].ToString().IndexOf("+") != -1)
                        return false;
                    //END - [Alex Wong] - 05 / 04 / 2005

                    //BEGIN - [OSKAR] - 24 / 03 / 2005 - Added for DRIB P3 April Release
                    if (arrNumber[i].ToString().IndexOf(@"^[a-zA-Z%_&@\#=\(\)\?/:\|']") != -1)
                        return false;
                    //END - [OSKAR] - 24 / 03 / 2005
                    if (arrNumber[i].CompareTo(chrGroupSeparator) == 0)
                    {
                        if (intPosition < 0)
                            intPosition = i;
                        else
                            if ((i - intPosition) < 4)
                                return false;
                            else
                                intPosition = i;
                    }
                    else if (arrNumber[i].CompareTo(chrFractionMark) == 0)
                    {
                        if (intPosition >= 0)
                        {
                            if ((i - intPosition) < 4)
                                return false;
                        }
                    }
                }

                //2009-04-07 - Willy PL - Change from using Convert.ToDecimal to use decimal.Parse (using CultureInfo parameter)
                //decAmount = Convert.ToDecimal(strNumber);
                decAmount = decimal.Parse(strNumber, new System.Globalization.CultureInfo(strCultureInfo));
                blIsDecimal = true;
            }
            catch (Exception ex)
            {
                string strExMessage = ex.Message;
                return blIsDecimal;
            }
            return blIsDecimal;
        }
        #endregion

        /// </summary>
        /// <param name="strNumber">String to be validated</param>
        /// <param name="intMaxDecimalPlaces">Maximum number of decimal places allowed.</param>
        /// <param name="strFormat">String format</param>
        /// <returns>True if it is a valid decimal</returns>
        #region public static : IsDecimal (string strNumber, int intMaxDecimalPlaces, string strFormat, string strCultureInfo)
        public static bool IsDecimal(string strNumber, int intMaxDecimalPlaces, string strFormat, string strCultureInfo)
        {
            bool blIsDecimal = false;
            decimal decAmount = 0;
            string strGroupSeparator;
            string strFractionMark;
            string strPrecision;

            try
            {
                // Get Currency Format
                getCurrencyFormat(strCultureInfo, strFormat, out strGroupSeparator, out strFractionMark, out strPrecision);
                char chrFractionMark = char.Parse(strFractionMark);

                decAmount = Convert.ToDecimal(strNumber);
                strNumber = decAmount.ToString();

                // determine where the "." is located
                int intIndex = strNumber.IndexOf(chrFractionMark);
                if (intIndex != -1)
                {
                    if ((strNumber.Length - intIndex - 1) <= intMaxDecimalPlaces)
                        blIsDecimal = true;
                }
                else
                    blIsDecimal = true;
            }
            catch
            {
            }
            return blIsDecimal;
        }
        #endregion

        /// </summary>
        /// <param name="strFormat">String to be validated</param>
        /// <param name="strGroupSeparator">Thousand separator in format</param>
        /// <param name="strFractionMark">Fraction in format</param>
        /// <param name="strPrecision">Precision in format</param>
        /// <returns>Returns strGroupSeparator, strFractionMark, strPrecision</returns>
        #region public static void: getCurrencyFormat(string strFormat, out string strGroupSeparator, out string strFractionMark, out string strPrecision)
        public static void getCurrencyFormat(string strCultureInfo, string strFormat, out string strGroupSeparator, out string strFractionMark, out string strPrecision)
        {
            //bool blnFractionMark = false; // Kai Yi 05092008 ,remarked for cleaning warning error msg
            strGroupSeparator = "";
            strFractionMark = "";
            strPrecision = "";

            if (strFormat.Trim() != "")
            {
                decimal amount = 1000.00M;
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(strCultureInfo);
                string strAmount = amount.ToString(strFormat, ci);
                char[] arAmountChar = strAmount.ToCharArray();
                strGroupSeparator = arAmountChar[1].ToString().Trim();
                strFractionMark = arAmountChar[5].ToString().Trim();
                int nPrecision = arAmountChar.Length - 5 - 1;
                strPrecision = nPrecision.ToString();

                strGroupSeparator = ci.NumberFormat.CurrencyGroupSeparator;
                strFractionMark = ci.NumberFormat.CurrencyDecimalSeparator;
                nPrecision = ci.NumberFormat.CurrencyDecimalDigits;
                strPrecision = nPrecision.ToString();
                //for (int i = 0; i < strFormat.Length; i++)
                //{
                //    char chr = char.Parse(strFormat.Substring(i, 1));

                //    if (chr == '0')
                //        blnFractionMark = true;

                //    if (chr != '#' && chr != '0')
                //    {
                //        if (blnFractionMark)
                //            strFractionMark = chr.ToString();
                //        else
                //            strGroupSeparator = chr.ToString();
                //    }
                //}

                //int intFracPos = strFormat.IndexOf(strFractionMark);
                //string strPrec = strFormat.Substring(intFracPos + 1);
                //strPrecision = strPrec.Length.ToString();
            }
        }
        #endregion



        /// <summary>
        /// Checks to see whether a string is Integer or not. Will return false if "." or "," is present.
        /// </summary>
        /// <param name="strNumber">String to check</param>
        /// <returns>boolean, true or false</returns>
        #region public static bool: IsInteger
        public static bool IsInteger(string strNumber)
        {
            bool blIsNum = true;
            for (int index = 0; index < strNumber.Length; index++)
            {
                if (!Char.IsNumber(strNumber[index]))
                {
                    blIsNum = false;
                    break;
                }
            }
            return blIsNum;
        }
        #endregion

        #region public static bool: IsValidEmail

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        #endregion

        public static bool IsAlphabetic(string strNumber)
        {
            bool blIsNum = true;
            for (int index = 0; index < strNumber.Length; index++)
            {
                if (!Char.IsLetter(strNumber[index]))
                {
                    blIsNum = false;
                    break;
                }
            }
            return blIsNum;
        }

        #region
        public static bool IsNotAlphaNumeric(string strInput)
        {
            strInput = strInput.Replace(" ", "");
            Regex regex = new Regex("[^a-zA-Z0-9]");

            Match m = regex.Match(strInput);

            return m.Success;
        }
        #endregion


        //BEGIN - [Sam] - 09/06/2005 - Added for DRIB P3 July Release
        #region IsAcceptedAlphaNumeric
        public static bool IsAcceptedAlphaNumeric(string strInput)
        {
            // Everything in Regex is accepted
            Regex regex = new Regex(@"[^a-zA-Z0-9,%\+\-_&@\.#=\(\)\?/:\|'\t\r\n`~!\$\*\^""\{ }]"); //2008-09-20 - Willy PL - Change to "-" to "\-"

            // Trap those are not accepted
            Match m = regex.Match(strInput);

            // (m.Success == true) means got invalid char., not accpeted.
            return !m.Success;
        }
        #endregion

        #region IsValidAlphaNumeric
        public static bool IsValidAlphaNumeric(string strInput)
        {
            // Everything in Regex is accepted
            Regex regex = new Regex(@"[^a-zA-Z0-9]");

            // Trap those are not accepted
            Match m = regex.Match(strInput);

            // (m.Success == true) means got invalid char., not accpeted.
            return !m.Success;
        }
        #endregion

        public static bool IsNotSpecialChar(string strNumber)
        {

            //                   bool blIsNum = true;

            //                   for( int index = 0; index < strNumber.Length; index++ )

            //                   {

            //                         if( !Char.IsLetterOrDigit( strNumber[ index ] ))

            //                         {

            //                                blIsNum = false;

            //                                break;

            //                         }

            //                        

            //

            //                   }

            //                   return blIsNum;



            // Create a new Regex object.

            //Regex r = new Regex(@"[^a-zA-Z0-9,%\+-_&@\.#=\(\)\?/:\|'\t\r\n]"); // Minus sign need to be escaped - Jonathan 13/09/2008
            Regex r = new Regex(@"[^a-zA-Z0-9,%\+\-_&@\.#=\(\)\?/:\|'\s\t\r\n ]");

            // Find a single match in the string.

            Match m = r.Match(strNumber);



            return !m.Success;

        }

        public static bool IsNumeric(string theValue)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }
        #region IsValidPhoneCharacter
        public static bool IsValidPhoneCharacter(string strNumber)
        {
            //			// Create a new Regex object.
            //			//Regex r = new Regex(@"[^0-9,\+-\(\)]");
            //			Regex r = new Regex("[^0-9][+][-][(][)]");
            //			//Regex r = new Regex(@"[\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}]");
            //
            //			// Find a single match in the string.
            //			Match m = r.Match(strNumber);   
            //  
            //			return !m.Success;

            const string CONST_VALID_CHARS = "0123456789 +-()";
            return UICommonFunction.IsValidChar(strNumber, CONST_VALID_CHARS);
        }

        #endregion
        #region GetDateFromServerDateTime - Date and time
        private static void GetDateFromServerDateTime(string strDateTime,
            out string strDate, out string strTime)
        {
            string[] strResults;
            strResults = strDateTime.Split(new char[] { ' ' });

            if (strResults.Length != 2)
            {
                strDate = "1900-01-01";
                strTime = "00:00:00";
            }
            else
            {
                strDate = strResults[0];
                strTime = strResults[1];
            }
        }

        #endregion

        public static void LogMessage(string fileName, string strMessage)
        {
            //bool blnResult = true;
            System.IO.StreamWriter sw = null;
            string strFilePath = "D:\\DRIB\\Log\\DRIBMY_Logs";

            string strUATPath = "E:\\DRIB\\HostAdapter\\DRIB_Logs";
            string strActualPath = "";

            //string strMessage = "";
            try
            {

                if (Directory.Exists(strFilePath))
                {
                    strActualPath = strFilePath + "\\" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                }
                else
                {
                    strActualPath = strUATPath + "\\" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                }





                if (System.IO.File.Exists(strActualPath))
                    sw = new System.IO.StreamWriter(strActualPath, true);
                else
                {
                    sw = new System.IO.StreamWriter(strActualPath, false);
                }

                sw.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond + ": " + strMessage);
                sw.Flush();
                sw.Close();
                //blnResult = true;
            }
            catch (System.Exception ex)
            {
                strMessage = ex.Message;
                //blnResult = false;
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        public static bool IsValidSize(string strInputValue, int intSizeAllowed)
        {
            if (strInputValue.Length > intSizeAllowed)
                return false;
            else
                return true;
        }

        public static bool IsValidChar(string strInput, string strValidChars)
        {
            string strSingleChar = "";


            if (strInput.Trim() == "")
            {
                return false;
            }


            for (int i = 0; i < strInput.Length; i++)
            {
                strSingleChar = strInput.Substring(i, 1);

                if (strValidChars.IndexOf(strSingleChar) == -1)
                {
                    return false;
                }

            }

            return true;
        }
        /// <summary>
        /// Check if string is a decimal value. Also checks for number of decimal places.
        /// </summary>
        /// <param name="strNumber">String to be validated</param>
        /// <param name="intMaxDecimalPlaces">Maximum number of decimal places allowed.</param>
        /// <returns>True if it is a valid decimal</returns>
        #region public static : IsDecimal
        public static bool IsDecimal(string strNumber, int intMaxDecimalPlaces)
        {
            //[BEGIN] Raymond Chow 07/08/2008 - Remarked
            //bool blIsDecimal = false;
            //decimal decAmount = 0;
            //try
            //{
            //    decAmount = Convert.ToDecimal(strNumber);
            //    strNumber = decAmount.ToString();

            //    // determine where the "." is located
            //    int intIndex = strNumber.IndexOf(".");
            //    if (intIndex != -1)
            //    {
            //        if ((strNumber.Length - intIndex - 1) <= intMaxDecimalPlaces)
            //            blIsDecimal = true;
            //    }
            //    else
            //        blIsDecimal = true;
            //}
            //catch
            //{
            //}
            //[END] Raymond Chow 07/08/2008 - Remarked

            bool blIsDecimal = true;
            return blIsDecimal;
        }
        #endregion

        /// <summary>
        /// Copy from IsDecimal(string strNumber, int intMaxDecimalPlaces) as it is remarked
        /// Check if string is a decimal value. Also checks for number of decimal places.
        /// </summary>
        /// <param name="strNumber">String to be validated</param>
        /// <param name="intMaxDecimalPlaces">Maximum number of decimal places allowed.</param>
        /// <returns>True if it is a valid decimal</returns>
        #region public static : IsDecimalNew
        public static bool IsDecimalNew(string strNumber, int intMaxDecimalPlaces)
        {
            ////[BEGIN] Raymond Chow 07/08/2008 - Remarked //ganck [12-01-2009] - unremark
            bool blIsDecimal = false;
            decimal decAmount = 0;
            try
            {
                decAmount = Convert.ToDecimal(strNumber);
                strNumber = decAmount.ToString();

                // determine where the "." is located
                int intIndex = strNumber.IndexOf(".");
                if (intIndex != -1)
                {
                    if ((strNumber.Length - intIndex - 1) <= intMaxDecimalPlaces)
                        blIsDecimal = true;
                }
                else
                    blIsDecimal = true;
            }
            catch
            {
            }
            //[END] Raymond Chow 07/08/2008 - Remarked

            return blIsDecimal;
        }
        #endregion

        #region ReadFromExcelToDataset
        public static bool ReadFromExcelToDataset(string strTemplatePath, out DataSet dsExcel)
        {
            bool blnResult = false;
            dsExcel = new DataSet();

            try
            {
                string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                strTemplatePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                DataTable dt = new DataTable();

                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return false;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                //excel data saves in temp file here.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }

                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                {
                    dataAdapter.Fill(dsExcel);
                    blnResult = true;
                }
                excelConnection.Close();
            }
            catch (Exception ex)
            {
                string strExMessage = ex.Message;
            }

            return blnResult;
        }
        #endregion ReadFromExcelToDataset

        #region ValidateUploadTemplateHeader
        public static bool ValidateUploadTemplateHeader(string strUploadTemplatePath, DataSet dsCompareFromUpload)
        {
            bool blnResult = true;
            DataSet dsUploadTemplate = null;

            try
            {
                if (ReadFromExcelToDataset(strUploadTemplatePath, out dsUploadTemplate))
                {
                    if (dsCompareFromUpload != null && dsCompareFromUpload.Tables[0] != null && dsCompareFromUpload.Tables[0].Columns.Count > 0)
                    {
                        if (dsUploadTemplate != null && dsUploadTemplate.Tables[0] != null && dsUploadTemplate.Tables[0].Columns.Count > 0)
                        {
                            for (int i = 0; i < dsCompareFromUpload.Tables[0].Columns.Count; i++)
                            {
                                if (dsCompareFromUpload.Tables[0].Columns[i].ToString() != dsUploadTemplate.Tables[0].Columns[i].ToString())
                                {
                                    blnResult = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                blnResult = false;
                string strExMessage = ex.Message;
            }

            return blnResult;
        }
        #endregion ValidateUploadTemplateHeader

        #region Search Authorize Status For Upload Data
        public static string AuthorizeUploadDataUpper(string StrAuthorize)
        {
            if (StrAuthorize == "AUTHORIZE" || StrAuthorize == "UNAUTHORIZE" || StrAuthorize == "REJECTED")
            {
                StrAuthorize = SearchAuthorizeStatusUpload(StrAuthorize);
                return StrAuthorize;
            }
            return StrAuthorize;
        }
        public static string SearchAuthorizeStatusUpload(string AuthorizeStatus)
        {

            switch (AuthorizeStatus)
            {
                case "AUTHORIZE":
                    AuthorizeStatus = "1";
                    break;
                case "UNAUTHORIZE":
                    AuthorizeStatus = "0";
                    break;
                case "REJECTED":
                    AuthorizeStatus = "2";
                    break;
                default:
                    return AuthorizeStatus;
            }
            return AuthorizeStatus;
        }


        #endregion

        #region Convert Sum Data Tabel
        public static DataTable SumDataTabel(DataTable inputTabel)
        {
            DataTable NewTable = inputTabel.Clone();
            //List<AdditionalCheck> AdditionalCheckList = new List<AdditionalCheck>();
            var HeaderRowSysParam = 5;
            int HeaderRow = Convert.ToInt32(HeaderRowSysParam); //Header Template
            int HeaderFlagMandatory = Convert.ToInt32(HeaderRowSysParam) + 1; //Mandatory field && Non-Mandatory field
            int StartRow = HeaderRow + 2; //Start Row
            string SumCode = string.Empty;
            for (int i = 0; i < inputTabel.Rows.Count; i++)
            {
                DataRow dr = inputTabel.Rows[i];

                if (i >= HeaderRow)
                {
                    //DataRow[] drSelected = NewTable.Select("[" + NewTable.Columns[0].ToString() + "] = '" + dr[0].ToString() + "' AND [" + NewTable.Columns[1].ToString() + "] = '" + dr[1].ToString() + "' AND [" + NewTable.Columns[2].ToString() + "] = '" + dr[2].ToString() + "'", "");
                    DataRow[] drSelected = NewTable.Select("[" + NewTable.Columns[0].ToString() + "] = '" + dr[0].ToString() + "' AND [" + NewTable.Columns[1].ToString() + "] = '" + dr[1].ToString() + "' AND [" + NewTable.Columns[2].ToString() + "] = '" + dr[2].ToString() + "'", "");

                    if (drSelected.Length == 0)
                    {
                        DataRow drNew = NewTable.NewRow();
                        foreach (DataColumn dc in NewTable.Columns)
                        {
                            drNew[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        NewTable.Rows.Add(drNew);
                    }

                    else
                    {
                        AdditionalCheck AdditionalCheckRemak = new AdditionalCheck();
                        drSelected[0][4] = Convert.ToDecimal(dr[4]) + Convert.ToDecimal(drSelected[0][4]);
                        
                        drSelected[0][5] = string.Empty;
                        //NewTable.Rows.Add(dr);
                        //SumCode = drSelected[0][0] + " " + drSelected[0][1] + " " + drSelected[0][2];
                        //AdditionalCheckRemak.Employee_no = drSelected[0][0].ToString();
                        //AdditionalCheckRemak.Employee_name = drSelected[0][1].ToString();
                        //AdditionalCheckRemak.Component = drSelected[0][3].ToString();
                        //AdditionalCheckList.Add(AdditionalCheckRemak);
                        
                    }
                }
                else
                {
                    DataRow drNew = NewTable.NewRow();
                    foreach (DataColumn dc in NewTable.Columns)
                    {
                        drNew[dc.ColumnName] = dr[dc.ColumnName];
                    }
                    NewTable.Rows.Add(drNew);
                    
                }
               
            }


            return NewTable;
        }
        #endregion

        public static List<tbl_Appointment_Status_Information> GetLatestStatusAppointment(DateTime? EndDate,string Employee_Id)
        {
             //List<tbl_Appointment_Status_Information> ListStatusInfo = new List<tbl_Appointment_Status_Information>();
             IQueryable<tbl_Appointment_Status_Information> ListStatusInfo = null;
             var IDEmployee = Guid.Parse(Employee_Id);

             if (EndDate == null)
             {
                 ListStatusInfo = GeneralCore.AppointmentInformationStatusQuery(IDEmployee).OrderByDescending(o => o.Effective_Date).Take(1);
             }
             else
             {
                 ListStatusInfo = GeneralCore.AppointmentInformationStatusQuery(IDEmployee).Where(p => p.Effective_Date <= EndDate).OrderByDescending(o => o.Effective_Date).Take(1);
                 if (ListStatusInfo.Count() == 0)
                 {
                     ListStatusInfo = GeneralCore.AppointmentInformationStatusQuery(IDEmployee).OrderByDescending(o => o.Effective_Date).Take(1);
                 }
             } 
             return ListStatusInfo.ToList();
        }

        public static string FormatNPWP(string strTaxNo)
        {
            string result = "00.000.000.0-000.000";

            if(strTaxNo != "000000000000000")
            {

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";

                try
                {
                    str1 = strTaxNo.Substring(0, 2);
                    str2 = strTaxNo.Substring(2, 3);
                    str3 = strTaxNo.Substring(5, 3);
                    str4 = strTaxNo.Substring(8, 1);
                    str5 = strTaxNo.Substring(9, 3);
                    str6 = strTaxNo.Substring(12, 3);
                }
                catch(Exception ex)
                {
                    UIException.LogException(ex, "FormatNPWP(string strTaxNo) " + strTaxNo);
                }

                result = str1 + "." + str2 + "." + str3 + "." + str4 + "-" + str5 + "." + str6;
            }

            return result;
        }

        #region ConvertDateToString
        public static string ConvertDateToString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion ConvertDateToString

        #region WriteLogs WebServices Danamon
        public static void WriteLogs(string Desc, string strReq, StringBuilder sbTrace, Exception ex)
        {
            string strTraceToLog = null;

            if (Desc.Contains("Exception"))
            {
                sbTrace.Append("\r\n");
                sbTrace.Append("\r\n------------ " + Desc + " ------------");
                sbTrace.Append("\r\n" + UICommonFunction.ConvertDateToString(DateTime.Now) + " Exception");
                sbTrace.Append("\r\n" + ex.Message);
                sbTrace.Append("\r\n" + ex.StackTrace);
                strTraceToLog = sbTrace.ToString();
            }
            else
            {
                sbTrace.Append("\r\n");
                sbTrace.Append("\r\n------------ " + Desc + " ------------");
                sbTrace.Append("\r\n" + ConvertDateToString(DateTime.Now));
                sbTrace.Append("\r\n");
                sbTrace.Append(strReq);
                sbTrace.Append("\r\n------------ " + Desc + " ------------");
            }
        }
        #endregion WriteLogs WebServices Danamon

        #region Auto generate Date Payroll Schedule
        public static List<tbl_Cut_Off_Period> GetAutoGenerateDatePayrollSchedule(Guid payrollScheduleID, Guid orgID, DateTime? periodActive, int? startDate, int? endDate)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            List<tbl_Cut_Off_Period> getlist = new List<tbl_Cut_Off_Period>();
            int countMonth = 12;
            int addYearPeriodActive = periodActive.Value.Year;
            int DatePeriodActive = periodActive.Value.Month;
            try
            {
                var existingCutOffPeriod = db.tbl_Cut_Off_Period.Where(q => q.Payroll_Schedule_ID == payrollScheduleID && q.Cut_Off_Year == addYearPeriodActive).ToList();
                if (existingCutOffPeriod.Count > 0)
                {
                    //Delete existingCutOffPeriod
                    db.tbl_Cut_Off_Period.RemoveRange(existingCutOffPeriod);
                    db.SaveChanges();

                    if (startDate == 1)
                    {
                        DateTime selectStartDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(startDate));
                        DateTime selectEndDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(endDate));
                        for (int i = DatePeriodActive; i <= countMonth; i++)
                        {
                            tbl_Cut_Off_Period globalAttendanceSchedule = new tbl_Cut_Off_Period();
                            globalAttendanceSchedule.id = Guid.NewGuid();
                            globalAttendanceSchedule.Payroll_Schedule_ID = payrollScheduleID;
                            globalAttendanceSchedule.Cut_Off_Year = addYearPeriodActive;
                            globalAttendanceSchedule.Organization_ID = orgID;
                            globalAttendanceSchedule.Cut_Off_Start = selectStartDate;
                            globalAttendanceSchedule.Cut_Off_End = selectEndDate;
                            getlist.Add(globalAttendanceSchedule);

                            if (selectStartDate.Day != DateTime.DaysInMonth(selectStartDate.Year, selectStartDate.Month))
                            {
                                selectStartDate = selectStartDate.AddMonths(1);
                            }
                            else
                            {
                                selectStartDate = selectStartDate.AddDays(1).AddMonths(1).AddDays(-1);
                            }

                            if (selectEndDate.Day != DateTime.DaysInMonth(selectEndDate.Year, selectEndDate.Month))
                            {
                                selectEndDate = selectEndDate.AddMonths(1);
                            }
                            else
                            {
                                selectEndDate = selectEndDate.AddDays(1).AddMonths(1).AddDays(-1);
                            }
                        }
                    }
                    else
                    {
                        DateTime selectStartDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(startDate));
                        DateTime selectEndDate = new DateTime(addYearPeriodActive, periodActive.Value.AddMonths(1).Month, Convert.ToInt16(endDate));
                        for (int i = 0; i < countMonth; i++)
                        {
                            tbl_Cut_Off_Period globalAttendanceSchedule = new tbl_Cut_Off_Period();
                            globalAttendanceSchedule.id = Guid.NewGuid();
                            globalAttendanceSchedule.Payroll_Schedule_ID = payrollScheduleID;
                            globalAttendanceSchedule.Cut_Off_Year = addYearPeriodActive;
                            globalAttendanceSchedule.Organization_ID = orgID;
                            globalAttendanceSchedule.Cut_Off_Start = selectStartDate;
                            globalAttendanceSchedule.Cut_Off_End = selectEndDate;
                            getlist.Add(globalAttendanceSchedule);
                            selectStartDate = selectStartDate.AddMonths(1);
                            selectEndDate = selectEndDate.AddMonths(1);
                        }
                    }
                }
                else
                {
                    if (startDate == 1)
                    {
                        DateTime selectStartDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(startDate));
                        DateTime selectEndDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(endDate));
                        for (int i = 0; i < countMonth; i++)
                        {
                            tbl_Cut_Off_Period globalAttendanceSchedule = new tbl_Cut_Off_Period();
                            globalAttendanceSchedule.id = Guid.NewGuid();
                            globalAttendanceSchedule.Payroll_Schedule_ID = payrollScheduleID;
                            globalAttendanceSchedule.Cut_Off_Year = addYearPeriodActive;
                            globalAttendanceSchedule.Organization_ID = orgID;
                            globalAttendanceSchedule.Cut_Off_Start = selectStartDate;
                            globalAttendanceSchedule.Cut_Off_End = selectEndDate;
                            getlist.Add(globalAttendanceSchedule);

                            if (selectStartDate.Day != DateTime.DaysInMonth(selectStartDate.Year, selectStartDate.Month))
                            {
                                selectStartDate = selectStartDate.AddMonths(1);
                            }
                            else
                            {
                                selectStartDate = selectStartDate.AddDays(1).AddMonths(1).AddDays(-1);
                            }

                            if (selectEndDate.Day != DateTime.DaysInMonth(selectEndDate.Year, selectEndDate.Month))
                            {
                                selectEndDate = selectEndDate.AddMonths(1);
                            }
                            else
                            {
                                selectEndDate = selectEndDate.AddDays(1).AddMonths(1).AddDays(-1);
                            }
                        }
                    }
                    else
                    {
                        DateTime selectStartDate = new DateTime(addYearPeriodActive, periodActive.Value.Month, Convert.ToInt16(startDate));
                        DateTime selectEndDate = new DateTime(addYearPeriodActive, periodActive.Value.AddMonths(1).Month, Convert.ToInt16(endDate));
                        for (int i = 0; i < countMonth; i++)
                        {
                            tbl_Cut_Off_Period globalAttendanceSchedule = new tbl_Cut_Off_Period();
                            globalAttendanceSchedule.id = Guid.NewGuid();
                            globalAttendanceSchedule.Payroll_Schedule_ID = payrollScheduleID;
                            globalAttendanceSchedule.Cut_Off_Year = addYearPeriodActive;
                            globalAttendanceSchedule.Organization_ID = orgID;
                            globalAttendanceSchedule.Cut_Off_Start = selectStartDate;
                            globalAttendanceSchedule.Cut_Off_End = selectEndDate;
                            getlist.Add(globalAttendanceSchedule);
                            selectStartDate = selectStartDate.AddMonths(1);
                            selectEndDate = selectEndDate.AddMonths(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, ex.Source);
            }
            return getlist;
            
        }
        #endregion

        #region
        public static string ConvertAttendanceSyncStatus(int? intStatusCode)
        {
            string statusDescription = "";
            switch (intStatusCode)
            {
                case 0:
                    statusDescription = GlobalVariable.CONST_UPLOAD_ATTENDANCE_STR_STATUS_IN_PROGRESS;
                    break;
                case 1:
                    statusDescription = GlobalVariable.CONST_UPLOAD_ATTENDANCE_STR_STATUS_COMPLETED;
                    break;
                case 2:
                    statusDescription = GlobalVariable.CONST_UPLOAD_ATTENDANCE_STR_STATUS_FAIL;
                    break;
                case 3:
                    statusDescription = GlobalVariable.CONST_UPLOAD_ATTENDANCE_STR_STATUS_INVALID_TEMPLATE;
                    break;
            }

            return statusDescription;
        }
        #endregion
		public static DateTime AddTimeToDateTime(string date, string time)
        {
            DateTime Date = new DateTime();
            try
            {
                if (IsValidFormattedDate(date))
                {
                    //convert Datetime
                    System.Globalization.CultureInfo ciEngGB = new System.Globalization.CultureInfo("en-GB", true);
                    Date = DateTime.Parse(date + " " + time, ciEngGB);
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "UICommonFunction.AddTimeToDateTiime");
                return Date;
            }
            return Date;
        }
        public static string ConvertStatustoStr(int? intStatusCode, int? intAuthorizeStatus)
        {
            string statusDescription = "";

            if (intStatusCode == 1 && intAuthorizeStatus == 0) // pending 1 0
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_PENDING;
            }

            else if (intStatusCode == 1 && intAuthorizeStatus == 1)  //approve 1 1
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_APPROVED;
            }

            else if (intStatusCode == 2 && intAuthorizeStatus == 1) //reject 2 1
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_REJECT;
            }

            else if (intStatusCode == 0 && intAuthorizeStatus == 1) //cancel 0 1
            {
                statusDescription = GlobalVariable.CONST_STR_STATUS_CANCEL;
            }

            return statusDescription;
        }
        public static tbl_Cut_Off_Period DefaultCutoff(Guid OrganizationID)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            tbl_Cut_Off_Period model = new tbl_Cut_Off_Period();
            try
            {
                var Period = db.Get_Payroll_Period(OrganizationID).FirstOrDefault();
                Period.Payroll_Period_End_Date = Period.Payroll_Period_End_Date.Value.AddMonths(1);
                model = db.tbl_Cut_Off_Period.Where(p => p.Organization_ID == OrganizationID && p.Cut_Off_End <= Period.Payroll_Period_End_Date).OrderByDescending(p => p.Cut_Off_End).FirstOrDefault();
                return model;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "UICommonFunction.DefaultCutoff");
                return model;
            }

        }

        public static List<string> GetRestrictionLeave(string OrganizationId)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var GuidOrgId = Guid.Parse(OrganizationId);
            List<string> ListTmp = new List<string>();
            try
            {
                ListTmp = db.tbl_Employee_Leave_Entitlement_Restriction.Where(s => s.Organization_ID == GuidOrgId).Select(s=>s.Leave_Type).ToList();
                return ListTmp;
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "UICommonFunction.LeaveRestrictionList");
                return ListTmp;
            }
        }

        public class NumeralAlphaCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int nIndexX = x.Replace(":", " ").IndexOf(" ");
                int nIndexY = y.Replace(":", " ").IndexOf(" ");
                bool bSpaceX = nIndexX > -1;
                bool bSpaceY = nIndexY > -1;

                long nX;
                long nY;
                if (bSpaceX && bSpaceY)
                {
                    if (long.TryParse(x.Substring(0, nIndexX).Replace(".", ""), out nX)
                        && long.TryParse(y.Substring(0, nIndexY).Replace(".", ""), out nY))
                    {
                        if (nX < nY)
                        {
                            return -1;
                        }
                        else if (nX > nY)
                        {
                            return 1;
                        }
                    }
                }
                else if (bSpaceX)
                {
                    if (long.TryParse(x.Substring(0, nIndexX).Replace(".", ""), out nX)
                        && long.TryParse(y, out nY))
                    {
                        if (nX < nY)
                        {
                            return -1;
                        }
                        else if (nX > nY)
                        {
                            return 1;
                        }
                    }
                }
                else if (bSpaceY)
                {
                    if (long.TryParse(x, out nX)
                        && long.TryParse(y.Substring(0, nIndexY).Replace(".", ""), out nY))
                    {
                        if (nX < nY)
                        {
                            return -1;
                        }
                        else if (nX > nY)
                        {
                            return 1;
                        }
                    }
                }
                else
                {
                    if (long.TryParse(x, out nX)
                        && long.TryParse(y, out nY))
                    {
                        if (nX < nY)
                        {
                            return -1;
                        }
                        else if (nX > nY)
                        {
                            return 1;
                        }
                    }
                }
                return x.CompareTo(y);
            }
        }
    }
}    