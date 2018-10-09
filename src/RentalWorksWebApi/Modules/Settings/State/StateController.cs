using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.State
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class StateController : AppDataController
    {
        public StateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(StateLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/browse
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
        // GET api/v1/State
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<StateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<StateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<StateLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State
        [HttpPost]
        public async Task<ActionResult<StateLogic>> PostAsync([FromBody]StateLogic l)
        {
            return await DoPostAsync<StateLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/State/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}