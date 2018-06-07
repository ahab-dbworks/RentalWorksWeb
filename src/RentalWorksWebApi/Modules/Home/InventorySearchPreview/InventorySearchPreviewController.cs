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
    public class InventorySearchPreviewController : AppDataController
    {

        public class InventorySearchPreviewBrowseRequest : BrowseRequest
        {
            public string SessionId;
            public bool ShowAvailability;
            public DateTime FromDate;
            public DateTime ToDate;
        }
        //------------------------------------------------------------------------------------ 


        public InventorySearchPreviewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySearchPreviewLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]InventorySearchPreviewBrowseRequest browseRequest)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/inventorysearchpreview 
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<InventorySearchPreviewLogic>(pageno, pagesize, sort, typeof(InventorySearchPreviewLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/inventorysearchpreview/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<InventorySearchPreviewLogic>(id, typeof(InventorySearchPreviewLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearchpreview 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventorySearchPreviewLogic l)
        {
            return await DoPostAsync<InventorySearchPreviewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorysearchpreview/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventorySearchPreviewLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}