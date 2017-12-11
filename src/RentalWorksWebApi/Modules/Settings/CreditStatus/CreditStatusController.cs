using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CreditStatus
{
    [Route("api/v1/[controller]")]
    public class CreditStatusController : AppDataController
    {
        public CreditStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CreditStatusLogic>(pageno, pagesize, sort, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CreditStatusLogic>(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]CreditStatusLogic l)
        {
            return await DoPostAsync<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/creditstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}