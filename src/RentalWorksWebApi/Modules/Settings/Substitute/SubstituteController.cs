using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.Substitute
{
    [Route("api/v1/[controller]")]
    public class SubstituteController : RwDataController
    {
        public SubstituteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/substitute/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/substitute 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SubstituteLogic>(pageno, pagesize, sort, typeof(SubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/substitute/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubstituteLogic>(id, typeof(SubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/substitute 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SubstituteLogic l)
        {
            return await DoPostAsync<SubstituteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/substitute
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<SubstituteLogic>(request, typeof(SubstituteLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/substitute/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/substitute/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}