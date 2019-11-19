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
        [FwControllerMethod(Id:"NlbkJnGCYYzS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Rkp8e2nIg3t5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth 
        [HttpGet]
        [FwControllerMethod(Id:"Usr9d2xJyEEk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FiscalMonthLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalMonthLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"WAWqtp947RX8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FiscalMonthLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalMonthLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth 
        [HttpPost]
        [FwControllerMethod(Id:"Wzdca5UujceA", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FiscalMonthLogic>> NewAsync([FromBody]FiscalMonthLogic l)
        {
            return await DoNewAsync<FiscalMonthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/fiscalmonth/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "RKf9mDseONRiG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FiscalMonthLogic>> EditAsync([FromRoute] string id, [FromBody]FiscalMonthLogic l)
        {
            return await DoEditAsync<FiscalMonthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalmonth/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8zbAgyeoiH5Y", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FiscalMonthLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
