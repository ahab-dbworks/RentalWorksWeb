using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class VendorClassController : RwDataController
    {
        public VendorClassController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<VendorClassLogic>(pageno, pagesize, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<VendorClassLogic>(id, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorClassLogic l)
        {
            return await DoPostAsync<VendorClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorclass/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}