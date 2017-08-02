using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.DealType
{
    [Route("api/v1/[controller]")]
    public class DealTypeController : RwDataController
    {
        public DealTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealTypeLogic>(pageno, pagesize, sort, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealTypeLogic>(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DealTypeLogic l)
        {
            return await DoPostAsync<DealTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealtype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicate([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}