using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoApprovalStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"9CsrBJ9TN1wT")]
    public class PoApprovalStatusController : AppDataController
    {
        public PoApprovalStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApprovalStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprovalstatus/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"laSOrcj4QKeF")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"xGaFN6maSnJ8")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprovalstatus 
        [HttpGet]
        [FwControllerMethod(Id:"qjvZbBPuBWR3")]
        public async Task<ActionResult<IEnumerable<PoApprovalStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApprovalStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprovalstatus/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"uasssbdfPY6B")]
        public async Task<ActionResult<PoApprovalStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApprovalStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprovalstatus 
        [HttpPost]
        [FwControllerMethod(Id:"zto2K8DE9WbD")]
        public async Task<ActionResult<PoApprovalStatusLogic>> PostAsync([FromBody]PoApprovalStatusLogic l)
        {
            return await DoPostAsync<PoApprovalStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprovalstatus/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"UyjCA0PyDdXA")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoApprovalStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
