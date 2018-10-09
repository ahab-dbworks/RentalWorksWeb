using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UnretiredReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UnretiredReasonController : AppDataController
    {
        public UnretiredReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UnretiredReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason/browse
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
        // GET api/v1/unretiredreason
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnretiredReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnretiredReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unretiredreason/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<UnretiredReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnretiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason
        [HttpPost]
        public async Task<ActionResult<UnretiredReasonLogic>> PostAsync([FromBody]UnretiredReasonLogic l)
        {
            return await DoPostAsync<UnretiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unretiredreason/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}