using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcontact 
        [HttpPost]
        public async Task<ActionResult<ProjectContactLogic>> PostAsync([FromBody]ProjectContactLogic l)
        {
            return await DoPostAsync<ProjectContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectcontact/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
