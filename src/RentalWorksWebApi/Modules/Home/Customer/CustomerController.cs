using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.Customer
{
    [Route("api/v1/[controller]")]
    public class CustomerController : RwDataController
    {
        public CustomerController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CustomerLogic>(pageno, pagesize, sort, typeof(CustomerLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<CustomerLogic>(id, typeof(CustomerLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerLogic l)
        {
            return await DoPostAsync<CustomerLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customer/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}