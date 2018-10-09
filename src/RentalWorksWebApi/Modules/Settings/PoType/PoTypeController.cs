using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoTypeController : AppDataController
    {
        public PoTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype/browse 
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
        // GET api/v1/potype 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/potype/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype 
        [HttpPost]
        public async Task<ActionResult<PoTypeLogic>> PostAsync([FromBody]PoTypeLogic l)
        {
            return await DoPostAsync<PoTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/potype/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}