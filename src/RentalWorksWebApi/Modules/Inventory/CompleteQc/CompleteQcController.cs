using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Inventory.CompleteQc
{

    public class CompleteQcItemRequest
    {
        public string Code { get; set; }
        public bool? QcAnyway { get; set; }
    }

    public class UpdateQcItemRequest
    {
        public string ItemId { get; set; }
        public string ItemQcId { get; set; }
        public string ConditionId { get; set; }
        public string CurrentFootCandles { get; set; }
        public string CurrentSoftwareVersion { get; set; }
        public string SoftwareEffectiveDate { get; set; }
        public string Note { get; set; }
        public int? AssetHours { get; set; }
        public int? Strikes { get; set; }
        public int? LampHours1 { get; set; }
        public int? LampHours2 { get; set; }
        public int? LampHours3 { get; set; }
        public int? LampHours4 { get; set; }
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "VwNYsEONLutM")]
    public class CompleteQcController : AppDataController
    {
        public CompleteQcController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }


        //------------------------------------------------------------------------------------ 
        // POST api/v1/completeqc/completeqcitem
        [HttpPost("completeqcitem")]
        [FwControllerMethod(Id: "xzYWo1xclDpJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CompleteQcItemResponse>> CompleteQcItem([FromBody]CompleteQcItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompleteQcItemResponse response = await CompleteQcFunc.CompleteQcItem(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/completeqc/updateqcitem
        [HttpPost("updateqcitem")]
        [FwControllerMethod(Id: "ge5Y42NIEBGE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<UpdateQcItemResponse>> UpdateQcItem([FromBody]UpdateQcItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UpdateQcItemResponse response = await CompleteQcFunc.UpdateQcItem(AppConfig, UserSession, request);
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
