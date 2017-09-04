using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.InventoryCondition
{
    [Route("api/v1/[controller]")]
    public class InventoryConditionController : RwDataController
    {
        public InventoryConditionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycondition/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryConditionLogic>(pageno, pagesize, sort, typeof(InventoryConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryConditionLogic>(id, typeof(InventoryConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]InventoryConditionLogic l)
        {
            return await DoPostAsync<InventoryConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorycondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycondition/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) => await DoValidateDuplicateAsync(request);
        //------------------------------------------------------------------------------------
    }
}