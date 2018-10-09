using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderStatusSummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderStatusSummaryController : AppDataController
    {
        public OrderStatusSummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderStatusSummaryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatussummary/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}