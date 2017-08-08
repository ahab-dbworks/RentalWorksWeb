using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.RentalStatus
{
    [Route("api/v1/[controller]")]
    public class RentalStatusController : RwDataController
    {
        public RentalStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalstatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RentalStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalstatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalStatusLogic>(pageno, pagesize, sort, typeof(RentalStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalstatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalStatusLogic>(id, typeof(RentalStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RentalStatusLogic l)
        {
            return await DoPostAsync<RentalStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/rentalstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RentalStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}