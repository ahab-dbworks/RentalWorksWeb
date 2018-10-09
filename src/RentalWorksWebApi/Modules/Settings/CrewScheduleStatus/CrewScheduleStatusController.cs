using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CrewScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CrewScheduleStatusController : AppDataController
    {
        public CrewScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus/browse
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
        // GET api/v1/crewschedulestatus
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<ActionResult<IEnumerable<CrewScheduleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewschedulestatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CrewScheduleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CrewScheduleStatusLogic>> PostAsync([FromBody]CrewScheduleStatusLogic l)
        {
            return await DoPostAsync<CrewScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewschedulestatus/A0000001
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