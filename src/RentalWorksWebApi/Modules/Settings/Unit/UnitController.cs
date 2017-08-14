using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.Unit
{
    [Route("api/v1/[controller]")]
    public class UnitController : RwDataController
    {
        public UnitController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnitLogic>(pageno, pagesize, sort, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnitLogic>(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UnitLogic l)
        {
            return await DoPostAsync<UnitLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unit/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}