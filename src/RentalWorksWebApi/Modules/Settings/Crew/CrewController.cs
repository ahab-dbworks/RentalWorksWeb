using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Crew
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CrewController : AppDataController
    {
        public CrewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewLogic>(pageno, pagesize, sort, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<CrewLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewLogic>(id, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew 
        [HttpPost]
        public async Task<ActionResult<CrewLogic>> PostAsync([FromBody]CrewLogic l)
        {
            return await DoPostAsync<CrewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crew/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}