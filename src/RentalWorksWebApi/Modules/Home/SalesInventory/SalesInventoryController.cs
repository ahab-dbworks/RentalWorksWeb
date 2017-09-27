using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.SalesInventory
{
    [Route("api/v1/[controller]")]
    public class SalesInventoryController : RwDataController
    {
        public SalesInventoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SalesInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesInventoryLogic>(pageno, pagesize, sort, typeof(SalesInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/salesinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesInventoryLogic>(id, typeof(SalesInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SalesInventoryLogic l)
        {
            return await DoPostAsync<SalesInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/salesinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SalesInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventory/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
