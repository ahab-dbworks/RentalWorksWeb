using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoApproverController : AppDataController
    {
        public PoApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/browse 
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
        // GET api/v1/poapprover 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover 
        [HttpPost]
        public async Task<ActionResult<PoApproverLogic>> PostAsync([FromBody]PoApproverLogic l)
        {
            return await DoPostAsync<PoApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprover/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}