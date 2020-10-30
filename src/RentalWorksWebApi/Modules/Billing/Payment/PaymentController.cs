using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Utilities.GLDistribution;

namespace WebApi.Modules.Billing.Payment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "Y7YC6NpLqX8kx")]
    public class PaymentController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public PaymentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProcessCreditCardLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Y8plvKHzbbJPv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Y966BxoU9GaTT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "DC2saRJSipdW3", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment 
        [HttpGet]
        [FwControllerMethod(Id: "Y9ePWOz90DFZr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProcessCreditCardLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProcessCreditCardLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "y9OrvcH1WQQ4R", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProcessCreditCardLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProcessCreditCardLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment 
        [HttpPost]
        [FwControllerMethod(Id: "YA1slDDhm7nCv", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProcessCreditCardLogic>> NewAsync([FromBody]ProcessCreditCardLogic l)
        {
            return await DoNewAsync<ProcessCreditCardLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/payment/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "yaDkJFkrMenVX", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProcessCreditCardLogic>> EditAsync([FromRoute] string id, [FromBody]ProcessCreditCardLogic l)
        {
            return await DoEditAsync<ProcessCreditCardLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/payment/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "YAEY9PPoRZIbg", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProcessCreditCardLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/validatepaymenttype/browse
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "Q23AH20YjFsyB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/gldistribution/browse 
        [HttpPost("gldistribution/browse")]
        [FwControllerMethod(Id: "QCTR5CetaVbu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> GLDistribution_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GLDistributionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
