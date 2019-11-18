using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.CheckOutPendingItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"GO96A3pk0UE")]
    public class CheckOutPendingItemController : AppDataController
    {
        public CheckOutPendingItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CheckOutPendingItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkoutpendingitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"LqZGfvZBNvS")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkoutpendingitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ado6dYU78nf")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
