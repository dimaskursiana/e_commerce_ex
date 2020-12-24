using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_CORE.SetData
{
    public class Threed
    {
        string strMessage = string.Empty;
        string TrialParams = "1";

        public Threed()
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            string TrialParams = db.tbl_SysParam.Where(p => p.Param_Code == "TrialParams").FirstOrDefault().Value;
            TrialParams = TrialParams == "" ? "1" : TrialParams;
        }


        #region ex Threed
        //public async Task Exec_Tax_Calculation<T>(this IList<T> list, string NOTIFICATION_CODE, string COST_CENTER_CODE, string NPK, DataSet dataDetail = null)
        //{
        //    try
        //    {
        //        await Task.Run(() => { }).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.LogException(ex, "Notification ERROR", out strMessage);
        //    }
        //}

        #endregion

        public async Task Exec_Tax_Calculation(string Calculation_Id, bool SeverancePayment,bool isRecalculate_OnHistory)
        { 
            try
            { 
                int lOOP_TRY = 1;
                bool status = false;
                Execute_calculation:
                try
                {
                    await Task.Run(() => { SP_TAX_CALCULATION(Calculation_Id); }).ConfigureAwait(false);
                    status = true;
                }
                catch
                {
                    lOOP_TRY++;
                    status = false;
                }
                if (lOOP_TRY <= Convert.ToInt16(TrialParams) && status == false && !isRecalculate_OnHistory)
                {
                    goto Execute_calculation;
                }
                if (lOOP_TRY > Convert.ToInt16(TrialParams) && status == false && !isRecalculate_OnHistory)
                {
                    await Task.Run(() => { SP_TAX_CALCULATION(Calculation_Id); }).ConfigureAwait(false);
                }  
            } 
            catch (DbEntityValidationException E)
            {  
            var errorMessages = E.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                 
            var fullErrorMessage = string.Join("; ", errorMessages); 
            var exceptionMessage = string.Concat(E.Message, " The validation errors are: ", fullErrorMessage);
            long ErrlogResult = Error.LogException(E, "Exec_Tax_Calculation ERROR1", out strMessage);
            }
            catch (DbUpdateException E)
            { 
                long ErrlogResult = Error.LogException(E, "Exec_Tax_Calculation ERROR2", out strMessage);
            }
            catch (Exception E)
            {  
                long ErrlogResult = Error.LogException(E, "Exec_Tax_Calculation ERROR3", out strMessage);
            }
        }
         
        private void SP_TAX_CALCULATION(string Calculation_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            db.Configuration.LazyLoadingEnabled = false;
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = Convert.ToInt32(db.tbl_SysParam.Where(p=> p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);
            long Result = db.SP_TAX_CALCULATION(Calculation_Id);
        }

        private void SP_TAX_SEVERANCE(string Calculation_Id)
        { 
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            db.Configuration.LazyLoadingEnabled = false;
            var objectContext = (db as IObjectContextAdapter).ObjectContext; 
            objectContext.CommandTimeout = Convert.ToInt32(db.tbl_SysParam.Where(p => p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);

            int Result = db.SP_TAX_SEVERANCE_CALCULATION(Calculation_Id);
        }

        private void SP_TAX_CALCULATION_EMPLOYEE(string Calculation_Id, int startRow, int endRow)
        { 
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Convert.ToInt32(db.tbl_SysParam.Where(p => p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);
            long Result = db.SP_TAX_CALCULATION_EMPLOYEE(Calculation_Id, startRow, endRow); 
        }

        private void SP_TAX_CALCULATION_FINALIZE(string Calculation_Id)
        {
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Convert.ToInt32(db.tbl_SysParam.Where(p => p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);
            long Result = db.SP_TAX_CALCULATION_FINALIZE(Calculation_Id);
        }

        private void SP_TAX_CALCULATION_EMPLOYEE_PART(string Calculation_Id, string Employee_ID)
        { 
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Convert.ToInt32(db.tbl_SysParam.Where(p => p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);
            long Result = db.SP_TAX_CALCULATION_EMPLOYEE_PART(Calculation_Id, Employee_ID);
        }

        public async Task SP_TAX_CALCULATION_EMPLOYEE_ASYNC(string Calculation_Id, int startRow, int endRow)
        { 
            int lOOP_TRY = 1;
            bool status = false;
            Execute_calculation:
            try
            { 
                await Task.Run(() => { SP_TAX_CALCULATION_EMPLOYEE(Calculation_Id, startRow, endRow); }).ConfigureAwait(false);
                status = true;
            }
            catch
            {
                lOOP_TRY++;
                status = false;
            }
            if (lOOP_TRY <= Convert.ToInt16(TrialParams) && status == false)
            {
                goto Execute_calculation;
            }
            if (lOOP_TRY > Convert.ToInt16(TrialParams) && status == false)
            {
                try
                {
                    await Task.Run(() => { SP_TAX_CALCULATION_EMPLOYEE(Calculation_Id, startRow, endRow); }).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    string Exec_Script = "SP_TAX_CALCULATION_EMPLOYEE '" + Calculation_Id + "'," + startRow.ToString() + "," + endRow.ToString();
                    try
                    {
                        await Task.Run(() => { Error.LogException(Exec_Script, "RETRIGER_SP_TAX_CALCULATION_EMPLOYEE", out strMessage); }).ConfigureAwait(false);
                    }
                    catch (Exception Ex)
                    {
                        Error.LogException(Ex, "ERROR_RETRIGER_SP_TAX_CALCULATION_EMPLOYEE", out strMessage);
                    }
                }
            }
            
        }

        public async Task SP_TAX_CALCULATION_FINALIZE_ASYNC(string Calculation_Id)
        {
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                string TrialParams = db.tbl_SysParam.Where(p => p.Param_Code == "TrialParams").FirstOrDefault().Value;
                TrialParams = TrialParams == "" ? "1" : TrialParams;

                int lOOP_TRY = 1;
                bool status = false;
                Execute_calculation:
                try
                {
                    await Task.Run(() => { SP_TAX_CALCULATION_FINALIZE(Calculation_Id); }).ConfigureAwait(false); 
                    status = true;
                }
                catch (Exception E)
                { 
                    lOOP_TRY++;
                    status = false;
                    Error.LogException(E, "SP_TAX_CALCULATION_FINALIZE_ASYNC ERROR3", out strMessage); 
                }
                if (lOOP_TRY <= Convert.ToInt16(TrialParams) && status == false)
                {
                    goto Execute_calculation;
                }
                if (lOOP_TRY > Convert.ToInt16(TrialParams) && status == false)
                {
                    string Exec_Script = "SP_TAX_CALCULATION_FINALIZE '" + Calculation_Id + "'";
                    try
                    {
                        await Task.Run(() => { Error.LogException(Exec_Script, "RETRIGER_SP_TAX_CALCULATION_EMPLOYEE", out strMessage); }).ConfigureAwait(false);
                    }
                    catch (Exception Ex)
                    {
                        Error.LogException(Ex, "ERROR_SP_TAX_CALCULATION_FINALIZE", out strMessage);
                    }
                     
                }   
            }
            catch (DbEntityValidationException E)
            {  
            var errorMessages = E.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                 
            var fullErrorMessage = string.Join("; ", errorMessages); 
            var exceptionMessage = string.Concat(E.Message, " The validation errors are: ", fullErrorMessage);
            long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_FINALIZE_ASYNC ERROR1", out strMessage);
            }
            catch (DbUpdateException E)
            {
                long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_FINALIZE_ASYNC ERROR2", out strMessage);
            }
            catch (Exception E)
            {
                long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_FINALIZE_ASYNC ERROR3", out strMessage);
            }
        }

        public async Task SP_TAX_CALCULATION_EMPLOYEE_PART_ASYNC(string Calculation_Id,string Employee_Id)
        {
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                string TrialParams = db.tbl_SysParam.Where(p => p.Param_Code == "TrialParams").FirstOrDefault().Value;
                TrialParams = TrialParams == "" ? "1" : TrialParams;

                int lOOP_TRY = 1;
                bool status = false;
                Execute_calculation:
                try
                {
                    await Task.Run(() => { SP_TAX_CALCULATION_EMPLOYEE_PART(Calculation_Id, Employee_Id); }).ConfigureAwait(false);
                    status = true;
                }
                catch (Exception E)
                {
                    lOOP_TRY++;
                    status = false;
                    Error.LogException(E, "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC ERROR3", out strMessage);
                }
                if (lOOP_TRY <= Convert.ToInt16(TrialParams) && status == false)
                {
                    goto Execute_calculation;
                }
                if (lOOP_TRY > Convert.ToInt16(TrialParams) && status == false)
                {
                    string Exec_Script = "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC '" + Calculation_Id + "'";
                    try
                    {
                        await Task.Run(() => { Error.LogException(Exec_Script, "RETRIGER_SP_TAX_CALCULATION_EMPLOYEE_ASYNC", out strMessage); }).ConfigureAwait(false);
                    }
                    catch (Exception Ex)
                    {
                        Error.LogException(Ex, "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC", out strMessage);
                    }

                }
            }
            catch (DbEntityValidationException E)
            {
                var errorMessages = E.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(E.Message, " The validation errors are: ", fullErrorMessage);
                long ErrlogResult = Error.LogException(E, "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC ERROR1", out strMessage);
            }
            catch (DbUpdateException E)
            {
                long ErrlogResult = Error.LogException(E, "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC ERROR2", out strMessage);
            }
            catch (Exception E)
            {
                long ErrlogResult = Error.LogException(E, "ERROR_SP_TAX_CALCULATION_EMPLOYEE_ASYNC ERROR3", out strMessage);
            }
        }
      

        public async Task SP_TAX_CALCULATION_SEVERANCE_ASYNC(string Calculation_Id)
        {
            try
            {
                ModelEntitiesWebsite db = new ModelEntitiesWebsite(); 
                string TrialParams = db.tbl_SysParam.Where(p => p.Param_Code == "TrialParams").FirstOrDefault().Value;
                TrialParams = TrialParams == "" ? "1" : TrialParams;

                int lOOP_TRY = 1;
                bool status = false;
                Execute_calculation:
                try
                {
                    await Task.Run(() => { SP_TAX_SEVERANCE(Calculation_Id); }).ConfigureAwait(false);
                    status = true;
                }
                catch (Exception E)
                {
                    lOOP_TRY++;
                    status = false;
                    Error.LogException(E, "SP_TAX_CALCULATION_SEVERANCE_ASYNC ERROR3", out strMessage);
                }
                if (lOOP_TRY <= Convert.ToInt16(TrialParams) && status == false)
                {
                    goto Execute_calculation;
                }
                if (lOOP_TRY > Convert.ToInt16(TrialParams) && status == false)
                {
                    string Exec_Script = "SP_TAX_CALCULATION_SEVERANCE_ASYNC '" + Calculation_Id + "'";
                    try
                    {
                        await Task.Run(() => { Error.LogException(Exec_Script, "RETRIGER_SP_TAX_CALCULATION_SEVERANCE_ASYNC", out strMessage); }).ConfigureAwait(false);
                    }
                    catch (Exception Ex)
                    {
                        Error.LogException(Ex, "SP_TAX_CALCULATION_SEVERANCE_ASYNC", out strMessage);
                    }

                }
            }
            catch (DbEntityValidationException E)
            {
                var errorMessages = E.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(E.Message, " The validation errors are: ", fullErrorMessage);
                long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_SEVERANCE_ASYNC ERROR1", out strMessage);
            }
            catch (DbUpdateException E)
            {
                long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_SEVERANCE_ASYNC ERROR2", out strMessage);
            }
            catch (Exception E)
            {
                long ErrlogResult = Error.LogException(E, "SP_TAX_CALCULATION_SEVERANCE_ASYNC ERROR3", out strMessage);
            }
        }

    } 
}
