using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.HomeControls.ContainerItem;
using WebApi.Modules.Settings.InventorySettings.RentalCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Settings.InventorySettings.InventoryType;

namespace WebApi.Modules.Containers.ContainerStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"AQCjfAUtfHy5")]
    public class ContainerStatusController : AppDataController
    {
        public ContainerStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatecontaineritem/browse 
        [HttpPost("validatecontaineritem/browse")]
        [FwControllerMethod(Id: "49rQNhdCTFyE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContainerItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContainerItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "eXkw9GXjyMMa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "HOyUVlt8MUhK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "wTA50Oh6lXXI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "M38GcbHWNvpH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
    }
}
