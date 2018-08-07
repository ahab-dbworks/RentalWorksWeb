using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.GlDistributionReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class GlDistributionReportController : AppDataController
    {
        public GlDistributionReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlDistributionReportLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionreport/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/gldistributionreport 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<GlDistributionReportLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/gldistributionreport/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<GlDistributionReportLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/gldistributionreport 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]GlDistributionReportLogic l)
        //{
        //    return await DoPostAsync<GlDistributionReportLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/gldistributionreport/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
