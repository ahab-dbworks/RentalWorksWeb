using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DealType
{
    [Route("api/v1/[controller]")]
    public class DealTypeController : AppDataController
    {
        public DealTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{9B31A9CF-F852-45F4-9944-4AE386C826C7}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype
        [HttpGet]
        [Authorize(Policy = "{9862D27F-0B5C-4399-A238-DD306EC7C39C}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealTypeLogic>(pageno, pagesize, sort, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{7B6498F1-EC58-4627-9E71-67C689FB37A8}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealTypeLogic>(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype
        [HttpPost]
        [Authorize(Policy = "{044DC0AB-B54A-4A29-A784-648E341BDC06}")]
        public async Task<IActionResult> Post([FromBody]DealTypeLogic l)
        {
            return await DoPostAsync<DealTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealtype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{B9BFE8F0-BB31-40A3-9DAC-A38C4CA65F30}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{43FCAEE3-57AF-440B-9BF2-BE36E13D9356}")]
        public async Task<IActionResult> ValidateDuplicate([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}