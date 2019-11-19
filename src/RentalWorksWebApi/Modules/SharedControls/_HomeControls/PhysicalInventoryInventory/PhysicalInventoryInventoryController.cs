using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.PhysicalInventoryInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "BEoHoFVd3JFXN")]
    public class PhysicalInventoryInventoryController : AppDataController
    {
        public PhysicalInventoryInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "V2Cl3r49EVY8B", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "31kDu5tRgsaYj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryinventory 
        [HttpGet]
        [FwControllerMethod(Id: "lYX4o25qLLXJq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "CdopKV0vubEcU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PhysicalInventoryInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory 
        [HttpPost]
        [FwControllerMethod(Id: "Mgoxy3IvCQEM3", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PhysicalInventoryInventoryLogic>> NewAsync([FromBody]PhysicalInventoryInventoryLogic l)
        {
            return await DoNewAsync<PhysicalInventoryInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/physicalinventoryinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ZMlEoqg0XWlQQ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PhysicalInventoryInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]PhysicalInventoryInventoryLogic l)
        {
            return await DoEditAsync<PhysicalInventoryInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/physicalinventoryinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "i3spKrXxtqJk1", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PhysicalInventoryInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
