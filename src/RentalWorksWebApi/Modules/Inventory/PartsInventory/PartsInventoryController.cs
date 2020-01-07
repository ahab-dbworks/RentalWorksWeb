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
using WebApi.Modules.Settings.InventorySettings.PartsCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.Rank;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Settings.AddressSettings.Country;

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
        [FwControllerMethod(Id:"0m6oe9mXIrXk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "WXNUa4yQceOUW", ActionType: FwControllerActionTypes.Browse)]
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
        // GET api/v1/partsinventory/availabilitylegend 
        [HttpGet("availabilitylegend")]
        [FwControllerMethod(Id: "X3UGavanh3YP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAvailabilityLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            //legend.Add("Reserved", RwGlobals.AVAILABILITY_COLOR_RESERVED);
            //legend.Add("Sub Sale", RwGlobals.SUB_COLOR);
            //legend.Add("Staged", RwGlobals.STAGED_COLOR);
            //legend.Add("Out", RwGlobals.OUT_COLOR);
            //legend.Add("In Transit", RwGlobals.IN_TRANSIT_COLOR);
            legend.Add("In Repair", RwGlobals.IN_REPAIR_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"bE9Fkk0FGmdv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory 
        [HttpGet]
        [FwControllerMethod(Id:"15qS30cmG6yl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PartsInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3Zv1GUBp8NDH", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PartsInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory 
        [HttpPost]
        [FwControllerMethod(Id:"gaLAUtuam2Wf", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PartsInventoryLogic>> NewAsync([FromBody]PartsInventoryLogic l)
        {
            return await DoNewAsync<PartsInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/partsinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xg78aYq5i7Orx", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PartsInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]PartsInventoryLogic l)
        {
            return await DoEditAsync<PartsInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/partsinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hPUVWG4l9BLE", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PartsInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "y6mW1DJpN2sD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatecategory/browse
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "1r90drH6rRf7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PartsCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "8nwogQ5dN4px", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "vmuGWLvkmB8y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validaterank/browse
        [HttpPost("validaterank/browse")]
        [FwControllerMethod(Id: "tcTdq10Q0pYN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRankBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RankLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatemanufacturer/browse
        [HttpPost("validatemanufacturer/browse")]
        [FwControllerMethod(Id: "hvh67oY6bLF2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "7alOw4UHo75d", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "62XWeIHfxPzB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "Ezo7LbxQH2gp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validateprofitandloss/browse
        [HttpPost("validateprofitandloss/browse")]
        [FwControllerMethod(Id: "Rkv5DrzlAd9f", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProfitAndLossBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PartsCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "J3hDOnj1JYd7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/validatecountryoforigin/browse
        [HttpPost("validatecountryoforigin/browse")]
        [FwControllerMethod(Id: "HUYLSE0AdvF7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryOfOriginBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
    }
} 
