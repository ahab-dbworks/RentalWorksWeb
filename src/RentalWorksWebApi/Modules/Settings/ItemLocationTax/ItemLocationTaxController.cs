using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.ItemLocationTax
{
    [Route("api/v1/[controller]")]
    public class ItemLocationTaxController : RwDataController
    {
        public ItemLocationTaxController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemlocationtax/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemlocationtax 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemLocationTaxLogic>(pageno, pagesize, sort, typeof(ItemLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemlocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemLocationTaxLogic>(id, typeof(ItemLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemlocationtax 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemLocationTaxLogic l)
        {
            return await DoPostAsync<ItemLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemlocationtax
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<ItemLocationTaxLogic>(request, typeof(ItemLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/itemlocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemlocationtax/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}