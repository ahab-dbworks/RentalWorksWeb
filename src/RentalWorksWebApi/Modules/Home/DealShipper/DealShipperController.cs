using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.DealShipper
{
    [Route("api/v1/[controller]")]
    public class DealShipperController : RwDataController
    {
        public DealShipperController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealShipperLogic>(pageno, pagesize, sort, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealShipperLogic>(id, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DealShipperLogic l)
        {
            return await DoPostAsync<DealShipperLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<DealShipperLogic>(request, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealshipper/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}