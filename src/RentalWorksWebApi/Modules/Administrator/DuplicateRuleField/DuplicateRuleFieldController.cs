using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.DuplicateRuleField
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class DuplicateRuleFieldController : AppDataController
    {
        public DuplicateRuleFieldController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DuplicateRuleFieldLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield/browse 
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
        // GET api/v1/duplicaterulefield 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuplicateRuleFieldLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterulefield/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<DuplicateRuleFieldLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield 
        [HttpPost]
        public async Task<ActionResult<DuplicateRuleFieldLogic>> PostAsync([FromBody]DuplicateRuleFieldLogic l)
        {
            return await DoPostAsync<DuplicateRuleFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/duplicaterulefield/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}