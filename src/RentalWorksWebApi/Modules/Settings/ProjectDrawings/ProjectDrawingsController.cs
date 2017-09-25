using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.ProjectDrawings
{
    [Route("api/v1/[controller]")]
    public class ProjectDrawingsController : RwDataController
    {
        public ProjectDrawingsController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(pageno, pagesize, sort, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(id, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectDrawingsLogic l)
        {
            return await DoPostAsync<ProjectDrawingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdrawings/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectDrawingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}