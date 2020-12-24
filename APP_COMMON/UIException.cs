using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text; 
using System.Configuration; 
using System.Threading.Tasks; 
using APP_MODEL.ModelData;

namespace APP_COMMON
{
    public class UIException
    {
        #region Default Constructor
        public UIException()
        {
        }
        #endregion

        #region Constant
        private const string CONST_EVENT_LOG_KEY = "UIEventLog";
        private const string CONST_LOG_FILE_KEY = "UIErrFileLog";
        private const string CONST_APP_ID = "AppId";
        public static ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        #endregion
        public static string ErrMessage = string.Empty;
        /// <summary>
        /// Method use to Log UI exception
        /// </summary>
        /// <param name="ex">Exception from UI</param>
        /// <param name="strSource">Source of exception</param>
        #region public static void : LogException
        public static long LogException(Exception ex, string strSource,string UserId = "-")
        {
            //ImFOException objIFOException = null; 
            long lngLogSuccess = -1;  
            try
            {
                if(UserId == null)
                {
                    UserId = "-";
                }
                tbl_Error_Log ErrorLog = new tbl_Error_Log()
                {
                    id = Guid.NewGuid(),
                    Created_By = UserId ,
                    Error_Source = strSource,
                    Error_Description = ex.ToString(),
                    Log_Date = DateTime.Now
                };
                db = new ModelEntitiesWebsite();
                db.tbl_Error_Log.Add(ErrorLog);
                db.SaveChanges();
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "APP_COMMON.UIException.LogException()");
                    lngLogSuccess = 0;
                }
                catch 
                {
                    //strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }

            return lngLogSuccess;
        }

        public static long LogException(string error, string strSource, string UserId = "-")
        {
            //ImFOException objIFOException = null; 
            if (UserId == null)
            {
                UserId = "-";
            }
            long lngLogSuccess = -1;
            try
            {
                tbl_Error_Log ErrorLog = new tbl_Error_Log()
                {
                    id = Guid.NewGuid(),
                    Created_By = UserId,
                    Error_Source = strSource,
                    Error_Description = error,
                    Log_Date = DateTime.Now
                }; 
                db.tbl_Error_Log.Add(ErrorLog);
                db.SaveChanges();
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "APP_COMMON.UIException.LogException()");
                    lngLogSuccess = 0;
                }
                catch
                {
                    //strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }

            return lngLogSuccess;
        }

        #endregion

        /// <summary>
        /// Log To Windows Event Log
        /// </summary>
        /// <param name="r_strErrCode">Error Code</param>
        /// <param name="r_strErrSource">Error Source</param>
        /// <param name="r_strErrDscp">Error Description</param>
        #region Log Error To Windows Event Log
        private static long LogToEventLog(string strErrDscp, string strErrSource)
        {
            //Variable declaration
            EventLog Log = null;
            string strLog = null;
            string strError = null;
            string strEventSource = null;
            long lngLogged = -1;

            try
            {
                strLog = "";// System.Configuration.ConfigurationManager.AppSettings[CONST_EVENT_LOG_KEY];

                strEventSource = strLog + "." + strErrSource;

                //Construct Error String to store in Event Log
                strError = "Error Source : " + strErrSource + "\n" +
                    "Error Description : " + strErrDscp;

                if (strErrSource == "" || strErrSource == null)
                {
                    strEventSource = strLog + "." + "UIException.LogToEventLog()";
                }
                //check existance of event log
                if (!EventLog.SourceExists(strEventSource))
                {
                    //if Event Log not exist, create new event log
                    EventLog.CreateEventSource(strEventSource, strLog);
                }

                //Create a event Log
                Log = new EventLog();

                //Set event log source
                Log.Source = strEventSource;

                //Write error to Event Log
                Log.WriteEntry(strError, EventLogEntryType.Error);
                lngLogged = 0;

            }//end of try

            catch (Exception ex)
            {
                //If error, Log To Text File
                try
                {
                    //Log original error to text file
                    LogToFile(strErrSource, strErrDscp);

                    //Log current error to text file
                    string strCurrSource = "UIException.LogToEventLog()";
                    LogToFile(strCurrSource, ex.Message);

                }//end of inner try

                catch (Exception ex1)
                {
                    throw ex1;

                }//end of inner catch

            }//end of try

            finally
            {
                //close the event log
                if (Log != null)
                {
                    Log.Close();
                }

            }//end of finally

            //return status
            return lngLogged;

        }//end of LogToEventLog(string strErrSource, string strErrDscp)
        #endregion

        /// <summary>
        /// Log To Text File
        /// </summary>
        /// <param name="r_strErrCode">Error Code</param>
        /// <param name="r_strErrSource">Error Source</param>
        /// <param name="r_strErrDscp">Error Description</param>
        #region Log To File
        private static long LogToFile(string strErrSource, string strErrDscp)
        {
            //Variable declaration
            FileStream fsLog = null;
            StreamWriter swLog = null;
            string strError = "";
            string strLogPath = "";
            long lngSuccess = -1;

            try
            {
                strLogPath = "";// System.Configuration.ConfigurationManager.AppSettings[CONST_LOG_FILE_KEY];

                //Construct Error string To log into text file
                strError = "--- " + DateTime.UtcNow.ToString() + " : " +
                     " : " + strErrSource + " : " +
                    strErrDscp + "\n";

                string strLogDir = System.IO.Path.GetDirectoryName(strLogPath);

                if (!System.IO.Directory.Exists(strLogDir))
                {
                    System.IO.Directory.CreateDirectory(strLogDir);
                }

                //Create or open a file
                fsLog = new FileStream(strLogPath, FileMode.Append, FileAccess.Write);

                //instantiate a Stream Writer
                swLog = new StreamWriter(fsLog);

                //Search for EOF
                swLog.BaseStream.Seek(0, SeekOrigin.End);

                //write Error to file
                swLog.Write(strError);
                lngSuccess = 0;

            }//end of try

            catch (Exception ex)
            {
                throw ex;

            }//end of catch

            finally
            {
                //Close the Stream Writer
                if (swLog != null)
                {
                    swLog.Flush();
                    swLog.Close();
                }

                //Close the File Stream
                if (fsLog != null)
                {
                    fsLog.Close();
                }

            }//end of finally

            //return Status
            return lngSuccess;

        }//end of LogToFile(string strErrSource, string strErrDscp)
        #endregion

       
    }
}
