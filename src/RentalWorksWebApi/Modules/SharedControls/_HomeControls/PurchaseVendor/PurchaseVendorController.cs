using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.HomeControls.PurchaseVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "15yjeHiHe1x99")]
    public class PurchaseVendorController : AppDataController
    {
        public PurchaseVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseVendorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "18hQcXxqhyTs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchasevendor/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "1axDBZJDBotJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
