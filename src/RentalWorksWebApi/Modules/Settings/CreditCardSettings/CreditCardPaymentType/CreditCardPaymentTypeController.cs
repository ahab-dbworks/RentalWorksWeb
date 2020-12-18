using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;

namespace WebApi.Modules.Settings.CreditCardSettings.CreditCardPaymentType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "XHLACwQDGIzl")]
    public class CreditCardPaymentTypeController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CreditCardPaymentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CreditCardPaymentTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpaytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "oAusrDnJdSkA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpaytype/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0bpGeRf81BAb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/creditcardpaytype 
        [HttpGet]
        [FwControllerMethod(Id: "jsJHQtXqBqGx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CreditCardPaymentTypeLogic>>> GetManyAsync([FromQuery] int pageno, [FromQuery] int pagesize, [FromQuery] string sort)
        {
            return await DoGetAsync<CreditCardPaymentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/creditcardpaytype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "fWgg6oIexkKB", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CreditCardPaymentTypeLogic>> GetOneAsync([FromRoute] string id)
        {
            return await DoGetAsync<CreditCardPaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpaytype 
        [HttpPost]
        [FwControllerMethod(Id: "hkzXDBsV9sl8", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CreditCardPaymentTypeLogic>> NewAsync([FromBody] CreditCardPaymentTypeLogic l)
        {
            return await DoNewAsync<CreditCardPaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/creditcardpaytype/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "4O2T458KE1As", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CreditCardPaymentTypeLogic>> EditAsync([FromRoute] string id, [FromBody] CreditCardPaymentTypeLogic l)
        {
            return await DoEditAsync<CreditCardPaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/creditcardpaytype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "L8bN2vO8RhzG", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string id)
        {
            return await DoDeleteAsync<CreditCardPaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditcardpaymenttype/validatepaymenttype/browse
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "tG8WOoNDhEhD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
