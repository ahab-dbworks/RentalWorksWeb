using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FiscalMonth
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FiscalMonthController : AppDataController
    {
        public FiscalMonthController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FiscalMonthLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FiscalMonthLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalMonthLogic>(pageno, pagesize, sort, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<FiscalMonthLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalMonthLogic>(id, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth 
        [HttpPost]
        public async Task<ActionResult<FiscalMonthLogic>> PostAsync([FromBody]FiscalMonthLogic l)
        {
            return await DoPostAsync<FiscalMonthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalmonth/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}