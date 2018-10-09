using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.ScheduleType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ScheduleTypeController : AppDataController
    {
        public ScheduleTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ScheduleTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/scheduletype/browse
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
        // GET api/v1/scheduletype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ScheduleTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/scheduletype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<ScheduleTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/scheduletype
        [HttpPost]
        public async Task<ActionResult<ScheduleTypeLogic>> PostAsync([FromBody]ScheduleTypeLogic l)
        {
            return await DoPostAsync<ScheduleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/scheduletype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}