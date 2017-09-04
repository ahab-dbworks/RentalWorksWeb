using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.LaborType
{
    [Route("api/v1/[controller]")]
    public class LaborTypeController : RwDataController
    {
        public LaborTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborTypeLogic>(pageno, pagesize, sort, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborTypeLogic>(id, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]LaborTypeLogic l)
        {
            return await DoPostAsync<LaborTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/labortype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}