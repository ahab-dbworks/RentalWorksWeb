using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleColorController : AppDataController
    {
        public VehicleColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleColorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclecolor/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleColorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclecolor
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleColorLogic>(pageno, pagesize, sort, typeof(VehicleColorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclecolor/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleColorLogic>(id, typeof(VehicleColorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclecolor
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleColorLogic l)
        {
            return await DoPostAsync<VehicleColorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclecolor/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleColorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclecolor/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}