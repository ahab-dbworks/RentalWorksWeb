using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CrewStatus
{
    [Route("api/v1/[controller]")]
    public class CrewStatusController : RwDataController
    {
        public CrewStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewstatus
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewStatusLogic>(pageno, pagesize, sort, typeof(CrewStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewStatusLogic>(id, typeof(CrewStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]CrewStatusLogic l)
        {
            return await DoPostAsync<CrewStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}