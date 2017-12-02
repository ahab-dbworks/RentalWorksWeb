using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.VehicleType
{
    [Route("api/v1/[controller]")]
    public class VehicleTypeController : RwDataController
    {
        public VehicleTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeLogic>(pageno, pagesize, sort, typeof(VehicleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeLogic>(id, typeof(VehicleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleTypeLogic l)
        {
            return await DoPostAsync<VehicleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}