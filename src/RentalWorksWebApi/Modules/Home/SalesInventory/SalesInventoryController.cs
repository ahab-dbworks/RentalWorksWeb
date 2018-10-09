using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Home.SalesInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class SalesInventoryController : AppDataController
    {
        public SalesInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/browse 
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
        // GET api/v1/salesinventory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory 
        [HttpPost]
        public async Task<ActionResult<SalesInventoryLogic>> PostAsync([FromBody]SalesInventoryLogic l)
        {
            return await DoPostAsync<SalesInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/salesinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
