using PuppeteerSharp;
using System.Threading.Tasks;

namespace FwStandard.Utilities
{
    public class FwPDF
    {
        //------------------------------------------------------------------------------------
        //const int CHROMIUM_REVISION = 579032a;
        const int CHROMIUM_REVISION = BrowserFetcher.DefaultRevision;
        //------------------------------------------------------------------------------------
        public async static Task<bool> GeneratePdfFromHtmlAsync(string htmlinput, string pdfOutputPath, PdfOptions pdfOptions)
        {
            await new BrowserFetcher().DownloadAsync(CHROMIUM_REVISION);
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                bool success = true;
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetContentAsync(htmlinput);

                    await page.PdfAsync(pdfOutputPath, pdfOptions);

                    // Clean up pdf directory, a different approach is probably needed so this isn't getting fired off by a bunch of users at the same time
                    //string pdfDirectory = Path.GetDirectoryName(pdfOutputPath);
                    //string[] filePaths = Directory.GetFiles(pdfDirectory, "*.pdf");
                    //for (int i = 0; i < filePaths.Length; i++)
                    //{
                    //    if (File.GetLastWriteTime(filePaths[i]) < DateTime.Today.AddDays(-7))
                    //    {
                    //        File.Delete(filePaths[i]);
                    //    }
                    //}
                }
                return success;
            }
        }
        //------------------------------------------------------------------------------------
    }
}