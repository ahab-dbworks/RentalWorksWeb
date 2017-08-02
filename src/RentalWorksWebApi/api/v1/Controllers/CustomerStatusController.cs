using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.api.v1.Models;
using RentalWorksWebLogic.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CustomerStatusController : RwDataController
    {
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        /// <summary>
        /// Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpPost("browse")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        /// <summary>
        /// REST: Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<CustomerStatusLogic>))]
        [SwaggerResponse(200, Type = typeof(List<CustomerStatusLogic>))]
        [SwaggerResponse(500, Type = typeof(ApiException))]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerStatusLogic>(pageno, pagesize, sort, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(CustomerStatusLogic))]
        [SwaggerResponse(200, Type = typeof(CustomerStatusLogic))]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerStatusLogic>(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerStatusLogic l)
        {
            return await DoPostAsync<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}