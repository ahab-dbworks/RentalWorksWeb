using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryLocationTax
{
    [Route("api/v1/[controller]")]
    public class InventoryLocationTaxController : RwDataController
    {
        public InventoryLocationTaxController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(pageno, pagesize, sort, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryLocationTaxLogic l)
        {
            return await DoPostAsync<InventoryLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<InventoryLocationTaxLogic>(request, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorylocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}