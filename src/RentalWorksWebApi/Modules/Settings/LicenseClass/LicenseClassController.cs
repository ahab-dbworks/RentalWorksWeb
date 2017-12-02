using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.LicenseClass
{
    [Route("api/v1/[controller]")]
    public class LicenseClassController : RwDataController
    {
        public LicenseClassController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LicenseClassLogic>(pageno, pagesize, sort, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<LicenseClassLogic>(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]LicenseClassLogic l)
        {
            return await DoPostAsync<LicenseClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/licenseclass/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}