using FwStandard.AppManager;
using FwStandard.SqlServer;
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
    [FwController(Id: "DOnlknWuWfYS")]
    public class PickListUtilityItemController : AppDataController
    {
        public PickListUtilityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListUtilityItemLogic); return404IfGetNotFound = false; }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "odmnB0Sdaau0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "O3BNoaLPlQXn")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem 
        [HttpPost]
        [FwControllerMethod(Id: "DklEUpjtmhxA")]
        public async Task<ActionResult<PickListUtilityItemLogic>> PostAsync([FromBody]PickListUtilityItemLogic l)
        {
            return await DoPostAsync<PickListUtilityItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "7spGz1CdWYwR")]
        public async Task<ActionResult<FwJsonDataTable>> SelectAll([FromBody]BrowseRequest browseRequest)
        {
            IDictionary<string, object> miscfields = ((IDictionary<string, object>)browseRequest.miscfields);
            miscfields.Add("SelectAll", true);
            browseRequest.miscfields = miscfields;
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "GTeOy9BAsQOD")]
        public async Task<ActionResult<FwJsonDataTable>> SelectNone([FromBody]BrowseRequest browseRequest)
        {
            IDictionary<string, object> miscfields = ((IDictionary<string, object>)browseRequest.miscfields);
            miscfields.Add("SelectNone", true);
            browseRequest.miscfields = miscfields;
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/createpicklist
        [HttpPost("createpicklist")]
        [FwControllerMethod(Id: "LzGhicy7Mgje")]
        public async Task<ActionResult<PickListLogic>> CreatePickList([FromBody]BrowseRequest browseRequest)
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
