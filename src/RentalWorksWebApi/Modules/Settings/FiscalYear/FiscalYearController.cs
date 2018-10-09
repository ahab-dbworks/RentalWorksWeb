using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FiscalYear
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FiscalYearController : AppDataController
    {
        public FiscalYearController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FiscalYearLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear/browse 
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
        // GET api/v1/fiscalyear 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FiscalYearLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalYearLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalyear/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<FiscalYearLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalYearLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear 
        [HttpPost]
        public async Task<ActionResult<FiscalYearLogic>> PostAsync([FromBody]FiscalYearLogic l)
        {
            return await DoPostAsync<FiscalYearLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalyear/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}