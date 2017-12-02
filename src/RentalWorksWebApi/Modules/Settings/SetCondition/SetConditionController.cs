using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.SetCondition
{
    [Route("api/v1/[controller]")]
    public class SetConditionController : RwDataController
    {
        public SetConditionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SetConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetConditionLogic>(pageno, pagesize, sort, typeof(SetConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setscondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetConditionLogic>(id, typeof(SetConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]SetConditionLogic l)
        {
            return await DoPostAsync<SetConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setscondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SetConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setscondition/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) => await DoValidateDuplicateAsync(request);
        //------------------------------------------------------------------------------------
    }
}