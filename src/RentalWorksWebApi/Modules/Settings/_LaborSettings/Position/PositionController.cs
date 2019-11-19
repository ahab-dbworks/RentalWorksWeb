using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborSettings.Position
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ZKb7ET3WoPs2")]
    public class PositionController : AppDataController
    {
        public PositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"CSgiodfm4Gv3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SQKMLiwGHtiJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position 
        [HttpGet]
        [FwControllerMethod(Id:"0SCcUCSgNDEE")]
        public async Task<ActionResult<IEnumerable<PositionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PositionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BmbDTN0hlqyX")]
        public async Task<ActionResult<PositionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position 
        [HttpPost]
        [FwControllerMethod(Id:"GIDNEYljjedr")]
        public async Task<ActionResult<PositionLogic>> PostAsync([FromBody]PositionLogic l)
        {
            return await DoPostAsync<PositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/position/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"DAghbxbvBTj2")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
