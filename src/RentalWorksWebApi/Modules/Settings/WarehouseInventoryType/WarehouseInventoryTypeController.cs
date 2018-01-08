using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseInventoryType
{
    [Route("api/v1/[controller]")]
    public class WarehouseInventoryTypeController : AppDataController
    {
        public WarehouseInventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinventorytype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseInventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseinventorytype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseInventoryTypeLogic>(pageno, pagesize, sort, typeof(WarehouseInventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseinventorytype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseInventoryTypeLogic>(id, typeof(WarehouseInventoryTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinventorytype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseInventoryTypeLogic l)
        {
            return await DoPostAsync<WarehouseInventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/warehouseinventorytype/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(WarehouseInventoryTypeLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinventorytype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}