using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.BarCodeRange
{
    [Route("api/v1/[controller]")]
    public class BarCodeRangeController : RwDataController
    {
        public BarCodeRangeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BarCodeRangeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BarCodeRangeLogic>(pageno, pagesize, sort, typeof(BarCodeRangeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<BarCodeRangeLogic>(id, typeof(BarCodeRangeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BarCodeRangeLogic l)
        {
            return await DoPostAsync<BarCodeRangeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/barcoderange/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BarCodeRangeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}