using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Home.PickList;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApi.Modules.Home.PickListUtilityItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PickListUtilityItemController : AppDataController
    {
        public PickListUtilityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListUtilityItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListUtilityItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PickListUtilityItemLogic l)
        {
            return await DoPostAsync<PickListUtilityItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/selectall
        [HttpPost("selectall")]
        public async Task<IActionResult> SelectAll([FromBody]BrowseRequest browseRequest)
        {
            IDictionary<string, object> miscfields = ((IDictionary<string, object>)browseRequest.miscfields);
            miscfields.Add("SelectAll", true);
            browseRequest.miscfields = miscfields;
            return await DoBrowseAsync(browseRequest, typeof(PickListUtilityItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/selectnone
        [HttpPost("selectnone")]
        public async Task<IActionResult> SelectNone([FromBody]BrowseRequest browseRequest)
        {
            IDictionary<string, object> miscfields = ((IDictionary<string, object>)browseRequest.miscfields);
            miscfields.Add("SelectNone", true);
            browseRequest.miscfields = miscfields;
            return await DoBrowseAsync(browseRequest, typeof(PickListUtilityItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/createpicklist
        [HttpPost("createpicklist")]
        public async Task<IActionResult> CreatePickList([FromBody]BrowseRequest browseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PickListLogic l = new PickListLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                bool b = await l.LoadFromSession(browseRequest);
                return new OkObjectResult(l);
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
