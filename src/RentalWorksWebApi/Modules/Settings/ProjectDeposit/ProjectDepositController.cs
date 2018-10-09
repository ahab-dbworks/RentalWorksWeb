using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectDeposit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectDepositController : AppDataController
    {
        public ProjectDepositController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDepositLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdeposit/browse 
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
        // GET api/v1/projectdeposit 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDepositLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDepositLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdeposit/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDepositLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDepositLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdeposit 
        [HttpPost]
        public async Task<ActionResult<ProjectDepositLogic>> PostAsync([FromBody]ProjectDepositLogic l)
        {
            return await DoPostAsync<ProjectDepositLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdeposit/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}