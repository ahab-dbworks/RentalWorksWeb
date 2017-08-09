using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.FacilityScheduleStatus
{
    [Route("api/v1/[controller]")]
    public class FacilityScheduleStatusController : RwDataController
    {
        public FacilityScheduleStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(pageno, pagesize, sort, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(id, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FacilityScheduleStatusLogic l)
        {
            return await DoPostAsync<FacilityScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilityschedulestatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}