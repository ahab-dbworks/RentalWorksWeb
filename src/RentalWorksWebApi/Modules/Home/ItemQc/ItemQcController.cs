using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ItemQc
{
    [Route("api/v1/[controller]")]
    public class ItemQcController : AppDataController
    {
        public ItemQcController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemQcLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemQcLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemqc 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemQcLogic>(pageno, pagesize, sort, typeof(ItemQcLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemqc/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemQcLogic>(id, typeof(ItemQcLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemQcLogic l)
        {
            return await DoPostAsync<ItemQcLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/itemqc/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ItemQcLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}