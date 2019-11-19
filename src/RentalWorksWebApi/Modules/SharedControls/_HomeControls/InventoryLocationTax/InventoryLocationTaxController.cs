using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InventoryLocationTax
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"dpDtvVrXRZrd")]
    public class InventoryLocationTaxController : AppDataController
    {
        public InventoryLocationTaxController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryLocationTaxLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GNQxSIJYaT1C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Be4XkpKdpS6Q", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax 
        [HttpGet]
        [FwControllerMethod(Id:"NgjytzWxWlSg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryLocationTaxLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jlWSyNVtC9nn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryLocationTaxLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax 
        [HttpPost]
        [FwControllerMethod(Id:"sfZ7Tlqb7TVM", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryLocationTaxLogic>> NewAsync([FromBody]InventoryLocationTaxLogic l)
        {
            return await DoNewAsync<InventoryLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorylocationtax/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "06fTM0WNIToJG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryLocationTaxLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryLocationTaxLogic l)
        {
            return await DoEditAsync<InventoryLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorylocationtax/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hvrSMK5zNq46", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryLocationTaxLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
