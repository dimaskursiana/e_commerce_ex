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
using APP_MODEL.ModelData;
using System.Globalization;


namespace APP_CORE.SetData
{
    public class MasterCore
    {
        #region Core Master Orgnization Working Time
        public static List<string> ChkWorkingDay(List<string> DayChk)
        {
            List<string> Day = new List<string>();
            if (DayChk.Count < 5)
            {
                for (int i = 1; i < 6; i++)
                {
                    Day.Add(i.ToString());
                }
                return Day;
            }
            else
            {
             return DayChk;
            }
            
        }
        #endregion


    }
}
