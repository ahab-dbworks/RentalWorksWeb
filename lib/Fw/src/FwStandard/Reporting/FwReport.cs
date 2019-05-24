using FwStandard.Models;
using FwStandard.SqlServer;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.Reporting
{
    public class FwReport
    {
        //const int CHROMIUM_REVISION = 565530;
        const int CHROMIUM_REVISION = BrowserFetcher.DefaultRevision;

        public async static Task<string> GeneratePdfFromUrlAsync(string apiUrl, string reportUrl, string pdfOutputPath, string authorizationHeader, dynamic parameters, PdfOptions pdfOptions)
        {
            await new BrowserFetcher().DownloadAsync(CHROMIUM_REVISION);
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                StringBuilder jsConsole = new StringBuilder();
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetViewportAsync(new ViewPortOptions() { /*Width = 816, Height = 1056*/ });
                    NavigationOptions navOptions = new NavigationOptions();
                    navOptions.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Networkidle0 };
                    await page.GoToAsync(reportUrl, navOptions);
                    page.Console += delegate(object sender, ConsoleEventArgs e)
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

        public async static Task EmailPdfAsync(string fromusersid, string uniqueid, string title, string from, string to, string cc, string subject, string body, string pdfPath, FwApplicationConfig appConfig)
        {
            var message = new MailMessage(from, to, subject, body);
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
                    accountname     = qry.GetField("accountname").ToString().TrimEnd();
                    accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                    authtype        = qry.GetField("authtype").ToString().TrimEnd();
                    host            = qry.GetField("host").ToString().TrimEnd();
                    port            = qry.GetField("port").ToInt32();
                }
            }
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(accountname, accountpassword, domain);
            await client.SendMailAsync(message);
        }
    }
}
