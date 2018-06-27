using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Holiday
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class HolidayController : AppDataController
    {
        public HolidayController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HolidayLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HolidayLogic>(pageno, pagesize, sort, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<HolidayLogic>(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]HolidayLogic l)
        {
            return await DoPostAsync<HolidayLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/holiday/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}