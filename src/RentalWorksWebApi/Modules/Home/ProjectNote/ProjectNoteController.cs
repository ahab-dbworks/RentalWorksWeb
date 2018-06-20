using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ProjectNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ProjectNoteController : AppDataController
    {
        public ProjectNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectnote/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectnote 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectNoteLogic>(pageno, pagesize, sort, typeof(ProjectNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectnote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectNoteLogic>(id, typeof(ProjectNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectnote 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectNoteLogic l)
        {
            return await DoPostAsync<ProjectNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectnote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}