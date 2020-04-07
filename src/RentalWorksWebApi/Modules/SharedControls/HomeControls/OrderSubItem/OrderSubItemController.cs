using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.OrderSubItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "nothing-v1")]
    [FwController(Id: "kaFlpRnRQzIIz")]
    public class OrderSubItemController : AppDataController
    {
        public OrderSubItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSubItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersubitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "KamOhQs3cThZJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersubitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "kBjAVfNn9peh6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
