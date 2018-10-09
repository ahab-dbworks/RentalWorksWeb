using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HolidayLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HolidayLogic>(pageno, pagesize, sort, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<HolidayLogic>(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday 
        [HttpPost]
        public async Task<ActionResult<HolidayLogic>> PostAsync([FromBody]HolidayLogic l)
        {
            return await DoPostAsync<HolidayLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/holiday/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}