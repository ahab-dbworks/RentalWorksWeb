using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System.IO;
using FwCore.Controllers;
using System;
using System.Diagnostics;
using FwStandard.Reporting;
using System.Dynamic;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace WebApi.Modules.Reports.ContractReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class OutContractReportController : AppReportController
    {
        public OutContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        public class ContractReportResponse
        {
            public string htmlReportDownloadUrl { get; set; } = string.Empty;
            public string pdfReportDownloadUrl { get; set; } = string.Empty;
        }

        [HttpGet("{contractid}")]
        public async Task<IActionResult> GetContract([FromRoute] string contractid)
        {
            OutContractReportRepository contractReportRepo = new OutContractReportRepository(this.AppConfig, this.UserSession);
            var contractReport = await contractReportRepo.Get(contractid);
            return new OkObjectResult(contractReport);
        }

        // POST api/v1/contractreport/htmlreport/{contractid}
        [HttpPost("emailpdf/{contractid}")]
        public async Task<IActionResult> EmailPdfAsync([FromRoute] string contractid, [FromBody] EmailPdfRequest request)
        {
            string baseUrl = this.GetFullyQualifiedBaseUrl();
            string guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
            string baseFileName = $"OutContractReport_{this.UserSession.WebUsersId}_{guid}";
            string htmlFileName = $"{baseFileName}.html";
            string pathHtmlReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), htmlFileName);
            
            ContractReportResponse response = new ContractReportResponse();
            response.htmlReportDownloadUrl = $"{baseUrl}/temp/downloads/{htmlFileName}";

            Uri uriHtmlReport = new Uri(pathHtmlReport);
            string pdfFileName = $"{baseFileName}.pdf";
            string pathPdfReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), pdfFileName);
            response.pdfReportDownloadUrl = $"{baseUrl}/temp/downloads/{pdfFileName}";

            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            dynamic parameters = new ExpandoObject();
            parameters.contractid = contractid;
            PdfOptions pdfOptions = new PdfOptions();
            pdfOptions.DisplayHeaderFooter = true;
            await FwReport.GeneratePdfFromUrlAsync($"{baseUrl}/Reports/OutContractReport/index.html", pathPdfReport, authorizationHeader, parameters, pdfOptions);
            if (System.IO.File.Exists(pathPdfReport))
            {
                await FwReport.EmailPdfAsync(
                    fromusersid: this.UserSession.UsersId,
                    uniqueid: contractid,
                    title: "Check-Out Contract", 
                    from: request.from, 
                    to: request.to, 
                    cc: request.cc,
                    subject: request.subject,
                    body: request.body, 
                    pdfPath: pathPdfReport, 
                    appConfig: this.AppConfig);
            }

            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
    }

    public class ContractReportRequest
    {
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
    }
}