using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APP_COMMON
{
    public class WebApiLogger
    {
        private const string const_LOGFILE_KEY = "LogFile";
        private const string const_REGISTRY_KEY = "SOFTWARE\\BPO7\\OnlinePaymentDanamon";
        private const string const_DEFAULT_REG_KEY = "default";
        Logger m_oLogger = null;
        public WebApiLogger(string strHostName)
        {
            try
            {

                string strRegKey;
                string strAppId = ConfigurationManager.AppSettings["AppId"];
                if (strAppId == null || strAppId == string.Empty)
                {
                    strRegKey = const_REGISTRY_KEY + "\\" + const_DEFAULT_REG_KEY;
                }
                else
                {
                    strRegKey = const_REGISTRY_KEY + "\\" + strAppId;
                }

                string strLogFileKey = const_LOGFILE_KEY;
                if (strHostName != null && strHostName != string.Empty)
                {
                    strLogFileKey = strLogFileKey + "_" + strHostName;
                }
                string strLogFile = Logger.GetValFrReg(strRegKey, strLogFileKey);
                m_oLogger = new Logger();
                m_oLogger.LogFile = strLogFile;
            }
            catch { }

        }
        private static object objLock = new object();
        public void WriteAsych(string strTrace)
        {
            try
            {
                string strMessageForwarderLog = ConfigurationManager.AppSettings["LogServices"];
                if (strMessageForwarderLog == null || strMessageForwarderLog == "Y")
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.workerProcess), strTrace);
            }
            catch //(Exception ex)
            {
                //supress exception
            }
        }
        private void workerProcess(object stateInfo)
        {
            try
            {
                string strTrace = (string)stateInfo;
                lock (objLock)
                {
                    m_oLogger.Log(strTrace);
                }
            }
            catch //(Exception ex)
            {
                //supress exception
            }
        }
    }
}
