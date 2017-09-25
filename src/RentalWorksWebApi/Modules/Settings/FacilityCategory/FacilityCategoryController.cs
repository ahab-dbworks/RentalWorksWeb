using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.FacilityCategory
{
    [Route("api/v1/[controller]")]
    public class FacilityCategoryController : RwDataController
    {
        public FacilityCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityCategoryLogic>(pageno, pagesize, sort, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityCategoryLogic>(id, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FacilityCategoryLogic l)
        {
            return await DoPostAsync<FacilityCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitycategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}