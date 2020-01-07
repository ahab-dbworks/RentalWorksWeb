using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AddressSettings.Country;

namespace WebApi.Modules.Utilities.InventoryPurchaseUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "sOxbXBmCPc9y")]
    public class InventoryPurchaseUtilityController : AppDataController
    {
        public InventoryPurchaseUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseutility/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "MswwSEhfz761H", ActionType: FwControllerActionTypes.Option, Caption: "Start Session")]
        public async Task<ActionResult<StartInventoryPurchaseSessionResponse>> StartSession ([FromBody]StartInventoryPurchaseSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StartInventoryPurchaseSessionResponse response = await InventoryPurchaseUtilityFunc.StartSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseutility/updatesession
        [HttpPost("updatesession")]
        [FwControllerMethod(Id: "7PRcr9vgVyP38", ActionType: FwControllerActionTypes.Option, Caption: "Update Session")]
        public async Task<ActionResult<UpdateInventoryPurchaseSessionResponse>> UpdateSession([FromBody]UpdateInventoryPurchaseSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UpdateInventoryPurchaseSessionResponse response = await InventoryPurchaseUtilityFunc.UpdateSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/inventorypurchaseutility/assignbarcodes
        [HttpPost("assignbarcodes")]
        [FwControllerMethod(Id: "tMnf44QPZSZJ1", ActionType: FwControllerActionTypes.Option, Caption: "Assign Bar Codes")]
        public async Task<ActionResult<InventoryPurchaseAssignBarCodesResponse>> AssignBarCodes([FromBody]InventoryPurchaseAssignBarCodesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventoryPurchaseAssignBarCodesResponse response = await InventoryPurchaseUtilityFunc.AssignBarCodes(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/inventorypurchaseutility/completesession
        [HttpPost("completesession")]
        [FwControllerMethod(Id: "niBKGqW4TdGbC", ActionType: FwControllerActionTypes.Option, Caption: "Complete Session")]
        public async Task<ActionResult<InventoryPurchaseCompleteSessionResponse>> CompleteSession([FromBody]InventoryPurchaseCompleteSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventoryPurchaseCompleteSessionResponse response = await InventoryPurchaseUtilityFunc.CompleteSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "dOR7nhBHtgcb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "dpjJfCk0MVLr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validatemanufacturervendor/browse
        [HttpPost("validatemanufacturervendor/browse")]
        [FwControllerMethod(Id: "6yUXyKkdlaDC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "VRYssQWhAObK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validatepurchasevendor/browse
        [HttpPost("validatepurchasevendor/browse")]
        [FwControllerMethod(Id: "OrsnxJysdBbY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
    }
}
