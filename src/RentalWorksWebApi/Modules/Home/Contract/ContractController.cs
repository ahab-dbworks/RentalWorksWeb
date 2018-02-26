using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Contract
{
    [Route("api/v1/[controller]")]
    public class ContractController : AppDataController
    {
        public ContractController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContractLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contract 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContractLogic>(pageno, pagesize, sort, typeof(ContractLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contract/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContractLogic>(id, typeof(ContractLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContractLogic l)
        {
            return await DoPostAsync<ContractLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/contract/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ContractLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
