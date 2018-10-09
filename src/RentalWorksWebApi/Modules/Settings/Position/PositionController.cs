using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Position
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PositionController : AppDataController
    {
        public PositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PositionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position 
        [HttpPost]
        public async Task<ActionResult<PositionLogic>> PostAsync([FromBody]PositionLogic l)
        {
            return await DoPostAsync<PositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/position/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
