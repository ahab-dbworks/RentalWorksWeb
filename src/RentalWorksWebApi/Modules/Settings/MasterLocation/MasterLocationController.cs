using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.MasterLocation
{
    [Route("api/v1/[controller]")]
    public class MasterLocationController : RwDataController
    {
        public MasterLocationController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/masterlocation/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MasterLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/masterlocation 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MasterLocationLogic>(pageno, pagesize, sort, typeof(MasterLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/masterlocation/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MasterLocationLogic>(id, typeof(MasterLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/masterlocation 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MasterLocationLogic l)
        {
            return await DoPostAsync<MasterLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/masterlocation
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<MasterLocationLogic>(request, typeof(MasterLocationLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/masterlocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MasterLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/masterlocation/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}