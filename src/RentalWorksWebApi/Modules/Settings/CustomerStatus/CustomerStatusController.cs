using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.CustomerStatus
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "v1")]
    public class CustomerStatusController : AppDataController
    {
        public CustomerStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        /// <summary>
        /// Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieved records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpPost("browse")]
        [Authorize(Policy = "{33F721F5-0D91-464C-AFA7-FA46622CE3C0}")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        /// <summary>
        /// REST: Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieved records.</response>
        /// <response code="401">Unauthorized - A valid JWT Bearer Token must be provided in the header.</response>
        /// <response code="403">Forbidden - User is authenticated but does not have permission to access this resource.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Authorize(Policy = "{D1CECF78-CC21-4B3C-8F16-11D31B996032}")]
        [Produces(typeof(List<CustomerStatusLogic>))]
        [SwaggerResponse(200, Type = typeof(List<CustomerStatusLogic>))]
        [SwaggerResponse(401, Type = typeof(string))]
        [SwaggerResponse(403, Type = typeof(string))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerStatusLogic>(pageno, pagesize, sort, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{FF697F23-9150-4252-8C58-A0063419B88E}")]
        [Produces(typeof(CustomerStatusLogic))]
        [SwaggerResponse(200, Type = typeof(CustomerStatusLogic))]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerStatusLogic>(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        [Authorize(Policy = "{0178C657-4473-4889-945A-A2F88B3D31C0}")]
        public async Task<IActionResult> PostAsync([FromBody]CustomerStatusLogic l)
        {
            return await DoPostAsync<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{CDA85B7B-F766-410C-9B8E-D0DEFA313341}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{D212CE16-E6D8-4EBE-8ACD-4F9C086E1F03}")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}