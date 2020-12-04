using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Billing.ProcessCreditCard
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "billing-v1")]
    [FwController(Id: "yAale9IPIaUC")]
    public class ProcessCreditCardController : AppDataController
    {
        readonly IProcessCreditCardPlugin processCreditCardPlugin;
        //------------------------------------------------------------------------------------ 
        public ProcessCreditCardController(IOptions<FwApplicationConfig> appConfig, IProcessCreditCardPlugin processCreditCardPlugin) : base(appConfig) 
        {
            this.logicType = typeof(ProcessCreditCardLogic);
            this.processCreditCardPlugin = processCreditCardPlugin;
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/processcreditcard/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "pEhMzzYUHLn6", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProcessCreditCardLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProcessCreditCardLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/processcreditcard/processcreditcard
        [HttpPost("processcreditcardpayment")]
        [FwControllerMethod(Id: "pvc2YoVG316N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ProcessCreditCardPaymentResponse>> ProcessCreditCardPaymentAsync([FromBody]ProcessCreditCardPaymentRequest request) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcessCreditCardLogic logic = new ProcessCreditCardLogic();
                logic.SetDependencies(this.AppConfig, this.UserSession, processCreditCardPlugin);
                var response = await logic.ProcessPaymentAsync(request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
