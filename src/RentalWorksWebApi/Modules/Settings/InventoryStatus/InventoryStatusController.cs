using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class InventoryStatusController : AppDataController
    {
        public InventoryStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus/browse
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
        // GET api/v1/inventorystatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus
        [HttpPost]
        public async Task<ActionResult<InventoryStatusLogic>> PostAsync([FromBody]InventoryStatusLogic l)
        {
            return await DoPostAsync<InventoryStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorystatus/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}