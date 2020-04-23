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
using WebApi.Modules.Inventory.Inventory;
using System;

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
        [FwControllerMethod(Id: "LmaQ1Su1L3Wm", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
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
            legend.Add("Late But Returning", RwGlobals.AVAILABILITY_COLOR_LATE_BUT_RETURNING);
            legend.Add("Transfer", RwGlobals.IN_TRANSIT_COLOR);
            legend.Add("Repair", RwGlobals.IN_REPAIR_COLOR);
            legend.Add("Container", RwGlobals.CONTAINER_COLOR);
            legend.Add("Pending Exchange", RwGlobals.PENDING_EXCHANGE_COLOR);
            legend.Add("QC Required", RwGlobals.AVAILABILITY_COLOR_QC_REQUIRED);
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
        // POST api/v1/rentalinventory/qcrequiredallwarehouses
        [HttpPost("qcrequiredallwarehouses")]
        [FwControllerMethod(Id: "QiRmnMJzrVZE1", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<RentalInventoryQcRequiredAllWarehousesResponse>> SetQcRequiredAllWarehouses([FromBody]RentalInventoryQcRequiredAllWarehousesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalInventoryLogic l = new RentalInventoryLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.InventoryId = request.InventoryId;
                if (await l.LoadAsync<RentalInventoryLogic>())
                {
                    RentalInventoryQcRequiredAllWarehousesResponse response = await InventoryFunc.SetQcRequiredAllWarehouses(AppConfig, UserSession, request);
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "HJOX4aCjQNGv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatecategory/browse
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "tt6sXDSHFLk9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "K9pykP7HIBfS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "OwoLMPwRJ1sd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validaterank/browse
        [HttpPost("validaterank/browse")]
        [FwControllerMethod(Id: "8LhaM575zOo5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRankBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RankLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatemanufacturer/browse
        [HttpPost("validatemanufacturer/browse")]
        [FwControllerMethod(Id: "Ud7bP8W2onNC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "pg50pDzSQmAI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "H65MDlu9jt1J", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "RvbKeA0U8PVF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateequipmentsaleincomeaccount/browse
        [HttpPost("validateequipmentsaleincomeaccount/browse")]
        [FwControllerMethod(Id: "ahP5pEpINyET", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateEquipmentSaleIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateldincomeaccount/browse
        [HttpPost("validateldincomeaccount/browse")]
        [FwControllerMethod(Id: "dlyd3Ot6qvj4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLDIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "CYOrwOK1kzMw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatecostofgoodsrentedexpenseaccount/browse
        [HttpPost("validatecostofgoodsrentedexpenseaccount/browse")]
        [FwControllerMethod(Id: "DyTOes5t0sG5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsRentedExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateprofitandlosscategory/browse
        [HttpPost("validateprofitandlosscategory/browse")]
        [FwControllerMethod(Id: "08n8XMIOGcKF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProfitAndLossBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalinventory/validatecountryoforigin/browse
        [HttpPost("validatecountryoforigin/browse")]
        [FwControllerMethod(Id: "zTV6dEd6MqFh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryOfOriginBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "xcPcxaFGkDc7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
