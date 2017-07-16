using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class BillingCycleController : RwDataController
    {
        public BillingCycleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<BillingCycleLogic>(pageno, pagesize, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<BillingCycleLogic>(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle
        [HttpPost]
        public IActionResult Post([FromBody]BillingCycleLogic l)
        {
            return doPost<BillingCycleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycle/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}