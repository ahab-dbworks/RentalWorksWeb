using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Modules.Inventory.PartsInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"2WDCohbQV6GU")]
    public class PartsInventoryController : AppDataController
    {
        public PartsInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PartsInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"0m6oe9mXIrXk")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "WXNUa4yQceOUW")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Item", RwGlobals.ITEM_COLOR);
            legend.Add("Accessory", RwGlobals.ACCESSORY_COLOR);
            legend.Add("Complete", RwGlobals.COMPLETE_COLOR);
            legend.Add("Kit", RwGlobals.KIT_COLOR);
            legend.Add("Miscellaneous", RwGlobals.MISCELLANEOUS_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"bE9Fkk0FGmdv")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory 
        [HttpGet]
        [FwControllerMethod(Id:"15qS30cmG6yl")]
        public async Task<ActionResult<IEnumerable<PartsInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3Zv1GUBp8NDH")]
        public async Task<ActionResult<PartsInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory 
        [HttpPost]
        [FwControllerMethod(Id:"gaLAUtuam2Wf")]
        public async Task<ActionResult<PartsInventoryLogic>> PostAsync([FromBody]PartsInventoryLogic l)
        {
            return await DoPostAsync<PartsInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/partsinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hPUVWG4l9BLE")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PartsInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
