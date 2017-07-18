using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PaymentTermsController : RwDataController
    {
        public PaymentTermsController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<PaymentTermsLogic>(pageno, pagesize, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<PaymentTermsLogic>(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PaymentTermsLogic l)
        {
            return await DoPostAsync<PaymentTermsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymentterms/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}