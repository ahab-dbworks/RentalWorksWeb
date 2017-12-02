using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.RentalCategory
{
    [Route("api/v1/[controller]")]
    public class RentalCategoryController : RwDataController
    {
        public RentalCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalCategoryLogic>(pageno, pagesize, sort, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalCategoryLogic>(id, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RentalCategoryLogic l)
        {
            return await DoPostAsync<RentalCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/rentalcategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RentalCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}