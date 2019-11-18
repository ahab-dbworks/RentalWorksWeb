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

namespace WebApi.Modules.Home.InventorySearchPreview
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"JLDAuUcvHEx1")]
    public class InventorySearchPreviewController : AppDataController
    {

        public class InventorySearchPreviewBrowseRequest : BrowseRequest
        {
            public string SessionId;
            public bool? ShowAvailability;
            //public bool? RefreshAvailability;
            public DateTime? FromDate;
            public DateTime? ToDate;
        }
        //------------------------------------------------------------------------------------ 


        public InventorySearchPreviewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySearchPreviewLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"3WWbSkyMDadG")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]InventorySearchPreviewBrowseRequest browseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchPreviewLogic l = new InventorySearchPreviewLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.PreviewAsync(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview 
        [HttpPost]
        [FwControllerMethod(Id:"dGL92Uyu642J")]
        public async Task<ActionResult<InventorySearchPreviewLogic>> PostAsync([FromBody]InventorySearchPreviewLogic l)
        {
            return await DoPostAsync<InventorySearchPreviewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorysearchpreview/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OkVc9qhIP2sl")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventorySearchPreviewLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
