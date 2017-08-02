using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.EventCategory
{
    [Route("api/v1/[controller]")]
    public class EventCategoryController : RwDataController
    {
        public EventCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<EventCategoryLogic>(pageno, pagesize, sort, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/eventcategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<EventCategoryLogic>(id, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]EventCategoryLogic l)
        {
            return await DoPostAsync<EventCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/eventcategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(EventCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/eventcategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}