using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DepartmentLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class DepartmentLocationController : AppDataController
    {
        public DepartmentLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepartmentLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentlocation/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DepartmentLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentlocation 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepartmentLocationLogic>(pageno, pagesize, sort, typeof(DepartmentLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentlocation/A0000001~A0000002   (departmentid~locationid)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepartmentLocationLogic>(id, typeof(DepartmentLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentlocation 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DepartmentLocationLogic l)
        {
            return await DoPostAsync<DepartmentLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/departmentlocation/A0000001
        //[HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        // {
        //     return await DoDeleteAsync(id, typeof(DepartmentLocationLogic));
        // }
        // ------------------------------------------------------------------------------------ 
        // POST api/v1/departmentlocation/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
