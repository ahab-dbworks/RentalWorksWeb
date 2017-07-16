using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class DealTypeController : RwDataController
    {
        public DealTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<DealTypeLogic>(pageno, pagesize, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<DealTypeLogic>(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DealTypeLogic l)
        {
            return await DoPostAsync<DealTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealtype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await DoDeleteAsync(id, typeof(DealTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}