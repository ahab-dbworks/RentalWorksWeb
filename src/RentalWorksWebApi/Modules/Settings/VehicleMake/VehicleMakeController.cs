using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleMake
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleMakeController : AppDataController
    {
        public VehicleMakeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleMakeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleMakeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemake
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleMakeLogic>(pageno, pagesize, sort, typeof(VehicleMakeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemake/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleMakeLogic>(id, typeof(VehicleMakeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleMakeLogic l)
        {
            return await DoPostAsync<VehicleMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclemake/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleMakeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}