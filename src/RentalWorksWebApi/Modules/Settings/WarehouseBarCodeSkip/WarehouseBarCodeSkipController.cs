using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseBarCodeSkipLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(pageno, pagesize, sort, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousebarcodeskip/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseBarCodeSkipLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseBarCodeSkipLogic>(id, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousebarcodeskip 
        [HttpPost]
        public async Task<ActionResult<WarehouseBarCodeSkipLogic>> PostAsync([FromBody]WarehouseBarCodeSkipLogic l)
        {
            return await DoPostAsync<WarehouseBarCodeSkipLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousebarcodeskip/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WarehouseBarCodeSkipLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}