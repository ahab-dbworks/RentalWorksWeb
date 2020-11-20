using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Billing.BankAccount
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "xJzM0aYJ70srp")]
    public class BankAccountController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public BankAccountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BankAccountLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/bankaccount/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "xk1C2EDOK5woP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/bankaccount/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "XKncayBV5syiT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/bankaccount 
        [HttpGet]
        [FwControllerMethod(Id: "xknyN8ECn3MYl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<BankAccountLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BankAccountLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/bankaccount/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "XLKO8xhrpLX3L", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<BankAccountLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BankAccountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/bankaccount 
        [HttpPost]
        [FwControllerMethod(Id: "xlzCsqB2UKD1X", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<BankAccountLogic>> NewAsync([FromBody]BankAccountLogic l)
        {
            return await DoNewAsync<BankAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/bankaccount/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xlzubyKbAGuKa", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<BankAccountLogic>> EditAsync([FromRoute] string id, [FromBody]BankAccountLogic l)
        {
            return await DoEditAsync<BankAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/bankaccount/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "XN2LvAYrRieM5", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BankAccountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
