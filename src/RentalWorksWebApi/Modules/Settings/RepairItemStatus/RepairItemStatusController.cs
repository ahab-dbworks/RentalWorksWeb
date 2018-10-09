using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RepairItemStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RepairItemStatusController : AppDataController
    {
        public RepairItemStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairItemStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepairItemStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairItemStatusLogic>(pageno, pagesize, sort, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairItemStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairItemStatusLogic>(id, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus
        [HttpPost]
        public async Task<ActionResult<RepairItemStatusLogic>> PostAsync([FromBody]RepairItemStatusLogic l)
        {
            return await DoPostAsync<RepairItemStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/repairitemstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
    }
}