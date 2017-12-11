using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleRating
{
    [Route("api/v1/[controller]")]
    public class VehicleRatingController : AppDataController
    {
        public VehicleRatingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclerating/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclerating
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleRatingLogic>(pageno, pagesize, sort, typeof(VehicleRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclerating/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleRatingLogic>(id, typeof(VehicleRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclerating
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleRatingLogic l)
        {
            return await DoPostAsync<VehicleRatingLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclerating/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclerating/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}