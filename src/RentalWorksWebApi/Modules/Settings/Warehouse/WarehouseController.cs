using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Warehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WarehouseController : AppDataController
    {
        public WarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
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
        // GET api/v1/customertype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        public async Task<ActionResult<WarehouseLogic>> PostAsync([FromBody]WarehouseLogic l)
        {
            return await DoPostAsync<WarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}