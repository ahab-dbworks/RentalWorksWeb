using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.Vendor
{
    [Route("api/v1/[controller]")]
    public class VendorController : RwDataController
    {
        public VendorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<VendorLogic>(pageno, pagesize, sort, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<VendorLogic>(id, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorLogic l)
        {
            return await DoPostAsync<VendorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendor/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}