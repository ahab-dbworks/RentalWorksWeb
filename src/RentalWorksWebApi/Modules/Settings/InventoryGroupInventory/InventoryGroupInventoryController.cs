using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.InventoryGroupInventory
{
    [Route("api/v1/[controller]")]
    public class InventoryGroupInventoryController : RwDataController
    {
        public InventoryGroupInventoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(pageno, pagesize, sort, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroupinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupInventoryLogic>(id, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryGroupInventoryLogic l)
        {
            return await DoPostAsync<InventoryGroupInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory 
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<InventoryGroupInventoryLogic>(request, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroupinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryGroupInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroupinventory/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}