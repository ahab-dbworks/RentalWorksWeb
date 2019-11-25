using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.RentalCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.Rank;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

namespace WebApi.Modules.Inventory.RentalInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "3ICuf6pSeBh6G")]
    public class RentalInventoryController : AppDataController
    {
        public RentalInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RentalInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "w0K9FrGmrnY4D", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "LmaQ1Su1L3Wm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Item", RwGlobals.ITEM_COLOR);
            legend.Add("Accessory", RwGlobals.ACCESSORY_COLOR);
            legend.Add("Complete", RwGlobals.COMPLETE_COLOR);
            legend.Add("Kit", RwGlobals.KIT_COLOR);
            legend.Add("Miscellaneous", RwGlobals.MISCELLANEOUS_COLOR);
            legend.Add("Container", RwGlobals.CONTAINER_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory/availabilitylegend 
        [HttpGet("availabilitylegend")]
        [FwControllerMethod(Id: "QrAMF2SF1AZnJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAvailabilityLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Reserved", RwGlobals.AVAILABILITY_COLOR_RESERVED);
            legend.Add("Sub Rent", RwGlobals.SUB_COLOR);
            legend.Add("Staged", RwGlobals.STAGED_COLOR);
            legend.Add("Out", RwGlobals.OUT_COLOR);
            legend.Add("Late", RwGlobals.AVAILABILITY_COLOR_LATE);
            legend.Add("In Transit", RwGlobals.IN_TRANSIT_COLOR);
            legend.Add("In Repair", RwGlobals.IN_REPAIR_COLOR);
            legend.Add("Pending Exchange", RwGlobals.PENDING_EXCHANGE_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "PxgrXHTsXkrDh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory 
        [HttpGet]
        [FwControllerMethod(Id: "ERrwz0n6TN23W", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RentalInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "li638sfgYrN5f", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RentalInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory 
        [HttpPost]
        [FwControllerMethod(Id: "ZUrTgW9ORQwDB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<RentalInventoryLogic>> NewAsync([FromBody]RentalInventoryLogic l)
        {
            return await DoNewAsync<RentalInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/rentalinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "TfAlL8bl6nTcI", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RentalInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]RentalInventoryLogic l)
        {
            return await DoEditAsync<RentalInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/rentalinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "S5rVXgAojEEtz", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RentalInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
