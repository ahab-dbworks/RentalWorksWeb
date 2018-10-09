using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PersonalEvent
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PersonalEventController : AppDataController
    {
        public PersonalEventController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PersonalEventLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalEventLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PersonalEventLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/personalevent/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalEventLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PersonalEventLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/personalevent 
        [HttpPost]
        public async Task<ActionResult<PersonalEventLogic>> PostAsync([FromBody]PersonalEventLogic l)
        {
            return await DoPostAsync<PersonalEventLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/personalevent/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
