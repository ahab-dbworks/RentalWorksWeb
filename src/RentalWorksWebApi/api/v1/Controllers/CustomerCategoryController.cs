using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerCategoryController : RwDataController
    {
        public CustomerCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<CustomerCategoryLogic>(pageno, pagesize, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<CustomerCategoryLogic>(id, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerCategoryLogic l)
        {
            return await DoPostAsync<CustomerCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/CustomerCategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}