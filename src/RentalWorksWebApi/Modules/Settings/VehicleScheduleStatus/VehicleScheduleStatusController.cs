using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleScheduleStatusController : AppDataController
    {
        public VehicleScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleScheduleStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(pageno, pagesize, sort, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleScheduleStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(id, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus
        [HttpPost]
        public async Task<ActionResult<VehicleScheduleStatusLogic>> PostAsync([FromBody]VehicleScheduleStatusLogic l)
        {
            return await DoPostAsync<VehicleScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicleschedulestatus/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
    }
}