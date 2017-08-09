using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.VehicleScheduleStatus
{
    [Route("api/v1/[controller]")]
    public class VehicleScheduleStatusController : RwDataController
    {
        public VehicleScheduleStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(pageno, pagesize, sort, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicleschedulestatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleScheduleStatusLogic>(id, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VehicleScheduleStatusLogic l)
        {
            return await DoPostAsync<VehicleScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicleschedulestatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VehicleScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicleschedulestatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}