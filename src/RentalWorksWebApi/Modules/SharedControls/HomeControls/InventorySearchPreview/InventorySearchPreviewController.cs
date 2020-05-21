using FwStandard.AppManager;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.HomeControls.InventorySearchPreview
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"JLDAuUcvHEx1")]
    public class InventorySearchPreviewController : AppDataController
    {

        //public class InventorySearchPreviewBrowseRequest : BrowseRequest
        //{
        //    public string SessionId;
        //    public bool? ShowAvailability;
        //    public DateTime? FromDate;
        //    public DateTime? ToDate;
        //}
        ////------------------------------------------------------------------------------------ 


        public InventorySearchPreviewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySearchPreviewLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview/browse 
        //[HttpPost("browse")]
        //[FwControllerMethod(Id:"3WWbSkyMDadG", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]InventorySearchPreviewBrowseRequest browseRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        InventorySearchPreviewLogic l = new InventorySearchPreviewLogic();
        //        l.SetDependencies(this.AppConfig, this.UserSession);
        //        FwJsonDataTable dt = await l.PreviewAsync(browseRequest);
        //        return new OkObjectResult(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "3WWbSkyMDadG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview 
        [HttpPost]
        [FwControllerMethod(Id:"dGL92Uyu642J", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventorySearchPreviewLogic>> NewAsync([FromBody]InventorySearchPreviewLogic l)
        {
            return await DoNewAsync<InventorySearchPreviewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorysearchpreview/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "TqhqtvJrI4cvY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventorySearchPreviewLogic>> EditAsync([FromRoute] string id, [FromBody]InventorySearchPreviewLogic l)
        {
            return await DoEditAsync<InventorySearchPreviewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorysearchpreview/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OkVc9qhIP2sl", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventorySearchPreviewLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
