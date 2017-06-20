using System;
using System.IO;
using System.Net;
using System.Text;
using System.Dynamic;
//using System.Collections.Generic;
//using System.Web;
using FwCore.SqlServer.Entities;
using System.Text.RegularExpressions;

namespace FwCore.Utilities
{
    public class FwFunc
    {
        //---------------------------------------------------------------------------------------------
        public FwFunc()
        {
        }
        //---------------------------------------------------------------------------------------------
        public static string AddQuotes(string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }
        //---------------------------------------------------------------------------------------------
        public static string RequestUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentException("Parameter url cannot be null or empty.");
            
            string html = string.Empty;
            try
            {
                Encoding enc = Encoding.GetEncoding(1252);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result)
                {
                    using (StreamReader responseStream = new StreamReader(response.GetResponseStream(), enc))
                    {
                        html = responseStream.ReadToEnd();
                    }
                }
                    
                
            }
            catch
            {
                // ignore exceptions
            }
            return html;
        }
        //---------------------------------------------------------------------------------------------
        public static bool IsEmptyDate(string value)
        {
            return (value.Replace("/", "").Trim() == string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        public static string FileToString(string filename)
        {
            string str = null;
            using (StreamReader sr = File.OpenText(filename))
            {
                str = sr.ReadToEnd();
            }
            return str;
        }
        //----------------------------------------------------------------------------------
        public static void WriteLog(string message)
        {
            //string path, logmessage;

            //path       = HttpContext.Current.Server.MapPath("FwLog.txt");
            //logmessage = Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd mm:hh:ss") + " " + message;
            //File.AppendAllText(path, logmessage);
        }
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Gives you the current base url of the web applicaition, including the virtual directory if applicable with terminating /
        /// </summary>
        /// <returns></returns>
        //mv 2017-06-11 needs to be ported to .net core
        //public static string GetRequestBaseUrl()
        //{
        //    string baseurl;
        //    baseurl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        //    return baseurl;
        //}
        //----------------------------------------------------------------------------------
        public static string Space(int spaces)
        {
            string result;

            result = string.Empty.PadRight(spaces);

            return result;
        }
        //----------------------------------------------------------------------------------
        //mv 2017-06-112needs to be ported to .net core
        //public static string ToAbsoluteUrl(string relativeUrl) {
        //    string absoluteurl, port;
        //    Uri url;

        //    if (string.IsNullOrEmpty(relativeUrl))
        //    {
        //        absoluteurl = relativeUrl;
        //    }
        //    else if (HttpContext.Current == null)
        //    {
        //        absoluteurl = relativeUrl;
        //    }
        //    else if (relativeUrl.StartsWith("/"))
        //    {
        //        relativeUrl = relativeUrl.Insert(0, "~");
        //    }
        //    else if (!relativeUrl.StartsWith("~/"))
        //    {
        //        relativeUrl = relativeUrl.Insert(0, "~/");
        //    }
        //    url         = HttpContext.Current.Request.Url;
        //    port        = url.Port != 80 ? (":" + url.Port) : String.Empty;
        //    absoluteurl = String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));

        //    return absoluteurl;
        //}
        //----------------------------------------------------------------------------------
        public static dynamic CheckPasswordComplexity(FwControl controlrec, string password)
        {
            dynamic result;

            result       = new ExpandoObject();
            result.error = false;
            result.errmsg = "Password must contain the following:";

            if (controlrec.Settings.RequireMinLengthPassword)
            {
                if (password.Length < controlrec.Settings.MinLengthPassword)
                {
                    result.error = true;
                }
                result.errmsg = result.errmsg + "<br/>*At least " + controlrec.Settings.MinLengthPassword + " characters.";
            }
            if (controlrec.Settings.RequireDigitInPassword)
            {
                bool foundDigit = false;
                foreach (char c in password)
                {
                    if (c >= '0' && c <= '9')
                    {
                        foundDigit = true;
                        break;
                    }
                }
                if (!foundDigit)
                {
                    result.error = true;
                }
                result.errmsg = result.errmsg + "<br/>*Contain a digit.";
            }
            if (controlrec.Settings.RequireSymbolInPassword)
            {
                bool foundSymbol = false;
                if (Regex.Match(password, @"[`~!@#$%^&*()+\-?]", RegexOptions.ECMAScript).Success)
                {
                    foundSymbol = true;
                }
                if (!foundSymbol)
                {
                    result.error = true;
                }
                result.errmsg = result.errmsg + "<br/>*Contain a special character.";
            }

            return result;
        }
        //----------------------------------------------------------------------------------
    }
}
