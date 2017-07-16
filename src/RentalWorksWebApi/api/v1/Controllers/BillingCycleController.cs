using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class BillingCycleController : RwDataController
    {
        public BillingCycleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<BillingCycleLogic>(pageno, pagesize, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<BillingCycleLogic>(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BillingCycleLogic l)
        {
            return await DoPostAsync<BillingCycleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycle/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}