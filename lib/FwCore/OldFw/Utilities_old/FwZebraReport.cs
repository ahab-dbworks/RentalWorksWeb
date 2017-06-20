//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Web;
//using FwCore.SqlServer;

namespace FwCore.Utilities
{
    public class FwZebraReport
    {
        StringBuilder report;
        //---------------------------------------------------------------------------------
        public FwZebraReport(string reporttext)
        {
            this.report = new StringBuilder(reporttext);
        }
        //---------------------------------------------------------------------------------
        public void ReplaceField(string fieldName, string fieldValue)
        {
            Regex regex = new Regex(@"\[" + fieldName + @"(:(?<length>\d+))*\]");
            MatchCollection matches = regex.Matches(report.ToString());
            foreach (Match match in matches)
            {
                if (match.Groups["length"].Success)
                {
                    int length = int.Parse(match.Groups["length"].Value);
                    if (length > fieldValue.Length)
                    {
                        length = fieldValue.Length;    
                    }
                    //report.Replace(match.Value, fieldValue.Substring(0, length));
                    report.Remove(match.Index, match.Length).Insert(match.Index, fieldValue.Substring(0, length));
                }
                else 
                {
                    //report.Replace(match.Value, fieldValue);
                    report.Remove(match.Index, match.Length).Insert(match.Index, fieldValue);
                }
            }
        }
        //---------------------------------------------------------------------------------
        public override string ToString()
        {
            return report.ToString();
        }
        //---------------------------------------------------------------------------------
        //no DataTable in .net core
        //public static string RenderReport(string template, DataTable dt)
        //{
        //    string result;
        //    FwZebraReport report;

        //    result = string.Empty;
        //    if (dt.Rows.Count > 0)
        //    {
        //        report = new FwZebraReport(template);
        //        foreach(DataColumn column in dt.Columns)
        //        {
        //            string fieldName, fieldValue;
                    
        //            fieldName  = column.ColumnName.ToUpper();
        //            fieldValue = new FwDatabaseField(dt.Rows[0][column.Ordinal]).ToString().Trim();
        //            report.ReplaceField(fieldName, fieldValue);
        //        }
        //        result = report.ToString() + "\r\n";
        //    }

        //    return result;
        //}
        //---------------------------------------------------------------------------------
    }
}
