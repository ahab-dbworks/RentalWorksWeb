using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseCatalog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WarehouseCatalogController : AppDataController
    {
        public WarehouseCatalogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseCatalogLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseCatalogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseCatalogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog 
        [HttpPost]
        public async Task<ActionResult<WarehouseCatalogLogic>> PostAsync([FromBody]WarehouseCatalogLogic l)
        {
            return await DoPostAsync<WarehouseCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousecatalog/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}