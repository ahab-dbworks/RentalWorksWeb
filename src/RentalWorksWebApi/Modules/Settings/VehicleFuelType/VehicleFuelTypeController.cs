using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.VehicleFuelType
{
    [Route("api/v1/[controller]")]
    public class VehicleFuelTypeController : RwDataController
    {
        public VehicleFuelTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclefueltype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclefueltype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleFuelTypeLogic>(pageno, pagesize, sort, typeof(VehicleFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclefueltype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleFuelTypeLogic>(id, typeof(VehicleFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclefueltype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleFuelTypeLogic l)
        {
            return await DoPostAsync<VehicleFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclefueltype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclefueltype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}