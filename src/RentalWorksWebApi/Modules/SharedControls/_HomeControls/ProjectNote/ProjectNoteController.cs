using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.ProjectNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"tR09bf745p0YU")]
    public class ProjectNoteController : AppDataController
    {
        public ProjectNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectnote/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"HEdjwSofRHntv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1axc1alQaCGzb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectnote 
        [HttpGet]
        [FwControllerMethod(Id:"RbubmqKBKoYjM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectnote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"KRiE0myEU4TCb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ProjectNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectnote 
        [HttpPost]
        [FwControllerMethod(Id:"gcu7B3GMMDxeI", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectNoteLogic>> NewAsync([FromBody]ProjectNoteLogic l)
        {
            return await DoNewAsync<ProjectNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/projectnote/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "M8lw4CVBz9cQs", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectNoteLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectNoteLogic l)
        {
            return await DoEditAsync<ProjectNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectnote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"tzzdkC2n8TiFa", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
