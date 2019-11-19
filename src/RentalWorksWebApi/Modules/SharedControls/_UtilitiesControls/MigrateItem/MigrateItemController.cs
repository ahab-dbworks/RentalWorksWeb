using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Utilities.MigrateItem;

namespace WebApi.Modules.UtilitiesControls.MigrateItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "szZ66eT0VS5")]
    public class MigrateItemController : AppDataController
    {
        public MigrateItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MigrateItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrateitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Fz8JHFOgRft", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrateitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "lYDGBWo1fzc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
