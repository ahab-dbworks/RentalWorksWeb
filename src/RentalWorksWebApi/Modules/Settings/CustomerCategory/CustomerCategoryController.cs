using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CustomerCategory
{
    [Route("api/v1/[controller]")]
    public class CustomerCategoryController : RwDataController
    {
        public CustomerCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerCategoryLogic>(pageno, pagesize, sort, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerCategoryLogic>(id, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]CustomerCategoryLogic l)
        {
            return await DoPostAsync<CustomerCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/CustomerCategory/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}