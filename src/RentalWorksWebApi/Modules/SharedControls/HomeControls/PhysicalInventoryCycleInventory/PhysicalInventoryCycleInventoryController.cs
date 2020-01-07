using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.HomeControls.GeneralItem;
namespace WebApi.Modules.HomeControls.PhysicalInventoryCycleInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "juyq8FkxJPR5Q")]
    public class PhysicalInventoryCycleInventoryController : AppDataController
    {
        public PhysicalInventoryCycleInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryCycleInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "GwO069bIU8q7X", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "qTYHT385W14JO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventorycycleinventory 
        [HttpGet]
        [FwControllerMethod(Id: "0OLGeg9p8seDC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryCycleInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryCycleInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventorycycleinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "SxDKfqP6mAjjE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PhysicalInventoryCycleInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryCycleInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory 
        [HttpPost]
        [FwControllerMethod(Id: "WnSydK7diEE53", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PhysicalInventoryCycleInventoryLogic>> NewAsync([FromBody]PhysicalInventoryCycleInventoryLogic l)
        {
            return await DoNewAsync<PhysicalInventoryCycleInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/physicalinventorycycleinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "JMvLIpaXEcVRc", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PhysicalInventoryCycleInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]PhysicalInventoryCycleInventoryLogic l)
        {
            return await DoEditAsync<PhysicalInventoryCycleInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/physicalinventorycycleinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "9i0ofsxK0wV78", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PhysicalInventoryCycleInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "1seIh1AjeGSO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GeneralItemLogic>(browseRequest);
        }
    }
}
