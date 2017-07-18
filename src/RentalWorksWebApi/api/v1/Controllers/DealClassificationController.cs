using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class DealClassificationController : RwDataController
    {
        public DealClassificationController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<DealClassificationLogic>(pageno, pagesize, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<DealClassificationLogic>(id, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DealClassificationLogic l)
        {
            return await DoPostAsync<DealClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}