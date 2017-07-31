using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PoClassificationController : RwDataController
    {
        public PoClassificationController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<PoClassificationLogic>(pageno, pagesize, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<PoClassificationLogic>(id, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoClassificationLogic l)
        {
            return await DoPostAsync<PoClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poclassification/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(PoClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}