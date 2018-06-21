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

namespace WebApi.Modules.Reports.ContractReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class ContractReportController : AppReportController
    {
        public ContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        public class ContractReportResponse
        {
            public string htmlReportDownloadUrl { get; set; } = string.Empty;
            public string pdfReportDownloadUrl { get; set; } = string.Empty;
        }
        
        // POST api/v1/contractreport/htmlreport/{contractid}
        [HttpPost("emailpdf/{contractid}")]
        public async Task<IActionResult> EmailPdfAsync([FromRoute] string contractid)
        {
            string baseUrl = "http://localhost:57949";
            string guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
            string baseFileName = $"ContractReport_{this.UserSession.WebUsersId}_{guid}";
            string htmlFileName = $"{baseFileName}.html";
            string pathHtmlReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), htmlFileName);
            
            ContractReportResponse response = new ContractReportResponse();
            //response.htmlReportDownloadUrl = $"api/v1/download/{fileName}?downloadasfilename=ContractReport_{contractid}_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.html";
            response.htmlReportDownloadUrl = $"{baseUrl}/temp/downloads/{htmlFileName}";

            Uri uriHtmlReport = new Uri(pathHtmlReport);
            string pdfFileName = $"{baseFileName}.pdf";
            string pathPdfReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), pdfFileName);
            response.pdfReportDownloadUrl = $"{baseUrl}/temp/downloads/{pdfFileName}";

            string apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJtaWtlLnVzZXIiLCJqdGkiOiIzZmNlNTc2MC0xNDNmLTQyM2QtYjAwNC1kNDgzZjZkMzZkZmEiLCJpYXQiOjE1Mjg0MjY0NTEsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtaWtlLnVzZXIiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy93ZWJ1c2Vyc2lkIjoiQTAwMEszUEgiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy91c2Vyc2lkIjoiQTAwMEszUEYiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy9ncm91cHNpZCI6IjAwMDAwMDBCIiwiaHR0cDovL3d3dy5kYndvcmtzLmNvbS9jbGFpbXMvdXNlcnR5cGUiOiJVU0VSIiwibmJmIjoxNTI4NDI2NDUxLCJleHAiOjE3ODc2MjY0NTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTc5NDkiLCJhdWQiOiIqIn0.GJ1r6t3tYMZH8R-FOjKRR1l4A6YrQmcxEYImY-xA5n4";
            dynamic parameters = new ExpandoObject();
            parameters.contractid = contractid;
            await FwReport.GeneratePdfFromUrlAsync("/Reports/ContractReport/index.html", pathPdfReport, apiToken, baseUrl, parameters);
            if (System.IO.File.Exists(pathPdfReport))
            {
                await FwReport.EmailPdfAsync("mike@dbworks.com", "mike@dbworks.com", "PDF Test Mail", $"In case you didn't get the attachment, you can view your pdf online: <a href=\"{response.pdfReportDownloadUrl}\">View Pdf</a>", pathPdfReport);
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