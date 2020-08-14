using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi;
using WebApi.Modules.HomeControls.DealCredit;
using WebApi.Modules.HomeControls.CustomerCredit;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Utilities.GLDistribution;

namespace WebApi.Modules.Billing.Receipt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"q4PPGLusbFw")]
    public class ReceiptController : AppDataController
    {
        public ReceiptController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"UQfZi78fIik", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "5dkaEowIuuvj3", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Overpayment", RwGlobals.RECEIPT_RECTYPE_OVERPAYMENT_COLOR);
            legend.Add("Depleting Deposit", RwGlobals.RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR);
            legend.Add("Refund Check", RwGlobals.RECEIPT_RECTYPE_REFUND_CHECK_COLOR);
            legend.Add("NSF Adjustment", RwGlobals.RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR);
            legend.Add("Write Off", RwGlobals.RECEIPT_RECTYPE_WRITE_OFF_COLOR);
            legend.Add("Credit Memo", RwGlobals.RECEIPT_RECTYPE_CREDIT_MEMO_COLOR);
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"cD0i6Lu7l6y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt 
        [HttpGet]
        [FwControllerMethod(Id:"WXqdiCvvthD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ReceiptLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ReceiptLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"n5V9FG9rUry", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ReceiptLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ReceiptLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt 
        [HttpPost]
        [FwControllerMethod(Id:"JdkHwTtfSOq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ReceiptLogic>> NewAsync([FromBody]ReceiptLogic l)
        {
            return await DoNewAsync<ReceiptLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/receipt/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "AUWHk7opHV0Vv", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ReceiptLogic>> EditAsync([FromRoute] string id, [FromBody]ReceiptLogic l)
        {
            return await DoEditAsync<ReceiptLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/receipt/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"RZg83sSIUEo", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ReceiptLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/receipt/overridedelete/A0000001 
        [HttpDelete("overridedelete/{id}")]
        [FwControllerMethod(Id: "kXttDhRjrgZ", ActionType: FwControllerActionTypes.Option, Caption: "Override Delete")]
        public async Task<ActionResult<bool>> OverrideDelete([FromRoute]string id)
        {
            return await ReceiptFunc.DeleteReceipt(AppConfig, UserSession, id);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt/remainingdepositamounts
        [HttpGet("remainingdepositamounts")]
        [FwControllerMethod(Id: "rewXg7ccffYIe", ActionType: FwControllerActionTypes.Option, Caption: "Get Remaining Deposit Amounts")]
        public async Task<ActionResult<RemainingDepositAmountsResponse>> GetRemainingDepositAmounts([FromRoute] string CustomerId, string DealId, string OfficeLocationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemainingDepositAmountsRequest request = new RemainingDepositAmountsRequest();
                request.CustomerId = CustomerId;
                request.DealId = DealId;
                request.OfficeLocationId = OfficeLocationId;

                RemainingDepositAmountsResponse response = await ReceiptFunc.GetRemainingDepositAmounts(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------   
        // POST api/v1/receipt/validatedealdeposit/browse
        [HttpPost("validatedealdeposit/browse")]
        [FwControllerMethod(Id: "rSY8mWOwt83G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealDepositBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealCreditLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------   
        // POST api/v1/receipt/validatecustomerdeposit/browse
        [HttpPost("validatecustomerdeposit/browse")]
        [FwControllerMethod(Id: "4qGuCHPWtEHg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerDepositBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerCreditLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------   
        // POST api/v1/receipt/validateappliedby/browse
        [HttpPost("validateappliedby/browse")]
        [FwControllerMethod(Id: "YUkaRBBBo6O1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAppliedByBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------   
        // POST api/v1/receipt/validatepaymenttype/browse
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "sOLFyGFVmSjA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt/gldistribution/browse 
        [HttpPost("gldistribution/browse")]
        [FwControllerMethod(Id: "NaUcjbEO3bEX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> GLDistribution_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GLDistributionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
