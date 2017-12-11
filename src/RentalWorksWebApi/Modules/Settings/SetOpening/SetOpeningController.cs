using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetOpening
{
    [Route("api/v1/[controller]")]
    public class SetOpeningController : AppDataController
    {
        public SetOpeningController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetOpeningLogic>(pageno, pagesize, sort, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetOpeningLogic>(id, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SetOpeningLogic l)
        {
            return await DoPostAsync<SetOpeningLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setopening/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}