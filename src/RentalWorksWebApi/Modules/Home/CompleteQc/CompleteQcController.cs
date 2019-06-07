using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.CompleteQc
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
        public string Note { get; set; }
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
        [FwControllerMethod(Id: "xzYWo1xclDpJ")]
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/completeqc/updateqcitem
        [HttpPost("updateqcitem")]
        [FwControllerMethod(Id: "ge5Y42NIEBGE")]
        public async Task<ActionResult<UpdateQcItemResponse>> StageItem([FromBody]UpdateQcItemRequest request)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
