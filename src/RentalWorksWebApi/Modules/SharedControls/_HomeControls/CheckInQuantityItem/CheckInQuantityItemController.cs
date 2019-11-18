using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.CheckInQuantityItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"BfClP5w8rjl7")]
    public class CheckInQuantityItemController : AppDataController
    {
        public CheckInQuantityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CheckInQuantityItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkinquantityitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"oUH7EjZjpJvE")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkinquantityitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"jLnJRXra2nwk")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
