using FwStandard.Models;
using FwStandard.SqlServer;
using PuppeteerSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.Reporting
{
    public class FwReport
    {
        //------------------------------------------------------------------------------------ 
        //const int CHROMIUM_REVISION = 579032a;
        const int CHROMIUM_REVISION = BrowserFetcher.DefaultRevision;
        //------------------------------------------------------------------------------------ 
        public async static Task<string> GeneratePdfFromUrlAsync(string apiUrl, string reportUrl, string pdfOutputPath, string authorizationHeader, dynamic parameters, PdfOptions pdfOptions)
        {
            await new BrowserFetcher().DownloadAsync(CHROMIUM_REVISION);
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                StringBuilder jsConsole = new StringBuilder();
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetViewportAsync(new ViewPortOptions());
                    NavigationOptions navOptions = new NavigationOptions();
                    navOptions.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Networkidle0 };
                    await page.GoToAsync(reportUrl, navOptions);
                    page.Console += delegate (object sender, ConsoleEventArgs e)
                    {
                        if (e.Message.Type == ConsoleType.Error)
                        {
                            jsConsole.AppendLine(e.Message.Text);
                        }
                    };
                    var renderReportResponse = await page.EvaluateFunctionAsync("(apiUrl, authorizationHeader, parameters) => (report.renderReport(apiUrl, authorizationHeader, parameters))", apiUrl, authorizationHeader, parameters);
                    WaitForFunctionOptions waitOptions = new WaitForFunctionOptions();
                    waitOptions.Timeout = 3600000; // wait for up to an hour for renderReportCompleted to be set to true
                    await page.WaitForFunctionAsync("() => (report.renderReportCompleted === true)", waitOptions);

                    StringBuilder script = new StringBuilder();
                    script.AppendLine("const elOutputFormat = document.querySelector('[data-outputformat]');");
                    script.AppendLine("if (elOutputFormat !== null) {");
                    script.AppendLine("  elOutputFormat.setAttribute('data-outputformat', 'pdf');");
                    script.AppendLine("}");
                    await page.EvaluateExpressionAsync(script.ToString());

                    pdfOptions.PrintBackground = true;
                    pdfOptions.HeaderTemplate = await page.EvaluateExpressionAsync<string>("report.headerHtml");
                    pdfOptions.FooterTemplate = await page.EvaluateExpressionAsync<string>("report.footerHtml");
                    pdfOptions.HeaderTemplate = (pdfOptions.HeaderTemplate == null) ? string.Empty : pdfOptions.HeaderTemplate;
                    pdfOptions.FooterTemplate = (pdfOptions.FooterTemplate == null) ? string.Empty : pdfOptions.FooterTemplate;

                    await page.PdfAsync(pdfOutputPath, pdfOptions);

                    // Clean up pdf directory, a different approach is probably needed so this isn't getting fired off by a bunch of users at the same time
                    string pdfDirectory = Path.GetDirectoryName(pdfOutputPath);
                    string[] filePaths = Directory.GetFiles(pdfDirectory, "*.pdf");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        if (File.GetLastWriteTime(filePaths[i]) < DateTime.Today.AddDays(-7))
                        {
                            File.Delete(filePaths[i]);
                        }
                    }
                }
                return jsConsole.ToString();
            }
        }
        //------------------------------------------------------------------------------------ 
        public class FwReportStreams
        {
            public Stream ImageStream { get; set; } = null;
            public Stream PdfStream { get; set; } = null;
        }
        public async static Task<FwReportStreams> GeneratePdfAndImageStreamsFromUrlAsync(bool renderImageStream, bool renderPdfStream, string apiUrl, string reportUrl, string authorizationHeader, dynamic parameters, ViewPortOptions viewPortOptions, ScreenshotOptions screenshotOptions, PdfOptions pdfOptions)
        {
            await new BrowserFetcher().DownloadAsync(CHROMIUM_REVISION);
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetViewportAsync(viewPortOptions);
                    NavigationOptions navOptions = new NavigationOptions();
                    navOptions.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Networkidle0 };
                    await page.GoToAsync(reportUrl, navOptions);
                    //page.Console += delegate (object sender, ConsoleEventArgs e)
                    //{
                    //    if (e.Message.Type == ConsoleType.Error)
                    //    {
                    //        jsConsole.AppendLine(e.Message.Text);
                    //    }
                    //};
                    var renderReportResponse = await page.EvaluateFunctionAsync("(apiUrl, authorizationHeader, parameters) => (report.renderReport(apiUrl, authorizationHeader, parameters))", apiUrl, authorizationHeader, parameters);
                    WaitForFunctionOptions waitOptions = new WaitForFunctionOptions();
                    waitOptions.Timeout = 3600000; // wait for up to an hour for renderReportCompleted to be set to true
                    await page.WaitForFunctionAsync("() => (report.renderReportCompleted === true)", waitOptions);
                    FwReportStreams streams = new FwReportStreams();
                    StringBuilder script = new StringBuilder();
                    script.AppendLine("const elOutputFormat = document.querySelector('[data-outputformat]');");
                    await page.EvaluateExpressionAsync(script.ToString());
                    if (renderImageStream)
                    {
                        script = new StringBuilder();
                        script.AppendLine("if (elOutputFormat !== null) {");
                        script.AppendLine("  elOutputFormat.setAttribute('data-outputformat', 'png');");
                        script.AppendLine("}");
                        await page.EvaluateExpressionAsync(script.ToString());
                        string imageDataUrl = await page.ScreenshotBase64Async(screenshotOptions);
                        streams.ImageStream = await page.ScreenshotStreamAsync(screenshotOptions);
                    }
                    if (renderPdfStream)
                    {
                        script = new StringBuilder();
                        script.AppendLine("if (elOutputFormat !== null) {");
                        script.AppendLine("  elOutputFormat.setAttribute('data-outputformat', 'pdf');");
                        script.AppendLine("}");
                        await page.EvaluateExpressionAsync(script.ToString());
                        pdfOptions.PrintBackground = true;
                        streams.PdfStream = await page.PdfStreamAsync(pdfOptions);
                    }
                    return streams;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public async static Task EmailImageAsync(string apiUrl, string reportUrl, string authorizationHeader, dynamic parameters,
            string fromusersid, string uniqueid, string title, string from, string to, string cc, string subject,
            string bodyHeader, string bodyFooter, FwApplicationConfig appConfig, ViewPortOptions viewPortOptions, ScreenshotOptions screenshotOptions)
        {
            string body = $"<div class=\"body-header\">{bodyHeader}</div><div class=\"body\"><img src=\"cid:reportImage1\" width=\"{viewPortOptions.Width}\" height=\"{viewPortOptions.Height}\" /></div><div class=\"body-footer\">{bodyFooter}</div>";

            var message = new MailMessage();
            message.From = new System.Net.Mail.MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            FwReportStreams streams = await FwReport.GeneratePdfAndImageStreamsFromUrlAsync(true, false, apiUrl, reportUrl, authorizationHeader, parameters, viewPortOptions, screenshotOptions, null);
            Attachment imageAttachment = new Attachment(streams.ImageStream, "image/png");
            imageAttachment.ContentId = "reportImage1";
            message.Attachments.Add(imageAttachment);

            string accountname = string.Empty, accountpassword = string.Empty, authtype = string.Empty, host = string.Empty, domain = "";
            int port = 25;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from emailreportcontrol with (nolock)");
                    await qry.ExecuteAsync();
                    accountname = qry.GetField("accountname").ToString().TrimEnd();
                    accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                    authtype = qry.GetField("authtype").ToString().TrimEnd();
                    host = qry.GetField("host").ToString().TrimEnd();
                    port = qry.GetField("port").ToInt32();
                }
            }
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(accountname, accountpassword, domain);
            await client.SendMailAsync(message);
        }
        //------------------------------------------------------------------------------------ 
        public async static Task EmailPdfAsync(string fromusersid, string uniqueid, string title, string from, string to, string cc, string subject, string body, string pdfPath, FwApplicationConfig appConfig)
        {
            to = to.Replace(';', ',');
            cc = cc.Replace(';', ',');
            var message = new MailMessage(from, to, subject, body);
            //message.CC.Add(cc);
            if (!string.IsNullOrEmpty(cc))
            {
                message.CC.Add(cc);
            }
            message.IsBodyHtml = true;
            message.Attachments.Add(new Attachment(pdfPath, "application/pdf"));
            string accountname = string.Empty, accountpassword = string.Empty, authtype = string.Empty, host = string.Empty, domain = "";
            int port = 25;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string emailreportid = await FwSqlData.GetNextIdAsync(conn, appConfig.DatabaseSettings);
                byte[] pdfattachment = File.ReadAllBytes(pdfPath);
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@emailreportid", emailreportid);
                    qry.AddParameter("@reportdefid", string.Empty);
                    qry.AddParameter("@fromusersid", fromusersid);
                    qry.AddParameter("@createdate", DateTime.Now);
                    qry.AddParameter("@status", "OPEN");
                    qry.AddParameter("@emailtext", body);
                    //qry.AddParameter("@datestamp", DateTime.Now);
                    //qry.AddParameter("@rowguid", );
                    qry.AddParameter("@emailto", to);
                    qry.AddParameter("@subject", subject);
                    qry.AddParameter("@emailcc", cc);
                    qry.AddParameter("@title", title);
                    qry.AddParameter("@uniqueid", uniqueid);
                    qry.AddParameter("@pdfattachment", pdfattachment);
                    await qry.ExecuteInsertQueryAsync("emailreport");
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from emailreportcontrol with (nolock)");
                    await qry.ExecuteAsync();
                    accountname = qry.GetField("accountname").ToString().TrimEnd();
                    accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                    authtype = qry.GetField("authtype").ToString().TrimEnd();
                    host = qry.GetField("host").ToString().TrimEnd();
                    port = qry.GetField("port").ToInt32();
                }
            }
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(accountname, accountpassword, domain);
            await client.SendMailAsync(message);
        }
        //------------------------------------------------------------------------------------ 
    }
}
