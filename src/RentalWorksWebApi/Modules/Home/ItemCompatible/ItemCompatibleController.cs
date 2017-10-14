using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.ItemCompatible
{
    [Route("api/v1/[controller]")]
    public class ItemCompatibleController : RwDataController
    {
        public ItemCompatibleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemcompatible/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemcompatible 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemCompatibleLogic>(pageno, pagesize, sort, typeof(ItemCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemcompatible/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemCompatibleLogic>(id, typeof(ItemCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemcompatible 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemCompatibleLogic l)
        {
            return await DoPostAsync<ItemCompatibleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemcompatible
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<ItemCompatibleLogic>(request, typeof(ItemCompatibleLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/itemcompatible/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemcompatible/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}