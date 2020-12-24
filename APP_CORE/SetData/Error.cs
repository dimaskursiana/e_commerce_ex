using APP_MODEL.ModelData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_CORE.SetData
{ 
    public class Error
    {
        private static ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        #region public static void : LogException
        public static long LogException(Exception ex, string strSource, out string strMessage)
        {
            strMessage = string.Empty;
            long lngLogSuccess = -1;

            try
            {
                db.SP_ERROR_LOG(strSource, ex.Message + " | " + ex.StackTrace);
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "public static long LogException()");
                    lngLogSuccess = 0;
                }
                catch (Exception excep)
                {
                    strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }
            return lngLogSuccess;
        }

        public static long LogException(string ex, string strSource, out string strMessage)
        {
            strMessage = string.Empty;
            long lngLogSuccess = -1;

            try
            {
                db.SP_ERROR_LOG(strSource, ex);
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "public static long LogException()");
                    lngLogSuccess = 0;
                }
                catch (Exception excep)
                {
                    strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }
            return lngLogSuccess;
        }
        #endregion
        #region Log Error To Windows Event Log
        public static long LogToEventLog(string strErrDscp, string strErrSource)
        {
            //Variable declaration
            EventLog Log = null;
            string strLog = null;
            string strError = null;
            string strEventSource = null;
            long lngLogged = -1;

            try
            {
                strLog = "FDLMS_UIEventLog";

                strEventSource = strLog + "." + strErrSource;

                //Construct Error String to store in Event Log
                strError = "Error Source : " + strErrSource + "\n" +
                    "Error Description : " + strErrDscp;

                if (strErrSource == "" || strErrSource == null)
                {
                    strEventSource = strLog + "." + "public static long LogToEventLog()";
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
                throw ex;
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
        #region Log To File
        public static long LogToFile(string strErrSource, string strErrDscp)
        {
            //Variable declaration
            FileStream fsLog = null;
            StreamWriter swLog = null;
            string strError = "";
            string strLogPath = "C:\"UIFDLMSErr.Log";
            long lngSuccess = -1;

            try
            {
                //Construct Error string To log into text file
                strError = "--- " + DateTime.Now.ToString() + " : " +
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
