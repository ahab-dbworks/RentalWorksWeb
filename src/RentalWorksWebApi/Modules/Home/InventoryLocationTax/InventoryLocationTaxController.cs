using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryLocationTax
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryLocationTaxController : AppDataController
    {
        public InventoryLocationTaxController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryLocationTaxLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryLocationTaxLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(pageno, pagesize, sort, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryLocationTaxLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax 
        [HttpPost]
        public async Task<ActionResult<InventoryLocationTaxLogic>> PostAsync([FromBody]InventoryLocationTaxLogic l)
        {
            return await DoPostAsync<InventoryLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorylocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}