using FwCore.Api;
using FwCore.Logic;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Reporting;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static FwCore.Logic.FwJwtLogic;
using static FwStandard.Reporting.FwReport;

namespace FwCore.Utilities
{
    public static class FwReportUtil
    {
        //------------------------------------------------------------------------------------
        public static string GetApiUrl(FwApplicationConfig appConfig)
        {
            string apiUrl = appConfig.PublicBaseUrl;
            if (string.IsNullOrEmpty(apiUrl))
            {
                var serverAddressesFeature = FwProgram.Host.ServerFeatures.Get<IServerAddressesFeature>();
                foreach (string address in serverAddressesFeature.Addresses)
                {
                    apiUrl = address.Replace("0.0.0.0", "127.0.0.1");
                    break;
                }
            }
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new Exception("PublicBaseUrl must be configured in appsettings.json");
            }
            return apiUrl;
        }
        //------------------------------------------------------------------------------------
        public static async Task<string> GetAuthorizationHeaderAsync(FwApplicationConfig appConfig, string controllerId)
        {
            ServiceTokenOptions options = new ServiceTokenOptions();
            options.ControllerIds.Add(controllerId);
            string authorizationHeader = "Bearer " + await FwJwtLogic.GetServiceTokenAsync(appConfig, options);

            return authorizationHeader;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Runs a reports under a service token and returns the results as an image and/or pdf stream.
        /// </summary>
        /// <returns></returns>
        public class GetReportStreamsRequest
        {
            public FwApplicationConfig AppConfig { get; set; } = null;
            public dynamic Parameters { get; set; } = new ExpandoObject();
            public Type ReportControllerType { get; set; } = null;
            public bool UseCustomReportLayout { get; set; } = false;
            public string CustomReportLayoutId { get; set; } = string.Empty;
            public string CustomReportLayoutCategory { get; set; } = string.Empty;
            public string CustomReportLayoutDescription { get; set; } = string.Empty;
            public int ImageWidth { get; set; } = 480;
            public int ImageHeight { get; set; } = 480;
            public bool AttachPdf { get; set; } = false;
            public bool AttachImage { get; set; } = false;


            public GetReportStreamsRequest(FwApplicationConfig appConfig, Type reportControllerType)
            {
                this.AppConfig = appConfig;
                this.ReportControllerType = reportControllerType;
            }
        }
        
        public static async Task<FwReportStreams> GetReportStreams(GetReportStreamsRequest request)
        {
            using (FwSqlConnection conn = new FwSqlConnection(request.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwControllerAttribute reportControllerAttribute = request.ReportControllerType.GetCustomAttribute<FwControllerAttribute>(false);
                string apiUrl = FwReportUtil.GetApiUrl(request.AppConfig);
                string authorizationHeader = await FwReportUtil.GetAuthorizationHeaderAsync(request.AppConfig, reportControllerAttribute.Id);

                request.Parameters.isCustomReport = request.UseCustomReportLayout;
                if (request.UseCustomReportLayout)
                {

                    using (FwSqlCommand qryWebReportLayout = new FwSqlCommand(conn, request.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        qryWebReportLayout.Add("select html");
                        qryWebReportLayout.Add("from webreportlayout with(nolock)");
                        qryWebReportLayout.Add("where basereport = 'VisitorPassReport'");
                        if (!string.IsNullOrEmpty(request.CustomReportLayoutId))
                        {
                            qryWebReportLayout.Add("  and webreportlayoutid = @webreportlayoutid");
                            qryWebReportLayout.AddParameter("@webreportlayoutid", request.CustomReportLayoutId);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(request.CustomReportLayoutCategory))
                                throw new ArgumentException("customreportlayoutcategory attribute is required on <ReportEmail> element in VISITOR PASS CONFIRMATION email template");
                            if (string.IsNullOrEmpty(request.CustomReportLayoutDescription))
                                throw new ArgumentException("customreportlayoutdescription attribute is required on <ReportEmail> element in VISITOR PASS CONFIRMATION email template");
                            qryWebReportLayout.Add("  and category = @category");
                            qryWebReportLayout.Add("  and description = @description");
                            qryWebReportLayout.AddParameter("@category", request.CustomReportLayoutCategory);
                            qryWebReportLayout.AddParameter("@description", request.CustomReportLayoutDescription);
                        }
                        await qryWebReportLayout.ExecuteAsync();
                        string html = qryWebReportLayout.GetField("html").ToString();
                        request.Parameters.ReportTemplate = html;
                        if (string.IsNullOrEmpty(html))
                        {
                            if (!string.IsNullOrEmpty(request.CustomReportLayoutId))
                            {
                                throw new Exception("no customreportlayout has been configured for customreportlayoutid: " + request.CustomReportLayoutId);
                            }
                            else
                            {
                                throw new Exception($"no customreportlayout has been configured for customreportlayoutcategory=\"{request.CustomReportLayoutCategory}\" customerreportlayoutdescription=\"{request.CustomReportLayoutDescription}\"");
                            }
                        }
                    }
                }
                //request.Parameters.companyName = await FwSqlCommand.GetStringDataAsync(conn, request.AppConfig.DatabaseSettings.QueryTimeout, "control", "controlid", "1", "company");

                string reportName = request.ReportControllerType.Name.ToLower();
                if (reportName.EndsWith("controller"))
                {
                    reportName = reportName.Substring(0, reportName.Length - "controller".Length);
                }

                ViewPortOptions viewPortOptions = new ViewPortOptions();
                viewPortOptions.Width = request.ImageWidth;
                viewPortOptions.Height = request.ImageHeight;
                ScreenshotOptions screenshotOptions = new ScreenshotOptions();
                screenshotOptions.Type = ScreenshotType.Png;
                PdfOptions pdfOptions = new PdfOptions();
                pdfOptions.Width = "8.5in";
                pdfOptions.Height = "11in";
                pdfOptions.Scale = 1;

                FwReportStreams streams = await FwReport.GeneratePdfAndImageStreamsFromUrlAsync(request.AttachImage, request.AttachPdf, apiUrl, apiUrl + $"/reports/{reportName}/index.html", authorizationHeader, request.Parameters, viewPortOptions, screenshotOptions, pdfOptions);
                return streams;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
