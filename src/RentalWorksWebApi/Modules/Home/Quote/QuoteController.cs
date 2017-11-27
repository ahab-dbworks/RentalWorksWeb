using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.Quote
{
    [Route("api/v1/[controller]")]
    public class QuoteController : RwDataController
    {
        public QuoteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuoteLogic>(pageno, pagesize, sort, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuoteLogic>(id, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]QuoteLogic l)
        {
            return await DoPostAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/quote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}