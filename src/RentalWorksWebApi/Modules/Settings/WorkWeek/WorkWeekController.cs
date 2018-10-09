using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WorkWeek
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WorkWeekController : AppDataController
    {
        public WorkWeekController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WorkWeekLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/workweek 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkWeekLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WorkWeekLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/workweek/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkWeekLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WorkWeekLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek 
        [HttpPost]
        public async Task<ActionResult<WorkWeekLogic>> PostAsync([FromBody]WorkWeekLogic l)
        {
            return await DoPostAsync<WorkWeekLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/workweek/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
