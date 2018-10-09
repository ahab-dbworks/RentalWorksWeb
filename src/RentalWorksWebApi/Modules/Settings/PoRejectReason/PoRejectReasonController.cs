using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoRejectReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoRejectReasonController : AppDataController
    {
        public PoRejectReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoRejectReasonLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason/browse 
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
        // GET api/v1/porejectreason 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoRejectReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoRejectReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/porejectreason/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoRejectReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoRejectReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason 
        [HttpPost]
        public async Task<ActionResult<PoRejectReasonLogic>> PostAsync([FromBody]PoRejectReasonLogic l)
        {
            return await DoPostAsync<PoRejectReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/porejectreason/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
