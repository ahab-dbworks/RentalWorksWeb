using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.QuikActivityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "yhYOLhLE92IT")]
    public class QuikActivityTypeController : AppDataController
    {
        public QuikActivityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuikActivityTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivitytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "RhaSuoafWaVn0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/auikactivitytype/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0RUvheRQ5Bgn")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
