using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.PropsCondition
{
    [Route("api/v1/[controller]")]
    public class PropsConditionController : RwDataController
    {
        public PropsConditionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PropsConditionLogic>(pageno, pagesize, sort, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PropsConditionLogic>(id, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]PropsConditionLogic l)
        {
            return await DoPostAsync<PropsConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/propscondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) => await DoValidateDuplicateAsync(request);
        //------------------------------------------------------------------------------------
    }
}