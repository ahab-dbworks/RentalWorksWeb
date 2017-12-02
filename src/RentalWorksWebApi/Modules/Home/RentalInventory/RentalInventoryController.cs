using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.RentalInventory
{
    [Route("api/v1/[controller]")]
    public class RentalInventoryController : RwDataController
    {
        public RentalInventoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RentalInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalInventoryLogic>(pageno, pagesize, sort, typeof(RentalInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalInventoryLogic>(id, typeof(RentalInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RentalInventoryLogic l)
        {
            return await DoPostAsync<RentalInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/rentalinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RentalInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
