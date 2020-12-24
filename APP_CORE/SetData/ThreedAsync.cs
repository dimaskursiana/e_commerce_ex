using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_CORE.SetData
{
    public class ThreedAsync
    {
        string strMessage = string.Empty; 

        public long Connector(List<string> Params,string type,string Connection)
        {
            long results = 0;
            try
            {
                if (type == "TaxCalculation")
                {
                    #region remark for manual command
                    //string Command_Query = "EXECUTE [dbo].[SP_TAX_CALCULATION] '" + Params[0] + "' GO";

                    //Execute_SP(Command_Query, Connection);
                    #endregion

                    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
                    var Mode_Partial = db.tbl_SysParam.Where(p => p.Param_Code == "PATRIAL_CALCULATION").FirstOrDefault();
                    Guid Calculate_Id_Guid = Guid.Parse(Params[0]);
                    var calculation = db.tbl_Payroll_Calculation.Where(p => p.id == Calculate_Id_Guid).FirstOrDefault();

                    if (Mode_Partial.Value == "Y" && !calculation.Recalculate && calculation.YearAndCorrection != true)
                    {
                        int MaxEmployee = 20;
                        var MaxEmployeeParam = db.tbl_SysParam.Where(p => p.Param_Code == "PATRIAL_CALCULATION_EMPLOYEE_MAX").FirstOrDefault();
                        if (MaxEmployeeParam != null)
                            MaxEmployee = Convert.ToInt32(MaxEmployeeParam.Value);

                        int MaxThreed = 4;
                        var MaxThreedParam = db.tbl_SysParam.Where(p => p.Param_Code == "PATRIAL_CALCULATION_THREED_MAX").FirstOrDefault();
                        if (MaxThreedParam != null)
                            MaxThreed = Convert.ToInt32(MaxThreedParam.Value);

                        if (MaxThreed == 0)
                        {
                            var Cal_Employe = db.tbl_Payroll_Calculation_Employee.Where(p => p.Payroll_Calculation_ID == Calculate_Id_Guid).Count();
                            MaxEmployee = Convert.ToInt32(Math.Ceiling((double)(Cal_Employe / MaxThreed)));
                            int sisa = Convert.ToInt32(Math.Ceiling((double)(Cal_Employe % MaxThreed)));
                            MaxThreed = sisa > 0 ? MaxThreed + 1 : MaxThreed;
                        }
                        else
                        {
                            //List<Calculate_Employee> List_Thred = new List<Calculate_Employee>();
                            var Cal_Employe = db.tbl_Payroll_Calculation_Employee.Where(p => p.Payroll_Calculation_ID == Calculate_Id_Guid).Count();
                            MaxThreed = Convert.ToInt32(Math.Ceiling((double)(Cal_Employe / MaxEmployee)));
                            int sisa = Convert.ToInt32(Math.Ceiling((double)(Cal_Employe % MaxEmployee)));
                            MaxThreed = sisa > 0 ? MaxThreed + 1 : MaxThreed;
                        }
                        int startRow = 1;
                        int endRow = MaxEmployee;
                        Threed NewThreed = new Threed();
                        for (int i = 0; i < MaxThreed; i++)
                        {
                            NewThreed = new Threed();
                            NewThreed.SP_TAX_CALCULATION_EMPLOYEE_ASYNC(Params[0], startRow, endRow);
                            startRow = startRow + MaxEmployee;
                            endRow = endRow + MaxEmployee; 
                        }
                        NewThreed = new Threed();
                        NewThreed.SP_TAX_CALCULATION_FINALIZE_ASYNC(Params[0]);
                        if (Params[2] == "Severance")
                        {
                            NewThreed.SP_TAX_CALCULATION_SEVERANCE_ASYNC(Params[1]);
                        }
                        MaxEmployee = 0; 
                    }
                    else
                    {
                        Threed NewThreed = new Threed();
                        NewThreed.Exec_Tax_Calculation(Params[0], false, calculation.Recalculate);
                    }
                      
                }

                if (type == "ReTaxCalculation")
                {
                    Threed NewThreed = new Threed();
                    NewThreed.SP_TAX_CALCULATION_EMPLOYEE_PART_ASYNC(Params[0], Params[3]);
                }

            }
            catch (Exception ex)
            {
                results = -1;
                Error.LogException(ex, "ThreedAsync.Connector", out strMessage);

            }
            return results;
        }
         
        private void Execute_SP(string SP_Command,string Connection)
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(Connection)
            {
                ConnectTimeout = 4000,
                AsynchronousProcessing = true
            };

            SqlConnection conn = new SqlConnection(connectionBuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand(SP_Command, conn);
            try
            {
                conn.Open();

                //The actual T-SQL execution happens in a separate work thread.
                cmd.BeginExecuteReader(new AsyncCallback(MyCallbackFunction), cmd);
            }
            catch (SqlException se)
            {
                //ToDo : Swallow exception log
            }
            catch (Exception ex)
            {
                //ToDo : Swallow exception log
            }
        }
         
        private void MyCallbackFunction(IAsyncResult asyncResult)
        {
            try
            {
                //un-box the AsynState back to the SqlCommand
                SqlCommand cmd = (SqlCommand)asyncResult.AsyncState;
                SqlDataReader reader = cmd.EndExecuteReader(asyncResult); 
                if (cmd.Connection.State.Equals(ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //ToDo : Swallow exception log
            }
        }

    }
     
}
