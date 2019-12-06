using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public InventoryPurchaseUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {  }
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
