using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.SetSurface
{
    [Route("api/v1/[controller]")]
    public class SetSurfaceController : RwDataController
    {
        public SetSurfaceController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SetSurfaceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setsurface
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetSurfaceLogic>(pageno, pagesize, sort, typeof(SetSurfaceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setsurface/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetSurfaceLogic>(id, typeof(SetSurfaceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SetSurfaceLogic l)
        {
            return await DoPostAsync<SetSurfaceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setsurface/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SetSurfaceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}