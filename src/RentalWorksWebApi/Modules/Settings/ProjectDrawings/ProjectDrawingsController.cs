using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectDrawings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectDrawingsController : AppDataController
    {
        public ProjectDrawingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDrawingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDrawingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(pageno, pagesize, sort, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDrawingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(id, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings 
        [HttpPost]
        public async Task<ActionResult<ProjectDrawingsLogic>> PostAsync([FromBody]ProjectDrawingsLogic l)
        {
            return await DoPostAsync<ProjectDrawingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdrawings/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}