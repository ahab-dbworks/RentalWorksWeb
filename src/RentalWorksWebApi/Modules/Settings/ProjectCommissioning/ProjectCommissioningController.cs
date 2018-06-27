using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectCommissioning
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectCommissioningController : AppDataController
    {
        public ProjectCommissioningController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectCommissioningLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcommissioning/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectCommissioningLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcommissioning 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectCommissioningLogic>(pageno, pagesize, sort, typeof(ProjectCommissioningLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcommissioning/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectCommissioningLogic>(id, typeof(ProjectCommissioningLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcommissioning 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectCommissioningLogic l)
        {
            return await DoPostAsync<ProjectCommissioningLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectcommissioning/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectCommissioningLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}