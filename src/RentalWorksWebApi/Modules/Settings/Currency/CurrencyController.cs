using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.Currency
{
    [Route("api/v1/[controller]")]
    public class CurrencyController : RwDataController
    {
        public CurrencyController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/currency/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CurrencyLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/currency
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CurrencyLogic>(pageno, pagesize, sort, typeof(CurrencyLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/currency/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<CurrencyLogic>(id, typeof(CurrencyLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/currency
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CurrencyLogic l)
        {
            return await DoPostAsync<CurrencyLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/currency/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(CurrencyLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/currency/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}