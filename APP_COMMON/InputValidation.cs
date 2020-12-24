using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APP_COMMON
{
    public class InputValidation
    {
        /// <summary>
        /// Check if string is a decimal value. Will pass even if string value has "." or ","
        /// </summary>
        /// <param name="strNumber">String to be validated</param>
        /// <returns>True if it is a decimal</returns>
        #region private bool: IsDecimal
        public bool IsDecimal(string strNumber, string strAllowNegatif, string strMaxDecimalPlaces, string strAllowZero)
        {
            bool blIsDecimal = true;

            try
            {
                if (UICommonFunction.IsDecimal(strNumber))
                {
                    if (UICommonFunction.IsDecimal(strNumber, int.Parse(strMaxDecimalPlaces)))
                    {
                        if (strAllowZero == "0") //0 means not allow zero
                        {
                            if (Convert.ToDecimal(strNumber) == 0)
                                blIsDecimal = false;
                        }

                        if (strAllowNegatif == "0") //0 means not allow negatif
                        {
                            if (Convert.ToDecimal(strNumber) < 0) //Negatif value
                                blIsDecimal = false;
                        }

                    }
                    else
                        blIsDecimal = false;
                }
                else
                    blIsDecimal = false;
            }
            catch
            {
                blIsDecimal = false;
            }

            return blIsDecimal;
        }
        #endregion

        /// <summary>
        /// Check the decimal value length against with Max length
        /// </summary>
        /// <param name="decAmount">Decimal value to be validated</param>
        /// <param name="iMaxLength">Maximum Length that allow</param>
        /// <returns>True if the value within the max length else return false</returns>
        #region private bool: IsValidDecimalLength
        public bool IsValidDecimalLength(string strAmount, int iMaxLength)
        {
            bool blnValid = false;
            string[] strArrayAmount = null;

            try
            {
                strArrayAmount = strAmount.ToString().Trim().Split(new char[] { '.' });

                if (strArrayAmount.GetValue(0).ToString().Length <= iMaxLength)
                    blnValid = true;
            }
            catch
            { }

            return blnValid;
        }
        #endregion

        /// <summary>
        /// Check if string is date time value
        /// </summary>
        /// <param name="strDateTime">String to be validated</param>
        /// <returns>True if it is date time</returns>
        #region private bool : IsDateTime
        public bool IsDateTime(string strDateTime)
        {
            //BEGIN - [Low Chee Sam] - 12 May 2005 - Added for DRIB P3 July release

            #region Check for valid datetime format

            DateTime dtDate;
            IFormatProvider culture = new CultureInfo("en-GB", true);


            try
            {
                if (strDateTime.Trim().Length > 0)
                {
                    dtDate = DateTime.Parse(strDateTime, culture);
                    //					return true;
                }
                else
                    return true;

            }
            catch
            {
                return false;
            }

            #endregion

            //			return UICommonFunction.IsValidFormattedDate(strDateTime);
            return UICommonFunction.IsValidFormattedDate(dtDate.ToString("dd/MM/yyyy"));

            // Don't need to check for Time format because it will generate exception 
            // at try-catch block above, if the time format is invalid

            //END - [Low Chee Sam] - 12 May 2005
        }
        #endregion


        public bool IsDateTimeTest(string strDateTimeTest)
        {
            #region Check for valid datetime format
            //DateTime dtDate;
            try
            {
                //dtDate = DateTime.Parse(strDateTimeTest);//Convert.ToDateTime(strDateTimeTest);//DateTime.Parse(strDateTimeTest, culture);
 

                string[] formats = { "dMMMyyyy", "dMMMyy", "ddMMMyyyy", "ddMMMyy", "ddMMyyyy", "ddMMyy","dMMMMyyyy","dMMMMyy","ddMMMMyyyy","ddMMMMyy","dMyyyy","dMyy",
                                     "d/M/yyyy", "d/M/yy", "dd/MM/yyyy", "dd/MM/yy", "d/MMMM/yyyy", "d/MMMM/yy","dd/MMM/yyyy","dd/MMM/yy","dd/MMMM/yyyy","dd/MMMM/yy","d/MMMM/yyyy","d/MMMM/yy",
                                     "d-M-yyyy", "d-M-yy", "dd-MM-yyyy", "dd-MM-yy", "d-MMMM-yyyy", "d-MMMM-yy","dd-MMM-yyyy","dd-MMM-yy","dd-MMMM-yyyy","dd-MMMM-yy","d-MMMM-yyyy","d-MMMM-yy",
                                     "d M yyyy", "d M yy", "dd MM yyyy", "dd MM yy", "d MMMM yyyy", "d MMMM yy","dd MMM yyyy","dd MMM yy","dd MMMM yyyy","dd MMMM yy","d MMMM yyyy","d MMMM yy", };

                var converted = DateTime.ParseExact(strDateTimeTest, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                return true;
            }
            catch //(Exception ex)
            {
                //UIException.LogException(ex, "public bool IsDateTimeTest(string strDateTimeTest)");
                return false;
            }
            #endregion
        }
        #region Check for valid datetime format
        public bool IsValidDateTime(string strDateTimeTest,out DateTime DateValue)
        {
            
            //DateTime dtDate;
            DateValue = new DateTime();
            try
            {
                DateValue = Convert.ToDateTime(strDateTimeTest);
                if (DateValue.ToString() == "1/1/0001 12:00:00 AM")
                    return false;
                return true;
            }
            catch 
            { 
                return false;
            }
            
        }
        #endregion


        /// <summary>
        /// Check if string is integer value
        /// </summary>
        /// <param name="strDateTime">String to be validated</param>
        /// <returns>True if it is integer</returns>
        #region private bool : IsInteger
        public bool IsInteger(string strInteger, string sAllowNegatif)
        {
            bool blIsInteger = false;
            Int64 iInteger = 0;

            try
            {
                iInteger = Int64.Parse(strInteger);

                if (sAllowNegatif == "0") //0 means not allow negatif
                {
                    if (iInteger >= 0) //Positif value
                        blIsInteger = true;
                }
                else
                    blIsInteger = true;
            }
            catch
            { }

            return blIsInteger;
        }
        #endregion

        /// <summary>
        /// Check if string is boolean value
        /// </summary>
        /// <param name="strBoolean">String to be validated</param>
        /// <returns>True if it is boolean</returns>
        #region private bool : IsBoolean
        public bool IsBoolean(string strBoolean)
        {
            try
            {
                Convert.ToBoolean(strBoolean);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Function to check isNumeric

        public bool IsNumeric(string theValue)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }
        #endregion

    }


}
