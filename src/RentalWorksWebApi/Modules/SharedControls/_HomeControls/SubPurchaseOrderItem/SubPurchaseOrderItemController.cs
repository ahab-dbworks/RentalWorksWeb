using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.HomeControls.SubPurchaseOrderItem
{


    public class SelectAllNonePoWorksheetItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNonePoWorksheetItemResponse : TSpStatusResponse
    {
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"8orfHWAhottty")]
    public class SubPurchaseOrderItemController : AppDataController
    {
        public SubPurchaseOrderItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SubPurchaseOrderItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Tr79QWBbuTtqg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Vu5rkrfYymlxu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/subpurchaseorderitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"btVJZXfQs4fyC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<SubPurchaseOrderItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubPurchaseOrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem 
        [HttpPost]
        [FwControllerMethod(Id:"9rlJdMvepp8jr", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SubPurchaseOrderItemLogic>> NewAsync([FromBody]SubPurchaseOrderItemLogic l)
        {
            return await DoNewAsync<SubPurchaseOrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/subpurchaseorderitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "0YnX1QJTGA4qU", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SubPurchaseOrderItemLogic>> EditAsync([FromRoute] string id, [FromBody]SubPurchaseOrderItemLogic l)
        {
            return await DoEditAsync<SubPurchaseOrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "cAECvO0xOlKfG")]
        public async Task<List<ActionResult<SubPurchaseOrderItemLogic>>> PostAsync([FromBody]List<SubPurchaseOrderItemLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<SubPurchaseOrderItemLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/subpurchaseorderitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Bhu77r9vju2F3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SubPurchaseOrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id:"H4js7q9cNOXa", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNonePoWorksheetItemResponse>> SelectAll([FromBody] SelectAllNonePoWorksheetItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNonePoWorksheetItemResponse response = await OrderFunc.SelectAllPoWorksheetItem(AppConfig, UserSession, request.SessionId);
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
        // POST api/v1/subpurchaseorderitem/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id:"RscMcG0CkiBqJ", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNonePoWorksheetItemResponse>> SelectNone([FromBody] SelectAllNonePoWorksheetItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNonePoWorksheetItemResponse response = await OrderFunc.SelectNonePoWorksheetItem(AppConfig, UserSession, request.SessionId);
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
