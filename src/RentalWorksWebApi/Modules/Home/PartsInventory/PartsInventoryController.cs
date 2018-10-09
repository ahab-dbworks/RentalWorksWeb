using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Home.PartsInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PartsInventoryController : AppDataController
    {
        public PartsInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PartsInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory/browse 
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
        // GET api/v1/partsinventory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartsInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/partsinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PartsInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/partsinventory 
        [HttpPost]
        public async Task<ActionResult<PartsInventoryLogic>> PostAsync([FromBody]PartsInventoryLogic l)
        {
            return await DoPostAsync<PartsInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/partsinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
