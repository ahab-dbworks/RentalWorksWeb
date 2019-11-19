using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.PhysicalInventoryQuantityInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "EZDA4vdM8wY32")]
    public class PhysicalInventoryQuantityInventoryController : AppDataController
    {
        public PhysicalInventoryQuantityInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryQuantityInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryquantityinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "EuEHlkSDYLR9Z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryquantityinventory/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "zgq3uXpS68RQm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryquantityinventory 
        [HttpGet]
        [FwControllerMethod(Id: "j9PkQMz2SZZVZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryQuantityInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryQuantityInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryquantityinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "gzML1HNqTfaMf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PhysicalInventoryQuantityInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryQuantityInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryquantityinventory 
        [HttpPost]
        [FwControllerMethod(Id: "WdwWMW2QOb1YA", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PhysicalInventoryQuantityInventoryLogic>> NewAsync([FromBody]PhysicalInventoryQuantityInventoryLogic l)
        {
            return await DoNewAsync<PhysicalInventoryQuantityInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/physicalinventoryquantityinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "PegeTyRFM8lw8", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PhysicalInventoryQuantityInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]PhysicalInventoryQuantityInventoryLogic l)
        {
            return await DoEditAsync<PhysicalInventoryQuantityInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/physicalinventoryquantityinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "fzb8pvqJCCM38", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PhysicalInventoryQuantityInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
