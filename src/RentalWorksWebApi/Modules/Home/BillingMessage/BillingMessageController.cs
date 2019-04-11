using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Home.BillingMessage
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "U0HFTNmYWt3a7")]
    public class BillingMessageController : AppDataController
    {
        public BillingMessageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingMessageLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingmessage/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "wtg8ZEAsVGYZp")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingmessage/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "lmWaQTGgRsHq")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
