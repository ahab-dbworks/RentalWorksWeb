using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.DealStatus
{
    [Route("api/v1/[controller]")]
    public class DealStatusController : RwDataController
    {
        public DealStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{051A9D01-5B55-4805-931A-75937FA04F33}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus
        [HttpGet]
        [Authorize(Policy = "{A17E8E69-1427-472E-9F15-EA4E31590ABE}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealStatusLogic>(pageno, pagesize, sort, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{A8FFA28F-E260-4B28-BB88-B4A5C2F0745B}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealStatusLogic>(id, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus
        [HttpPost]
        [Authorize(Policy = "{C0DAAFEF-B169-4153-A6D4-C3B6D3C07F94}")]
        public async Task<IActionResult> PostAsync([FromBody]DealStatusLogic l)
        {
            return await DoPostAsync<DealStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{0C9DE590-155F-459E-B33E-90AEABFB5F50}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{27EDAA20-C1E1-4687-9A17-32D7E812A17E}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}