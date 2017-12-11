using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscCategory
{
    [Route("api/v1/[controller]")]
    public class MiscCategoryController : AppDataController
    {
        public MiscCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscCategoryLogic>(pageno, pagesize, sort, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscCategoryLogic>(id, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MiscCategoryLogic l)
        {
            return await DoPostAsync<MiscCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misccategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}