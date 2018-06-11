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
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectDepositLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdeposit 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDepositLogic>(pageno, pagesize, sort, typeof(ProjectDepositLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdeposit/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDepositLogic>(id, typeof(ProjectDepositLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdeposit 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectDepositLogic l)
        {
            return await DoPostAsync<ProjectDepositLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdeposit/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectDepositLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}