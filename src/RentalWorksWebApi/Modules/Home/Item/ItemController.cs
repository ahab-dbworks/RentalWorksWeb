using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Item
{
    [Route("api/v1/[controller]")]
    public class ItemController : AppDataController
    {
        public ItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemLogic>(pageno, pagesize, sort, typeof(ItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemLogic>(id, typeof(ItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemLogic l)
        {
            return await DoPostAsync<ItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/item/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/item/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}