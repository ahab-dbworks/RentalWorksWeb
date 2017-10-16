using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.InventoryRank
{
    [Route("api/v1/[controller]")]
    public class InventoryRankController : RwDataController
    {
        public InventoryRankController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryRankLogic>(pageno, pagesize, sort, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryrank/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryRankLogic>(id, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryRankLogic l)
        {
            return await DoPostAsync<InventoryRankLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryrank 
        //[HttpPost("saveform")] 
        //public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
        //{ 
        //    return await DoSaveFormAsync<InventoryRankLogic>(request, typeof(InventoryRankLogic)); 
        //} 
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryrank/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryRankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryrank/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}