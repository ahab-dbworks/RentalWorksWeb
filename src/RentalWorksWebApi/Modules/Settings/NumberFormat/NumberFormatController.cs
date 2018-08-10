using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.NumberFormat
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class NumberFormatController : AppDataController
    {
        public NumberFormatController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(NumberFormatLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/numberformat/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(NumberFormatLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/numberformat/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/numberformat 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<NumberFormatLogic>(pageno, pagesize, sort, typeof(NumberFormatLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/numberformat/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<NumberFormatLogic>(id, typeof(NumberFormatLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/numberformat 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]NumberFormatLogic l)
        //{
        //    return await DoPostAsync<NumberFormatLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/numberformat/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(NumberFormatLogic));
        //}
        ////------------------------------------------------------------------------------------ 
    }
}