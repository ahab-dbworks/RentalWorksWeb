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
        const int CHROMIUM_REVISION = 565530;

        public async static Task GeneratePdfFromStringAsync(string htmlHeaderTemplate, string htmlBodyTemplate, string htmlFooterTemplate, string pdfOutputPath)
        {
            //var downloadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "temp" + Path.DirectorySeparatorChar + "");
            //var downloader = new Downloader(downloadsFolder);
            await Downloader.CreateDefault().DownloadRevisionAsync(CHROMIUM_REVISION);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
            }, CHROMIUM_REVISION);
            try
            {
                var page = await browser.NewPageAsync();
                await page.SetContentAsync(htmlBodyTemplate);
                PdfOptions pdfOptions = new PdfOptions();
                pdfOptions.DisplayHeaderFooter = true;
                pdfOptions.HeaderTemplate = htmlHeaderTemplate;
                pdfOptions.FooterTemplate = htmlFooterTemplate;
                pdfOptions.Format = PaperFormat.Letter;
                pdfOptions.Landscape = false;
                pdfOptions.MarginOptions.Top = "96px";
                pdfOptions.MarginOptions.Right = "24px";
                pdfOptions.MarginOptions.Bottom = "96px";
                pdfOptions.MarginOptions.Left = "24px";
                await page.PdfAsync(pdfOutputPath, pdfOptions);
            }
            finally
            {
                browser.Dispose();
            }
        }

        public async static Task GeneratePdfFromUrlAsync(string reportUrl, string pdfOutputPath, string apiToken, string baseUrl, dynamic parameters)
        {
            //var downloadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "temp" + Path.DirectorySeparatorChar + "");
            //var downloader = new Downloader(downloadsFolder);
            await Downloader.CreateDefault().DownloadRevisionAsync(CHROMIUM_REVISION);
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }, CHROMIUM_REVISION))
            {
                using (var page = await browser.NewPageAsync())
                {
                    page.Console += Page_Console;
                    await page.SetViewportAsync(new ViewPortOptions() { Width = 816, Height = 1056 });
                    NavigationOptions navOptions = new NavigationOptions();
                    navOptions.WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Networkidle0 };
                    navOptions.Timeout = 3600; // 1 hour
                    await page.GoToAsync(baseUrl + reportUrl, navOptions);
                    //var renderReportResponse = await page.EvaluateExpressionAsync<FwRenderReportResponse>($"report.renderReport('{apiToken}', '{baseUrl}', '{primaryKey}')"); 
                    var renderReportResponse = await page.EvaluateFunctionAsync("(apiToken, baseUrl, parameters) => (report.renderReport(apiToken, baseUrl, parameters))", apiToken, baseUrl, parameters);
                    PdfOptions pdfOptions = new PdfOptions();
                    pdfOptions.DisplayHeaderFooter = true;
                    pdfOptions.Format = PaperFormat.Letter;
                    pdfOptions.Landscape = false;
                    pdfOptions.MarginOptions.Top = "96px";
                    pdfOptions.MarginOptions.Right = "24px";
                    pdfOptions.MarginOptions.Bottom = "96px";
                    pdfOptions.MarginOptions.Left = "24px";
                    await page.WaitForFunctionAsync("() => (report.renderReportCompleted === true)");
                    pdfOptions.HeaderTemplate = await page.EvaluateExpressionAsync<string>("report.headerHtml");
                    pdfOptions.FooterTemplate = await page.EvaluateExpressionAsync<string>("report.footerHtml");
                    //var pageHtml = page.GetContentAsync();
                    //SaveHtmlToDB(pageHtml);

                    await page.PdfAsync(pdfOutputPath, pdfOptions);
                }
            }
        }

        private static void Page_Console(object sender, ConsoleEventArgs e)
        {
            var message = e.Message;
        }

        public async static Task EmailPdfAsync(string from, string to, string subject, string body, string pdfPath)
        {
            var message = new MailMessage(from, to, subject, body);
            message.IsBodyHtml = true;
            //message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay | DeliveryNotificationOptions.OnSuccess;
            message.Attachments.Add(new Attachment(pdfPath, "application/pdf"));
            var host = "ftp01.dbworkscloud.com";
            var port = 25;
            string username = "";
            string password = "";
            string domain = "";
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(username, password, domain);
            //client.EnableSsl = true;
            await client.SendMailAsync(message);
        }
    }
}
