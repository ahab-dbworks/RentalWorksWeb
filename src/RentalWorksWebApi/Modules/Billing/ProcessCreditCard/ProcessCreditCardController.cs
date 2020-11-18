

using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.Inventory.Inventory;
using WebApi.Modules.Inventory.RentalInventory;

namespace WebApi.Modules.Billing.ProcessCreditCard
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "billing-v1")]
    [FwController(Id: "yAale9IPIaUC")]
    public class ProcessCreditCardController : AppDataController
    {
        readonly IProcessCreditCardService _processCreditCardService;
        //------------------------------------------------------------------------------------ 
        public ProcessCreditCardController(IOptions<FwApplicationConfig> appConfig, IProcessCreditCardService processCreditCardService) : base(appConfig) 
        {
            this.logicType = typeof(ProcessCreditCardLogic);
            this._processCreditCardService = processCreditCardService;
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
        [HttpPost("processcreditcard")]
        [FwControllerMethod(Id: "pvc2YoVG316N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ProcessCreditCardResponse>> ProcessCreditCardAsync([FromBody]ProcessCreditCardRequest request) 
        {
            await Task.CompletedTask;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcessCreditCardLogic logic = FwBusinessLogic.CreateBusinessLogic<ProcessCreditCardLogic>(this.AppConfig, this.UserSession);
                logic.ProcessCreditCardService = this._processCreditCardService;
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
