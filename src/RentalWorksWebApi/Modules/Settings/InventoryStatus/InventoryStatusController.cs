using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryStatus
{
    [Route("api/v1/[controller]")]
    public class InventoryStatusController : AppDataController
    {
        public InventoryStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryStatusLogic>(pageno, pagesize, sort, typeof(InventoryStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorystatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryStatusLogic>(id, typeof(InventoryStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryStatusLogic l)
        {
            return await DoPostAsync<InventoryStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorystatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorystatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}