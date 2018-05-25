using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Space
{
    [Route("api/v1/[controller]")]
    public class SpaceController : AppDataController
    {
        public SpaceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceLogic>(pageno, pagesize, sort, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceLogic>(id, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SpaceLogic l)
        {
            return await DoPostAsync<SpaceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/space/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}