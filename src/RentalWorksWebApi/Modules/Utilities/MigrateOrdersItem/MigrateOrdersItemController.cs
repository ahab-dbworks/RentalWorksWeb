using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Home.MigrateOrdersItem;

namespace WebApi.Modules.Utilities.MigrateOrdersItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "szZ66eT0VS5")]
    public class MigrateOrdersItemController : AppDataController
    {
        public MigrateOrdersItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MigrateOrdersItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrateordersitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Fz8JHFOgRft")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrateordersitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "lYDGBWo1fzc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
