using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.ShipVia
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ShipViaController : AppDataController
    {
        public ShipViaController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ShipViaLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/shipvia/browse
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
        // GET api/v1/shipvia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipViaLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ShipViaLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/shipvia/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipViaLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ShipViaLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/shipvia
        [HttpPost]
        public async Task<ActionResult<ShipViaLogic>> PostAsync([FromBody]ShipViaLogic l)
        {
            return await DoPostAsync<ShipViaLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/shipvia/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}