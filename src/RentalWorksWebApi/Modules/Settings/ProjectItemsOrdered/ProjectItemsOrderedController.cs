using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectItemsOrdered
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectItemsOrderedController : AppDataController
    {
        public ProjectItemsOrderedController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectItemsOrderedLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectitemsordered/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectItemsOrderedLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectitemsordered 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectItemsOrderedLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectItemsOrderedLogic>(pageno, pagesize, sort, typeof(ProjectItemsOrderedLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectitemsordered/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectItemsOrderedLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectItemsOrderedLogic>(id, typeof(ProjectItemsOrderedLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectitemsordered 
        [HttpPost]
        public async Task<ActionResult<ProjectItemsOrderedLogic>> PostAsync([FromBody]ProjectItemsOrderedLogic l)
        {
            return await DoPostAsync<ProjectItemsOrderedLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectitemsordered/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectItemsOrderedLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}