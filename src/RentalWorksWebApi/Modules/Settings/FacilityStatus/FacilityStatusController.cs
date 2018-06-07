using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FacilityStatusController : AppDataController
    {
        public FacilityStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{4CBD88A1-AF2C-4DF5-A3CE-B2BB6C40092D}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus
        [HttpGet]
        [Authorize(Policy = "{6C73F07A-50C9-471D-84B6-3115B8495662}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityStatusLogic>(pageno, pagesize, sort, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{6206FA53-91F1-4B6E-BAC9-D02490571609}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityStatusLogic>(id, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus
        [HttpPost]
        [Authorize(Policy = "{521B52BB-9C28-425D-9B06-BFD0333FDCBF}")]
        public async Task<IActionResult> PostAsync([FromBody]FacilityStatusLogic l)
        {
            return await DoPostAsync<FacilityStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitystatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{92046B12-F2B5-4371-9448-01113F0FD496}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{149F4190-E046-4FB3-94CD-3F4C727C511D}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}