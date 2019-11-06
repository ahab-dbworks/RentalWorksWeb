using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventoryGroupInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"IC5rbdvS3Me7")]
    public class InventoryGroupInventoryController : AppDataController
    {
        public InventoryGroupInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryGroupInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"80FXatx3BHkB")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"bmgviOtVPuHH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory 
        [HttpGet]
        [FwControllerMethod(Id:"9IggnaXwWejp")]
        public async Task<ActionResult<IEnumerable<InventoryGroupInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Vp4m4BPkQU9C")]
        public async Task<ActionResult<InventoryGroupInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory 
        [HttpPost]
        [FwControllerMethod(Id:"brwOVho2KD7i")]
        public async Task<ActionResult<InventoryGroupInventoryLogic>> PostAsync([FromBody]InventoryGroupInventoryLogic l)
        {
            return await DoPostAsync<InventoryGroupInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroupinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"inTP3b6Z0QsI")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryGroupInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
