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
using WebApi.Modules.Settings.InventorySettings.SalesCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.Rank;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Agent.Vendor;

namespace WebApi.Modules.Inventory.SalesInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"ShjGAzM2Pq3kk")]
    public partial class SalesInventoryController : AppDataController
    {
        public SalesInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"dSuCVYOLVhygM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "PwpKssPBV7EWV", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
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
        // GET api/v1/salesinventory/availabilitylegend 
        [HttpGet("availabilitylegend")]
        [FwControllerMethod(Id: "fVRcYkvYvzFNs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAvailabilityLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Reserved", RwGlobals.AVAILABILITY_COLOR_RESERVED);
            legend.Add("Sub Sale", RwGlobals.SUB_COLOR);
            legend.Add("Staged", RwGlobals.STAGED_COLOR);
            legend.Add("Out", RwGlobals.OUT_COLOR);
            legend.Add("In Transit", RwGlobals.IN_TRANSIT_COLOR);
            legend.Add("In Repair", RwGlobals.IN_REPAIR_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PXI94ha4SwIXU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory 
        [HttpGet]
        [FwControllerMethod(Id:"uYKsUb9hxZNkn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SalesInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BHKilcPFXm9Lr", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SalesInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory 
        [HttpPost]
        [FwControllerMethod(Id:"odAO7uIqwgBsc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SalesInventoryLogic>> NewAsync([FromBody]SalesInventoryLogic l)
        {
            return await DoNewAsync<SalesInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/salesinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "3mZL7yxlal5Z9", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SalesInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]SalesInventoryLogic l)
        {
            return await DoEditAsync<SalesInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/salesinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"pOFZu9oVTFwHf", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SalesInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "IpGNSKMXrgI2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatecategoryid/browse
        [HttpPost("validatecategoryid/browse")]
        [FwControllerMethod(Id: "TEH7Nk4YrnQf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "z1Lgt1sGyFt8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "uMB1h5z8jmhW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validaterank/browse
        [HttpPost("validaterank/browse")]
        [FwControllerMethod(Id: "XyAej7JvHBp2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRankBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RankLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatecountryoforigin/browse
        [HttpPost("validatecountryoforigin/browse")]
        [FwControllerMethod(Id: "8FEzXcDpkJMb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryOfOriginBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateprofitandlosscategory/browse
        [HttpPost("validateprofitandlosscategory/browse")]
        [FwControllerMethod(Id: "SdRvXS7nSYt5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProfitAndLossBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "BoxB57A3xJ3L", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "B9kfS3LpUzvf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "qpHYDbjjnJ8K", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "PbtnjSNllf3Y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "cHvF69VUCP1x", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validatemanufacturer/browse
        [HttpPost("validatemanufacturer/browse")]
        [FwControllerMethod(Id: "SoNWYzKpXW19", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
    }
} 
