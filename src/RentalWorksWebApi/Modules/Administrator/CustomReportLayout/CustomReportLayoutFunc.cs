using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.CustomReportLayout
{
    public static class CustomReportLayoutFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public class CustomReportLayoutResponse
        {
            public string ReportTemplate { get; set; } = "";
        }
        //-------------------------------------------------------------------------------------------------------
        public static CustomReportLayoutResponse GetReportTemplate(string report)
        {
            CustomReportLayoutResponse response = new CustomReportLayoutResponse();
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] dirs = Directory.GetDirectories(currentDirectory + "\\WebpackReports\\src\\Reports", report, SearchOption.AllDirectories);
            string filePath = Path.Combine(dirs[0], "hbReport.hbs");
            response.ReportTemplate = File.ReadAllText(filePath);

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
