using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.DiscountReason
{
    [Route("api/v1/[controller]")]
    public class DiscountReasonController : RwDataController
    {
        public DiscountReasonController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountReasonLogic>(pageno, pagesize, sort, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/discountreason/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountReasonLogic>(id, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DiscountReasonLogic l)
        {
            return await DoPostAsync<DiscountReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/discountreason/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DiscountReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountreason/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}