using FwStandard.AppManager;
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
    [FwController(Id:"n8p9E78kGRM6")]
    public class FiscalYearController : AppDataController
    {
        public FiscalYearController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FiscalYearLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"s0te36TXWzDj", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"o4FKr6l5Daik", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalyear 
        [HttpGet]
        [FwControllerMethod(Id:"obaFRIxJcRQ3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FiscalYearLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalYearLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalyear/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"KKByxrB6MzeB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FiscalYearLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalYearLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear 
        [HttpPost]
        [FwControllerMethod(Id:"6grLIwkg5c5F", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FiscalYearLogic>> NewAsync([FromBody]FiscalYearLogic l)
        {
            return await DoNewAsync<FiscalYearLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/fiscalyear/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "d9CnTxNc2Cj6Z", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FiscalYearLogic>> EditAsync([FromRoute] string id, [FromBody]FiscalYearLogic l)
        {
            return await DoEditAsync<FiscalYearLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalyear/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"yP1syJGaLNaI", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FiscalYearLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
