using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.Control
{
    [Route("api/v1/[controller]")]
    public class ControlController : RwDataController
    {
        public ControlController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/control/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/control 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ControlLogic>(pageno, pagesize, sort, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/control/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ControlLogic>(id, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/control 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ControlLogic l)
        {
            return await DoPostAsync<ControlLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/control 
        //[HttpPost("saveform")] 
        //public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
        //{ 
        //    return await DoSaveFormAsync<ControlLogic>(request, typeof(ControlLogic)); 
        //} 
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/control/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/control/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}