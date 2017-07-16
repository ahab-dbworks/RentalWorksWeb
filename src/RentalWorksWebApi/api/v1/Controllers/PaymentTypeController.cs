using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PaymentTypeController : RwDataController
    {
        public PaymentTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<PaymentTypeLogic>(pageno, pagesize, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<PaymentTypeLogic>(id, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PaymentTypeLogic l)
        {
            return await DoPostAsync<PaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymenttype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}