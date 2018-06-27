using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseBarCodeSkip
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WarehouseBarCodeSkipController : AppDataController
    {
        public WarehouseBarCodeSkipController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseBarCodeSkipLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousebarcodeskip/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(pageno, pagesize, sort, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(id, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousebarcodeskip 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseBarCodeSkipLogic l)
        {
            return await DoPostAsync<WarehouseBarCodeSkipLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousebarcodeskip/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}