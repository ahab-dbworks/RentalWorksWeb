using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.ProjectDropShipItems
{
    [Route("api/v1/[controller]")]
    public class ProjectDropShipItemsController : RwDataController
    {
        public ProjectDropShipItemsController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(pageno, pagesize, sort, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(id, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProjectDropShipItemsLogic l)
        {
            return await DoPostAsync<ProjectDropShipItemsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdropshipitems/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}