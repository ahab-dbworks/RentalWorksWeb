using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CreditStatus
{
    [Route("api/v1/[controller]")]
    public class CreditStatusController : RwDataController
    {
        public CreditStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CreditStatusLogic>(pageno, pagesize, sort, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CreditStatusLogic>(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CreditStatusLogic l)
        {
            return await DoPostAsync<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/creditstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}