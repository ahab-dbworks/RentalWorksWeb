using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.FacilityStatus
{
    [Route("api/v1/[controller]")]
    public class FacilityStatusController : RwDataController
    {
        public FacilityStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityStatusLogic>(pageno, pagesize, sort, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityStatusLogic>(id, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FacilityStatusLogic l)
        {
            return await DoPostAsync<FacilityStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitystatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}