using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PickList
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PickListController : AppDataController
    {
        public PickListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListLogic>(pageno, pagesize, sort, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListLogic>(id, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PickListLogic l)
        {
            return await DoPostAsync<PickListLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklist/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}