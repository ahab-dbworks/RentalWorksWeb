using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.VehicleModel
{
    [Route("api/v1/[controller]")]
    public class VehicleModelController : RwDataController
    {
        public VehicleModelController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemodel/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleModelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemodel
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleModelLogic>(pageno, pagesize, sort, typeof(VehicleModelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemodel/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleModelLogic>(id, typeof(VehicleModelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemodel
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleModelLogic l)
        {
            return await DoPostAsync<VehicleModelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclemodel/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleModelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemodel/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}