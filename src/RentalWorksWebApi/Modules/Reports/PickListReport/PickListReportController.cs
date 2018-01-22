using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.PickListReport
{
    [Route("api/v1/[controller]")]
    public class PickListReportController : AppDataController
    {
        public PickListReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListReportLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/picklistreport 
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<PickListReportLogic>(pageno, pagesize, sort, typeof(PickListReportLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/picklistreport/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<PickListReportLogic>(id, typeof(PickListReportLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/picklistreport 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]PickListReportLogic l)
        //{
        //    return await DoPostAsync<PickListReportLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/picklistreport/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(PickListReportLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/picklistreport/validateduplicate 
        //[HttpPost("validateduplicate")]
        //public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        //{
        //    return await DoValidateDuplicateAsync(request);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}