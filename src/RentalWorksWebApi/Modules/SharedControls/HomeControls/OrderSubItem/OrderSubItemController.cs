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
        // POST api/v1/ordersubitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "kBjAVfNn9peh6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersubitem/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "ykdUoQoA82I7H", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Estimated Date", RwGlobals.ORDER_SUB_ITEM_DATE_ESTIMATED_COLOR) ;
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
    }
}
