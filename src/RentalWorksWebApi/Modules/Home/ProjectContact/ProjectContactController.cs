using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ProjectContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ProjectContactController : AppDataController
    {
        public ProjectContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcontact/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectContactLogic>(pageno, pagesize, sort, typeof(ProjectContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectContactLogic>(id, typeof(ProjectContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcontact 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectContactLogic l)
        {
            return await DoPostAsync<ProjectContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectcontact/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectContactLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
