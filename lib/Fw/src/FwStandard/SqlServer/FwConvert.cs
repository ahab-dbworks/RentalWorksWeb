﻿using FwStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FwStandard.SqlServer
{
    public static class FwConvert
    {
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Convert a string or DateTime object into a date string in the format: yyyy-MM-dd.
        /// </summary>
        public static string ToShortDate(object value)
        {
            string str = string.Empty;
            DateTime dateTime;
            if (value is DateTime)
            {
                dateTime = (DateTime)value;
                str = dateTime.ToString("yyyy-MM-dd");
            }
            else if ((value is String) && (!string.IsNullOrEmpty((string)value)))
            {
                dateTime = DateTime.Parse((string)value);
                str = dateTime.ToString("yyyy-MM-dd");
            }
            return str;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Convert a string or DateTime object to a time string in the format: HH:mm:ss.
        /// </summary>
        public static string ToShortTime(object value)
        {
            string str = string.Empty;
            DateTime dateTime;
            if (value is DateTime)
            {
                dateTime = (DateTime)value;
                str = dateTime.ToString("HH:mm:ss");
            }
            else if ((value is String) && (!string.IsNullOrEmpty((string)value)))
            {
                dateTime = DateTime.Parse((string)value);
                str = dateTime.ToString("HH:mm:ss");
            }
            return str;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Convert a date string into a DateTime.
        /// </summary>
        public static DateTime ToDateTime(string date)
        {
            DateTime result = DateTime.MinValue;
            if (!DateTime.TryParse(date, out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Combine a strings for date and time into a DateTime.
        /// </summary>
        public static DateTime ToDateTime(string date, string time)
        {
            DateTime result = DateTime.MinValue;
            if (!DateTime.TryParse(date + " " + time, out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// (Created for the FwScheduler control.)
        /// It puts the Z on the end of the 8601 string specifying that it's in UTC time.
        /// </summary>
        /// <param name="value">value must be UTC, so call ToUniveralTime() on it before passing it in if you need to.</param>
        /// <returns></returns>
        public static string ToUtcIso8601DateTime(DateTime value)
        {
            string datestr = string.Empty;
            if (value != DateTime.MinValue)
            {
                datestr = value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return datestr;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public static string ToString(int value)
        {
            string result = "0";
            if (!value.ToString().Equals(string.Empty))
            {
                result = value.ToString().Trim();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public static string ToString(double value)
        {
            string result = "0.0";
            if (!value.ToString().Equals(string.Empty))
            {
                result = value.ToString().Trim();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static Int32 ToInt32(object value)
        {
            if ((value == null) || (value.ToString().Equals("")))
            {
                value = "0";
            }
            return Convert.ToInt32(value);
        }
        //---------------------------------------------------------------------------------------------
        public static DateTime GetSQLMinDate(DateTime date)
        {
            DateTime result = date;
            if (date == DateTime.MinValue)
            {
                result = ToDateTime("01/01/1754");
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string LogicalToCharacter(bool value)
        {
            return (value ? "T" : "F");
        }
        //---------------------------------------------------------------------------------------------
        public static string ToUpperCamelCase(string value)
        {
            return (value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower());
        }
        //---------------------------------------------------------------------------------------------
        public static string ToFormattedPhoneString(string phone)
        {
            StringBuilder formattedPhone = new StringBuilder();
            if ((phone.Length != 14) || (phone[0] != '(') || (phone[4] != ')') || (phone[5] != ' ') || (phone[9] != '-'))
            {
                int j = phone.Length - 1;
                for (int i = 0; i < 14; i++)
                {
                    if (i == 4)
                    {
                        formattedPhone.Insert(0, "-");
                    }
                    else if (i == 8)
                    {
                        formattedPhone.Insert(0, " ");
                    }
                    else if (i == 9)
                    {
                        formattedPhone.Insert(0, ")");
                    }
                    else if (i == 13)
                    {
                        formattedPhone.Insert(0, "(");
                    }
                    else
                    {
                        if (j >= 0)
                        {
                            formattedPhone.Insert(0, phone[j]);
                            j--;
                        }
                        else
                        {
                            formattedPhone.Insert(0, " ");
                        }
                    }
                }
            }
            else
            {
                formattedPhone.Append(phone);
            }
            return formattedPhone.ToString();
        }
        //---------------------------------------------------------------------------------------------
        public static string ToFormattedZipCodeString(string zipcode)
        {
            StringBuilder formattedZipCode = new StringBuilder();
            if ((zipcode.Length != 10) || (zipcode[5] != '-'))
            {
                int j = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (i == 5)
                    {
                        formattedZipCode.Append("-");
                    }
                    else
                    {
                        if (j < zipcode.Length)
                        {
                            formattedZipCode.Append(zipcode[j]);
                            j++;
                        }
                        else
                        {
                            formattedZipCode.Append(" ");
                        }
                    }
                }
            }
            else
            {
                formattedZipCode.Append(zipcode);
            }
            return formattedZipCode.ToString();
        }
        //---------------------------------------------------------------------------------------------
        public static bool ToBoolean(string value)
        {
            string cleanedUpValue = value.ToUpper().Trim();
            bool isTrue = cleanedUpValue.Equals("T") || cleanedUpValue.Equals("TRUE") || cleanedUpValue.Equals("Y");

            return isTrue;
        }
        //---------------------------------------------------------------------------------------------
        public static Decimal ToDecimal(object value)
        {
            Decimal dec;

            //jh 08/28/2018
            if (value is string)
            {
                value = value.ToString().Replace("(", "-").Replace(")", string.Empty);
            }

            if ((value == null) || (value.ToString().Trim() == string.Empty))
            {
                dec = 0.0m;
            }
            else
            {
                dec = (Decimal)Convert.ToDecimal(value);
            }
            return dec;
        }
        //---------------------------------------------------------------------------------------------
        public static float ToFloat(object value)
        {
            float result;

            if ((value == null) || (value.ToString().Trim() == string.Empty))
            {
                result = 0.0f;
            }
            else
            {
                result = (float)Convert.ToSingle(value);
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static Double ToDouble(object value)
        {
            Double dou;

            if ((value == null) || (value.ToString().Trim() == string.Empty))
            {
                dou = 0.0d;
            }
            else
            {
                dou = (Double)Convert.ToDouble(value);
            }
            return dou;
        }
        //---------------------------------------------------------------------------------------------        
        public static string ToJsonArray(string array)
        {
            StringBuilder json = new StringBuilder();
            bool isFirst = true;
            json.Append("[");
            foreach (string field in new List<string>(array.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    json.Append(",");
                }
                json.AppendFormat("'{0}'", field);
            }
            json.Append("]");
            return json.ToString();
        }
        //---------------------------------------------------------------------------------------------
        //public static string ToUpper(string text, bool allowOverride)
        //{
        //    string result;
        //    if (allowOverride)
        //    {
        //        result = FwApplicationOptions.Current.HasMixedCase.Enabled ? text : text.ToUpper();
        //    }
        //    else
        //    {
        //        result = text.ToUpper();
        //    }
        //    return result; 
        //}
        //---------------------------------------------------------------------------------------------
        //public static string ToLower(string text, bool allowOverride)
        //{
        //    string result;
        //    if (allowOverride)
        //    {
        //        result = FwApplicationOptions.Current.HasMixedCase.Enabled ? text : text.ToLower();
        //    }
        //    else
        //    {
        //        result = text.ToLower();
        //    }
        //    return result;
        //}
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public static string ToString(bool value)
        {
            return (value ? "T" : "F");
        }
        //---------------------------------------------------------------------------------------------
        public static string HtmlEncodeString(string str)
        {
            str = str.Replace("\r", "&#13;");
            str = str.Replace("\n", "&#10;");
            str = str.Replace("'", "&#39;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }
        //---------------------------------------------------------------------------------------------
        public static string JavaScriptEncodeString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            str = str.Replace("\r", "\\r");
            str = str.Replace("\n", "\\n");
            return str;
        }
        //---------------------------------------------------------------------------------------------
        //public static string ToMimeType(string extension)
        //{
        //    string mimeType = "application/unknown";
        //    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("." + extension.ToLower());
        //    if (regKey != null && regKey.GetValue("Content Type") != null)
        //    {
        //        mimeType = regKey.GetValue("Content Type").ToString();
        //    }
        //    return mimeType;
        //}
        //---------------------------------------------------------------------------------------------
        public static List<string> ToStringList(object[] items)
        {
            List<string> result;

            result = new List<string>();
            for (int i = 0; i < items.Length; i++)
            {
                result.Add(items[i].ToString());
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string ToSqlParameter(List<string> items)
        {
            StringBuilder sb;
            string result;

            sb = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(items[i].Replace("'", "''"));
            }
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static Guid ToGuid(string str)
        {
            Guid guid;

            guid = new Guid(str);

            return guid;
        }
        //---------------------------------------------------------------------------------------------
        public static string StripNonAlphaNumericCharacters(string str)
        {
            char[] arr;

            arr = str.ToCharArray();
            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c))));
            str = new string(arr);

            return str;
        }
        //---------------------------------------------------------------------------------------------
        public static string OleColorToHtmlColor(int oleColor)
        {
            string htmlColor = FwColorTranslator.OleColorToHtmlColor(oleColor);
            return htmlColor;
        }
        //---------------------------------------------------------------------------------------------
        public static string OleColorToHtmlColor(int oleColor, double opacity)
        {
            string htmlColor = FwColorTranslator.OleColorToHtmlColor(oleColor, opacity);
            return htmlColor;
        }
        //---------------------------------------------------------------------------------------------
        public static int HtmlColorToOleColor(string htmlColor)
        {
            int oleColor = FwColorTranslator.HtmlColorToOleColor(htmlColor);
            return oleColor;
        }
        //---------------------------------------------------------------------------------------------
        public static string ToCurrencyString(decimal amount) // $1,000,000.34
        {
            return String.Format("{0:C}", amount);
        }
        //---------------------------------------------------------------------------------------------
        public static string ToCurrencyStringNoDollarSign(decimal amount) // 1,000,000.34
        {
            return ToCurrencyString(amount).Replace("$", String.Empty);
        }
        //---------------------------------------------------------------------------------------------
        public static string ToCurrencyStringNoDollarSignNoDecimalPlaces(decimal amount) // 1,000,000.34
        {
            string val;
            decimal roundedAmt;
            int intAmt;

            roundedAmt = Math.Round(amount);
            intAmt = Convert.ToInt32(roundedAmt);
            val = intAmt.ToString();
            if (intAmt < 0)
            {
                val = "(" + val.Replace("-", "") + ")";
            }

            return val;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// This works on columns that are stored without a phone format, eg it takes in 1111111111 and returns (111) 111-1111
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPhoneUS(string value)
        {
            StringBuilder sb;
            string phone;

            if (string.IsNullOrWhiteSpace(value))
            {
                phone = String.Empty;
            }
            else
            {
                if (value.Length > 10) value = value.Substring(0, 10);
                if (value.Length < 10) value = value.PadLeft(11, ' ');
                sb = new StringBuilder();
                for (int charno = 1; charno <= 10; charno++)
                {
                    switch (charno)
                    {
                        case 1: sb.Append("("); break;
                        case 4: sb.Append(") "); break;
                        case 7: sb.Append("-"); break;
                    }
                    sb.Append(value[charno - 1]);
                }
                phone = sb.ToString();
            }

            return phone;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// This works on columns that are stored without a phone format, eg it takes in 915217790 and returns 91521-7790
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToZipcodeUS(string value)
        {
            StringBuilder sb;
            string zip;
            bool breakout;

            if (value.Length > 10) value = value.Substring(0, 10);
            if (value.Length < 10) value = value.PadRight(10, ' ');
            sb = new StringBuilder();
            breakout = false;
            for (int charno = 1; charno <= 10; charno++)
            {
                switch (charno)
                {
                    case 6:
                        if ((value[6] != ' ') || (value[7] != ' ') || (value[8] != ' ') || (value[9] != ' '))
                        {
                            sb.Append("-");
                        }
                        else
                        {
                            breakout = true;
                        }
                        break;
                }
                if (breakout) break;
                sb.Append(value[charno - 1]);
            }
            zip = sb.ToString();

            return zip;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a text string to title case.  For example, "we like to program" becomes "We Like To Program".
        /// "MiXed CasE" values are converted as "Mixed Case"
        /// "lower case" values are converted as "Lower Case"
        /// ALL CAPS values are not converted
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
        //---------------------------------------------------------------------------------------------
    }
}
