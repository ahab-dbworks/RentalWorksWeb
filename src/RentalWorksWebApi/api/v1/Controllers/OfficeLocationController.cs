using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class OfficeLocationController : RwDataController
    {
        public OfficeLocationController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/Location/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OfficeLocationLogic
                ));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Location
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<OfficeLocationLogic>(pageno, pagesize, typeof(OfficeLocationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Location/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<OfficeLocationLogic>(id, typeof(OfficeLocationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Location
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OfficeLocationLogic l)
        {
            return await DoPostAsync<OfficeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/Location/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(OfficeLocationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Location/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}