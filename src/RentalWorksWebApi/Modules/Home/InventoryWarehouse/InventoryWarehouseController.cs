using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.InventoryWarehouse
{
    [Route("api/v1/[controller]")]
    public class InventoryWarehouseController : RwDataController
    {
        public InventoryWarehouseController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(pageno, pagesize, sort, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(id, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryWarehouseLogic l)
        {
            return await DoPostAsync<InventoryWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorywarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}