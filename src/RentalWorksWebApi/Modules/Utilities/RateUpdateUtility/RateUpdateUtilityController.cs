using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Inventory.Inventory;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.Category;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

namespace WebApi.Modules.Utilities.RateUpdateUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "MUIYTomUGshV")]
    public class RateUpdateUtilityController : AppDataController
    {
        public RateUpdateUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/apply
        [HttpPost("apply")]
        [FwControllerMethod(Id: "cA6TgPc9lZ6kA", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ApplyPendingRateUpdateModificationsResponse>> ApplyPendingModificationsAsync([FromBody]ApplyPendingRateUpdateModificationsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ApplyPendingRateUpdateModificationsResponse response = await InventoryFunc.ApplyPendingModificationsAsync(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "2C78FvM73Uiu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "JtBywX0TyX1L", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validatecategory/browse
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "Bda8ZijNCD4G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "2GSzMtXga27N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "cCIxiEY5nxe0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "LOEVQa16fR7G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validatemanufacturer/browse
        [HttpPost("validatemanufacturer/browse")]
        [FwControllerMethod(Id: "feL1B77ZtfoQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}

