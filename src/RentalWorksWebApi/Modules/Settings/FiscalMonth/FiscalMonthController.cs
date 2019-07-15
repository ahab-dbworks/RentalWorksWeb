using FwStandard.AppManager;
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
    [FwController(Id:"wt6RLPk0GOrm")]
    public class FiscalMonthController : AppDataController
    {
        public FiscalMonthController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FiscalMonthLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"NlbkJnGCYYzS")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Rkp8e2nIg3t5")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth 
        [HttpGet]
        [FwControllerMethod(Id:"Usr9d2xJyEEk")]
        public async Task<ActionResult<IEnumerable<FiscalMonthLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalMonthLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"WAWqtp947RX8")]
        public async Task<ActionResult<FiscalMonthLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalMonthLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth 
        [HttpPost]
        [FwControllerMethod(Id:"Wzdca5UujceA")]
        public async Task<ActionResult<FiscalMonthLogic>> PostAsync([FromBody]FiscalMonthLogic l)
        {
            return await DoPostAsync<FiscalMonthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalmonth/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8zbAgyeoiH5Y")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FiscalMonthLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
