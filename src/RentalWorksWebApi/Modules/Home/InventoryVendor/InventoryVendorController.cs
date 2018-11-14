using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"s9vdtBqItIEi")]
    public class InventoryVendorController : AppDataController
    {
        public InventoryVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryVendorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvendor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"IHxNdefO8zUm")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"v4L0Hcg0kOJi")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryvendor 
        [HttpGet]
        [FwControllerMethod(Id:"8GlpeK1vpg3n")]
        public async Task<ActionResult<IEnumerable<InventoryVendorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryVendorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryvendor/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"NnyCGrQe006J")]
        public async Task<ActionResult<InventoryVendorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryVendorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvendor 
        [HttpPost]
        [FwControllerMethod(Id:"ezzYvgp4Q0GN")]
        public async Task<ActionResult<InventoryVendorLogic>> PostAsync([FromBody]InventoryVendorLogic l)
        {
            return await DoPostAsync<InventoryVendorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryvendor/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"uQdY90pbWeto")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
