using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CrewStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CrewStatusController : AppDataController
    {
        public CrewStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
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
        // GET api/v1/crewstatus
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<ActionResult<IEnumerable<CrewStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CrewStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CrewStatusLogic>> PostAsync([FromBody]CrewStatusLogic l)
        {
            return await DoPostAsync<CrewStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}