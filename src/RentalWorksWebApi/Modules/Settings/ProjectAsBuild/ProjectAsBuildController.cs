using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectAsBuild
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectAsBuildController : AppDataController
    {
        public ProjectAsBuildController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectAsBuildLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectasbuild/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectAsBuildLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectasbuild 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectAsBuildLogic>(pageno, pagesize, sort, typeof(ProjectAsBuildLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectasbuild/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectAsBuildLogic>(id, typeof(ProjectAsBuildLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectasbuild 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectAsBuildLogic l)
        {
            return await DoPostAsync<ProjectAsBuildLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectasbuild/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectAsBuildLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectasbuild/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}