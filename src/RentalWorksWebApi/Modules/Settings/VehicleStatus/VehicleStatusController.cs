using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleStatusController : AppDataController
    {
        public VehicleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclestatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleStatusLogic>(pageno, pagesize, sort, typeof(VehicleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclestatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleStatusLogic>(id, typeof(VehicleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclestatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleStatusLogic l)
        {
            return await DoPostAsync<VehicleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclestatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleStatusLogic));
        }
        //------------------------------------------------------------------------------------
    }
}