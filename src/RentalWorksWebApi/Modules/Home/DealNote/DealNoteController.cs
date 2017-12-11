using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.DealNote
{
    [Route("api/v1/[controller]")]
    public class DealNoteController : AppDataController
    {
        public DealNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealNoteLogic>(pageno, pagesize, sort, typeof(DealNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealnote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealNoteLogic>(id, typeof(DealNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DealNoteLogic l)
        {
            return await DoPostAsync<DealNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealnote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealnote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}