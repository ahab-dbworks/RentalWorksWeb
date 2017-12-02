using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.TermsConditions
{
    [Route("api/v1/[controller]")]
    public class TermsConditionsController : RwDataController
    {
        public TermsConditionsController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/termsconditions/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(TermsConditionsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/termsconditions
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TermsConditionsLogic>(pageno, pagesize, sort, typeof(TermsConditionsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/termsconditions/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<TermsConditionsLogic>(id, typeof(TermsConditionsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/termsconditions
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TermsConditionsLogic l)
        {
            return await DoPostAsync<TermsConditionsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/termsconditions/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(TermsConditionsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/termsconditions/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}